Imports fmDotNet
Imports System.Web.Services
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed

Public Class annullaRichiesta
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If
        Dim deEnco As New Ed

        Dim record_ID As String = deEnco.QueryStringDecode(Request.QueryString("record_ID"))

        If Not String.IsNullOrEmpty(record_ID) Then
            Dim risposta As Integer = 0
            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()

            fmsP.SetLayout("webEquiparazioniMaster")

            Dim RequestP = fmsP.CreateDeleteRequest(record_ID)

            Try
                risposta = RequestP.Execute()


            Catch ex As Exception
            End Try

        End If



        Response.Redirect("dashboardEqui2.aspx")
    End Sub

End Class