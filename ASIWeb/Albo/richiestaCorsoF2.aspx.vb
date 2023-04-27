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
Imports System.ComponentModel.DataAnnotations

Public Class richiestaCorsoF2
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
    Dim tokenZ As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim giorno As Integer = Now.Day()
        Dim mese As Integer = Now.Month()
        Dim anno As Integer = Now.Year()

        Dim dataCorrente As Date = Now.ToShortDateString
        Dim annoCorrente As Integer = Now.Year
        Dim meseCorrente As Integer = Now.Month
        Dim giornoCorrente As Integer = Now.Day
        Dim dataInizio As Date
        Dim dataFine As Date
        dataInizio = "01-01-" & annoCorrente
        dataFine = "31-12-" & annoCorrente
        Calendar3.DateMin = dataInizio
        Calendar3.DateMax = dataFine


        'DateFirstMonth = "2022-08-26"
        'DateMax = "2022-09-13" DateMin="2022-08-26"
        Dim fase As String = Request.QueryString("fase")
        If Not String.IsNullOrEmpty(fase) Then
            fase = deEnco.QueryStringDecode(Request.QueryString("fase"))
            Session("fase") = fase

        End If
        If Session("fase") <> "2" Then
            Response.Redirect("../home.aspx")
        End If

        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If
        If IsNothing(Session("codice")) Then
            Response.Redirect("../login.aspx")
        End If

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
        Dim nomef As String = Request.QueryString("nomef")
        If fase = 2 Then
            lblnomef.Text = "Documento caricato precedentemente"
        Else
            ' lblnomef.Text = "Documento " & nomef & " caricato con successo"
            lblnomef.Text = "Documento caricato con successo"
        End If

        If Not String.IsNullOrEmpty(codR) Then


            Session("IDCorso") = codR
            Dim DettaglioCorso As New DatiNuovoCorso

            DettaglioCorso = Corso.PrendiValoriNuovoCorso(Session("IDCorso"))
            Dim IDCorso As String = DettaglioCorso.IDCorso
            Dim CodiceEnteRichiedente As String = DettaglioCorso.CodiceEnteRichiedente
            Dim DescrizioneEnteRichiedente As String = DettaglioCorso.DescrizioneEnteRichiedente
            Dim TipoEnte As String = DettaglioCorso.TipoEnte
            Dim CodiceStatus As String = DettaglioCorso.CodiceStatus
            Dim DescrizioneStatus As String = DettaglioCorso.DescrizioneStatus

            HiddenIdRecord.Value = DettaglioCorso.IdRecord

            lblIntestazioneCorso.Text = "<strong>ID Corso: </strong>" & IDCorso & "<strong> - Ente Richiedente: </strong>" & DescrizioneEnteRichiedente
        End If

        If Not Page.IsPostBack Then

            Regioni()


        End If
    End Sub
    Sub Regioni()

        Dim ds As DataSet

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("webProvinceRegioni")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.AllRecords)

        RequestP.AddSortField("Regione", Enumerations.Sort.Ascend)

        ds = RequestP.Execute()

        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then


            Dim SingleSport As DataTable = ds.Tables("main").DefaultView.ToTable(True, "Regione")




            ddlRegione.DataSource = SingleSport

            ddlRegione.DataTextField = "Regione"
            ddlRegione.DataValueField = "Regione"

            ddlRegione.DataBind()
            'ddlSport.DataValueField = "Sport"
            ddlRegione.Items.Insert(0, New ListItem("##", "##"))



        End If



    End Sub






    Public Function CaricaDatiDocumentoCorso(codR As String, IDCorso As String) As Boolean
        '  Dim litNumRichieste As Literal = DirectCast(ContentPlaceHolder1.FindControl("LitNumeroRichiesta"), Literal)


        ' Dim ds As DataSet


        Dim fmsP As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
        '  Dim ds As DataSet
        Dim risposta As String = ""
        fmsP.SetLayout("webCorsiRichiesta")
        Dim Request = fmsP.CreateEditRequest(IDCorso)
        Request.AddField("Titolo_Corso", Data.PrendiStringaT(Server.HtmlEncode(txtDenominazione.Text)))

        Request.AddField("Cap_Svolgimento", Data.PrendiStringaT(Server.HtmlEncode(txtCap.Text)))
        Request.AddField("Comune_Svolgimento", Data.PrendiStringaT(Server.HtmlEncode(ddlComune.SelectedItem.Text)))
        Request.AddField("LocalitaSvolgimento", Data.PrendiStringaT(Server.HtmlEncode(txtLocalitaSvolgimento.Text)))
        Request.AddField("PR_Svolgimento", ddlProvincia.SelectedItem.Text)
        Request.AddField("Regione_Svolgimento", ddlRegione.SelectedItem.Text)
        Request.AddField("Indirizzo_Svolgimento", Data.PrendiStringaT(Server.HtmlEncode(txtIndirizzo.Text)))





        Dim dataInseriraDa = txtDataInizio.Text
        Dim oDateDa As DateTime = DateTime.Parse(dataInseriraDa)
        Dim giorno = oDateDa.Day
        Dim anno = oDateDa.Year
        Dim mese = oDateDa.Month



        Request.AddField("Svolgimento_Da_Data", mese & "/" & giorno & "/" & anno)

        Dim dataInseriraA = txtDataFine.Text
        Dim oDateA As DateTime = DateTime.Parse(dataInseriraA)
        giorno = oDateA.Day
        anno = oDateA.Year
        mese = oDateA.Month
        Dim annoattuale As Integer = Now.Year

        If Session("CorsoModificaDataEmissione") = "S" Then
            If mese = 12 Then
                Request.AddField("Svolgimento_A_Data", "01/01/" & annoattuale + 1)
            Else
                'Request.AddField("Data_Emissione", Data.SistemaDataUK(Data.SonoDieci(dataSelezionata)))
                Request.AddField("Svolgimento_A_Data", mese & "/" & giorno & "/" & anno)
            End If
        Else

            If mese = 12 Then
                Request.AddField("Svolgimento_A_Data", "01/01/" & annoattuale + 1)
            Else
                'Request.AddField("Data_Emissione", Data.SistemaDataUK(Data.SonoDieci(dataSelezionata)))
                Request.AddField("Svolgimento_A_Data", mese & "/" & giorno & "/" & anno)
            End If

        End If


        Dim dataEmissione = txtDataEmissione.Text

        If Not String.IsNullOrEmpty(dataEmissione) Then


            Dim oDateEmissione As DateTime = DateTime.Parse(dataEmissione)
            giorno = oDateEmissione.Day
            anno = oDateEmissione.Year
            mese = oDateEmissione.Month






            Request.AddField("Data_Emissione", mese & "/" & giorno & "/" & anno)
        End If






        Request.AddField("OreCorso", txtOreCorso.Text)

        Request.AddField("Fase", "2")
        Request.AddScript("SistemaEncodingCorsoFase2", IDCorso)

        Try
            risposta = Request.Execute()




        Catch ex As Exception

        End Try



        Return True
    End Function
    'Protected Sub cvDate_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles cvDate.ServerValidate
    '    If txtDataInizio.Text = txtDataFine.Text Then
    '        args.IsValid = False
    '    Else
    '        args.IsValid = True


    '    End If
    'End Sub

    Protected Sub btnFase3_Click(sender As Object, e As EventArgs) Handles btnFase3.Click
        If Page.IsValid Then
            If Session("auth") = "0" Or IsNothing(Session("auth")) Then
                Response.Redirect("../login.aspx")
            End If


            CaricaDatiDocumentoCorso(Session("IDCorso"), Session("id_record"))

            Session("fase") = "3"
            Response.Redirect("richiestaCorsoF3.aspx?codR=" & deEnco.QueryStringEncode(Session("IDCorso")) & "&record_ID=" & deEnco.QueryStringEncode(Session("id_record")))

        End If
    End Sub

    Protected Sub ddlRegione_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRegione.SelectedIndexChanged
        Dim selezionato As String = ddlRegione.SelectedItem.Value
        Dim ds1 As DataSet

        Dim fmsP1 As FMSAxml = AsiModel.Conn.Connect()
        fmsP1.SetLayout("webProvinceRegioni")
        Dim RequestP1 = fmsP1.CreateFindRequest(Enumerations.SearchType.Subset)
        RequestP1.AddSearchField("regione", selezionato, Enumerations.SearchOption.equals)
        RequestP1.AddSortField("sigla", Enumerations.Sort.Ascend)

        ds1 = RequestP1.Execute()

        If Not IsNothing(ds1) AndAlso ds1.Tables("main").Rows.Count > 0 Then

            'For Each dr In ds.Tables("main").Rows

            '    Response.Write("Sport: " & dr("Sport") & " - Disciplina: " & dr("Disciplina") & " - Specialità: " & dr("Specialita") & "<br />")


            Dim province As DataTable = ds1.Tables("main").DefaultView.ToTable(True, "sigla")

            ddlProvincia.DataSource = province

            ddlProvincia.DataTextField = "sigla"
            ddlProvincia.DataValueField = "sigla"

            ddlProvincia.DataBind()

            ddlProvincia.Items.Insert(0, New ListItem("##", "##"))
            ' ddlProvincia.Items.Clear()

        End If
    End Sub


    Protected Sub ddlProvincia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProvincia.SelectedIndexChanged
        Dim selezionato As String = ddlProvincia.SelectedItem.Value
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

            ddlComune.DataSource = comuni

            ddlComune.DataTextField = "descrizioneComune"
            ddlComune.DataValueField = "descrizioneComune"

            ddlComune.DataBind()

            ddlComune.Items.Insert(0, New ListItem("##", "##"))
            ' ddlProvincia.Items.Clear()

        End If
    End Sub

    Protected Sub CustomValidator1_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles CustomValidator1.ServerValidate

        Dim dataFine As Date = txtDataFine.Text
        Dim dataEmissione As Date = txtDataEmissione.Text

        If dataEmissione < dataFine Then
            If Session("CorsoModificaDataEmissione") = "S" Then
                args.IsValid = True
            Else
                args.IsValid = False
                CustomValidator1.ErrorMessage = "La data emissione deve essere successiva alla data fine corso"
            End If



        Else
            Dim annoCorrente = Now.Year()
            Dim annoInserito = Right(txtDataFine.Text, 4)

            If annoCorrente = annoInserito Then

                args.IsValid = True



            Else
                If Session("CorsoModificaDataEmissione") = "S" Then
                    args.IsValid = True
                Else

                    args.IsValid = False
                    CustomValidator1.ErrorMessage = "Inserire una data dell'anno corrente"
                End If


                '    avvisoData.Text = "Inserire data anno corrente"
            End If


        End If






    End Sub


End Class