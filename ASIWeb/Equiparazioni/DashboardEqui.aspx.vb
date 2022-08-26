
Imports fmDotNet
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed
Public Class DashboardEqui
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If
        If IsNothing(Session("codice")) Then
            Response.Redirect("../login.aspx")
        End If
        Dim webserver As String = ConfigurationManager.AppSettings("webserver")
        Dim utente As String = ConfigurationManager.AppSettings("utente")
        Dim porta As String = ConfigurationManager.AppSettings("porta")
        Dim pass As String = ConfigurationManager.AppSettings("pass")
        Dim dbb As String = ConfigurationManager.AppSettings("dbb")
        Dim cultureFormat As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("it-IT")
        Dim deEnco As New Ed()

        Dim ris As String = Request.QueryString("ris")

        If Not String.IsNullOrEmpty(ris) Then
            If Session("visto") = "ok" Then

                ris = deEnco.QueryStringDecode(ris)

                If ris = "ok" Then
                    'If Session("procedi") = "OK" Then
                    '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Richiesta Corso caricata nel sistema! ' ).set('resizable', true).resizeTo('20%', 200);", True)
                    '    Session("procedi") = Nothing
                    'End If
                ElseIf ris = "ko" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Non risulta un tesseramento attivo per questo codice fiscale! ' ).set('resizable', true).resizeTo('20%', 200);", True)
                    Session("procedi") = Nothing
                End If
                Session("procedi") = Nothing
            End If
        End If


    End Sub

End Class