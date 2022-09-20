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
Public Class scaricaDiploma

    Inherits System.Web.UI.Page
    Dim codiceCorso As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If

        'If Not Page.IsPostBack Then


        '    If Session("ScaricaTessera") = "ok" Then
        '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Tessera download effettuato! ' ).set('resizable', true).resizeTo('20%', 200);", True)
        '        Session("ScaricaTessera") = Nothing
        '    End If
        'End If



        Dim deEnco As New Ed
        Dim pdf As String
        codiceCorso = deEnco.QueryStringDecode(Request.QueryString("codR"))
        Dim record_ID As String = deEnco.QueryStringDecode(Request.QueryString("record_ID"))
        Dim nomeFilePC As String = deEnco.QueryStringDecode(Request.QueryString("nomeFilePC"))

        pdf = FotoS("https://crm.asinazionale.it/fmi/xml/cnt/ " & nomeFilePC & "?-db=Asi&-lay=webCorsisti&-recid=" & record_ID & "&-field=Diploma(1)", record_ID)








        '  Response.Redirect("corsistiDoc.aspx")

        '  Dim IDRecord As String = deEnco.QueryStringDecode(Request.QueryString("id"))
        'Dim risposta As Integer = 0
        'Dim fmsP As FMSAxml = AsiModel.Conn.Connect()

        'fmsP.SetLayout("webCorsiRichiesta")

        'Dim RequestP = fmsP.CreateEditRequest(record_ID)
        'RequestP.AddField("Codice_Status", "101")

        'Try
        '    risposta = RequestP.Execute()

        '   AsiModel.LogIn.LogCambioStatus(CodiceRichiesta, "10", Session("WebUserEnte"))
        '  Session("ScaricaTessera") = "ok"

        'Catch ex As Exception

        'End Try





    End Sub

    Public Function FotoS(urlFoto As String, record_id As String)
        Dim nominativo As String = ""
        Dim pictureURL As String = urlFoto
        nominativo = Corso.PrendiNominativo(record_id)

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
            Response.AddHeader("content-disposition", "attachment;filename=" & nominativo & "_Diploma.pdf")
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