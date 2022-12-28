﻿Imports fmDotNet
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
            Try
                fmsb = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)

                fmsb.SetDatabase(Database)


            Catch ex As Exception
                '     Dim errore As String = ex.InnerException.ToString
                HttpContext.Current.Server.Transfer("login.aspx?err=dbc")
            End Try
            Return fmsb
        End Function






    End Class





    Public Class LogIn

        Public Shared Property Denominazione As String
        Public Shared Property Codice As String
        Public Shared Property TipoEnte As String
        Public Shared Property WebUserEnte As String

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
            '    Try
            fms = Conn.Connect()
            '    Catch ex As Exception

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
            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            ' Dim ds As DataSet


            fmsP.SetLayout("Log_Status_Richiesta")
            Dim Request = fmsP.CreateNewRecordRequest()
            Request.AddField("Codice_Richiesta", CodiceRichiesta)
            Request.AddField("User_Cambio_Status", User)

            Request.AddField("Status_ID", Status_ID)

            Request.AddField("tipo", tipo)


            Try
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

    ''' <summary>
    ''' controlla il codice fiscale per Equiparazione
    ''' </summary>
    ''' <param name="codiceFiscale"></param>
    ''' <returns></returns>
    Public Shared Function controllaCodiceFiscale(codiceFiscale As String, data As String)
        Dim fms As FMSAxml = Nothing
        Dim ds As DataSet = Nothing
        Dim ritorno As Boolean = False
        Dim dataScadenza As DateTime
        fms = Conn.Connect()
        Dim it As DateTime = DateTime.Now.Date.ToString("dd/MM/yyyy", New CultureInfo("it-IT"))

        '    it = SistemaData(it)

        '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
        '     fmsB.SetDatabase(Database)
        fms.SetLayout("webCheckCF")
        Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
        RequestA.AddSearchField("CodiceFiscale", codiceFiscale, Enumerations.SearchOption.equals)
        ' RequestA.AddSearchField("DataScadenza", it, Enumerations.SearchOption.lessOrEqualThan)


        '  Try

        ds = RequestA.Execute()


        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
            For Each dr In ds.Tables("main").Rows

                '   dataScadenza = dr("DataScadenza")

                'If CDate(it) <= CDate(dr("DataScadenza")) Then
                If it <= dr("Data Scadenza") Then



                    ritorno = True

                End If



            Next

        Else
            ritorno = False
            End If




        '  Catch ex As Exception
        '  ritorno = False
        ' End Try
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


        Catch ex As Exception

        End Try
        Return DatiCodiceFiscale
    End Function

    Public Shared Function getSiglaProvincia(provincia As String) As String
        Dim fms As FMSAxml = Nothing
        Dim ds As DataSet = Nothing
        Dim ritorno As String = ""



        fms = Conn.Connect()
        fms.SetLayout("webProvinceRegioni")
        Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
        RequestA.AddSearchField("Provincia", provincia, Enumerations.SearchOption.equals)
        'Try

        ds = RequestA.Execute()


            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                For Each dr In ds.Tables("main").Rows


                ritorno = dr("Sigla")





            Next

            Else
                ritorno = " "
            End If
        '  Catch ex As Exception
        'ritorno = " "
        'End Try
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
        Public trovato As Boolean
        Public PagamentoTotale As Decimal


    End Class

    Public Class DatiNuovoRinnovo

        Public IdRecord As String
        Public IDRinnovo As String
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

        Public Shared Function PrendiValoriNuovoRinnovoByCF(codiceFiscale As String, codiceEnte As String) As DatiNuovoRinnovo
            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing
            Dim DatiRinnovo As New DatiNuovoRinnovo
            Dim ritorno As Boolean = false
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
            fmsP.SetLayout("webCheckCFRinnovi")
            Dim RequestA = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("CodiceFiscale", codiceFiscale, Enumerations.SearchOption.equals)
            '  RequestA.AddSearchField("DataScadenza", it, Enumerations.SearchOption.lessOrEqualThan)



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
                        '    DatiEquiparazione.MostraMonteOreFormazione = Data.FixNull(dr("MonteOreFormazioneFlag"))
                    Next



                End If



            Catch ex As Exception

            End Try

            Return DatiEquiparazione

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




    End Class

    Public Class Corso

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
            'RequestP.AddSortField("Codice_Status", Enumerations.Sort.Ascend)
            ' RequestP.AddSortField("IDCorso", Enumerations.Sort.Descend)



            ds = RequestP.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                Dim counter1 As Integer = 0
                For Each dr In ds.Tables("main").Rows



                    If Data.FixNull(dr("Codice_Status")) = "84" Or Data.FixNull(dr("Codice_Status")) = "99" _
                    Or Data.FixNull(dr("Codice_Status")) = "60" Then
                        counter1 += 1
                    Else




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
            'RequestP.AddSortField("Codice_Status", Enumerations.Sort.Ascend)
            ' RequestP.AddSortField("IDCorso", Enumerations.Sort.Descend)



            ds = RequestP.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                Dim counter1 As Integer = 0
                For Each dr In ds.Tables("main").Rows



                    If Data.FixNull(dr("Codice_Status")) = "51" Or Data.FixNull(dr("Codice_Status")) = "54" _
                Or Data.FixNull(dr("Codice_Status")) = "57" Or Data.FixNull(dr("Codice_Status")) = "70" _
                Or Data.FixNull(dr("Codice_Status")) = "63" Or Data.FixNull(dr("Codice_Status")) = "64" _
                Or Data.FixNull(dr("Codice_Status")) = "65" Or Data.FixNull(dr("Codice_Status")) = "66" _
                Or Data.FixNull(dr("Codice_Status")) = "67" Or Data.FixNull(dr("Codice_Status")) = "68" _
                Or Data.FixNull(dr("Codice_Status")) = "69" Or Data.FixNull(dr("Codice_Status")) = "72" _
                Or Data.FixNull(dr("Codice_Status")) = "73" Or Data.FixNull(dr("Codice_Status")) = "83" _
                Or Data.FixNull(dr("Codice_Status")) = "75" Or Data.FixNull(dr("Codice_Status")) = "78" _
                Or Data.FixNull(dr("Codice_Status")) = "82" Or Data.FixNull(dr("Codice_Status")) = "85" Or Data.FixNull(dr("Codice_Status")) = "81" Then
                        counter1 += 1
                    Else




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

        Public Shared Function quanteEquiAttive(codice As String) As Integer

            Dim ritorno As Integer = 0

            Dim ds As DataSet

            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            fmsP.SetLayout("webEquiparazioniRichiesta")
            Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            ' RequestP.AddSearchField("pre_stato_web", "1")
            RequestP.AddSearchField("Codice_Ente_Richiedente", codice, Enumerations.SearchOption.equals)
            'RequestP.AddSortField("Codice_Status", Enumerations.Sort.Ascend)
            ' RequestP.AddSortField("IDCorso", Enumerations.Sort.Descend)



            ds = RequestP.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                Dim counter1 As Integer = 0
                For Each dr In ds.Tables("main").Rows


                    If Data.FixNull(dr("Codice_Status")) = "101" Or Data.FixNull(dr("Codice_Status")) = "102" _
                Or Data.FixNull(dr("Codice_Status")) = "103" Or Data.FixNull(dr("Codice_Status")) = "104" _
                Or Data.FixNull(dr("Codice_Status")) = "105" Or Data.FixNull(dr("Codice_Status")) = "106" _
                Or Data.FixNull(dr("Codice_Status")) = "107" Or Data.FixNull(dr("Codice_Status")) = "108" _
                Or Data.FixNull(dr("Codice_Status")) = "109" Or Data.FixNull(dr("Codice_Status")) = "110" _
                Or Data.FixNull(dr("Codice_Status")) = "111" _
                Or Data.FixNull(dr("Codice_Status")) = "112" Or Data.FixNull(dr("Codice_Status")) = "113" _
                Or Data.FixNull(dr("Codice_Status")) = "114" Then
                        counter1 += 1
                    Else

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

            Dim ds As DataSet

            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            fmsP.SetLayout("webRinnoviRichiesta")
            Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            ' RequestP.AddSearchField("pre_stato_web", "1")
            RequestP.AddSearchField("Codice_Ente_Richiedente", codice, Enumerations.SearchOption.equals)
            'RequestP.AddSortField("Codice_Status", Enumerations.Sort.Ascend)
            ' RequestP.AddSortField("IDCorso", Enumerations.Sort.Descend)



            ds = RequestP.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                Dim counter1 As Integer = 0
                For Each dr In ds.Tables("main").Rows


                    If Data.FixNull(dr("Codice_Status")) = "151" Or Data.FixNull(dr("Codice_Status")) = "152" _
                Or Data.FixNull(dr("Codice_Status")) = "153" _
                Or Data.FixNull(dr("Codice_Status")) = "154" Or Data.FixNull(dr("Codice_Status")) = "155" _
                Or Data.FixNull(dr("Codice_Status")) = "156" Or Data.FixNull(dr("Codice_Status")) = "156" _
                Or Data.FixNull(dr("Codice_Status")) = "157" _
                Or Data.FixNull(dr("Codice_Status")) = "158" Or Data.FixNull(dr("Codice_Status")) = "159" Then

                        counter1 += 1

                    Else


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
    End Class



    Public Class IndirizzoEA
            Public IndirizzoConsegnaEA As String
            Public ComuneEA As String
            Public CapEA As String
            Public ProvinciaEA As String

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
                                .ProvinciaEA = Data.FixNull(dr("SpedizioneProvincia"))
                                                                                             })
            Next



            '


        End If



        '  Catch ex As Exception


        'd Try

        Return indirizzo

    End Function

End Class


