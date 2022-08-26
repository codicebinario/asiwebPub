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

Public Class richiestaEquiparazioneDati1
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim fase As String = Request.QueryString("fase")
        If Not String.IsNullOrEmpty(fase) Then
            '     fase = deEnco.QueryStringDecode(Request.QueryString("fase"))
            Session("fase") = fase
        End If


        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If
        If IsNothing(Session("codice")) Then
            Response.Redirect("../login.aspx")
        End If

        If IsNothing(Session("codiceFiscale")) Then
            Response.Redirect("../login.aspx")
        End If
        If Session("procedi") <> "OK" Then

            Response.Redirect("checkTesseramento.aspx")

        End If

        '  Dim newCulture As System.Globalization.CultureInfo = System.Globalization.CultureInfo.CurrentUICulture.Clone()
        cultureFormat.NumberFormat.CurrencySymbol = "€"
        cultureFormat.NumberFormat.CurrencyDecimalDigits = 2
        cultureFormat.NumberFormat.CurrencyGroupSeparator = String.Empty
        cultureFormat.NumberFormat.CurrencyDecimalSeparator = ","
        System.Threading.Thread.CurrentThread.CurrentCulture = cultureFormat
        System.Threading.Thread.CurrentThread.CurrentUICulture = cultureFormat

        'pag = Request.QueryString("pag")
        'skip = Request.QueryString("skip")

        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If

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
            Dim DettaglioEquiparazione As New DatiNuovaEquiparazione

            DettaglioEquiparazione = Equiparazione.PrendiValoriNuovaEquiparazione(Session("IDEquiparazione"))
            Dim IDEquiparazione As String = DettaglioEquiparazione.IDEquiparazione
            Dim CodiceEnteRichiedente As String = DettaglioEquiparazione.CodiceEnteRichiedente
            Dim DescrizioneEnteRichiedente As String = DettaglioEquiparazione.DescrizioneEnteRichiedente
            Dim TipoEnte As String = DettaglioEquiparazione.TipoEnte
            Dim CodiceStatus As String = DettaglioEquiparazione.CodiceStatus
            Dim DescrizioneStatus As String = DettaglioEquiparazione.DescrizioneStatus
            '  Dim TitoloCorso As String = DettaglioEquiparazione.TitoloCorso
            HiddenIdRecord.Value = DettaglioEquiparazione.IdRecord
            HiddenIDEquiparazione.Value = DettaglioEquiparazione.IDEquiparazione
            lblIntestazioneEquiparazione.Text = "<strong>ID Equiparazione: </strong>" & IDEquiparazione & " - " & "<strong> - Ente Richiedente: </strong>" & DescrizioneEnteRichiedente
        End If

        If fase = 3 Then
            lblnomef.Text = "Foto correttamente caricata "


        End If

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


            ddlProvinciaConsegna.DataSource = SingleSport


            ddlProvinciaConsegna.DataTextField = "sigla"
            ddlProvinciaConsegna.DataValueField = "sigla"

            ddlProvinciaConsegna.DataBind()
            'ddlSport.DataValueField = "Sport"
            ddlProvinciaConsegna.Items.Insert(0, New ListItem("##", "##"))


        End If



    End Sub

    Sub leggiDatiEsistenti()

        Dim datiCodiceFiscale As New DatiCodiceFiscale

        datiCodiceFiscale = getDatiCodiceFiscale(Session("codiceFiscale"))

        txtCognome.Text = datiCodiceFiscale.Cognome
        txtNome.Text = datiCodiceFiscale.Nome
        txtCodiceFiscale.Text = datiCodiceFiscale.CodiceFiscale
        txtDataNascita.Text = datiCodiceFiscale.DataNascita.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
        txtComuneNascita.Text = datiCodiceFiscale.LuogoNascita
        txtCodiceTessera.Text = datiCodiceFiscale.CodiceTessera
        txtDataScadenza.Text = datiCodiceFiscale.DataScadenza.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)



        'txtIndirizzoResidenza.Text = datiCodiceFiscale.Indirizzo
        'txtComuneResidenza.Text = datiCodiceFiscale.Comune
        'txtCapResidenza.Text = datiCodiceFiscale.Cap
        'txtProvinciaResidenza.Text = datiCodiceFiscale.Provincia






    End Sub

    Protected Sub ddlProvinciaConsegna_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProvinciaConsegna.SelectedIndexChanged
        Dim selezionato As String = ddlProvinciaConsegna.SelectedItem.Value
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

            ddlComuneConsegna.DataSource = comuni

            ddlComuneConsegna.DataTextField = "descrizioneComune"
            ddlComuneConsegna.DataValueField = "descrizioneComune"

            ddlComuneConsegna.DataBind()

            ddlComuneConsegna.Items.Insert(0, New ListItem("##", "##"))
            ' ddlProvincia.Items.Clear()

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

    Protected Sub btnFase3_Click(sender As Object, e As EventArgs) Handles btnFase3.Click
        If Page.IsValid Then

            CaricaDatiDocumentoCorso(Session("IDEquiparazione"), Session("id_record"))

            Session("fase") = "3"
            Response.Redirect("richiestaEquiparazioneDati2.aspx?codR=" & deEnco.QueryStringEncode(Session("IDEquiparazione")) & "&record_ID=" & deEnco.QueryStringEncode(Session("id_record")) & "&fase=4")

        End If
    End Sub

    Public Function CaricaDatiDocumentoCorso(codR As String, IDEquiparazione As String) As Boolean
        '  Dim litNumRichieste As Literal = DirectCast(ContentPlaceHolder1.FindControl("LitNumeroRichiesta"), Literal)


        ' Dim ds As DataSet


        Dim fmsP As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
        '  Dim ds As DataSet
        Dim risposta As String = ""
        fmsP.SetLayout("webEquiparazioniRichiesta")
        Dim Request = fmsP.CreateEditRequest(IDEquiparazione)


        Request.AddField("Equi_Nome", Data.PrendiStringaT(Server.HtmlEncode(txtNome.Text)))
        Request.AddField("Equi_Cognome", Data.PrendiStringaT(Server.HtmlEncode(txtCognome.Text)))
        Request.AddField("Equi_NumeroTessera", Data.PrendiStringaT(Server.HtmlEncode(txtCodiceTessera.Text)))
        Request.AddField("Equi_CodiceFiscale", Data.PrendiStringaT(Server.HtmlEncode(txtCodiceFiscale.Text)))
        Request.AddField("Equi_ComuneNascita", Data.PrendiStringaT(Server.HtmlEncode(txtComuneNascita.Text)))

        Request.AddField("Equi_DataScadenza", SistemaData(txtDataScadenza.Text))
        Request.AddField("Equi_DataNascita", SistemaData(txtDataNascita.Text))

        Request.AddField("Equi_IndirizzoEmail", Data.PrendiStringaT(Server.HtmlEncode(txtEmail.Text)))
        Request.AddField("Equi_Telefono", Data.PrendiStringaT(Server.HtmlEncode(txtTelefonoCellulare.Text)))


        Request.AddField("Equi_IndirizzoResidenza", Data.PrendiStringaT(Server.HtmlEncode(txtIndirizzoResidenza.Text)))
        Request.AddField("Equi_ProvinciaResidenza", Data.PrendiStringaT(Server.HtmlEncode(ddlProvinciaResidenza.SelectedItem.Text)))
        Request.AddField("Equi_ComuneResidenza", Data.PrendiStringaT(Server.HtmlEncode(ddlComuneResidenza.SelectedItem.Text)))
        Request.AddField("Equi_CapResidenza", Data.PrendiStringaT(Server.HtmlEncode(txtCapResidenza.Text)))



        Request.AddField("Equi_IndirizzoConsegna", Data.PrendiStringaT(Server.HtmlEncode(txtIndirizzoConsegna.Text)))
        Request.AddField("Equi_ProvinciaConsegna", Data.PrendiStringaT(Server.HtmlEncode(ddlProvinciaConsegna.SelectedItem.Text)))
        Request.AddField("Equi_ComuneConsegna", Data.PrendiStringaT(Server.HtmlEncode(ddlComuneConsegna.SelectedItem.Text)))
        Request.AddField("Equi_CapConsegna", Data.PrendiStringaT(Server.HtmlEncode(txtCapConsegna.Text)))

        If chkStampaCartacea.Checked = True Then
            Request.AddField("Equi_StampaCartaceo", "si")
        End If

        Request.AddField("Equi_Fase", "3")
        '    Request.AddScript("SistemaEncodingCorsoFase2", IDCorso)

        '   Try
        risposta = Request.Execute()



        'Catch ex As Exception

        'End Try



        Return True
    End Function

    Function SistemaData(valore As String) As String

        Dim oDateDa As DateTime
        Dim giorno
        Dim anno
        Dim mese

        Dim miaData = valore
        oDateDa = DateTime.Parse(miaData)
        giorno = oDateDa.Day
        anno = oDateDa.Year
        mese = oDateDa.Month

        Return mese & "/" & giorno & "/" & anno


    End Function
End Class