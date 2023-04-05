
Imports fmDotNet
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed
Imports System.Net

Public Class DashboardRifiutate
    Inherits System.Web.UI.Page
    Dim open As Integer = 0
    Dim foto As String
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



        Dim deEnco As New Ed()
        Dim heading As String = "heading"
        Dim collapse As String = "collapse"
        Dim quantiPerGruppo As Integer = 0
        Dim ds As DataSet

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("webEquiparazioniMaster")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        ' RequestP.AddSearchField("pre_stato_web", "1")
        RequestP.AddSearchField("CodiceEnteRichiedente", Session("codice"), Enumerations.SearchOption.equals)
        ' RequestP.AddSearchField("CodiceStatus", 160, Enumerations.SearchOption.lessThan)
        RequestP.AddSearchField("CodiceStatus", "104")
        RequestP.AddSortField("IDEquiparazioneM", Enumerations.Sort.Descend)
        '   RequestP.AddSortField("CodiceStatus", Enumerations.Sort.Ascend)

        ds = RequestP.Execute()


        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
            phDash.Visible = True
            'Dim counter As Integer = 0
            Dim counter2 As Integer = 0
            'Dim totale As Decimal = 0
            phDash.Controls.Add(New LiteralControl("<div class=""accordion"" id=""accordionDash"">"))

            For Each dr In ds.Tables("main").Rows

                Dim quantiPerProgetto As Integer = AsiModel.Equiparazione.QuanteEquiparazioniPerGruppo(dr("IDEquiparazioneM"))
                '  Dim quantiPerProgettoEA As Integer = AsiModel.Rinnovi.QuantiRinnoviPerGruppoEA(dr("IDEquipaezioneM"))
                Dim quantiPerProgettoEA As Integer = 0

                counter2 += 1



                Dim hpUPPag As New LinkButton

                hpUPPag.ID = "hpPag_" & counter2
                hpUPPag.Attributes.Add("runat", "server")
                'codR = WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("IDRinnovo"))))
                'record_id = WebUtility.UrlEncode(deEnco.QueryStringEncode(WebUtility.UrlEncode(dr("id_record"))))
                hpUPPag.PostBackUrl = "upLegEquiparazioni2.aspx?codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("IDEquiparazioneM"))))

                hpUPPag.Text = "<i class=""bi bi-wallet""> </i>Invia Pagamento di: " & Data.FixNull(dr("costoEquiM")) & " Euro"

                hpUPPag.CssClass = "btn btn-success btn-sm btn-sette btn-custom  mb-1"
                'If ((Data.FixNull(dr("CodiceStatus")) = "111" Or Data.FixNull(dr("CodiceStatus")) = "114") And Data.FixNull(dr("checkweb")) = "s") Then
                '    hpUPPag.Visible = True
                'Else
                '    hpUPPag.Visible = False
                'End If
                hpUPPag.Visible = False

                Dim addEquiparazione As New LinkButton
                addEquiparazione.ID = "Rin_" & counter2
                addEquiparazione.Attributes.Add("runat", "server")
                addEquiparazione.Text = "<i class=""bi bi-emoji-sunglasses""> </i>Aggiungi Equiparazione"

                addEquiparazione.PostBackUrl = "checkTesseramento2.aspx?codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("IDEquiparazioneM")))) & "&type=same"
                ' fotoCorsistiLnk.PostBackUrl = "UpFotoRinnovo.aspx?codR=" & Data.FixNull(dr("IDRinnovo")) & "&record_ID=" & dr("id_record")

                addEquiparazione.CssClass = "btn btn-success btn-sm  btn-custom  mb-1  mr-1"

                'If (Data.FixNull(dr("checkweb")) = "n") Then
                '    addEquiparazione.Visible = True
                'Else
                '    
                'End If
                addEquiparazione.Visible = False
                Dim Chiudi As New LinkButton
                Chiudi.ID = "Clo_" & counter2
                Chiudi.Attributes.Add("runat", "server")
                Chiudi.Text = "<i class=""bi bi-emoji-sunglasses""> </i>Termina questo gruppo"

                Chiudi.PostBackUrl = "closeEquiparazione2.aspx?idrecord=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("idrecord")))) & "&codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("IDEquiparazioneM"))))
                ' fotoCorsistiLnk.PostBackUrl = "UpFotoRinnovo.aspx?codR=" & Data.FixNull(dr("IDRinnovo")) & "&record_ID=" & dr("id_record")

                Chiudi.CssClass = "btn btn-success btn-sm  btn-custom  mb-1"

                'If quantiPerProgetto > 0 Then
                '    Chiudi.Visible = True
                'Else
                '    Chiudi.Visible = False
                'End If
                Chiudi.Visible = False
                phDash.Controls.Add(New LiteralControl("<div Class=""accordion-item"">"))

                phDash.Controls.Add(New LiteralControl("<h2 Class=""accordion-header"" id=""" & heading & "_" & Data.FixNull(dr("IDEquiparazioneM")) & """>"))
                If Data.FixNull(dr("IDEquiparazioneM")) = open Then
                    phDash.Controls.Add(New LiteralControl("<button Class=""accordion-button moltopiccolo"" type=""button"" data-bs-toggle=""collapse"" data-bs-target=""#collapse" & Data.FixNull(dr("IDEquiparazioneM")) & """ aria-expanded=""False"" aria-controls=""collapse" & Data.FixNull(dr("IDEquiparazioneM")) & """>"))

                Else
                    phDash.Controls.Add(New LiteralControl("<button Class=""accordion-button collapsed moltopiccolo"" type=""button"" data-bs-toggle=""collapse"" data-bs-target=""#collapse" & Data.FixNull(dr("IDEquiparazioneM")) & """ aria-expanded=""False"" aria-controls=""collapse" & Data.FixNull(dr("IDEquiparazioneM")) & """>"))

                End If

                quantiPerGruppo = AsiModel.Equiparazione.quanteRichiestePerGruppo(dr("IDEquiparazioneM"))


                Dim leggendaRinnovi As String = ""

                If quantiPerGruppo = 1 Then

                    leggendaRinnovi = "equiparazione"
                Else
                    leggendaRinnovi = "equiparazioni"
                End If

                Dim legendaStatus As String = ""



                legendaStatus = " -  Status <b>&nbsp;" & Data.FixNull(dr("Descrizione_StatusWeb")) & " </b>- Motivo:" & Data.FixNull(dr("NoteValutazioneDT"))






                '         legendaStatus = " -  Status <b>&nbsp;" & Data.FixNull(dr("Descrizione_StatusWeb")) & "&nbsp;</b>"

                Dim prezzoDaPagare As String = ""
                If (Data.FixNull(dr("CodiceStatus")) = "111" And Data.FixNull(dr("checkweb")) = "s") Then

                    If quantiPerGruppo = 1 Then
                        prezzoDaPagare = "Costo Equiparazione (" & Data.FixNull(dr("costoEquiM")) & ") + Costo Spedizione (" & Data.FixNull(dr("costoSpedizioneM")) & ") =<b>&nbsp;Costo Totale di " & Data.FixNull(dr("costoTotaleM")) & " Euro &nbsp;</b>"

                    Else
                        prezzoDaPagare = "Costo Equiparazioni (" & Data.FixNull(dr("costoEquiM")) & ") + Costo Spedizione (" & Data.FixNull(dr("costoSpedizioneM")) & ") = <b>&nbsp;Costo Totale di " & Data.FixNull(dr("costoTotaleM")) & " Euro &nbsp;</b>"

                    End If
                Else
                    prezzoDaPagare = ""
                End If


                phDash.Controls.Add(New LiteralControl("Codice richiesta " & " <b>&nbsp;" & Data.FixNull(dr("IDEquiparazioneM")) & "&nbsp;</b> del " & Data.FixNull(dr("CreationTimestamp")) & " : [<strong>" & Data.FixNull(dr("Equi_Sport_Interessato")) & " - " & Data.FixNull(dr("Equi_Disciplina_Interessata")) & togliND(Data.FixNull(dr("Equi_Specialita"))) & "</strong>] - <b>&nbsp;" & quantiPerGruppo & "&nbsp;</b>&nbsp;" & leggendaRinnovi & legendaStatus & "&nbsp;-&nbsp;" & prezzoDaPagare))
                phDash.Controls.Add(New LiteralControl("</button>"))
                phDash.Controls.Add(New LiteralControl("</h2>"))
                If Data.FixNull(dr("IDEquiparazioneM")) = open Then
                    phDash.Controls.Add(New LiteralControl("<div id=""" & collapse & Data.FixNull(dr("IDEquiparazioneM")) & """ class=""accordion-collapse collapse show"" aria-labelledby=""heading_" & Data.FixNull(dr("IDEquiparazioneM")) & """ data-bs-parent=""#accordionDash"">"))

                Else
                    phDash.Controls.Add(New LiteralControl("<div id=""" & collapse & Data.FixNull(dr("IDEquiparazioneM")) & """ class=""accordion-collapse collapse"" aria-labelledby=""heading_" & Data.FixNull(dr("IDEquiparazioneM")) & """ data-bs-parent=""#accordionDash"">"))

                End If

                phDash.Controls.Add(New LiteralControl("<div class=""accordion-body"">"))
                If dr("CheckWeb") = "n" Then
                    phDash.Controls.Add(New LiteralControl("<p>"))
                    phDash.Controls.Add(addEquiparazione)


                    phDash.Controls.Add(Chiudi)




                    phDash.Controls.Add(New LiteralControl("</p>"))

                End If

                phDash.Controls.Add(New LiteralControl("<p>"))
                If (Data.FixNull(dr("CodiceStatus")) = "111") Then
                    phDash.Controls.Add(hpUPPag)

                End If
                phDash.Controls.Add(New LiteralControl("</p>"))

                Dim ds1 As DataSet

                Dim fmsP1 As FMSAxml = AsiModel.Conn.Connect()
                fmsP1.SetLayout("webEquiparazioniRichiestaMolti")
                Dim RequestP1 = fmsP1.CreateFindRequest(Enumerations.SearchType.Subset)
                ' RequestP.AddSearchField("pre_stato_web", "1")
                RequestP1.AddSearchField("IDEquiparazioneM", Data.FixNull(dr("IDEquiparazioneM")), Enumerations.SearchOption.equals)
                RequestP1.AddSearchField("Codice_Status", "104")
                RequestP1.AddSortField("Codice_Status", Enumerations.Sort.Ascend)
                RequestP1.AddSortField("IDRecord", Enumerations.Sort.Descend)



                ds1 = RequestP1.Execute()

                If Not IsNothing(ds1) AndAlso ds1.Tables("main").Rows.Count > 0 Then


                    Dim tessera As String

                    Dim counter1 As Integer = 0

                    For Each dr1 In ds1.Tables("main").Rows


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


                        'Dim btnFase2 As New LinkButton

                        'btnFase2.ID = "btnFase2_" & counter1
                        'btnFase2.Attributes.Add("runat", "server")
                        'btnFase2.Text = "<i class=""bi bi-terminal""> </i>Completa l'Equiparazione"
                        'btnFase2.PostBackUrl = "richiestaEquiparazioneFoto2.aspx?fase=" & WebUtility.UrlEncode(deEnco.QueryStringEncode("1")) & "&codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr1("IDRecord")))) & "&record_ID=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(dr1("idrecord")))
                        'btnFase2.CssClass = "btn btn-success btn-sm btn-uno btn-custom mb-2"
                        ''btnFase2.Visible = True
                        ''btnFase3.Visible = False
                        ''hpUP.Visible = False

                        'If (Data.FixNull(dr1("Codice_Status")) = "101" And Data.FixNull(dr1("Equi_Fase")) = "1") Then
                        '    btnFase2.Visible = True
                        'Else
                        '    btnFase2.Visible = False
                        'End If


                        'Dim btnFase3 As New LinkButton

                        'btnFase3.ID = "btnFase3_" & counter1
                        'btnFase3.Attributes.Add("runat", "server")
                        'btnFase3.Text = "<i class=""bi bi-terminal""> </i>Completa l'Equiparazione"
                        'btnFase3.PostBackUrl = "richiestaEquiparazioneDati12.aspx?fase=" & WebUtility.UrlEncode(deEnco.QueryStringEncode("2")) & "&codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr1("IDRecord")))) & "&record_ID=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(dr1("idrecord")))
                        'btnFase3.CssClass = "btn btn-success btn-sm btn-due btn-custom mb-2"
                        ''btnFase3.Visible = True
                        ''btnFase3.Visible = False
                        ''hpUP.Visible = False

                        'If (Data.FixNull(dr1("Codice_Status")) = "101" And Data.FixNull(dr1("Equi_Fase")) = "2") Then
                        '    btnFase3.Visible = True
                        'Else
                        '    btnFase3.Visible = False
                        'End If

                        'Dim btnFase4 As New LinkButton

                        'btnFase4.ID = "btnFase4_" & counter1
                        'btnFase4.Attributes.Add("runat", "server")
                        'btnFase4.Text = "<i class=""bi bi-terminal""> </i>Completa l'Equiparazione"
                        'btnFase4.PostBackUrl = "richiestaEquiparazioneDati22.aspx?fase=" & WebUtility.UrlEncode(deEnco.QueryStringEncode("3")) & "&codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr1("IDRecord")))) & "&record_ID=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(dr1("idrecord")))
                        'btnFase4.CssClass = "btn btn-success btn-sm btn-tre btn-custom mb-2"
                        ''btnFase4.Visible = True
                        ''btnFase4.Visible = False
                        ''hpUP.Visible = False

                        'If (Data.FixNull(dr1("Codice_Status")) = "101" And Data.FixNull(dr1("Equi_Fase")) = "3") Then
                        '    btnFase4.Visible = True
                        'Else
                        '    btnFase4.Visible = False
                        'End If


                        'colore assegnato
                        'Dim AnnPrimaFase As New LinkButton

                        'AnnPrimaFase.ID = "annPF_" & counter1
                        'AnnPrimaFase.Attributes.Add("runat", "server")
                        'AnnPrimaFase.Text = "<i class=""bi bi-file-earmark-x""> </i>Annulla Equiparazione -"
                        'AnnPrimaFase.PostBackUrl = "annullaEquiparazione2.aspx?codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr1("IDEquiparazioneM")))) & "&record_ID=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(dr1("idrecord")))
                        'AnnPrimaFase.CssClass = "btn btn-danger btn-sm btn-tre btn-custom mb-2"
                        'AnnPrimaFase.Attributes.Add("OnClick", "if(!myAnnulla())return false;")
                        'If Data.FixNull(dr1("Codice_Status")) <= 101 Then
                        '    AnnPrimaFase.Visible = True
                        'Else
                        '    AnnPrimaFase.Visible = False
                        'End If

                        'colore assegnato
                        'Dim Ann As New LinkButton

                        'Ann.ID = "ann_" & counter1
                        'Ann.Attributes.Add("runat", "server")
                        'Ann.Text = "<i class=""bi bi-file-earmark-x""> </i>Annulla Equiparazione"
                        'Ann.PostBackUrl = "annullaEquiparazioneMotivo2.aspx?codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr1("IDEquiparazioneM")))) & "&record_ID=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(dr1("idrecord")))
                        'Ann.CssClass = "btn btn-success btn-sm btn-quattro btn-custom mb-2"
                        'Ann.Attributes.Add("OnClick", "if(!myAnnulla())return false;")


                        Dim VediDocumentazione As New LinkButton
                        VediDocumentazione.ID = "VediDoc_" & counter1
                        VediDocumentazione.Attributes.Add("runat", "server")
                        VediDocumentazione.Text = "<i class=""bi bi-file-earmark-pdf""> </i>Diploma e Foto"
                        VediDocumentazione.PostBackUrl = "vediDocumentazione2.aspx?codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr1("IDEquiparazioneM")))) & "&record_ID=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(dr1("idrecord")))
                        VediDocumentazione.CssClass = "btn btn-success btn-sm btn-nove btn-custom mb-2"


                        If Data.FixNull(dr1("Codice_Status") <= 114) Then
                            VediDocumentazione.Visible = True
                        Else
                            VediDocumentazione.Visible = False
                        End If


                        phDash.Controls.Add(New LiteralControl("<div class=""col-sm-12 mb-3 mb-md-0"">"))



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

                        '  phDash.Controls.Add(AnnPrimaFase)
                        'If Data.FixNull(dr1("Codice_Status")) > 101 Then

                        '    phDash.Controls.Add(Ann)
                        'End If

                        'phDash.Controls.Add(btnFase2)
                        'phDash.Controls.Add(btnFase3)
                        'phDash.Controls.Add(btnFase4)
                        phDash.Controls.Add(VediDocumentazione)
                        'phDash.Controls.Add(hpUP)
                        'phDash.Controls.Add(Verb)
                        'phDash.Controls.Add(fotoCorsisti)



                        '            '   phDash.Controls.Add(stopFoto)
                        '  phDash.Controls.Add(hpUPPag)






                        phDash.Controls.Add(New LiteralControl("</div>"))





                        phDash.Controls.Add(New LiteralControl("</div>"))





                        phDash.Controls.Add(New LiteralControl("<hr>"))


                        phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))


                        If Data.FixNull(dr1("Codice_Status") = 104) Then
                            phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-6 text-left moltopiccolo text-white"">"))

                        Else

                            phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-6 text-left moltopiccolo"">"))

                        End If





                        'phDash.Controls.Add(New LiteralControl("Sport: <small>" & Data.FixNull(dr1("Equi_Sport_Interessato")) & "</small><br />"))

                        'phDash.Controls.Add(New LiteralControl("Disciplina: <small>" & Data.FixNull(dr1("Equi_Disciplina_Interessata")) & "</small><br />"))
                        'phDash.Controls.Add(New LiteralControl("Specialità: <small>" & Data.FixNull(dr1("Equi_Specialita")) & "</small><br />"))
                        phDash.Controls.Add(New LiteralControl("Livello: <small>" & Data.FixNull(dr1("Equi_Livello")) & "</small><br />"))
                        phDash.Controls.Add(New LiteralControl("Qualifica da Rilasciare: <small>" & Data.FixNull(dr1("Equi_Qualifica_Tecnica_Da_Rilasciare")) & "</small><br />"))
                        phDash.Controls.Add(New LiteralControl("Qualifica DT:  "))

                        phDash.Controls.Add(New LiteralControl("<small>" & Data.FixNull(dr1("Dicitura_Qualifica_DT")) & "</small><br />"))

                        If Not String.IsNullOrWhiteSpace(Data.FixNull(dr1("NoteValutazioneSettore"))) Then
                            phDash.Controls.Add(New LiteralControl("Note da Settore:  "))
                            phDash.Controls.Add(New LiteralControl("<small>" & Data.FixNull(dr1("NoteValutazioneSettore")) & "</small><br />"))


                        End If

                        '  phDash.Controls.Add(New LiteralControl())

                        '  phDash.Controls.Add(New LiteralControl("</span><br />"))

                        '   phDash.Controls.Add(New LiteralControl())

                        ' phDash.Controls.Add(New LiteralControl("</span>"))

                        phDash.Controls.Add(New LiteralControl("</div>"))






                        phDash.Controls.Add(New LiteralControl("</div>"))








                        counter1 += 1
                        phDash.Controls.Add(New LiteralControl("</div>"))

                        phDash.Controls.Add(New LiteralControl("</div>"))

                        phDash.Controls.Add(New LiteralControl("</div>"))
                        phDash.Controls.Add(New LiteralControl("</div>"))

                        '  End If
                    Next

                End If



                phDash.Controls.Add(New LiteralControl("</div>"))



                phDash.Controls.Add(New LiteralControl("</div>"))


                phDash.Controls.Add(New LiteralControl("</div>"))

            Next


            phDash.Controls.Add(New LiteralControl("</div>"))
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