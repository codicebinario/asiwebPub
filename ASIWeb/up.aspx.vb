Public Class up1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("login.aspx")
        End If
        hcodR.text = Request.QueryString("codR")

        If Not Page.IsPostBack Then

            If Not IsNothing(Session("denominazione")) Then


                '  Dim lblMasterDen As Literal = DirectCast(Master.FindControl("litDenominazione"), Literal)
                litDenominazione.Text = "Codice: " & Session("codice") & " - " & "Tipo Ente: " & AsiModel.LogIn.TipoEnte & " - " & AsiModel.LogIn.Denominazione
            End If

        End If
    End Sub

    Protected Sub lnkOut_Click(sender As Object, e As EventArgs) Handles lnkOut.Click
        Session("auth") = "0"
        Session("auth") = Nothing
        Response.Redirect("login.aspx")
    End Sub

End Class