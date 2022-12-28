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
Imports System.Threading

Public Class checkTesseramentoRinnovi2
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
    Dim codR As Integer = 0
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
        'Dim record_ID As String = ""
        'record_ID = deEnco.QueryStringDecode(Request.QueryString("record_ID"))
        'If Not String.IsNullOrEmpty(record_ID) Then

        '    Session("id_record") = record_ID

        'End If

        'If IsNothing(Session("id_record")) Then
        '    Response.Redirect("../login.aspx")
        'End If

        codR = deEnco.QueryStringDecode(Request.QueryString("codR"))
        'If Not String.IsNullOrEmpty(codR) Then


        '    Session("IDRinnovo") = codR
        '    Dim DettaglioRinnovo As New DatiNuovoRinnovo
        '    DettaglioRinnovo = Rinnovi.PrendiValoriNuovoRinnovo(Session("IDRinnovo"))
        '    Dim IDRinnovo As String = DettaglioRinnovo.IDRinnovo
        '    Dim CodiceEnteRichiedente As String = DettaglioRinnovo.CodiceEnteRichiedente
        '    Dim DescrizioneEnteRichiedente As String = DettaglioRinnovo.DescrizioneEnteRichiedente
        '    Dim TipoEnte As String = DettaglioRinnovo.TipoEnte
        '    Dim CodiceStatus As String = DettaglioRinnovo.CodiceStatus
        '    Dim DescrizioneStatus As String = DettaglioRinnovo.DescrizioneStatus
        '    HiddenIdRecord.Value = DettaglioRinnovo.IdRecord
        '    HiddenIDRinnovo.Value = DettaglioRinnovo.IDRinnovo
        '    lblIntestazioneRinnovo.Text = "<strong>ID Rinnovo: </strong>" & IDRinnovo & "<strong> - Ente Richiedente: </strong>" & DescrizioneEnteRichiedente
        'End If

        If Page.IsPostBack Then

            '  pnlFase1.Visible = False


        End If
    End Sub


    Public Function CaricaDatiDocumentoRinnovo(codR As String, codiceEnte As String, codiceFiscale As String,
                                             nome As String, cognome As String, codiceTessera As String, dataScadenza As String, comuneNascita As String, datanascita As String) As Integer

        Dim idRecord As Integer = 0

        Dim fmsP As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
        '  Dim ds As DataSet
        Dim risposta As Integer = 0
        fmsP.SetLayout("webRinnoviRichiesta2")

        Dim Request = fmsP.CreateNewRecordRequest()

        Request.AddField("IDRinnovoM", codR)
        Request.AddField("Codice_Status", "0")


        idRecord = Request.Execute()

        Dim RequestE = fmsP.CreateEditRequest(idRecord)

        Dim SettaggioCulture As CultureInfo = CultureInfo.CreateSpecificCulture("it-IT")
        Thread.CurrentThread.CurrentCulture = SettaggioCulture
        Thread.CurrentThread.CurrentUICulture = SettaggioCulture
        ' DateTime.Parse(dataScadenza, SettaggioCulture)
        Dim DataScadenzaPulita As String

        DataScadenzaPulita = DateTime.Parse(Data.SonoDieci(dataScadenza), SettaggioCulture)




        RequestE.AddField("Rin_CFVerificatoTessera", "1")
        RequestE.AddField("Codice_Ente_Richiedente", codiceEnte)
        RequestE.AddField("Rin_CodiceFiscale", codiceFiscale)
        RequestE.AddField("Rin_NumeroTessera", codiceTessera)
        RequestE.AddField("Rin_Nome", nome)
        RequestE.AddField("Rin_Cognome", cognome)
        'Request.AddField("Data_ScadenzaTesseraASI", Data.SistemaData(dataScadenza))
        Dim miaDataScadenza As DateTime
        Dim miaDataScadenza2 As DateTime

        RequestE.AddField("Data_ScadenzaTesseraASI", Data.SistemaDataUK(DataScadenzaPulita))


        RequestE.AddField("Rin_ComuneNascita", comuneNascita)
        RequestE.AddField("Rin_DataNascita", Data.SonoDieci(datanascita))

        risposta = RequestE.Execute()


        Return idRecord
    End Function

    Protected Sub lnkCheck_Click(sender As Object, e As EventArgs) Handles lnkCheck.Click
        If Page.IsValid Then

            Dim idrecord As Integer
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



                idrecord = CaricaDatiDocumentoRinnovo(codR, Session("codice"), Trim(txtCodiceFiscale.Text),
DettaglioRinnovo.Nome, DettaglioRinnovo.Cognome, DettaglioRinnovo.CodiceTessera, DettaglioRinnovo.DataScadenza, DettaglioRinnovo.ComuneNascita, DettaglioRinnovo.DataNascita)



                Response.Redirect("richiestaRinnovo2.aspx?codR=" & deEnco.QueryStringEncode(codR) & "&record_ID=" & deEnco.QueryStringEncode(idrecord))



            Else

                'Response.Write("ko")

                Session("procedi") = "KO"
                Response.Redirect("DashboardRinnovi2.aspx?ris=" & deEnco.QueryStringEncode("ko"))


            End If




        End If
    End Sub
End Class