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

Public Class checkTesseramento2
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
    Dim t As Integer = 0
    Dim codR As Integer
    Dim type As String
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

        type = Request.QueryString("type")


        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If
        Dim record_ID As String = ""

        t = Request.QueryString("t")

        codR = deEnco.QueryStringDecode(Request.QueryString("codR"))




        If Page.IsPostBack Then

            '  pnlFase1.Visible = False


        End If
    End Sub



    Public Function CaricaDatiDocumentoEquiparazione(codR As String, codiceFiscale As String,
                                             nome As String, cognome As String, codiceTessera As String, dataScadenza As Date, codiceEnteRichiedente As String) As Integer
        '  Dim litNumRichieste As Literal = DirectCast(ContentPlaceHolder1.FindControl("LitNumeroRichiesta"), Literal)
        Dim idRecord As Integer = 0
        Dim DataScadenzaSistemata As String
        ' Dim ds As DataSet


        Dim fmsP As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
        '  Dim ds As DataSet
        Dim risposta As String = ""
        fmsP.SetLayout("webEquiparazioniRichiestaMolti")


        Dim Request = fmsP.CreateNewRecordRequest()

        Request.AddField("IDEquiparazioneM", codR)
        Request.AddField("Codice_Status", "0")
        Request.AddField("Codice_Ente_Richiedente", codiceEnteRichiedente)

        idRecord = Request.Execute()


        Dim SettaggioCulture As CultureInfo = CultureInfo.CreateSpecificCulture("it-IT")
        Thread.CurrentThread.CurrentCulture = SettaggioCulture
        Thread.CurrentThread.CurrentUICulture = SettaggioCulture
        ' DateTime.Parse(dataScadenza, SettaggioCulture)
        Dim DataScadenzaPulita As String

        DataScadenzaPulita = DateTime.Parse(Data.SonoDieci(dataScadenza), SettaggioCulture)






        Dim RequestE = fmsP.CreateEditRequest(idRecord)


        RequestE.AddField("Equi_CFVerificatoTessera", "1")

        RequestE.AddField("Equi_CodiceFiscale", codiceFiscale)
        RequestE.AddField("Equi_NumeroTessera", codiceTessera)
        RequestE.AddField("Equi_Nome", nome)
        RequestE.AddField("Equi_Cognome", cognome)
        DataScadenzaSistemata = Data.SistemaDataUK(Data.SonoDieci(dataScadenza))
        RequestE.AddField("Equi_DataScadenza", DataScadenzaSistemata)



        RequestE.AddField("Equi_Fase", "0")
        '    Request.AddScript("SistemaEncodingCorsoFase2", IDCorso)

        '  Try
        risposta = RequestE.Execute()



        '  Catch ex As Exception

        '  End Try



        Return idRecord
    End Function

    Protected Sub lnkCheck_Click(sender As Object, e As EventArgs) Handles lnkCheck.Click
        If Page.IsValid Then

            If Not String.IsNullOrEmpty(codR) Then

                Dim CFCheck As Boolean

                CFCheck = AsiModel.controllaCodiceFiscaleEquipazione(Trim(txtCodiceFiscale.Text), codR)

                Dim risultatoCheck As Integer
                Dim dataOggi As Date = Today.Date
                Dim it As String = DateTime.Now.Date.ToString("dd/MM/yyyy", New CultureInfo("it-IT"))

                Dim DettaglioEquiparazione As New DatiNuovaEquiparazione


                risultatoCheck = AsiModel.controllaCodiceFiscale(Trim(txtCodiceFiscale.Text), it)
                '1 tessera valida e non scaduto 
                '2 tessera valida ma scaduta
                '3 tessera non trovata
                '4 errore generico di connessione
                DettaglioEquiparazione = AsiModel.Equiparazione.CaricaDatiTesseramento(txtCodiceFiscale.Text)

                Session("visto") = "ok"

                If CFCheck = True Then

                    Response.Redirect("DashboardEqui2.aspx?open=" & codR & "&ris=" & deEnco.QueryStringEncode("cfInEqui"))


                End If

                If risultatoCheck = 1 Then

                    '   Response.Write("ok")
                    Session("procedi") = "OK"
                    Session("codiceFiscale") = Trim(txtCodiceFiscale.Text)

                    Dim record_ID As String = ""


                    record_ID = CaricaDatiDocumentoEquiparazione(codR, Trim(txtCodiceFiscale.Text),
                    DettaglioEquiparazione.Nome, DettaglioEquiparazione.Cognome, DettaglioEquiparazione.CodiceTessera, DettaglioEquiparazione.DataScadenza, Session("codice"))



                    Response.Redirect("richiestaEquiparazione2.aspx?codR=" & deEnco.QueryStringEncode(codR) & "&record_ID=" & deEnco.QueryStringEncode(record_ID) & "&cf=" & deEnco.QueryStringEncode(Trim(txtCodiceFiscale.Text)))



                Else

                    'Response.Write("ko")
                    '1 tessera valida e non scaduto 
                    '2 tessera valida ma scaduta
                    '3 tessera non trovata
                    '4 errore generico di connessione
                    'Session("procedi") = "KO"
                    Select Case risultatoCheck
                        Case 2
                            Response.Redirect("DashboardEqui2.aspx?open=" & codR & "&ris=" & deEnco.QueryStringEncode("valScad"))
                        Case 3
                            Response.Redirect("DashboardEqui2.aspx?open=" & codR & "&ris=" & deEnco.QueryStringEncode("notFound"))
                        Case 4
                            Response.Redirect("DashboardEqui2.aspx?open=" & codR & "&ris=" & deEnco.QueryStringEncode("erroreGen"))

                    End Select


                    '    Response.Redirect("DashboardEqui2.aspx?ris=" & deEnco.QueryStringEncode("ko"))


                End If




            End If
        End If
    End Sub
End Class