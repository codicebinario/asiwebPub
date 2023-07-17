Public Class WebForm5
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Page.IsValid Then
            Response.Write(txtNote.Text & "<br />")
            Response.Write(Len(txtNote.Text))
        End If
    End Sub
End Class