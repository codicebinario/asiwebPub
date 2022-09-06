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
Imports System.Globalization

Public Class checkTesseramentoRinnovi
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


            Session("IDRinnovo") = codR
            Dim DettaglioRinnovo As New DatiNuovoRinnovo
            DettaglioRinnovo = Rinnovi.PrendiValoriNuovoRinnovo(Session("IDRinnovo"))
            Dim IDRinnovo As String = DettaglioRinnovo.IDRinnovo
            Dim CodiceEnteRichiedente As String = DettaglioRinnovo.CodiceEnteRichiedente
            Dim DescrizioneEnteRichiedente As String = DettaglioRinnovo.DescrizioneEnteRichiedente
            Dim TipoEnte As String = DettaglioRinnovo.TipoEnte
            Dim CodiceStatus As String = DettaglioRinnovo.CodiceStatus
            Dim DescrizioneStatus As String = DettaglioRinnovo.DescrizioneStatus
            HiddenIdRecord.Value = DettaglioRinnovo.IdRecord
            HiddenIDRinnovo.Value = DettaglioRinnovo.IDRinnovo
            lblIntestazioneRinnovo.Text = "<strong>ID Rinnovo: </strong>" & IDRinnovo & "<strong> - Ente Richiedente: </strong>" & DescrizioneEnteRichiedente
        End If

        If Page.IsPostBack Then

            '  pnlFase1.Visible = False


        End If
    End Sub

    Protected Sub btnCheck_Click(sender As Object, e As EventArgs) Handles btnCheck.Click
        If Page.IsValid Then


            Dim risultatoCheck As Boolean
            Dim dataOggi As Date = Today.Date
            Dim it As String = DateTime.Now.Date.ToString("dd/MM/yyyy", New CultureInfo("it-IT"))

            Dim DettaglioRinnovo As New DatiNuovoRinnovo


            risultatoCheck = AsiModel.controllaCodiceFiscale(Trim(txtCodiceFiscale.Text), it)
            DettaglioRinnovo = AsiModel.Rinnovi.CaricaDatiTesseramento(txtCodiceFiscale.Text)
            Session("visto") = "ok"
            If risultatoCheck = True Then

                '   Response.Write("ok")
                Session("procedi") = "OK"
                Session("codiceFiscale") = Trim(txtCodiceFiscale.Text)



                CaricaDatiDocumentoRinnovo(Session("IDRinnovo"), Session("id_record"), Trim(txtCodiceFiscale.Text),
DettaglioRinnovo.Nome, DettaglioRinnovo.Cognome, DettaglioRinnovo.CodiceTessera, DettaglioRinnovo.DataScadenza, DettaglioRinnovo.ComuneNascita, DettaglioRinnovo.DataNascita)



                Response.Redirect("richiestaRinnovo.aspx?codR=" & deEnco.QueryStringEncode(Session("IDRinnovo")) & "&record_ID=" & deEnco.QueryStringEncode(Session("id_record")))



            Else

                'Response.Write("ko")

                Session("procedi") = "KO"
                Response.Redirect("DashboardRinnovi.aspx?ris=" & deEnco.QueryStringEncode("ko"))


            End If




        End If
    End Sub

    Public Function CaricaDatiDocumentoRinnovo(codR As String, IDRinnovo As String, codiceFiscale As String,
                                             nome As String, cognome As String, codiceTessera As String, dataScadenza As String, comuneNascita As String, datanascita As String) As Boolean
        '  Dim litNumRichieste As Literal = DirectCast(ContentPlaceHolder1.FindControl("LitNumeroRichiesta"), Literal)


        ' Dim ds As DataSet


        Dim fmsP As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
        '  Dim ds As DataSet
        Dim risposta As String = ""
        fmsP.SetLayout("webRinnoviRichiesta")
        Dim Request = fmsP.CreateEditRequest(IDRinnovo)


        Request.AddField("Rin_CFVerificatoTessera", "1")

        Request.AddField("Rin_CodiceFiscale", codiceFiscale)
        Request.AddField("Rin_NumeroTessera", codiceTessera)
        Request.AddField("Rin_Nome", nome)
        Request.AddField("Rin_Cognome", cognome)
        Request.AddField("Rin_DataScadenza", Data.SistemaData(dataScadenza))
        Request.AddField("Rin_ComuneNascita", comuneNascita)
        Request.AddField("Rin_DataNascita", Data.SistemaData(datanascita))



        '    Request.AddScript("SistemaEncodingCorsoFase2", IDCorso)

        '  Try
        risposta = Request.Execute()



        '  Catch ex As Exception

        '  End Try



        Return True
    End Function
End Class