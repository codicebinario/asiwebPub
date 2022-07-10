﻿Imports fmDotNet
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
Public Class scaricaTessera

    Inherits System.Web.UI.Page
    Dim codiceCorso As String
    Dim deEnco As New Ed
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If
        ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "redirectScript", "window.location.href='whateverurlhere.aspx';", True)



        Dim pdf As String
        codiceCorso = deEnco.QueryStringDecode(Request.QueryString("codR"))
        Dim record_ID As String = deEnco.QueryStringDecode(Request.QueryString("record_ID"))
        Dim nomeFilePC As String = deEnco.QueryStringDecode(Request.QueryString("nomeFilePC"))

        pdf = FotoS("https://crm.asinazionale.it/fmi/xml/cnt/ " & nomeFilePC & "?-db=Asi&-lay=webCorsisti&-recid=" & record_ID & "&-field=Tessera(1)")


        ' Response.Redirect("corsistiDoc.apsx?codR=" & deEnco.QueryStringEncode(codiceCorso) & "&record_ID=" & deEnco.QueryStringEncode(record_ID))
        ' Session("ScaricaDiploma") = "ok"







    End Sub

    Public Function FotoS(urlFoto As String)

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
            Response.Flush()

            Response.End()


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


End Class