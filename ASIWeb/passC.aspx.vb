Imports ASIWeb.Ed
Public Class passC
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim deEnco As New Ed()
        Dim val As String = ""

        val = deEnco.QueryStringDecode(Request.QueryString("val"))

        If String.IsNullOrEmpty(val) Or val <> "ok" Then
            Response.Redirect("login.aspx")

        End If



    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Page.IsValid Then



        End If
    End Sub
End Class