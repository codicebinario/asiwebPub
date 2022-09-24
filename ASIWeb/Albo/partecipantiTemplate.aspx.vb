Imports System.IO
Imports System.Net
Imports AjaxControlToolkit
Imports fmDotNet

Public Class partecipantiTemplate
    Inherits System.Web.UI.Page
    Dim nomeFile As String = "partecipantiTemplate.xlsx"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If


        Dim pdf As String
        'codiceCorso = deEnco.QueryStringDecode(Request.QueryString("codR"))
        'play = deEnco.QueryStringDecode(Request.QueryString("play"))
        'Dim record_ID As String = deEnco.QueryStringDecode(Request.QueryString("record_ID"))
        'Dim nomeFilePC As String = deEnco.QueryStringDecode(Request.QueryString("nomeFilePC"))

        pdf = FotoS("https://crm.asinazionale.it/fmi/xml/cnt/ " & nomeFile & "?-db=Asi&-lay=webUtility&-recid=" & 3 & "&-field=TemplateExcel(1)", nomeFile)


        'Dim filePath As String = Server.MapPath(ResolveUrl("~/file_storage_Excel/"))
        'Dim filePathV As String = Server.MapPath("~/file_storage_Excel/")
        ''   deleteFile(Session("filenuovo"))


        'Dim File As String = "partecipantiTemplate.xlsx"

        'Dim dove As String = filePath & File

        'FotoS(dove, File)

    End Sub
    Public Function FotoS(urlFoto As String, nomefile As String)

        Dim pictureURL As String = urlFoto
        Dim nominativo As String = ""

        '    nominativo = Corso.PrendiNominativo(record_ID)

        Dim wClient As WebClient = New WebClient()
        Dim nc As NetworkCredential = New NetworkCredential("enteweb", "web01")
        wClient.Credentials = nc
        Dim response1 As Stream = wClient.OpenRead(pictureURL)
        Dim temp As Stream = response1
        Using stream As MemoryStream = New MemoryStream()
            CopyStream(temp, stream)
            Dim bytes As Byte() = stream.ToArray()
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "application/xlsx"
            Response.AddHeader("content-disposition", "attachment;filename=" & nomefile)
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