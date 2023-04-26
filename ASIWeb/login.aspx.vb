Imports ASIWeb.Ed
Public Class login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ltlAvviso.Visible = False
            Dim errore As String = ""
            If Not String.IsNullOrEmpty(Request.QueryString("err")) Then

                errore = "server in manutenzione, provare tra qualche minuto!"
                ltlerrore.Visible = True
            Else
                ltlerrore.Visible = False
            End If
            Dim edEnco1 As New Ed()
            If Not String.IsNullOrEmpty(Request.QueryString("p")) Then
                Dim cambio As String = edEnco1.QueryStringDecode(Request.QueryString("p"))

                If cambio = "ok" Then

                    pnlPAssCambiata.Visible = True
                ElseIf cambio = "ko" Then

                    pnlPAssCambiata1.Visible = True

                End If
            End If
        End If
    End Sub

    Protected Sub btnLogIn_Click(sender As Object, e As EventArgs) Handles btnLogIn.Click
        If Page.IsValid Then
            Dim userID = Trim(txtUserID.Text)
            Dim pass = Trim(txtPassword.Text)
            Session("password") = pass
            Dim deEnco As New Ed()

            If AsiModel.LogIn.Login(userID, pass) = True Then
                ltlAvviso.Visible = False
                Session("auth") = "1"
                Session("WebUserEnte") = AsiModel.LogIn.WebUserEnte
                Session("denominazione") = AsiModel.LogIn.Denominazione()
                Session("tipoEnte") = AsiModel.LogIn.TipoEnte()
                Session("codice") = AsiModel.LogIn.Codice()
                Session("HasToBeChanged") = AsiModel.LogIn.HasToBeChanged()
                Session("EquiparazioneSaltaDiploma") = AsiModel.LogIn.EquiparazioneSaltaDiploma()
                Session("idRecordLogin") = AsiModel.LogIn.IdRecord()
                Session("EquiparazioneModificaDataEmissione") = AsiModel.LogIn.EquiparazioneModificaDataEmissione()
                Session("CorsoModificaDataEmissione") = AsiModel.LogIn.CorsoModificaDataEmissione()

                AsiModel.AnnullaRichiesteSenzaRighe(Session("codice"))

                If Session("HasToBeChanged") = "1" Then
                    Session("tobeChanged") = "1"
                    Response.Redirect("passM.aspx?val=" & deEnco.QueryStringEncode("ok") & "&user=" & deEnco.QueryStringEncode(userID))

                End If



                Response.Redirect("Home.aspx")
            Else
                ltlAvviso.Visible = True
                ' lblAvviso.Text = "accesso non consentito"


            End If







        End If
    End Sub


End Class