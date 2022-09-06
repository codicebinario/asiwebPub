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

Public Class RichiestaRinnovo1
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
        Dim idSelected As String = ""
        'idSelected = Request.QueryString("idSelected")
        idSelected = deEnco.QueryStringDecode(Request.QueryString("idSelected"))
        If Not String.IsNullOrEmpty(idSelected) Then

            Session("idSelected") = idSelected

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
            Dim CodiceEnteRichiedente As String = DettaglioRinnovo.CodiceEnteRichiedente
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
                "<strong> - N.Tessera: </strong>" & datiCF.CodiceTessera & "<br />" &
                "<strong> - Nominativo: </strong>" & datiCF.Nome & " " & datiCF.Cognome &
                "<strong> - Ente Richiedente: </strong>" & DescrizioneEnteRichiedente
        End If

        If Not Page.IsPostBack Then

            '  pnlFase1.Visible = False
            Province()
            leggiDatiEsistenti()


        End If

    End Sub
    Sub Province()

        Dim ds As DataSet

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("webProvinceRegioni")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.AllRecords)

        RequestP.AddSortField("Sigla", Enumerations.Sort.Ascend)

        ds = RequestP.Execute()

        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then


            Dim SingleSport As DataTable = ds.Tables("main").DefaultView.ToTable(True, "Sigla")




            ddlProvinciaResidenza.DataSource = SingleSport


            ddlProvinciaResidenza.DataTextField = "sigla"
            ddlProvinciaResidenza.DataValueField = "sigla"

            ddlProvinciaResidenza.DataBind()
            'ddlSport.DataValueField = "Sport"
            ddlProvinciaResidenza.Items.Insert(0, New ListItem("##", "##"))

            '  LeggiDatiProvincia()
            'ddlProvinciaConsegna.DataSource = SingleSport


            'ddlProvinciaConsegna.DataTextField = "sigla"
            'ddlProvinciaConsegna.DataValueField = "sigla"

            'ddlProvinciaConsegna.DataBind()
            ''ddlSport.DataValueField = "Sport"
            'ddlProvinciaConsegna.Items.Insert(0, New ListItem("##", "##"))


        End If



    End Sub
    Sub leggiDatiEsistenti()

        Dim datiCodiceFiscale As New DatiCodiceFiscaleRinnovi

        datiCodiceFiscale = getDatiCodiceFiscaleRinnovi(Session("idSelected"))

        txtCognome.Text = datiCodiceFiscale.Cognome
        txtNome.Text = datiCodiceFiscale.Nome
        txtCodiceFiscale.Text = datiCodiceFiscale.CodiceFiscale
        txtDataNascita.Text = datiCodiceFiscale.DataNascita.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
        txtComuneNascita.Text = datiCodiceFiscale.Comune
        txtCodiceTessera.Text = datiCodiceFiscale.CodiceTessera
        txtDataScadenza.Text = datiCodiceFiscale.DataScadenza.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)

        txtEmail.Text = datiCodiceFiscale.Email
        txtTelefonoCellulare.Text = datiCodiceFiscale.telefono

        txtIndirizzoResidenza.Text = datiCodiceFiscale.indirizzoResidenza

        txtCapResidenza.Text = datiCodiceFiscale.capResidenza

        txtSport.Text = datiCodiceFiscale.Sport
        txtSpecialita.Text = datiCodiceFiscale.Specialita
        txtLivello.Text = datiCodiceFiscale.Livello
        txtQualifica.Text = datiCodiceFiscale.qualifica

        Dim siglaProvincia As String = getSiglaProvincia(datiCodiceFiscale.Provincia)

        Dim itemProvinciaResidenza As ListItem = ddlProvinciaResidenza.Items.FindByText(siglaProvincia)
        If Not itemProvinciaResidenza Is Nothing Then
            itemProvinciaResidenza.Selected = True
        End If

        'Dim itemComuneResidenza As ListItem = ddlComuneResidenza.Items.FindByText(datiCodiceFiscale.comuneResidenza)
        'If Not itemComuneResidenza Is Nothing Then
        '    itemComuneResidenza.Selected = True
        'End If
        Dim ds2 As DataSet

        Dim fmsP1 As FMSAxml = AsiModel.Conn.Connect()
        fmsP1.SetLayout("webComuniItaliani")
        Dim RequestP2 = fmsP1.CreateFindRequest(Enumerations.SearchType.Subset)
        RequestP2.AddSearchField("sigla", siglaProvincia, Enumerations.SearchOption.equals)
        RequestP2.AddSortField("descrizioneComune", Enumerations.Sort.Ascend)

        ds2 = RequestP2.Execute()
        If Not IsNothing(ds2) AndAlso ds2.Tables("main").Rows.Count > 0 Then


            Dim comuni As DataTable = ds2.Tables("main").DefaultView.ToTable(True, "descrizioneComune")

            ddlComuneResidenza.DataSource = comuni

            ddlComuneResidenza.DataTextField = "descrizioneComune"
            ddlComuneResidenza.DataValueField = "descrizioneComune"

            ddlComuneResidenza.DataBind()
            ddlComuneResidenza.Items.Insert(0, New ListItem("##", "##"))
            leggiDatiComune()
        End If

    End Sub



    Sub leggiDatiComune()

        Dim datiCodiceFiscale As New DatiCodiceFiscaleRinnovi

        datiCodiceFiscale = getDatiCodiceFiscaleRinnovi(Session("idSelected"))


        Dim itemComuneResidenza As ListItem = ddlComuneResidenza.Items.FindByText(datiCodiceFiscale.comuneResidenza)
        If Not itemComuneResidenza Is Nothing Then
            itemComuneResidenza.Selected = True
        End If

    End Sub

    'Protected Sub ddlProvinciaConsegna_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProvinciaConsegna.SelectedIndexChanged
    '    Dim selezionato As String = ddlProvinciaConsegna.SelectedItem.Value
    '    Dim ds2 As DataSet

    '    Dim fmsP1 As FMSAxml = AsiModel.Conn.Connect()
    '    fmsP1.SetLayout("webComuniItaliani")
    '    Dim RequestP2 = fmsP1.CreateFindRequest(Enumerations.SearchType.Subset)
    '    RequestP2.AddSearchField("sigla", selezionato, Enumerations.SearchOption.equals)
    '    RequestP2.AddSortField("descrizioneComune", Enumerations.Sort.Ascend)

    '    ds2 = RequestP2.Execute()

    '    If Not IsNothing(ds2) AndAlso ds2.Tables("main").Rows.Count > 0 Then

    '        'For Each dr In ds.Tables("main").Rows

    '        '    Response.Write("Sport: " & dr("Sport") & " - Disciplina: " & dr("Disciplina") & " - Specialità: " & dr("Specialita") & "<br />")


    '        Dim comuni As DataTable = ds2.Tables("main").DefaultView.ToTable(True, "descrizioneComune")

    '        ddlComuneConsegna.DataSource = comuni

    '        ddlComuneConsegna.DataTextField = "descrizioneComune"
    '        ddlComuneConsegna.DataValueField = "descrizioneComune"

    '        ddlComuneConsegna.DataBind()

    '        ddlComuneConsegna.Items.Insert(0, New ListItem("##", "##"))
    '        ' ddlProvincia.Items.Clear()

    '    End If
    'End Sub

    Protected Sub ddlProvinciaResidenza_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProvinciaResidenza.SelectedIndexChanged
        Dim selezionato As String = ddlProvinciaResidenza.SelectedItem.Value
        Dim ds2 As DataSet

        Dim fmsP1 As FMSAxml = AsiModel.Conn.Connect()
        fmsP1.SetLayout("webComuniItaliani")
        Dim RequestP2 = fmsP1.CreateFindRequest(Enumerations.SearchType.Subset)
        RequestP2.AddSearchField("sigla", selezionato, Enumerations.SearchOption.equals)
        RequestP2.AddSortField("descrizioneComune", Enumerations.Sort.Ascend)

        ds2 = RequestP2.Execute()

        If Not IsNothing(ds2) AndAlso ds2.Tables("main").Rows.Count > 0 Then


            Dim comuni As DataTable = ds2.Tables("main").DefaultView.ToTable(True, "descrizioneComune")

            ddlComuneResidenza.DataSource = comuni

            ddlComuneResidenza.DataTextField = "descrizioneComune"
            ddlComuneResidenza.DataValueField = "descrizioneComune"

            ddlComuneResidenza.DataBind()




            ddlComuneResidenza.Items.Insert(0, New ListItem("##", "##"))
            leggiDatiComune()
            ' ddlProvincia.Items.Clear()

        End If
    End Sub

    Protected Sub chkStampaCartacea_CheckedChanged(sender As Object, e As EventArgs) Handles chkStampaCartacea.CheckedChanged
        If chkStampaCartacea.Checked = True Then

            pnlDatiConsegna.Visible = True
        ElseIf chkStampaCartacea.Checked = False Then
            pnlDatiConsegna.Visible = False
        End If
    End Sub


    Protected Sub chkcopia_CheckedChanged(sender As Object, e As EventArgs) Handles chkCopia.CheckedChanged
        If chkCopia.Checked Then


            txtIndirizzoConsegna.Text = txtIndirizzoResidenza.Text
            If ddlProvinciaResidenza.SelectedIndex > 1 Then
                txtProvinciaConsegna.Text = ddlProvinciaResidenza.SelectedItem.Text
            End If
            If ddlComuneResidenza.SelectedIndex > 1 Then
                txtComuneConsegna.Text = ddlComuneResidenza.SelectedItem.Text

            End If

            txtCapConsegna.Text = txtCapResidenza.Text



        End If

    End Sub

    Protected Sub btnFase3_Click(sender As Object, e As EventArgs) Handles btnFase3.Click
        If Page.IsValid Then

            Dim ritorno As Boolean


            ritorno = CaricaDatiDaSceltaRinnovo(Session("IDRinnovo"), Session("id_record"), Session("idScelto"))

            If ritorno = True Then
                Session("visto") = "ok"
                Session("rinnovoAggiunto") = "OK"
                Response.Redirect("dashboardRinnovi.aspx?ris=" & deEnco.QueryStringEncode("ok"))
            ElseIf ritorno = False Then
                Response.Redirect("dashboardRinnovi.aspx?ris=" & deEnco.QueryStringEncode("pr"))
                Session("visto") = "ok"
            End If

        End If
    End Sub
    Public Function CaricaDatiDaSceltaRinnovo(IDRinnovo As String, idrecord As String, idScelto As String) As Boolean
        '  Dim litNumRichieste As Literal = DirectCast(ContentPlaceHolder1.FindControl("LitNumeroRichiesta"), Literal)

        Dim ritorno As Boolean = False


        Dim datiAlbo As New DatiCodiceFiscaleRinnovi

        datiAlbo = getDatiCodiceFiscaleRinnovi(idScelto)




        Dim fmsP As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
        '  Dim ds As DataSet
        Dim risposta As String = ""
        fmsP.SetLayout("webRinnoviRichiesta")
        Dim Request = fmsP.CreateEditRequest(idrecord)



        Request.AddField("Asi_CodiceFiscale", datiAlbo.CodiceFiscale)
        Request.AddField("Asi_CodiceTessera", datiAlbo.CodiceTessera)
        Request.AddField("Asi_Nome", datiAlbo.Nome)
        Request.AddField("Asi_Cognome", datiAlbo.Cognome)
        Request.AddField("Asi_DataScadenza", Data.SistemaData(datiAlbo.DataScadenza))
        Request.AddField("Asi_LuogoNascita", datiAlbo.Comune)
        Request.AddField("Asi_Datanascita", Data.SistemaData(datiAlbo.DataNascita))
        Request.AddField("Asi_Email", datiAlbo.Email)
        Request.AddField("Asi_Telefono", datiAlbo.telefono)
        Request.AddField("Asi_sport", datiAlbo.Sport)
        Request.AddField("Asi_specialita", datiAlbo.Specialita)
        Request.AddField("Asi_livello", datiAlbo.Livello)
        Request.AddField("Asi_qualifica", datiAlbo.qualifica)





        Request.AddField("Rin_IndirizzoResidenza", Data.PrendiStringaT(Server.HtmlEncode(txtIndirizzoResidenza.Text)))
        Request.AddField("Rin_CapResidenza", Data.PrendiStringaT(Server.HtmlEncode(txtCapResidenza.Text)))
        Request.AddField("Rin_ComuneResidenza", ddlComuneResidenza.SelectedItem.Text)
        Request.AddField("Rin_ProvinciaResidenza", ddlProvinciaResidenza.SelectedItem.Text)
        If chkStampaCartacea.Checked = True Then
            Request.AddField("Rin_StampaCartaceo", "Si")

            Request.AddField("Rin_IndirizzoConsegna", Data.PrendiStringaT(Server.HtmlEncode(txtIndirizzoConsegna.Text)))
            Request.AddField("Rin_CapConsegna", Data.PrendiStringaT(Server.HtmlEncode(txtCapConsegna.Text)))
            Request.AddField("Rin_ComuneConsegna", txtComuneConsegna.Text)
            Request.AddField("Rin_ProvinciaConsegna", Data.PrendiStringaT(Server.HtmlEncode(txtProvinciaConsegna.Text)))


        End If
        Request.AddField("Codice_Status", "152")

        Try
            risposta = Request.Execute()

            ritorno = True

        Catch ex As Exception
            ritorno = False
        End Try

        AsiModel.LogIn.LogCambioStatus(IDRinnovo, "152", Session("WebUserEnte"), "rinnovo")


        ' occorre scrivere il codice che aggiorna albo con i dati giusti

        Return ritorno
    End Function

End Class