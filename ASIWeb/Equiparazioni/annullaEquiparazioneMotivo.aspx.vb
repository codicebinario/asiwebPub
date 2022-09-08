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
Public Class annullaEquiparazioneMotivo
    Inherits System.Web.UI.Page
    Dim webserver As String = ConfigurationManager.AppSettings("webserver")
    Dim utente As String = ConfigurationManager.AppSettings("utente")
    Dim porta As String = ConfigurationManager.AppSettings("porta")
    Dim pass As String = ConfigurationManager.AppSettings("pass")
    Dim dbb As String = ConfigurationManager.AppSettings("dbb")
    Dim cultureFormat As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("it-IT")
    Dim deEnco As New Ed()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("tipoEnte")) Then
            Response.Redirect("../login.aspx")
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


            HiddenIdRecord.Value = DettaglioEquiparazione.IdRecord

            lblIntestazioneEquiparazione.Text = "<strong>ID Equiparazione: </strong>" & IDEquiparazione & "<strong> - Ente Richiedente: </strong>" & DescrizioneEnteRichiedente & "<br />" &
                 "<strong>Nominativo: </strong>" & nominativo & "<br />" &
                 "<strong>Codice Fiscale: </strong>" & codiceFiscale & " <strong>Numero Tessera: </strong>" & codiceTessera &
                 " <strong>Data Scadenza: </strong>" & dataScadenza

        End If

    End Sub

    Protected Sub btnValuta_Click(sender As Object, e As EventArgs) Handles btnValuta.Click
        If Page.IsValid Then

            Dim fmsP As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
            '  Dim ds As DataSet
            Dim risposta As String = ""
            fmsP.SetLayout("webEquiparazioniRichiesta")
            Dim Request = fmsP.CreateEditRequest(Session("id_record"))




            Request.AddField("Codice_Status", "119")


            Request.AddField("Equi_NoteAnnullamentoEquiparazione", Data.PrendiStringaT(Server.HtmlEncode(txtNoteAnnullamento.Text)))
            '    Request.AddScript("SistemaEncodingNoteAnnullamento_Corso", Session("id_record"))
            'script per gestione caratteri speciali da inserire
            Try
                risposta = Request.Execute()
                AsiModel.LogIn.LogCambioStatus(Session("IDEquiparazione"), "119", Session("WebUserEnte"), "equiparazione")
            Catch ex As Exception

            End Try


            Response.Redirect("DashboardEquiEvasi.aspx#" & Session("IDEquiparazioni"))

        End If


    End Sub

End Class