<%@ WebHandler Language="VB" Class="HandlerVB" %>
Imports System.IO
Imports System.Net
Imports System.Text
Imports fmDotNet
Imports System.Net.Mail
Imports System.Web.UI.WebControls

Imports System.Security.Cryptography

Imports System.Threading.Tasks
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Web.SessionState
Imports System.Web.Script.Serialization
Imports RestSharp

Imports System.Collections.Generic

Imports System.Net.http
'Imports FMdotNet__DataAPI

Imports System.Linq
Imports System.Configuration
Imports System.Drawing
Imports Image = System.Drawing.Image
Imports System
Imports System.Web
Imports System.Net.Security


Public Class HandlerVB : Implements IHttpHandler

    Dim webserver As String = ConfigurationManager.AppSettings("webserver")
    Dim utente As String = ConfigurationManager.AppSettings("utente")
    Dim porta As String = ConfigurationManager.AppSettings("porta")
    Dim pass As String = ConfigurationManager.AppSettings("pass")
    Dim dbb As String = ConfigurationManager.AppSettings("dbb")

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim codR As String = context.Request.QueryString("hcodR").ToString()
        Dim token As String = ""
        Dim ext As String = " "
        Dim nomefileReale As String = " "

        Dim nomecaricato As String = ""
        For Each key As String In context.Request.Files
            Dim postedFile As HttpPostedFile = context.Request.Files(key)
            Dim folderPath As String = context.Server.MapPath("~/file_storage/")
            If Not Directory.Exists(folderPath) Then
                Directory.CreateDirectory(folderPath)
            End If

            token = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()))
            ext = Path.GetExtension((postedFile.FileName))
            nomecaricato = codR & "_" + token + ext

            postedFile.SaveAs(folderPath + nomecaricato)
            '  postedFile.SaveAs(folderPath + postedFile.FileName)

            ' context.Response.Redirect("~/up.aspx")

        Next

        Dim tokenx As String = ""
        Dim id_att As String = ""
        Dim tuttoRitorno As String = ""

        tuttoRitorno = NuovaRichiestaAllegato(codR, nomecaricato)

        Dim arrKeywords As String() = Split(tuttoRitorno, "_|_")
        tokenx = arrKeywords(1)
        id_att = arrKeywords(0)





        context.Response.StatusCode = 200
        context.Response.ContentType = "text/plain"

        context.Response.Write(nomecaricato)








    End Sub


    Public Function NuovaRichiestaAllegato(codR As String, nomecaricato As String) As String
        '  Dim litNumRichieste As Literal = DirectCast(ContentPlaceHolder1.FindControl("LitNumeroRichiesta"), Literal)

        Dim fmsP As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
        ' Dim ds As DataSet


        fmsP.SetLayout("web_richiesta_allegati")
        Dim Request = fmsP.CreateNewRecordRequest()

        Request.AddField("Codice_Richiesta", codR)
        Request.AddField("NomeFileOnFS", nomecaricato)

        'Request.AddField("Codice_Articolo", AsiModel.CodiceArticolo)
        'Request.AddField("Nome_Articolo", AsiModel.NomeArticolo)
        'Request.AddField("Quantita", txtQuantita.Text)


        Dim IdAllegato As String = Request.Execute()

        Dim record_id As String = ASIWeb.AsiModel.GetRecord_IDbyCodR.GetRecord_ID(codR)

        Dim fmsP1 As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
        '  Dim ds As DataSet
        Dim risposta As String = ""
        fmsP1.SetLayout("web_richiesta_master")
        Dim Request1 = fmsP1.CreateEditRequest(record_id)


        Request1.AddField("Status_ID", "6")

        risposta = Request1.Execute()
        Dim token = PrendiToken()

        '     CaricaInFM(context, token, record_id)

        Return record_id & "_|_" & token
    End Function

    'Public Function CaricaInFM(context As HttpContext, token As String, record_id As String) As Boolean
    '    Dim cont As HttpContext
    '    Dim filePath As String = cont.Server.MapPath("~/file_storage/")

    '    Dim File As HttpPostedFile = inputfile.PostedFile



    '    Dim dove = Path.Combine(filePath, inputfile.PostedFile.FileName)
    '    File.SaveAs(dove)


    '    Return True
    'End Function


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

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class