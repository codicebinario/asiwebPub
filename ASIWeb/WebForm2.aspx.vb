Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Imports System.Web.Script.Serialization
Imports RestSharp

Imports System.Collections.Generic
Imports System.Net.Security
Imports System.Net

Imports System.Drawing
Imports System.Drawing.Imaging

Imports System.IO

Public Class WebForm2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Foto()

        Dim myImage As Image = Foto()
        Dim base64 As String = ImageHelper.ImageToBase64String(myImage, ImageFormat.Jpeg)
        Response.Write("<img alt=""Embedded Image"" src=""data:image/Jpeg;base64," & base64 & """ />")

        ' Response.Write(Foto())

    End Sub

    Public Function Foto()

        Dim pictureURL As String = "https://93.63.195.98/fmi/xml/cnt/145_ZGQ5YmRiOTYtZjc0YS00NWFmLTgyNTAtZTIyMjRjYjgzYzg0.jpg?-db=Asi&-lay=webCorsisti&-recid=145&-field=Foto(1)"
        Dim wClient As WebClient = New WebClient()
        Dim nc As NetworkCredential = New NetworkCredential("enteweb", "web01")
        wClient.Credentials = nc
        Dim response As Stream = wClient.OpenRead(pictureURL)
        Dim temp = Image.FromStream(response)
        response.Close()
        Return temp

    End Function


End Class