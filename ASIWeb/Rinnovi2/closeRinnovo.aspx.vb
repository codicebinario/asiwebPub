Imports fmDotNet
Imports System.Web.Services
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed
Public Class closeRinnovo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If
        Dim deEnco As New Ed


        Dim record_ID As String = deEnco.QueryStringDecode(Request.QueryString("idrecord"))
        Dim codR As String = deEnco.QueryStringDecode(Request.QueryString("codR"))
        Dim risposta As Integer = 0
        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()

        fmsP.SetLayout("webRinnoviMaster")

        Dim RequestP = fmsP.CreateEditRequest(record_ID)
        RequestP.AddField("CheckWeb", "s")

        Try
            risposta = RequestP.Execute()



        Catch ex As Exception

        End Try





        Response.Redirect("DashBoardRinnovi2.aspx?open=" & codR)
    End Sub

End Class