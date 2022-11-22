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

Public Class checkTesseramento
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


            Session("IDEquiparazione") = codR
            Dim DettaglioEquiparazione As New DatiNuovaEquiparazione
            DettaglioEquiparazione = Equiparazione.PrendiValoriNuovaEquiparazione(Session("IDEquiparazione"))
            Dim IDEquiparazione As String = DettaglioEquiparazione.IDEquiparazione
            Dim CodiceEnteRichiedente As String = DettaglioEquiparazione.CodiceEnteRichiedente
            Dim DescrizioneEnteRichiedente As String = DettaglioEquiparazione.DescrizioneEnteRichiedente
            Dim TipoEnte As String = DettaglioEquiparazione.TipoEnte
            Dim CodiceStatus As String = DettaglioEquiparazione.CodiceStatus
            Dim DescrizioneStatus As String = DettaglioEquiparazione.DescrizioneStatus
            HiddenIdRecord.Value = DettaglioEquiparazione.IdRecord
            HiddenIDEquiparazione.Value = DettaglioEquiparazione.IDEquiparazione
            lblIntestazioneEquiparazione.Text = "<strong>ID Equiparazione: </strong>" & IDEquiparazione & "<strong> - Ente Richiedente: </strong>" & DescrizioneEnteRichiedente
        End If

        If Page.IsPostBack Then

            '  pnlFase1.Visible = False


        End If
    End Sub

    '    Protected Sub btnCheck_Click(sender As Object, e As EventArgs) Handles btnCheck.Click
    '        If Page.IsValid Then


    '            Dim risultatoCheck As Boolean
    '            Dim dataOggi As Date = Today.Date
    '            Dim it As String = DateTime.Now.Date.ToString("dd/MM/yyyy", New CultureInfo("it-IT"))

    '            Dim DettaglioEquiparazione As New DatiNuovaEquiparazione


    '            risultatoCheck = AsiModel.controllaCodiceFiscale(Trim(txtCodiceFiscale.Text), it)
    '            DettaglioEquiparazione = AsiModel.Equiparazione.CaricaDatiTesseramento(txtCodiceFiscale.Text)
    '            Session("visto") = "ok"
    '            If risultatoCheck = True Then

    '                '   Response.Write("ok")
    '                Session("procedi") = "OK"
    '                Session("codiceFiscale") = Trim(txtCodiceFiscale.Text)



    '                CaricaDatiDocumentoEquiparazione(Session("IDEquiparazione"), Session("id_record"), Trim(txtCodiceFiscale.Text),
    'DettaglioEquiparazione.Nome, DettaglioEquiparazione.Cognome, DettaglioEquiparazione.CodiceTessera, DettaglioEquiparazione.DataScadenza)



    '                Response.Redirect("richiestaEquiparazione.aspx?codR=" & deEnco.QueryStringEncode(Session("IDEquiparazione")) & "&record_ID=" & deEnco.QueryStringEncode(Session("id_record")))



    '            Else

    '                'Response.Write("ko")

    '                Session("procedi") = "KO"
    '                Response.Redirect("DashboardEqui.aspx?ris=" & deEnco.QueryStringEncode("ko"))


    '            End If




    '        End If
    '    End Sub

    Public Function CaricaDatiDocumentoEquiparazione(codR As String, IDEquiparazione As String, codiceFiscale As String,
                                             nome As String, cognome As String, codiceTessera As String, dataScadenza As Date) As Boolean
        '  Dim litNumRichieste As Literal = DirectCast(ContentPlaceHolder1.FindControl("LitNumeroRichiesta"), Literal)

        Dim DataScadenzaSistemata As String
        ' Dim ds As DataSet


        Dim fmsP As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
        '  Dim ds As DataSet
        Dim risposta As String = ""
        fmsP.SetLayout("webEquiparazioniRichiesta")
        Dim Request = fmsP.CreateEditRequest(IDEquiparazione)


        Request.AddField("Equi_CFVerificatoTessera", "1")

        Request.AddField("Equi_CodiceFiscale", codiceFiscale)
        Request.AddField("Equi_NumeroTessera", codiceTessera)
        Request.AddField("Equi_Nome", nome)
        Request.AddField("Equi_Cognome", cognome)
        DataScadenzaSistemata = Data.SistemaDataUK(Data.SonoDieci(dataScadenza))
        Request.AddField("Equi_DataScadenza", DataScadenzaSistemata)



        Request.AddField("Equi_Fase", "0")
        '    Request.AddScript("SistemaEncodingCorsoFase2", IDCorso)

        '  Try
        risposta = Request.Execute()



        '  Catch ex As Exception

        '  End Try



        Return True
    End Function

    Protected Sub lnkCheck_Click(sender As Object, e As EventArgs) Handles lnkCheck.Click
        If Page.IsValid Then


            Dim risultatoCheck As Boolean
            Dim dataOggi As Date = Today.Date
            Dim it As String = DateTime.Now.Date.ToString("dd/MM/yyyy", New CultureInfo("it-IT"))

            Dim DettaglioEquiparazione As New DatiNuovaEquiparazione


            risultatoCheck = AsiModel.controllaCodiceFiscale(Trim(txtCodiceFiscale.Text), it)
            DettaglioEquiparazione = AsiModel.Equiparazione.CaricaDatiTesseramento(txtCodiceFiscale.Text)
            Session("visto") = "ok"
            If risultatoCheck = True Then

                '   Response.Write("ok")
                Session("procedi") = "OK"
                Session("codiceFiscale") = Trim(txtCodiceFiscale.Text)



                CaricaDatiDocumentoEquiparazione(Session("IDEquiparazione"), Session("id_record"), Trim(txtCodiceFiscale.Text),
DettaglioEquiparazione.Nome, DettaglioEquiparazione.Cognome, DettaglioEquiparazione.CodiceTessera, DettaglioEquiparazione.DataScadenza)



                Response.Redirect("richiestaEquiparazione.aspx?codR=" & deEnco.QueryStringEncode(Session("IDEquiparazione")) & "&record_ID=" & deEnco.QueryStringEncode(Session("id_record")))



            Else

                'Response.Write("ko")

                Session("procedi") = "KO"
                Response.Redirect("DashboardEqui.aspx?ris=" & deEnco.QueryStringEncode("ko"))


            End If




        End If
    End Sub
End Class