Public Class upc
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("login.aspx")
        End If
        hcodR.Text = Request.QueryString("codR")
        litRichiesta.Text = "Carimento documenti per la Richiesta numero: " & hcodR.Text

        If Not Page.IsPostBack Then

            If Not IsNothing(Session("denominazione")) Then


                '  Dim lblMasterDen As Literal = DirectCast(Master.FindControl("litDenominazione"), Literal)
                litDenominazione.Text = "Codice: " & AsiModel.LogIn.Codice & " - " & "Tipo Ente: " & AsiModel.LogIn.TipoEnte & " - " & AsiModel.LogIn.Denominazione
            End If

        End If
    End Sub

    Protected Sub lnkOut_Click(sender As Object, e As EventArgs) Handles lnkOut.Click
        Session("auth") = "0"
        Session("auth") = Nothing
        Response.Redirect("login.aspx")
    End Sub

    Protected Sub lnkUpLegacy_Click(sender As Object, e As EventArgs) Handles lnkUpLegacy.Click
        Response.Redirect("upLeg.aspx?codR=" & hcodR.Text)
    End Sub
End Class