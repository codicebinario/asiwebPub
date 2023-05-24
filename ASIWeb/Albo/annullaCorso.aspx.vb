Imports fmDotNet
Imports System.Web.Services
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed
Public Class annullaCorso
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If
        Dim deEnco As New Ed

        Dim codiceCorso As String = deEnco.QueryStringDecode(Request.QueryString("codR"))
        Dim record_ID As String = deEnco.QueryStringDecode(Request.QueryString("record_ID"))
        '  Dim IDRecord As String = deEnco.QueryStringDecode(Request.QueryString("id"))
        Dim risposta As Integer = 0
        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()

        fmsP.SetLayout("webCorsiRichiesta")

        Dim RequestP = fmsP.CreateEditRequest(record_ID)
        RequestP.AddField("Codice_Status", "99")
        RequestP.AddScript("RunWebAnnullaCorsoDT", codiceCorso)
        Try
            risposta = RequestP.Execute()
            AsiModel.LogIn.LogCambioStatus(codiceCorso, "99", Session("WebUserEnte"), "corso")
            '   AsiModel.LogIn.LogCambioStatus(CodiceRichiesta, "10", Session("WebUserEnte"))
            Session("annullaCorso") = "ok"

        Catch ex As Exception

        End Try



        Session("AnnullaREqui") = "annullatoCorso"

        Response.Redirect("dashboardB.aspx")
    End Sub

End Class