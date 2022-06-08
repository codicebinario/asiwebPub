Imports fmDotNet
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed
Public Class Dashboard
    Inherits System.Web.UI.Page
    Dim cultureFormat As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("it-IT")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '    Dim newCulture As System.Globalization.CultureInfo = System.Globalization.CultureInfo.CurrentUICulture.Clone()
        cultureFormat.NumberFormat.CurrencySymbol = "€"
        cultureFormat.NumberFormat.CurrencyDecimalDigits = 2
        cultureFormat.NumberFormat.CurrencyGroupSeparator = String.Empty
        cultureFormat.NumberFormat.CurrencyDecimalSeparator = ","
        System.Threading.Thread.CurrentThread.CurrentCulture = cultureFormat
        System.Threading.Thread.CurrentThread.CurrentUICulture = cultureFormat









        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("login.aspx")
        End If
        If IsNothing(Session("codice")) Then
            Response.Redirect("login.aspx")
        End If
        Dim ris As String = Request.QueryString("ris")




        If Session("risultato") = "ok" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Ordine Annullato! ' ).set('resizable', true).resizeTo('20%', 200);", True)
            Session("risultato") = Nothing
        ElseIf Session("risultato") = "ko" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Ordine impossibile da annullare ' ).set('resizable', true).resizeTo('20%', 200);", True)
            Session("risultato") = Nothing
        End If

        'If Not IsNothing(Session("denominazione")) Then

        '    litDenominazioneJumboDash.Text = "Codice: " & AsiModel.LogIn.Codice & " - " & "Tipo Ente: " & AsiModel.LogIn.TipoEnte & " - " & AsiModel.LogIn.Denominazione
        '    'LitNumeroRichiesta.Text = Session("Codice_Richiesta")
        '    'lblRichiestaConferma.Text = Session("Codice_Richiesta")

        'End If

        'If IsNothing(Session("Codice_Richiesta")) Then
        '    Response.Redirect("Dashboard.aspx")
        'End If

        If Not Page.IsPostBack Then


            Richieste()





        End If

    End Sub


    Sub Richieste()


        Dim ds As DataSet

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("web_richiesta_master")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        ' RequestP.AddSearchField("pre_stato_web", "1")
        RequestP.AddSearchField("Ente_Codice", Session("codice"), Enumerations.SearchOption.equals)
        RequestP.AddSortField("Status_ID", Enumerations.Sort.Ascend)
        RequestP.AddSortField("Codice_Richiesta", Enumerations.Sort.Descend)





        'Dim RequestP = fmsP.CreateCompoundFindRequest()
        'RequestP.AddSearchCriterion("Ente_Codice", "==" & AsiModel.LogIn.Codice, False, False)
        'RequestP.AddSearchCriterion("Status_ID", "==1", True, False)
        'RequestP.AddSearchCriterion("Status_ID", "==2", True, False)
        'RequestP.AddSearchCriterion("Status_ID", "==3", True, False)
        'RequestP.AddSearchCriterion("Status_ID", "==6", False, False)
        'RequestP.AddSearchCriterion("Status_ID", "==8", False, False)
        'RequestP.AddSortField("Status_ID", Enumerations.Sort.Ascend)
        'RequestP.AddSortField("Codice_Richiesta", Enumerations.Sort.Descend)
        '    Try
        ds = RequestP.Execute()

        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then



            phDash.Visible = True
            'Dim counter As Integer = 0
            Dim counter1 As Integer = 0
            'Dim totale As Decimal = 0




            For Each dr In ds.Tables("main").Rows


                If Data.FixNull(dr("Status_ID")) = "1" Or Data.FixNull(dr("Status_ID")) = "2" Or Data.FixNull(dr("Status_ID")) = "3" Or Data.FixNull(dr("Status_ID")) = "4" Or Data.FixNull(dr("Status_ID")) = "5" Or Data.FixNull(dr("Status_ID")) = "6" Or Data.FixNull(dr("Status_ID")) = "7" Or Data.FixNull(dr("Status_ID")) = "8" Then

                    If Utility.RichApertaSenzaRighe(Data.FixNull(dr("Codice_Richiesta"))) = True Then
                        counter1 += 1
                        Dim deEnco As New Ed()

                        '  upLeg.aspx?codR=1015
                        Dim hpUP As New Button

                        hpUP.ID = "hp_" & counter1
                        hpUP.Attributes.Add("runat", "server")
                        hpUP.Text = "Invia documenti"
                        hpUP.PostBackUrl = "upLeg.aspx?codR=" & deEnco.QueryStringEncode(Data.FixNull(dr("Codice_Richiesta"))) & "&st=" & deEnco.QueryStringEncode("3")
                        hpUP.CssClass = "btn btn-success btn-tre"
                        If Data.FixNull(dr("Status_ID")) = "3" Then
                            hpUP.Visible = True
                        Else
                            hpUP.Visible = False
                        End If


                        Dim hpUP5 As New Button

                        hpUP5.ID = "hp5_" & counter1
                        hpUP5.Attributes.Add("runat", "server")
                        hpUP5.Text = "Invia documenti"
                        hpUP5.PostBackUrl = "upLeg.aspx?codR=" & deEnco.QueryStringEncode(Data.FixNull(dr("Codice_Richiesta"))) & "&st=" & deEnco.QueryStringEncode("5")
                        hpUP5.CssClass = "btn btn-success btn-cinque"

                        If Data.FixNull(dr("Status_ID")) = "5" Then
                            hpUP5.Visible = True
                        Else
                            hpUP5.Visible = False
                        End If


                        Dim Finisci As New Button

                        Finisci.ID = "Finisci_" & counter1
                        Finisci.Attributes.Add("runat", "server")
                        Finisci.Text = "Finalizza Ordine"
                        Finisci.PostBackUrl = "richiestaTessere.aspx?codR=" & deEnco.QueryStringEncode(Data.FixNull(dr("Codice_Richiesta"))) & "&record_ID=" & deEnco.QueryStringEncode(dr("Record_ID"))
                        Finisci.CssClass = "btn btn-secondary btn-uno"


                        If Data.FixNull(dr("Status_ID")) = "1" Then
                            Finisci.Visible = True
                        Else
                            Finisci.Visible = False
                        End If

                        Dim Annulla As New Button
                        Annulla.ID = "Annulla_" & counter1
                        Annulla.Attributes.Add("runat", "server")
                        Annulla.Text = "Annulla Ordine"
                        Annulla.PostBackUrl = "annulla.aspx?codR=" & deEnco.QueryStringEncode(Data.FixNull(dr("Codice_Richiesta"))) & "&record_ID=" & deEnco.QueryStringEncode(dr("Record_ID")) & "&status=" & deEnco.QueryStringEncode(dr("Status_ID"))
                        Annulla.CssClass = "btn btn-secondary btn-annulla"
                        '  Annulla.OnClientClick = "return confirm(""ciappa"");"
                        Annulla.Attributes.Add("OnClick", "if(!myConfirm())return false;")

                        If Data.FixNull(dr("Status_ID")) = "1" Or Data.FixNull(dr("Status_ID")) = "2" Or Data.FixNull(dr("Status_ID")) = "3" Then
                            Annulla.Visible = True
                        Else
                            Annulla.Visible = False
                        End If

                        Dim note As New Label
                        note.ID = "Note_" & counter1
                        note.Attributes.Add("runat", "server")
                        note.Text = Data.FixNull(dr("Note_Ente"))


                        Dim noteAsi As New Label
                        noteAsi.ID = "Note_" & counter1
                        noteAsi.Attributes.Add("runat", "server")
                        noteAsi.Text = Data.FixNull(dr("Note_ASI_Ordine"))


                        'counter += 1
                        counter1 += 1

                        phDash.Controls.Add(New LiteralControl("<div class=""col-sm-10 mb-3 mb-md-0"">"))

                        'Accordion wrapper
                        phDash.Controls.Add(New LiteralControl("<div Class=""accordion md-accordion"" id=""accordionEx1"" role=""tablist"" aria-multiselectable=""false"">"))


                        'accordion card
                        phDash.Controls.Add(New LiteralControl("<div class=""card mb-3 shadow-sm rounded"">"))









                        'accordion heder
                        phDash.Controls.Add(New LiteralControl("<div class=""card-header"">"))

                        phDash.Controls.Add(New LiteralControl("<div Class=""container-fluid"">"))






                        ' inizio prima riga

                        phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))


                        phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-3 text-left"">"))

                        phDash.Controls.Add(New LiteralControl("Ordine:  "))
                        phDash.Controls.Add(New LiteralControl("<span " & Utility.statusColor(Data.FixNull(dr("Status_ID"))) & ">"))
                        phDash.Controls.Add(New LiteralControl(Data.FixNull(dr("Codice_Richiesta"))))

                        phDash.Controls.Add(New LiteralControl("</div>"))

                        phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4  text-left"">"))

                        phDash.Controls.Add(New LiteralControl("</span><small>Status: </small><small " & Utility.statusColorText(Data.FixNull(dr("Status_ID"))) & ">" & Data.FixNull(dr("Status")) & "</small>"))

                        phDash.Controls.Add(New LiteralControl("</div>"))


                        phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-5 text-right"">"))


                        phDash.Controls.Add(New LiteralControl("<div Class=""container"">"))
                        phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))






                        phDash.Controls.Add(New LiteralControl("<div Class=""col-sm"">"))
                        phDash.Controls.Add(New LiteralControl("<span class=""text-left mr-3"">"))
                        phDash.Controls.Add(Annulla)
                        phDash.Controls.Add(New LiteralControl("</span>"))


                        phDash.Controls.Add(New LiteralControl("<span class=""text-right"">"))


                        phDash.Controls.Add(Finisci)
                        phDash.Controls.Add(hpUP)
                        phDash.Controls.Add(hpUP5)
                        phDash.Controls.Add(New LiteralControl("</span>"))


                        phDash.Controls.Add(New LiteralControl("</div>"))

                        phDash.Controls.Add(New LiteralControl("</div>"))
                        phDash.Controls.Add(New LiteralControl("</div>"))








                        phDash.Controls.Add(New LiteralControl("</div>"))

                        phDash.Controls.Add(New LiteralControl("</div>"))

                        ' fine primma riga


                        ' inizio seconda riga

                        phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))

                        phDash.Controls.Add(New LiteralControl("<div Class=""col-md-3"">"))
                        phDash.Controls.Add(New LiteralControl("<a class=""collapsed"" data-toggle=""collapse"" data-parent=""#accordionEx1"" href=""#collapseTwo" & counter1 & """ aria-expanded=""false"" aria-controls=""collapseTwo" & counter1 & """>"))
                        phDash.Controls.Add(New LiteralControl("<span class=""smb-0"">"))
                        phDash.Controls.Add(New LiteralControl("<small " & Utility.statusColorText(Data.FixNull(dr("Status_ID"))) & ">Apri/Chiudi dettaglio ordine" & "</small>"))
                        phDash.Controls.Add(New LiteralControl("</span>"))
                        phDash.Controls.Add(New LiteralControl("</a>"))
                        phDash.Controls.Add(New LiteralControl("</div>"))

                        phDash.Controls.Add(New LiteralControl("<div Class=""col-md-10"">"))


                        'phDash.Controls.Add(New LiteralControl("<div class=""progress mt-3"">"))

                        'If Data.FixNull(dr("Status_ID")) = "1" Then
                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-uno"" role=""progressbar"" style=""width: 20%"" aria-valuenow=""15"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))



                        'ElseIf Data.FixNull(dr("Status_ID")) = "2" Then

                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-uno"" role=""progressbar"" style=""width: 20%"" aria-valuenow=""15"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))
                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-due"" role=""progressbar"" style=""width: 20%"" aria-valuenow=""30"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))



                        'ElseIf Data.FixNull(dr("Status_ID")) = "3" Then
                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-uno"" role=""progressbar"" style=""width: 20%"" aria-valuenow=""15"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))
                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-due"" role=""progressbar"" style=""width: 20%"" aria-valuenow=""30"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))
                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-tre"" role=""progressbar"" style=""width: 20%"" aria-valuenow=""20"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))


                        'ElseIf Data.FixNull(dr("Status_ID")) = "4" Then
                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-uno"" role=""progressbar"" style=""width: 20%"" aria-valuenow=""15"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))
                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-due"" role=""progressbar"" style=""width: 20%"" aria-valuenow=""30"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))
                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-tre"" role=""progressbar"" style=""width: 20%"" aria-valuenow=""20"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))
                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-quattro"" role=""progressbar"" style=""width: 20%"" aria-valuenow=""20"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))

                        'ElseIf Data.FixNull(dr("Status_ID")) = "5" Then
                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-uno"" role=""progressbar"" style=""width: 20%"" aria-valuenow=""15"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))
                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-due"" role=""progressbar"" style=""width: 20%"" aria-valuenow=""30"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))
                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-tre"" role=""progressbar"" style=""width: 20%"" aria-valuenow=""20"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))
                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-quattro"" role=""progressbar"" style=""width: 20%"" aria-valuenow=""20"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))
                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-cinque"" role=""progressbar"" style=""width: 20%"" aria-valuenow=""20"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))



                        'ElseIf Data.FixNull(dr("Status_ID")) = "6" Then
                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-uno"" role=""progressbar"" style=""width: 20%"" aria-valuenow=""15"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))
                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-due"" role=""progressbar"" style=""width: 20%"" aria-valuenow=""30"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))
                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-tre"" role=""progressbar"" style=""width: 20%"" aria-valuenow=""20"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))
                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-sei"" role=""progressbar"" style=""width: 20%"" aria-valuenow=""20"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))
                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-otto"" role=""progressbar"" style=""width: 20%"" aria-valuenow=""20"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))

                        'ElseIf Data.FixNull(dr("Status_ID")) = "7" Then
                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-uno"" role=""progressbar"" style=""width: 20%"" aria-valuenow=""15"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))
                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-due"" role=""progressbar"" style=""width: 20%"" aria-valuenow=""30"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))
                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-tre"" role=""progressbar"" style=""width: 20%"" aria-valuenow=""20"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))
                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-quattro"" role=""progressbar"" style=""width: 20%"" aria-valuenow=""20"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))

                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-sette"" role=""progressbar"" style=""width: 20%"" aria-valuenow=""20"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))
                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-otto"" role=""progressbar"" style=""width: 20%"" aria-valuenow=""20"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))


                        'ElseIf Data.FixNull(dr("Status_ID")) = "8" Then
                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-uno"" role=""progressbar"" style=""width: 20%"" aria-valuenow=""15"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))
                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-due"" role=""progressbar"" style=""width: 20%"" aria-valuenow=""30"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))
                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-tre"" role=""progressbar"" style=""width: 20%"" aria-valuenow=""20"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))
                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-quattro"" role=""progressbar"" style=""width: 20%"" aria-valuenow=""20"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))


                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-sei"" role=""progressbar"" style=""width: 20%"" aria-valuenow=""20"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))
                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-otto"" role=""progressbar"" style=""width: 20%"" aria-valuenow=""20"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))


                        'Else



                        '    phDash.Controls.Add(New LiteralControl("<div Class=""progress-bar bg-secondary"" role=""progressbar"" style=""width: 25%"" aria-valuenow=""15"" aria-valuemin=""0"" aria-valuemax=""100""></div>"))




                        'End If



                        '    phDash.Controls.Add(New LiteralControl("</div>"))

                        phDash.Controls.Add(New LiteralControl("</div>"))
                        phDash.Controls.Add(New LiteralControl("</div>"))

                        'fine seconda riga

                        ' inzio terza riga




                        phDash.Controls.Add(New LiteralControl("<div Class=""row mt-3 mb-1"">"))


                        phDash.Controls.Add(New LiteralControl("<div Class=""col-md-4 text-left"">"))



                        phDash.Controls.Add(New LiteralControl("<h6>Creazione Ordine</h6>"))


                        phDash.Controls.Add(New LiteralControl("</div>"))

                        phDash.Controls.Add(New LiteralControl("<div Class=""col-md-4  text-left"">"))


                        phDash.Controls.Add(New LiteralControl("<h6>Modifica Ordine</h6>"))

                        phDash.Controls.Add(New LiteralControl("</div>"))

                        phDash.Controls.Add(New LiteralControl("<div Class=""col-md-4  text-left"">"))

                        phDash.Controls.Add(New LiteralControl("<h6>Invio Ordine</h6>"))

                        phDash.Controls.Add(New LiteralControl("</div>"))


                        phDash.Controls.Add(New LiteralControl("</div>"))

                        ' fine terza riga

                        'inizio quarta riga







                        phDash.Controls.Add(New LiteralControl("<div Class=""row mb-3"">"))


                        phDash.Controls.Add(New LiteralControl("<div Class=""col-md-4 text-left"">"))



                        phDash.Controls.Add(New LiteralControl("<h6><small>" & Data.FixNull(dr("CreationTimestamp")) & "</small></h6>"))


                        phDash.Controls.Add(New LiteralControl("</div>"))

                        phDash.Controls.Add(New LiteralControl("<div Class=""col-md-4  text-left"">"))


                        phDash.Controls.Add(New LiteralControl("<h6><small>" & Data.FixNull(dr("ModificationTimestamp")) & "</small></h6>"))

                        phDash.Controls.Add(New LiteralControl("</div>"))

                        phDash.Controls.Add(New LiteralControl("<div Class=""col-md-4  text-left"">"))

                        phDash.Controls.Add(New LiteralControl("<h6><small>" & Data.FixNull(dr("Data_Ordine")) & "</small></h6>"))

                        phDash.Controls.Add(New LiteralControl("</div>"))


                        phDash.Controls.Add(New LiteralControl("</div>"))


                        ' fine quarta riga



                        'inizio quinta riga



                        If note.Text.Length >= 2 Then



                            phDash.Controls.Add(New LiteralControl("<div Class=""row mb-3"">"))


                            phDash.Controls.Add(New LiteralControl("<div Class=""col-md-12 text-left"">"))



                            phDash.Controls.Add(New LiteralControl("<h6>Note: "))





                            phDash.Controls.Add(note)


                            phDash.Controls.Add(New LiteralControl("</h6></div>"))




                            phDash.Controls.Add(New LiteralControl("</div>"))


                            ' fine quinta riga

                        End If


                        'inizio sesta riga



                        If noteAsi.Text.Length >= 2 Then



                            phDash.Controls.Add(New LiteralControl("<div Class=""row mb-3"">"))


                            phDash.Controls.Add(New LiteralControl("<div Class=""col-md-12 text-left"">"))



                            phDash.Controls.Add(New LiteralControl("<h6>Note ASI: "))





                            phDash.Controls.Add(noteAsi)


                            phDash.Controls.Add(New LiteralControl("</h6></div>"))




                            phDash.Controls.Add(New LiteralControl("</div>"))


                            ' fine sesta riga

                        End If




                        '      phDash.Controls.Add(New LiteralControl("</div>"))
                        phDash.Controls.Add(New LiteralControl("</div>"))


                            phDash.Controls.Add(New LiteralControl("</div>"))









                            ' card body
                            phDash.Controls.Add(New LiteralControl(" <div id=""collapseTwo" & counter1 & """ class=""collapse"" role=""tabpanel"" aria-labelledby=""headingTwo" & counter1 & """ data-parent=""#accordionEx1"">"))


                            phDash.Controls.Add(New LiteralControl("<div class=""card-body"">"))
                            '  phDash.Controls.Add(New LiteralControl("<h5 class=""card-title""> " & Data.FixNull(dr("Status")) & " </h5>"))

                            phDash.Controls.Add(New LiteralControl("<p class=""card-text"">"))


                            '   phDash.Controls.Add(New LiteralControl("<p>ciao"))

                            Dim deTtotali As New GetTotali()

                            deTtotali = GetTotaliRichiesta(dr("Codice_Richiesta"))

                            Dim ImportoRighe As Decimal = deTtotali.ImportoRighe
                            Dim ImportoSconto As Decimal = deTtotali.ImportoSconto
                            Dim ImportoTessere As Decimal = deTtotali.ImportoTessere
                            Dim totalesconto As Decimal = ImportoSconto

                            Dim DaPagare As Decimal = CType(ImportoRighe, Decimal) - CType(ImportoSconto, Decimal)

                            Dim fmsD As FMSAxml = AsiModel.Conn.Connect()
                            fmsD.SetLayout("web_richiesta_dettaglio")
                            Dim ds1 As DataSet




                            Dim RequestD = fmsD.CreateFindRequest(Enumerations.SearchType.Subset)
                        RequestD.AddSearchField("Codice_Richiesta", dr("Codice_Richiesta"), Enumerations.SearchOption.equals)

                        ds1 = RequestD.Execute()

                            If Not IsNothing(ds1) AndAlso ds1.Tables("main").Rows.Count > 0 Then


                                Dim counter As Integer = 0
                                Dim totale As Decimal = 0
                                Dim tessereTotale As Integer = 0

                                phDash.Controls.Add(New LiteralControl("<div class=""table-responsive-md rounded"">"))
                                phDash.Controls.Add(New LiteralControl("<Table class=""table table-striped table-fit"">"))

                                phDash.Controls.Add(New LiteralControl("<thead class=""thead-dark"">"))


                                phDash.Controls.Add(New LiteralControl("<tr>"))


                                phDash.Controls.Add(New LiteralControl("<th scope = ""col"" class="""">Articolo</th>"))
                                phDash.Controls.Add(New LiteralControl("<th scope = ""col"" class="""" >Quantità</th>"))
                                phDash.Controls.Add(New LiteralControl("<th scope = ""col"" Class=""text-right"">Pr.Un.</th>"))
                                phDash.Controls.Add(New LiteralControl("<th scope = ""col"" Class=""text-right"">Pr.Fi.</th>"))
                                phDash.Controls.Add(New LiteralControl("</tr>"))
                                phDash.Controls.Add(New LiteralControl("</thead>"))
                                phDash.Controls.Add(New LiteralControl("<tbody>"))






                                For Each dr1 In ds1.Tables("main").Rows
                                    counter += 1
                                    totalesconto += totalesconto


                                    Dim price As Decimal = dr1("Prezzo_Finale")
                                    Dim prezzoUnitario As Decimal = dr1("Prezzo_Unitario_Listino")


                                    Dim tessere As Integer = dr1("Quantita")
                                    totale += price
                                    tessereTotale += tessere
                                    phDash.Controls.Add(New LiteralControl("<tr>"))
                                    '      phDash.Controls.Add(New LiteralControl("<th scope ='row'>"))





                                    '      phDash.Controls.Add(New LiteralControl("<td>" & Data.FixNull(dr1("Codice_Articolo")) & "</td>"))
                                    phDash.Controls.Add(New LiteralControl("<td><strong>" & Data.FixNull(dr1("Nome_Articolo")) & "</strong></td>"))
                                    phDash.Controls.Add(New LiteralControl("<td width=""12%"" class=' text-center'>" & Data.FixNull(dr1("Quantita")) & "</td>"))
                                    phDash.Controls.Add(New LiteralControl("<td class=' text-right'>" & prezzoUnitario.ToString("C2", New System.Globalization.CultureInfo("it-IT")) & "</td>"))
                                    phDash.Controls.Add(New LiteralControl("<td class=' text-right'>" & price.ToString("C2", New System.Globalization.CultureInfo("it-IT")) & "</td>"))
                                    phDash.Controls.Add(New LiteralControl("</tr>"))

                                Next

                                If totalesconto <> 0 Then
                                    phDash.Controls.Add(New LiteralControl("<tr>"))
                                    phDash.Controls.Add(New LiteralControl("<td></td>"))
                                    phDash.Controls.Add(New LiteralControl("<td></td>"))
                                    phDash.Controls.Add(New LiteralControl("<td class='text-right'><b>Importo Totale</b></td>"))
                                    phDash.Controls.Add(New LiteralControl("<td class='text-right'>" & ImportoRighe.ToString("C2", New System.Globalization.CultureInfo("it-IT")) & "</td>"))
                                    phDash.Controls.Add(New LiteralControl("</tr>"))


                                    phDash.Controls.Add(New LiteralControl("<tr>"))
                                    phDash.Controls.Add(New LiteralControl("<td></td>"))
                                    phDash.Controls.Add(New LiteralControl("<td></td>"))
                                    phDash.Controls.Add(New LiteralControl("<td class='text-right'><b>Importo Tessere</b></td>"))
                                    phDash.Controls.Add(New LiteralControl("<td class='text-right'>" & ImportoTessere.ToString("C2", New System.Globalization.CultureInfo("it-IT")) & "</td>"))
                                    phDash.Controls.Add(New LiteralControl("</tr>"))


                                    phDash.Controls.Add(New LiteralControl("<tr>"))
                                    phDash.Controls.Add(New LiteralControl("<td></td>"))
                                    phDash.Controls.Add(New LiteralControl("<td></td>"))
                                    phDash.Controls.Add(New LiteralControl("<td class='text-right'><b>Sconto</b></td>"))
                                    phDash.Controls.Add(New LiteralControl("<td class='text-right'>" & ImportoSconto.ToString("C2", New System.Globalization.CultureInfo("it-IT")) & "</td>"))
                                    phDash.Controls.Add(New LiteralControl("</tr>"))

                                    phDash.Controls.Add(New LiteralControl("<tr>"))
                                    '    phDash.Controls.Add(New LiteralControl("<th scope ='row'></th>"))

                                    '  phDash.Controls.Add(New LiteralControl("<td></td>"))
                                    phDash.Controls.Add(New LiteralControl("<td></td>"))
                                    phDash.Controls.Add(New LiteralControl("<td></td>"))
                                    phDash.Controls.Add(New LiteralControl("<td class='text-right'><b>Importo Ordine</b></td>"))
                                    phDash.Controls.Add(New LiteralControl("<td class='text-right'>" & DaPagare.ToString("C2", New System.Globalization.CultureInfo("it-IT")) & "</td>"))
                                    phDash.Controls.Add(New LiteralControl("</tr>"))

                                    phDash.Controls.Add(New LiteralControl("</tbody>"))
                                    phDash.Controls.Add(New LiteralControl("</table>"))
                                    phDash.Controls.Add(New LiteralControl("</div>"))

                                Else

                                    phDash.Controls.Add(New LiteralControl("<tr>"))
                                    '    phDash.Controls.Add(New LiteralControl("<th scope ='row'></th>"))

                                    '  phDash.Controls.Add(New LiteralControl("<td></td>"))
                                    phDash.Controls.Add(New LiteralControl("<td></td>"))
                                    phDash.Controls.Add(New LiteralControl("<td></td>"))
                                    phDash.Controls.Add(New LiteralControl("<td class='text-right'><b>Importo Ordine</b></td>"))
                                    phDash.Controls.Add(New LiteralControl("<td class='text-right'>" & DaPagare.ToString("C2", New System.Globalization.CultureInfo("it-IT")) & "</td>"))
                                    phDash.Controls.Add(New LiteralControl("</tr>"))

                                    phDash.Controls.Add(New LiteralControl("</tbody>"))
                                    phDash.Controls.Add(New LiteralControl("</table>"))
                                    phDash.Controls.Add(New LiteralControl("</div>"))

                                End If




                            End If



                            '   phDash.Controls.Add(New LiteralControl("</p>"))
                            phDash.Controls.Add(New LiteralControl("</p>"))

                            phDash.Controls.Add(New LiteralControl("</div>"))
                            phDash.Controls.Add(New LiteralControl("</div>"))
                            phDash.Controls.Add(New LiteralControl("</div>"))
                            phDash.Controls.Add(New LiteralControl("</div>"))
                            phDash.Controls.Add(New LiteralControl("</div>"))
                        Else   '      

                        End If

                End If
            Next

        End If



        '   Catch


        '   End Try



    End Sub








End Class