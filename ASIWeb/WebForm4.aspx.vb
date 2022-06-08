Imports fmDotNet
Imports System.Web.Services
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed
Imports System.Drawing
Imports System.Drawing.Imaging

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
Imports Image = System.Drawing.Image
Imports RestSharp.Authenticators

Public Class WebForm4
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '
        Dim pdf = FotoS("https://crm.asinazionale.it/fmi/xml/cnt/181_MzRmZmY0OWEtMGRlMi00NWY5LWE0Y2ItMTI4M2UxZmE4ODk0.pdf?-db=Asi&-lay=webCorsiRichiesta&-recid=181&-field=Programma_Tecnico_Didattico(1)")




        '  plPdf.Controls.Add(New LiteralControl("<a href='#' onclick='GotPDF(" + pdf + ")'>ciao</a>"))

        '  Response.Write(pdf.ToString)
        '     SurroundingSub()
    End Sub

    Public Function FotoS(urlFoto As String)

        ' Dim pictureURL As String = "https://93.63.195.98/fmi/xml/cnt/181_MzRmZmY0OWEtMGRlMi00NWY5LWE0Y2ItMTI4M2UxZmE4ODk0.pdf?-db=Asi&-lay=webCorsiRichiesta&-recid=181&-field=Programma_Tecnico_Didattico(1)"
        Dim pictureURL As String = urlFoto

        Dim wClient As WebClient = New WebClient()
        Dim nc As NetworkCredential = New NetworkCredential("enteweb", "web01")
        wClient.Credentials = nc
        Dim response1 As Stream = wClient.OpenRead(pictureURL)
        Dim temp As Stream = response1
        Using stream As MemoryStream = New MemoryStream()
            CopyStream(temp, stream)
            Dim bytes As Byte() = stream.ToArray()
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "application/pdf"
            Response.AddHeader("content-disposition", "attachment;filename=Example.pdf")
            Response.BinaryWrite(bytes)
            response.Flush()
            response.End()
        End Using




        Return temp

    End Function

    Public Shared Sub CopyStream(ByVal input As Stream, ByVal output As Stream)
        Dim buffer As Byte() = New Byte(4095) {}
        While True
            Dim read As Integer = input.Read(buffer, 0, buffer.Length)
            If read <= 0 Then
                Return
            End If
            output.Write(buffer, 0, read)
        End While
    End Sub

    Private Sub SurroundingSub()
        Dim client = New RestClient("https://crm.asinazionale.it/fmi/xml/cnt/181_MzRmZmY0OWEtMGRlMi00NWY5LWE0Y2ItMTI4M2UxZmE4ODk0.pdf?-db=Asi&-lay=webCorsiRichiesta&-recid=181&-field=Programma_Tecnico_Didattico(1)")
        client.Timeout = -1
        client.Authenticator = New HttpBasicAuthenticator("enteweb", "web01")
        Dim request = New RestRequest(Method.[GET])
        '  request.AddHeader("Authorization", "Basic ZW50ZXdlYjp3ZWIwMQ==")
        '   request.AddHeader("Cookie", "WPCSessionID=9omVnjUMb3sr4xa3jqyf4w==")
        Dim response1 As IRestResponse = client.Execute(request)


        'Dim fs = response1
        'Dim br As New System.IO.BinaryReader(fs)
        'Dim bytes As Byte() = br.ReadBytes(CType(fs.ContentLength, Integer))
        'Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
        'lnkPdf.PostBackUrl = "data:application/pdf;base64," & response1.Content
        'lnkPdf.Visible = True


        'Dim bytes As Byte() = Convert.FromBase64String(response1.ToString)
        'File.WriteAllBytes("d:\pdfFileName.pdf", bytes)

        'Response.AddHeader("Content-Type", "application/pdf")
        ' Response.AddHeader("Content-Length", response1.Length.ToString())
        ' Response.AddHeader("Content-Disposition", "inline;")
        '   Response.AddHeader("Cache-Control", "private, max-age=0, must-revalidate")
        '   Response.AddHeader("Pragma", "public")
        'Response.BinaryWrite(Convert.FromBase64String(response1.ToString))
        ' Set the appropriate ContentType.
        'Response.ClearContent()
        'Response.ContentType = "application/pdf"
        'Response.AddHeader("Content-Disposition", "attachment; filename=pippo.pdf")
        'Response.AddHeader("Content-Length", response1.Content.Length.ToString())
        ''   Response.BinaryWrite(response1.Content.ToString())
        'Response.End()

        ' Write file directly to browser.
        Response.Write(response1.Content)
    End Sub



End Class


