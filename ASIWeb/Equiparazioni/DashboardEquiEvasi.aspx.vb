Imports fmDotNet
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed
Imports System.Globalization

Public Class DashboardEquiEvasi
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
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Nessuna Equiparazione Evasa con questo codice fiscale ' ).set('resizable', true).resizeTo('20%', 200);", True)
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



            Equiparazioni()





        End If

    End Sub

    Sub Equiparazioni()

        Dim ds As DataSet

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("webEquiparazioniRichiesta")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        ' RequestP.AddSearchField("pre_stato_web", "1")
        RequestP.AddSearchField("Codice_Ente_Richiedente", Session("codice"), Enumerations.SearchOption.equals)
        '  RequestP.AddSearchField("Codice_Status", "115", Enumerations.SearchOption.equals)
        '  RequestP.AddSearchField("Codice_Status", "119", Enumerations.SearchOption.equals)
        RequestP.AddSortField("Codice_Status", Enumerations.Sort.Descend)


        RequestP.SetMax(10)


        ds = RequestP.Execute()

        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

            'Dim counter As Integer = 0
            Dim counter1 As Integer = 0
            Dim totale As Decimal = 0





            For Each dr In ds.Tables("main").Rows

                Dim VediDocumentazione As New Button
                VediDocumentazione.CausesValidation = False
                VediDocumentazione.ID = "VediDoc_" & counter1
                VediDocumentazione.Attributes.Add("runat", "server")
                VediDocumentazione.Text = "Diploma e Foto"
                VediDocumentazione.PostBackUrl = "vediDocumentazione.aspx?codR=" & deEnco.QueryStringEncode(Data.FixNull(dr("IDEquiparazione"))) & "&record_ID=" & deEnco.QueryStringEncode(dr("id_record"))
                VediDocumentazione.CssClass = "btn btn-success btn-sm btn-nove btn-custom"
                '  Annulla.OnClientClick = "return confirm(""ciappa"");"
                'VediDocumentazione.Attributes.Add("OnClick", "if(!myConfirm())return false;")
                '   StopFoto.Attributes.Add("OnClick", "if(!alertify.confirm)return false;")



                If Data.FixNull(dr("Codice_Status")) = "115" Then
                    VediDocumentazione.Visible = True
                Else
                    VediDocumentazione.Visible = False
                End If



                If Data.FixNull(dr("Codice_Status")) = "115" Or Data.FixNull(dr("Codice_Status")) = "119" Then
                    'If counter1 <= 10 Then


                    phDash10.Visible = True



                    phDash10.Controls.Add(New LiteralControl("<div class=""col-sm-10 mb-3 mb-md-0"">"))



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
                    phDash10.Controls.Add(New LiteralControl("<a name=" & Data.FixNull(dr("IDEquiparazione")) & ">" & Data.FixNull(dr("IDEquiparazione")) & "</a>"))
                    phDash10.Controls.Add(New LiteralControl())

                    phDash10.Controls.Add(New LiteralControl("</span><br />"))


                    phDash10.Controls.Add(New LiteralControl("Nominativo: <small>" & Data.FixNull(dr("Equi_Nome")) & " " & Data.FixNull(dr("Equi_Cognome")) & "</small><br />"))

                    phDash10.Controls.Add(New LiteralControl("CF: <small>" & Data.FixNull(dr("Equi_CodiceFiscale")) & "</small><br />"))
                    phDash10.Controls.Add(New LiteralControl("Data Scadenza: <small>" & SonoDieci(Data.FixNull(dr("Equi_DataScadenza"))) & "</small><br />"))

                    phDash10.Controls.Add(New LiteralControl())

                    ' phDash10.Controls.Add(New LiteralControl("</span>"))

                    phDash10.Controls.Add(New LiteralControl("</div>"))


                    phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-4  text-left"">"))

                    phDash10.Controls.Add(New LiteralControl("</span><small>Status: </small><small " & Utility.statusColorTextCorsi(Data.FixNull(dr("Codice_Status"))) & ">" & Data.FixNull(dr("Descrizione_StatusWeb")) & "</small>"))

                    phDash10.Controls.Add(New LiteralControl("</div>"))


                    phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-right"">"))

                    'phDash10.Controls.Add(AnnPrimaFase)
                    'phDash10.Controls.Add(Ann)
                    'phDash10.Controls.Add(btnFase2)
                    'phDash10.Controls.Add(btnFase3)
                    'phDash10.Controls.Add(btnFase4)
                    phDash10.Controls.Add(VediDocumentazione)
                    'phDash10.Controls.Add(hpUP)
                    'phDash10.Controls.Add(Verb)
                    'phDash10.Controls.Add(fotoCorsisti)



                    '   phDash10.Controls.Add(stopFoto)
                    ' phDash10.Controls.Add(hpUPPag)






                    phDash10.Controls.Add(New LiteralControl("</div>"))





                    phDash10.Controls.Add(New LiteralControl("</div>"))


                    If Not String.IsNullOrWhiteSpace(Data.FixNull(dr("Equi_NoteAnnullamentoEquiparazione"))) Then




                        phDash10.Controls.Add(New LiteralControl("<div Class=""row"">"))


                        phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-12 text-left"">"))


                        phDash10.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Note Annullamento: <span>"))
                        phDash10.Controls.Add(New LiteralControl("<small>" & Data.FixNull(dr("Equi_NoteAnnullamentoEquiparazione"))))

                        phDash10.Controls.Add(New LiteralControl("</small></h6></span></div>"))



                        phDash10.Controls.Add(New LiteralControl("</div>"))

                    End If







                    counter1 += 1
                    phDash10.Controls.Add(New LiteralControl("</div>"))

                    phDash10.Controls.Add(New LiteralControl("</div>"))

                    phDash10.Controls.Add(New LiteralControl("</div>"))
                    phDash10.Controls.Add(New LiteralControl("</div>"))

                End If
                ' End If

            Next

        End If

    End Sub
    Protected Sub btnCheck_Click(sender As Object, e As EventArgs) Handles btnCheck.Click
        If Page.IsValid Then
            Dim ds As DataSet

            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            fmsP.SetLayout("webEquiparazioniRichiesta")
            Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            ' RequestP.AddSearchField("pre_stato_web", "1")

            RequestP.AddSearchField("Equi_CodiceFiscale", Trim(txtCodiceFiscale.Text), Enumerations.SearchOption.equals)
            RequestP.AddSearchField("Codice_Ente_Richiedente", Session("codice"), Enumerations.SearchOption.equals)





            Try


                ds = RequestP.Execute()

                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

                    'Dim counter As Integer = 0
                    Dim counter1 As Integer = 0
                    Dim totale As Decimal = 0

                    For Each dr In ds.Tables("main").Rows

                        Dim VediDocumentazione As New Button
                        VediDocumentazione.CausesValidation = False
                        VediDocumentazione.ID = "VediDoc_" & counter1
                        VediDocumentazione.Attributes.Add("runat", "server")
                        VediDocumentazione.Text = "Diploma e Foto"
                        VediDocumentazione.PostBackUrl = "vediDocumentazione.aspx?codR=" & deEnco.QueryStringEncode(Data.FixNull(dr("IDEquiparazione"))) & "&record_ID=" & deEnco.QueryStringEncode(dr("id_record"))
                        VediDocumentazione.CssClass = "btn btn-success btn-sm btn-nove btn-custom"
                        '  Annulla.OnClientClick = "return confirm(""ciappa"");"
                        'VediDocumentazione.Attributes.Add("OnClick", "if(!myConfirm())return false;")
                        '   StopFoto.Attributes.Add("OnClick", "if(!alertify.confirm)return false;")



                        If Data.FixNull(dr("Codice_Status")) = "115" Then
                            VediDocumentazione.Visible = True
                        Else
                            VediDocumentazione.Visible = False
                        End If



                        If Data.FixNull(dr("Codice_Status")) = "115" Or Data.FixNull(dr("Codice_Status")) = "119" Then
                            'If counter1 <= 10 Then






                            phDash.Visible = True



                            phDash.Controls.Add(New LiteralControl("<div class=""col-sm-10 mb-3 mb-md-0"">"))



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
                            phDash.Controls.Add(New LiteralControl("<a name=" & Data.FixNull(dr("IDEquiparazione")) & ">" & Data.FixNull(dr("IDEquiparazione")) & "</a>"))
                            phDash.Controls.Add(New LiteralControl())

                            phDash.Controls.Add(New LiteralControl("</span><br />"))


                            phDash.Controls.Add(New LiteralControl("Nominativo: <small>" & Data.FixNull(dr("Equi_Nome")) & " " & Data.FixNull(dr("Equi_Cognome")) & "</small><br />"))

                            phDash.Controls.Add(New LiteralControl("CF: <small>" & Data.FixNull(dr("Equi_CodiceFiscale")) & "</small><br />"))
                            phDash.Controls.Add(New LiteralControl("Data Scadenza: <small>" & SonoDieci(Data.FixNull(dr("Equi_DataScadenza"))) & "</small><br />"))

                            phDash.Controls.Add(New LiteralControl())

                            ' phDash.Controls.Add(New LiteralControl("</span>"))

                            phDash.Controls.Add(New LiteralControl("</div>"))


                            phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4  text-left"">"))

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






                            phDash.Controls.Add(New LiteralControl("</div>"))





                            phDash.Controls.Add(New LiteralControl("</div>"))




                            If Not String.IsNullOrWhiteSpace(Data.FixNull(dr("Equi_NoteAnnullamentoEquiparazione"))) Then




                                phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))


                                phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-12 text-left"">"))


                                phDash.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Note Annullamento: <span>"))
                                phDash.Controls.Add(New LiteralControl("<small>" & Data.FixNull(dr("Equi_NoteAnnullamentoEquiparazione"))))

                                phDash.Controls.Add(New LiteralControl("</small></h6></span></div>"))



                                phDash.Controls.Add(New LiteralControl("</div>"))

                            End If






                            counter1 += 1
                            phDash.Controls.Add(New LiteralControl("</div>"))

                            phDash.Controls.Add(New LiteralControl("</div>"))

                            phDash.Controls.Add(New LiteralControl("</div>"))
                            phDash.Controls.Add(New LiteralControl("</div>"))

                        End If
                        ' End If

                    Next



                End If

            Catch ex As Exception
                Session("procedi") = "KO"
                Response.Redirect("DashboardEquiEvasi.aspx?ris=" & deEnco.QueryStringEncode("ko"))
            End Try



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

    Protected Sub btnUltimi5_Click(sender As Object, e As EventArgs) Handles btnUltimi5.Click
        Equiparazioni()
    End Sub
End Class