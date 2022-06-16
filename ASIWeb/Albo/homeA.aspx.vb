Public Class homeA
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("..login.aspx")
        End If
        If IsNothing(Session("codice")) Then
            Response.Redirect("..login.aspx")
        End If
    End Sub

End Class