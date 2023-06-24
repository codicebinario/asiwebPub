Public Class sceltaCheck
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

        type = Request.QueryString("type")


        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If
        Dim record_ID As String = ""

        t = Request.QueryString("t")

        codR = deEnco.QueryStringDecode(Request.QueryString("codR"))




        If Page.IsPostBack Then

            '  pnlFase1.Visible = False


        End If
    End Sub

    Protected Sub lnkCheck_Click(sender As Object, e As EventArgs)
        If Page.IsValid Then

            If rbIT.Checked Then

                Response.Redirect("checkTesseramento2.aspx?codR=" & deEnco.QueryStringEncode(codR))
            ElseIf rbEE.Checked Then
                Response.Redirect("checkTesseramento3.aspx?codR=" & deEnco.QueryStringEncode(codR))

            End If

        End If
    End Sub
End Class