Imports fmDotNet
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed
Public Class archivioAlbo
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


            Corsi()





        End If
    End Sub
    Sub Corsi()


        Dim ds As DataSet

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("webCorsiRichiesta")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        ' RequestP.AddSearchField("pre_stato_web", "1")
        RequestP.AddSearchField("Codice_Ente_Richiedente", Session("codice"), Enumerations.SearchOption.equals)
        RequestP.AddSortField("Codice_Status", Enumerations.Sort.Ascend)
        RequestP.AddSortField("IDCorso", Enumerations.Sort.Descend)



        ds = RequestP.Execute()

        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then




            phDash.Visible = True
            'Dim counter As Integer = 0
            Dim counter1 As Integer = 0
            'Dim totale As Decimal = 0
            For Each dr In ds.Tables("main").Rows
                Dim oldStatus As String = Data.FixNull(dr("StatusPrimaCaricamentoXL"))
                If Data.FixNull(dr("Codice_Status")) = "84" Or Data.FixNull(dr("Codice_Status")) = "101" _
                    Or Data.FixNull(dr("Codice_Status")) = "60" Or Data.FixNull(dr("Codice_Status")) = "72" _
                     Then


                    counter1 += 1




                    Dim deEnco As New Ed()

                    Dim fotoCorsisti As New Button

                    fotoCorsisti.ID = "Fot_" & counter1
                    fotoCorsisti.Attributes.Add("runat", "server")
                    fotoCorsisti.Text = "Corsisti KO"
                    fotoCorsisti.PostBackUrl = "corsistiKO.aspx?codR=" & deEnco.QueryStringEncode(Data.FixNull(dr("IDCorso"))) & "&record_ID=" & deEnco.QueryStringEncode(dr("id_record")) & "&oldStatus=" & deEnco.QueryStringEncode(oldStatus)
                    fotoCorsisti.CssClass = "btn btn-success btn-sm btn-otto btn-custom"
                    If (Data.FixNull(dr("Codice_Status")) = "72" And Data.FixNull(dr("fase")) = "3") Then
                        fotoCorsisti.Visible = True
                    Else
                        fotoCorsisti.Visible = False
                    End If





                    phDash.Controls.Add(New LiteralControl("<div class=""col-sm-10 mb-3 mb-md-0"">"))



                    'accordion card
                    phDash.Controls.Add(New LiteralControl("<div class=""card mb-2 shadow-sm rounded"">"))
                    'accordion heder
                    phDash.Controls.Add(New LiteralControl("<div class=""card-header"">"))

                    phDash.Controls.Add(New LiteralControl("<div Class=""container-fluid"">"))

                    ' inizio prima riga

                    phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))


                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-3 text-left"">"))

                    phDash.Controls.Add(New LiteralControl("Corso:  "))
                    phDash.Controls.Add(New LiteralControl("<span  " & Utility.statusColorCorsi(Data.FixNull(dr("Codice_Status"))) & ">"))
                    phDash.Controls.Add(New LiteralControl("<a name=" & Data.FixNull(dr("IDCorso")) & ">" & Data.FixNull(dr("IDCorso")) & "</a>"))
                    phDash.Controls.Add(New LiteralControl())


                    phDash.Controls.Add(New LiteralControl("</span></div>"))

                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-5  text-left"">"))

                    phDash.Controls.Add(New LiteralControl("</span><small>Status: </small><small " & Utility.statusColorTextCorsi(Data.FixNull(dr("Codice_Status"))) & ">" & Data.FixNull(dr("Descrizione_Status")) & "</small>"))

                    phDash.Controls.Add(New LiteralControl("</div>"))


                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left"">"))






                    phDash.Controls.Add(fotoCorsisti)
                    phDash.Controls.Add(New LiteralControl("<br />"))

                    phDash.Controls.Add(New LiteralControl("<h6 class=""piccolo text-left""><br />Note Corsisti KO: <span><br />"))
                    phDash.Controls.Add(New LiteralControl("<small>" & Data.FixNull(dr("Motivo_Corsisti_KO"))))

                    phDash.Controls.Add(New LiteralControl("</small></h6></span>"))



                    phDash.Controls.Add(New LiteralControl("</div>"))





                    phDash.Controls.Add(New LiteralControl("</div>"))











                    ' fine primma riga





                    'inizio seconda riga

                    ' <h5>h5 heading <small>secondary text</small></h5>

                    phDash.Controls.Add(New LiteralControl("<div class=""row"">"))

                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-7 text-left"">"))

                    phDash.Controls.Add(New LiteralControl("<h6>Nome Corso: <span><small>" & Data.FixNull(dr("Titolo_Corso")) & "</small></h6><span />"))


                    phDash.Controls.Add(New LiteralControl("</div>"))

                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-5 text-left"">"))



                    phDash.Controls.Add(New LiteralControl("</div>"))

                    phDash.Controls.Add(New LiteralControl("</div>"))


                    'intermezzo
                    phDash.Controls.Add(New LiteralControl("<div class=""row"">"))

                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-12 text-left"">"))

                    phDash.Controls.Add(New LiteralControl("<hr>"))

                    phDash.Controls.Add(New LiteralControl("</div>"))



                    phDash.Controls.Add(New LiteralControl("</div>"))

                    'fine seconda riga


                    'inizio seconda riga bis

                    phDash.Controls.Add(New LiteralControl("<div class=""row"">"))

                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-8 text-left"">"))

                    phDash.Controls.Add(New LiteralControl("<h6>Indirizzo: <span><small> " & Data.FixNull(dr("Indirizzo_Svolgimento")) & " " & Data.FixNull(dr("Cap_Svolgimento")) & " " & Data.FixNull(dr("Comune_Svolgimento")) & " " & Data.FixNull(dr("PR_Svolgimento")) & "</small></h6><span />"))


                    phDash.Controls.Add(New LiteralControl("</div>"))

                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left"">"))



                    phDash.Controls.Add(New LiteralControl("</div>"))

                    phDash.Controls.Add(New LiteralControl("</div>"))


                    'fine seconda riga bis




                    'inizio terza riga



                    phDash.Controls.Add(New LiteralControl("<div class=""row"">"))

                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-3 text-left"">"))

                    phDash.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Da: <span><small> " & SonoDieci(Data.FixNull(dr("Svolgimento_da_Data"))) & "</small></h6><span />"))


                    phDash.Controls.Add(New LiteralControl("</div>"))

                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-3 text-left"">"))

                    phDash.Controls.Add(New LiteralControl("<h6 class=""piccolo"">A: <span><small> " & SonoDieci(Data.FixNull(dr("Svolgimento_a_Data"))) & "</small></h6><span />"))


                    phDash.Controls.Add(New LiteralControl("</div>"))

                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-6 text-left"">"))

                    phDash.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Data Richiesta: <span><small> " & Data.FixNull(dr("IndicatoreDataOraCreazione")) & "</small></h6><span />"))


                    phDash.Controls.Add(New LiteralControl("</div>"))

                    phDash.Controls.Add(New LiteralControl("</div>"))




                    'fine terza riga





                    phDash.Controls.Add(New LiteralControl("<div class=""row"">"))

                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left"">"))

                    phDash.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Sport: <span><small> " & Data.FixNull(dr("Sport_Interessato")) & "</small></h6><span />"))


                    phDash.Controls.Add(New LiteralControl("</div>"))

                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left"">"))

                    phDash.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Disciplina: <span><small> " & Data.FixNull(dr("Disciplina_interessata")) & "</small></h6><span />"))


                    phDash.Controls.Add(New LiteralControl("</div>"))

                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left"">"))

                    phDash.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Specialità: <span><small> " & Data.FixNull(dr("Specialita")) & "</small></h6><span />"))


                    phDash.Controls.Add(New LiteralControl("</div>"))

                    phDash.Controls.Add(New LiteralControl("</div>"))




                    'fine quarta riga

                    'inizio quinta riga



                    phDash.Controls.Add(New LiteralControl("<div class=""row"">"))

                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-3 text-left"">"))

                    phDash.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Qualifica: <span><small> " & Data.FixNull(dr("Qualifica_Tecnica_Da_Rilasciare")) & "</small></h6><span />"))


                    phDash.Controls.Add(New LiteralControl("</div>"))

                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-3 text-left"">"))

                    phDash.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Livello: <span><small> " & Data.FixNull(dr("Livello")) & "</small></h6><span />"))


                    phDash.Controls.Add(New LiteralControl("</div>"))



                    Dim quota As Decimal
                    If String.IsNullOrEmpty(Data.FixNull(dr("Quota_Partecipazione"))) Then
                        quota = 0
                    Else
                        quota = dr("Quota_Partecipazione")
                    End If









                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-3 text-left"">"))

                    phDash.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Quota: <span><small> " & quota.ToString("C2", New Globalization.CultureInfo("it-IT")) & "</small></h6><span />"))


                    phDash.Controls.Add(New LiteralControl("</div>"))

                    phDash.Controls.Add(New LiteralControl("</div>"))




                    'fine quinta riga


                    ' inizio sesta riga 


                    phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))


                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-12 text-left"">"))


                    phDash.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Note Piano Corso: <span>"))
                    phDash.Controls.Add(New LiteralControl("<small>" & Data.FixNull(dr("NoteUploadDocumentoCorso"))))

                    phDash.Controls.Add(New LiteralControl("</small></h6></span></div>"))



                    phDash.Controls.Add(New LiteralControl("</div>"))



                    'fine sesta riga sottosezione


                    ' inizio settima riga 


                    phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))


                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-12 text-left"">"))


                    phDash.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Note Elenco Partecipanti: <span>"))
                    phDash.Controls.Add(New LiteralControl("<small>" & Data.FixNull(dr("NoteUploadElencoCorso"))))

                    phDash.Controls.Add(New LiteralControl("</small></h6></span></div>"))



                    phDash.Controls.Add(New LiteralControl("</div>"))



                    'fine settima riga sottosezione


                    ' inizio ottava riga 


                    phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))


                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-12 text-left"">"))


                    phDash.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Note Pagamento: <span>"))
                    phDash.Controls.Add(New LiteralControl("<small>" & Data.FixNull(dr("NoteUploadPagamento"))))

                    phDash.Controls.Add(New LiteralControl("</small></h6></span></div>"))



                    phDash.Controls.Add(New LiteralControl("</div>"))



                    'fine ottava riga sottosezione



                    ' inizio ottava riga 


                    phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))


                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-12 text-left"">"))


                    phDash.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Note Verbale: <span>"))
                    phDash.Controls.Add(New LiteralControl("<small>" & Data.FixNull(dr("NoteUploadVerbale"))))

                    phDash.Controls.Add(New LiteralControl("</small></h6></span></div>"))



                    phDash.Controls.Add(New LiteralControl("</div>"))



                    'fine ottava riga sottosezione
                    ' inizio ottava riga 


                    phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))


                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-12 text-left"">"))


                    phDash.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Note Valutazione DT: <span>"))
                    phDash.Controls.Add(New LiteralControl("<small>" & Data.FixNull(dr("NoteValutazioneDT"))))

                    phDash.Controls.Add(New LiteralControl("</small></h6></span></div>"))



                    phDash.Controls.Add(New LiteralControl("</div>"))


                    ' inizio ottava riga 


                    phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))


                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-12 text-left"">"))


                    phDash.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Note Annullamento: <span>"))
                    phDash.Controls.Add(New LiteralControl("<small>" & Data.FixNull(dr("NoteAnnullamentoCorso"))))

                    phDash.Controls.Add(New LiteralControl("</small></h6></span></div>"))



                    phDash.Controls.Add(New LiteralControl("</div>"))







                    counter1 += 1
                    phDash.Controls.Add(New LiteralControl("</div>"))

                    phDash.Controls.Add(New LiteralControl("</div>"))

                    phDash.Controls.Add(New LiteralControl("</div>"))
                    phDash.Controls.Add(New LiteralControl("</div>"))
                End If
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

End Class