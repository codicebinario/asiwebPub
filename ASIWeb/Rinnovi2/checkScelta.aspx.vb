Public Class checkScelta
    Inherits System.Web.UI.Page
    Dim codR As Integer
    Dim type As String
    Dim t As Integer = 0
    Dim cultureFormat As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("it-IT")
    Dim deEnco As New Ed()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If
        If IsNothing(Session("codice")) Then
            Response.Redirect("../login.aspx")
        End If
        cultureFormat.NumberFormat.CurrencySymbol = "€"
        cultureFormat.NumberFormat.CurrencyDecimalDigits = 2
        cultureFormat.NumberFormat.CurrencyGroupSeparator = String.Empty
        cultureFormat.NumberFormat.CurrencyDecimalSeparator = ","
        System.Threading.Thread.CurrentThread.CurrentCulture = cultureFormat
        System.Threading.Thread.CurrentThread.CurrentUICulture = cultureFormat

        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If

        codR = deEnco.QueryStringDecode(Request.QueryString("codR"))
        t = Request.QueryString("t")

    End Sub
    Protected Sub lnkCheck_Click(sender As Object, e As EventArgs)
        If Page.IsValid Then

            If rbIT.Checked Then

                Response.Redirect("checkTesseramentoRinnovi2.aspx?codR=" & deEnco.QueryStringEncode(codR))
            ElseIf rbEE.Checked Then
                Response.Redirect("checkTesseramentoRinnovi3.aspx?codR=" & deEnco.QueryStringEncode(codR))

            End If

        End If
    End Sub
End Class