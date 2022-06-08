Imports System.IO
Imports System.Net
Imports System.Text
Imports fmDotNet
Imports System.Net.Mail
Imports System.Web.UI.WebControls

Imports System.Security.Cryptography
Imports System.Net.Http
Imports System.Threading.Tasks
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports RestSharp
Imports RestSharp.Deserializers
Imports ASIWeb.AsiModel


Public Class sendFirstEmail
    Inherits System.Web.UI.Page
    Dim soggetto As String
    Dim corpo As String
    Dim ritornoEmail As Boolean = False
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Public Function PrendiData(ByVal obj As Object) As String
        If IsDBNull(obj) Then Return "" Else Return obj
    End Function
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        Dim ds As DataSet
        Dim toEmail As String = "@"
        Dim user As String = ""
        Dim pass As String = ""
        Dim idRecord As String = ""
        Dim denominazione As String = ""

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("web_enti")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        ' RequestP.AddSearchField("pre_stato_web", "1")
        RequestP.AddSearchField("Web_Password_Speciale", "1", Enumerations.SearchOption.equals)
        ' RequestP.SetMax(20)

        Try



            ds = RequestP.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                For Each dr In ds.Tables("main").Rows

                    toEmail = PrendiData(dr("E_mailMailling"))
                    user = PrendiData(dr("Web_User_Ente"))
                    pass = PrendiData(dr("Web_Password_Ente"))
                    idRecord = dr("Record_ID")
                    denominazione = PrendiData(dr("Denominazione"))

                    soggetto = "ASI Nazionale - credenziali accesso nuova procedura richieste tessere via web"


                    corpo = AsiModel.IntestazioniEmail.TestataEmail



                    corpo &= "<body style=""background-color: #f6f6f6; font-family: sans-serif; -webkit-font-smoothing: antialiased; font-size: 14px; line-height: 1.4; margin: 0; padding: 0; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;"">
    <span class=""preheader"" style=""color: transparent; display: none; height: 0; max-height: 0; max-width: 0; opacity: 0; overflow: hidden; mso-hide: all; visibility: hidden; width: 0;"">"
                    corpo &= "Questa email fornisce le credenziali per i primi accessi alla nuova procedura di richiesta tessere online."
                    corpo &= "</span>"
                    corpo &= "<table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""body"" style=""border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #f6f6f6; width: 100%;"" width=""100%"" bgcolor=""#f6f6f6"">
      <tr>
        <td style=""font-family: sans-serif; font-size: 14px; vertical-align: top;"" valign=""top"">&nbsp;</td>
        <td class=""container"" style=""font-family: sans-serif; font-size: 14px; vertical-align: top; display: block; max-width: 580px; padding: 10px; width: 580px; margin: 0 auto;"" width=""580"" valign=""top"">
          <div class=""content"" style=""box-sizing: border-box; display: block; margin: 0 auto; max-width: 580px; padding: 10px;"">"
                    corpo &= " <table role=""presentation"" class=""main"" style=""border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; background: #ffffff; border-radius: 3px; width: 100%;"" width=""100%"">"

                    corpo &= "<tr><td class=""wrapper"" style=""font-family: sans-serif; font-size: 14px; vertical-align: top; box-sizing: border-box; padding: 20px;"" valign=""top"">
                  <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" style=""border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%;"" width=""100%"">
                    <tr><td style=""font-family: sans-serif; font-size: 14px; vertical-align: top;"" valign=""top"">
                        <p style=""font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; margin-bottom: 15px;"">"
                    corpo &= "Spett.le <strong>" & denominazione & "</strong>,"
                    corpo &= "</p> <p style=""font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; margin-bottom: 15px;"">"

                    corpo &= "Facendo seguito alla comunicazione inviata da ASI Nazionale in merito alla nuova procedura di richiesta tessere online,"
                    corpo &= "</p><table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""btn btn-primary"" style=""border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; box-sizing: border-box; width: 100%;"" width=""100%"">"

                    corpo &= "<tbody>
                            <tr>
                              <td align=""left"" style=""font-family: sans-serif; font-size: 14px; vertical-align: top; padding-bottom: 15px;"" valign=""top"">
                                <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" style=""border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: auto;"">
                                  <tbody>
                                    <tr>
                                      <td style=""font-family: sans-serif; font-size: 14px; vertical-align: top; border-radius: 5px; text-align: center; background-color: #3498db;"" valign=""top"" align=""center"" bgcolor=""#3498db"">
                               </td>
                                    </tr>
                                  </tbody>
                                </table>
                              </td>
                            </tr>
                          </tbody>
                        </table>
                        <p style=""font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; margin-bottom: 15px;"">"
                    '***** frase contesto e pass
                    corpo &= "Le comunichiamo le nuove credenziali di accesso:"
                    corpo &= "<p>"
                    corpo &= "UserID: <strong>" & user & "</strong><br />"
                    corpo &= "Password: <strong>" & pass & "</strong>"
                    corpo &= "</p>"

                    corpo &= "</p><p style=""font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; margin-bottom: 15px;"">"

                    '*****  saludos
                    corpo &= "Distinti saluti" & "<br /><br /><br />"
                    corpo &= "ASI - Associazioni Sportive e Sociali Italiane"

                    corpo &= "</p></td>
                    </tr>
                  </table>
                </td>
              </tr>"

                    corpo &= "</table>"









                    corpo &= AsiModel.IntestazioniEmail.FooterEmail
                    If Not String.IsNullOrWhiteSpace(toEmail) Then
                        Dim emails As String = toEmail
                        Dim email() As String
                        email = emails.Split(",")
                        For Each s As String In email


                            ritornoEmail = AsiModel.LaNostraEmail.SendMail(s, "noreply@mammutmedia.com", soggetto, corpo, True)
                            Threading.Thread.Sleep(2000) ' wait 2 seconds
                        Next

                        If ritornoEmail = True Then

                            Dim fmsS As FMSAxml = AsiModel.Conn.Connect()
                            fmsS.SetLayout("web_enti")
                            Dim Request = fmsS.CreateEditRequest(idRecord)


                            Request.AddField("Web_Email_StartUp", "1")

                            Try
                                Request.Execute()
                            Catch ex As Exception

                            End Try


                        End If
                    End If

                Next

            End If
        Catch ex As Exception

        End Try

    End Sub
End Class