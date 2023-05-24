Imports fmDotNet
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed
Public Class archivioEquiValutati2
    Inherits System.Web.UI.Page

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
        Dim deEnco As New Ed()

        Dim ris As String = Request.QueryString("ris")

        If Not Page.IsPostBack Then


            'If Not String.IsNullOrEmpty(ris) Then
            '    If Session("visto") = "ok" Then

            '        ris = deEnco.QueryStringDecode(ris)

            '        If ris = "ok" Then
            '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Richiesta Corso caricata nel sistema! ' ).set('resizable', true).resizeTo('20%', 200);", True)

            '        ElseIf ris = "ko" Then
            '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Richiesta Corso non caricata nel sistema ' ).set('resizable', true).resizeTo('20%', 200);", True)

            '        End If
            '        Session("visto") = Nothing
            '    End If
            'End If


            'If Session("stoCorsi") = "ok" Then
            '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Ora non è più possibile aggiungere foto! ' ).set('resizable', true).resizeTo('20%', 200);", True)
            '    Session("stoCorsi") = Nothing
            'End If



        End If
        If Not Page.IsPostBack Then


            Equiparazioni()





        End If
    End Sub
    Function DettaglioTipo(valore As String) As String
        Dim risultato As String
        If valore = "cF" Then
            risultato = " questo codice fiscale"
        ElseIf valore = "nR" Then
            risultato = " questo numero richiesta"
        Else
            risultato = " questo ente"
        End If
        Return risultato
    End Function
    Sub Equiparazioni()

        Dim ds As DataSet

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("webEquiparazioniRichiestaMolti")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        ' RequestP.AddSearchField("pre_stato_web", "1")
        RequestP.AddSearchField("Equi_Settore_Approvazione_ID", Session("codice"), Enumerations.SearchOption.equals)
        RequestP.AddSearchField("Valutata", "S", Enumerations.SearchOption.equals)


        ' RequestP.AddSortField("Codice_Status", Enumerations.Sort.Ascend)
        RequestP.AddSortField("IdEquiparazioneM", Enumerations.Sort.Descend)
        RequestP.AddSortField("IDRecord", Enumerations.Sort.Descend)



        ds = RequestP.Execute()

        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then




            phDash.Visible = True
            'Dim counter As Integer = 0
            Dim counter1 As Integer = 0
            'Dim totale As Decimal = 0
            Dim diploma As String
            Dim rompiStatus As Integer
            Dim cambiato As String
            For Each dr In ds.Tables("main").Rows

                If rompiStatus = Data.FixNull(dr("IDEquiparazioneM")) Then
                    cambiato = ""
                Else
                    cambiato = "ok"
                End If


                counter1 += 1




                Dim deEnco As New Ed()

                phDash.Controls.Add(New LiteralControl("<div class=""col-sm-10 mb-3 mb-md-0"">"))
                If cambiato = "ok" Then


                    phDash.Controls.Add(New LiteralControl("<div Class=""section-divider"">"))
                    phDash.Controls.Add(New LiteralControl("<span>Richiesta " & Data.FixNull(dr("IDEquiparazioneM")) & "</span>"))
                    phDash.Controls.Add(New LiteralControl("</div>"))
                End If




                'accordion card
                phDash.Controls.Add(New LiteralControl("<div class=""card mb-2 shadow-sm rounded"">"))
                'accordion heder
                phDash.Controls.Add(New LiteralControl("<div class=""card-header"">"))

                phDash.Controls.Add(New LiteralControl("<div Class=""container-fluid"">"))

                ' inizio prima riga

                phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))


                phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left"">"))

                phDash.Controls.Add(New LiteralControl("Equiparazione:  "))
                phDash.Controls.Add(New LiteralControl("<span  " & Utility.statusColorCorsi(Data.FixNull(dr("Codice_Status"))) & ">"))
                phDash.Controls.Add(New LiteralControl("<a name=" & Data.FixNull(dr("IDRecord")) & ">" & Data.FixNull(dr("IDRecord")) & "</a>"))
                phDash.Controls.Add(New LiteralControl())

                phDash.Controls.Add(New LiteralControl("</span><br />"))


                phDash.Controls.Add(New LiteralControl("Nominativo: <small>" & Data.FixNull(dr("Equi_Nome")) & " " & Data.FixNull(dr("Equi_Cognome")) & "</small><br />"))

                phDash.Controls.Add(New LiteralControl("CF: <small>" & Data.FixNull(dr("Equi_CodiceFiscale")) & "</small><br />"))
                phDash.Controls.Add(New LiteralControl("Data Scadenza: <small>" & Data.SonoDieci(Data.FixNull(dr("Equi_DataScadenza"))) & "</small><br />"))

                phDash.Controls.Add(New LiteralControl())

                ' phDash.Controls.Add(New LiteralControl("</span>"))

                phDash.Controls.Add(New LiteralControl("</div>"))


                phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-6  text-left"">"))

                phDash.Controls.Add(New LiteralControl("</span><small>Status: </small><small " & Utility.statusColorTextCorsi(Data.FixNull(dr("Codice_Status"))) & ">" & Data.FixNull(dr("Descrizione_StatusWeb")) & "</small>"))

                phDash.Controls.Add(New LiteralControl("</div>"))







                phDash.Controls.Add(New LiteralControl("</div>"))




                phDash.Controls.Add(New LiteralControl("<hr>"))


                phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))


                phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left"">"))


                'phDash.Controls.Add(New LiteralControl())

                'phDash.Controls.Add(New LiteralControl("</span><br />"))


                phDash.Controls.Add(New LiteralControl("Sport: <small>" & Data.FixNull(dr("Equi_Sport_Interessato")) & "</small><br />"))

                phDash.Controls.Add(New LiteralControl("Disciplina: <small>" & Data.FixNull(dr("Equi_Disciplina_Interessata")) & "</small><br />"))
                phDash.Controls.Add(New LiteralControl("Specialità: <small>" & Data.FixNull(dr("Equi_Specialita")) & "</small><br />"))
                phDash.Controls.Add(New LiteralControl("Livello: <small>" & Data.FixNull(dr("Equi_Livello")) & "</small><br />"))
                phDash.Controls.Add(New LiteralControl("Qualifica da Rilasciare: <small>" & Data.FixNull(dr("Equi_Qualifica_Tecnica_Da_Rilasciare")) & "</small><br />"))
                phDash.Controls.Add(New LiteralControl("Qualifica DT:  "))

                phDash.Controls.Add(New LiteralControl("<small>" & Data.FixNull(dr("Dicitura_Qualifica_DT")) & "</small><br />"))

                If Not String.IsNullOrWhiteSpace(Data.FixNull(dr("NoteValutazioneSettore"))) Then
                    phDash.Controls.Add(New LiteralControl("Note da Settore:  "))
                    phDash.Controls.Add(New LiteralControl("<small>" & Data.FixNull(dr("NoteValutazioneSettore")) & "</small><br />"))


                End If

                phDash.Controls.Add(New LiteralControl("</div>"))






                phDash.Controls.Add(New LiteralControl("</div>"))





                counter1 += 1
                phDash.Controls.Add(New LiteralControl("</div>"))

                phDash.Controls.Add(New LiteralControl("</div>"))

                phDash.Controls.Add(New LiteralControl("</div>"))
                phDash.Controls.Add(New LiteralControl("</div>"))
                rompiStatus = Data.FixNull(dr("IDEquiparazioneM"))

            Next

        End If

    End Sub
    Protected Sub lnkLast10_Click(sender As Object, e As EventArgs) Handles lnkLast10.Click
        Equiparazioni()
    End Sub

    Protected Sub lnkCheck_Click(sender As Object, e As EventArgs) Handles lnkCheck.Click

        If Page.IsValid Then
            Dim codiceFiscaleInserito As String
            Dim numeroRichiestaInserita As Integer
            Dim tipoInserito As String
            If Integer.TryParse(Trim(txtCodiceFiscale.Text), vbNull) Then
                numeroRichiestaInserita = Trim(txtCodiceFiscale.Text)
                tipoInserito = "nR"
            Else
                tipoInserito = "cF"
                codiceFiscaleInserito = Trim(txtCodiceFiscale.Text)
            End If

            Dim ds As DataSet

            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            fmsP.SetLayout("webEquiparazioniRichiestaMolti")
            Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            ' RequestP.AddSearchField("pre_stato_web", "1")
            RequestP.AddSearchField("Equi_Settore_Approvazione_ID", Session("codice"), Enumerations.SearchOption.equals)
            RequestP.AddSearchField("Valutata", "S", Enumerations.SearchOption.equals)
            If tipoInserito = "cF" Then
                RequestP.AddSearchField("Equi_CodiceFiscale", Trim(txtCodiceFiscale.Text), Enumerations.SearchOption.equals)

            Else
                RequestP.AddSearchField("IDEquiparazioneM", Trim(txtCodiceFiscale.Text), Enumerations.SearchOption.equals)

            End If

            '   RequestP.AddSortField("Codice_Status", Enumerations.Sort.Ascend)
            RequestP.AddSortField("IdEquiparazioneM", Enumerations.Sort.Descend)
            RequestP.AddSortField("IDRecord", Enumerations.Sort.Descend)

            ds = RequestP.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then




                phDash10.Visible = True
                'Dim counter As Integer = 0
                Dim counter1 As Integer = 0
                'Dim totale As Decimal = 0
                Dim diploma As String
                Dim rompiStatus As Integer
                Dim cambiato As String
                For Each dr In ds.Tables("main").Rows

                    If rompiStatus = Data.FixNull(dr("IDEquiparazioneM")) Then
                        cambiato = ""
                    Else
                        cambiato = "ok"
                    End If


                    counter1 += 1




                    Dim deEnco As New Ed()

                    phDash10.Controls.Add(New LiteralControl("<div class=""col-sm-10 mb-3 mb-md-0"">"))
                    If cambiato = "ok" Then


                        phDash10.Controls.Add(New LiteralControl("<div Class=""section-divider"">"))
                        phDash10.Controls.Add(New LiteralControl("<span>Richiesta " & Data.FixNull(dr("IDEquiparazioneM")) & "</span>"))
                        phDash10.Controls.Add(New LiteralControl("</div>"))
                    End If




                    'accordion card
                    phDash10.Controls.Add(New LiteralControl("<div class=""card mb-2 shadow-sm rounded"">"))
                    'accordion heder
                    phDash10.Controls.Add(New LiteralControl("<div class=""card-header"">"))

                    phDash10.Controls.Add(New LiteralControl("<div Class=""container-fluid"">"))

                    ' inizio prima riga

                    phDash10.Controls.Add(New LiteralControl("<div Class=""row"">"))


                    phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left"">"))

                    phDash10.Controls.Add(New LiteralControl("Equiparazione:  "))
                    phDash10.Controls.Add(New LiteralControl("<span  " & Utility.statusColorCorsi(Data.FixNull(dr("Codice_Status"))) & ">"))
                    phDash10.Controls.Add(New LiteralControl("<a name=" & Data.FixNull(dr("IDRecord")) & ">" & Data.FixNull(dr("IDRecord")) & "</a>"))
                    phDash10.Controls.Add(New LiteralControl())

                    phDash10.Controls.Add(New LiteralControl("</span><br />"))


                    phDash10.Controls.Add(New LiteralControl("Nominativo: <small>" & Data.FixNull(dr("Equi_Nome")) & " " & Data.FixNull(dr("Equi_Cognome")) & "</small><br />"))

                    phDash10.Controls.Add(New LiteralControl("CF: <small>" & Data.FixNull(dr("Equi_CodiceFiscale")) & "</small><br />"))
                    phDash10.Controls.Add(New LiteralControl("Data Scadenza: <small>" & Data.SonoDieci(Data.FixNull(dr("Equi_DataScadenza"))) & "</small><br />"))

                    phDash10.Controls.Add(New LiteralControl())

                    ' phDash10.Controls.Add(New LiteralControl("</span>"))

                    phDash10.Controls.Add(New LiteralControl("</div>"))


                    phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-6  text-left"">"))

                    phDash10.Controls.Add(New LiteralControl("</span><small>Status: </small><small " & Utility.statusColorTextCorsi(Data.FixNull(dr("Codice_Status"))) & ">" & Data.FixNull(dr("Descrizione_StatusWeb")) & "</small>"))

                    phDash10.Controls.Add(New LiteralControl("</div>"))







                    phDash10.Controls.Add(New LiteralControl("</div>"))




                    phDash10.Controls.Add(New LiteralControl("<hr>"))


                    phDash10.Controls.Add(New LiteralControl("<div Class=""row"">"))


                    phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left"">"))


                    'phDash10.Controls.Add(New LiteralControl())

                    'phDash10.Controls.Add(New LiteralControl("</span><br />"))


                    phDash10.Controls.Add(New LiteralControl("Sport: <small>" & Data.FixNull(dr("Equi_Sport_Interessato")) & "</small><br />"))

                    phDash10.Controls.Add(New LiteralControl("Disciplina: <small>" & Data.FixNull(dr("Equi_Disciplina_Interessata")) & "</small><br />"))
                    phDash10.Controls.Add(New LiteralControl("Specialità: <small>" & Data.FixNull(dr("Equi_Specialita")) & "</small><br />"))
                    phDash10.Controls.Add(New LiteralControl("Livello: <small>" & Data.FixNull(dr("Equi_Livello")) & "</small><br />"))
                    phDash10.Controls.Add(New LiteralControl("Qualifica da Rilasciare: <small>" & Data.FixNull(dr("Equi_Qualifica_Tecnica_Da_Rilasciare")) & "</small><br />"))
                    phDash10.Controls.Add(New LiteralControl("Qualifica DT:  "))

                    phDash10.Controls.Add(New LiteralControl("<small>" & Data.FixNull(dr("Dicitura_Qualifica_DT")) & "</small><br />"))

                    If Not String.IsNullOrWhiteSpace(Data.FixNull(dr("NoteValutazioneSettore"))) Then
                        phDash10.Controls.Add(New LiteralControl("Note da Settore:  "))
                        phDash10.Controls.Add(New LiteralControl("<small>" & Data.FixNull(dr("NoteValutazioneSettore")) & "</small><br />"))


                    End If

                    phDash10.Controls.Add(New LiteralControl("</div>"))






                    phDash10.Controls.Add(New LiteralControl("</div>"))





                    counter1 += 1
                    phDash10.Controls.Add(New LiteralControl("</div>"))

                    phDash10.Controls.Add(New LiteralControl("</div>"))

                    phDash10.Controls.Add(New LiteralControl("</div>"))
                    phDash10.Controls.Add(New LiteralControl("</div>"))
                    rompiStatus = Data.FixNull(dr("IDEquiparazioneM"))

                Next

            End If



        End If
    End Sub
End Class