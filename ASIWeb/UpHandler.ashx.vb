
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

Imports System.Web.Script.Serialization
Imports RestSharp

Imports System.Collections.Generic


'Imports FMdotNet__DataAPI

Imports System.Linq
Imports System.Configuration
Imports System.Drawing
Imports Image = System.Drawing.Image
Imports System
Imports System.Web

Public Class UpHandler
    Implements System.Web.IHttpHandler

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        For Each key As String In context.Request.Files
            Dim postedFile As HttpPostedFile = context.Request.Files(key)
            Dim folderPath As String = context.Server.MapPath("~/file_storage/")
            If Not Directory.Exists(folderPath) Then
                Directory.CreateDirectory(folderPath)
            End If
            postedFile.SaveAs(folderPath + postedFile.FileName)
        Next

        context.Response.StatusCode = 200
        context.Response.ContentType = "text/plain"
        context.Response.Write("Success")


        Dim auth As String

        Dim urlsign As String = "https://93.63.195.98/fmi/data/vLatest/databases/Asi/sessions"


        'Using client = New WebClient()
        '    client.Encoding = System.Text.Encoding.UTF8

        '    client.Headers.Add(HttpRequestHeader.ContentType, "application/json")
        '    client.UseDefaultCredentials = True
        '    client.Headers.Add("Authorization", "Basic " +
        '   Convert.ToBase64String(Encoding.UTF8.GetBytes("enteweb:web01")))

        '    '      Try
        '    Dim reply As String = client.UploadString(New Uri(urlsign), "POST", "")
        '    Dim risposta1 = JsonConvert.DeserializeObject(reply)

        '    Dim bearer = JObject.Parse(risposta1.ToString)
        '    Dim token = bearer.SelectToken("response.token").ToString()
        '    auth = "Bearer " + token
        '    auth = token
        '    '    Response.Write(auth)
        '    '   Catch ex As Exception

        '    '   Response.Redirect("errore.aspx?esito=errore&tipo=" & " token non ricevuto")

        '    '  End Try
        '    '    Response.Write(Server.MapPath("\img\2.jpg"))
        'End Using







    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property


End Class