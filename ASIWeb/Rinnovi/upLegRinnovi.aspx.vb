Imports System.IO
Imports OboutInc.FileUpload
Imports fmDotNet
Imports System.Net.Mail
Imports System.Web.UI.WebControls
Imports ASIWeb.AsiModel
Imports System.Security.Cryptography

Imports System.Threading.Tasks
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Imports System.Web.Script.Serialization
Imports RestSharp

Imports System.Collections.Generic
Imports System.Net.Security
Imports System.Net
Imports ASIWeb.Ed

Public Class upLegRinnovi
    Inherits System.Web.UI.Page

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
    Dim deEnco As New Ed()
    Dim webserver As String = ConfigurationManager.AppSettings("webserver")
    Dim utente As String = ConfigurationManager.AppSettings("utente")
    Dim porta As String = ConfigurationManager.AppSettings("porta")
    Dim pass As String = ConfigurationManager.AppSettings("pass")
    Dim dbb As String = ConfigurationManager.AppSettings("dbb")
    Dim codR As String = ""
    Dim cultureFormat As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("it-IT")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If
        If IsNothing(Session("codice")) Then
            Response.Redirect("../login.aspx")
        End If

        cultureFormat.NumberFormat.CurrencySymbol = "€"
        cultureFormat.NumberFormat.CurrencyDecimalDigits = 2
        cultureFormat.NumberFormat.CurrencyGroupSeparator = String.Empty
        cultureFormat.NumberFormat.CurrencyDecimalSeparator = ","
        System.Threading.Thread.CurrentThread.CurrentCulture = cultureFormat
        System.Threading.Thread.CurrentThread.CurrentUICulture = cultureFormat


        Dim idEqui As String = 0

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

        codR = deEnco.QueryStringDecode(Request.QueryString("codR"))


        If Not String.IsNullOrEmpty(codR) Then


            Session("IDRinnovo") = codR
            Dim DettaglioRinnovo As New DatiNuovoRinnovo

            DettaglioRinnovo = Rinnovi.PrendiValoriNuovoRinnovo(Session("IDRinnovo"))
            Dim IDRinnovo As String = DettaglioRinnovo.IDRinnovo
            Dim CodiceEnteRichiedente As String = DettaglioRinnovo.CodiceEnteRichiedente
            Dim DescrizioneEnteRichiedente As String = DettaglioRinnovo.DescrizioneEnteRichiedente
            Dim TipoEnte As String = DettaglioRinnovo.TipoEnte
            Dim CodiceStatus As String = DettaglioRinnovo.CodiceStatus
            '    Session("codiceStatus") = DettaglioCorso.CodiceStatus
            Dim DescrizioneStatus As String = DettaglioRinnovo.DescrizioneStatus
            'Dim indirizzoSvolgimento As String = DettaglioCorso.IndirizzoSvolgimento & " - " & DettaglioCorso.LocalitaSvolgimento _
            '     & DettaglioCorso.CapSvolgimento & " - " & DettaglioCorso.PRSvolgimento & " - " & DettaglioCorso.RegioneSvolgimento
            Dim nominativo As String = DettaglioRinnovo.Nome & " " & DettaglioRinnovo.Cognome
            Dim codiceFiscale As String = DettaglioRinnovo.CodiceFiscale
            Dim codiceTessera As String = DettaglioRinnovo.CodiceTessera
            Dim dataScadenza As String = DettaglioRinnovo.DataScadenza
            idEqui = DettaglioRinnovo.IDRinnovo
            HiddenIdRecord.Value = DettaglioRinnovo.IDRinnovo
            HiddenIDRinnovi.Value = DettaglioRinnovo.IdRecord

            lblIntestazioneRinnovi.Text = "<strong>ID Equiparazione: </strong>" & IDRinnovo & "<strong> - Ente Richiedente: </strong>" & DescrizioneEnteRichiedente & "<br />" &
                 "<strong>Nominativo: </strong>" & nominativo & "<br />" &
                 "<strong>Codice Fiscale: </strong>" & codiceFiscale & " <strong>Numero Tessera: </strong>" & codiceTessera &
                 " <strong>Data Scadenza: </strong>" & dataScadenza

        End If




        'If QuantiAllegati(IDRinnovo) = "no" Then

        '    Button1.Enabled = False
        '    uploadedFiles.Text = ""

        '    uploadedFiles.Text = "<b>non è possibile caricare ulteriori documenti </b>"

        'End If

    End Sub

End Class