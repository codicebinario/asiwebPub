Imports fmDotNet
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed
Imports System.Globalization
Imports System.Net
Imports System.Security.Policy

Public Class DashboardEquiEvasi2
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
        Dim tipo As String = Request.QueryString("tipo")
        If Not Page.IsPostBack Then


            If Not String.IsNullOrEmpty(ris) Then
                '  If Session("visto") = "ok" Then

                ris = deEnco.QueryStringDecode(ris)

                    If ris = "ok" Then
                        If Session("equiparazioneaggiunta") = "OK" Then
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Richiesta Equiparazione caricata nel sistema! ' ).set('resizable', true).resizeTo('20%', 200);", True)
                            Session("equiparazioneaggiunta") = Nothing
                        End If
                    ElseIf ris = "ko" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Nessuna equiparazione evasa con" & DettaglioTipo(tipo) & " ' ).set('resizable', true).resizeTo('20%', 200);", True)
                        Session("equiparazioneaggiunta") = Nothing

                    ElseIf ris = "no" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Richiesta Equiparazione senza verifica tessera.<br />Procedere con una nuova richiesta ' ).set('resizable', true).resizeTo('20%', 200);", True)
                        Session("equiparazioneaggiunta") = Nothing
                    End If
                    Session("visto") = Nothing
                ' End If
            End If


            If Session("stoCorsi") = "ok" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Ora non è più possibile aggiungere foto! ' ).set('resizable', true).resizeTo('20%', 200);", True)
                Session("stoCorsi") = Nothing
            End If



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
        RequestP.AddSearchField("Codice_Ente_Richiedente", Session("codice"), Enumerations.SearchOption.equals)
        RequestP.AddSearchField("Codice_Status", "115")
        ' RequestP.AddSearchField("Codice_Status", "115", Enumerations.SearchOption.equals)
        '  RequestP.AddSearchField("Codice_Status", "119", Enumerations.SearchOption.equals)
        RequestP.SetMax(10)

        RequestP.AddSortField("Codice_Status", Enumerations.Sort.Descend)




        ds = RequestP.Execute()

        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

            'Dim counter As Integer = 0
            Dim counter1 As Integer = 0
            Dim totale As Decimal = 0

            Dim tessera As String
            Dim nominativo As String
            Dim diploma As String
            Dim rompiStatus As Integer
            Dim cambiato As String
            For Each dr In ds.Tables("main").Rows

                If rompiStatus = Data.FixNull(dr("IDEquiparazioneM")) Then
                    cambiato = ""
                Else
                    cambiato = "ok"
                End If

                If String.IsNullOrWhiteSpace(Data.FixNull(dr("DiplomaAsiText"))) Then
                    diploma = "..\img\noPdf.jpg"
                Else
                    diploma = "https://93.63.195.98" & Data.FixNull(dr("DiplomaAsiText"))
                End If

                If String.IsNullOrWhiteSpace(Data.FixNull(dr("TesseraEquiparazioneText"))) Then
                    tessera = "..\img\noPdf.jpg"
                Else
                    tessera = "https://93.63.195.98" & Data.FixNull(dr("TesseraEquiparazioneText"))
                End If


                Dim VediDocumentazione As New LinkButton
                VediDocumentazione.CausesValidation = False
                VediDocumentazione.ID = "VediDoc_" & counter1
                VediDocumentazione.Attributes.Add("runat", "server")
                VediDocumentazione.Text = "<i class=""bi bi-file-earmark-pdf""> </i>Documentazione Presentata"
                VediDocumentazione.PostBackUrl = "vediDocumentazione2.aspx?codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("IDEquiparazioneM")))) & "&record_ID=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(dr("idrecord")))
                VediDocumentazione.CssClass = "btn btn-success btn-sm btn-nove btn-custom mb-2"



                If Data.FixNull(dr("Codice_Status")) = "115" Then
                    VediDocumentazione.Visible = True
                Else
                    VediDocumentazione.Visible = False
                End If

                phDash10.Visible = True

                phDash10.Controls.Add(New LiteralControl("<div class=""col-sm-12 mb-3 mb-md-0"">"))
                If cambiato = "ok" Then


                    phDash10.Controls.Add(New LiteralControl("<div Class=""section-divider"">"))
                    phDash10.Controls.Add(New LiteralControl("<span>Richiesta " & Data.FixNull(dr("IDEquiparazioneM")) & "</span>"))
                    phDash10.Controls.Add(New LiteralControl("</div>"))
                End If





                phDash10.Controls.Add(New LiteralControl("</div>"))

                phDash10.Controls.Add(New LiteralControl("<div class=""col-sm-12 mb-3 mb-md-0"">"))
                'accordion card
                phDash10.Controls.Add(New LiteralControl("<div class=""card mb-2 shadow-sm rounded"">"))
                'accordion heder
                phDash10.Controls.Add(New LiteralControl("<div class=""card-header"">"))
                phDash10.Controls.Add(New LiteralControl("<div Class=""container-fluid"">"))
                ' inizio prima riga
                phDash10.Controls.Add(New LiteralControl("<div Class=""row"">"))
                phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left moltopiccolo"">"))
                phDash10.Controls.Add(New LiteralControl("Codice Richiesta: <small><b>" & Data.FixNull(dr("IDEquiparazioneM")) & "</b></small><br />"))
                phDash10.Controls.Add(New LiteralControl("Nominativo: <small>" & Data.FixNull(dr("Equi_Nome")) & " " & Data.FixNull(dr("Equi_Cognome")) & "</small><br />"))
                phDash10.Controls.Add(New LiteralControl("CF: <small>" & Data.FixNull(dr("Equi_CodiceFiscale")) & "</small><br />"))
                phDash10.Controls.Add(New LiteralControl("Tessera Ass.: <small>" & Data.FixNull(dr("Equi_NumeroTessera")) & "</small><br />"))
                phDash10.Controls.Add(New LiteralControl("Data Scadenza: <small>" & SonoDieci(Data.FixNull(dr("Equi_DataScadenza"))) & "</small><br />"))
                phDash10.Controls.Add(New LiteralControl())
                phDash10.Controls.Add(New LiteralControl("</div>"))
                phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-4  text-left moltopiccolo"">"))
                phDash10.Controls.Add(New LiteralControl("</span><small>Status: </small><small " & Utility.statusColorTextCorsi(Data.FixNull(dr("Codice_Status"))) & ">" & Data.FixNull(dr("Descrizione_StatusWeb")) & "</small>"))
                phDash10.Controls.Add(New LiteralControl("</div>"))
                phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-right"">"))


                phDash10.Controls.Add(VediDocumentazione)



                If tessera = "..\img\noPdf.jpg" Then
                    '     phDash10.Controls.Add(New LiteralControl("<td><img src='" & tessera & "' height='70' width='70' alt='" & Data.FixNull(dr("Asi_Nome")) & " " & Data.FixNull(dr("Asi_Cognome")) & "'></td>"))


                Else
                    phDash10.Controls.Add(New LiteralControl("<a class=""btn btn-success btn-sm btn-due btn-custom mb-2 "" target=""_blank"" href='scaricaTesseraEquiparazioneN2.aspx?record_ID=" & deEnco.QueryStringEncode(dr("idrecord")) & "&nomeFilePC=" _
& deEnco.QueryStringEncode(Data.FixNull(dr("TesseraEquiparazioneText"))) & "&nominativo=" _
                             & deEnco.QueryStringEncode(Data.FixNull(dr("Equi_Cognome")) & "_" & Data.FixNull(dr("Equi_Nome"))) & "'><i class=""bi bi-person-badge""> </i>Scarica Tess. Tecnico</a>"))
                End If
                If diploma = "..\img\noPdf.jpg" Then
                    '     phDash10.Controls.Add(New LiteralControl("<td><img src='" & tessera & "' height='70' width='70' alt='" & Data.FixNull(dr("Asi_Nome")) & " " & Data.FixNull(dr("Asi_Cognome")) & "'></td>"))


                Else
                    If Data.FixNull(dr("Equi_StampaDiploma")) = "no" Then
                    Else
                        If Data.FixNull(dr("Equi_StampaDiploma")) = "no" Then

                        Else
                            phDash10.Controls.Add(New LiteralControl("<a class=""btn btn-success btn-sm btn-due btn-custom mb-2"" target=""_blank"" href='scaricaDiplomaEquiparazioneN2.aspx?record_ID=" & deEnco.QueryStringEncode(dr("idrecord")) & "&nomeFilePC=" _
& deEnco.QueryStringEncode(Data.FixNull(dr("DiplomaAsiText"))) & "&nominativo=" _
                             & deEnco.QueryStringEncode(Data.FixNull(dr("Equi_Cognome")) & "_" & Data.FixNull(dr("Equi_Nome"))) & "'><i class=""bi bi-person-badge""> </i>Scarica Diploma</a>"))
                        End If
                    End If
                End If


                phDash10.Controls.Add(New LiteralControl("</div>"))
                phDash10.Controls.Add(New LiteralControl("</div>"))
                'phDash10.Controls.Add(New LiteralControl("</div>"))
                'phDash10.Controls.Add(New LiteralControl("</div>"))

                If Not String.IsNullOrWhiteSpace(Data.FixNull(dr("Equi_NoteAnnullamentoEquiparazione"))) Then

                    phDash10.Controls.Add(New LiteralControl("<div Class=""row"">"))
                    phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-12 text-left"">"))
                    phDash10.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Note Annullamento: <span>"))
                    phDash10.Controls.Add(New LiteralControl("<small>" & Data.FixNull(dr("Equi_NoteAnnullamentoEquiparazione"))))
                    phDash10.Controls.Add(New LiteralControl("</small></h6></span></div>"))
                    phDash10.Controls.Add(New LiteralControl("</div>"))

                End If



                phDash10.Controls.Add(New LiteralControl("<hr>"))
                phDash10.Controls.Add(New LiteralControl("<div Class=""row"">"))
                phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left moltopiccolo"">"))
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


                '  End If

                rompiStatus = Data.FixNull(dr("IDEquiparazioneM"))
            Next



        End If





    End Sub












    Function SonoDieci(valore As String) As String
        Dim risultato As String = ""

        If Len(valore) >= 10 Then


            risultato = Left(valore, 10)


            risultato = Left(valore, 10)



        Else

            risultato = ""

        End If




        Return risultato


    End Function



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
            If tipoInserito = "cF" Then
                RequestP.AddSearchField("Equi_CodiceFiscale", Trim(txtCodiceFiscale.Text), Enumerations.SearchOption.equals)

            Else
                RequestP.AddSearchField("IDEquiparazioneM", Trim(txtCodiceFiscale.Text), Enumerations.SearchOption.equals)

            End If



            RequestP.AddSearchField("Codice_Ente_Richiedente", Session("codice"), Enumerations.SearchOption.equals)
            RequestP.AddSearchField("Codice_Status", "115")




            Try


                ds = RequestP.Execute()

                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

                    'Dim counter As Integer = 0
                    Dim counter1 As Integer = 0
                    Dim totale As Decimal = 0
                    Dim tessera As String
                    Dim nominativo As String
                    Dim diploma As String
                    For Each dr In ds.Tables("main").Rows
                        If String.IsNullOrWhiteSpace(Data.FixNull(dr("DiplomaAsiText"))) Then
                            diploma = "..\img\noPdf.jpg"
                        Else
                            diploma = "https://93.63.195.98" & Data.FixNull(dr("DiplomaAsiText"))
                        End If

                        If String.IsNullOrWhiteSpace(Data.FixNull(dr("TesseraEquiparazioneText"))) Then
                            tessera = "..\img\noPdf.jpg"
                        Else
                            tessera = "https://93.63.195.98" & Data.FixNull(dr("TesseraEquiparazioneText"))
                        End If

                        Dim VediDocumentazione As New LinkButton
                        VediDocumentazione.CausesValidation = False
                        VediDocumentazione.ID = "VediDoc_" & counter1
                        VediDocumentazione.Attributes.Add("runat", "server")
                        VediDocumentazione.Text = "<i class=""bi bi-file-earmark-pdf""> </i>Documentazione Presentata"
                        VediDocumentazione.PostBackUrl = "vediDocumentazione2.aspx?codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("IDEquiparazioneM")))) & "&record_ID=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(dr("idrecord")))
                        VediDocumentazione.CssClass = "btn btn-success btn-sm btn-nove btn-custom mb-2"
                        '  Annulla.OnClientClick = "return confirm(""ciappa"");"
                        'VediDocumentazione.Attributes.Add("OnClick", "if(!myConfirm())return false;")
                        '   StopFoto.Attributes.Add("OnClick", "if(!alertify.confirm)return false;")



                        If Data.FixNull(dr("Codice_Status")) = "115" Then
                            VediDocumentazione.Visible = True
                        Else
                            VediDocumentazione.Visible = False
                        End If



                        '     If Data.FixNull(dr("Codice_Status")) = "115" Or Data.FixNull(dr("Codice_Status")) = "119" Then
                        'If counter1 <= 10 Then






                        phDash.Visible = True



                        phDash.Controls.Add(New LiteralControl("<div class=""col-sm-12 mb-3 mb-md-0"">"))



                        'accordion card
                        phDash.Controls.Add(New LiteralControl("<div class=""card mb-2 shadow-sm rounded"">"))
                        'accordion heder
                        phDash.Controls.Add(New LiteralControl("<div class=""card-header"">"))

                        phDash.Controls.Add(New LiteralControl("<div Class=""container-fluid"">"))

                        ' inizio prima riga

                        phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))


                        phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left moltopiccolo"">"))
                        phDash.Controls.Add(New LiteralControl("Codice Richiesta: <small><b>" & Data.FixNull(dr("IDEquiparazioneM")) & "</b></small><br />"))
                        'phDash.Controls.Add(New LiteralControl("Equiparazione:  "))
                        'phDash.Controls.Add(New LiteralControl("<span  " & Utility.statusColorCorsi(Data.FixNull(dr("Codice_Status"))) & ">"))
                        'phDash.Controls.Add(New LiteralControl("<a name=" & Data.FixNull(dr("IDEquiparazioneM")) & ">" & Data.FixNull(dr("IDEquiparazioneM")) & "</a>"))
                        'phDash.Controls.Add(New LiteralControl())

                        'phDash.Controls.Add(New LiteralControl("</span><br />"))


                        phDash.Controls.Add(New LiteralControl("Nominativo: <small>" & Data.FixNull(dr("Equi_Nome")) & " " & Data.FixNull(dr("Equi_Cognome")) & "</small><br />"))

                        phDash.Controls.Add(New LiteralControl("CF: <small>" & Data.FixNull(dr("Equi_CodiceFiscale")) & "</small><br />"))
                        phDash.Controls.Add(New LiteralControl("Tessera Ass.: <small>" & Data.FixNull(dr("Equi_NumeroTessera")) & "</small><br />"))
                        phDash.Controls.Add(New LiteralControl("Data Scadenza: <small>" & SonoDieci(Data.FixNull(dr("Equi_DataScadenza"))) & "</small><br />"))

                        phDash.Controls.Add(New LiteralControl())

                        ' phDash.Controls.Add(New LiteralControl("</span>"))

                        phDash.Controls.Add(New LiteralControl("</div>"))


                        phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4  text-left moltopiccolo"">"))

                        phDash.Controls.Add(New LiteralControl("</span><small>Status: </small><small " & Utility.statusColorTextCorsi(Data.FixNull(dr("Codice_Status"))) & ">" & Data.FixNull(dr("Descrizione_StatusWeb")) & "</small>"))

                        phDash.Controls.Add(New LiteralControl("</div>"))


                        phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-right"">"))

                        'phDash.Controls.Add(AnnPrimaFase)
                        'phDash.Controls.Add(Ann)
                        'phDash.Controls.Add(btnFase2)
                        'phDash.Controls.Add(btnFase3)
                        'phDash.Controls.Add(btnFase4)
                        phDash.Controls.Add(VediDocumentazione)
                        'phDash.Controls.Add(hpUP)
                        'phDash.Controls.Add(Verb)
                        'phDash.Controls.Add(fotoCorsisti)



                        '   phDash.Controls.Add(stopFoto)
                        ' phDash.Controls.Add(hpUPPag)

                        If tessera = "..\img\noPdf.jpg" Then
                            '     phDash10.Controls.Add(New LiteralControl("<td><img src='" & tessera & "' height='70' width='70' alt='" & Data.FixNull(dr("Asi_Nome")) & " " & Data.FixNull(dr("Asi_Cognome")) & "'></td>"))


                        Else
                            phDash.Controls.Add(New LiteralControl("<a class=""btn btn-success btn-sm btn-due btn-custom mb-2 "" target=""_blank"" href='scaricaTesseraEquiparazioneN2.aspx?record_ID=" & deEnco.QueryStringEncode(dr("idrecord")) & "&nomeFilePC=" _
                     & deEnco.QueryStringEncode(Data.FixNull(dr("TesseraEquiparazioneText"))) & "&nominativo=" _
                     & deEnco.QueryStringEncode(Data.FixNull(dr("Equi_Cognome")) & "_" & Data.FixNull(dr("Equi_Nome"))) & "'><i class=""bi bi-person-badge""> </i>Scarica Tessera</a>"))


                        End If


                        If diploma = "..\img\noPdf.jpg" Then
                            '     phDash10.Controls.Add(New LiteralControl("<td><img src='" & tessera & "' height='70' width='70' alt='" & Data.FixNull(dr("Asi_Nome")) & " " & Data.FixNull(dr("Asi_Cognome")) & "'></td>"))


                        Else
                            phDash.Controls.Add(New LiteralControl("<a class=""btn btn-success btn-sm btn-due btn-custom mb-2"" target=""_blank"" href='scaricaDiplomaEquiparazioneN2.aspx?record_ID=" & deEnco.QueryStringEncode(dr("idrecord")) & "&nomeFilePC=" _
                     & deEnco.QueryStringEncode(Data.FixNull(dr("DiplomaAsiText"))) & "&nominativo=" _
                     & deEnco.QueryStringEncode(Data.FixNull(dr("Equi_Cognome")) & "_" & Data.FixNull(dr("Equi_Nome"))) & "'><i class=""bi bi-person-badge""> </i>Scarica Diploma</a>"))


                        End If






                        phDash.Controls.Add(New LiteralControl("</div>"))





                        phDash.Controls.Add(New LiteralControl("</div>"))




                        If Not String.IsNullOrWhiteSpace(Data.FixNull(dr("Equi_NoteAnnullamentoEquiparazione"))) Then




                            phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))


                            phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-12 text-left moltopiccolo"">"))


                            phDash.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Note Annullamento: <span>"))
                            phDash.Controls.Add(New LiteralControl("<small>" & Data.FixNull(dr("Equi_NoteAnnullamentoEquiparazione"))))

                            phDash.Controls.Add(New LiteralControl("</small></h6></span></div>"))



                            phDash.Controls.Add(New LiteralControl("</div>"))

                        End If



                        phDash.Controls.Add(New LiteralControl("<hr>"))


                        phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))


                        phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left moltopiccolo"">"))


                        '   phDash.Controls.Add(New LiteralControl("</span><br />"))


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


                        ' End If

                    Next

                Else
                    Session("procedi") = "KO"
                    Response.Redirect("DashboardEquiEvasi2.aspx?tipo=" & tipoInserito & "&ris=" & deEnco.QueryStringEncode("ko"))


                End If

            Catch ex As Exception
            End Try



        End If
    End Sub
End Class