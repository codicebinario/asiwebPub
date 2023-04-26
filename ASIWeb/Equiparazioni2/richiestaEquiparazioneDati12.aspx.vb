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
Imports System.Globalization

Public Class richiestaEquiparazioneDati12
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
    Dim codR As String = ""
    Dim tokenZ As String = ""
    Dim record_ID As String = ""
    Dim codiceFiscale As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        '      Dim fase As String = Request.QueryString("fase")
        'Dim fase = deEnco.QueryStringDecode(Request.QueryString("fase"))
        'If Not String.IsNullOrEmpty(fase) Then

        '    Session("fase") = fase
        'End If

        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If
        If IsNothing(Session("codice")) Then
            Response.Redirect("../login.aspx")
        End If

        If Session("EquiparazioneModificaDataEmissione") = "S" Then

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


        'If IsNothing(Session("codiceFiscale")) Then
        '    Response.Redirect("../login.aspx")
        'End If
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

        'pag = Request.QueryString("pag")
        'skip = Request.QueryString("skip")



        record_ID = deEnco.QueryStringDecode(Request.QueryString("record_id"))
        If Not String.IsNullOrEmpty(record_ID) Then

            Session("id_record") = record_ID

        End If

        If IsNothing(Session("id_record")) Then
            Response.Redirect("../login.aspx")
        End If

        codR = deEnco.QueryStringDecode(Request.QueryString("codR"))
        If Not String.IsNullOrEmpty(codR) Then


            Session("IDEquiparazione") = codR
            '  Dim DettaglioEquiparazione As New DatiNuovaEquiparazione


            Dim DettaglioEquiparazione As New DatiNuovaEquiparazione
            DettaglioEquiparazione = Equiparazione.PrendiValoriNuovaEquiparazione2(Session("id_record"))


            Dim verificato As String = DettaglioEquiparazione.EquiCF

            If verificato = "0" Then
                Response.Redirect("DashboardEqui2.aspx?ris=" & deEnco.QueryStringEncode("no"))

            End If
            Dim IDEquiparazione As String = DettaglioEquiparazione.IDEquiparazione
            Dim CodiceEnteRichiedente As String = DettaglioEquiparazione.CodiceEnteRichiedente
            Dim DescrizioneEnteRichiedente As String = DettaglioEquiparazione.DescrizioneEnteRichiedente
            Dim TipoEnte As String = DettaglioEquiparazione.TipoEnte
            Dim CodiceStatus As String = DettaglioEquiparazione.CodiceStatus
            Dim DescrizioneStatus As String = DettaglioEquiparazione.DescrizioneStatus

            '  Dim TitoloCorso As String = DettaglioEquiparazione.TitoloCorso
            HiddenIdRecord.Value = DettaglioEquiparazione.IdRecord
            HiddenIDEquiparazione.Value = DettaglioEquiparazione.IDEquiparazione
            codiceFiscale = DettaglioEquiparazione.CodiceFiscale
            Dim datiCF = AsiModel.getDatiCodiceFiscale(codiceFiscale)

            lblIntestazioneEquiparazione.Text =
                "<strong> - Codice Fiscale: </strong>" & datiCF.CodiceFiscale &
                "<strong> - Tessera Ass.: </strong>" & datiCF.CodiceTessera & "<br />" &
                "<strong> - Nominativo: </strong>" & datiCF.Nome & " " & datiCF.Cognome &
                "<strong> - Ente Richiedente: </strong>" & DescrizioneEnteRichiedente


        End If

        'If fase = 3 Then
        '  lblnomef.Text = "Foto correttamente caricata "


        '   End If

        If Not Page.IsPostBack Then

            '  pnlFase1.Visible = False
            leggiDatiEsistenti()
            Province()

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


            'ddlProvinciaConsegna.DataSource = SingleSport


            'ddlProvinciaConsegna.DataTextField = "sigla"
            'ddlProvinciaConsegna.DataValueField = "sigla"

            'ddlProvinciaConsegna.DataBind()
            ''ddlSport.DataValueField = "Sport"
            'ddlProvinciaConsegna.Items.Insert(0, New ListItem("##", "##"))


        End If



    End Sub

    Sub leggiDatiEsistenti()

        Dim datiCodiceFiscale As New DatiCodiceFiscale

        datiCodiceFiscale = getDatiCodiceFiscale(codiceFiscale)

        txtCognome.Text = datiCodiceFiscale.Cognome
        txtNome.Text = datiCodiceFiscale.Nome
        txtCodiceFiscale.Text = datiCodiceFiscale.CodiceFiscale
        txtDataNascita.Text = datiCodiceFiscale.DataNascita.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
        txtComuneNascita.Text = datiCodiceFiscale.LuogoNascita
        txtCodiceTessera.Text = datiCodiceFiscale.CodiceTessera
        txtDataScadenza.Text = datiCodiceFiscale.DataScadenza.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
        txtIndirizzoResidenza.Text = datiCodiceFiscale.Indirizzo
        txtDataEmissione.Text = Now.ToShortDateString
        txtDataEmissioneM.Text = Now.ToShortDateString





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

            'For Each dr In ds.Tables("main").Rows

            '    Response.Write("Sport: " & dr("Sport") & " - Disciplina: " & dr("Disciplina") & " - Specialità: " & dr("Specialita") & "<br />")


            Dim comuni As DataTable = ds2.Tables("main").DefaultView.ToTable(True, "descrizioneComune")

            ddlComuneResidenza.DataSource = comuni

            ddlComuneResidenza.DataTextField = "descrizioneComune"
            ddlComuneResidenza.DataValueField = "descrizioneComune"

            ddlComuneResidenza.DataBind()

            ddlComuneResidenza.Items.Insert(0, New ListItem("##", "##"))
            ' ddlProvincia.Items.Clear()

        End If
    End Sub

    'Protected Sub btnFase3_Click(sender As Object, e As EventArgs) Handles btnFase3.Click
    '    If Page.IsValid Then

    '        CaricaDatiDocumentoCorso(Session("IDEquiparazione"), Session("id_record"))

    '        Session("fase") = "3"
    '        Response.Redirect("richiestaEquiparazioneDati2.aspx?codR=" & deEnco.QueryStringEncode(Session("IDEquiparazione")) & "&record_ID=" & deEnco.QueryStringEncode(Session("id_record")) & "&fase=" & deEnco.QueryStringEncode(3))

    '    End If
    'End Sub

    Public Function CaricaDatiDocumentoCorso(codR As String, IDEquiparazione As String) As Boolean
        '  Dim litNumRichieste As Literal = DirectCast(ContentPlaceHolder1.FindControl("LitNumeroRichiesta"), Literal)


        ' Dim ds As DataSet


        Dim fmsP As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
        '  Dim ds As DataSet
        Dim risposta As String = ""
        fmsP.SetLayout("webEquiparazioniRichiestaMolti")
        Dim Request = fmsP.CreateEditRequest(IDEquiparazione)


        Request.AddField("Equi_Nome", Data.PrendiStringaT(Server.HtmlEncode(txtNome.Text)))
        Request.AddField("Equi_Cognome", Data.PrendiStringaT(Server.HtmlEncode(txtCognome.Text)))
        Request.AddField("Equi_NumeroTessera", Data.PrendiStringaT(Server.HtmlEncode(txtCodiceTessera.Text)))
        Request.AddField("Equi_CodiceFiscale", Data.PrendiStringaT(Server.HtmlEncode(txtCodiceFiscale.Text)))
        Request.AddField("Equi_ComuneNascita", Data.PrendiStringaT(Server.HtmlEncode(txtComuneNascita.Text)))
        Request.AddField("Equi_DataScadenza", Data.SistemaDataUK(txtDataScadenza.Text))
        Request.AddField("Equi_DataNascita", Data.SistemaData(txtDataNascita.Text))
        Request.AddField("Equi_IndirizzoEmail", Data.PrendiStringaT(Server.HtmlEncode(txtEmail.Text)))
        Request.AddField("Equi_Telefono", Data.PrendiStringaT(Server.HtmlEncode(txtTelefonoCellulare.Text)))
        Request.AddField("Equi_IndirizzoResidenza", Data.PrendiStringaT(Server.HtmlEncode(txtIndirizzoResidenza.Text)))
        Request.AddField("Equi_ProvinciaResidenza", Data.PrendiStringaT(Server.HtmlEncode(ddlProvinciaResidenza.SelectedItem.Text)))
        Request.AddField("Equi_ComuneResidenza", Data.PrendiStringaT(Server.HtmlEncode(ddlComuneResidenza.SelectedItem.Text)))
        Request.AddField("Equi_CapResidenza", Data.PrendiStringaT(Server.HtmlEncode(txtCapResidenza.Text)))

        If Session("EquiparazioneModificaDataEmissione") = "S" Then
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
            Request.AddField("Equi_StampaCartaceo", "si")
            Request.AddField("Equi_IndirizzoConsegna", Data.FixNull(Data.PrendiStringaT(Server.HtmlEncode(txtIndirizzoConsegna.Text))))
            Request.AddField("Equi_ProvinciaConsegna", Data.PrendiStringaT(Server.HtmlEncode(txtProvinciaConsegna.Text)))
            Request.AddField("Equi_ComuneConsegna", Data.PrendiStringaT(Server.HtmlEncode(txtComuneConsegna.Text)))
            Request.AddField("Equi_CapConsegna", Data.FixNull(Data.PrendiStringaT(Server.HtmlEncode(txtCapConsegna.Text))))
            Request.AddField("Equi_Telefono", Data.PrendiStringaT(Server.HtmlEncode(txtTelefono.Text)))



            If chkEA.Checked Then
                Request.AddField("Equi_InviaA", "EA")
            Else
                Request.AddField("Equi_InviaA", "T")
            End If
            If chkStampaDiploma.Checked = True Then
                Request.AddField("Equi_StampaDiploma", "si")
            End If
        End If






        ' Request.AddField("Equi_Fase", "3")
        '    Request.AddScript("SistemaEncodingCorsoFase2", IDCorso)

        '   Try
        risposta = Request.Execute()



        'Catch ex As Exception

        'End Try



        Return True
    End Function

    Protected Sub lnkButton1_Click(sender As Object, e As EventArgs) Handles lnkButton1.Click
        If Page.IsValid Then

            CaricaDatiDocumentoCorso(Session("IDEquiparazione"), Session("id_record"))

            'Session("fase") = "3"
            Response.Redirect("richiestaEquiparazioneDati22.aspx?codR=" & deEnco.QueryStringEncode(Session("IDEquiparazione")) & "&record_ID=" & deEnco.QueryStringEncode(Session("id_record")))

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

            txtTelefono.Text = txtTelefonoCellulare.Text

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
End Class