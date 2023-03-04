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
Public Class upDichiarazione2
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
    Dim CodiceEnteRichiedente As String = ""
    Dim codiceFiscale As String = ""
    Dim idSelected As String = ""
    Dim record_ID As String = ""
    Dim codR As String = ""
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

        idSelected = deEnco.QueryStringDecode(Request.QueryString("idSelected"))

        If Not String.IsNullOrEmpty(idSelected) Then

            Session("idSelected") = idSelected

        End If

        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If

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

            DettaglioRinnovo = Rinnovi.PrendiValoriNuovoRinnovo2(Session("id_record"))
            Dim verificato As String = DettaglioRinnovo.RinnovoCF
            If verificato = "0" Then
                Response.Redirect("DashboardRinnovi2.aspx?ris=" & deEnco.QueryStringEncode("no"))

            End If
            Dim IDRinnovo As String = DettaglioRinnovo.IDRinnovo
            CodiceEnteRichiedente = DettaglioRinnovo.CodiceEnteRichiedente
            Dim DescrizioneEnteRichiedente As String = DettaglioRinnovo.DescrizioneEnteRichiedente
            Dim TipoEnte As String = DettaglioRinnovo.TipoEnte
            Dim CodiceStatus As String = DettaglioRinnovo.CodiceStatus
            Dim DescrizioneStatus As String = DettaglioRinnovo.DescrizioneStatus
            '      leggiDatiEsistenti(Session("codiceFiscale"))

            HiddenIdRecord.Value = DettaglioRinnovo.IdRecord
            HiddenIDRinnovo.Value = DettaglioRinnovo.IDRinnovo
            codiceFiscale = DettaglioRinnovo.CodiceFiscale
            Dim datiCF = AsiModel.getDatiCodiceFiscale(codiceFiscale)

            lblIntestazioneRinnovo.Text = "" &
                "<strong> - Codice Fiscale: </strong>" & UCase(datiCF.CodiceFiscale) &
                "<strong> - N.Tessera: </strong>" & datiCF.CodiceTessera & "<br />" &
                "<strong> - Nominativo: </strong>" & datiCF.Nome & " " & datiCF.Cognome &
                "<strong> - Ente Richiedente: </strong>" & DescrizioneEnteRichiedente


        End If

        If Page.IsPostBack Then

            '  pnlFase1.Visible = False


        End If
    End Sub


    Public Function CaricaSuFM(tokenx As String, id As String, nomecaricato As String) As Boolean

        Dim host As String = HttpContext.Current.Request.Url.Host.ToLower()

        Dim filePath As String = Server.MapPath(ResolveUrl("~/file_storage_dichiarazioni/"))

        '  Dim File As HttpPostedFile = inputfile.PostedFile
        Dim File As String = nomecaricato


        Dim dove = Path.Combine(filePath, File)
        ' File.SaveAs(dove)



        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12


        '    Try


        Dim clientUP As New RestClient("https://93.63.195.98/fmi/data/vLatest/databases/Asi/layouts/webRinnoviRichiesta2/records/" & id & "/containers/DichiarazioneEA/1")
        clientUP.Timeout = -1
        Dim Requestx = New RestRequest(Method.POST)
        Requestx.AddHeader("Content-Type", "application/json")
        Requestx.AddHeader("Content-Type", "multipart/form-data")
        Requestx.AddHeader("Authorization", "bearer " & tokenx.ToString())
        Requestx.AddParameter("modId", "1")
        'If host = "localhost" Then
        '    Requestx.AddParameter("upload", "D:\Soft\c#\webUP\webUP\file_storage\" & nomecaricato & "")
        'Else
        '    Requestx.AddParameter("upload", "D:\Soft\c#\webUP\webUP\file_storage\" & nomecaricato & "")
        'End If

        '  Dim filePath As String = Server.MapPath(ResolveUrl("~/file_storage/"))
        Requestx.AddParameter("upload", Server.MapPath(ResolveUrl("~/file_storage_dichiarazioni/")) & nomecaricato & "")
        '  Requestx.AddParameter("upload", "D:\Dropbox\soft\Projects\ASIWeb\ASIWeb\file_storage" & nomecaricato & "")
        '   Requestx.AddParameter("upload", "C:\file_storage\" & nomecaricato & "")
        Requestx.AddFile("upload", dove)


        Dim ResponseUP As IRestResponse = clientUP.Execute(Requestx)

        Return True

        ' Catch ex As Exception

        '  End Try


    End Function
    Public Function CaricaDatiDocumentoCorso(codR As String, IDRinnovo As String, nomecaricato As String) As String
        '  Dim litNumRichieste As Literal = DirectCast(ContentPlaceHolder1.FindControl("LitNumeroRichiesta"), Literal)


        ' Dim ds As DataSet


        Dim fmsP As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
        '  Dim ds As DataSet
        Dim risposta As String = ""
        fmsP.SetLayout("webRinnoviRichiesta2")
        Dim Request = fmsP.CreateEditRequest(codR)
        Request.AddField("DichiarazioneEAOnFS", nomecaricato)
        Request.AddField("NoteDichiarazione", Data.PrendiStringaT(Server.HtmlEncode(txtNote.Text)))
        Request.AddField("Codice_Status", "152")
        Request.AddField("DichiarazioneEAInviata", "s")
        Request.AddScript("SistemaEncodingNoteDichiarazioneRinnovi", IDRinnovo)

        AsiModel.LogIn.LogCambioStatus(IDRinnovo, "152", Session("WebUserEnte"), "rinnovo")

        Try
            risposta = Request.Execute()




        Catch ex As Exception

        End Try

        Dim token = PrendiToken()

        Return codR & "_|_" & token
    End Function

    Public Function PrendiToken() As String
        Dim urlsign As String = "https://93.63.195.98/fmi/data/vLatest/databases/Asi/sessions"
        Dim auth As String = ""
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        Using client1 = New WebClient()
            client1.Encoding = System.Text.Encoding.UTF8

            client1.Headers.Add(HttpRequestHeader.ContentType, "application/json")
            client1.UseDefaultCredentials = True
            client1.Headers.Add("Authorization", "Basic " +
           Convert.ToBase64String(Encoding.UTF8.GetBytes("enteweb:web01")))

            '   Try
            Dim reply1 As String = client1.UploadString(New Uri(urlsign), "POST", "")
            Dim risposta1 = JsonConvert.DeserializeObject(reply1)

            Dim bearer = JObject.Parse(risposta1.ToString)
            Dim token = bearer.SelectToken("response.token").ToString()
            auth = "Bearer " + token
            auth = token
            ' Response.Write(auth)
            '   Catch ex As Exception

            '   Response.Redirect("errore.aspx?esito=errore&tipo=" & " token non ricevuto")

            ' End Try
            '    Response.Write(Server.MapPath("\img\2.jpg"))
        End Using
        Return auth

    End Function

    Protected Sub btnFase2_Click(sender As Object, e As EventArgs) Handles btnFase2.Click
        Session("fase") = "2"
        Response.Redirect("richiestaCorsoF2.aspx?codR=" & deEnco.QueryStringEncode(Session("IDCorso")) & "&record_ID=" & deEnco.QueryStringEncode(Session("id_record")))
    End Sub

    Protected Sub lnkButton1_Click(sender As Object, e As EventArgs) Handles lnkButton1.Click
        If uploadProgress.Files.Count > 0 Then







            '****************************************************
            Dim files As OboutFileCollection = uploadProgress.Files
            Dim i As Integer

            uploadedFiles.Text = ""
            '   Try

            For i = 0 To files.Count - 1 Step 1
                Dim file As OboutPostedFile = files(i)



                Dim whereToSave As String = "../file_storage_dichiarazioni/"

                tokenZ = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()))
                ext = Path.GetExtension((file.FileName))
                nomefileReale = Path.GetFileName(file.FileName)
                nomecaricato = HiddenIdRecord.Value & "_" + tokenZ + ext





                '   nomecaricato = "cv_" + Session("codiceProvvisorio") + ext

                file.SaveAs(MapPath(whereToSave + nomecaricato))




                If uploadedFiles.Text.Length = 0 Then


                    lnkButton1.Enabled = False
                    uploadedFiles.Text = ""

                    uploadedFiles.Text = "<b>Documento caricato con successo: " & nomecaricato & "</b><br/>"




                End If



            Next

            Dim tokenx As String = ""
            Dim id_att As String = ""
            Dim tuttoRitorno As String = ""

            tuttoRitorno = CaricaDatiDocumentoCorso(HiddenIdRecord.Value, HiddenIDRinnovo.Value, nomecaricato)
            txtNote.Text = ""
            Dim arrKeywords As String() = Split(tuttoRitorno, "_|_")
            tokenx = arrKeywords(1)
            id_att = arrKeywords(0)
            CaricaSuFM(tokenx, id_att, nomecaricato)
            '   pnlFase1.Visible = False
            ' btnFase2.Visible = True
            'deleteFile(nomecaricato)
            '  Session("fase") = "2"
            '  Response.Redirect("dashboardB.aspx?codR=" & deEnco.QueryStringEncode(Session("IDCorso")) & "&record_ID=" & deEnco.QueryStringEncode(Session("id_record")) & "&nomef=" & nomecaricato)
            ' Response.Redirect("dashboardRinnovi2.aspx#" & Session("IDRinnovo"))
            Response.Redirect("richiestaRinnovo12.aspx?idSelected=" & deEnco.QueryStringEncode(Session("idSelected")) & "&codR=" & deEnco.QueryStringEncode(Session("IDRinnovo")) & "&record_ID=" & deEnco.QueryStringEncode(Session("id_record")))




            '   Catch ex As Exception

            '      uploadedFiles.Text = "<b>Documento non caricato: </b><br/>"


            '   End Try

        Else
            uploadedFiles.Text = "<b>Il Documento non deve superare i 2 mb di dimensione! </b><br/>"
        End If
    End Sub
End Class