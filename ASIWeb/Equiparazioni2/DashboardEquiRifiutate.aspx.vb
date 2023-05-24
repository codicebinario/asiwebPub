
Imports fmDotNet
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed
Imports System.Net

Public Class DashboardRifiutate
    Inherits System.Web.UI.Page
    Dim open As Integer = 0
    Dim foto As String
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
                If Session("visto") = "ok" Then

                    ris = deEnco.QueryStringDecode(ris)

                    If ris = "ok" Then
                        If Session("equiparazioneaggiunta") = "OK" Then
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Richiesta Equiparazione caricata nel sistema! ' ).set('resizable', true).resizeTo('20%', 200);", True)
                            Session("equiparazioneaggiunta") = Nothing
                        End If
                    ElseIf ris = "ko" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Richiesta Equiparazione non caricata nel sistema ' ).set('resizable', true).resizeTo('20%', 200);", True)
                        Session("equiparazioneaggiunta") = Nothing
                    ElseIf ris = "no" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Richiesta Equiparazione senza verifica tessera.<br />Procedere con una nuova richiesta ' ).set('resizable', true).resizeTo('20%', 200);", True)
                        Session("equiparazioneaggiunta") = Nothing
                    End If
                    Session("visto") = Nothing
                End If
            End If


            If Session("stoCorsi") = "ok" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Ora non è più possibile aggiungere foto! ' ).set('resizable', true).resizeTo('20%', 200);", True)
                Session("stoCorsi") = Nothing
            End If



        End If
        If Not Page.IsPostBack Then
            Dim aperto As String
            aperto = Request.QueryString("aperto")
            '   rinnovi2()

            open = Request.QueryString("open")

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

    Function togliND(valore As String) As String
        Dim ritorno As String = ""
        If valore = "ND" Then
            ritorno = ""
        Else
            ritorno = " - " & valore

        End If

        Return ritorno
    End Function
    Sub Equiparazioni()






        phDash.Visible = True




        Dim ds1 As DataSet

                Dim fmsP1 As FMSAxml = AsiModel.Conn.Connect()
                fmsP1.SetLayout("webEquiparazioniRichiestaMolti")
                Dim RequestP1 = fmsP1.CreateFindRequest(Enumerations.SearchType.Subset)
        ' RequestP.AddSearchField("pre_stato_web", "1")
        '  RequestP1.AddSearchField("IDEquiparazioneM", Data.FixNull(dr1("IDEquiparazioneM")), Enumerations.SearchOption.equals)
        RequestP1.AddSearchField("Codice_Ente_Richiedente", Session("codice"))
        RequestP1.AddSearchField("Codice_Status", "104")
        RequestP1.SetMax(10)
        RequestP1.AddSortField("IDEquiparazioneM", Enumerations.Sort.Descend)
        'RequestP1.AddSortField("IDRecord", Enumerations.Sort.Descend)



        ds1 = RequestP1.Execute()

                If Not IsNothing(ds1) AndAlso ds1.Tables("main").Rows.Count > 0 Then


                    Dim tessera As String
            Dim rompiStatus As Integer
            Dim cambiato As String
            Dim counter1 As Integer = 0
            Dim motivo As String = ""

            For Each dr1 In ds1.Tables("main").Rows
                If rompiStatus = Data.FixNull(dr1("IDEquiparazioneM")) Then
                    cambiato = ""
                Else
                    cambiato = "ok"
                End If
                motivo = Equiparazione.GetMotivoRespintoEqui(Data.FixNull(dr1("IDEquiparazioneM")))
                counter1 += 1


                        If String.IsNullOrWhiteSpace(Data.FixNull(dr1("FotoEquiparazione"))) Then
                            foto = "..\img\noimg.jpg"
                        Else
                            foto = "https://93.63.195.98" & Data.FixNull(dr1("FotoEquiparazione"))
                        End If

                        If String.IsNullOrWhiteSpace(Data.FixNull(dr1("tesseraEquiparazione"))) Then
                            tessera = "..\img\noPdf.jpg"
                        Else
                            tessera = "https://93.63.195.98" & Data.FixNull(dr1("tesseraEquiparazione"))
                        End If




                Dim VediDocumentazione As New LinkButton
                        VediDocumentazione.ID = "VediDoc_" & counter1
                        VediDocumentazione.Attributes.Add("runat", "server")
                        VediDocumentazione.Text = "<i class=""bi bi-file-earmark-pdf""> </i>Diploma e Foto"
                        VediDocumentazione.PostBackUrl = "vediDocumentazione2.aspx?codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr1("IDEquiparazioneM")))) & "&record_ID=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(dr1("idrecord")))
                        VediDocumentazione.CssClass = "btn btn-success btn-sm btn-nove btn-custom mb-2"
                VediDocumentazione.Visible = False

                ' If Data.FixNull(dr1("Codice_Status") <= 114) Then
                'umentazione.Visible = True
                'Else
                'VediDocumentazione.Visible = False
                'End If


                phDash.Controls.Add(New LiteralControl("<div class=""col-sm-12 mb-3 mb-md-0"">"))
                If cambiato = "ok" Then


                    phDash.Controls.Add(New LiteralControl("<div Class=""section-divider"">"))
                    phDash.Controls.Add(New LiteralControl("<span>Richiesta: " & Data.FixNull(dr1("IDEquiparazioneM")) & " - Motivo: " & motivo & "</span>"))
                    phDash.Controls.Add(New LiteralControl("</div>"))
                End If



                'accordion card
                If Data.FixNull(dr1("Codice_Status") = 104) Then
                            phDash.Controls.Add(New LiteralControl("<div class=""card mb-2 shadow-sm rounded bg-danger"">"))
                        Else
                            phDash.Controls.Add(New LiteralControl("<div class=""card mb-2 shadow-sm rounded "">"))

                        End If


                        'accordion heder
                        phDash.Controls.Add(New LiteralControl("<div class=""card-header"">"))

                        phDash.Controls.Add(New LiteralControl("<div Class=""container-fluid"">"))

                        '            ' inizio prima riga

                        phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))

                        If Data.FixNull(dr1("Codice_Status") = 104) Then
                            phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left moltopiccolo text-white"">"))

                        Else

                            phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left moltopiccolo"">"))

                        End If


                        'phDash.Controls.Add(New LiteralControl("Equiparazione:  "))
                        'phDash.Controls.Add(New LiteralControl("<span  " & Utility.statusColorCorsi(Data.FixNull(dr1("Codice_Status"))) & ">"))
                        'phDash.Controls.Add(New LiteralControl("<a name=" & Data.FixNull(dr1("IDRecord")) & ">" & Data.FixNull(dr1("IDRecord")) & "</a>"))
                        'phDash.Controls.Add(New LiteralControl())

                        'phDash.Controls.Add(New LiteralControl("</span><br />"))


                        phDash.Controls.Add(New LiteralControl("Nominativo: <small>" & Data.FixNull(dr1("Equi_Nome")) & " " & Data.FixNull(dr1("Equi_Cognome")) & "</small><br />"))

                        phDash.Controls.Add(New LiteralControl("CF: <small>" & Data.FixNull(dr1("Equi_CodiceFiscale")) & "</small><br />"))
                        phDash.Controls.Add(New LiteralControl("Tessera Ass.: <small>" & Data.FixNull(dr1("Equi_NumeroTessera")) & "</small><br />"))
                        phDash.Controls.Add(New LiteralControl("Data Scadenza: <small>" & SonoDieci(Data.FixNull(dr1("Equi_DataScadenza"))) & "</small><br />"))

                        phDash.Controls.Add(New LiteralControl())

                        ' phDash.Controls.Add(New LiteralControl("</span>"))

                        phDash.Controls.Add(New LiteralControl("</div>"))

                        If Data.FixNull(dr1("Codice_Status") = 104) Then
                            phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left moltopiccolo text-white"">"))

                        Else

                            phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left moltopiccolo"">"))

                        End If










                        phDash.Controls.Add(New LiteralControl("</span><small>Status: </small><small " & Utility.statusColorTextCorsi(Data.FixNull(dr1("Codice_Status"))) & ">" & Data.FixNull(dr1("Descrizione_StatusWeb")) & "</small>"))

                        If Data.FixNull(dr1("Codice_Status") = 104) Then

                            phDash.Controls.Add(New LiteralControl("<br />Motivo: " & Data.FixNull(dr1("NoteValutazioneDT"))))

                        End If



                        phDash.Controls.Add(New LiteralControl("</div>"))


                        phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-right"">"))


                phDash.Controls.Add(VediDocumentazione)







                phDash.Controls.Add(New LiteralControl("</div>"))





                        phDash.Controls.Add(New LiteralControl("</div>"))





                        phDash.Controls.Add(New LiteralControl("<hr>"))


                        phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))


                        If Data.FixNull(dr1("Codice_Status") = 104) Then
                            phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-6 text-left moltopiccolo text-white"">"))

                        Else

                            phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-6 text-left moltopiccolo"">"))

                        End If





                phDash.Controls.Add(New LiteralControl("Livello: <small>" & Data.FixNull(dr1("Equi_Livello")) & "</small><br />"))
                phDash.Controls.Add(New LiteralControl("Qualifica da Rilasciare: <small>" & Data.FixNull(dr1("Equi_Qualifica_Tecnica_Da_Rilasciare")) & "</small><br />"))
                        phDash.Controls.Add(New LiteralControl("Qualifica DT:  "))

                        phDash.Controls.Add(New LiteralControl("<small>" & Data.FixNull(dr1("Dicitura_Qualifica_DT")) & "</small><br />"))

                        If Not String.IsNullOrWhiteSpace(Data.FixNull(dr1("NoteValutazioneSettore"))) Then
                            phDash.Controls.Add(New LiteralControl("Note da Settore:  "))
                            phDash.Controls.Add(New LiteralControl("<small>" & Data.FixNull(dr1("NoteValutazioneSettore")) & "</small><br />"))


                        End If



                phDash.Controls.Add(New LiteralControl("</div>"))






                        phDash.Controls.Add(New LiteralControl("</div>"))








                        counter1 += 1
                        phDash.Controls.Add(New LiteralControl("</div>"))

                        phDash.Controls.Add(New LiteralControl("</div>"))

                        phDash.Controls.Add(New LiteralControl("</div>"))
                        phDash.Controls.Add(New LiteralControl("</div>"))
                rompiStatus = Data.FixNull(dr1("IDEquiparazioneM"))
                '  End If
            Next

                End If



                phDash.Controls.Add(New LiteralControl("</div>"))



                phDash.Controls.Add(New LiteralControl("</div>"))


                phDash.Controls.Add(New LiteralControl("</div>"))

        '    Next


        '    phDash.Controls.Add(New LiteralControl("</div>"))
        'End If

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

    Protected Sub lnkLast10_Click(sender As Object, e As EventArgs)
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
            RequestP.AddSearchField("Codice_Status", "104")
            RequestP.AddSortField("IDEquiparazioneM", Enumerations.Sort.Descend)



            Try


                ds = RequestP.Execute()

                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

                    'Dim counter As Integer = 0
                    Dim counter1 As Integer = 0
                    Dim totale As Decimal = 0
                    Dim tessera As String
                    Dim nominativo As String
                    Dim diploma As String
                    Dim motivo As String
                    Dim rompiStatus As Integer
                    Dim cambiato As String
                    For Each dr In ds.Tables("main").Rows
                        If rompiStatus = Data.FixNull(dr("IDEquiparazioneM")) Then
                            cambiato = ""
                        Else
                            cambiato = "ok"
                        End If
                        motivo = Equiparazione.GetMotivoRespintoEqui(Data.FixNull(dr("IDEquiparazioneM")))
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
                        VediDocumentazione.Visible = False







                        phDash10.Visible = True
                        If cambiato = "ok" Then


                            phDash10.Controls.Add(New LiteralControl("<div Class=""section-divider"">"))
                            phDash10.Controls.Add(New LiteralControl("<span>Richiesta: " & Data.FixNull(dr("IDEquiparazioneM")) & " - Motivo: " & motivo & "</span>"))
                            phDash10.Controls.Add(New LiteralControl("</div>"))
                        End If
                        phDash10.Controls.Add(New LiteralControl("<div class=""col-sm-12 mb-3 mb-md-0 text-white"">"))



                        'accordion card
                        If Data.FixNull(dr("Codice_Status") = 104) Then
                            phDash10.Controls.Add(New LiteralControl("<div class=""card mb-2 shadow-sm rounded bg-danger"">"))
                        Else
                            phDash10.Controls.Add(New LiteralControl("<div class=""card mb-2 shadow-sm rounded "">"))

                        End If
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

                        ' phDash.Controls.Add(New LiteralControl("</span>"))

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
                     & deEnco.QueryStringEncode(Data.FixNull(dr("Equi_Cognome")) & "_" & Data.FixNull(dr("Equi_Nome"))) & "'><i class=""bi bi-person-badge""> </i>Scarica Tessera</a>"))


                        End If


                        If diploma = "..\img\noPdf.jpg" Then
                            '     phDash10.Controls.Add(New LiteralControl("<td><img src='" & tessera & "' height='70' width='70' alt='" & Data.FixNull(dr("Asi_Nome")) & " " & Data.FixNull(dr("Asi_Cognome")) & "'></td>"))


                        Else
                            phDash10.Controls.Add(New LiteralControl("<a class=""btn btn-success btn-sm btn-due btn-custom mb-2"" target=""_blank"" href='scaricaDiplomaEquiparazioneN2.aspx?record_ID=" & deEnco.QueryStringEncode(dr("idrecord")) & "&nomeFilePC=" _
                     & deEnco.QueryStringEncode(Data.FixNull(dr("DiplomaAsiText"))) & "&nominativo=" _
                     & deEnco.QueryStringEncode(Data.FixNull(dr("Equi_Cognome")) & "_" & Data.FixNull(dr("Equi_Nome"))) & "'><i class=""bi bi-person-badge""> </i>Scarica Diploma</a>"))


                        End If






                        phDash10.Controls.Add(New LiteralControl("</div>"))





                        phDash10.Controls.Add(New LiteralControl("</div>"))




                        If Not String.IsNullOrWhiteSpace(Data.FixNull(dr("Equi_NoteAnnullamentoEquiparazione"))) Then




                            phDash10.Controls.Add(New LiteralControl("<div Class=""row"">"))


                            phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-12 text-left moltopiccolo"">"))


                            phDash10.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Note Annullamento: <span>"))
                            phDash10.Controls.Add(New LiteralControl("<small>" & Data.FixNull(dr("Equi_NoteAnnullamentoEquiparazione"))))

                            phDash10.Controls.Add(New LiteralControl("</small></h6></span></div>"))



                            phDash10.Controls.Add(New LiteralControl("</div>"))

                        End If



                        phDash10.Controls.Add(New LiteralControl("<hr>"))


                        phDash10.Controls.Add(New LiteralControl("<div Class=""row"">"))


                        phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left moltopiccolo"">"))


                        '   phDash.Controls.Add(New LiteralControl("</span><br />"))


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