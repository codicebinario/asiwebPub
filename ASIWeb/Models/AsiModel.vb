Imports fmDotNet
Imports ASIWeb.Data
Imports RestSharp
Imports System.IO
Imports System.Net
Imports System.Text

Imports System.Net.Mail
Imports System.Web.UI.WebControls

Imports System.Security.Cryptography

Imports System.Threading.Tasks
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Web.SessionState
Imports System.Web.Script.Serialization


Imports System.Collections.Generic
Imports Microsoft.VisualBasic.ApplicationServices
Imports System.Net.Http
'Imports FMdotNet__DataAPI

Imports System.Linq
Imports System.Configuration
Imports System.Drawing
Imports Image = System.Drawing.Image
Imports System
Imports System.Web
Imports System.Net.Security
Imports System.Globalization



Public Class AsiModel

    Public Class IntestazioniEmail

        Public Shared Function FooterEmail() As String
            Dim corpo As String
            corpo = "<div class=""footer"" style=""clear: both; margin-top: 10px; text-align: center; width: 100%;"">"
            corpo &= "<table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" style=""border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%;"" width=""100%"">"
            corpo &= "<tr>"
            corpo &= "<td class=""content-block"" style=""font-family: sans-serif; vertical-align: top; padding-bottom: 10px; padding-top: 10px; color: #999999; font-size: 12px; text-align: center;"" valign=""top"" align=""center"">
                    <span class=""apple-link"" style=""color: #999999; font-size: 12px; text-align: center;"">ASI - Associazioni Sportive e Sociali Italiane</span>
                    <br><a href=""https://www.asinazionale.it/"" style=""text-decoration: underline; color: #999999; font-size: 12px; text-align: center;"">ASI Nazionale</a>.
                  </td>"
            corpo &= "</tr>"

            corpo &= "<tr>"
            corpo &= "<td class=""content-block powered-by"" style=""font-family: sans-serif; vertical-align: top; padding-bottom: 10px; padding-top: 10px; color: #999999; font-size: 12px; text-align: center;"" valign=""top"" align=""center"">
                    Powered by <a href=""https://www.mammutmedia.com/"" style=""color: #999999; font-size: 12px; text-align: center; text-decoration: none;"">MAMMUTMEDIA SOCIETA' COOPERATIVA</a>.
                  </td>"
            corpo &= "</tr>"
            corpo &= "</table>"
            corpo &= "</div>"
            corpo &= "</div></td> <td style=""font-family: sans-serif; font-size: 14px; vertical-align: top;"" valign=""top"">&nbsp;</td></tr></table></body></html> "

            Return corpo

        End Function

        Public Shared Function TestataEmail() As String



            Dim corpo As String

            corpo = "<!doctype html>"

            corpo &= "<html><head>"

            corpo &= "<meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">"
            corpo &= "<meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"">"
            corpo &= "<title>ASI</title>"
            corpo &= "<style>"
            corpo &= "@media only screen and (max-width: 620px) {table.body h1 {font-size: 28px !important;margin-bottom: 10px !important;}"
            corpo &= "table.body p,table.body ul,table.body ol,table.body td,table.body span,table.body a {font-size: 16px !important;}"
            corpo &= "table.body .wrapper,table.body .article {padding: 10px !important;} table.body .content {padding: 0 !important;}table.body .container {padding: 0 !important;width: 100% !important;}"
            corpo &= " table.body .main {border-left-width: 0 !important;border-radius: 0 !important;border-right-width: 0 !important;}table.body .btn table {width: 100% !important;}table.body .btn a {width: 100% !important;}"
            corpo &= " table.body .img-responsive {height: auto !important;max-width: 100% !important;width: auto !important;}}@media all {.ExternalClass {width: 100%;}"
            corpo &= ".ExternalClass,.ExternalClass p,.ExternalClass span,.ExternalClass font,.ExternalClass td,.ExternalClass div {line-height: 100%;}"
            corpo &= ".apple-link a {color: inherit !important;font-family: inherit !important;font-size: inherit !important;font-weight: inherit !important;line-height: inherit !important;text-decoration: none !important;}"
            corpo &= "#MessageViewBody a {color: inherit;text-decoration: none;font-size: inherit;font-family: inherit;font-weight: inherit;line-height: inherit;}"
            corpo &= " .btn-primary table td:hover {background-color: #34495e !important;}"
            corpo &= " .btn-primary a:hover {background-color: #34495e !important;border-color: #34495e !important;}"
            corpo &= "</style>"
            corpo &= "</head>"



            Return corpo


        End Function


    End Class
    Public Class LaNostraEmail
        Dim mailpost As String = ConfigurationManager.AppSettings("mailpost")
        Public Shared Function SendMail(ByVal [To] As String,
    ByVal From As String, ByVal Subject As String,
    ByVal Body As String, ByVal IsHTML As Boolean, Optional ByVal cc2 As String = "@",
    Optional ByVal SmtpServer As String = "domain.com") As Boolean

            Dim esito As Boolean = False

            Dim message As New MailMessage
            message.From = New MailAddress("noreply@mammutmedia.com")

            message.To.Add(New MailAddress([To]))
            ' message.CC.Add(New MailAddress(cc))


            message.Bcc.Add(New MailAddress("e.burani@mammutmedia.com"))
            message.Subject = Subject
            message.Body = Body
            Dim av As AlternateView
            av = AlternateView.CreateAlternateViewFromString(Body, New System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Text.Html))
            av.TransferEncoding = System.Net.Mime.TransferEncoding.SevenBit
            message.AlternateViews.Add(av)
            message.Priority = MailPriority.High
            message.IsBodyHtml = True

            Dim client As SmtpClient = New SmtpClient
            client.Host = "smtps.aruba.it"
            client.Port = 587
            client.UseDefaultCredentials = False
            client.Credentials = New NetworkCredential("e.burani@mammutmedia.com", "ebqa34aqAm!")

            Try

                client.Send(message)
                esito = True
            Catch ex As Exception
                esito = False
            End Try


            Return esito
        End Function

    End Class

    Public Class Conn

        Private Shared ReadOnly Property Webserver() As String

            Get
                Return ConfigurationManager.AppSettings("webserver")
            End Get

        End Property

        Private Shared ReadOnly Property Porta() As String

            Get
                Return ConfigurationManager.AppSettings("porta")
            End Get

        End Property

        Private Shared ReadOnly Property Utente() As String

            Get
                Return ConfigurationManager.AppSettings("utente")
            End Get

        End Property

        Private Shared ReadOnly Property Password() As String

            Get
                Return ConfigurationManager.AppSettings("pass")
            End Get

        End Property
        Private Shared ReadOnly Property Database() As String

            Get
                Return ConfigurationManager.AppSettings("dbb")
            End Get

        End Property





        Public Shared Function Connect()

            Dim fmsb As FMSAxml = Nothing
            '    Try
            fmsb = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)

                fmsb.SetDatabase(Database)

            '  Catch ex As Exception
            'Dim errore As String = ex.InnerException.ToString
            '    HttpContext.Current.Server.Transfer("login.aspx?err=dbc")
            '    End Try
            Return fmsb
        End Function






    End Class





    Public Class LogIn

        Public Shared Property Denominazione As String
        Public Shared Property Codice As String
        Public Shared Property TipoEnte As String
        Public Shared Property WebUserEnte As String
        Public Shared Property EquiparazioneSaltaDiploma As String
        Public Shared Property EquiparazioneModificaDataEmissione As String
        Public Shared Property RinnovoModificaDataEmissione As String
        Public Shared Property CorsoModificaDataEmissione As String
        Public Shared Property IdRecord As String
        Public Shared Property HasToBeChanged As String

        Public Shared Function LoginDiversaPassword(user As String, pass As String) As Boolean
            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing
            Dim accesso As Boolean = False
            Dim IpAddress As String = ""

            fms = Conn.Connect()

            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fms.SetLayout("web_enti")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("Web_User_Ente", user, Enumerations.SearchOption.equals)
            RequestA.AddSearchField("Web_Password_Ente", pass, Enumerations.SearchOption.equals)
            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows

                        If Data.FixNull(dr("Web_Password_Ente")) = pass Then
                            accesso = False
                        Else
                            accesso = True
                        End If


                    Next



                    '    IpAddress = GetIPA()

                    ' LogAccessi(Codice, user)
                Else
                    accesso = True
                End If



            Catch ex As Exception
                accesso = False
            End Try

            Return accesso

        End Function
        Public Shared Function LoginToBeChanged(user As String, pass As String) As Boolean
            Dim fms1 As FMSAxml = Nothing
            Dim ds1 As DataSet = Nothing
            Dim accesso As Boolean = False
            Dim IpAddress As String = ""

            fms1 = Conn.Connect()

            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)


            fms1.SetLayout("web_enti")
            Dim RequestA = fms1.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("Web_User_Ente", user, Enumerations.SearchOption.equals)
            RequestA.AddSearchField("Web_Password_Ente", pass, Enumerations.SearchOption.equals)
            Try
                ds1 = RequestA.Execute()

                If Not IsNothing(ds1) AndAlso ds1.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds1.Tables("main").Rows



                        If Data.FixNull(dr("Web_HasToChanged")) = "1" Then
                            IdRecord = Data.FixNull(dr("Record_ID"))
                            accesso = True
                        End If


                    Next



                    '    IpAddress = GetIPA()

                    ' LogAccessi(Codice, user)

                End If



            Catch ex As Exception
                accesso = False
            End Try

            Return accesso

        End Function



        Public Shared Function Login(user As String, pass As String) As Boolean
            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing
            Dim accesso As Boolean = False
            Dim IpAddress As String
            ' Try
            fms = Conn.Connect()
            '   Catch ex As Exception

            '  End Try


            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fms.SetLayout("web_enti")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("Web_User_Ente", user, Enumerations.SearchOption.equals)
            RequestA.AddSearchField("Web_Password_Ente", pass, Enumerations.SearchOption.equals)
            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows
                        WebUserEnte = Data.FixNull(dr("Web_User_Ente"))
                        Denominazione = Data.FixNull(dr("denominazione"))
                        TipoEnte = Data.FixNull(dr("tipo_ente"))
                        Codice = Data.FixNull(dr("codice"))
                        IdRecord = Data.FixNull(dr("Record_ID"))
                        EquiparazioneSaltaDiploma = Data.FixNull(dr("EquiparazioneSaltaDiploma"))
                        EquiparazioneModificaDataEmissione = Data.FixNull(dr("EquiparazioneModificaDataEmissione"))
                        RinnovoModificaDataEmissione = Data.FixNull(dr("RinnoviModificaDataEmissione"))

                        CorsoModificaDataEmissione = Data.FixNull(dr("CorsiModificaDataEmissione"))
                        HasToBeChanged = Data.FixNull(dr("Web_HasToChanged"))
                    Next

                    accesso = True

                    '    IpAddress = GetIPA()

                    LogAccessi(Codice, user)

                End If



            Catch ex As Exception
                accesso = False

            End Try

            Return accesso

        End Function
        Public Shared Function LogCambioStatus(CodiceRichiesta As String, Status_ID As String, User As String) As Boolean
            '  Dim litNumRichieste As Literal = DirectCast(ContentPlaceHolder1.FindControl("LitNumeroRichiesta"), Literal)
            Dim ritorno As Boolean = False
            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            ' Dim ds As DataSet


            fmsP.SetLayout("Log_Status_Richiesta")
            Dim Request = fmsP.CreateNewRecordRequest()
            Request.AddField("Codice_Richiesta", CodiceRichiesta)
            Request.AddField("User_Cambio_Status", User)

            Request.AddField("Status_ID", Status_ID)


            Try
                Request.Execute()
                ritorno = True
            Catch ex As Exception
                ritorno = False
            End Try

            Return ritorno

        End Function

        Public Shared Function LogCambioStatus(CodiceRichiesta As String, Status_ID As String, User As String, tipo As String) As Boolean
            '  Dim litNumRichieste As Literal = DirectCast(ContentPlaceHolder1.FindControl("LitNumeroRichiesta"), Literal)
            Dim ritorno As Boolean = False
            Try

                Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
                ' Dim ds As DataSet


                fmsP.SetLayout("Log_Status_Richiesta")
                Dim Request = fmsP.CreateNewRecordRequest()
                Request.AddField("Codice_Richiesta", CodiceRichiesta)
                Request.AddField("User_Cambio_Status", User)

                Request.AddField("Status_ID", Status_ID)

                Request.AddField("tipo", tipo)



                Request.Execute()
                ritorno = True
            Catch ex As Exception
                ritorno = False
            End Try

            Return ritorno

        End Function
        Public Shared Function LogCambioStatus(CodiceRichiesta As String, Status_ID As String, User As String, tipo As String, idRecord As Integer) As Boolean
            '  Dim litNumRichieste As Literal = DirectCast(ContentPlaceHolder1.FindControl("LitNumeroRichiesta"), Literal)
            Dim ritorno As Boolean = False
            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            ' Dim ds As DataSet


            fmsP.SetLayout("Log_Status_Richiesta")
            Dim Request = fmsP.CreateNewRecordRequest()
            Request.AddField("Codice_Richiesta", CodiceRichiesta)
            Request.AddField("User_Cambio_Status", User)

            Request.AddField("Status_ID", Status_ID)
            Request.AddField("tipo", tipo)
            Request.AddField("IdRecordEquiparazione", idRecord)


            Try
                Request.Execute()
                ritorno = True
            Catch ex As Exception
                ritorno = False
            End Try

            Return ritorno

        End Function
        Public Shared Function LogErrori(exx As Exception, pagina As String, appa As String) As Boolean
            Dim ritorno As Boolean = False
            Try
                Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
                ' Dim ds As DataSet


                fmsP.SetLayout("logErrors")
                Dim Request = fmsP.CreateNewRecordRequest()

                Request.AddField("ExceptionMessage", exx.Message)
                Request.AddField("Source", exx.Source)
                Request.AddField("tipo", exx.GetType.Name)
                Request.AddField("WebApp", appa)
                Request.AddField("pagina", pagina)
                If exx.InnerException IsNot Nothing Then
                    Request.AddField("InnerExceptionMessage", exx.InnerException.Message)
                End If

                Request.Execute()
                ritorno = True
            Catch ex As Exception
                ritorno = False
            End Try

            Return ritorno
        End Function

        Public Shared Function LogAccessi(CodiceEnte As String, User As String) As Boolean
            '  Dim litNumRichieste As Literal = DirectCast(ContentPlaceHolder1.FindControl("LitNumeroRichiesta"), Literal)
            Dim ritorno As Boolean = False
            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            ' Dim ds As DataSet


            fmsP.SetLayout("Log_Web")
            Dim Request = fmsP.CreateNewRecordRequest()

            Request.AddField("CodiceEnte", CodiceEnte)
            '    Request.AddField("Dove", Dove)
            Request.AddField("User", User)
            '    Request.AddScript("Pass", Pass)

            Try
                Request.Execute()
                ritorno = True
            Catch ex As Exception
                ritorno = False
            End Try

            Return ritorno

        End Function
        Public Shared Function GetIPA() As String
            Dim Host As IPHostEntry
            Dim Hostname As String
            Hostname = My.Computer.Name
            Host = Dns.GetHostEntry(Hostname)
            Dim ritorno As String = "0.0.0.0"
            For Each IP As IPAddress In Host.AddressList
                If IP.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                    ritorno = Convert.ToString(IP)
                End If
            Next
            Return ritorno
        End Function
        Public Shared Function GetIPAddress() As String
            Dim context As System.Web.HttpContext = System.Web.HttpContext.Current
            Dim sIPAddress As String = context.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
            If String.IsNullOrEmpty(sIPAddress) Then
                Return context.Request.ServerVariables("REMOTE_ADDR")
            Else
                Dim ipArray As String() = sIPAddress.Split(New [Char]() {","c})
                Return ipArray(0)
            End If
        End Function

    End Class

    Public Class GetTotali
        Public ImportoRighe As Decimal
        Public ImportoSconto As Decimal
        Public ImportoTessere As Decimal

    End Class

    Public Shared Function AnnullaOrdine(recordID As String) As String
        Dim cultureFormat As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("it-IT")


        Dim fms As FMSAxml = Nothing
        Dim ds As DataSet = Nothing
        Dim Risultato As String = ""

        fms = Conn.Connect()

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        '  Dim ds As DataSet
        Dim risposta As String = ""
        fmsP.SetLayout("web_richiesta_master")
        Dim Request = fmsP.CreateEditRequest(recordID)




        Request.AddField("Status_ID", "10")




        Try
            risposta = Request.Execute()

            Risultato = "ok"

        Catch ex As Exception
            Risultato = "no"
        End Try





        Return Risultato


    End Function
    Public Shared Function controllaCodiceFiscaleEE(Nome As String, Cognome As String, dataNascita As Date, numeroTessera As String) As Integer
        Dim fms As FMSAxml = Nothing
        Dim ds As DataSet = Nothing
        Dim ritorno As Integer = 0
        'Dim dataScadenza As DateTime
        Dim it As DateTime = DateTime.Now.Date.ToString("dd/MM/yyyy", New CultureInfo("it-IT"))

        Try

            fms = Conn.Connect()

            fms.SetLayout("webCheckCF")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("Nome", Nome, Enumerations.SearchOption.equals)
            RequestA.AddSearchField("Cognome", Cognome, Enumerations.SearchOption.equals)
            RequestA.AddSearchField("Codice tessera", numeroTessera, Enumerations.SearchOption.equals)
            RequestA.AddSearchField("Data nascita", Data.SistemaDataUK(Data.SonoDieci(dataNascita)), Enumerations.SearchOption.equals)
            RequestA.AddSearchField("Estero", "EE", Enumerations.SearchOption.equals)

            ds = RequestA.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                For Each dr In ds.Tables("main").Rows

                    If it <= dr("Data Scadenza") Then
                        'tessera valida e non scaduto
                        ritorno = 1
                    Else
                        'tessera valida ma scaduta
                        ritorno = 2
                    End If
                Next

            Else
                'tessera non trovata
                ritorno = 3
            End If

        Catch ex As Exception
            'errore generico di connessione
            ritorno = 4
        End Try
        Return ritorno
    End Function
    ''' <summary>
    ''' controlla il codice fiscale per Equiparazione
    ''' </summary>
    ''' <param name="codiceFiscale"></param>
    ''' <returns></returns>
    Public Shared Function controllaCodiceFiscale(codiceFiscale As String, data As String) As Integer
        Dim fms As FMSAxml = Nothing
        Dim ds As DataSet = Nothing
        Dim ritorno As Integer = 0
        'Dim dataScadenza As DateTime
        Dim it As DateTime = DateTime.Now.Date.ToString("dd/MM/yyyy", New CultureInfo("it-IT"))

        Try

            fms = Conn.Connect()

            fms.SetLayout("webCheckCF")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("CodiceFiscale", codiceFiscale, Enumerations.SearchOption.equals)

            ds = RequestA.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                For Each dr In ds.Tables("main").Rows

                    If it <= dr("Data Scadenza") Then
                        'tessera valida e non scaduto
                        ritorno = 1
                    Else
                        'tessera valida ma scaduta
                        ritorno = 2
                    End If
                Next

            Else
                'tessera non trovata
                ritorno = 3
            End If

        Catch ex As Exception
            'errore generico di connessione
            ritorno = 4
        End Try
        Return ritorno
    End Function
    Public Shared Function controllaCodiceFiscaleRinnovo(codiceFiscale As String, codr As String) As Boolean
        Dim fms As FMSAxml = Nothing
        Dim ds As DataSet = Nothing
        Dim ritorno As Boolean = False

        fms = Conn.Connect()

        fms.SetLayout("webRinnoviRichiesta2")
        Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
        RequestA.AddSearchField("Rin_CodiceFiscale", codiceFiscale, Enumerations.SearchOption.equals)
        RequestA.AddSearchField("IDRinnovoM", codr, Enumerations.SearchOption.equals)


        Try

            ds = RequestA.Execute()


            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                For Each dr In ds.Tables("main").Rows

                    ritorno = True


                Next



            End If




        Catch ex As Exception

            ritorno = False
        End Try
        Return ritorno
    End Function
    Public Shared Function controllaCodiceFiscaleEquipazione(codiceFiscale As String, codr As String) As Boolean
        Dim fms As FMSAxml = Nothing
        Dim ds As DataSet = Nothing
        Dim ritorno As Boolean = False

        fms = Conn.Connect()

        fms.SetLayout("webEquiparazioniRichiestaMolti")
        Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
        RequestA.AddSearchField("Equi_CodiceFiscale", codiceFiscale, Enumerations.SearchOption.equals)
        RequestA.AddSearchField("IDEquiparazioneM", codr, Enumerations.SearchOption.equals)


        Try

            ds = RequestA.Execute()


            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                For Each dr In ds.Tables("main").Rows

                    ritorno = True


                Next



            End If




        Catch ex As Exception

            ritorno = False
        End Try
        Return ritorno
    End Function



    Public Shared Function AnnullaRichiesteSenzaRighe(codiceEnte As String)
        Dim fms As FMSAxml = Nothing
        Dim ds As DataSet = Nothing
        Dim ritorno As Boolean = False

        fms = Conn.Connect()

        '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
        '     fmsB.SetDatabase(Database)
        fms.SetLayout("web_richiesta_master")
        Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
        RequestA.AddSearchField("Ente_Codice", codiceEnte, Enumerations.SearchOption.equals)

        Try

            ds = RequestA.Execute()


            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                For Each dr In ds.Tables("main").Rows

                    If dr("Status_ID") = "0" Then

                        CancellaRichiestaZero(dr("Record_ID"))
                        'delete richiesta


                    End If



                Next
            End If


            ritorno = True
        Catch ex As Exception
            ritorno = False
        End Try
        Return ritorno
    End Function

    Shared Function CancellaRichiestaZero(idRichiesta As String)
        Dim ritorno As Boolean = False

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        '  Dim ds As DataSet

        fmsP.SetLayout("web_richiesta_master")
        Dim Request = fmsP.CreateDeleteRequest(idRichiesta)
        Try
            Request.Execute()
            ritorno = True
        Catch ex As Exception
            ritorno = False
        End Try

        Return ritorno

    End Function

    Public Shared Function GetTotaliRichiesta(codR As String) As GetTotali
        Dim cultureFormat As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("it-IT")


        Dim fms As FMSAxml = Nothing
        Dim ds As DataSet = Nothing
        Dim deTotali As New GetTotali

        fms = Conn.Connect()

        '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
        '     fmsB.SetDatabase(Database)
        fms.SetLayout("web_richiesta_master")
        Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
        RequestA.AddSearchField("Codice_Richiesta", codR, Enumerations.SearchOption.equals)

        Try
            ds = RequestA.Execute()


            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                For Each dr In ds.Tables("main").Rows

                    deTotali.ImportoRighe = dr("Importo_Righe")
                    deTotali.ImportoSconto = dr("Importo_Sconto")
                    deTotali.ImportoTessere = dr("Importo_Tessere")
                Next



            End If



        Catch ex As Exception

        End Try

        Return deTotali


    End Function
    Public Class GetRecord_IDbyCodRCorsi

        'Public Shared Property Denominazione As String
        'Public Shared Property Codice As String
        'Public Shared Property TipoEnte As String

        Public Shared Property Record_Id As String


        Public Shared Function GetRecord_ID(codR As String) As String
            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing


            fms = Conn.Connect()

            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fms.SetLayout("webCorsiRichiesta")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("IdCorso", codR, Enumerations.SearchOption.equals)

            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows

                        Record_Id = Data.FixNull(dr("Id_record"))


                    Next



                End If



            Catch ex As Exception

            End Try

            Return Record_Id


        End Function






    End Class
    Public Class GetRecord_IDbyCodREquiparazione

        'Public Shared Property Denominazione As String
        'Public Shared Property Codice As String
        'Public Shared Property TipoEnte As String

        Public Shared Property Record_Id As String
        Public Shared Function GetRecord_IDRinnovi(codR As String) As String
            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing


            fms = Conn.Connect()

            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fms.SetLayout("webRinnoviRichiesta")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("IdRinnovo", codR, Enumerations.SearchOption.equals)

            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows

                        Record_Id = Data.FixNull(dr("Id_record"))


                    Next



                End If



            Catch ex As Exception

            End Try

            Return Record_Id


        End Function

        Public Shared Function GetRecord_IDRinnovi2(codR As String) As String
            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing
            Try

                fms = Conn.Connect()

                '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
                '     fmsB.SetDatabase(Database)
                fms.SetLayout("webRinnoviMaster")
                Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
                RequestA.AddSearchField("IdRinnovoM", codR, Enumerations.SearchOption.equals)


                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows

                        Record_Id = Data.FixNull(dr("idRecord"))


                    Next



                End If



            Catch ex As Exception

            End Try

            Return Record_Id


        End Function

        Public Shared Function AggiornaStatusMasterEquiparazine(idrecord As Integer, status As Integer) As Integer
            Dim risposta As Integer
            Dim fmsP1 As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
            '  Dim ds As DataSet

            fmsP1.SetLayout("webEquiparazioniMaster")
            Dim Request1 = fmsP1.CreateEditRequest(idrecord)

            'If qualeStatus = "3" Then
            Request1.AddField("CodiceStatus", status)
            'Else
            '    Request1.AddField("Status_ID", "12")
            'End If

            risposta = Request1.Execute()



        End Function



        Public Shared Function GetRecord_IDEquiMaster(codR As String) As String
            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing


            fms = Conn.Connect()

            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fms.SetLayout("webEquiparazioniMaster")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("IdEquiparazioneM", codR, Enumerations.SearchOption.equals)

            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows
                        Record_Id = Data.FixNull(dr("Idrecord"))
                    Next
                End If



            Catch ex As Exception

            End Try

            Return Record_Id


        End Function
        Public Shared Function GetRecord_ID(codR As String) As String
            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing


            fms = Conn.Connect()

            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fms.SetLayout("webEquiparazioniRichiesta")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("IdEquiparazione", codR, Enumerations.SearchOption.equals)

            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows

                        Record_Id = Data.FixNull(dr("Id_record"))


                    Next



                End If



            Catch ex As Exception

            End Try

            Return Record_Id


        End Function






    End Class

    Public Class GetRecord_IDbyCodR

        'Public Shared Property Denominazione As String
        'Public Shared Property Codice As String
        'Public Shared Property TipoEnte As String

        Public Shared Property Record_Id As String


        Public Shared Function GetRecord_ID(codR As String) As String
            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing


            fms = Conn.Connect()

            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fms.SetLayout("web_richiesta_master")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("Codice_Richiesta", codR, Enumerations.SearchOption.equals)

            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows

                        Record_Id = Data.FixNull(dr("Record_ID"))


                    Next



                End If



            Catch ex As Exception

            End Try

            Return Record_Id


        End Function






    End Class
    Public Class DatiNuovoCorso
        Public IdRecord As String
        Public IDCorso As String
        Public CodiceEnteRichiedente As String
        Public DescrizioneStatus As String
        Public CodiceStatus As String
        Public DescrizioneEnteRichiedente As String
        Public TipoEnte As String
        Public IndirizzoSvolgimento As String
        Public LocalitaSvolgimento As String
        Public CapSvolgimento As String
        Public PRSvolgimento As String
        Public RegioneSvolgimento As String
        Public SvolgimentoDataDa As String
        Public SvolgimentoDataA As String
        Public OraSvolgimentoDa As String
        Public OraSvolgimentoA As String
        Public TitoloCorso As String
        Public TotaleOre As String
        Public StatusPrimaCaricamentoXL As String
        Public DataEmissione As String
        Public MostraMonteOreFormazione As Integer

    End Class
    Public Class DatiCodiceFiscale
        Public IdRecord As String
        Public Nome As String
        Public Cognome As String
        Public Indirizzo As String
        Public Provincia As String
        Public Comune As String
        Public CodiceTessera As String
        Public Cap As String
        Public CodiceFiscale As String
        Public DataScadenza As Date
        Public DataNascita As Date
        Public Email As String
        Public LuogoNascita As String
        Public StatoNascita As String

    End Class

    Public Class DatiCodiceFiscaleRinnovi
        Public IdRecord As String
        Public Nome As String
        Public Cognome As String
        Public Indirizzo As String
        Public Provincia As String
        Public Comune As String
        Public CodiceTessera As String
        Public Cap As String
        Public CodiceFiscale As String
        Public DataScadenza As Date
        Public DataNascita As Date
        Public Email As String
        Public LuogoNascita As String
        Public StatoNascita As String
        Public telefono As String

        Public indirizzoResidenza As String
        Public comuneResidenza As String
        Public capResidenza As String
        Public Sport As String
        Public Specialita As String
        Public Livello As String
        Public qualifica As String
        Public disciplina As String
        Public codiceIscrizione As String
        Public codiceEnteEx As String
        Public nomeEnteEx As String
    End Class
    Public Shared Function getDatiCodiceFiscaleEE(Nome, Cognome, NumeroTessera, DataNascita) As DatiCodiceFiscale
        Dim fms As FMSAxml = Nothing
        Dim ds As DataSet = Nothing
        Dim ritorno As Boolean = False
        Dim dataScadenza As Date
        Dim DatiCodiceFiscale As New DatiCodiceFiscale

        fms = Conn.Connect()
        Dim it As String = DateTime.Now.Date.ToString("dd/MM/yyyy", New CultureInfo("it-IT"))
        '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
        '     fmsB.SetDatabase(Database)
        fms.SetLayout("webCheckCF")
        Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
        RequestA.AddSearchField("Nome", Nome, Enumerations.SearchOption.equals)
        RequestA.AddSearchField("Cognome", Cognome, Enumerations.SearchOption.equals)
        RequestA.AddSearchField("Codice tessera", NumeroTessera, Enumerations.SearchOption.equals)
        RequestA.AddSearchField("Data nascita", Data.SistemaDataUK(DataNascita), Enumerations.SearchOption.equals)
        RequestA.AddSearchField("Estero", "EE", Enumerations.SearchOption.equals)

        Try
            ds = RequestA.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                For Each dr In ds.Tables("main").Rows

                    If CDate(it) <= CDate(dr("DataScadenza")) Then

                        DatiCodiceFiscale.Nome = ASIWeb.Data.FixNull(dr("nome"))
                        DatiCodiceFiscale.Cognome = ASIWeb.Data.FixNull(dr("cognome"))
                        DatiCodiceFiscale.CodiceFiscale = ASIWeb.Data.FixNull(dr("CodiceFiscale"))
                        DatiCodiceFiscale.DataScadenza = ASIWeb.Data.FixNull(dr("DataScadenza"))
                        DatiCodiceFiscale.Indirizzo = ASIWeb.Data.FixNull(dr("Indirizzo Residenza"))
                        DatiCodiceFiscale.Provincia = ASIWeb.Data.FixNull(dr("provincia residenza"))
                        DatiCodiceFiscale.Comune = ASIWeb.Data.FixNull(dr("comune residenza"))
                        DatiCodiceFiscale.CodiceTessera = ASIWeb.Data.FixNull(dr("codice tessera"))
                        DatiCodiceFiscale.Cap = ASIWeb.Data.FixNull(dr("cap residenza"))

                        '*****  da sistemare la data **********

                        'Dim dataInseriraDa = ASIWeb.Data.FixNull(dr("Data Nascita"))
                        'Dim oDateDa As DateTime = DateTime.Parse(dataInseriraDa)
                        'Dim giorno = oDateDa.Day
                        'Dim anno = oDateDa.Year
                        'Dim mese = oDateDa.Month

                        'DatiCodiceFiscale.DataNascita = mese & "/" & giorno & "/" & anno

                        DatiCodiceFiscale.DataNascita = ASIWeb.Data.FixNull(dr("Data Nascita"))
                        DatiCodiceFiscale.Email = ASIWeb.Data.FixNull(dr("email"))
                        DatiCodiceFiscale.LuogoNascita = ASIWeb.Data.FixNull(dr("Luogo nascita"))
                        DatiCodiceFiscale.StatoNascita = ASIWeb.Data.FixNull(dr("Stato nascita"))
                    End If

                Next

            Else
                ritorno = False
            End If

            'If Today.Date <= dataScadenza Then

            '    ritorno = "tessera valida"
            'Else
            '    ritorno = "tessera scaduta"

            'End If
            Return DatiCodiceFiscale

        Catch ex As Exception

        End Try

    End Function
    Public Shared Function getDatiCodiceFiscale(codiceFiscale As String) As DatiCodiceFiscale
        Dim fms As FMSAxml = Nothing
        Dim ds As DataSet = Nothing
        Dim ritorno As Boolean = False
        Dim dataScadenza As Date
        Dim DatiCodiceFiscale As New DatiCodiceFiscale

        fms = Conn.Connect()
        Dim it As String = DateTime.Now.Date.ToString("dd/MM/yyyy", New CultureInfo("it-IT"))
        '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
        '     fmsB.SetDatabase(Database)
        fms.SetLayout("webCheckCF")
        Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
        RequestA.AddSearchField("CodiceFiscale", codiceFiscale, Enumerations.SearchOption.equals)
        '  RequestA.AddSearchField("DataScadenza", it, Enumerations.SearchOption.lessOrEqualThan)



        Try

            ds = RequestA.Execute()


            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                For Each dr In ds.Tables("main").Rows

                    If CDate(it) <= CDate(dr("DataScadenza")) Then

                        DatiCodiceFiscale.Nome = ASIWeb.Data.FixNull(dr("nome"))
                        DatiCodiceFiscale.Cognome = ASIWeb.Data.FixNull(dr("cognome"))
                        DatiCodiceFiscale.CodiceFiscale = ASIWeb.Data.FixNull(dr("CodiceFiscale"))
                        DatiCodiceFiscale.DataScadenza = ASIWeb.Data.FixNull(dr("DataScadenza"))
                        DatiCodiceFiscale.Indirizzo = ASIWeb.Data.FixNull(dr("Indirizzo Residenza"))
                        DatiCodiceFiscale.Provincia = ASIWeb.Data.FixNull(dr("provincia residenza"))
                        DatiCodiceFiscale.Comune = ASIWeb.Data.FixNull(dr("comune residenza"))
                        DatiCodiceFiscale.CodiceTessera = ASIWeb.Data.FixNull(dr("codice tessera"))
                        DatiCodiceFiscale.Cap = ASIWeb.Data.FixNull(dr("cap residenza"))

                        '*****  da sistemare la data **********

                        'Dim dataInseriraDa = ASIWeb.Data.FixNull(dr("Data Nascita"))
                        'Dim oDateDa As DateTime = DateTime.Parse(dataInseriraDa)
                        'Dim giorno = oDateDa.Day
                        'Dim anno = oDateDa.Year
                        'Dim mese = oDateDa.Month

                        'DatiCodiceFiscale.DataNascita = mese & "/" & giorno & "/" & anno



                        DatiCodiceFiscale.DataNascita = ASIWeb.Data.FixNull(dr("Data Nascita"))
                        DatiCodiceFiscale.Email = ASIWeb.Data.FixNull(dr("email"))
                        DatiCodiceFiscale.LuogoNascita = ASIWeb.Data.FixNull(dr("Luogo nascita"))
                        DatiCodiceFiscale.StatoNascita = ASIWeb.Data.FixNull(dr("Stato nascita"))
                    End If



                Next

            Else
                ritorno = False
            End If

            'If Today.Date <= dataScadenza Then

            '    ritorno = "tessera valida"
            'Else
            '    ritorno = "tessera scaduta"

            'End If
            Return DatiCodiceFiscale

        Catch ex As Exception

        End Try

    End Function

    Public Shared Function getSiglaProvincia(provincia As String) As String
        Dim fms As FMSAxml = Nothing
        Dim ds As DataSet = Nothing
        Dim ritorno As String = ""



        fms = Conn.Connect()
        fms.SetLayout("webProvinceRegioni")
        Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
        RequestA.AddSearchField("Provincia", provincia, Enumerations.SearchOption.equals)
        Try



            ds = RequestA.Execute()


            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                For Each dr In ds.Tables("main").Rows


                    ritorno = dr("Sigla")





                Next

            Else
                ritorno = " "
            End If
        Catch ex As Exception

        End Try
        Return ritorno
    End Function

    Public Shared Function getDatiCodiceFiscaleRinnovi(idSelected As String) As DatiCodiceFiscaleRinnovi
        Dim fms As FMSAxml = Nothing
        Dim ds As DataSet = Nothing
        Dim ritorno As Boolean = False
        Dim dataScadenza As Date
        Dim DatiCodiceFiscale As New DatiCodiceFiscaleRinnovi

        fms = Conn.Connect()
        Dim it As String = DateTime.Now.Date.ToString("dd/MM/yyyy", New CultureInfo("it-IT"))
        '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
        '     fmsB.SetDatabase(Database)
        fms.SetLayout("webAlbo")
        Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
        RequestA.AddSearchField("IDrecord", idSelected, Enumerations.SearchOption.equals)
        '  RequestA.AddSearchField("DataScadenza", it, Enumerations.SearchOption.lessOrEqualThan)



        Try

            ds = RequestA.Execute()


            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                For Each dr In ds.Tables("main").Rows

                    '   If CDate(it) <= CDate(dr("DataScadenza")) Then

                    DatiCodiceFiscale.Nome = ASIWeb.Data.FixNull(dr("nome"))
                    DatiCodiceFiscale.Cognome = ASIWeb.Data.FixNull(dr("cognome"))
                    DatiCodiceFiscale.CodiceFiscale = ASIWeb.Data.FixNull(dr("Codice Fiscale"))
                    DatiCodiceFiscale.DataScadenza = ASIWeb.Data.FixNull(dr("Scadenza"))
                    DatiCodiceFiscale.indirizzoResidenza = ASIWeb.Data.FixNull(dr("Indirizzo Residenza"))
                    DatiCodiceFiscale.Provincia = ASIWeb.Data.FixNull(dr("provincia"))
                    DatiCodiceFiscale.Comune = ASIWeb.Data.FixNull(dr("comune di nascita"))
                    DatiCodiceFiscale.CodiceTessera = ASIWeb.Data.FixNull(dr("numero tessera ASI"))
                    DatiCodiceFiscale.capResidenza = ASIWeb.Data.FixNull(dr("cap residenza"))
                    DatiCodiceFiscale.DataNascita = ASIWeb.Data.FixNull(dr("Data di Nascita"))
                    DatiCodiceFiscale.Email = ASIWeb.Data.FixNull(dr("indirizzo mail"))
                    DatiCodiceFiscale.LuogoNascita = ASIWeb.Data.FixNull(dr("comune di nascita"))
                    '  DatiCodiceFiscale.StatoNascita = ASIWeb.Data.FixNull(dr("Stato nascita"))
                    '  End If 
                    DatiCodiceFiscale.comuneResidenza = ASIWeb.Data.FixNull(dr("comune di residenza"))
                    DatiCodiceFiscale.telefono = ASIWeb.Data.FixNull(dr("telefono"))

                    DatiCodiceFiscale.Sport = ASIWeb.Data.FixNull(dr("SPORT"))
                    DatiCodiceFiscale.Specialita = ASIWeb.Data.FixNull(dr("SPECIALITA"))
                    DatiCodiceFiscale.Livello = ASIWeb.Data.FixNull(dr("livello_grado"))
                    DatiCodiceFiscale.qualifica = ASIWeb.Data.FixNull(dr("Qualifica"))
                    DatiCodiceFiscale.disciplina = ASIWeb.Data.FixNull(dr("disciplina"))
                    DatiCodiceFiscale.codiceIscrizione = ASIWeb.Data.FixNull(dr("codice iscrizione"))
                    DatiCodiceFiscale.codiceEnteEx = ASIWeb.Data.FixNull(dr("CodiceEnteAffiliante"))
                    DatiCodiceFiscale.nomeEnteEx = ASIWeb.Data.FixNull(dr("RILASCIATO Da"))
                Next

            Else
                ritorno = False
            End If

            'If Today.Date <= dataScadenza Then

            '    ritorno = "tessera valida"
            'Else
            '    ritorno = "tessera scaduta"

            'End If


        Catch ex As Exception

        End Try
        Return DatiCodiceFiscale
    End Function
    Public Class DatiNuovaEquiparazione

        Public IdRecord As String
        Public IDEquiparazione As String
        Public CodiceEnteRichiedente As String
        Public DescrizioneStatus As String
        Public CodiceStatus As String
        Public DescrizioneEnteRichiedente As String
        Public TipoEnte As String
        Public EquiCF As String
        Public CodiceFiscale As String
        Public CodiceTessera As String
        Public Nome As String
        Public Cognome As String
        Public DataScadenza As Date
        Public DataNascita As Date
        Public trovato As Boolean
        Public PagamentoTotale As Decimal
        Public Sport As String
        Public Disciplina As String
        Public Specialita As String
        Public IdEquiparazioneM As String



    End Class

    Public Class DatiNuovoRinnovo

        Public IdRecord As String
        Public IDRinnovo As String
        Public IDRinnovoM As String
        Public CodiceEnteRichiedente As String
        Public DescrizioneStatus As String
        Public CodiceStatus As String
        Public DescrizioneEnteRichiedente As String
        Public TipoEnte As String
        Public RinnovoCF As String
        Public CodiceFiscale As String
        Public CodiceTessera As String
        Public Nome As String
        Public Cognome As String
        Public DataScadenza As String
        Public trovato As Boolean
        Public ComuneNascita As String
        Public DataNascita As String
        Public StatoNascita As String
        Public CodiceIscrizione As String
        Public Disciplina As String
        Public Sport As String
        Public Specialita As String
        Public livello As String
        Public qualifica As String

    End Class
    Public Class Rinnovi
        Shared Function CancellaGruppo(idRecord As Integer)
            Dim ritorno As Boolean = False
            Try
                Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
                '  Dim ds As DataSet

                fmsP.SetLayout("webRinnoviMaster")
                Dim Request = fmsP.CreateDeleteRequest(idRecord)

                Request.Execute()
                ritorno = True
            Catch ex As Exception
                ritorno = False
            End Try

            Return ritorno

        End Function
        Public Shared Function GetIdRecordRinnovi(codR As String) As Integer
            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing
            Dim idrecord As Integer = 0

            fms = Conn.Connect()

            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fms.SetLayout("webRinnoviMaster")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("IDRinnovoM", codR, Enumerations.SearchOption.equals)

            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows
                        If Not String.IsNullOrEmpty(Data.FixNull(dr("ZipMaster"))) Then
                            idrecord = Data.FixNull(dr("idRecord"))
                        End If

                    Next
                End If



            Catch ex As Exception
                Return idrecord
            End Try

            Return idrecord


        End Function
        Public Shared Function NomeZipRinnovi(codR As String) As String
            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing
            Dim nomeZip As String = ""

            fms = Conn.Connect()

            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fms.SetLayout("webRinnoviMaster")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("IDRinnovoM", codR, Enumerations.SearchOption.equals)

            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows
                        If Not String.IsNullOrEmpty(Data.FixNull(dr("ZipMaster"))) Then
                            nomeZip = Data.FixNull(dr("ZipMasterContent"))
                        End If

                    Next
                End If



            Catch ex As Exception
                Return nomeZip
            End Try

            Return nomeZip


        End Function
        Public Shared Function EsisteZipRinnovi(codR As String) As Boolean
            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing
            Dim esiste As Boolean = False

            fms = Conn.Connect()

            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fms.SetLayout("webRinnoviMaster")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("IDRinnovoM", codR, Enumerations.SearchOption.equals)

            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows
                        If Not String.IsNullOrEmpty(Data.FixNull(dr("ZipMaster"))) Then
                            esiste = True
                        End If

                    Next
                End If



            Catch ex As Exception
                Return esiste
            End Try

            Return esiste


        End Function
        Public Shared Function quanteRichiestePerGruppo(idRinnovoM As Integer) As Integer
            Dim ritorno As Integer = 0
            Dim counter1 As Integer = 0
            Dim ds As DataSet

            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            fmsP.SetLayout("webRinnoviRichiesta2")
            Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)

            RequestP.AddSearchField("idRinnovoM", idRinnovoM, Enumerations.SearchOption.equals)
            '  RequestP.AddSearchField("idRinnovo", idRinnovo, Enumerations.SearchOption.equals)
            RequestP.AddSearchField("Codice_Status", "1...159")

            ds = RequestP.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

                counter1 = ds.Tables("main").Rows.Count


            Else


                counter1 = 0

            End If


            Return counter1

        End Function
        Shared Function AggiornaStatusEquiparazioniMoltia102(IdEquiparazione As Integer)
            Dim idrecord As Integer = 0
            Dim risposta As Integer = 0
            Dim ds As DataSet = Nothing
            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            '  Dim ds As DataSet

            fmsP.SetLayout("webEquiparazioniRichiestaMolti")
            Dim dsx As DataSet = Nothing
            Dim fmsx As FMSAxml = AsiModel.Conn.Connect()
            '  Dim ds As DataSet

            fmsx.SetLayout("webEquiparazioniRichiestaMolti")

            Dim RequestA = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("idEquiparazioneM", IdEquiparazione, Enumerations.SearchOption.equals)
            RequestA.AddSearchField("Equi_Fase", "2", Enumerations.SearchOption.equals)
            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows

                        idrecord = dr("idRecord")
                        If dr("codice_status") = "101" Then


                            Dim Request1 = fmsx.CreateEditRequest(idrecord)
                        Request1.AddField("codice_status", "102")
                            risposta = Request1.Execute()
                        End If
                    Next
                End If

            Catch ex As Exception
                idrecord = 0
            End Try

            Return idrecord
        End Function
        Shared Function AggiornaStatusMoltia155(IdRinnovo As Integer)
            Dim idrecord As Integer = 0
            Dim risposta As Integer = 0
            Dim ds As DataSet = Nothing

            Try
                Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
                '  Dim ds As DataSet

                fmsP.SetLayout("webRinnoviRichiesta2")
                Dim dsx As DataSet = Nothing
                Dim fmsx As FMSAxml = AsiModel.Conn.Connect()
                '  Dim ds As DataSet

                fmsx.SetLayout("webRinnoviRichiesta2")

                Dim RequestA = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
                RequestA.AddSearchField("idRinnovoM", IdRinnovo, Enumerations.SearchOption.equals)
                RequestA.AddSearchField("Rin_Fase", "1")
                ds = RequestA.Execute()
                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows
                        idrecord = dr("id_Record")
                        Dim Request1 = fmsx.CreateEditRequest(idrecord)
                        Request1.AddField("codice_status", "155")
                        risposta = Request1.Execute()
                    Next
                End If
            Catch ex As Exception
                idrecord = 0
            End Try

            Return idrecord
        End Function
        Shared Function AggiornaStatusEquiMoltia1145(IdCord As Integer)
            Dim idrecord As Integer = 0
            Dim risposta As Integer = 0
            Dim ds As DataSet = Nothing
            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            '  Dim ds As DataSet

            fmsP.SetLayout("webEquiparazioniRichiestaMolti")
            Dim dsx As DataSet = Nothing
            Dim fmsx As FMSAxml = AsiModel.Conn.Connect()
            '  Dim ds As DataSet

            fmsx.SetLayout("webEquiparazioniRichiestaMolti")

            Dim RequestA = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("idEquiparazioneM", IdCord, Enumerations.SearchOption.equals)

            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows

                        idrecord = dr("idRecord")

                        If dr("codice_status") = 104 Or dr("codice_status") = 104.5 Then

                        Else
                            If Not dr("codice_status") = "119" Then


                                Dim Request1 = fmsx.CreateEditRequest(idrecord)
                                Request1.AddField("codice_status", "114.5")
                                risposta = Request1.Execute()
                            End If
                        End If


                    Next
                End If

            Catch ex As Exception
                idrecord = 0
            End Try

            Return idrecord
        End Function
        Shared Function AggiornaStatusEquiMoltia113(IdCord As Integer)
            Dim idrecord As Integer = 0
            Dim risposta As Integer = 0
            Dim ds As DataSet = Nothing
            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            '  Dim ds As DataSet

            fmsP.SetLayout("webEquiparazioniRichiestaMolti")
            Dim dsx As DataSet = Nothing
            Dim fmsx As FMSAxml = AsiModel.Conn.Connect()
            '  Dim ds As DataSet

            fmsx.SetLayout("webEquiparazioniRichiestaMolti")

            Dim RequestA = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("idEquiparazioneM", IdCord, Enumerations.SearchOption.equals)

            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows

                        idrecord = dr("idRecord")

                        If dr("codice_status") = 104 Or dr("codice_status") = 104.5 Then

                        Else
                            If Not dr("codice_status") = "119" Then



                                Dim Request1 = fmsx.CreateEditRequest(idrecord)
                                Request1.AddField("codice_status", "113")
                                risposta = Request1.Execute()
                            End If
                        End If


                    Next
                End If

            Catch ex As Exception
                idrecord = 0
            End Try

            Return idrecord
        End Function

        Shared Function AggiornaStatusEquiMoltia112(IdCord As Integer)
            Dim idrecord As Integer = 0
            Dim risposta As Integer = 0
            Dim ds As DataSet = Nothing
            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            '  Dim ds As DataSet

            fmsP.SetLayout("webEquiparazioniRichiestaMolti")
            Dim dsx As DataSet = Nothing
            Dim fmsx As FMSAxml = AsiModel.Conn.Connect()
            '  Dim ds As DataSet

            fmsx.SetLayout("webEquiparazioniRichiestaMolti")

            Dim RequestA = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("idEquiparazioneM", IdCord, Enumerations.SearchOption.equals)

            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows

                        idrecord = dr("idRecord")

                        If dr("codice_status") = 104 Or dr("codice_status") = 104.5 Then

                        Else
                            Dim Request1 = fmsx.CreateEditRequest(idrecord)
                            Request1.AddField("codice_status", "112")
                            risposta = Request1.Execute()
                        End If


                    Next
                End If

            Catch ex As Exception
                idrecord = 0
            End Try

            Return idrecord
        End Function
        Shared Function AggiornaStatusMoltia156(IdRinnovo As Integer)
            Dim idrecord As Integer = 0
            Dim risposta As Integer = 0
            Dim ds As DataSet = Nothing
            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            '  Dim ds As DataSet

            fmsP.SetLayout("webRinnoviRichiesta2")
            Dim dsx As DataSet = Nothing
            Dim fmsx As FMSAxml = AsiModel.Conn.Connect()
            '  Dim ds As DataSet

            fmsx.SetLayout("webRinnoviRichiesta2")

            Dim RequestA = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("idRinnovoM", IdRinnovo, Enumerations.SearchOption.equals)

            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows

                        idrecord = dr("id_Record")

                        Dim Request1 = fmsx.CreateEditRequest(idrecord)
                        Request1.AddField("codice_status", "156")
                        risposta = Request1.Execute()
                    Next
                End If

            Catch ex As Exception
                idrecord = 0
            End Try

            Return idrecord
        End Function
        Shared Function AggiornaStatusMoltia158(IdRinnovo As Integer)
            Dim idrecord As Integer = 0
            Dim risposta As Integer = 0
            Dim ds As DataSet = Nothing
            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            '  Dim ds As DataSet

            fmsP.SetLayout("webRinnoviRichiesta2")
            Dim dsx As DataSet = Nothing
            Dim fmsx As FMSAxml = AsiModel.Conn.Connect()
            '  Dim ds As DataSet

            fmsx.SetLayout("webRinnoviRichiesta2")

            Dim RequestA = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("idRinnovoM", IdRinnovo, Enumerations.SearchOption.equals)

            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows

                        idrecord = dr("id_Record")

                        Dim Request1 = fmsx.CreateEditRequest(idrecord)
                        Request1.AddField("codice_status", "158")
                        risposta = Request1.Execute()
                    Next
                End If

            Catch ex As Exception
                idrecord = 0
            End Try

            Return idrecord
        End Function
        Shared Function AggiornaStatusMoltia157(IdRinnovo As Integer)
            Dim idrecord As Integer = 0
            Dim risposta As Integer = 0
            Dim ds As DataSet = Nothing
            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            '  Dim ds As DataSet

            fmsP.SetLayout("webRinnoviRichiesta2")
            Dim dsx As DataSet = Nothing
            Dim fmsx As FMSAxml = AsiModel.Conn.Connect()
            '  Dim ds As DataSet

            fmsx.SetLayout("webRinnoviRichiesta2")

            Dim RequestA = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("idRinnovoM", IdRinnovo, Enumerations.SearchOption.equals)

            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows

                        idrecord = dr("id_Record")

                        Dim Request1 = fmsx.CreateEditRequest(idrecord)
                        Request1.AddField("codice_status", "157")
                        risposta = Request1.Execute()
                    Next
                End If

            Catch ex As Exception
                idrecord = 0
            End Try

            Return idrecord
        End Function
        Shared Function AggiornaStatusMoltia159(IdRinnovo As Integer)
            Dim idrecord As Integer = 0
            Dim risposta As Integer = 0
            Dim ds As DataSet = Nothing
            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            '  Dim ds As DataSet

            fmsP.SetLayout("webRinnoviRichiesta2")
            Dim dsx As DataSet = Nothing
            Dim fmsx As FMSAxml = AsiModel.Conn.Connect()
            '  Dim ds As DataSet

            fmsx.SetLayout("webRinnoviRichiesta2")

            Dim RequestA = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("idRinnovoM", IdRinnovo, Enumerations.SearchOption.equals)

            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows

                        idrecord = dr("id_Record")

                        Dim Request1 = fmsx.CreateEditRequest(idrecord)
                        Request1.AddField("codice_status", "159")
                        risposta = Request1.Execute()
                    Next
                End If

            Catch ex As Exception
                idrecord = 0
            End Try

            Return idrecord
        End Function
        Shared Function PrendiIDrecordMaster(IdRinnovo As Integer)
            Dim idrecord As Integer = 0

            Dim ds As DataSet = Nothing
            Try

                Dim fmsP As FMSAxml = AsiModel.Conn.Connect()

                fmsP.SetLayout("webRinnoviMaster")
                Dim RequestA = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
                RequestA.AddSearchField("idRinnovoM", IdRinnovo, Enumerations.SearchOption.equals)
                ds = RequestA.Execute()
                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows
                        idrecord = dr("idRecord")
                    Next
                End If

            Catch ex As Exception
                idrecord = 0
            End Try

            Return idrecord
        End Function

        Public Shared Function PrendiValoriNuovoRinnovoByCF(codiceFiscale As String, codiceEnte As String) As DatiNuovoRinnovo
            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing
            Dim DatiRinnovo As New DatiNuovoRinnovo
            Dim ritorno As Boolean = False
            fms = Conn.Connect()

            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fms.SetLayout("webRinnoviRichiesta")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("Asi_CodiceFiscale", codiceFiscale, Enumerations.SearchOption.equals)
            RequestA.AddSearchField("Codice_Ente_Richiedente", codiceEnte, Enumerations.SearchOption.equals)

            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows

                        If Data.FixNull(dr("Codice_Status")) = "159" Or Data.FixNull(dr("Codice_Status")) = "160" Then

                            DatiRinnovo.CodiceEnteRichiedente = Data.FixNull(dr("Codice_Ente_Richiedente"))
                            DatiRinnovo.DescrizioneEnteRichiedente = Data.FixNull(dr("Descrizione_Ente_Richiedente"))
                            DatiRinnovo.IDRinnovo = Data.FixNull(dr("IDRinnovo"))
                            DatiRinnovo.DescrizioneStatus = Data.FixNull(dr("Descrizione_StatusWeb"))
                            DatiRinnovo.CodiceStatus = Data.FixNull(dr("Codice_Status"))
                            DatiRinnovo.TipoEnte = Data.FixNull(dr("Tipo_Ente"))
                            DatiRinnovo.IdRecord = Data.FixNull(dr("Id_record"))
                            DatiRinnovo.RinnovoCF = Data.FixNull(dr("Rin_CFVerificatoTessera"))
                            DatiRinnovo.CodiceFiscale = Data.FixNull(dr("Rin_CodiceFiscale"))
                            DatiRinnovo.CodiceTessera = Data.FixNull(dr("Rin_CodiceTessera"))
                            DatiRinnovo.Nome = Data.FixNull(dr("Rin_Nome"))
                            DatiRinnovo.Cognome = Data.FixNull(dr("Rin_Cognome"))
                            DatiRinnovo.DataScadenza = Data.FixNull(dr("Rin_DataScadenza"))
                            DatiRinnovo.trovato = True


                        End If
                    Next


                Else
                    DatiRinnovo.trovato = False
                End If



            Catch ex As Exception
                DatiRinnovo.trovato = False
            End Try

            Return DatiRinnovo

        End Function
        Public Shared Function PrendiValoriNuovoRinnovoByCFASI(codiceFiscale As String, codiceEnte As String) As DatiNuovoRinnovo
            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing
            Dim DatiRinnovo As New DatiNuovoRinnovo

            fms = Conn.Connect()

            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fms.SetLayout("webRinnoviRichiesta")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("Asi_CodiceFiscale", codiceFiscale, Enumerations.SearchOption.equals)
            RequestA.AddSearchField("Codice_Ente_Richiedente", codiceEnte, Enumerations.SearchOption.equals)

            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows

                        If Data.FixNull(dr("Codice_Status")) = "159" Or Data.FixNull(dr("Codice_Status")) = "160" Then

                            DatiRinnovo.CodiceEnteRichiedente = Data.FixNull(dr("Codice_Ente_Richiedente"))
                            DatiRinnovo.DescrizioneEnteRichiedente = Data.FixNull(dr("Descrizione_Ente_Richiedente"))
                            DatiRinnovo.IDRinnovo = Data.FixNull(dr("IDRinnovo"))
                            DatiRinnovo.DescrizioneStatus = Data.FixNull(dr("Descrizione_StatusWeb"))
                            DatiRinnovo.CodiceStatus = Data.FixNull(dr("Codice_Status"))
                            DatiRinnovo.TipoEnte = Data.FixNull(dr("Tipo_Ente"))
                            DatiRinnovo.IdRecord = Data.FixNull(dr("Id_record"))
                            DatiRinnovo.RinnovoCF = Data.FixNull(dr("Rin_CFVerificatoTessera"))
                            DatiRinnovo.CodiceFiscale = Data.FixNull(dr("ASI_CodiceFiscale"))
                            DatiRinnovo.CodiceTessera = Data.FixNull(dr("ASI_CodiceTessera"))
                            DatiRinnovo.Nome = Data.FixNull(dr("ASI_Nome"))
                            DatiRinnovo.Cognome = Data.FixNull(dr("ASI_Cognome"))
                            DatiRinnovo.DataScadenza = Data.FixNull(dr("ASI_DataScadenza"))
                            DatiRinnovo.CodiceIscrizione = Data.FixNull(dr("ASI_CodiceIscrizione"))
                            DatiRinnovo.Sport = Data.FixNull(dr("ASI_sport"))
                            DatiRinnovo.Disciplina = Data.FixNull(dr("ASI_disciplina"))
                            DatiRinnovo.Specialita = Data.FixNull(dr("ASI_specialita"))
                            DatiRinnovo.livello = Data.FixNull(dr("ASI_livello"))
                            DatiRinnovo.qualifica = Data.FixNull(dr("ASI_qualifica"))
                            DatiRinnovo.trovato = True

                        End If
                    Next


                Else
                    DatiRinnovo.trovato = False
                End If



            Catch ex As Exception

            End Try

            Return DatiRinnovo

        End Function
        Public Shared Function PrendiValoriNuovoRinnovo2(IdRecord As Integer) As DatiNuovoRinnovo
            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing
            Dim DatiRinnovo As New DatiNuovoRinnovo

            fms = Conn.Connect()

            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fms.SetLayout("webRinnoviRichiesta2")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("id_record", IdRecord, Enumerations.SearchOption.equals)


            Try


                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows

                        DatiRinnovo.CodiceEnteRichiedente = Data.FixNull(dr("Codice_Ente_Richiedente"))
                        DatiRinnovo.DescrizioneEnteRichiedente = Data.FixNull(dr("Descrizione_Ente_Richiedente"))
                        DatiRinnovo.IDRinnovo = Data.FixNull(dr("IDRinnovo"))
                        DatiRinnovo.DescrizioneStatus = Data.FixNull(dr("Descrizione_StatusWeb"))
                        DatiRinnovo.CodiceStatus = Data.FixNull(dr("Codice_Status"))
                        DatiRinnovo.TipoEnte = Data.FixNull(dr("Tipo_Ente"))
                        DatiRinnovo.IdRecord = Data.FixNull(dr("Id_record"))
                        DatiRinnovo.RinnovoCF = Data.FixNull(dr("Rin_CFVerificatoTessera"))
                        DatiRinnovo.CodiceFiscale = Data.FixNull(dr("Rin_CodiceFiscale"))
                        DatiRinnovo.CodiceTessera = Data.FixNull(dr("Rin_NumeroTessera"))
                        DatiRinnovo.Nome = Data.FixNull(dr("Rin_Nome"))
                        DatiRinnovo.Cognome = Data.FixNull(dr("Rin_Cognome"))
                        DatiRinnovo.DataScadenza = Data.FixNull(dr("Rin_DataScadenza"))
                        DatiRinnovo.IDRinnovoM = Data.FixNull(dr("IDRinnovoM"))
                    Next



                End If
                Return DatiRinnovo

            Catch ex As Exception

            End Try


        End Function


        Public Shared Function QuantiRinnoviPerGruppoEA(IDRinnovoM As Integer) As Integer
            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing
            Dim DatiRinnovo As New DatiNuovoRinnovo
            Dim quanti As Integer = 0
            fms = Conn.Connect()

            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fms.SetLayout("webRinnoviRichiesta2")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("IDRinnovoM", IDRinnovoM, Enumerations.SearchOption.equals)
            RequestA.AddSearchField("dichiarazioneEAInviata", "n", Enumerations.SearchOption.equals)
            '  RequestA.AddSearchField("Rin_InviaA", "EA", Enumerations.SearchOption.equals)
            RequestA.AddSearchField("Codice_Status", "151", Enumerations.SearchOption.equals)

            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

                    quanti = ds.Tables("main").Rows.Count

                End If



            Catch ex As Exception

            End Try

            Return quanti
        End Function
        Public Shared Function QuantiRinnoviPerGruppo(IDRinnovoM As Integer) As Integer
            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing
            Dim DatiRinnovo As New DatiNuovoRinnovo
            Dim quanti As Integer = 0
            fms = Conn.Connect()

            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fms.SetLayout("webRinnoviRichiesta2")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("IDRinnovoM", IDRinnovoM, Enumerations.SearchOption.equals)
            RequestA.AddSearchField("Codice_Status", "1...159")
            RequestA.AddSearchField("Rin_Fase", "1")
            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

                    quanti = ds.Tables("main").Rows.Count

                End If



            Catch ex As Exception

            End Try

            Return quanti
        End Function
        Public Shared Function PrendiValoriNuovoRinnovo(IDRinnovo As String) As DatiNuovoRinnovo
            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing
            Dim DatiRinnovo As New DatiNuovoRinnovo

            fms = Conn.Connect()

            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fms.SetLayout("webRinnoviRichiesta")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("IDRinnovo", IDRinnovo, Enumerations.SearchOption.equals)


            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows

                        DatiRinnovo.CodiceEnteRichiedente = Data.FixNull(dr("Codice_Ente_Richiedente"))
                        DatiRinnovo.DescrizioneEnteRichiedente = Data.FixNull(dr("Descrizione_Ente_Richiedente"))
                        DatiRinnovo.IDRinnovo = Data.FixNull(dr("IDRinnovo"))
                        DatiRinnovo.DescrizioneStatus = Data.FixNull(dr("Descrizione_StatusWeb"))
                        DatiRinnovo.CodiceStatus = Data.FixNull(dr("Codice_Status"))
                        DatiRinnovo.TipoEnte = Data.FixNull(dr("Tipo_Ente"))
                        DatiRinnovo.IdRecord = Data.FixNull(dr("Id_record"))
                        DatiRinnovo.RinnovoCF = Data.FixNull(dr("Rin_CFVerificatoTessera"))
                        DatiRinnovo.CodiceFiscale = Data.FixNull(dr("Rin_CodiceFiscale"))
                        DatiRinnovo.CodiceTessera = Data.FixNull(dr("Rin_CodiceTessera"))
                        DatiRinnovo.Nome = Data.FixNull(dr("Rin_Nome"))
                        DatiRinnovo.Cognome = Data.FixNull(dr("Rin_CognomeE"))
                        DatiRinnovo.DataScadenza = Data.FixNull(dr("Rin_DataScadenza"))
                    Next



                End If



            Catch ex As Exception

            End Try

            Return DatiRinnovo

        End Function
        Public Shared Function CaricaDatiTesseramentoEE(nome As String, cognome As String, dataNascita As Date, numeroTessera As String) As DatiNuovoRinnovo
            '  Dim litNumRichieste As Literal = DirectCast(ContentPlaceHolder1.FindControl("LitNumeroRichiesta"), Literal)


            Dim ds As DataSet

            Dim dataOggi As Date = Today.Date
            Dim DatiRinnovo As New DatiNuovoRinnovo
            'Dim dataNascitaOK As Date = Data.SistemaDataUK(dataNascita)
            Dim fmsP As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
            '  Dim ds As DataSet
            Dim risposta As String = ""
            Dim it As String = DateTime.Now.Date.ToString("dd/MM/yyyy", New CultureInfo("it-IT"))
            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fmsP.SetLayout("webCheckCF")
            Dim RequestA = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("Nome", nome, Enumerations.SearchOption.equals)
            RequestA.AddSearchField("Cognome", cognome, Enumerations.SearchOption.equals)
            RequestA.AddSearchField("Codice tessera", numeroTessera, Enumerations.SearchOption.equals)
            '   RequestA.AddSearchField("Data nascita", dataNascita, Enumerations.SearchOption.equals)
            RequestA.AddSearchField("Data nascita", Data.SistemaDataUK(dataNascita), Enumerations.SearchOption.equals)
            RequestA.AddSearchField("Estero", "EE", Enumerations.SearchOption.equals)
            '  RequestA.AddSearchField("DataScadenza", it, Enumerations.SearchOption.lessOrEqualThan)

            Try



                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows

                        If CDate(it) <= CDate(dr("DataScadenza")) Then


                            DatiRinnovo.Cognome = Data.FixNull(dr("Cognome"))
                            DatiRinnovo.Nome = Data.FixNull(dr("Nome"))
                            ' DatiRinnovo.DataScadenza = Data.FixNull(dr("Rin_DataScadenza"))
                            DatiRinnovo.DataScadenza = Data.FixNull(dr("DataScadenza"))
                            DatiRinnovo.CodiceTessera = Data.FixNull(dr("Codice tessera"))
                            DatiRinnovo.ComuneNascita = Data.FixNull(dr("Luogo nascita"))
                            DatiRinnovo.DataNascita = Data.FixNull(dr("Data nascita"))
                            DatiRinnovo.StatoNascita = Data.FixNull(dr("Stato nascita"))

                        End If



                    Next

                Else

                End If





                '    Request.AddScript("SistemaEncodingCorsoFase2", IDCorso)
                Return DatiRinnovo

            Catch ex As Exception

            End Try



        End Function
        Public Shared Function CaricaDatiTesseramento(codiceFiscale As String) As DatiNuovoRinnovo
            '  Dim litNumRichieste As Literal = DirectCast(ContentPlaceHolder1.FindControl("LitNumeroRichiesta"), Literal)


            Dim ds As DataSet

            Dim dataOggi As Date = Today.Date
            Dim DatiRinnovo As New DatiNuovoRinnovo

            Dim fmsP As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
            '  Dim ds As DataSet
            Dim risposta As String = ""
            Dim it As String = DateTime.Now.Date.ToString("dd/MM/yyyy", New CultureInfo("it-IT"))
            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fmsP.SetLayout("webCheckCF")
            Dim RequestA = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("CodiceFiscale", codiceFiscale, Enumerations.SearchOption.equals)
            '  RequestA.AddSearchField("DataScadenza", it, Enumerations.SearchOption.lessOrEqualThan)

            Try



                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows

                        If CDate(it) <= CDate(dr("DataScadenza")) Then


                            DatiRinnovo.Cognome = Data.FixNull(dr("Cognome"))
                            DatiRinnovo.Nome = Data.FixNull(dr("Nome"))
                            ' DatiRinnovo.DataScadenza = Data.FixNull(dr("Rin_DataScadenza"))
                            DatiRinnovo.DataScadenza = Data.FixNull(dr("DataScadenza"))
                            DatiRinnovo.CodiceTessera = Data.FixNull(dr("Codice tessera"))
                            DatiRinnovo.ComuneNascita = Data.FixNull(dr("Luogo nascita"))
                            DatiRinnovo.DataNascita = Data.FixNull(dr("Data nascita"))
                            DatiRinnovo.StatoNascita = Data.FixNull(dr("Stato nascita"))

                        End If



                    Next

                Else

                End If





                '    Request.AddScript("SistemaEncodingCorsoFase2", IDCorso)
                Return DatiRinnovo

            Catch ex As Exception

            End Try



        End Function

        Public Shared Function CercaIDRecordRinnovoM(IDRinnovoM As Integer) As Integer


            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            Dim ds As DataSet
            Dim idrecord As Integer


            fmsP.SetLayout("webRinnoviMaster")
            Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestP.AddSearchField("IDRinnovoM", IDRinnovoM, Enumerations.SearchOption.equals)
            Try


                ds = RequestP.Execute()
            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                For Each dr In ds.Tables("main").Rows

                    idrecord = Data.FixNull(dr("idRecord"))

                Next


            End If


                Return idrecord
            Catch ex As Exception

            End Try

        End Function
        Public Shared Function NuovoRinnovo(codiceEnteRichiedente As Integer) As Integer


            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            Dim ds As DataSet
            Dim idrecord As Integer
            Dim idRinnovoM As Integer = 0
            fmsP.SetLayout("webRinnoviMaster")
            Dim Request = fmsP.CreateNewRecordRequest()

            Request.AddField("CodiceEnteRichiedente", codiceEnteRichiedente)
            Request.AddField("CodiceStatus", "0")

            Try


                idrecord = Request.Execute()

                Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
                RequestP.AddSearchField("idrecord", idrecord, Enumerations.SearchOption.equals)

                ds = RequestP.Execute()
                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows

                        idRinnovoM = Data.FixNull(dr("IDRinnovoM"))

                    Next


                End If


                Return idRinnovoM
            Catch ex As Exception

            End Try
        End Function


    End Class
    Public Shared Function GetPaging() As String
        Dim fms As FMSAxml = Nothing
        Dim ds As DataSet = Nothing
        Dim numero As String = 0


        fms = Conn.Connect()

        fms.SetLayout("webUtility")
        Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
        RequestA.AddSearchField("id", "3", Enumerations.SearchOption.equals)

        Try
            ds = RequestA.Execute()


            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                For Each dr In ds.Tables("main").Rows
                    numero = dr("pagingFoto")

                Next
            End If



        Catch ex As Exception

            numero = "2"
        End Try

        Return numero

    End Function
    Public Class Equiparazione
        Public Shared Function GetMotivoRespintoEqui(codR As String) As String
            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing
            Dim motivo As String = ""

            fms = Conn.Connect()

            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fms.SetLayout("webEquiparazioniMaster")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("IdEquiparazioneM", codR, Enumerations.SearchOption.equals)

            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows
                        motivo = Data.FixNull(dr("NoteValutazioneDT"))
                    Next
                End If



            Catch ex As Exception

            End Try

            Return motivo


        End Function
        Public Shared Function PrendiValoriNuovaEquiparazioneByCF(codiceFiscale As String, codiceEnte As String) As DataSet
            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing
            '   Dim DatiEquiparazione As New DatiNuovaEquiparazione

            fms = Conn.Connect()

            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fms.SetLayout("webEquiparazioniRichiesta")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("Equi_CodiceFiscale", codiceFiscale, Enumerations.SearchOption.equals)
            RequestA.AddSearchField("Codice_Ente_Richiedente", codiceEnte, Enumerations.SearchOption.equals)

            Try
                ds = RequestA.Execute()


                '  If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                '   For Each dr In ds.Tables("main").Rows
                'If Data.FixNull(dr("Codice_Status")) = "115" Or Data.FixNull(dr("Codice_Status")) = "119" Then

                '    DatiEquiparazione.CodiceEnteRichiedente = Data.FixNull(dr("Codice_Ente_Richiedente"))
                '    DatiEquiparazione.DescrizioneEnteRichiedente = Data.FixNull(dr("Descrizione_Ente_Richiedente"))
                '    DatiEquiparazione.IDEquiparazione = Data.FixNull(dr("IDEquiparazione"))
                '    DatiEquiparazione.DescrizioneStatus = Data.FixNull(dr("Descrizione_StatusWeb"))
                '    DatiEquiparazione.CodiceStatus = Data.FixNull(dr("Codice_Status"))
                '    DatiEquiparazione.TipoEnte = Data.FixNull(dr("Tipo_Ente"))
                '    DatiEquiparazione.IdRecord = Data.FixNull(dr("Id_record"))
                '    DatiEquiparazione.EquiCF = Data.FixNull(dr("Equi_CFVerificatoTessera"))
                '    DatiEquiparazione.CodiceFiscale = Data.FixNull(dr("Equi_CodiceFiscale"))
                '    DatiEquiparazione.CodiceTessera = Data.FixNull(dr("Equi_NumeroTessera"))
                '    DatiEquiparazione.Nome = Data.FixNull(dr("Equi_Nome"))
                '    DatiEquiparazione.Cognome = Data.FixNull(dr("Equi_Cognome"))
                '    DatiEquiparazione.DataScadenza = Data.FixNull(dr("Equi_DataScadenza"))
                '    DatiEquiparazione.trovato = True

                'End If
                '  Next


                ' Else
                ' DatiEquiparazione.trovato = False
                ' End If



            Catch ex As Exception

            End Try

            Return ds

        End Function
        Public Shared Function QuanteEquiparazioniPerGruppoEA(IDEquiparazioneM As Integer) As Integer
            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing
            Dim DatiRinnovo As New DatiNuovoRinnovo
            Dim quanti As Integer = 0
            fms = Conn.Connect()

            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fms.SetLayout("webRinnoviRichiestaMolti")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("IDEquiparazioneM", IDEquiparazioneM, Enumerations.SearchOption.equals)
            RequestA.AddSearchField("dichiarazioneEAInviata", "n", Enumerations.SearchOption.equals)
            '  RequestA.AddSearchField("Rin_InviaA", "EA", Enumerations.SearchOption.equals)
            RequestA.AddSearchField("Codice_Status", "151", Enumerations.SearchOption.equals)

            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

                    quanti = ds.Tables("main").Rows.Count

                End If



            Catch ex As Exception

            End Try

            Return quanti
        End Function

        Public Shared Function quanteRichiesteValutazione105(IdEquiparazioneM As Integer, status As Integer) As Integer
            Dim ritorno As Integer = 0
            Dim counter1 As Integer = 0
            Dim ds As DataSet

            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            fmsP.SetLayout("webEquiparazioniRichiestaMolti")
            Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)

            RequestP.AddSearchField("idEquiparazioneM", IdEquiparazioneM, Enumerations.SearchOption.equals)
            '  RequestP.AddSearchField("idRinnovo", idRinnovo, Enumerations.SearchOption.equals)
            RequestP.AddSearchField("Codice_Status", status)

            ds = RequestP.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

                counter1 = ds.Tables("main").Rows.Count


            Else


                counter1 = 0

            End If


            Return counter1

        End Function






        Public Shared Function quanteRichiesteValutazioneEsito(IdEquiparazioneM As Integer, status As Integer) As Integer
            Dim ritorno As Integer = 0
            Dim counter1 As Integer = 0
            Dim ds As DataSet

            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            fmsP.SetLayout("webEquiparazioniRichiestaMolti")
            Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)

            RequestP.AddSearchField("idEquiparazioneM", IdEquiparazioneM, Enumerations.SearchOption.equals)
            '  RequestP.AddSearchField("idRinnovo", idRinnovo, Enumerations.SearchOption.equals)
            RequestP.AddSearchField("Codice_Status", status)

            ds = RequestP.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

                counter1 = ds.Tables("main").Rows.Count


            Else


                counter1 = 0

            End If


            Return counter1

        End Function




        Public Shared Function quanteRichiestePerGruppo(IdEquiparazioneM As Integer) As Integer
            Dim ritorno As Integer = 0
            Dim counter1 As Integer = 0
            Dim ds As DataSet

            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            fmsP.SetLayout("webEquiparazioniRichiestaMolti")
            Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)

            RequestP.AddSearchField("idEquiparazioneM", IdEquiparazioneM, Enumerations.SearchOption.equals)
            '  RequestP.AddSearchField("idRinnovo", idRinnovo, Enumerations.SearchOption.equals)
            RequestP.AddSearchField("Codice_Status", "101...114.5")
            RequestP.AddSearchField("Equi_Fase", "2", Enumerations.SearchOption.equals)
            ds = RequestP.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

                counter1 = ds.Tables("main").Rows.Count


            Else


                counter1 = 0

            End If


            Return counter1

        End Function


        Public Shared Function QuanteEquiparazioniPerGruppo(IDEquiparazioneM As Integer) As Integer
            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing
            'Dim DatiRinnovo As New DatiNuovoRinnovo
            Dim quanti As Integer = 0
            fms = Conn.Connect()

            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fms.SetLayout("webEquiparazioniRichiestaMolti")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("IDEquiparazioneM", IDEquiparazioneM, Enumerations.SearchOption.equals)
            RequestA.AddSearchField("Codice_Status", "1...114.5")
            RequestA.AddSearchField("Equi_Fase", "2")
            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

                    quanti = ds.Tables("main").Rows.Count

                End If



            Catch ex As Exception

            End Try

            Return quanti
        End Function



        Public Shared Function PrendiValoriNuovaEquiparazione(IDEquiparazione As String) As DatiNuovaEquiparazione
            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing
            Dim DatiEquiparazione As New DatiNuovaEquiparazione

            fms = Conn.Connect()

            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fms.SetLayout("webEquiparazioniRichiesta")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("IDEquiparazione", IDEquiparazione, Enumerations.SearchOption.equals)


            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows

                        DatiEquiparazione.CodiceEnteRichiedente = Data.FixNull(dr("Codice_Ente_Richiedente"))
                        DatiEquiparazione.DescrizioneEnteRichiedente = Data.FixNull(dr("Descrizione_Ente_Richiedente"))
                        DatiEquiparazione.IDEquiparazione = Data.FixNull(dr("IDEquiparazione"))
                        DatiEquiparazione.DescrizioneStatus = Data.FixNull(dr("Descrizione_StatusWeb"))
                        DatiEquiparazione.CodiceStatus = Data.FixNull(dr("Codice_Status"))
                        DatiEquiparazione.TipoEnte = Data.FixNull(dr("Tipo_Ente"))
                        DatiEquiparazione.IdRecord = Data.FixNull(dr("Id_record"))
                        DatiEquiparazione.EquiCF = Data.FixNull(dr("Equi_CFVerificatoTessera"))
                        DatiEquiparazione.CodiceFiscale = Data.FixNull(dr("Equi_CodiceFiscale"))
                        DatiEquiparazione.CodiceTessera = Data.FixNull(dr("Equi_NumeroTessera"))
                        DatiEquiparazione.Nome = Data.FixNull(dr("Equi_Nome"))
                        DatiEquiparazione.Cognome = Data.FixNull(dr("Equi_Cognome"))
                        DatiEquiparazione.DataScadenza = Data.FixNull(dr("Equi_DataScadenza"))
                        DatiEquiparazione.PagamentoTotale = Data.FixNull(dr("QuotaPagamento"))
                        '  DatiEquiparazione.IdEquiparazioneM = Data.FixNull(dr("IdEquiparazioneM"))
                        '    DatiEquiparazione.MostraMonteOreFormazione = Data.FixNull(dr("MonteOreFormazioneFlag"))
                    Next



                End If



            Catch ex As Exception

            End Try

            Return DatiEquiparazione

        End Function

        Public Shared Function PrendiValoriNuovaEquiparazione2(IDEquiparazione As String) As DatiNuovaEquiparazione
            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing
            Dim DatiEquiparazione As New DatiNuovaEquiparazione
            Try
                fms = Conn.Connect()

                '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
                '     fmsB.SetDatabase(Database)
                fms.SetLayout("webEquiparazioniRichiestaMolti")
                Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
                RequestA.AddSearchField("IDRecord", IDEquiparazione, Enumerations.SearchOption.equals)



                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows

                        DatiEquiparazione.CodiceEnteRichiedente = Data.FixNull(dr("Codice_Ente_Richiedente"))
                        DatiEquiparazione.DescrizioneEnteRichiedente = Data.FixNull(dr("Descrizione_Ente_Richiedente"))
                        DatiEquiparazione.IDEquiparazione = Data.FixNull(dr("idrecord"))
                        DatiEquiparazione.DescrizioneStatus = Data.FixNull(dr("Descrizione_StatusWeb"))
                        DatiEquiparazione.CodiceStatus = Data.FixNull(dr("Codice_Status"))
                        DatiEquiparazione.TipoEnte = Data.FixNull(dr("Tipo_Ente"))
                        DatiEquiparazione.IdRecord = Data.FixNull(dr("Idrecord"))
                        DatiEquiparazione.EquiCF = Data.FixNull(dr("Equi_CFVerificatoTessera"))
                        DatiEquiparazione.CodiceFiscale = Data.FixNull(dr("Equi_CodiceFiscale"))
                        DatiEquiparazione.CodiceTessera = Data.FixNull(dr("Equi_NumeroTessera"))
                        DatiEquiparazione.Nome = Data.FixNull(dr("Equi_Nome"))
                        DatiEquiparazione.Cognome = Data.FixNull(dr("Equi_Cognome"))
                        DatiEquiparazione.DataScadenza = Data.FixNull(dr("Equi_DataScadenza"))
                        DatiEquiparazione.Sport = Data.FixNull(dr("Equi_Sport_Interessato"))
                        DatiEquiparazione.Disciplina = Data.FixNull(dr("Equi_Disciplina_Interessata"))
                        DatiEquiparazione.Specialita = Data.FixNull(dr("Equi_Specialita"))
                        DatiEquiparazione.IdEquiparazioneM = Data.FixNull(dr("IdEquiparazioneM"))
                        '   DatiEquiparazione.PagamentoTotale = Data.FixNull(dr("QuotaPagamento"))
                        '    DatiEquiparazione.MostraMonteOreFormazione = Data.FixNull(dr("MonteOreFormazioneFlag"))
                    Next



                End If



            Catch ex As Exception

            End Try

            Return DatiEquiparazione

        End Function
        Public Shared Function CaricaDatiTesseramentoEE(Nome As String, Cognome As String, dataNascita As Date, numeroTessera As String) As DatiNuovaEquiparazione
            '  Dim litNumRichieste As Literal = DirectCast(ContentPlaceHolder1.FindControl("LitNumeroRichiesta"), Literal)


            Dim ds As DataSet

            Dim dataOggi As Date = Today.Date
            Dim DettaglioEquiparazione As New DatiNuovaEquiparazione

            Dim fmsP As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
            '  Dim ds As DataSet
            Dim risposta As String = ""
            Dim it As DateTime = DateTime.Now.Date.ToString("dd/MM/yyyy", New CultureInfo("it-IT"))
            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fmsP.SetLayout("webCheckCF")
            Dim RequestA = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("Nome", Nome, Enumerations.SearchOption.equals)
            RequestA.AddSearchField("Cognome", Cognome, Enumerations.SearchOption.equals)
            RequestA.AddSearchField("Codice tessera", numeroTessera, Enumerations.SearchOption.equals)
            RequestA.AddSearchField("Data nascita", Data.SistemaDataUK(dataNascita), Enumerations.SearchOption.equals)
            RequestA.AddSearchField("Estero", "EE", Enumerations.SearchOption.equals)




            ds = RequestA.Execute()


            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                For Each dr In ds.Tables("main").Rows

                    If it <= dr("Data Scadenza") Then


                        DettaglioEquiparazione.Cognome = Data.FixNull(dr("Cognome"))
                        DettaglioEquiparazione.Nome = Data.FixNull(dr("Nome"))
                        DettaglioEquiparazione.DataScadenza = Data.FixNull(dr("Data Scadenza"))
                        DettaglioEquiparazione.CodiceTessera = Data.FixNull(dr("Codice tessera"))
                        DettaglioEquiparazione.DataNascita = Data.FixNull(dr("Data nascita"))

                    End If



                Next

            Else

            End If





            '    Request.AddScript("SistemaEncodingCorsoFase2", IDCorso)





            Return DettaglioEquiparazione
        End Function
        Public Shared Function CaricaDatiTesseramento(codiceFiscale As String) As DatiNuovaEquiparazione
            '  Dim litNumRichieste As Literal = DirectCast(ContentPlaceHolder1.FindControl("LitNumeroRichiesta"), Literal)


            Dim ds As DataSet

            Dim dataOggi As Date = Today.Date
            Dim DettaglioEquiparazione As New DatiNuovaEquiparazione

            Dim fmsP As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
            '  Dim ds As DataSet
            Dim risposta As String = ""
            Dim it As DateTime = DateTime.Now.Date.ToString("dd/MM/yyyy", New CultureInfo("it-IT"))
            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fmsP.SetLayout("webCheckCF")
            Dim RequestA = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("CodiceFiscale", codiceFiscale, Enumerations.SearchOption.equals)
            '  RequestA.AddSearchField("DataScadenza", it, Enumerations.SearchOption.lessOrEqualThan)



            ds = RequestA.Execute()


            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                For Each dr In ds.Tables("main").Rows

                    If it <= dr("Data Scadenza") Then


                        DettaglioEquiparazione.Cognome = Data.FixNull(dr("Cognome"))
                        DettaglioEquiparazione.Nome = Data.FixNull(dr("Nome"))
                        DettaglioEquiparazione.DataScadenza = Data.FixNull(dr("Data Scadenza"))
                        DettaglioEquiparazione.CodiceTessera = Data.FixNull(dr("Codice tessera"))


                    End If



                Next

            Else

            End If





            '    Request.AddScript("SistemaEncodingCorsoFase2", IDCorso)





            Return DettaglioEquiparazione
        End Function

        Public Shared Function NuovaEquiparazioneFolder(codiceEnteRichiedente As Integer, Equi_Sport_Interessato_ID As Integer,
                                                        Equi_Disciplina_Interessata_ID As Integer, Equi_Specialita_ID As Integer) As Integer


            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            Dim ds As DataSet
            Dim idrecord As Integer
            Dim idEquiparazioneM As Integer
            fmsP.SetLayout("webEquiparazioniMaster")
            Dim Request = fmsP.CreateNewRecordRequest()

            Request.AddField("CodiceEnteRichiedente", codiceEnteRichiedente)
            Request.AddField("CodiceStatus", "101")

            'sport inizio
            Request.AddField("Equi_Sport_Interessato_ID", Equi_Sport_Interessato_ID)

            'disciplina inizio
            Request.AddField("Equi_Disciplina_Interessata_ID", Equi_Disciplina_Interessata_ID)


            'specialità inizio
            Request.AddField("Equi_Specialita_ID", Equi_Specialita_ID)





            idrecord = Request.Execute()

            Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestP.AddSearchField("idrecord", idrecord, Enumerations.SearchOption.equals)

            ds = RequestP.Execute()
            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                For Each dr In ds.Tables("main").Rows

                    idEquiparazioneM = Data.FixNull(dr("IDEquiparazioneM"))

                Next


            End If


            Return idEquiparazioneM

        End Function

        Public Shared Function CercaIDRecordEquiparazioneM(IDEquiparazioneM As Integer) As Integer


            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            Dim ds As DataSet
            Dim idrecord As Integer


            fmsP.SetLayout("webEquiparazioniMaster")
            Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestP.AddSearchField("EquiparazioneM", IDEquiparazioneM, Enumerations.SearchOption.equals)

            ds = RequestP.Execute()
            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                For Each dr In ds.Tables("main").Rows

                    idrecord = Data.FixNull(dr("idRecord"))

                Next


            End If


            Return idrecord

        End Function
    End Class

    Public Class Corso

        Public Shared Function NomeZipCorsi(codR As String) As String
            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing
            Dim nomeZip As String = ""

            fms = Conn.Connect()

            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fms.SetLayout("webCorsiRichiesta")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("IDCorso", codR, Enumerations.SearchOption.equals)

            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows
                        If Not String.IsNullOrEmpty(Data.FixNull(dr("ZipMaster"))) Then
                            nomeZip = Data.FixNull(dr("ZipMasterContent"))
                        End If

                    Next
                End If



            Catch ex As Exception
                Return nomeZip
            End Try

            Return nomeZip


        End Function
        Public Shared Function GetIdRecordCorsi(codR As String) As Integer
            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing
            Dim idrecord As Integer = 0

            fms = Conn.Connect()

            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fms.SetLayout("webCorsiRichiesta")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("IDCorso", codR, Enumerations.SearchOption.equals)

            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows
                        If Not String.IsNullOrEmpty(Data.FixNull(dr("ZipMaster"))) Then
                            idrecord = Data.FixNull(dr("idRecord"))
                        End If

                    Next
                End If



            Catch ex As Exception
                Return idrecord
            End Try

            Return idrecord


        End Function

        Public Shared Function EsisteZipCorsi(codR As String) As Boolean
            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing
            Dim esiste As Boolean = False

            fms = Conn.Connect()

            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fms.SetLayout("webCorsiRichiesta")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("IDCorso", codR, Enumerations.SearchOption.equals)

            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows
                        If Not String.IsNullOrEmpty(Data.FixNull(dr("ZipMaster"))) Then
                            esiste = True
                        End If

                    Next
                End If



            Catch ex As Exception
                Return esiste
            End Try

            Return esiste


        End Function

        Public Shared Function PrendiNominativo(record_id As String) As String

            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            Dim ds As DataSet
            Dim ritorno As String = "noName"
            Dim nome As String
            Dim cognome As String
            Dim nominativo As String

            fmsP.SetLayout("webCorsisti")

            Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestP.AddSearchField("ID", record_id, Enumerations.SearchOption.equals)

            ds = RequestP.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

                For Each dr In ds.Tables("main").Rows

                    nominativo = Data.FixNull(dr("Cognome")) & "_" & Data.FixNull(dr("Nome"))

                    ritorno = nominativo

                Next

            Else

                ritorno = "noName"
            End If

            Return ritorno

        End Function


        Public Shared Function QuantiCorsisti(codR As String) As Boolean
            Dim fms As FMSAxml = AsiModel.Conn.Connect()
            Dim ds As DataSet = Nothing
            Dim esiste As Boolean = False

            fms.SetLayout("webCorsisti")

            Dim RequestPTot = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestPTot.AddSearchField("IDCorso", codR, Enumerations.SearchOption.equals)

            ds = RequestPTot.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 1 Then

                esiste = True
            Else


                esiste = False

            End If

            Return esiste

        End Function


        Public Shared Function ControllaQuanti(IDEnte As String, tipo As String)
            Dim vabene As Boolean = False
            Dim ds As DataSet
            Dim quanti As Integer = 0
            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            fmsP.SetLayout("webAlboEx")
            Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            ' RequestP.AddSearchField("pre_stato_web", "1")
            RequestP.AddSearchField("CodiceEnteAffiliante", IDEnte, Enumerations.SearchOption.equals)

            RequestP.AddSearchField("valido", tipo, Enumerations.SearchOption.equals)


            RequestP.AddSortField("Cognome", Enumerations.Sort.Ascend)

            ds = RequestP.Execute()
            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

                quanti = ds.Tables("main").Rows.Count

                If quanti > 0 Then
                    vabene = True
                Else
                    vabene = False


                End If


            End If

            Return vabene

        End Function

        Public Shared Function PrendiValoriNuovoCorso(IDCorso As String) As DatiNuovoCorso

            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing
            Dim DatiCorso As New DatiNuovoCorso

            fms = Conn.Connect()

            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fms.SetLayout("WebCorsiRichiesta")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("IDCorso", IDCorso, Enumerations.SearchOption.equals)

            '  Try
            ds = RequestA.Execute()


            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                For Each dr In ds.Tables("main").Rows

                    DatiCorso.CodiceEnteRichiedente = Data.FixNull(dr("Codice_Ente_Richiedente"))
                    DatiCorso.DescrizioneEnteRichiedente = Data.FixNull(dr("Descrizione_Ente_Richiedente"))
                    DatiCorso.IDCorso = Data.FixNull(dr("IDCorso"))
                    DatiCorso.DescrizioneStatus = Data.FixNull(dr("Descrizione_StatusWeb"))
                    DatiCorso.CodiceStatus = Data.FixNull(dr("Codice_Status"))
                    DatiCorso.TipoEnte = Data.FixNull(dr("Tipo_Ente"))
                    DatiCorso.IdRecord = Data.FixNull(dr("Id_record"))
                    DatiCorso.IndirizzoSvolgimento = Data.FixNull(dr("Indirizzo_Svolgimento"))
                    DatiCorso.LocalitaSvolgimento = Data.FixNull(dr("Comune_Svolgimento"))
                    DatiCorso.CapSvolgimento = Data.FixNull(dr("Cap_Svolgimento"))
                    DatiCorso.PRSvolgimento = Data.FixNull(dr("PR_Svolgimento"))
                    DatiCorso.RegioneSvolgimento = Data.FixNull(dr("Regione_Svolgimento"))
                    DatiCorso.SvolgimentoDataDa = Data.FixNull(dr("Svolgimento_Da_Data"))
                    DatiCorso.SvolgimentoDataA = Data.FixNull(dr("Svolgimento_A_Data"))
                    'DatiCorso.OraSvolgimentoDa = Data.FixNull(dr("Ora_Svolgimento_Inizio"))
                    'DatiCorso.OraSvolgimentoA = Data.FixNull(dr("Ora_Svolgimento_Fine"))
                    DatiCorso.TitoloCorso = Data.FixNull(dr("Titolo_Corso"))
                    DatiCorso.TotaleOre = Data.FixNull(dr("oreCorso"))
                    DatiCorso.DataEmissione = Data.FixNull(dr("Data_Emissione"))
                    DatiCorso.MostraMonteOreFormazione = Data.FixNullInteger(dr("MonteOreFormazioneFlag"))                        '  DatiCorso.StatusPrimaCaricamentoXL = Data.FixNull(dr("StatusPrimaCaricamentoXL"))


                Next



            End If



            'Catch ex As Exception

            'End Try

            Return DatiCorso

        End Function
    End Class

    Public Class QuantiCorsisti




        Public Class EsisteCodRAllegati

            'Public Shared Property Denominazione As String
            'Public Shared Property Codice As String
            'Public Shared Property TipoEnte As String




            Public Shared Function EsisteCodR(codR As String) As Boolean
                Dim fms As FMSAxml = Nothing
                Dim ds As DataSet = Nothing
                Dim esiste As Boolean = False

                fms = Conn.Connect()

                '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
                '     fmsB.SetDatabase(Database)
                fms.SetLayout("web_richiesta_allegati")
                Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
                RequestA.AddSearchField("Codice_Richiesta", codR, Enumerations.SearchOption.equals)

                Try
                    ds = RequestA.Execute()


                    If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                        'For Each dr In ds.Tables("main").Rows

                        '    Denominazione = Data.FixNull(dr("denominazione"))
                        '    TipoEnte = Data.FixNull(dr("tipo_ente"))
                        '    Codice = Data.FixNull(dr("codice"))
                        'Next

                        esiste = True

                    End If



                Catch ex As Exception
                    esiste = False
                End Try

                Return esiste

            End Function

        End Class



        'Public Shared Property Denominazione As String
        'Public Shared Property Codice As String
        'Public Shared Property TipoEnte As String




        Public Shared Function EsisteCodR(codR As String) As Boolean
            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing
            Dim esiste As Boolean = False

            fms = Conn.Connect()

            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fms.SetLayout("web_richiesta_allegati")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("Codice_Richiesta", codR, Enumerations.SearchOption.equals)

            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    'For Each dr In ds.Tables("main").Rows

                    '    Denominazione = Data.FixNull(dr("denominazione"))
                    '    TipoEnte = Data.FixNull(dr("tipo_ente"))
                    '    Codice = Data.FixNull(dr("codice"))
                    'Next

                    esiste = True

                End If



            Catch ex As Exception
                esiste = False
            End Try

            Return esiste

        End Function

    End Class

    Public Shared Property CodiceRichiesta As String
    Public Shared Property CodiceArticolo As String
    Public Shared Property NomeArticolo As String
    Public Shared Property IDCorso As String

    Public Class Equiparazioni
        Public Shared Function NomeZipRinnovi(codR As String) As String
            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing
            Dim nomeZip As String = ""

            fms = Conn.Connect()

            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fms.SetLayout("webEquiparazioniMaster")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("IDEquiparazioneM", codR, Enumerations.SearchOption.equals)

            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows
                        If Not String.IsNullOrEmpty(Data.FixNull(dr("ZipMaster"))) Then
                            nomeZip = Data.FixNull(dr("ZipMasterContent"))
                        End If

                    Next
                End If



            Catch ex As Exception
                Return nomeZip
            End Try

            Return nomeZip


        End Function
        Public Shared Function GetIdRecordEquiparazioni(codR As String) As Integer
            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing
            Dim idrecord As Integer = 0

            fms = Conn.Connect()

            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fms.SetLayout("webEquiparazioniMaster")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("IDEquiparazioneM", codR, Enumerations.SearchOption.equals)

            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows
                        If Not String.IsNullOrEmpty(Data.FixNull(dr("ZipMaster"))) Then
                            idrecord = Data.FixNull(dr("idRecord"))
                        End If

                    Next
                End If



            Catch ex As Exception
                Return idrecord
            End Try

            Return idrecord


        End Function

        Public Shared Function EsisteZipEquiparazioni(codR As String) As Boolean
            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing
            Dim esiste As Boolean = False

            fms = Conn.Connect()

            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fms.SetLayout("webEquiparazioniMaster")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("IDEquiparazioneM", codR, Enumerations.SearchOption.equals)

            Try
                ds = RequestA.Execute()


                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows
                        If Not String.IsNullOrEmpty(Data.FixNull(dr("ZipMaster"))) Then
                            esiste = True
                        End If

                    Next
                End If



            Catch ex As Exception
                Return esiste
            End Try

            Return esiste


        End Function
    End Class


    Public Class ContatoriEvasi

        Public Shared Function quantiDaValutare(codice As String) As Integer
            Dim ritorno As Integer = 0

            Dim ds As DataSet

            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            fmsP.SetLayout("webCorsiRichiesta")
            Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            ' RequestP.AddSearchField("pre_stato_web", "1")
            RequestP.AddSearchField("Settore_Approvazione_ID", codice, Enumerations.SearchOption.equals)




            ds = RequestP.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                Dim counter1 As Integer = 0
                For Each dr In ds.Tables("main").Rows



                    If Data.FixNull(dr("Codice_Status")) = "63" Then
                        counter1 += 1


                    End If

                Next
                If counter1 >= 1 Then
                    ritorno = counter1
                Else
                    ritorno = 0
                End If

            Else
                '  ritorno = counter1

            End If
            Return ritorno

        End Function

        Public Shared Function quantiValutati(codice As String) As Integer
            Dim ritorno As Integer = 0

            Dim ds As DataSet

            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            fmsP.SetLayout("webCorsiRichiesta")
            Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            ' RequestP.AddSearchField("pre_stato_web", "1")
            RequestP.AddSearchField("Settore_Approvazione_ID", codice, Enumerations.SearchOption.equals)
            'RequestP.AddSortField("Codice_Status", Enumerations.Sort.Ascend)
            'RequestP.AddSortField("IDCorso", Enumerations.Sort.Descend)



            ds = RequestP.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

                Dim counter1 As Integer = 0
                For Each dr In ds.Tables("main").Rows



                    If Data.FixNull(dr("Codice_Status")) = "64" Or Data.FixNull(dr("Codice_Status")) = "65" Then
                        counter1 += 1


                    End If

                Next
                If counter1 >= 1 Then
                    ritorno = counter1
                Else
                    ritorno = 0
                End If

            Else

                ' non si sono records
                ' ritorno = 0


            End If


            Return ritorno

        End Function


        Public Shared Function quantiCorsiEvasi(codice As String) As Integer

            Dim ritorno As Integer = 0

            Dim ds As DataSet

            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            fmsP.SetLayout("webCorsiRichiesta")
            Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            ' RequestP.AddSearchField("pre_stato_web", "1")
            RequestP.AddSearchField("Codice_Ente_Richiedente", codice, Enumerations.SearchOption.equals)
            RequestP.AddSearchField("Codice_Status", "84", Enumerations.SearchOption.equals)
            'RequestP.AddSortField("Codice_Status", Enumerations.Sort.Ascend)
            ' RequestP.AddSortField("IDCorso", Enumerations.Sort.Descend)



            ds = RequestP.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                Dim counter1 As Integer = 0
                For Each dr In ds.Tables("main").Rows

                    'If Data.FixNull(dr("Codice_Status")) = "84" Or Data.FixNull(dr("Codice_Status")) = "99" _
                    'Or Data.FixNull(dr("Codice_Status")) = "60" Then


                    'If Data.FixNull(dr("Codice_Status")) = "84" Then
                    counter1 += 1
                    'Else




                    ' End If

                Next
                If counter1 >= 1 Then
                    ritorno = counter1
                Else
                    ritorno = 0
                End If

            Else
                '  ritorno = counter1

            End If
            Return ritorno

        End Function


    End Class

    Public Class ContatoriAttivi

        Public Shared Function quantiCorsiAttivi(codice As String) As Integer

            Dim ritorno As Integer = 0

            Dim ds As DataSet

            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            fmsP.SetLayout("webCorsiRichiesta")
            Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            ' RequestP.AddSearchField("pre_stato_web", "1")
            RequestP.AddSearchField("Codice_Ente_Richiedente", codice, Enumerations.SearchOption.equals)
            RequestP.AddSearchField("Codice_Status", "51...85")
            'RequestP.AddSortField("Codice_Status", Enumerations.Sort.Ascend)
            ' RequestP.AddSortField("IDCorso", Enumerations.Sort.Descend)



            ds = RequestP.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                Dim counter1 As Integer = 0

                For Each dr In ds.Tables("main").Rows

                    If (Not Data.FixNull(dr("Codice_Status")) = "84" And Not Data.FixNull(dr("Codice_Status")) = "60") Then

                        counter1 += 1

                    End If
                Next
                If counter1 >= 1 Then
                    ritorno = counter1
                Else
                    ritorno = 0
                End If

            Else
                '  ritorno = counter1

            End If
            Return ritorno

        End Function
        Public Shared Function quantiCorsiRespinti(codice As String) As Integer

            Dim ritorno As Integer = 0

            Dim ds As DataSet

            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            fmsP.SetLayout("webCorsiRichiesta")
            Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            ' RequestP.AddSearchField("pre_stato_web", "1")
            RequestP.AddSearchField("Codice_Ente_Richiedente", codice, Enumerations.SearchOption.equals)
            RequestP.AddSearchField("Codice_Status", "60", Enumerations.SearchOption.equals)
            'RequestP.AddSortField("Codice_Status", Enumerations.Sort.Ascend)
            ' RequestP.AddSortField("IDCorso", Enumerations.Sort.Descend)



            ds = RequestP.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                Dim counter1 As Integer = 0

                For Each dr In ds.Tables("main").Rows


                    counter1 += 1







                Next
                If counter1 >= 1 Then
                    ritorno = counter1
                Else
                    ritorno = 0
                End If

            Else
                '  ritorno = counter1

            End If
            Return ritorno

        End Function

        Public Shared Function quanteEquiAttive(codice As String) As Integer

            Dim ritorno As Integer = 0

            Dim ds As DataSet

            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            fmsP.SetLayout("webEquiparazioniMaster")
            Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            ' RequestP.AddSearchField("pre_stato_web", "1")
            RequestP.AddSearchField("CodiceEnteRichiedente", codice, Enumerations.SearchOption.equals)
            RequestP.AddSearchField("CodiceStatus", "101...114.5")
            ' RequestP.AddSortField("IDCorso", Enumerations.Sort.Descend)



            ds = RequestP.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                Dim counter1 As Integer = 0
                For Each dr In ds.Tables("main").Rows






                    If dr("codiceStatus") <> 104 Then

                            counter1 += 1
                        End If







                    Next
                    If counter1 >= 1 Then
                    ritorno = counter1
                Else
                    ritorno = 0
                End If

            Else
                '  ritorno = counter1

            End If
            Return ritorno

        End Function


        Public Shared Function quantiRinnoviAttivi(codice As String) As Integer

            Dim ritorno As Integer = 0
            Dim counter1 As Integer = 0
            Dim ds As DataSet

            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            fmsP.SetLayout("webRinnoviMaster")
            Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            ' RequestP.AddSearchField("pre_stato_web", "1")
            RequestP.AddSearchField("CodiceEnteRichiedente", codice, Enumerations.SearchOption.equals)
            RequestP.AddSearchField("CodiceStatus", "1...159")
            'RequestP.AddSortField("CodiceStatus", Enumerations.Sort.Ascend)
            ' RequestP.AddSortField("IDCorso", Enumerations.Sort.Descend)



            ds = RequestP.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

                '    For Each dr In ds.Tables("main").Rows


                '    If Data.FixNull(dr("CodiceStatus")) = "151" Or Data.FixNull(dr("CodiceStatus")) = "152" _
                'Or Data.FixNull(dr("CodiceStatus")) = "153" _
                'Or Data.FixNull(dr("CodiceStatus")) = "154" Or Data.FixNull(dr("CodiceStatus")) = "155" _
                'Or Data.FixNull(dr("CodiceStatus")) = "156" Or Data.FixNull(dr("CodiceStatus")) = "156" _
                'Or Data.FixNull(dr("CodiceStatus")) = "157" _
                'Or Data.FixNull(dr("CodiceStatus")) = "158" Or Data.FixNull(dr("CodiceStatus")) = "159" Then

                counter1 = ds.Tables("main").Rows.Count

                '  Else


                '

                '   Next
                'If counter1 >= 1 Then
                '    ritorno = counter1
                'Else
                '    ritorno = 0
                'End If

            Else
                '  ritorno = counter1
                counter1 = 0
            End If
            Return counter1

        End Function
    End Class



    Public Class IndirizzoEA
            Public IndirizzoConsegnaEA As String
            Public ComuneEA As String
            Public CapEA As String
        Public ProvinciaEA As String
        Public TelefonoEA As String

    End Class

        Public Shared Function leggiIndirizzoSpedizioneEA(user As String, pass As String) As List(Of IndirizzoEA)
            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing

            '    Try
            fms = Conn.Connect()
        '    Catch ex As Exception

        '  End Try
        Dim indirizzo As New List(Of IndirizzoEA)

        '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
        '     fmsB.SetDatabase(Database)
        fms.SetLayout("web_enti")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("Web_User_Ente", user, Enumerations.SearchOption.equals)
            RequestA.AddSearchField("Web_Password_Ente", pass, Enumerations.SearchOption.equals)

        '  Try
        ds = RequestA.Execute()



        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
            For Each dr In ds.Tables("main").Rows
                indirizzo.Add(New IndirizzoEA With {
                                .IndirizzoConsegnaEA = Data.FixNull(dr("SpedizioneIndirizzo")),
                                .CapEA = Data.FixNull(dr("SpedizioneCap")),
                                .ComuneEA = Data.FixNull(dr("SpedizioneComune")),
                                .ProvinciaEA = Data.FixNull(dr("SpedizioneProvincia")),
                                .TelefonoEA = Data.FixNull(dr("telefono"))
                                                                                             })
            Next



            '


        End If



        '  Catch ex As Exception


        'd Try

        Return indirizzo

    End Function

End Class


