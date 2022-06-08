Imports System.Net
Imports System.Net.Mail
Imports System.Web.SessionState
Public Class Global_asax
    Inherits HttpApplication
    Dim mailpost As String = ConfigurationManager.AppSettings("mailpost")

    Sub Application_Start(sender As Object, e As EventArgs)
        ' Generato all'avvio dell'applicazione
        Application("ActiveUsers") = 0
    End Sub

    Sub Session_Start(sender As Object, e As EventArgs)
        Session("Time") = Now
        Application.Lock()
        Application("ActiveUsers") = CInt(Application("ActiveUsers")) + 1
        Application.UnLock()


    End Sub


    Sub Session_End(sender As Object, e As EventArgs)
        Application.Lock()
        Application("ActiveUsers") = CInt(Application("ActiveUsers")) - 1
        Application.UnLock()

    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs

        Dim httpUnhandledException As HttpUnhandledException = New HttpUnhandledException(Server.GetLastError().Message, Server.GetLastError())
        SendEmailWithErrors(httpUnhandledException.GetHtmlErrorMessage(), mailpost)


        Dim ex As Exception = Server.GetLastError()

        If TypeOf ex Is HttpRequestValidationException Then
            Response.Redirect("FriendlyMessage.aspx", True)
        End If

    End Sub

    Private Shared Sub SendEmailWithErrors(ByVal result As String, ByVal mailpost As String)
        Try
            Dim message As New MailMessage
            '  Dim Message As MailMessage = New MailMessage()


            message.From = New MailAddress("e.burani@mammutmedia.com")

            message.To.Add(New MailAddress("enrico.burani@gmail.com"))
            message.To.Add(New MailAddress("m.ditoro@mammutmedia.com"))




            message.Subject = "errore in ASI Web APP"
            message.Body = result
            message.Priority = MailPriority.High
            message.IsBodyHtml = True
            '  message.IsBodyHtml = False
            Dim client As SmtpClient = New SmtpClient
            client.Host = mailpost
            client.Port = 587
            client.UseDefaultCredentials = False
            client.Credentials = New NetworkCredential("e.burani@mammutmedia.com", "ebqa34aqAm!")
            '  client.Host = "mail.tin.it"


            client.Send(message)


        Catch ehttp As System.Web.HttpException


        End Try
    End Sub
End Class