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
Public Class AnnullaCorsoMotivo
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


            Session("IDCorso") = codR
            Dim DettaglioCorso As New DatiNuovoCorso

            DettaglioCorso = Corso.PrendiValoriNuovoCorso(Session("IDCorso"))
            Dim IDCorso As String = DettaglioCorso.IDCorso
            Dim CodiceEnteRichiedente As String = DettaglioCorso.CodiceEnteRichiedente
            Dim DescrizioneEnteRichiedente As String = DettaglioCorso.DescrizioneEnteRichiedente
            Dim TipoEnte As String = DettaglioCorso.TipoEnte
            Dim CodiceStatus As String = DettaglioCorso.CodiceStatus
            '    Session("codiceStatus") = DettaglioCorso.CodiceStatus
            Dim DescrizioneStatus As String = DettaglioCorso.DescrizioneStatus
            Dim indirizzoSvolgimento As String = DettaglioCorso.IndirizzoSvolgimento & " - " & DettaglioCorso.LocalitaSvolgimento _
                & DettaglioCorso.CapSvolgimento & " - " & DettaglioCorso.PRSvolgimento & " - " & DettaglioCorso.RegioneSvolgimento
            Dim DataCorsoDa As String = Left(DettaglioCorso.SvolgimentoDataDa, 10)
            Dim DataCorsoA As String = Left(DettaglioCorso.SvolgimentoDataA, 10)
            Dim OraCorsoDa As String = DettaglioCorso.OraSvolgimentoDa
            Dim OraCorsoA As String = DettaglioCorso.OraSvolgimentoA
            Dim OreCorso As String = DettaglioCorso.TotaleOre


            HiddenIdRecord.Value = DettaglioCorso.IdRecord

            lblIntestazioneCorso.Text = "<strong>ID Corso: </strong>" & IDCorso & "<strong> - Ente Richiedente: </strong>" & DescrizioneEnteRichiedente & "<br />" &
                 "<strong>Indirizzo: </strong>" & indirizzoSvolgimento & "<br />" &
                 "<strong>Data Inizio: </strong>" & DataCorsoDa & " <strong>Data Fine: </strong>" & DataCorsoA &
                 " <strong>Ore: </strong>" & OreCorso

        End If

    End Sub

    Protected Sub btnValuta_Click(sender As Object, e As EventArgs) Handles btnValuta.Click
        If Page.IsValid Then

            Dim fmsP As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
            '  Dim ds As DataSet
            Dim risposta As String = ""
            fmsP.SetLayout("webCorsiRichiesta")
            Dim Request = fmsP.CreateEditRequest(Session("id_record"))




            Request.AddField("Codice_Status", "99")


            Request.AddField("NoteAnnullamentoCorso", Data.PrendiStringaT(Server.HtmlEncode(txtNoteAnnullamento.Text)))
            Request.AddScript("SistemaEncodingNoteAnnullamento_Corso", Session("id_record"))
            Request.AddScript("RunWebMailInvioDT", Session("IDCorso"))

            'script per gestione caratteri speciali da inserire
            Try
                risposta = Request.Execute()
                AsiModel.LogIn.LogCambioStatus(Session("IDCorso"), "99", Session("WebUserEnte"), "corso")
            Catch ex As Exception

            End Try


            Response.Redirect("archivioAlbo.aspx#" & Session("IDCorso"))

        End If


    End Sub
End Class