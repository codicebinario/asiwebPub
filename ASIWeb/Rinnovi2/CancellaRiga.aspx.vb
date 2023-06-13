Imports fmDotNet
Imports ASIWeb.Ed

Public Class CancellaRiga
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Dim deEnco As New Ed

        Dim codiceRinnovoM As String = deEnco.QueryStringDecode(Request.QueryString("codR"))
        Dim record_ID As String = deEnco.QueryStringDecode(Request.QueryString("record_ID"))

        If Not String.IsNullOrEmpty(record_ID) Then
            Try

                Dim risposta As Integer = 0
                Dim fmsP As FMSAxml = AsiModel.Conn.Connect()

                fmsP.SetLayout("webRinnoviRichiesta2")

                Dim RequestP = fmsP.CreateDeleteRequest(record_ID)


                risposta = RequestP.Execute()

                Session("AnnullaREqui") = "annullataRin"
                Response.Redirect("DashboardRinnovi2.aspx?open=" & codiceRinnovoM, False)
            Catch ex As Exception
                AsiModel.LogIn.LogErrori(ex, "CancellaRiga", "rinnovi")
                Response.Redirect("../FriendlyMessage.aspx", False)
            End Try

        Else

            Session("AnnullaREqui") = "annullataRinKO"
            Response.Redirect("dashboardRinnovi2.aspx?open=" & codiceRinnovoM, False)

        End If



    End Sub

End Class