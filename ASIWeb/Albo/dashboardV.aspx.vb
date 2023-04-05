Imports fmDotNet
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed
Imports System.Net

Public Class dashboardV
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



        End If
        If Not Page.IsPostBack Then


            Corsi()





        End If
    End Sub
    Private Shared Function GetCode(ByVal text As String) As String
        Return text.Split(New Char() {"."c})(0).Last().ToString()
    End Function
    Sub Corsi()


        Dim ds As DataSet
        Dim pdf As String
        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("webCorsiRichiesta")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        ' RequestP.AddSearchField("pre_stato_web", "1")
        RequestP.AddSearchField("Settore_Approvazione_ID", Session("codice"), Enumerations.SearchOption.equals)
        RequestP.AddSortField("Codice_Status", Enumerations.Sort.Ascend)
        RequestP.AddSortField("IDCorso", Enumerations.Sort.Descend)



        ds = RequestP.Execute()

        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then




            phDash.Visible = True
            'Dim counter As Integer = 0
            Dim counter1 As Integer = 0
            'Dim totale As Decimal = 0
            For Each dr In ds.Tables("main").Rows


                If Data.FixNull(dr("Codice_Status")) = "63" Then


                    counter1 += 1

                    '  pdf = FotoS("https://crm.asinazionale.it/fmi/xml/cnt/ " & Data.FixNull(dr("NomeFileOnFS")) & "?-db=Asi&-lay=webCorsiRichiesta&-recid=" & Data.FixNull(dr("IDCorso")) & "&-field=Programma_Tecnico_Didattico(1)")



                    Dim deEnco As New Ed()
                    Dim nomeFile As String
                    Dim nomeFile2 As String
                    Dim nomeFile3 As String
                    'Dim extension As String
                    'Dim extension2 As String
                    'Dim extension3 As String

                    'Dim PianoCorso As New LinkButton
                    'Dim PianoCorso2 As New LinkButton
                    'Dim PianoCorso3 As New LinkButton

                    nomeFile = Data.FixNull(dr("NomeFileOnFS"))
                    nomeFile2 = Data.FixNull(dr("NomeFileOnFS2"))
                    nomeFile3 = Data.FixNull(dr("NomeFileOnFS3"))
                    'extension = nomeFile.Split(".").Last().ToString()

                    If String.IsNullOrWhiteSpace(nomeFile) Then
                        nomeFile = Data.FixNull(dr("NomeFileOnFSFromFM"))
                        'extension = nomeFile.Split(".").Last().ToString()
                    End If
                    If String.IsNullOrWhiteSpace(nomeFile2) Then
                        nomeFile2 = Data.FixNull(dr("NomeFileOnFSFromFM2"))
                        'extension = nomeFile.Split(".").Last().ToString()
                    End If
                    If String.IsNullOrWhiteSpace(nomeFile3) Then
                        nomeFile3 = Data.FixNull(dr("NomeFileOnFSFromFM3"))
                        'extension = nomeFile.Split(".").Last().ToString()
                    End If

                    'caricato solo il documento corso







                    Dim Ann As New LinkButton

                    Ann.ID = "ann_" & counter1
                    Ann.Attributes.Add("runat", "server")
                    Ann.Text = "<i class=""bi bi-hand-index""> </i>Valuta Corso"
                    Ann.PostBackUrl = "valutaCorso.aspx?codR=" &
                        WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("IDCorso")))) & "&record_ID=" &
                        WebUtility.UrlEncode(deEnco.QueryStringEncode(dr("id_record")))
                    Ann.CssClass = "btn btn-success btn-sm btn-uno btn-custom mb-2"
                    '    Ann.Attributes.Add("OnClick", "if(!myValuta())return false;")


                    'If String.IsNullOrWhiteSpace(nomeFile) Then



                    '    PianoCorso.ID = "vediPianoCorso_" & counter1
                    '    PianoCorso.Attributes.Add("runat", "server")
                    '    PianoCorso.Text = "<i class=""bi bi-house-door""> </i>Scarica Piano Corso"
                    '    PianoCorso.PostBackUrl = "scaricaPianoCorso.aspx?codR=" & WebUtility.UrlEncode(dr("IDCorso")) & "&nomeFilePC=" & WebUtility.UrlEncode(nomeFile)
                    '    PianoCorso.CssClass = "btn btn-success btn-sm btn-due btn-custom mb-2"

                    'End If


                    'If String.IsNullOrWhiteSpace(nomeFile2) Then

                    '    PianoCorso2.ID = "vediPianoCorso_" & counter1
                    '    PianoCorso2.Attributes.Add("runat", "server")
                    '    PianoCorso2.Text = "<i class=""bi bi-house-door""> </i>Scarica Documento"
                    '    PianoCorso2.PostBackUrl = "scaricaPianoCorso.aspx?codR=" & WebUtility.UrlEncode(dr("IDCorso")) & "&nomeFilePC=" & WebUtility.UrlEncode(nomeFile)
                    '    PianoCorso2.CssClass = "btn btn-success btn-sm btn-due btn-custom mb-2"


                    'End If


                    'If String.IsNullOrWhiteSpace(nomeFile3) Then

                    '    PianoCorso3.ID = "vediPianoCorso_" & counter1
                    '    PianoCorso3.Attributes.Add("runat", "server")
                    '    PianoCorso3.Text = "<i class=""bi bi-house-door""> </i>Scarica Documento"
                    '    PianoCorso3.PostBackUrl = "scaricaPianoCorso.aspx?codR=" & WebUtility.UrlEncode(dr("IDCorso")) & "&nomeFilePC=" & WebUtility.UrlEncode(nomeFile)
                    '    PianoCorso3.CssClass = "btn btn-success btn-sm btn-due btn-custom mb-2"



                    'End If






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
                    phDash.Controls.Add(New LiteralControl("<a name=" & Data.FixNull(dr("IDCorso")) & ">" & Data.FixNull(dr("IDCorso")) & "</a><br />"))
                    '    phDash.Controls.Add(New LiteralControl("</span>"))


                    phDash.Controls.Add(New LiteralControl())


                    phDash.Controls.Add(New LiteralControl("</span></div>"))

                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-5  text-left"">"))

                    phDash.Controls.Add(New LiteralControl("</span><small>Status: </small><small " & Utility.statusColorTextCorsi(Data.FixNull(dr("Codice_Status"))) & ">" & Data.FixNull(dr("Descrizione_StatusWeb")) & "</small>"))

                    phDash.Controls.Add(New LiteralControl("</div>"))


                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-right"">"))


                    phDash.Controls.Add(Ann)

                    '     "scaricaPianoCorso.aspx?codR=" & deEnco.QueryStringEncode(Data.FixNull(dr("IDCorso"))) & "&record_ID=" & deEnco.QueryStringEncode(dr("id_record")) & "&nomeFilePC=" & deEnco.QueryStringEncode(nomeFile)



                    '
                    If Not String.IsNullOrWhiteSpace(nomeFile) Then

                        phDash.Controls.Add(New LiteralControl("<a class=""btn btn-success btn-sm btn-due btn-custom mb-2"" href='scaricaPianoCorso.aspx?s=1&codR=" &
                                                           dr("ID_Record") & "&nomeFilePC=" &
                                                           nomeFile & "'><i class=""bi bi-download""> </i>Piano Corso</a>"))


                    End If

                    If Not String.IsNullOrWhiteSpace(nomeFile2) Then

                        phDash.Controls.Add(New LiteralControl("<a class=""btn btn-success btn-sm btn-due btn-custom mb-2"" href='scaricaPianoCorso.aspx?s=2&codR=" &
                                                           dr("ID_Record") & "&nomeFilePC=" &
                                                           nomeFile2 & "'><i class=""bi bi-download""> </i>Altro Documento</a>"))


                    End If



                    If Not String.IsNullOrWhiteSpace(nomeFile3) Then

                        phDash.Controls.Add(New LiteralControl("<a class=""btn btn-success btn-sm btn-due btn-custom mb-2"" href='scaricaPianoCorso.aspx?s=3&codR=" &
                                                           dr("ID_Record") & "&nomeFilePC=" &
                                                           nomeFile3 & "'><i class=""bi bi-download""> </i>Altro Documento</a>"))


                    End If



                    ' phDash.Controls.Add(New LiteralControl("<a class=""btn btn-success btn-sm btn-due btn-custom mb-2"" href='scaricaPianoCorso.aspx?codR=" &
                    '  WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("IDCorso")))) & "&nomeFilePC=" &
                    '  WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(nomeFile))) & "'><i class=""bi bi-download""> </i>Piano Corso</a>"))

                    'phDash.Controls.Add(PianoCorso)


                    phDash.Controls.Add(New LiteralControl("</div>"))





                    phDash.Controls.Add(New LiteralControl("</div>"))











                    ' fine primma riga



                    If Data.FixNull(dr("fase")) <> "1" Then

                        'inizio seconda riga

                        ' <h5>h5 heading <small>secondary text</small></h5>

                        phDash.Controls.Add(New LiteralControl("<div class=""row"">"))

                        phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-7 text-left"">"))

                        phDash.Controls.Add(New LiteralControl("<h6>Nome Corso: <span><small>" & Data.FixNull(dr("Titolo_Corso")) & "</small></h6><span />"))


                        phDash.Controls.Add(New LiteralControl("</div>"))

                        phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-5 text-left"">"))



                        phDash.Controls.Add(New LiteralControl("</div>"))

                        phDash.Controls.Add(New LiteralControl("</div>"))


                        phDash.Controls.Add(New LiteralControl("<div class=""row"">"))

                        phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-7 text-left"">"))

                        phDash.Controls.Add(New LiteralControl("<h6>Comitato richiedente: <span><small>" & Data.FixNull(dr("Codice_Ente_Richiedente")) & "_" & Data.FixNull(dr("Descrizione_Ente_Richiedente")) & "</small></h6><span />"))


                        phDash.Controls.Add(New LiteralControl("</div>"))

                        phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-5 text-left"">"))



                        phDash.Controls.Add(New LiteralControl("</div>"))

                        phDash.Controls.Add(New LiteralControl("</div>"))




                        'phDash.Controls.Add(New LiteralControl("Comitato:  "))
                        'phDash.Controls.Add(New LiteralControl("<span  " & Data.FixNull(dr("Codice_Ente_Richiedente")) & "_" & Data.FixNull(dr("Descrizione_Ente_Richiedente")) & ">"))




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

                        phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-3 text-left"">"))

                        phDash.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Data Richiesta: <span><small> " & Data.FixNull(dr("IndicatoreDataOraCreazione")) & "</small></h6><span />"))


                        phDash.Controls.Add(New LiteralControl("</div>"))
                        phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-3 text-left"">"))

                        phDash.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Data Emissione: <span><small> " & SonoDieci(Data.FixNull(dr("Data_Emissione"))) & "</small></h6><span />"))
                        phDash.Controls.Add(New LiteralControl("</div>"))

                        phDash.Controls.Add(New LiteralControl("</div>"))




                        'fine terza riga

                        If Data.FixNull(dr("fase")) = "3" Then
                            'inizio quarta riga



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

                            phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))


                            phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-12 text-left"">"))


                            phDash.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Docenti: <span>"))
                            phDash.Controls.Add(New LiteralControl("<small>" & Data.FixNull(dr("Elenco_Docenti"))))

                            phDash.Controls.Add(New LiteralControl("</small></h6></span></div>"))



                            phDash.Controls.Add(New LiteralControl("</div>"))



                            'fine sesta riga sottosezione

                            ' inizio sesta riga 


                            phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))


                            phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-12 text-left"">"))


                            phDash.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Commissione: <span>"))
                            phDash.Controls.Add(New LiteralControl("<small>" & Data.FixNull(dr("Elenco_Componenti_Commissione"))))

                            phDash.Controls.Add(New LiteralControl("</small></h6></span></div>"))



                            phDash.Controls.Add(New LiteralControl("</div>"))

                            'intermezzo
                            phDash.Controls.Add(New LiteralControl("<div class=""row"">"))

                            phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-12 text-left"">"))

                            phDash.Controls.Add(New LiteralControl("<hr>"))

                            phDash.Controls.Add(New LiteralControl("</div>"))



                            phDash.Controls.Add(New LiteralControl("</div>"))

                            'fine sesta riga sottosezione

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



                            'fine ottava riga sottosezione



                        End If

                    End If

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