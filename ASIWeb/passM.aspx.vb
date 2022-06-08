Imports ASIWeb.Ed
Imports fmDotNet
Public Class passM
    Inherits System.Web.UI.Page
    Dim userID As String = ""
    Dim val As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("tobeChanged") <> "1" Then
            Response.Redirect("login.aspx")
        End If
        nascosto.Value = Session("password")
        Dim deEnco As New Ed()
        If Session("cambiata") = "s" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Password cambiata! ' ).set('resizable', true).resizeTo('20%', 200);", True)

        ElseIf Session("cambiata") = "n" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Password non cambiata ' ).set('resizable', true).resizeTo('20%', 200);", True)

        End If

        If Not Page.IsPostBack Then



            val = deEnco.QueryStringDecode(Request.QueryString("val"))
            userID = deEnco.QueryStringDecode(Request.QueryString("user"))
            Session("utenteC") = userID
            If String.IsNullOrEmpty(val) Or val <> "ok" Then
                Response.Redirect("login.aspx")

            End If
        Else

        End If
    End Sub

    Protected Sub btnCambia_Click(sender As Object, e As EventArgs) Handles btnCambia.Click

        If Page.IsValid Then
            Dim deEnco1 As New Ed()
            If Trim(txtNewPassoword.Text) = Trim(txtNewPasswordControllo.Text) Then


                ' salvo la nuova password

                Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
                '  Dim ds As DataSet
                Dim risposta As String = ""
                fmsP.SetLayout("web_enti")
                Dim Request = fmsP.CreateEditRequest(Session("idRecordLogin"))

                Request.AddField("Web_Password_Ente", txtNewPassoword.Text)
                Request.AddField("Web_Password_Changed", "1")
                Request.AddField("Web_HasToChanged", "0")
                Dim giorno = Now.Day
                Dim mese = Now.Month
                'If mese < 10 Then
                '    mese = "0" & mese
                'End If
                Dim anno = Now.Year
                Dim ora = Now.Hour
                Dim minuto = Now.Minute
                Dim secondo = Now.Second
                ' MM/dd/yyyy
                '  HH:mm:ss 
                Request.AddField("Web_DataCambioPassword", mese & "/" & giorno & "/" & anno & " " & ora & ":" & minuto & ":" & secondo)

                Try


                    risposta = Request.Execute()
                    Session("tobeChanged") = "0"
                    'btnCambia.Enabled = False
                    'pnlPAssCambiata.Visible = True
                    Response.Redirect("login.aspx?p=" & deEnco1.QueryStringEncode("ok"))
                Catch ex As Exception

                    ex.InnerException.Message.ToString()
                    'pnlPAssCambiata.Visible = False
                    'btnCambia.Enabled = True
                    Session("tobeChanged") = "0"
                    Response.Redirect("login.aspx?p=" & deEnco1.QueryStringEncode("ko"))
                End Try
            Else
                Session("tobeChanged") = "0"
                Response.Redirect("login.aspx?p=" & deEnco1.QueryStringEncode("ko"))
                'btnCambia.Enabled = True
                'pnlPAssCambiata.Visible = False


            End If




        End If

    End Sub




    Protected Sub checkSeDiversa_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles checkSeDiversa.ServerValidate





        If AsiModel.LogIn.LoginDiversaPassword(Session("utenteC"), txtNewPassoword.Text) = True Then
            args.IsValid = True

        Else

            args.IsValid = False
            '    checkSeDiversa.ErrorMessage = "questa password è uguale a quelle vecchia"



        End If
    End Sub

    Protected Sub btnVaiALogin_Click(sender As Object, e As EventArgs) Handles btnVaiALogin.Click

        Session.Abandon()
        Session.Clear()
        Response.Redirect("login.aspx")
    End Sub
End Class