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
Public Class valutaEquiparazione
    Inherits System.Web.UI.Page
    ' 100  kb=100*1024
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


            Session("IDEquiparazione") = codR
            Dim DettaglioEquiparazione As New DatiNuovaEquiparazione

            DettaglioEquiparazione = Equiparazione.PrendiValoriNuovaEquiparazione(Session("IDEquiparazione"))
            Dim IDEquiparazione As String = DettaglioEquiparazione.IDEquiparazione
            Dim CodiceEnteRichiedente As String = DettaglioEquiparazione.CodiceEnteRichiedente
            Dim DescrizioneEnteRichiedente As String = DettaglioEquiparazione.DescrizioneEnteRichiedente
            Dim TipoEnte As String = DettaglioEquiparazione.TipoEnte
            Dim CodiceStatus As String = DettaglioEquiparazione.CodiceStatus
            '    Session("codiceStatus") = DettaglioCorso.CodiceStatus
            Dim DescrizioneStatus As String = DettaglioEquiparazione.DescrizioneStatus
            'Dim indirizzoSvolgimento As String = DettaglioCorso.IndirizzoSvolgimento & " - " & DettaglioCorso.LocalitaSvolgimento _
            '     & DettaglioCorso.CapSvolgimento & " - " & DettaglioCorso.PRSvolgimento & " - " & DettaglioCorso.RegioneSvolgimento
            Dim nominativo As String = DettaglioEquiparazione.Nome & " " & DettaglioEquiparazione.Cognome
            Dim codiceFiscale As String = DettaglioEquiparazione.CodiceFiscale
            Dim codiceTessera As String = DettaglioEquiparazione.CodiceTessera
            Dim dataScadenza As String = DettaglioEquiparazione.DataScadenza
            idEqui = DettaglioEquiparazione.IDEquiparazione
            HiddenIDEquiparazione.Value = DettaglioEquiparazione.IDEquiparazione
            HiddenIdRecord.Value = DettaglioEquiparazione.IdRecord

            lblIntestazioneEquiparazioni.Text = "<strong>ID Equiparazione: </strong>" & IDEquiparazione & "<strong> - Ente Richiedente: </strong>" & DescrizioneEnteRichiedente & "<br />" &
                 "<strong>Nominativo: </strong>" & nominativo & "<br />" &
                 "<strong>Codice Fiscale: </strong>" & codiceFiscale & " <strong>Numero Tessera: </strong>" & codiceTessera &
                 " <strong>Data Scadenza: </strong>" & Data.SonoDieci(dataScadenza)

        End If

    End Sub

    Protected Sub btnValuta_Click(sender As Object, e As EventArgs) Handles btnValuta.Click
        If Page.IsValid Then

            Dim fmsP As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
            '  Dim ds As DataSet
            Dim risposta As String = ""
            fmsP.SetLayout("webEquiparazioniRichiesta")
            Dim Request = fmsP.CreateEditRequest(Session("id_record"))

            Dim valutazione As String = ddlValutazione.SelectedItem.Value

            If valutazione = "S" Then
                Request.AddField("Codice_Status", "106")
            ElseIf valutazione = "N" Then
                Request.AddField("Codice_Status", "107")

            End If

            Request.AddField("NoteValutazioneSettore", Data.PrendiStringaT(Server.HtmlEncode(txtNote.Text)))
            'Request.AddScript("SistemaEncodingNoteValuta_PianoCorso", Session("id_record"))
            'script per gestione caratteri speciali da inserire.
            Try


                risposta = Request.Execute()
                If valutazione = "S" Then
                    AsiModel.LogIn.LogCambioStatus(codR, "106", Session("WebUserEnte"), "equiparazione")
                ElseIf valutazione = "N" Then
                    AsiModel.LogIn.LogCambioStatus(codR, "107", Session("WebUserEnte"), "equiparazione")

                End If
            Catch ex As Exception

            End Try
            Response.Redirect("archivioEquiValutati.aspx#" & codR)

        End If


    End Sub
End Class