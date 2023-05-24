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

Public Class RichiestaRinnovo12
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

        If Session("RinnovoModificaDataEmissione") = "S" Then

            pnlModificaDataEmissione.Visible = True
            pnlDataEmissione.Visible = False
        Else
            pnlModificaDataEmissione.Visible = False
            pnlDataEmissione.Visible = True
        End If
        Dim dataCorrente As Date = Now.ToShortDateString
        Dim annoCorrente As Integer = Now.Year
        Dim meseCorrente As Integer = Now.Month
        Dim giornoCorrente As Integer = Now.Day
        Dim dataInizio As Date
        Dim dataFine As Date
        dataInizio = "01-01-" & annoCorrente
        dataFine = "31-12-" & annoCorrente
        Calendar1.DateMin = dataInizio
        Calendar1.DateMax = dataFine

        lnkConcludi.Attributes.Add("OnClick", String.Format("this.disabled = true; {0};", ClientScript.GetPostBackEventReference(lnkConcludi, Nothing)))

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


            DettaglioRinnovo = Rinnovi.PrendiValoriNuovoRinnovo2(Session("id_record"))
            Dim verificato As String = DettaglioRinnovo.RinnovoCF
            If verificato = "0" Then
                Response.Redirect("DashboardRinnovi2.aspx?ris=" & deEnco.QueryStringEncode("no"))

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

            lblIntestazioneRinnovo.Text = "" &
                "<strong> - Codice Fiscale: </strong>" & datiCF.CodiceFiscale &
                "<strong> - Tessera Ass.: </strong>" & datiCF.CodiceTessera & "<br />" &
                "<strong> - Nominativo: </strong>" & datiCF.Nome & " " & datiCF.Cognome &
                "<strong> - Ente Richiedente: </strong>" & DescrizioneEnteRichiedente
        End If

        If Not Page.IsPostBack Then

            '  pnlFase1.Visible = False
            Province()
            leggiDatiEsistenti()


        End If

    End Sub
    Protected Sub validator33_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles validator33.ServerValidate

        If String.IsNullOrEmpty(txtDataEmissioneM.Text) Then
            args.IsValid = False

        Else
            Dim annoCorrente = Now.Year()
            Dim annoInserito = Right(txtDataEmissioneM.Text, 4)

            If annoCorrente = annoInserito Then
                args.IsValid = True
                avvisoData.Text = ""
            Else
                args.IsValid = False
                validator33.ErrorMessage = "Inserire una data dell'anno corrente"
                '    avvisoData.Text = "Inserire data anno corrente"
            End If


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
        txtCodiceIscrizione.Text = datiCodiceFiscale.codiceIscrizione
        txtEmail.Text = datiCodiceFiscale.Email
        txtTelefonoCellulare.Text = datiCodiceFiscale.telefono
        txtDisciplina.Text = datiCodiceFiscale.disciplina
        txtIndirizzoResidenza.Text = datiCodiceFiscale.indirizzoResidenza

        txtCapResidenza.Text = datiCodiceFiscale.capResidenza

        txtSport.Text = datiCodiceFiscale.Sport
        txtSpecialita.Text = datiCodiceFiscale.Specialita
        txtLivello.Text = datiCodiceFiscale.Livello
        txtQualifica.Text = datiCodiceFiscale.qualifica
        txtDataEmissione.Text = Now.ToShortDateString
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
            If ddlProvinciaResidenza.SelectedIndex > 0 Then
                txtProvinciaConsegna.Text = ddlProvinciaResidenza.SelectedItem.Text
            End If
            If ddlComuneResidenza.SelectedIndex > 0 Then
                txtComuneConsegna.Text = ddlComuneResidenza.SelectedItem.Text

            End If

            txtCapConsegna.Text = txtCapResidenza.Text
            txtTelefono.Text = txtTelefonoCellulare.Text


        End If

    End Sub


    Public Function CaricaDatiDaSceltaRinnovo(IDRinnovo As String, idrecord As String, idScelto As String) As Boolean
        '  Dim litNumRichieste As Literal = DirectCast(ContentPlaceHolder1.FindControl("LitNumeroRichiesta"), Literal)
        Dim SettaggioCulture As CultureInfo = CultureInfo.CreateSpecificCulture("it-IT")
        Thread.CurrentThread.CurrentCulture = SettaggioCulture
        Thread.CurrentThread.CurrentUICulture = SettaggioCulture
        Dim ritorno As Boolean = False


        Dim datiAlbo As New DatiCodiceFiscaleRinnovi

        datiAlbo = getDatiCodiceFiscaleRinnovi(idScelto)




        Dim fmsP As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
        '  Dim ds As DataSet
        Dim risposta As String = ""
        fmsP.SetLayout("webRinnoviRichiesta2")
        Dim Request = fmsP.CreateEditRequest(idrecord)



        Request.AddField("Asi_CodiceFiscale", datiAlbo.CodiceFiscale)
        Request.AddField("Asi_CodiceTessera", datiAlbo.CodiceTessera)
        Request.AddField("Asi_Nome", datiAlbo.Nome)
        Request.AddField("Asi_Cognome", datiAlbo.Cognome)

        Dim miaDataScadenza As DateTime

        Dim DataScadenzaPulita As String

        DataScadenzaPulita = DateTime.Parse(Data.SonoDieci(datiAlbo.DataScadenza), SettaggioCulture)
        'If DateTime.Parse(DataScadenzaPulita) Then
        Request.AddField("Asi_DataScadenza", Data.SistemaDataUK(DataScadenzaPulita))
        'miaDataScadenza2 = miaDataScadenza.ToString("MM/dd/yyyy hh:mm:ss")
        '  Request.AddField("Data_ScadenzaTesseraASI", miaDataScadenza.ToString("dd/MM/yyyy", New CultureInfo("it-IT")))
        'End If



        '  Request.AddField("Asi_DataScadenza", Data.SistemaData(datiAlbo.DataScadenza))
        Request.AddField("Asi_LuogoNascita", datiAlbo.Comune)
        Request.AddField("Asi_Datanascita", Data.SistemaData(datiAlbo.DataNascita))
        Request.AddField("Asi_Email", datiAlbo.Email)
        Request.AddField("Asi_Telefono", datiAlbo.telefono)
        Request.AddField("Asi_sport", datiAlbo.Sport)
        Request.AddField("Asi_specialita", datiAlbo.Specialita)
        If String.IsNullOrEmpty(datiAlbo.Livello) Then
            Request.AddField("Asi_livello", "ND")
        Else
            Request.AddField("Asi_livello", datiAlbo.Livello)
        End If

        Request.AddField("Asi_qualifica", datiAlbo.qualifica)
        Request.AddField("Asi_CodiceIscrizione", datiAlbo.qualifica)
        Request.AddField("Asi_Disciplina", datiAlbo.disciplina)
        Request.AddField("Asi_CodiceIscrizione", datiAlbo.codiceIscrizione)
        Request.AddField("ASI_CodiceEnteEx", datiAlbo.codiceEnteEx)
        Request.AddField("ASI_NomeEnteEx", datiAlbo.nomeEnteEx)
        Request.AddField("Codice_Status", "152")
        Request.AddField("Rin_IndirizzoResidenza", Data.PrendiStringaT(Server.HtmlEncode(txtIndirizzoResidenza.Text)))
        Request.AddField("Rin_CapResidenza", Data.PrendiStringaT(Server.HtmlEncode(txtCapResidenza.Text)))
        Request.AddField("Rin_ComuneResidenza", ddlComuneResidenza.SelectedItem.Text)
        Request.AddField("Rin_ProvinciaResidenza", ddlProvinciaResidenza.SelectedItem.Text)

        If Session("RinnovoModificaDataEmissione") = "S" Then
            Dim dataSelezionata As Date = txtDataEmissioneM.Text
            Dim annoAttuale As Integer = Now.Year
            Dim mese As Integer = dataSelezionata.Month
            Dim anno As Integer = dataSelezionata.Year
            If mese = 12 Then
                Request.AddField("Data_Emissione", "01/01/" & annoAttuale + 1)
            Else
                Request.AddField("Data_Emissione", Data.SistemaDataUK(Data.SonoDieci(dataSelezionata)))
            End If


        Else
            Dim dataSelezionata As Date = txtDataEmissione.Text
            Dim annoAttuale As Integer = Now.Year
            Dim mese As Integer = dataSelezionata.Month
            Dim anno As Integer = dataSelezionata.Year
            If mese = 12 Then
                Request.AddField("Data_Emissione", "01/01/" & annoAttuale + 1)
            Else
                Request.AddField("Data_Emissione", Data.SistemaDataUK(txtDataEmissione.Text))
            End If
        End If




        If chkStampaCartacea.Checked = True Then

            Request.AddField("Rin_StampaCartaceo", "Si")
            Request.AddField("Rin_IndirizzoConsegna", Data.PrendiStringaT(Server.HtmlEncode(txtIndirizzoConsegna.Text)))
            Request.AddField("Rin_CapConsegna", Data.PrendiStringaT(Server.HtmlEncode(txtCapConsegna.Text)))
            Request.AddField("Rin_ComuneConsegna", Data.PrendiStringaT(Server.HtmlEncode(txtComuneConsegna.Text)))
            Request.AddField("Rin_ProvinciaConsegna", Data.PrendiStringaT(Server.HtmlEncode(txtProvinciaConsegna.Text)))
            Request.AddField("Rin_Telefono", Data.PrendiStringaT(Server.HtmlEncode(txtTelefono.Text)))
        End If
        If chkEA.Checked Then
            Request.AddField("Rin_InviaA", "EA")
        Else
            Request.AddField("Rin_InviaA", "T")
        End If






        ' Try
        risposta = Request.Execute()


        If Not IsNothing(Session("IdRecordMaster")) Then




            Dim rispostax As Integer
        Dim fmsPx As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
        fmsPx.SetLayout("webRinnoviMaster")
        '    Dim Requestx = fmsP.CreateDeleteRequest(Session("IdRecordMaster"))
        Dim Requestx = fmsPx.CreateEditRequest(Session("IdRecordMaster"))



        Requestx.AddField("CodiceStatus", "152")
            ' Try
            rispostax = Requestx.Execute()
            AsiModel.LogIn.LogCambioStatus(IDRinnovo, "152", Session("WebUserEnte"), "rinnovo")
        End If
        '  Catch ex As Exception

        ' End Try

        'Dim rispostaY As Integer

        'fmsP.SetLayout("webRinnovoMaster")
        ''    Dim Requestx = fmsP.CreateDeleteRequest(Session("IdRecordMaster"))
        'Dim RequestY = fmsP.CreateEditRequest(Session("IdRecordMaster"))



        'RequestY.AddField("CodiceStatus", "152")
        'Try
        '    rispostax = Requestx.Execute()
        'Catch ex As Exception

        'End Try




        'Session("IdRecordMaster")


        ritorno = True




        'Catch ex As Exception

        'End Try





        'Else
        '    AsiModel.LogIn.LogCambioStatus(IDRinnovo, "151", Session("WebUserEnte"), "rinnovo")
        'End If


        ' occorre scrivere il codice che aggiorna albo con i dati giusti

        Return ritorno
    End Function

    Protected Sub lnkConcludi_Click(sender As Object, e As EventArgs) Handles lnkConcludi.Click
        If Page.IsValid Then

            Dim ritorno As Boolean


            ritorno = CaricaDatiDaSceltaRinnovo(Session("IDRinnovo"), Session("id_record"), Session("idScelto"))

            If ritorno = True Then
                'Session("visto") = "ok"
                'Session("rinnovoAggiunto") = "OK"
                Session("AnnullaREqui") = "newRin"
                Response.Redirect("dashboardRinnovi2.aspx?open=" & Session("IDRinnovo"))

                '    Response.Redirect("dashboardRinnovi2.aspx?open=" & Session("IDRinnovo") & "&ris=" & deEnco.QueryStringEncode("ok"))
            ElseIf ritorno = False Then
                Session("AnnullaREqui") = "newRinKO"
                Response.Redirect("dashboardRinnovi2.aspx")
                'Response.Redirect("dashboardRinnovi2.aspx?ris=" & deEnco.QueryStringEncode("pr"))
                'Session("visto") = "ok"
            End If

        End If
    End Sub

    Protected Sub chkEA_CheckedChanged(sender As Object, e As EventArgs) Handles chkEA.CheckedChanged
        If chkEA.Checked = True Then

            If Session("auth") = "0" Or IsNothing(Session("auth")) Then
                Response.Redirect("../login.aspx")
            End If
            Dim address As New List(Of IndirizzoEA)
            address = AsiModel.leggiIndirizzoSpedizioneEA(Session("WebUserEnte"), Session("password"))

            If address.Count >= 1 Then


                For Each item In address

                    txtIndirizzoConsegna.Text = item.IndirizzoConsegnaEA
                    txtCapConsegna.Text = item.CapEA
                    txtComuneConsegna.Text = item.ComuneEA
                    txtProvinciaConsegna.Text = item.ProvinciaEA
                    txtTelefono.Text = item.TelefonoEA


                Next
            End If
            chkCopia.Enabled = False

        Else
            chkCopia.Enabled = True

        End If
    End Sub
End Class