Imports fmDotNet
Imports ASIWeb.Ed

Public Class CancellaRiga
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Dim deEnco As New Ed

        Dim codiceRinnovoM As String = deEnco.QueryStringDecode(Request.QueryString("codR"))
        Dim record_ID As String = deEnco.QueryStringDecode(Request.QueryString("record_ID"))
        '  Dim IDRecord As String = deEnco.QueryStringDecode(Request.QueryString("id"))

        If Not String.IsNullOrEmpty(record_ID) Then


            Dim risposta As Integer = 0
            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()

            fmsP.SetLayout("webRinnoviRichiesta2")

            Dim RequestP = fmsP.CreateDeleteRequest(record_ID)
            '   RequestP.AddField("Codice_Status", "99")

            Try
                risposta = RequestP.Execute()
                '   AsiModel.LogIn.LogCambioStatus(codiceCorso, "99", Session("WebUserEnte"), "corso")
                '   AsiModel.LogIn.LogCambioStatus(CodiceRichiesta, "10", Session("WebUserEnte"))
                '   Session("annullaCorso") = "ok"

            Catch ex As Exception
            End Try
            'Session("visto") = "ok"
            Session("AnnullaREqui") = "annullataRin"
            ' Response.Redirect("DashboardRinnovi2.aspx?open=" & codiceRinnovoM & "&ris=" & deEnco.QueryStringEncode("casi"))
            Response.Redirect("DashboardRinnovi2.aspx?open=" & codiceRinnovoM)

        Else
            'Session("visto") = "ok"
            Session("AnnullaREqui") = "annullataRinKO"
            Response.Redirect("dashboardRinnovi2.aspx?open=" & codiceRinnovoM)

            '   Response.Redirect("dashboardRinnovi2.aspx?open=" & codiceRinnovoM & "&ris=" & deEnco.QueryStringEncode("cano"))

        End If



    End Sub

End Class