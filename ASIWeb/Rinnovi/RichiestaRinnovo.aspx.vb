Imports fmDotNet
Imports System.Web.Services
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed
Imports System.IO
Imports OboutInc.FileUpload

Imports System.Net.Mail
Imports System.Web.UI.WebControls

Imports System.Security.Cryptography

Imports System.Threading.Tasks
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Imports System.Web.Script.Serialization
Imports RestSharp

Imports System.Collections.Generic
Imports System.Net.Security
Imports System.Net
Imports System.Web.Services.Description
Imports System.EnterpriseServices.CompensatingResourceManager
Imports System.Security.Policy
Imports System.Windows
Imports System.Runtime.InteropServices

Public Class RichiestaRinnovo
    Inherits System.Web.UI.Page
    Dim webserver As String = ConfigurationManager.AppSettings("webserver")
    Dim utente As String = ConfigurationManager.AppSettings("utente")
    Dim porta As String = ConfigurationManager.AppSettings("porta")
    Dim pass As String = ConfigurationManager.AppSettings("pass")
    Dim dbb As String = ConfigurationManager.AppSettings("dbb")
    Dim cultureFormat As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("it-IT")
    Dim deEnco As New Ed()
    Const MassimoPeso As Integer = 3102400
    Const FileType As String = "image/*"
    ' in pixel
    Const massimaaltezza As Integer = 140
    Const massinalarghezza As Integer = 100

    Dim ext As String = " "
    Dim nomefileReale As String = " "
    Dim qualeStatus As String = ""
    Dim nomecaricato As String = ""
    Dim codiceFiscale As String
    Dim tokenZ As String = ""
    Dim CodiceEnteRichiedente As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If
        If IsNothing(Session("codice")) Then
            Response.Redirect("../login.aspx")
        End If


        'If Session("procedi") <> "OK" Then

        '    Response.Redirect("checkTesseramento.aspx")

        'End If

        '  Dim newCulture As System.Globalization.CultureInfo = System.Globalization.CultureInfo.CurrentUICulture.Clone()
        cultureFormat.NumberFormat.CurrencySymbol = "€"
        cultureFormat.NumberFormat.CurrencyDecimalDigits = 2
        cultureFormat.NumberFormat.CurrencyGroupSeparator = String.Empty
        cultureFormat.NumberFormat.CurrencyDecimalSeparator = ","
        System.Threading.Thread.CurrentThread.CurrentCulture = cultureFormat
        System.Threading.Thread.CurrentThread.CurrentUICulture = cultureFormat




        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If
        Dim record_ID As String = ""
        record_ID = deEnco.QueryStringDecode(Request.QueryString("record_ID"))
        If Not String.IsNullOrEmpty(record_ID) Then

            Session("id_record") = record_ID

        End If

        If IsNothing(Session("id_record")) Then
            Response.Redirect("../login.aspx")
        End If
        Dim codR As String = ""
        codR = deEnco.QueryStringDecode(Request.QueryString("codR"))
        If Not String.IsNullOrEmpty(codR) Then


            Session("IDRinnovo") = codR
            Dim DettaglioRinnovo As New DatiNuovoRinnovo





            DettaglioRinnovo = Rinnovi.PrendiValoriNuovoRinnovo(Session("IDRinnovo"))
            Dim verificato As String = DettaglioRinnovo.RinnovoCF
            If verificato = "0" Then
                Response.Redirect("DashboardRinnovi.aspx?ris=" & deEnco.QueryStringEncode("no"))

            End If
            Dim IDRinnovo As String = DettaglioRinnovo.IDRinnovo
            CodiceEnteRichiedente = DettaglioRinnovo.CodiceEnteRichiedente
            Dim DescrizioneEnteRichiedente As String = DettaglioRinnovo.DescrizioneEnteRichiedente
            Dim TipoEnte As String = DettaglioRinnovo.TipoEnte
            Dim CodiceStatus As String = DettaglioRinnovo.CodiceStatus
            Dim DescrizioneStatus As String = DettaglioRinnovo.DescrizioneStatus
            '      leggiDatiEsistenti(Session("codiceFiscale"))

            HiddenIdRecord.Value = DettaglioRinnovo.IdRecord
            HiddenIDRinnovo.Value = DettaglioRinnovo.IDRinnovo
            codiceFiscale = DettaglioRinnovo.CodiceFiscale
            Dim datiCF = AsiModel.getDatiCodiceFiscale(codiceFiscale)

            lblIntestazioneRinnovo.Text = "<strong>IDRinnovo: </strong>" & IDRinnovo &
                "<strong> - Codice Fiscale: </strong>" & datiCF.CodiceFiscale &
                "<strong> - Tessera Ass.: </strong>" & datiCF.CodiceTessera & "<br />" &
                "<strong> - Nominativo: </strong>" & datiCF.Nome & " " & datiCF.Cognome &
                "<strong> - Ente Richiedente: </strong>" & DescrizioneEnteRichiedente
        End If

        If Not Page.IsPostBack Then

            '  pnlFase1.Visible = False
            rinnoviCF()

        End If
    End Sub
    Sub rinnoviCF()

        Dim ds As DataSet
        Dim codiceAffialiante As String = ""
        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("WebAlbo")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        ' RequestP.AddSearchField("pre_stato_web", "1")
        RequestP.AddSearchField("Codice Fiscale", codiceFiscale, Enumerations.SearchOption.equals)
        RequestP.AddSearchField("RinnovoFlagVar", "1", Enumerations.SearchOption.equals)
        RequestP.AddSearchField("CodiceEnteAffiliante", 0, Enumerations.SearchOption.biggerThan)
        RequestP.AddSortField("scadenza", Enumerations.Sort.Ascend)
        '  RequestP.AddSortField("IDEquiparazione", Enumerations.Sort.Descend)

        ds = RequestP.Execute()

        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then





            Dim cf As DataTable = ds.Tables("main")



            '  phDash.Visible = True
            'Dim counter As Integer = 0
            Dim counter1 As Integer = 0
            'Dim totale As Decimal = 0
            '  For Each dr In ds.Tables("main").Rows



            counter1 += 1
            '  Response.Write("cf: " & Data.FixNull(dr("codice fiscale")) & "<br />")

            Dim deEnco As New Ed()
            ddlCF.Visible = True
            ddlCF.Font.Size = FontUnit.Small
            ddlCF.RepeatLayout = RepeatLayout.Table
            ddlCF.RepeatDirection = RepeatDirection.Vertical
            ddlCF.DataSource = ds
            '  ddlCF.DataSourceID = "IDRecord"
            ddlCF.DataTextField = "descrizione"
            ddlCF.DataValueField = "IDRecord"
            ddlCF.DataBind()





            counter1 += 1
            '  Next
        Else


            Session("procedi") = "KO"
            Response.Redirect("DashboardRinnovi.aspx?ris=" & deEnco.QueryStringEncode("koCFAlbo"))


        End If

    End Sub

    'Protected Sub btnCF_Click(sender As Object, e As EventArgs) Handles btnCF.Click

    '    'Dim message As RadioButtonList = CType(phDash.FindControl("CFPresenti"), RadioButtonList)
    '    If Page.IsValid Then


    '        lblScelta.Visible = True
    '    lblScelta.Text = "<small><strong>Hai selezionato per il rinnovo:</strong> <br />" & ddlCF.SelectedItem.Text & "</small>"

    '        Dim idScelto As String = ""
    '        idScelto = ddlCF.SelectedValue.ToString
    '        Session("idScelto") = idScelto
    '        ddlCF.ClearSelection()
    '        BtnAvanti.Visible = True
    '    End If
    'End Sub

    'Protected Sub BtnAvanti_Click(sender As Object, e As EventArgs) Handles BtnAvanti.Click

    '    AsiModel.LogIn.LogCambioStatus(Session("IDRinnovo"), "151", Session("WebUserEnte"), "rinnovo")
    '    Response.Redirect("richiestaRinnovo1.aspx?idSelected=" & deEnco.QueryStringEncode(Session("idScelto")) & "&codR=" & deEnco.QueryStringEncode(Session("IDRinnovo")) & "&record_ID=" & deEnco.QueryStringEncode(Session("id_record")))

    'End Sub

    Protected Sub lnkCF_Click(sender As Object, e As EventArgs) Handles lnkCF.Click
        'Dim message As RadioButtonList = CType(phDash.FindControl("CFPresenti"), RadioButtonList)
        If Page.IsValid Then


            lblScelta.Visible = True
            lblScelta.Text = "<small><strong>Hai selezionato per il rinnovo:</strong> <br />" & ddlCF.SelectedItem.Text & "</small>"

            Dim idScelto As String = ""
            idScelto = ddlCF.SelectedValue.ToString
            Session("idScelto") = idScelto
            ddlCF.ClearSelection()
            lnkAvanti.Visible = True
        End If
    End Sub

    Protected Sub lnkAvanti_Click(sender As Object, e As EventArgs) Handles lnkAvanti.Click
        AsiModel.LogIn.LogCambioStatus(Session("IDRinnovo"), "151", Session("WebUserEnte"), "rinnovo")
        Response.Redirect("richiestaRinnovo1.aspx?idSelected=" & deEnco.QueryStringEncode(Session("idScelto")) & "&codR=" & deEnco.QueryStringEncode(Session("IDRinnovo")) & "&record_ID=" & deEnco.QueryStringEncode(Session("id_record")))

    End Sub
End Class