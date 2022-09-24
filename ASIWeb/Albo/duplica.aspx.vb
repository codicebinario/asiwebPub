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
Public Class duplica
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
    Dim codiceCorso As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim fase As String = Request.QueryString("fase")
        'If Not String.IsNullOrEmpty(fase) Then
        '    fase = deEnco.QueryStringDecode(Request.QueryString("fase"))
        '    Session("fase") = fase

        'End If
        'If Session("fase") <> "2" Then
        '    Response.Redirect("../home.aspx")
        'End If

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
        'If fase = 2 Then
        '    lblnomef.Text = "Documento caricato precedentemente"
        'Else
        '    lblnomef.Text = "Documento " & nomef & "caricato con successo"
        'End If

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

    End Sub

    Protected Sub btnFase3_Click(sender As Object, e As EventArgs) Handles btnFase3.Click
        If Page.IsValid Then

            '*********** duplico ***********

            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            fmsP.SetLayout("webCorsiRichiesta")
            Dim RequestP = fmsP.CreateDuplicateRequest(235)
            Dim dupRecID = RequestP.Execute()

            '************* trovo il nuovo codice corso *************

            Dim ds As DataSet
            Dim fmsPx As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
            '  Dim ds As DataSet
            Dim risposta As String = ""
            fmsPx.SetLayout("webCorsiRichiesta")
            Dim Request = fmsPx.CreateFindRequest(Enumerations.SearchType.Subset)
            Request.AddSearchField("id_record", dupRecID, Enumerations.SearchOption.equals)

            ds = Request.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then


                For Each dr In ds.Tables("main").Rows

                    codiceCorso = dr("IdCorso")

                Next
            End If


            ' ******** aggiorno alcuni campi del nuovo corso

            Dim fmsPy As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
            '  Dim ds As DataSet
            Dim rispostay As String = ""
            fmsPy.SetLayout("webCorsiRichiesta")
            Dim Requesty = fmsPy.CreateEditRequest(dupRecID)

            Dim dataInseriraDa = txtDataInizio.Text
            Dim oDateDa As DateTime = DateTime.Parse(dataInseriraDa)
            Dim giorno = oDateDa.Day
            Dim anno = oDateDa.Year
            Dim mese = oDateDa.Month



            Requesty.AddField("Svolgimento_Da_Data", mese & "/" & giorno & "/" & anno)

            Dim dataInseriraA = txtDataFine.Text
            Dim oDateA As DateTime = DateTime.Parse(dataInseriraA)
            giorno = oDateA.Day
            anno = oDateA.Year
            mese = oDateA.Month

            Requesty.AddField("Svolgimento_A_Data", mese & "/" & giorno & "/" & anno)
            Requesty.AddField("Fase", "3")
            Requesty.AddField("Codice_status", "54")
            Requesty.AddField("NoteValutazioneSettore", "")
            Requesty.AddField("NoteAnnullamentoCorso", "")
            Requesty.AddField("Motivo_Corsisti_KO", "")
            Requesty.AddField("StatusPrimaCaricamentoXL", "")
            Requesty.AddField("NoteUploadVerbale", "")
            Requesty.AddField("ElencoPartecipanti", "")
            Requesty.AddField("NomeFileElencoPartecipanti", "")
            Requesty.AddField("NoteUploadElencoCorso", "")
            Requesty.AddField("NoteUploadVerbale", "")
            Requesty.AddField("NomeFileVerbale", "")
            Requesty.AddField("CheckVerbale", "0")
            Try
                risposta = Requesty.Execute()


                '****** aggiorno il log ********************

                AsiModel.LogIn.LogCambioStatus(codiceCorso, "54", Session("WebUserEnte"), "corso")


            Catch ex As Exception

            End Try

            Response.Redirect("dashboardB.aspx#" & codiceCorso)
        End If
    End Sub
End Class