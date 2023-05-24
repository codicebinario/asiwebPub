Imports fmDotNet
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed
Imports System.Globalization
Imports AjaxControlToolkit

Public Class DashboardAlbo
    Inherits System.Web.UI.Page
    Dim deEnco As New Ed()
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



        Dim ris As String = Request.QueryString("ris")
        Dim showScript As String = ""
        Dim customizeScript As String = " 
            toastr.options = {
              'closeButton': true,
              'debug': false,
              'newestOnTop': false,
              'progressBar': false,
              'positionClass': 'toast-top-right',
              'preventDuplicates': true,   
              'onclick': null,
              'timeOut': 5000,
              'showDuration': 1000,
              'hideDuration': 1000,
              'extendedTimeOut': 1000,
              'showEasing': 'swing',
              'hideEasing': 'linear',
              'showMethod': 'fadeIn',
              'hideMethod': 'fadeOut'
        };
        "

        If Not Page.IsPostBack Then

            If Not Session("AnnullaREqui") Is Nothing Then


                Select Case Session("AnnullaREqui")
                    Case "OKTessereAttive"
                        showScript = "toastr.success('Albo tessere attive scaricato', 'ASI');"
                        Session("AnnullaREqui") = Nothing

                    Case "KOTessereAttive"
                        showScript = "toastr.error('Albo tessere attive vuoto', 'ASI');"
                        Session("AnnullaREqui") = Nothing
                End Select



                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showSuccess", customizeScript & showScript, True)
            End If
            If Not String.IsNullOrEmpty(ris) Then
                ' If Session("visto") = "ok" Then

                ris = deEnco.QueryStringDecode(ris)

                If ris = "ok" Then

                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Richiesta Equiparazione caricata nel sistema! ' ).set('resizable', true).resizeTo('20%', 200);", True)


                ElseIf ris = "ko" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Nessun rinnovo evaso con questo codice fiscale ' ).set('resizable', true).resizeTo('20%', 200);", True)

                ElseIf ris = "no" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Richiesta Equiparazione senza verifica tessera.<br />Procedere con una nuova richiesta ' ).set('resizable', true).resizeTo('20%', 200);", True)

                End If
                Session("visto") = Nothing
                '  End If
            End If


            If Session("stoCorsi") = "ok" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Ora non è più possibile aggiungere foto! ' ).set('resizable', true).resizeTo('20%', 200);", True)
                Session("stoCorsi") = Nothing
            End If



        End If
        If Not Page.IsPostBack Then








        End If
    End Sub

    Protected Sub lnkAttive_Click(sender As Object, e As EventArgs) Handles lnkAttive.Click
        '   deleteFile(Session("codice") & "_TessereAttive_" & Session("tokenZ") & ".xlsx")
        Dim filePath As String = Server.MapPath(ResolveUrl("~/file_storage_Excel/"))
        For Each foundFile As String In My.Computer.FileSystem.GetFiles(filePath)
            If foundFile.Contains(Session("codice")) Then
                Try
                    My.Computer.FileSystem.DeleteFile(foundFile, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)

                Catch ex As Exception

                End Try
            End If
        Next


        Dim vabene As Boolean = False

        vabene = AsiModel.Corso.ControllaQuanti(Session("codice"), "s")

        If vabene = True Then

            Response.Redirect("scaricaTessere.aspx?IDEnte=" & deEnco.QueryStringEncode(Session("codice")))
        Else
            lblAvviso.Visible = True
            Session("AnnullaREqui") = "KOTessereAttive"
            lblAvviso.Text = "Non ci sono tessere attive"

        End If


    End Sub
    Private Function deleteFile(nomecaricato As String) As Boolean
        Dim FileToDelete As String
        Dim ritorno As Boolean = False
        '    FileToDelete = "D:\Soft\Lisa\ASIWeb\ASIWeb\file_storage\" & nomecaricato
        FileToDelete = Server.MapPath("~/file_storage_Excel/" & nomecaricato)
        Try


            If System.IO.File.Exists(FileToDelete.ToString()) = True Then

                System.IO.File.Delete(FileToDelete.ToString())

                ritorno = True

            Else

                ritorno = False
            End If

        Catch ex As Exception

            ritorno = False
        End Try


        Return ritorno

    End Function


    Protected Sub lnkScadute_Click(sender As Object, e As EventArgs) Handles lnkScadute.Click
        Dim filePath As String = Server.MapPath(ResolveUrl("~/file_storage_Excel/"))
        For Each foundFile As String In My.Computer.FileSystem.GetFiles(filePath)
            If foundFile.Contains(Session("codice")) Then
                Try
                    My.Computer.FileSystem.DeleteFile(foundFile, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)

                Catch ex As Exception

                End Try
            End If
        Next
        Dim vabene As Boolean = False

        vabene = AsiModel.Corso.ControllaQuanti(Session("codice"), "n")

        If vabene = True Then
            Response.Redirect("scaricaTessereS.aspx?IDEnte=" & deEnco.QueryStringEncode(Session("codice")))
        Else
            lblAvviso.Visible = True

            lblAvviso.Text = "Non ci sono tessere scadute"

        End If


    End Sub

    'Protected Sub btnCheck_Click(sender As Object, e As EventArgs) Handles btnCheck.Click

    '    If Page.IsValid Then
    '        Dim ds As DataSet



    '        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
    '        fmsP.SetLayout("WebAlboEx")
    '        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
    '        ' RequestP.AddSearchField("pre_stato_web", "1")


    '        If rbCF.Checked = True Then
    '            RequestP.AddSearchField("Codice Fiscale", Trim(txtValore.Text), Enumerations.SearchOption.equals)
    '        ElseIf rbCognome.Checked = True Then

    '            RequestP.AddSearchField("Cognome", Trim(txtValore.Text), Enumerations.SearchOption.equals)
    '        End If



    '        RequestP.AddSearchField("CodiceEnteAffiliante", Session("codice"), Enumerations.SearchOption.equals)





    '        '    Try




    '        ds = RequestP.Execute()

    '            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

    '                'Dim counter As Integer = 0
    '                Dim counter1 As Integer = 0
    '                Dim totale As Decimal = 0
    '                Dim tessera As String
    '                Dim nominativo As String

    '                For Each dr In ds.Tables("main").Rows

    '                    phDash.Visible = True

    '                    If String.IsNullOrWhiteSpace(Data.FixNull(dr("tessera"))) Then
    '                        tessera = "..\img\noPdf.jpg"
    '                    Else
    '                        tessera = "https://93.63.195.98" & Data.FixNull(dr("tessera"))
    '                    End If

    '                    phDash.Controls.Add(New LiteralControl("<div class=""col-sm-10 mb-3 mb-md-0"">"))



    '                    'accordion card
    '                    phDash.Controls.Add(New LiteralControl("<div class=""card mb-2 shadow-sm rounded"">"))
    '                    'accordion heder
    '                    phDash.Controls.Add(New LiteralControl("<div class=""card-header"">"))

    '                    phDash.Controls.Add(New LiteralControl("<div Class=""container-fluid"">"))

    '                    ' inizio prima riga

    '                    phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))


    '                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-6 text-left"">"))

    '                    phDash.Controls.Add(New LiteralControl("Nominativo: <small>" & Data.FixNull(dr("Cognome")) & " " & Data.FixNull(dr("Nome")) & "</small><br />"))

    '                    phDash.Controls.Add(New LiteralControl("CF: <small>" & Data.FixNull(dr("Codice Fiscale")) & "</small><br />"))
    '                    phDash.Controls.Add(New LiteralControl("Data Scadenza: <small>" & Data.SonoDieci(Data.FixNull(dr("Scadenza"))) & "</small><br />"))
    '                    phDash.Controls.Add(New LiteralControl("Tessera ASI: <small>" & Data.FixNull(dr("Numero Tessera ASI")) & "</small><br />"))
    '                    phDash.Controls.Add(New LiteralControl("Codice Iscrizione: <small>" & Data.FixNull(dr("CODICE ISCRIZIONE")) & "</small><br />"))
    '                    phDash.Controls.Add(New LiteralControl("Email: <small>" & Data.FixNull(dr("Indirizzo mail")) & "</small><br />"))
    '                    phDash.Controls.Add(New LiteralControl("Comune Nascita: <small>" & Data.FixNull(dr("Comune di Nascita")) & "</small><br />"))
    '                    phDash.Controls.Add(New LiteralControl("Data Nascita: <small>" & Data.SonoDieci(Data.FixNull(dr("Data di Nascita"))) & "</small><br />"))


    '                    phDash.Controls.Add(New LiteralControl())

    '                    ' phDash.Controls.Add(New LiteralControl("</span>"))

    '                    phDash.Controls.Add(New LiteralControl("</div>"))


    '                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-6  text-left"">"))
    '                    phDash.Controls.Add(New LiteralControl("Dati Sportivi: <small>" & Data.FixNull(dr("Qualifica")) & "</small><br />"))
    '                    phDash.Controls.Add(New LiteralControl("Sport        : <small>" & Data.FixNull(dr("Sport")) & "</small><br />"))
    '                    phDash.Controls.Add(New LiteralControl("Disciplina   : <small>" & Data.FixNull(dr("Disciplina")) & "</small><br />"))
    '                    phDash.Controls.Add(New LiteralControl("Specialità   : <small>" & Data.FixNull(dr("Specialita")) & "</small><br />"))
    '                    phDash.Controls.Add(New LiteralControl("Livello      : <small>" & Data.FixNull(dr("Livello_Grado")) & "</small><br />"))
    '                If tessera = "..\img\noPdf.jpg" Then
    '                    '     phDash10.Controls.Add(New LiteralControl("<td><img src='" & tessera & "' height='70' width='70' alt='" & Data.FixNull(dr("Asi_Nome")) & " " & Data.FixNull(dr("Asi_Cognome")) & "'></td>"))


    '                Else
    '                    phDash.Controls.Add(New LiteralControl("<a class=""btn btn-success btn-sm btn-due btn-custom "" target=""_blank"" href='scaricaTesseraAlbo.aspx?record_ID=" & deEnco.QueryStringEncode(dr("idrecord")) & "&nomeFilePC=" _
    '                         & deEnco.QueryStringEncode(Data.FixNull(dr("TesseraNomeFile"))) & "&nominativo=" _
    '                         & deEnco.QueryStringEncode(Data.FixNull(dr("Cognome")) & "_" & Data.FixNull(dr("Nome"))) & "'><i class=""bi bi-person-badge""> </i>Scarica Tessera</a>"))


    '                End If


    '                phDash.Controls.Add(New LiteralControl("</div>"))

    '                    phDash.Controls.Add(New LiteralControl("</div>"))
    '                    'intermezzo
    '                    phDash.Controls.Add(New LiteralControl("<div class=""row"">"))

    '                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-12 text-left"">"))

    '                    phDash.Controls.Add(New LiteralControl("<hr>"))

    '                    phDash.Controls.Add(New LiteralControl("</div>"))



    '                    phDash.Controls.Add(New LiteralControl("</div>"))


    '                    phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))
    '                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-12 text-left"">"))

    '                    phDash.Controls.Add(New LiteralControl("Indirizzo: <small>" & Data.FixNull(dr("Indirizzo Residenza")) & " - " & Data.FixNull(dr("Comune di Residenza")) & " - " & Data.FixNull(dr("Cap Residenza")) & " - " & Data.FixNull(dr("Provincia")) & "</small><br />"))



    '                    phDash.Controls.Add(New LiteralControl())

    '                    phDash.Controls.Add(New LiteralControl("</span>"))

    '                    phDash.Controls.Add(New LiteralControl("</div>"))

    '                    phDash.Controls.Add(New LiteralControl("</div>"))


    '                    '   phDash.Controls.Add(New LiteralControl("</div>"))


    '                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4  text-left"">"))

    '                '      phDash.Controls.Add(New LiteralControl("</span><small>Status: </small><small " & Utility.statusColorTextCorsi(Data.FixNull(dr("Codice_Status"))) & ">" & Data.FixNull(dr("Descrizione_StatusWeb")) & "</small><br /><br />"))
    '                '  phDash10.Controls.Add(New LiteralControl("</span><small>Tessera: </small><small></small><br /><br />"))


    '                phDash.Controls.Add(New LiteralControl("</div>"))









    '                    counter1 += 1
    '                    phDash.Controls.Add(New LiteralControl("</div>"))

    '                    phDash.Controls.Add(New LiteralControl("</div>"))

    '                    phDash.Controls.Add(New LiteralControl("</div>"))
    '                    phDash.Controls.Add(New LiteralControl("</div>"))



    '                Next
    '            Else
    '                phDash.Visible = True



    '                phDash.Controls.Add(New LiteralControl("<div class=""col-sm-10 mb-3 mb-md-0"">"))



    '                'accordion card
    '                phDash.Controls.Add(New LiteralControl("<div class=""card mb-2 shadow-sm rounded"">"))
    '                'accordion heder
    '                phDash.Controls.Add(New LiteralControl("<div class=""card-header"">"))

    '                phDash.Controls.Add(New LiteralControl("<div Class=""container-fluid"">"))

    '                phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-12 text-left"">"))
    '                phDash.Controls.Add(New LiteralControl("<small>nessun risultato dalla ricerca</small><br />"))


    '                phDash.Controls.Add(New LiteralControl("</div>"))
    '                phDash.Controls.Add(New LiteralControl("</div>"))

    '                phDash.Controls.Add(New LiteralControl("</div>"))

    '                phDash.Controls.Add(New LiteralControl("</div>"))
    '                phDash.Controls.Add(New LiteralControl("</div>"))


    '            End If
    '        '    Catch ex As Exception
    '        'Session("procedi") = "KO"
    '        'Response.Redirect("DashboardEquiEvasi.aspx?ris=" & deEnco.QueryStringEncode("ko"))
    '        '   End Try


    '    End If


    'End Sub

    Protected Sub CustomValidator1_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles CustomValidator1.ServerValidate
        If rbCF.Checked = False And rbCognome.Checked = False Then
            args.IsValid = False
        Else
            args.IsValid = True
        End If
    End Sub

    Protected Sub lnkCheck_Click(sender As Object, e As EventArgs) Handles lnkCheck.Click
        If Page.IsValid Then
            Dim ds As DataSet



            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            fmsP.SetLayout("WebAlboEx")
            Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            ' RequestP.AddSearchField("pre_stato_web", "1")


            If rbCF.Checked = True Then
                RequestP.AddSearchField("Codice Fiscale", Trim(txtValore.Text), Enumerations.SearchOption.equals)
            ElseIf rbCognome.Checked = True Then

                RequestP.AddSearchField("Cognome", Trim(txtValore.Text), Enumerations.SearchOption.equals)
            End If



            RequestP.AddSearchField("CodiceEnteAffiliante", Session("codice"), Enumerations.SearchOption.equals)





            '    Try




            ds = RequestP.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

                'Dim counter As Integer = 0
                Dim counter1 As Integer = 0
                Dim totale As Decimal = 0
                Dim tessera As String
                Dim nominativo As String

                For Each dr In ds.Tables("main").Rows

                    phDash.Visible = True

                    If String.IsNullOrWhiteSpace(Data.FixNull(dr("tessera"))) Then
                        tessera = "..\img\noPdf.jpg"
                    Else
                        tessera = "https://93.63.195.98" & Data.FixNull(dr("tessera"))
                    End If

                    phDash.Controls.Add(New LiteralControl("<div class=""col-sm-10 mb-3 mb-md-0"">"))



                    'accordion card
                    phDash.Controls.Add(New LiteralControl("<div class=""card mb-2 shadow-sm rounded"">"))
                    'accordion heder
                    phDash.Controls.Add(New LiteralControl("<div class=""card-header"">"))

                    phDash.Controls.Add(New LiteralControl("<div Class=""container-fluid"">"))

                    ' inizio prima riga

                    phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))


                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-6 text-left"">"))

                    phDash.Controls.Add(New LiteralControl("Nominativo: <small>" & Data.FixNull(dr("Cognome")) & " " & Data.FixNull(dr("Nome")) & "</small><br />"))

                    phDash.Controls.Add(New LiteralControl("CF: <small>" & Data.FixNull(dr("Codice Fiscale")) & "</small><br />"))
                    phDash.Controls.Add(New LiteralControl("Data Scadenza: <small>" & Data.SonoDieci(Data.FixNull(dr("Scadenza"))) & "</small><br />"))
                    phDash.Controls.Add(New LiteralControl("Tessera ASI: <small>" & Data.FixNull(dr("Numero Tessera ASI")) & "</small><br />"))
                    phDash.Controls.Add(New LiteralControl("Codice Iscrizione: <small>" & Data.FixNull(dr("CODICE ISCRIZIONE")) & "</small><br />"))
                    phDash.Controls.Add(New LiteralControl("Email: <small>" & Data.FixNull(dr("Indirizzo mail")) & "</small><br />"))
                    phDash.Controls.Add(New LiteralControl("Comune Nascita: <small>" & Data.FixNull(dr("Comune di Nascita")) & "</small><br />"))
                    phDash.Controls.Add(New LiteralControl("Data Nascita: <small>" & Data.SonoDieci(Data.FixNull(dr("Data di Nascita"))) & "</small><br />"))


                    phDash.Controls.Add(New LiteralControl())

                    ' phDash.Controls.Add(New LiteralControl("</span>"))

                    phDash.Controls.Add(New LiteralControl("</div>"))


                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-6  text-left"">"))
                    phDash.Controls.Add(New LiteralControl("Dati Sportivi: <small>" & Data.FixNull(dr("Qualifica")) & "</small><br />"))
                    phDash.Controls.Add(New LiteralControl("Sport        : <small>" & Data.FixNull(dr("Sport")) & "</small><br />"))
                    phDash.Controls.Add(New LiteralControl("Disciplina   : <small>" & Data.FixNull(dr("Disciplina")) & "</small><br />"))
                    phDash.Controls.Add(New LiteralControl("Specialità   : <small>" & Data.FixNull(dr("Specialita")) & "</small><br />"))
                    phDash.Controls.Add(New LiteralControl("Livello      : <small>" & Data.FixNull(dr("Livello_Grado")) & "</small><br />"))
                    If tessera = "..\img\noPdf.jpg" Then
                        '     phDash10.Controls.Add(New LiteralControl("<td><img src='" & tessera & "' height='70' width='70' alt='" & Data.FixNull(dr("Asi_Nome")) & " " & Data.FixNull(dr("Asi_Cognome")) & "'></td>"))


                    Else
                        phDash.Controls.Add(New LiteralControl("<a class=""btn btn-success btn-sm btn-due btn-custom "" onclick=""showToast('tesserino');"" target=""_blank"" href='scaricaTesseraAlbo.aspx?record_ID=" & deEnco.QueryStringEncode(dr("idrecord")) & "&nomeFilePC=" _
                             & deEnco.QueryStringEncode(Data.FixNull(dr("TesseraNomeFile"))) & "&nominativo=" _
                             & deEnco.QueryStringEncode(Data.FixNull(dr("Cognome")) & "_" & Data.FixNull(dr("Nome"))) & "'><i class=""bi bi-person-badge""> </i>Scarica Tess. Tecnico</a>"))


                    End If


                    phDash.Controls.Add(New LiteralControl("</div>"))

                    phDash.Controls.Add(New LiteralControl("</div>"))
                    'intermezzo
                    phDash.Controls.Add(New LiteralControl("<div class=""row"">"))

                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-12 text-left"">"))

                    phDash.Controls.Add(New LiteralControl("<hr>"))

                    phDash.Controls.Add(New LiteralControl("</div>"))



                    phDash.Controls.Add(New LiteralControl("</div>"))


                    phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))
                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-12 text-left"">"))

                    phDash.Controls.Add(New LiteralControl("Indirizzo: <small>" & Data.FixNull(dr("Indirizzo Residenza")) & " - " & Data.FixNull(dr("Comune di Residenza")) & " - " & Data.FixNull(dr("Cap Residenza")) & " - " & Data.FixNull(dr("Provincia")) & "</small><br />"))



                    phDash.Controls.Add(New LiteralControl())

                    phDash.Controls.Add(New LiteralControl("</span>"))

                    phDash.Controls.Add(New LiteralControl("</div>"))

                    phDash.Controls.Add(New LiteralControl("</div>"))


                    '   phDash.Controls.Add(New LiteralControl("</div>"))


                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4  text-left"">"))

                    '      phDash.Controls.Add(New LiteralControl("</span><small>Status: </small><small " & Utility.statusColorTextCorsi(Data.FixNull(dr("Codice_Status"))) & ">" & Data.FixNull(dr("Descrizione_StatusWeb")) & "</small><br /><br />"))
                    '  phDash10.Controls.Add(New LiteralControl("</span><small>Tessera: </small><small></small><br /><br />"))


                    phDash.Controls.Add(New LiteralControl("</div>"))









                    counter1 += 1
                    phDash.Controls.Add(New LiteralControl("</div>"))

                    phDash.Controls.Add(New LiteralControl("</div>"))

                    phDash.Controls.Add(New LiteralControl("</div>"))
                    phDash.Controls.Add(New LiteralControl("</div>"))



                Next
            Else
                phDash.Visible = True



                phDash.Controls.Add(New LiteralControl("<div class=""col-sm-10 mb-3 mb-md-0"">"))



                'accordion card
                phDash.Controls.Add(New LiteralControl("<div class=""card mb-2 shadow-sm rounded"">"))
                'accordion heder
                phDash.Controls.Add(New LiteralControl("<div class=""card-header"">"))

                phDash.Controls.Add(New LiteralControl("<div Class=""container-fluid"">"))

                phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-12 text-left"">"))
                phDash.Controls.Add(New LiteralControl("<small>nessun risultato dalla ricerca</small><br />"))


                phDash.Controls.Add(New LiteralControl("</div>"))
                phDash.Controls.Add(New LiteralControl("</div>"))

                phDash.Controls.Add(New LiteralControl("</div>"))

                phDash.Controls.Add(New LiteralControl("</div>"))
                phDash.Controls.Add(New LiteralControl("</div>"))


            End If
            '    Catch ex As Exception
            'Session("procedi") = "KO"
            'Response.Redirect("DashboardEquiEvasi.aspx?ris=" & deEnco.QueryStringEncode("ko"))
            '   End Try


        End If
    End Sub
End Class