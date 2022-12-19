Public Class DashBoardRinnovi1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        rinnovi()
    End Sub
    Sub rinnovi()
        phDash.Visible = True
        phDash.Controls.Add(New LiteralControl("<div class=""accordion"" id=""accordionDash"">"))
        Dim heading As String = "heading"
        Dim collapse As String = "collapse"

        For i As Integer = 1 To 5
            phDash.Controls.Add(New LiteralControl("<div class=""accordion-item"">"))

            phDash.Controls.Add(New LiteralControl("<h2 class=""accordion-header"" id=""" & heading & "_" & i.ToString & """>"))

            phDash.Controls.Add(New LiteralControl("<button class=""accordion-button collapsed"" type=""button"" data-bs-toggle=""collapse"" data-bs-target=""#collapse" & i.ToString & """ aria-expanded=""False"" aria-controls=""collapse" & i.ToString & """>"))
            phDash.Controls.Add(New LiteralControl("titolo"))
            phDash.Controls.Add(New LiteralControl("</button>"))
            phDash.Controls.Add(New LiteralControl("</h2>"))

            phDash.Controls.Add(New LiteralControl("<div id=""" & collapse & i.ToString & """ class=""accordion-collapse collapse"" aria-labelledby=""heading_" & i.ToString & """ data-bs-parent=""#accordionDash"">"))

            phDash.Controls.Add(New LiteralControl("<div class=""accordion-body"">"))
            ' corpo del contenuto del panel
            phDash.Controls.Add(New LiteralControl("ciao ciao"))

            ' corpo del contenuto del panel

            phDash.Controls.Add(New LiteralControl("</div>"))



            phDash.Controls.Add(New LiteralControl("</div>"))


            phDash.Controls.Add(New LiteralControl("</div>"))
        Next



        phDash.Controls.Add(New LiteralControl("</div>"))

    End Sub
End Class