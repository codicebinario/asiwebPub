﻿Imports fmDotNet
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
        Try
            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            fmsP.SetLayout("webRinnoviMaster")
            Dim RequestP = fmsP.CreateEditRequest(record_ID)
            RequestP.AddField("CheckWeb", "s")
            RequestP.AddField("CodiceStatus", "155")
            risposta = RequestP.Execute()

            AsiModel.LogIn.LogCambioStatus(codR, "155", Session("WebUserEnte"), "Rinnovo")
            AsiModel.Rinnovi.AggiornaStatusMoltia155(codR)
            Session("AnnullaREqui") = "closeRin"
            Response.Redirect("DashBoardRinnovi2.aspx?open=" & codR, False)

        Catch ex As Exception
            AsiModel.LogIn.LogErrori(ex, "closeRinnovo", "rinnovi")
            Response.Redirect("../FriendlyMessage.aspx", False)
        End Try

    End Sub

End Class