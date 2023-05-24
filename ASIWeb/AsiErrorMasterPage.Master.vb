Imports fmDotNet
Imports ASIWeb.Ed
Public Class AsiErrorMasterPage
    Inherits System.Web.UI.MasterPage
    Dim deEnco As New Ed
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("login.aspx")
        End If
        If IsNothing(Session("codice")) Then
            Response.Redirect("login.aspx")
        End If

        If Not Page.IsPostBack Then

            If Not IsNothing(Session("denominazione")) Then


                '  Dim lblMasterDen As Literal = DirectCast(Master.FindControl("litDenominazione"), Literal)
                ' litDenominazione.Text = "Codice: " & AsiModel.LogIn.Codice & " - " & "Tipo Ente: " & AsiModel.LogIn.TipoEnte & " - " & AsiModel.LogIn.Denominazione
                litDenominazione.Text = "<i Class=""bi bi-intersect""> </i>" & Session("denominazione")

            End If

        End If

    End Sub

    Protected Sub lnkOut_Click(sender As Object, e As EventArgs) Handles lnkOut.Click
        Session("auth") = "0"
        Session("auth") = Nothing
        Session.Clear()
        Session.Abandon()

        Response.Redirect("login.aspx")

    End Sub
    Protected Sub lnkHome_Click(sender As Object, e As EventArgs) Handles lnkHome.Click

        Response.Redirect("home.aspx")
    End Sub









End Class