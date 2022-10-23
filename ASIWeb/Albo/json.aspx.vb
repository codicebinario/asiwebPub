Imports System.Data
Imports System.IO
Imports DocumentFormat.OpenXml.Wordprocessing
Imports System.Net
Imports System.Security.Policy
Imports System.Windows
Imports Newtonsoft.Json
Imports RestSharp
Imports Newtonsoft.Json.Linq
Imports DocumentFormat.OpenXml.Office2010.ExcelAc
Imports DocumentFormat.OpenXml.Bibliography
Imports System.Runtime.Serialization
Imports Microsoft.VisualBasic.Logging
Imports DocumentFormat.OpenXml.Drawing.Charts
Imports ASIWeb.Fonte
Imports System.Threading.Tasks
Imports System.Runtime.CompilerServices
Imports System.Net.Http
Imports System.Web.Script.Serialization

Public Class json
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        jsonRead()
    End Sub
    Sub jsonRead()

        Dim url As String = ""
        Dim esito As Boolean = False
        Dim token As String = PrendiToken()

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        url = "https://93.63.195.98/fmi/data/vLatest/databases/Asi/layouts/testApi/records/"

        Dim clientF = New RestClient(url)
        clientF.Timeout = -1
        Dim RequestF = New RestRequest(Method.GET)
        RequestF.AddHeader("Authorization", "Bearer " & token)

        RequestF.AddHeader("Cookie", "mn_salt=gyKolfJ8PXuqHKOUE0ic0VhxTV3DbdiK")
        '   Try


        Dim ResponseF As IRestResponse = clientF.Execute(RequestF)


        Dim j As Object = New JavaScriptSerializer().Deserialize(Of Object)(ResponseF.Content)
        Dim quanti = j("response")("dataInfo")("totalRecordCount")
        '   Dim Codice_Status = j("response")("data")(0)("fieldData")("Codice_Status")
        '  Dim IDCorso = j("response")("data")(0)("fieldData")("IDCorso")
        Dim codice_status As String = ""
        Dim idCorso As String = ""
        Dim i As Integer = 0
        For i = 0 To CInt(quanti - 1)

            codice_status = j("response")("data")(i)("fieldData")("Id")
            idCorso = j("response")("data")(i)("fieldData")("Cognome")
            '   Response.Write("Quanti: " & quanti.ToString)
            Response.Write(i & " - ID: " & codice_status.ToString & " - Cognome: " & idCorso.ToString & "<br/>")

        Next






    End Sub


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

End Class