﻿Imports fmDotNet
Imports System.Web.Services
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed
Public Class annullaEquiparazione2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If

        Dim deEnco As New Ed

        Dim codiceEquiparazione As String = deEnco.QueryStringDecode(Request.QueryString("codR"))
        Dim record_ID As String = deEnco.QueryStringDecode(Request.QueryString("record_ID"))


        If Not String.IsNullOrEmpty(record_ID) Then


            Dim risposta As Integer = 0

            Try
                Dim fmsP As FMSAxml = AsiModel.Conn.Connect()

                fmsP.SetLayout("webEquiparazioniRichiestaMolti")

                Dim RequestP = fmsP.CreateDeleteRequest(record_ID)



                risposta = RequestP.Execute()
            Catch ex As Exception
                AsiModel.LogIn.LogErrori(ex, "annullaEquiparazione2", "equiparazioni")
                Response.Redirect("../FriendlyMessage.aspx", False)
            End Try

        End If






        Session("AnnullaREqui") = "annullataEqui"

        Response.Redirect("dashboardEqui2.aspx?open=" & codiceEquiparazione & "&toa=canc")
    End Sub

End Class