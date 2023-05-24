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
Imports System.Globalization
Imports System.Threading

Public Class RichiestaRinnovo2
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
        btnConcludi.Attributes.Add("OnClick", String.Format("this.disabled = true; {0};", ClientScript.GetPostBackEventReference(btnConcludi, Nothing)))


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
        'record_ID = deEnco.QueryStringDecode(Request.QueryString("record_ID"))
        'If Not String.IsNullOrEmpty(record_ID) Then

        '    Session("id_record") = record_ID

        'End If
        Dim cf As String = ""
        cf = deEnco.QueryStringDecode(Request.QueryString("cf"))
        If Not String.IsNullOrEmpty(cf) Then

            Session("cf") = cf

        End If


        If IsNothing(Session("cf")) Then
            Response.Redirect("../login.aspx")
        End If
        Dim codR As String = ""
        codR = deEnco.QueryStringDecode(Request.QueryString("codR"))
        If Not String.IsNullOrEmpty(codR) Then


            Session("IDRinnovo") = codR

        End If

        If Not Page.IsPostBack Then

            '  pnlFase1.Visible = False
            rinnoviCF(Session("cf"))

        End If
    End Sub

    Function TestEnteAffiliante(cf As String) As Boolean
        Dim risposta As Boolean = False
        Dim ds As DataSet
        Dim codiceAffialiante As String = ""
        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("WebAlbo")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        ' RequestP.AddSearchField("pre_stato_web", "1")
        RequestP.AddSearchField("Codice Fiscale", cf, Enumerations.SearchOption.equals)
        RequestP.AddSearchField("RinnovoFlagVar", "1", Enumerations.SearchOption.equals)
        RequestP.AddSearchField("CodiceEnteAffiliante", 0, Enumerations.SearchOption.biggerThan)
        'RequestP.AddSortField("scadenza", Enumerations.Sort.Ascend)
        '  RequestP.AddSortField("IDEquiparazione", Enumerations.Sort.Descend)
        Try


            ds = RequestP.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

                risposta = True
            Else
                risposta = False
            End If
        Catch ex As Exception
            risposta = False
        End Try
        Return risposta
    End Function
    Function TestVar(cf As String) As Boolean
        Dim risposta As Boolean = False
        Dim ds As DataSet
        Dim codiceAffialiante As String = ""
        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("WebAlbo")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        ' RequestP.AddSearchField("pre_stato_web", "1")
        RequestP.AddSearchField("Codice Fiscale", cf, Enumerations.SearchOption.equals)
        RequestP.AddSearchField("RinnovoFlagVar", "1", Enumerations.SearchOption.equals)
        '  RequestP.AddSearchField("CodiceEnteAffiliante", 0, Enumerations.SearchOption.biggerThan)
        'RequestP.AddSortField("scadenza", Enumerations.Sort.Ascend)
        '  RequestP.AddSortField("IDEquiparazione", Enumerations.Sort.Descend)

        'nome cognome e datadinascita e rinnovoFlagVar = 1
        Try


            ds = RequestP.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

                risposta = True
            Else
                risposta = False
            End If
        Catch ex As Exception
            risposta = False
        End Try
        Return risposta
    End Function
    Function TestCf(cf As String) As Boolean
        Dim risposta As Boolean = False
        Dim ds As DataSet
        Dim codiceAffialiante As String = ""
        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("WebAlbo")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        ' RequestP.AddSearchField("pre_stato_web", "1")
        RequestP.AddSearchField("Codice Fiscale", cf, Enumerations.SearchOption.equals)
        '  RequestP.AddSearchField("RinnovoFlagVar", "1", Enumerations.SearchOption.equals)
        '  RequestP.AddSearchField("CodiceEnteAffiliante", 0, Enumerations.SearchOption.biggerThan)
        'RequestP.AddSortField("scadenza", Enumerations.Sort.Ascend)
        '  RequestP.AddSortField("IDEquiparazione", Enumerations.Sort.Descend)
        Try


            ds = RequestP.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

                risposta = True
            Else
                risposta = False
            End If
        Catch ex As Exception
            risposta = False
        End Try
        Return risposta
    End Function


    Sub rinnoviCF(cf As String)
        Dim risultato As String = ""
        Dim rispostaCF As Boolean = False
        Dim rispostaVar As Boolean = False
        Dim rispostaEA As Boolean = False
        Dim nota As String = ""
        Dim ds As DataSet
        Dim codiceAffialiante As String = ""
        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("WebAlbo")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        ' RequestP.AddSearchField("pre_stato_web", "1")
        RequestP.AddSearchField("Codice Fiscale", cf, Enumerations.SearchOption.equals)
        RequestP.AddSearchField("RinnovoFlagVar", "1", Enumerations.SearchOption.equals)
        RequestP.AddSearchField("CodiceEnteAffiliante", 0, Enumerations.SearchOption.biggerThan)
        RequestP.AddSortField("scadenza", Enumerations.Sort.Ascend)
        '  RequestP.AddSortField("IDEquiparazione", Enumerations.Sort.Descend)
        Try


            ds = RequestP.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

                Dim counter1 As Integer = 0

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
                rispostaCF = TestCf(cf)
                If rispostaCF = True Then
                    rispostaVar = TestVar(cf)
                    If rispostaVar = True Then
                        rispostaEA = TestEnteAffiliante(cf)

                        If rispostaEA = False Then
                            nota = "noEA"
                        End If
                    Else
                        nota = "toNorma"
                    End If
                Else
                    nota = "noCF"
                End If

                risultato = ""

                Session("procedi") = "KO"
                Response.Redirect("DashboardRinnovi2.aspx?ris=" & deEnco.QueryStringEncode(nota))


            End If
        Catch ex As Exception
            Response.Redirect("../FriendlyMessage.aspx")
        End Try
    End Sub



    Public Function CaricaDatiDocumentoRinnovo(codR As String, codiceEnte As String, codiceFiscale As String,
                                             nome As String, cognome As String, codiceTessera As String, dataScadenza As String, comuneNascita As String, datanascita As String) As Integer

        Dim idRecord As Integer = 0
        Dim SettaggioCulture As CultureInfo = CultureInfo.CreateSpecificCulture("it-IT")
        Thread.CurrentThread.CurrentCulture = SettaggioCulture
        Thread.CurrentThread.CurrentUICulture = SettaggioCulture
        Dim DataScadenzaPulita As String

        DataScadenzaPulita = DateTime.Parse(Data.SonoDieci(dataScadenza), SettaggioCulture)

        Dim fmsP As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
        '  Dim ds As DataSet
        Dim risposta As Integer = 0
        fmsP.SetLayout("webRinnoviRichiesta2")

        Dim Request = fmsP.CreateNewRecordRequest()

        Request.AddField("IDRinnovoM", codR)
        Request.AddField("Codice_Status", "0")


        Request.AddField("Rin_CFVerificatoTessera", "1")
        Request.AddField("Codice_Ente_Richiedente", codiceEnte)
        Request.AddField("Rin_CodiceFiscale", codiceFiscale)
        Request.AddField("Rin_NumeroTessera", codiceTessera)
        Request.AddField("Rin_Nome", nome)
        Request.AddField("Rin_Cognome", cognome)
        'Request.AddField("Data_ScadenzaTesseraASI", Data.SistemaData(dataScadenza))
        Dim miaDataScadenza As DateTime
        Dim miaDataScadenza2 As DateTime

        Request.AddField("Data_ScadenzaTesseraASI", Data.SistemaDataUK(DataScadenzaPulita))


        Request.AddField("Rin_ComuneNascita", comuneNascita)
        Request.AddField("Rin_DataNascita", Data.SonoDieci(datanascita))

        risposta = Request.Execute()


        Return risposta
    End Function

    Protected Sub btnConcludi_Click(sender As Object, e As EventArgs) Handles btnConcludi.Click

        Dim idrecord As Integer
        Dim DettaglioRinnovo As New DatiNuovoRinnovo
        DettaglioRinnovo = AsiModel.Rinnovi.CaricaDatiTesseramento(Session("cf"))

        idrecord = CaricaDatiDocumentoRinnovo(Session("IDRinnovo"), Session("codice"), Session("cf"),
         DettaglioRinnovo.Nome, DettaglioRinnovo.Cognome, DettaglioRinnovo.CodiceTessera, DettaglioRinnovo.DataScadenza, DettaglioRinnovo.ComuneNascita, DettaglioRinnovo.DataNascita)

        Dim datiAlbo As New DatiCodiceFiscaleRinnovi

        datiAlbo = getDatiCodiceFiscaleRinnovi(Session("idScelto"))

        Dim SameCode As Integer = String.Compare(datiAlbo.codiceEnteEx, Session("codice"))
        If SameCode = 0 Then

            '    AsiModel.LogIn.LogCambioStatus(Session("IDRinnovo"), "151", Session("WebUserEnte"), "rinnovo")


            Response.Redirect("richiestaRinnovo12.aspx?idSelected=" & deEnco.QueryStringEncode(Session("idScelto")) & "&codR=" & deEnco.QueryStringEncode(Session("IDRinnovo")) & "&record_ID=" & deEnco.QueryStringEncode(idrecord))

        Else

            Response.Redirect("upDichiarazione2.aspx?idSelected=" & deEnco.QueryStringEncode(Session("idScelto")) & "&codR=" & deEnco.QueryStringEncode(Session("IDRinnovo")) & "&record_ID=" & deEnco.QueryStringEncode(idrecord))

        End If



    End Sub

    Protected Sub btnCF_Click(sender As Object, e As EventArgs) Handles btnCF.Click
        'Dim message As RadioButtonList = CType(phDash.FindControl("CFPresenti"), RadioButtonList)
        If Page.IsValid Then


            lblScelta.Visible = True
            lblScelta.Text = "<small><strong>Hai selezionato per il rinnovo:</strong> <br />" & ddlCF.SelectedItem.Text & "</small>"

            Dim idScelto As String = ""
            idScelto = ddlCF.SelectedValue.ToString
            Session("idScelto") = idScelto
            ddlCF.ClearSelection()
            btnConcludi.Visible = True
            'btnConcludi.Visible = True
        End If
    End Sub

End Class