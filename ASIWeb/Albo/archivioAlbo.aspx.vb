Imports fmDotNet
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed
Imports System.Net
Imports DocumentFormat.OpenXml.InkML
Imports System.Security.Policy

Public Class archivioAlbo
    Inherits System.Web.UI.Page
    Dim deEnco As New Ed()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If
        If IsNothing(Session("codice")) Then
            Response.Redirect("../login.aspx")
        End If

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
                    Case "noResult"
                        Session("AnnullaREqui") = Nothing
                        showScript = "toastr.success('Non è stato trovato alcun Corso', 'ASI');"


                End Select

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showSuccess", customizeScript & showScript, True)



            End If
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

    Protected Sub lnkLast10_Click(sender As Object, e As EventArgs) Handles lnkLast10.Click
        Corsi()
    End Sub
    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click


        If Page.IsValid Then

            Dim ds As DataSet

            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            fmsP.SetLayout("webCorsiRichiesta")
            Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            ' RequestP.AddSearchField("pre_stato_web", "1")
            RequestP.AddSearchField("Codice_Ente_Richiedente", Session("codice"), Enumerations.SearchOption.equals)
            RequestP.AddSearchField("Codice_Status", "84")
            RequestP.AddSearchField("IDCorso", txtNumeroCorso.Text, Enumerations.SearchOption.equals)
            'RequestP.AddSortField("Codice_Status", Enumerations.Sort.Ascend)
            'RequestP.AddSortField("IDCorso", Enumerations.Sort.Descend)
            'RequestP.SetMax(10)


            ds = RequestP.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then




                phDash.Visible = True
                'Dim counter As Integer = 0
                Dim counter1 As Integer = 0
                'Dim totale As Decimal = 0
                For Each dr In ds.Tables("main").Rows
                    Dim oldStatus As String = Data.FixNull(dr("StatusPrimaCaricamentoXL"))

                    counter1 += 1




                    Dim deEnco As New Ed()




                    Dim CorsistiDoc As New LinkButton

                    CorsistiDoc.ID = "CorsistiDoc_" & counter1
                    CorsistiDoc.Attributes.Add("CausesValidation", "false")
                    CorsistiDoc.Attributes.Add("runat", "server")
                    CorsistiDoc.Text = "<i class=""bi bi-file-earmark-text""> </i>Corsisti Documenti"
                    CorsistiDoc.PostBackUrl = "corsistiDoc.aspx?codR=" &
                            WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("IDCorso")))) &
                            "&record_ID=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(dr("id_record"))) & "&oldStatus=" &
                            WebUtility.UrlEncode(deEnco.QueryStringEncode(oldStatus))
                    CorsistiDoc.CssClass = "btn btn-success btn-sm btn-otto btn-custom mb-2"
                    If (Data.FixNull(dr("Codice_Status")) = "84" And Data.FixNull(dr("fase")) = "3") Then
                        CorsistiDoc.Visible = True
                    Else
                        CorsistiDoc.Visible = False
                    End If


                    Dim Duplica As New LinkButton

                    Duplica.ID = "Duplica" & counter1
                    Duplica.Attributes.Add("runat", "server")
                    Duplica.Text = "<i class=""bi bi-front""> </i>Duplica Corso"
                    Duplica.PostBackUrl = "Duplica.aspx?codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("IDCorso")))) &
                            "&record_ID=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(dr("id_record"))) & "&oldStatus=" &
                            WebUtility.UrlEncode(deEnco.QueryStringEncode(oldStatus))
                    Duplica.CssClass = "btn btn-success btn-sm btn-otto btn-custom mb-2"
                    If (Data.FixNull(dr("Codice_Status")) = "84" And Data.FixNull(dr("fase")) = "3") Then
                        Duplica.Visible = True
                    Else
                        Duplica.Visible = False
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

                    phDash.Controls.Add(New LiteralControl("</span><small>Status: </small><small " & Utility.statusColorTextCorsi(Data.FixNull(dr("Codice_Status"))) & ">" & Data.FixNull(dr("Descrizione_StatusWeb")) & "</small>"))

                    phDash.Controls.Add(New LiteralControl("</div>"))


                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left"">"))





                    phDash.Controls.Add(Duplica)
                    phDash.Controls.Add(CorsistiDoc)

                    ' phDash.Controls.Add(fotoCorsisti)



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

                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-3 text-left"">"))

                    phDash.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Data Richiesta: <span><small> " & Data.FixNull(dr("IndicatoreDataOraCreazione")) & "</small></h6><span />"))


                    phDash.Controls.Add(New LiteralControl("</div>"))

                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-3 text-left"">"))

                    phDash.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Data Emissione: <span><small> " & SonoDieci(Data.FixNull(dr("Data_Emissione"))) & "</small></h6><span />"))
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
                        quota = Data.FixNull(dr("Quota_Partecipazione"))
                    End If









                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-3 text-left"">"))

                    '    phDash.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Quota: <span><small> " & quota.ToString("C2", New Globalization.CultureInfo("it-IT")) & "</small></h6><span />"))
                    phDash.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Quota: <span><small> " & quota.ToString("N2") & "</small></h6><span />"))


                    phDash.Controls.Add(New LiteralControl("</div>"))

                    phDash.Controls.Add(New LiteralControl("</div>"))



                    ' inizio sesta riga 


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
                    '   End If
                Next
            Else

                phDash.Visible = True
                phDash.Controls.Add(New LiteralControl("<div class=""col-sm-10 mb-3 mb-md-0"">"))



                'accordion card
                phDash.Controls.Add(New LiteralControl("<div class=""card mb-2 shadow-sm rounded"">"))
                'accordion heder
                phDash.Controls.Add(New LiteralControl("<div class=""card-header"">"))

                phDash.Controls.Add(New LiteralControl("<div Class=""container-fluid"">"))

                ' inizio prima riga

                phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))


                phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-3 text-left"">"))

                phDash.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Nessun risultato<span>"))

                phDash.Controls.Add(New LiteralControl("</div>"))

                phDash.Controls.Add(New LiteralControl("</div>"))



                phDash.Controls.Add(New LiteralControl("</div>"))

                phDash.Controls.Add(New LiteralControl("</div>"))

                phDash.Controls.Add(New LiteralControl("</div>"))
                phDash.Controls.Add(New LiteralControl("</div>"))
            End If

        End If
    End Sub

    Sub Corsi()


        Dim ds As DataSet

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("webCorsiRichiesta")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        ' RequestP.AddSearchField("pre_stato_web", "1")
        RequestP.AddSearchField("Codice_Ente_Richiedente", Session("codice"), Enumerations.SearchOption.equals)
        RequestP.AddSearchField("Codice_Status", "84")

        RequestP.AddSortField("Codice_Status", Enumerations.Sort.Ascend)
        RequestP.AddSortField("IDCorso", Enumerations.Sort.Descend)
        RequestP.SetMax(10)


        ds = RequestP.Execute()

        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then




            phDash10.Visible = True
            'Dim counter As Integer = 0
            Dim counter1 As Integer = 0
            'Dim totale As Decimal = 0
            For Each dr In ds.Tables("main").Rows
                Dim oldStatus As String = Data.FixNull(dr("StatusPrimaCaricamentoXL"))


                counter1 += 1




                Dim deEnco As New Ed()




                'Dim CorsistiDoc As New LinkButton

                'CorsistiDoc.ID = "CorsistiDoc_" & counter1
                'CorsistiDoc.Attributes.Add("CausesValidation", "False")
                'CorsistiDoc.Attributes.Add("runat", "server")
                'CorsistiDoc.Text = "<i class=""bi bi-file-earmark-text""> </i>Corsisti Documenti"
                'CorsistiDoc.PostBackUrl = "corsistiDoc.aspx?codR=" &
                '        WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("IDCorso")))) &
                '        "&record_ID=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(dr("id_record"))) & "&oldStatus=" &
                '        WebUtility.UrlEncode(deEnco.QueryStringEncode(oldStatus))
                'CorsistiDoc.CssClass = "btn btn-success btn-sm btn-otto btn-custom mb-2"
                'If (Data.FixNull(dr("Codice_Status")) = "84" And Data.FixNull(dr("fase")) = "3") Then
                '    CorsistiDoc.Visible = True
                'Else
                '    CorsistiDoc.Visible = False
                'End If



                'Dim Duplica As New LinkButton

                'Duplica.ID = "Duplica" & counter1
                'Duplica.Attributes.Add("runat", "server")
                'Duplica.Text = "<i class=""bi bi-front""> </i>Duplica Corso"
                'Duplica.PostBackUrl = "Duplica.aspx?codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("IDCorso")))) &
                '        "&record_ID=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(dr("id_record"))) & "&oldStatus=" &
                '        WebUtility.UrlEncode(deEnco.QueryStringEncode(oldStatus))
                'Duplica.CssClass = "btn btn-success btn-sm btn-otto btn-custom mb-2"
                'If (Data.FixNull(dr("Codice_Status")) = "84" And Data.FixNull(dr("fase")) = "3") Then
                '    Duplica.Visible = True
                'Else
                '    Duplica.Visible = False
                'End If


                phDash10.Controls.Add(New LiteralControl("<div class=""col-sm-10 mb-3 mb-md-0"">"))



                'accordion card
                phDash10.Controls.Add(New LiteralControl("<div class=""card mb-2 shadow-sm rounded"">"))
                'accordion heder
                phDash10.Controls.Add(New LiteralControl("<div class=""card-header"">"))

                phDash10.Controls.Add(New LiteralControl("<div Class=""container-fluid"">"))

                ' inizio prima riga

                phDash10.Controls.Add(New LiteralControl("<div Class=""row"">"))


                phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-3 text-left"">"))

                phDash10.Controls.Add(New LiteralControl("Corso:  "))
                phDash10.Controls.Add(New LiteralControl("<span  " & Utility.statusColorCorsi(Data.FixNull(dr("Codice_Status"))) & ">"))
                phDash10.Controls.Add(New LiteralControl("<a name=" & Data.FixNull(dr("IDCorso")) & ">" & Data.FixNull(dr("IDCorso")) & "</a>"))
                phDash10.Controls.Add(New LiteralControl())


                phDash10.Controls.Add(New LiteralControl("</span></div>"))

                phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-5  text-left"">"))

                phDash10.Controls.Add(New LiteralControl("</span><small>Status: </small><small " & Utility.statusColorTextCorsi(Data.FixNull(dr("Codice_Status"))) & ">" & Data.FixNull(dr("Descrizione_StatusWeb")) & "</small>"))

                phDash10.Controls.Add(New LiteralControl("</div>"))


                phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left"">"))

                If (Data.FixNull(dr("Codice_Status")) = "84" And Data.FixNull(dr("fase")) = "3") Then
                    phDash10.Controls.Add(New LiteralControl("<a class=""btn btn-success btn-sm btn-otto btn-custom mb-2""   href='Duplica.aspx?codR=" _
                             & deEnco.QueryStringEncode(Data.FixNull(dr("IDCorso"))) & "&record_ID=" & deEnco.QueryStringEncode(dr("id_record")) & "&oldStatus=" &
                       deEnco.QueryStringEncode(oldStatus) & "'><i class=""bi bi-person-badge""> </i>Duplica Corso</a>"))
                End If

                'phDash10.Controls.Add(Duplica)
                If (Data.FixNull(dr("Codice_Status")) = "84" And Data.FixNull(dr("fase")) = "3") Then
                    phDash10.Controls.Add(New LiteralControl("<a class=""btn btn-success btn-sm btn-otto btn-custom mb-2""   href='corsistiDoc.aspx?codR=" _
                             & deEnco.QueryStringEncode(Data.FixNull(dr("IDCorso"))) & "&record_ID=" & deEnco.QueryStringEncode(dr("id_record")) & "&oldStatus=" &
                       deEnco.QueryStringEncode(oldStatus) & "'><i class=""bi bi-person-badge""> </i>Corsisti Documenti</a>"))

                End If


                'phDash10.Controls.Add(CorsistiDoc)

                ' phDash10.Controls.Add(fotoCorsisti)



                phDash10.Controls.Add(New LiteralControl("</div>"))





                phDash10.Controls.Add(New LiteralControl("</div>"))











                ' fine primma riga





                'inizio seconda riga

                ' <h5>h5 heading <small>secondary text</small></h5>

                phDash10.Controls.Add(New LiteralControl("<div class=""row"">"))

                phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-7 text-left"">"))

                phDash10.Controls.Add(New LiteralControl("<h6>Nome Corso: <span><small>" & Data.FixNull(dr("Titolo_Corso")) & "</small></h6><span />"))


                phDash10.Controls.Add(New LiteralControl("</div>"))

                phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-5 text-left"">"))



                phDash10.Controls.Add(New LiteralControl("</div>"))

                phDash10.Controls.Add(New LiteralControl("</div>"))


                'intermezzo
                phDash10.Controls.Add(New LiteralControl("<div class=""row"">"))

                phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-12 text-left"">"))

                phDash10.Controls.Add(New LiteralControl("<hr>"))

                phDash10.Controls.Add(New LiteralControl("</div>"))



                phDash10.Controls.Add(New LiteralControl("</div>"))

                'fine seconda riga


                'inizio seconda riga bis

                phDash10.Controls.Add(New LiteralControl("<div class=""row"">"))

                phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-8 text-left"">"))

                phDash10.Controls.Add(New LiteralControl("<h6>Indirizzo: <span><small> " & Data.FixNull(dr("Indirizzo_Svolgimento")) & " " & Data.FixNull(dr("Cap_Svolgimento")) & " " & Data.FixNull(dr("Comune_Svolgimento")) & " " & Data.FixNull(dr("PR_Svolgimento")) & "</small></h6><span />"))


                phDash10.Controls.Add(New LiteralControl("</div>"))

                phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left"">"))



                phDash10.Controls.Add(New LiteralControl("</div>"))

                phDash10.Controls.Add(New LiteralControl("</div>"))


                'fine seconda riga bis




                'inizio terza riga



                phDash10.Controls.Add(New LiteralControl("<div class=""row"">"))

                phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-3 text-left"">"))

                phDash10.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Da: <span><small> " & SonoDieci(Data.FixNull(dr("Svolgimento_da_Data"))) & "</small></h6><span />"))


                phDash10.Controls.Add(New LiteralControl("</div>"))

                phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-3 text-left"">"))

                phDash10.Controls.Add(New LiteralControl("<h6 class=""piccolo"">A: <span><small> " & SonoDieci(Data.FixNull(dr("Svolgimento_a_Data"))) & "</small></h6><span />"))


                phDash10.Controls.Add(New LiteralControl("</div>"))

                phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-3 text-left"">"))

                phDash10.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Data Richiesta: <span><small> " & Data.FixNull(dr("IndicatoreDataOraCreazione")) & "</small></h6><span />"))


                phDash10.Controls.Add(New LiteralControl("</div>"))

                phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-3 text-left"">"))

                phDash10.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Data Emissione: <span><small> " & SonoDieci(Data.FixNull(dr("Data_Emissione"))) & "</small></h6><span />"))
                phDash10.Controls.Add(New LiteralControl("</div>"))

                phDash10.Controls.Add(New LiteralControl("</div>"))




                'fine terza riga





                phDash10.Controls.Add(New LiteralControl("<div class=""row"">"))

                phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left"">"))

                phDash10.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Sport: <span><small> " & Data.FixNull(dr("Sport_Interessato")) & "</small></h6><span />"))


                phDash10.Controls.Add(New LiteralControl("</div>"))

                phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left"">"))

                phDash10.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Disciplina: <span><small> " & Data.FixNull(dr("Disciplina_interessata")) & "</small></h6><span />"))


                phDash10.Controls.Add(New LiteralControl("</div>"))

                phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left"">"))

                phDash10.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Specialità: <span><small> " & Data.FixNull(dr("Specialita")) & "</small></h6><span />"))


                phDash10.Controls.Add(New LiteralControl("</div>"))

                phDash10.Controls.Add(New LiteralControl("</div>"))




                'fine quarta riga

                'inizio quinta riga



                phDash10.Controls.Add(New LiteralControl("<div class=""row"">"))

                phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-3 text-left"">"))

                phDash10.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Qualifica: <span><small> " & Data.FixNull(dr("Qualifica_Tecnica_Da_Rilasciare")) & "</small></h6><span />"))


                phDash10.Controls.Add(New LiteralControl("</div>"))

                phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-3 text-left"">"))

                phDash10.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Livello: <span><small> " & Data.FixNull(dr("Livello")) & "</small></h6><span />"))


                phDash10.Controls.Add(New LiteralControl("</div>"))



                Dim quota As Decimal
                If String.IsNullOrEmpty(Data.FixNull(dr("Quota_Partecipazione"))) Then
                    quota = 0
                Else
                    quota = Data.FixNull(dr("Quota_Partecipazione"))
                End If









                phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-3 text-left"">"))

                '    phDash10.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Quota: <span><small> " & quota.ToString("C2", New Globalization.CultureInfo("it-IT")) & "</small></h6><span />"))
                phDash10.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Quota: <span><small> " & quota.ToString("N2") & "</small></h6><span />"))


                phDash10.Controls.Add(New LiteralControl("</div>"))

                phDash10.Controls.Add(New LiteralControl("</div>"))



                ' inizio sesta riga 


                phDash10.Controls.Add(New LiteralControl("<div Class=""row"">"))


                phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-12 text-left"">"))


                phDash10.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Docenti: <span>"))
                phDash10.Controls.Add(New LiteralControl("<small>" & Data.FixNull(dr("Elenco_Docenti"))))

                phDash10.Controls.Add(New LiteralControl("</small></h6></span></div>"))



                phDash10.Controls.Add(New LiteralControl("</div>"))



                'fine sesta riga sottosezione

                ' inizio sesta riga 


                phDash10.Controls.Add(New LiteralControl("<div Class=""row"">"))


                phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-12 text-left"">"))


                phDash10.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Commissione: <span>"))
                phDash10.Controls.Add(New LiteralControl("<small>" & Data.FixNull(dr("Elenco_Componenti_Commissione"))))

                phDash10.Controls.Add(New LiteralControl("</small></h6></span></div>"))



                phDash10.Controls.Add(New LiteralControl("</div>"))

                'intermezzo
                phDash10.Controls.Add(New LiteralControl("<div class=""row"">"))

                phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-12 text-left"">"))

                phDash10.Controls.Add(New LiteralControl("<hr>"))

                phDash10.Controls.Add(New LiteralControl("</div>"))



                phDash10.Controls.Add(New LiteralControl("</div>"))

                'fine sesta riga sottosezione

                'fine quinta riga


                ' inizio sesta riga 


                phDash10.Controls.Add(New LiteralControl("<div Class=""row"">"))


                phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-12 text-left"">"))


                phDash10.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Note Piano Corso: <span>"))
                phDash10.Controls.Add(New LiteralControl("<small>" & Data.FixNull(dr("NoteUploadDocumentoCorso"))))

                phDash10.Controls.Add(New LiteralControl("</small></h6></span></div>"))



                phDash10.Controls.Add(New LiteralControl("</div>"))



                'fine sesta riga sottosezione


                ' inizio settima riga 


                phDash10.Controls.Add(New LiteralControl("<div Class=""row"">"))


                phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-12 text-left"">"))


                phDash10.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Note Elenco Partecipanti: <span>"))
                phDash10.Controls.Add(New LiteralControl("<small>" & Data.FixNull(dr("NoteUploadElencoCorso"))))

                phDash10.Controls.Add(New LiteralControl("</small></h6></span></div>"))



                phDash10.Controls.Add(New LiteralControl("</div>"))



                'fine settima riga sottosezione


                ' inizio ottava riga 


                phDash10.Controls.Add(New LiteralControl("<div Class=""row"">"))


                phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-12 text-left"">"))


                phDash10.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Note Pagamento: <span>"))
                phDash10.Controls.Add(New LiteralControl("<small>" & Data.FixNull(dr("NoteUploadPagamento"))))

                phDash10.Controls.Add(New LiteralControl("</small></h6></span></div>"))



                phDash10.Controls.Add(New LiteralControl("</div>"))



                'fine ottava riga sottosezione



                ' inizio ottava riga 


                phDash10.Controls.Add(New LiteralControl("<div Class=""row"">"))


                phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-12 text-left"">"))


                phDash10.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Note Verbale: <span>"))
                phDash10.Controls.Add(New LiteralControl("<small>" & Data.FixNull(dr("NoteUploadVerbale"))))

                phDash10.Controls.Add(New LiteralControl("</small></h6></span></div>"))



                phDash10.Controls.Add(New LiteralControl("</div>"))



                'fine ottava riga sottosezione
                ' inizio ottava riga 


                phDash10.Controls.Add(New LiteralControl("<div Class=""row"">"))


                phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-12 text-left"">"))


                phDash10.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Note Valutazione DT: <span>"))
                phDash10.Controls.Add(New LiteralControl("<small>" & Data.FixNull(dr("NoteValutazioneDT"))))

                phDash10.Controls.Add(New LiteralControl("</small></h6></span></div>"))



                phDash10.Controls.Add(New LiteralControl("</div>"))


                ' inizio ottava riga 


                phDash10.Controls.Add(New LiteralControl("<div Class=""row"">"))


                phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-12 text-left"">"))


                phDash10.Controls.Add(New LiteralControl("<h6 class=""piccolo"">Note Annullamento: <span>"))
                phDash10.Controls.Add(New LiteralControl("<small>" & Data.FixNull(dr("NoteAnnullamentoCorso"))))

                phDash10.Controls.Add(New LiteralControl("</small></h6></span></div>"))



                phDash10.Controls.Add(New LiteralControl("</div>"))







                counter1 += 1
                phDash10.Controls.Add(New LiteralControl("</div>"))

                phDash10.Controls.Add(New LiteralControl("</div>"))

                phDash10.Controls.Add(New LiteralControl("</div>"))
                phDash10.Controls.Add(New LiteralControl("</div>"))
                '   End If
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