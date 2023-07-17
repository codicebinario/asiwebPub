Imports fmDotNet
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Net
Imports Image = System.Drawing.Image

Public Class DashBoardRinnovi2
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
                    Case "toa"
                        showScript = "toastr.success('La richiesta è stata eliminata', 'ASI');"
                        Session("AnnullaREqui") = Nothing
                    Case "newRin"
                        showScript = "toastr.success('Nuovo rinnovo aggiunto', 'ASI');"
                        Session("AnnullaREqui") = Nothing
                    Case "newRinKO"
                        showScript = "toastr.success('Il rinnovo non è stato aggiunto', 'ASI');"
                        Session("AnnullaREqui") = Nothing
                    Case "annullataRin"
                        showScript = "toastr.success('Il rinnovo è stato eliminato', 'ASI');"
                        Session("AnnullaREqui") = Nothing
                    Case "annullataRinKO"
                        showScript = "toastr.success('Il rinnovo non è stato eliminato', 'ASI');"
                        Session("AnnullaREqui") = Nothing
                    Case "closeRin"
                        showScript = "toastr.success('Richiesta terminata', 'ASI');"
                        Session("AnnullaREqui") = Nothing
                    Case "fotoTesseraRin"
                        showScript = "toastr.success('Foto tessera caricata', 'ASI');"
                        Session("AnnullaREqui") = Nothing
                    Case "pagamentoRin"
                        showScript = "toastr.success('Documento pagamento inviato', 'ASI');"
                        Session("AnnullaREqui") = Nothing
                End Select

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showSuccess", customizeScript & showScript, True)



            End If






            If Not String.IsNullOrEmpty(ris) Then
                If Session("visto") = "ok" Then

                    ris = deEnco.QueryStringDecode(ris)

                    If ris = "ok" Then
                        If Session("rinnovoAggiunto") = "OK" Then
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Richiesta Rinnovo caricata nel sistema! ' ).set('resizable', true).resizeTo('20%', 200);", True)
                            Session("rinnovoAggiunto") = Nothing
                        End If
                    ElseIf ris = "ko" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Richiesta Rinnovo non caricata nel sistema<br />CF inesistente o riferito ad una tessera scaduta! ' ).set('resizable', true).resizeTo('20%', 200);", True)
                        Session("rinnovoAggiunto") = Nothing

                    ElseIf ris = "valScad" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Richiesta Rinnovo non caricata nel sistema.<br />Tesseramento associativo scaduto! ' ).set('resizable', true).resizeTo('20%', 200);", True)
                        Session("rinnovoAggiunto") = Nothing
                    ElseIf ris = "notFound" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Richiesta Rinnovo non caricata nel sistema.<br />Tesseramento associativo non trovato!<br />Chiedere verifica CF. ' ).set('resizable', true).resizeTo('20%', 200);", True)
                        Session("rinnovoAggiunto") = Nothing
                    ElseIf ris = "erroreGen" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Richiesta Rinnovo non caricata nel sistema<br />Errore di Connessione!' ).set('resizable', true).resizeTo('20%', 200);", True)
                        Session("rinnovoAggiunto") = Nothing

                    ElseIf ris = "fo" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Fotografia caricata<br />' ).set('resizable', true).resizeTo('20%', 200);", True)
                        Session("rinnovoAggiunto") = Nothing
                    ElseIf ris = "pr" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Richiesta Rinnovo non caricata nel sistema<br />Si è verificato un problema tecnico durante la procedura! ' ).set('resizable', true).resizeTo('20%', 200);", True)
                        Session("rinnovoAggiunto") = Nothing


                    ElseIf ris = "cano" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Rinnovo non cancellato<br />Si è verificato un problema tecnico durante la procedura! ' ).set('resizable', true).resizeTo('20%', 200);", True)
                        Session("rinnovoAggiunto") = Nothing
                    ElseIf ris = "casi" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Rinnovo cancellato<br /> ' ).set('resizable', true).resizeTo('20%', 200);", True)
                        Session("rinnovoAggiunto") = Nothing
                    ElseIf ris = "noCF" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Impossibile andare avanti. Codice Fiscale non esistente in Albo!<br />Contattare ASI per risolvere il problema. Scrivi ad albo@asinazionale.it ' ).set('resizable', true).resizeTo('20%', 300);", True)
                        Session("rinnovoAggiunto") = Nothing
                    ElseIf ris = "toNorma" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Impossibile andare avanti. Codice Fiscale esistente ma dati da normalizzare in Albo!<br />Contattare ASI per risolvere il problema. Scrivi ad albo@asinazionale.it ' ).set('resizable', true).resizeTo('20%', 300);", True)
                        Session("rinnovoAggiunto") = Nothing
                    ElseIf ris = "noEA" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Impossibile andare avanti. Codice Fiscale esistente ma Ente Affiliante non indicato nella precedente iscrizione!<br />Contattare ASI per risolvere il problema. Scrivi ad albo@asinazionale.it ' ).set('resizable', true).resizeTo('20%', 300);", True)
                        Session("rinnovoAggiunto") = Nothing
                    ElseIf ris = "cfInRin" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Questo codice fiscale è già presente in questa richiesta' ).set('resizable', true).resizeTo('20%', 200);", True)
                        Session("rinnovoAggiunto") = Nothing

                    ElseIf ris = "koCFAlbo" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Impossibile andare avanti. Dati Albo da normalizzare.<br />Codice Fiscale e/o Codice Ente Affiliante inesistenti in Albo!<br />Contattare ASI per risolvere il problema. Scrivi ad albo@asinazionale.it ' ).set('resizable', true).resizeTo('20%', 300);", True)
                        Session("rinnovoAggiunto") = Nothing
                    ElseIf ris = "no" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Richiesta Rinnovo senza verifica tessera.<br />Procedere con una nuova richiesta ' ).set('resizable', true).resizeTo('20%', 200);", True)
                        Session("rinnovoAggiunto") = Nothing
                    ElseIf ris = "xx" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Richiesta Rinnovo senza verifica tessera.<br />Procedere con una nuova richiesta ' ).set('resizable', true).resizeTo('20%', 200);", True)
                        Session("rinnovoAggiunto") = Nothing
                    End If
                    Session("visto") = Nothing
                End If
            End If


            'If Session("stoCorsi") = "ok" Then
            '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Ora non è più possibile aggiungere foto! ' ).set('resizable', true).resizeTo('20%', 200);", True)
            '    Session("stoCorsi") = Nothing
            'End If



        End If



        Dim aperto As String
        aperto = Request.QueryString("aperto")
        '   rinnovi2()
        aperto = Request.QueryString("aperto")
        open = Request.QueryString("open")
        rinnovi()
    End Sub


    Sub rinnovi()

        Dim deEnco As New Ed()
        Dim heading As String = "heading"
        Dim collapse As String = "collapse"
        Dim quantiPerGruppo As Integer = 0
        Dim ds As DataSet
        Dim esisteZip As Boolean = False
        Dim nomeZip As String = ""
        Dim ArrayZip As Array = Nothing
        Dim idrecordMaster As Integer = 0
        Dim stringaZip As String = ""
        '    Try


        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            fmsP.SetLayout("webRinnoviMasterTutto")
            Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            ' RequestP.AddSearchField("pre_stato_web", "1")
            RequestP.AddSearchField("CodiceEnteRichiedente", Session("codice"), Enumerations.SearchOption.equals)
            ' RequestP.AddSearchField("CodiceStatus", 160, Enumerations.SearchOption.lessThan)
            RequestP.AddSearchField("CodiceStatus", "1...159")
            RequestP.AddSortField("IDRinnovoM", Enumerations.Sort.Descend)
            '   RequestP.AddSortField("CodiceStatus", Enumerations.Sort.Ascend)


            ds = RequestP.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then


                phDash.Visible = True
                'Dim counter As Integer = 0
                Dim counter2 As Integer = 0
                'Dim totale As Decimal = 0
                phDash.Controls.Add(New LiteralControl("<div class=""accordion"" id=""accordionDash"">"))
                For Each dr In ds.Tables("main").Rows

                    Dim quantiPerProgetto As Integer = AsiModel.Rinnovi.QuantiRinnoviPerGruppo(dr("IDRinnovoM"))
                    Dim quantiPerProgettoEA As Integer = AsiModel.Rinnovi.QuantiRinnoviPerGruppoEA(dr("IDRinnovoM"))



                    counter2 += 1

                    Dim AnnRichiesta As New LinkButton

                    AnnRichiesta.ID = "annRI_" & counter2
                    AnnRichiesta.Attributes.Add("runat", "server")
                    AnnRichiesta.Text = "<i class=""bi bi-file-earmark-x""> </i>Cancella questa richiesta"
                    AnnRichiesta.PostBackUrl = "annullaRichiestaR.aspx?record_ID=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(dr("idrecord")))
                    AnnRichiesta.CssClass = "btn btn-danger btn-sm btn-tre btn-custom mb-1  ml-1"
                    AnnRichiesta.Attributes.Add("OnClick", "if(!myAnnulla())return false;")
                    If Data.FixNull(dr("CodiceStatus")) = 150 Or Data.FixNull(dr("CodiceStatus")) = 152 Then
                        AnnRichiesta.Visible = True
                    Else
                        AnnRichiesta.Visible = False
                    End If


                    Dim hpUPPag As New LinkButton

                    hpUPPag.ID = "hpPag_" & counter2
                    hpUPPag.Attributes.Add("runat", "server")
                    'codR = WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("IDRinnovo"))))
                    'record_id = WebUtility.UrlEncode(deEnco.QueryStringEncode(WebUtility.UrlEncode(dr("id_record"))))
                    hpUPPag.PostBackUrl = "upLegRinnovi2.aspx?s=0&codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("IDRinnovoM"))))

                    hpUPPag.Text = "<i class=""bi bi-wallet""> </i>Invia Pagamento di: " & Data.FixNull(dr("costoTotaleM")) & " Euro"

                    hpUPPag.CssClass = "btn btn-success btn-sm btn-sette btn-custom  mb-1"
                    If ((Data.FixNull(dr("CodiceStatus")) = "155") And Data.FixNull(dr("checkweb")) = "s") Then
                        hpUPPag.Visible = True
                    Else
                        hpUPPag.Visible = False
                    End If

                    Dim hpUPPag158 As New LinkButton

                    hpUPPag158.ID = "hpPag_" & counter2
                    hpUPPag158.Attributes.Add("runat", "server")
                    'codR = WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("IDRinnovo"))))
                    'record_id = WebUtility.UrlEncode(deEnco.QueryStringEncode(WebUtility.UrlEncode(dr("id_record"))))
                    hpUPPag158.PostBackUrl = "upLegRinnovi2.aspx?s=158&codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("IDRinnovoM"))))

                    hpUPPag158.Text = "<i class=""bi bi-wallet""> </i>Invia Pagamento di: " & Data.FixNull(dr("costoTotaleM")) & " Euro"

                    hpUPPag158.CssClass = "btn btn-success btn-sm btn-sette btn-custom  mb-1"
                    If ((Data.FixNull(dr("CodiceStatus")) = "158") And Data.FixNull(dr("checkweb")) = "s") Then
                        hpUPPag158.Visible = True
                    Else
                        hpUPPag158.Visible = False
                    End If



                    Dim addRinnovo As New LinkButton
                    addRinnovo.ID = "Rin_" & counter2
                    addRinnovo.Attributes.Add("runat", "server")
                    addRinnovo.Text = "<i class=""bi bi-emoji-sunglasses""> </i>Aggiungi Rinnovo"

                    addRinnovo.PostBackUrl = "checkTesseramentoRinnovi2.aspx?codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("IDRinnovoM"))))
                    ' fotoCorsistiLnk.PostBackUrl = "UpFotoRinnovo.aspx?codR=" & Data.FixNull(dr("IDRinnovo")) & "&record_ID=" & dr("id_record")

                    addRinnovo.CssClass = "btn btn-success btn-sm  btn-custom  mb-1  mr-1"

                    If (Data.FixNull(dr("checkweb")) = "n") Then
                        addRinnovo.Visible = True
                    Else
                        addRinnovo.Visible = False
                    End If



                    Dim Chiudi As New LinkButton
                    Chiudi.ID = "Clo_" & counter2
                    Chiudi.Attributes.Add("runat", "server")
                    Chiudi.Text = "<i class=""bi bi-emoji-sunglasses""> </i>Termina questa richiesta"

                    Chiudi.PostBackUrl = "closeRinnovo.aspx?idrecord=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("idrecord")))) & "&codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("IDRinnovoM"))))
                    ' fotoCorsistiLnk.PostBackUrl = "UpFotoRinnovo.aspx?codR=" & Data.FixNull(dr("IDRinnovo")) & "&record_ID=" & dr("id_record")

                    Chiudi.CssClass = "btn btn-success btn-sm  btn-custom  mb-1"

                    If quantiPerProgetto > 0 Then
                        Chiudi.Visible = True
                    Else
                        Chiudi.Visible = False
                    End If




                    phDash.Controls.Add(New LiteralControl("<div Class=""accordion-item"">"))

                    phDash.Controls.Add(New LiteralControl("<h2 Class=""accordion-header"" id=""" & heading & "_" & Data.FixNull(dr("IDRinnovoM")) & """>"))
                    If Data.FixNull(dr("IDRinnovoM")) = open Then
                        phDash.Controls.Add(New LiteralControl("<button Class=""accordion-button moltopiccolo"" type=""button"" data-bs-toggle=""collapse"" data-bs-target=""#collapse" & Data.FixNull(dr("IDRinnovoM")) & """ aria-expanded=""False"" aria-controls=""collapse" & Data.FixNull(dr("IDRinnovoM")) & """>"))

                    Else
                        phDash.Controls.Add(New LiteralControl("<button Class=""accordion-button collapsed moltopiccolo"" type=""button"" data-bs-toggle=""collapse"" data-bs-target=""#collapse" & Data.FixNull(dr("IDRinnovoM")) & """ aria-expanded=""False"" aria-controls=""collapse" & Data.FixNull(dr("IDRinnovoM")) & """>"))

                    End If
                    quantiPerGruppo = AsiModel.Rinnovi.quanteRichiestePerGruppo(dr("IDRinnovoM"))

                    Dim leggendaRinnovi As String = ""

                    If quantiPerGruppo = 1 Then

                        leggendaRinnovi = "rinnovo"
                    Else
                        leggendaRinnovi = "rinnovi"
                    End If


                    Dim legendaStatus As String = ""

                    If Data.FixNull(dr("CodiceStatus")) <= 152 Then
                        legendaStatus = ""
                    Else
                        legendaStatus = " -  Status <b>&nbsp;" & Data.FixNull(dr("Descrizione_StatusWeb")) & "&nbsp;</b>"
                    End If
                    Dim prezzoDaPagare As String = ""
                    If ((Data.FixNull(dr("CodiceStatus")) = "155" Or Data.FixNull(dr("CodiceStatus")) = "158" Or Data.FixNull(dr("CodiceStatus")) = "159") And Data.FixNull(dr("checkweb")) = "s") Then

                        If quantiPerGruppo = 1 Then
                            prezzoDaPagare = "Costo Rinnovo (" & Data.FixNull(dr("costoRinnovoM")) & ") + Costo Spedizione (" & Data.FixNull(dr("costoSpedizioneM")) & ") =<b>&nbsp;Costo Totale di " & Data.FixNull(dr("costoTotaleM")) & " Euro &nbsp;</b>"

                        Else
                            prezzoDaPagare = "Costo Rinnovi (" & Data.FixNull(dr("costoRinnovoM")) & ") + Costo Spedizione (" & Data.FixNull(dr("costoSpedizioneM")) & ") = <b>&nbsp;Costo Totale di " & Data.FixNull(dr("costoTotaleM")) & " Euro &nbsp;</b>"

                        End If
                    Else
                        prezzoDaPagare = ""
                    End If

                    If Data.FixNull(dr("CodiceStatus")) >= 158 Then
                        If Not String.IsNullOrEmpty(Data.FixNull(dr("ZIPMaster"))) Then
                            nomeZip = AsiModel.Rinnovi.NomeZipRinnovi(Data.FixNull(dr("ZIPMasterContent")))
                            ArrayZip = nomeZip.Split(".")
                            idrecordMaster = Data.FixNull(dr("idRecord"))
                            stringaZip = "<a class=""btn btn-success btn-sm btn-due btn-customZip mb-2"" onclick=""showToast('zip');"" target=""_blank"" href='scaricaZipRinnovo.aspx?codR=" _
                                 & deEnco.QueryStringEncode(Data.FixNull(dr("IDRinnovoM"))) & "&record_ID=" & deEnco.QueryStringEncode(idrecordMaster) & "&nomeFilePC=" _
                                 & deEnco.QueryStringEncode(ArrayZip(0)) & "'><i class=""bi bi-person-badge""> </i>" & "Scarica tutte le tessere</a>"
                        End If
                    End If




                    phDash.Controls.Add(New LiteralControl("Codice richiesta " & " <b>&nbsp;" & Data.FixNull(dr("IDRinnovoM")) & "&nbsp;</b> del " & Data.FixNull(dr("CreationTimestamp")) & " - <b>&nbsp;" & quantiPerGruppo & "&nbsp;</b>&nbsp;" & leggendaRinnovi & legendaStatus & "&nbsp;-&nbsp;"))
                    phDash.Controls.Add(New LiteralControl("</button>"))
                    phDash.Controls.Add(New LiteralControl("</h2>"))
                    If Data.FixNull(dr("IDRinnovoM")) = open Then
                        phDash.Controls.Add(New LiteralControl("<div id=""" & collapse & Data.FixNull(dr("IDRinnovoM")) & """ class=""accordion-collapse collapse show"" aria-labelledby=""heading_" & Data.FixNull(dr("IDRinnovoM")) & """ data-bs-parent=""#accordionDash"">"))

                    Else
                        phDash.Controls.Add(New LiteralControl("<div id=""" & collapse & Data.FixNull(dr("IDRinnovoM")) & """ class=""accordion-collapse collapse"" aria-labelledby=""heading_" & Data.FixNull(dr("IDRinnovoM")) & """ data-bs-parent=""#accordionDash"">"))

                    End If

                    phDash.Controls.Add(New LiteralControl("<div class=""accordion-body"">"))
                    If dr("CheckWeb") = "n" Then
                        phDash.Controls.Add(New LiteralControl("<p>"))
                        phDash.Controls.Add(addRinnovo)
                        '   If quantiPerProgetto >= 1 And quantiPerProgettoEA < 1 Then
                        phDash.Controls.Add(Chiudi)
                        phDash.Controls.Add(AnnRichiesta)
                        '   End If



                        'phDash.Controls.Add(New LiteralControl("<a class=""btn btn-primary "" href=""checkTesseramentoRinnovi2.aspx?codR=" & deEnco.QueryStringEncode(dr("IDRinnovoM")) & """>aggiungi Rinnovo al Gruppo</a>"))
                        phDash.Controls.Add(New LiteralControl("</p>"))

                    End If
                    phDash.Controls.Add(New LiteralControl("<p>"))
                    'If (Data.FixNull(dr("CodiceStatus")) = "155") Then
                    phDash.Controls.Add(hpUPPag)
                    phDash.Controls.Add(hpUPPag158)

                    phDash.Controls.Add(New LiteralControl("<span class=""moltopiccolo""> - " & prezzoDaPagare & " - " & stringaZip & "</span>"))

                    '  End If
                    phDash.Controls.Add(New LiteralControl("</p>"))
                    ' corpo del contenuto del panel
                    '  phDash.Controls.Add(New LiteralControl(Data.FixNull(dr("IDRinnovoM"))))

                    Dim ds1 As DataSet
                '    Try
                If ds.Tables(1).Rows.Count > 0 Then

                            For Each dr1 In ds.Tables(1).Rows
                                If dr("IDRinnovoM").ToString = dr1("TBL_Rinnovi_Molti::IDRinnovoM") And
                                    dr1("TBL_Rinnovi_Molti::Codice_Status") <= 159 And dr1("TBL_Rinnovi_Molti::Rin_fase") = 1 Then



                                    'Dim fmsP1 As FMSAxml = AsiModel.Conn.Connect()
                                    'fmsP1.SetLayout("webRinnoviRichiesta2")
                                    'Dim RequestP1 = fmsP1.CreateFindRequest(Enumerations.SearchType.Subset)
                                    '' RequestP.AddSearchField("pre_stato_web", "1")
                                    'RequestP1.AddSearchField("IDRinnovoM", Data.FixNull(dr("IDRinnovoM")), Enumerations.SearchOption.equals)
                                    'RequestP1.AddSearchField("Codice_Status", "1...159")
                                    'RequestP1.AddSearchField("Rin_Fase", "1")
                                    'RequestP1.AddSortField("Codice_Status", Enumerations.Sort.Ascend)
                                    'RequestP1.AddSortField("IDRinnovo", Enumerations.Sort.Descend)
                                    'ds1 = RequestP1.Execute()

                                    '     If Not IsNothing(ds1) AndAlso ds1.Tables("main").Rows.Count > 0 Then
                                    Dim tessera As String
                                    Dim counter1 As Integer = 0
                                    Dim totale As Decimal = 0
                                    Dim codR As String
                                    Dim record_id As String
                                    'For Each dr1 In ds1.Tables("main").Rows

                                    counter1 += 1


                                    If String.IsNullOrWhiteSpace(Data.FixNull(dr1("TBL_Rinnovi_Molti::ASI_foto"))) Then
                                        foto = "..\img\noimg.jpg"
                                    Else
                                        foto = "https://93.63.195.98" & Data.FixNull(dr1("TBL_Rinnovi_Molti::ASI_foto"))
                                    End If

                                    If String.IsNullOrWhiteSpace(Data.FixNull(dr1("TBL_Rinnovi_Molti::tessera"))) Then
                                        tessera = "..\img\noPdf.jpg"
                                    Else
                                        tessera = "https://93.63.195.98" & Data.FixNull(dr1("TBL_Rinnovi_Molti::tessera"))
                                    End If





                                    Dim fotoCorsistiLnk As New LinkButton
                                    fotoCorsistiLnk.ID = "Fot_" & counter1
                                    fotoCorsistiLnk.Attributes.Add("runat", "server")
                                    fotoCorsistiLnk.Text = "<i class=""bi bi-emoji-sunglasses""> </i>Foto Opzionale"

                                    fotoCorsistiLnk.PostBackUrl = "UpFotoRinnovo2.aspx?codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr1("TBL_Rinnovi_Molti::IDRinnovoM")))) & "&record_ID=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(dr1("TBL_Rinnovi_Molti::id_record")))
                                    ' fotoCorsistiLnk.PostBackUrl = "UpFotoRinnovo.aspx?codR=" & Data.FixNull(dr("IDRinnovo")) & "&record_ID=" & dr("id_record")

                                    fotoCorsistiLnk.CssClass = "btn btn-success btn-sm btn-otto btn-custom  mb-1"

                                    fotoCorsistiLnk.Visible = True





                                    Dim Verb As New LinkButton

                                    Verb.ID = "verb_" & counter1
                                    Verb.Attributes.Add("runat", "server")
                                    Verb.Text = "<i class=""bi bi-file""> </i>Invia cambio E.A."
                                    Verb.PostBackUrl = "upDichiarazione2.aspx?codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr1("TBL_Rinnovi_Molti::IDRinnovoM")))) & "&record_ID=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(dr1("TBL_Rinnovi_Molti::id_record")))
                                    Verb.CssClass = "btn btn-success btn-sm btn-sei btn-custom  mb-1"
                                    'If Data.FixNull(dr1("TBL_Rinnovi_Molti::Codice_Status")) = "151" Then
                                    '    Verb.Visible = True
                                    'Else
                                    '    Verb.Visible = False
                                    'End If


                                    Dim Canc As New LinkButton

                                    Canc.ID = "verb_" & counter1
                                    Canc.Attributes.Add("runat", "server")
                                    Canc.Text = "<i class=""bi bi-file-x-fill""></i> </i>Cancella rinnovo"
                                    Canc.PostBackUrl = "cancellaRiga.aspx?codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr1("TBL_Rinnovi_Molti::IDRinnovoM")))) & "&record_ID=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(dr1("TBL_Rinnovi_Molti::id_record")))
                                    Canc.CssClass = "btn btn-success btn-sm btn-sei btn-custom  mb-1"





                                    phDash.Controls.Add(New LiteralControl("<div class=""col-sm-12 mb-3 mb-md-0"">"))
                                    'accordion card
                                    phDash.Controls.Add(New LiteralControl("<div class=""card mb-2 shadow-sm rounded"">"))
                                    'accordion heder
                                    phDash.Controls.Add(New LiteralControl("<div class=""card-header"">"))

                                    phDash.Controls.Add(New LiteralControl("<div Class=""container-fluid"">"))

                                    'inizio row

                                    phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))

                                    ' prima colonna
                                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left moltopiccolo"">"))


                                    phDash.Controls.Add(New LiteralControl("Rinnovo:  "))
                                    phDash.Controls.Add(New LiteralControl("<span  " & Utility.statusColorCorsi(Data.FixNull(dr1("TBL_Rinnovi_Molti::Codice_Status"))) & ">"))
                                    phDash.Controls.Add(New LiteralControl("<a name=" & Data.FixNull(dr1("TBL_Rinnovi_Molti::IDRinnovo")) & ">" & Data.FixNull(dr1("TBL_Rinnovi_Molti::IDRinnovo")) & "</a>"))
                                    phDash.Controls.Add(New LiteralControl())

                                    phDash.Controls.Add(New LiteralControl("</span><br />"))

                                    phDash.Controls.Add(New LiteralControl("Nominativo: <small class=""text-uppercase""><strong>" & Data.FixNull(dr1("TBL_Rinnovi_Molti::Asi_Nome")) & " " & Data.FixNull(dr1("TBL_Rinnovi_Molti::Asi_Cognome")) & "</strong></small><br />"))

                                    phDash.Controls.Add(New LiteralControl("CF: <small class=""text-uppercase"">" & Data.FixNull(dr1("TBL_Rinnovi_Molti::Asi_CodiceFiscale")) & "</small><br />"))
                                    phDash.Controls.Add(New LiteralControl("Tess. ASI: <small class=""text-uppercase"">" & Data.FixNull(dr1("TBL_Rinnovi_Molti::Asi_CodiceTessera")) & "</small><br />"))
                                    phDash.Controls.Add(New LiteralControl("Tess.Tecnico: <small class=""text-uppercase"">" & Data.FixNull(dr1("TBL_Rinnovi_Molti::Asi_CodiceIscrizione")) & "</small><br />"))
                                    phDash.Controls.Add(New LiteralControl("Data Scadenza: <small class=""text-uppercase"">" & Data.SonoDieci(Data.FixNull(dr1("TBL_Rinnovi_Molti::Asi_DataScadenza"))) & "</small><br />"))
                                    phDash.Controls.Add(New LiteralControl("Cartaceo: <small class=""text-uppercase"">" & Data.FixNull(dr1("TBL_Rinnovi_Molti::Rin_StampaCartaceo")) & "</small><br />"))
                                    Dim spedizione As String = ""
                                    If Data.FixNull(dr1("TBL_Rinnovi_Molti::Rin_StampaCartaceo")) = "Si" Then
                                        If Data.FixNull(dr1("TBL_Rinnovi_Molti::Rin_InviaA")) = "EA" Then
                                            spedizione = " Spedizione:<small class=""text-uppercase""> EA - Rinnovo:" & Data.FixNull(dr1("TBL_Rinnovi_Molti::Rin_CostoRinnovo")) & " Euro</small>"
                                        Else
                                            spedizione = " Spedizione: residenza - Rinnovo:" & Data.FixNull(dr1("TBL_Rinnovi_Molti::Rin_CostoRinnovo")) & " Euro</small>"

                                        End If
                                    ElseIf Data.FixNull(dr1("TBL_Rinnovi_Molti::Rin_StampaCartaceo")) = "No" Then
                                        spedizione = " Rinnovo:" & Data.FixNull(dr1("TBL_Rinnovi_Molti::Rin_CostoRinnovo")) & " Euro</small>"

                                    End If
                                    phDash.Controls.Add(New LiteralControl(spedizione))


                                    'fine prima colonna
                                    phDash.Controls.Add(New LiteralControl("</div>"))

                                    'inizio seconda colonna
                                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4  text-left moltopiccolo"">"))

                                    If Data.FixNull(dr1("TBL_Rinnovi_Molti::Codice_Status")) = "152" Then

                                        phDash.Controls.Add(New LiteralControl("Status:<small " & Utility.statusColorTextCorsi(Data.FixNull(dr1("TBL_Rinnovi_Molti::Codice_Status"))) & ">" & Data.FixNull(dr1("TBL_Rinnovi_Molti::Descrizione_StatusWeb")) & "</small><br />"))
                                    End If


                                    If Data.FixNull(dr1("TBL_Rinnovi_Molti::Codice_Status")) = "151" Then
                                        phDash.Controls.Add(New LiteralControl("Ente di Origine:<br /> <small>" & Data.FixNull(dr1("TBL_Rinnovi_Molti::Asi_NomeEnteEx")) & "</small><br />"))


                                    End If



                                    ' phDash.Controls.Add(New LiteralControl("-------------------------------<br />"))
                                    phDash.Controls.Add(New LiteralControl("Sport: <small>" & Data.FixNull(dr1("TBL_Rinnovi_Molti::Asi_Sport")) & "</small><br />"))
                                    phDash.Controls.Add(New LiteralControl("Disciplina: <small>" & Data.FixNull(dr1("TBL_Rinnovi_Molti::Asi_Disciplina")) & "</small><br />"))
                                    phDash.Controls.Add(New LiteralControl("Specialità: <small>" & Data.FixNull(dr1("TBL_Rinnovi_Molti::Asi_Specialita")) & "</small><br />"))
                                    phDash.Controls.Add(New LiteralControl("Qualifica: <small>" & Data.FixNull(dr1("TBL_Rinnovi_Molti::Asi_Qualifica")) & "</small><br />"))
                                    phDash.Controls.Add(New LiteralControl("Livello: <small>" & Data.FixNull(dr1("TBL_Rinnovi_Molti::Asi_Livello")) & "</small><br />"))



                                    'fine seconda colonna
                                    phDash.Controls.Add(New LiteralControl("</div>"))


                                    'inizio terza colonna
                                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4  text-left"">"))

                                    If Data.FixNull(dr1("TBL_Rinnovi_Molti::Codice_Status")) > 154 Then
                                        Canc.Visible = False
                                    Else
                                        Canc.Visible = True
                                        phDash.Controls.Add(Canc)
                                        phDash.Controls.Add(New LiteralControl("<br />"))
                                    End If

                                    If tessera = "..\img\noPdf.jpg" Then
                                        '     phDash10.Controls.Add(New LiteralControl("<td><img src='" & tessera & "' height='70' width='70' alt='" & Data.FixNull(dr("Asi_Nome")) & " " & Data.FixNull(dr("Asi_Cognome")) & "'></td>"))


                                    Else

                                        If Data.FixNull(dr1("TBL_Rinnovi_Molti::Codice_Status")) >= 158 Then





                                            phDash.Controls.Add(New LiteralControl("<a class=""btn btn-success btn-sm btn-sei btn-custom  mb-1"" onclick=""showToast('tesserino');"" target=""_blank"" href='scaricaTesseraRinnovo2.aspx?codR=" _
                             & deEnco.QueryStringEncode(Data.FixNull(dr1("TBL_Rinnovi_Molti::IDRinnovo"))) & "&record_ID=" & deEnco.QueryStringEncode(dr1("TBL_Rinnovi_Molti::id_record")) & "&nomeFilePC=" _
                             & deEnco.QueryStringEncode(Data.FixNull(dr1("TBL_Rinnovi_Molti::StringaNomeFile"))) & "&nominativo=" _
                             & deEnco.QueryStringEncode(Data.FixNull(dr1("TBL_Rinnovi_Molti::Asi_Cognome")) & "_" & Data.FixNull(dr1("TBL_Rinnovi_Molti::Asi_Nome"))) & "'><i class=""bi bi-person-badge""> </i>Scarica Tess. Tecnico</a>"))
                                        End If

                                    End If

                                    If Data.FixNull(dr1("TBL_Rinnovi_Molti::Codice_Status")) < 156 Then
                                        phDash.Controls.Add(fotoCorsistiLnk)

                                    End If

                                    phDash.Controls.Add(New LiteralControl("<br />"))

                                    If foto = "..\img\noimg.jpg" Then
                                        phDash.Controls.Add(New LiteralControl("<img src='" & foto & "' height='70' width='50' alt='" & Data.FixNull(dr1("TBL_Rinnovi_Molti::Asi_Nome")) & " " & Data.FixNull(dr1("TBL_Rinnovi_Molti::Asi_Cognome")) & "'>"))

                                    Else
                                        Dim myImage As Image = FotoS(foto)
                                        Dim base64 As String = ImageHelper.ImageToBase64String(myImage, ImageFormat.Jpeg)
                                        '  Response.Write("<img alt=""Embedded Image"" src=""data:image/Jpeg;base64," & base64 & """ />")
                                        phDash.Controls.Add(New LiteralControl("<img src='data:image/Jpeg;base64," & base64 & "' height='70' width='50' alt='" & Data.FixNull(dr1("TBL_Rinnovi_Molti::Asi_Nome")) & " " & Data.FixNull(dr1("TBL_Rinnovi_Molti::Asi_Cognome")) & "'>"))

                                    End If



                                    '            'fine terza colonna
                                    phDash.Controls.Add(New LiteralControl("</div>"))

                                    'inzio quarta colonna
                                    'phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-2  text-left"">"))
                                    'phDash.Controls.Add(New LiteralControl("<span class=""text-center"">"))



                                    'phDash.Controls.Add(New LiteralControl("</span>"))
                                    'phDash.Controls.Add(New LiteralControl("</div>"))
                                    'fine quarta colonna
                                    ' fine row
                                    phDash.Controls.Add(New LiteralControl("</div>"))





                                    phDash.Controls.Add(New LiteralControl("</div>"))

                                    phDash.Controls.Add(New LiteralControl("</div>"))

                                    phDash.Controls.Add(New LiteralControl("</div>"))
                                    phDash.Controls.Add(New LiteralControl("</div>"))


                                End If
                                'dd
                            Next

                        End If

                    'Catch ex As Exception
                    '    AsiModel.LogIn.LogErrori(ex, "DashBoardRinnovi2", "rinnovi")
                    '    Response.Redirect("../FriendlyMessage.aspx", False)
                    'End Try


                    ' corpo del contenuto del panel

                    phDash.Controls.Add(New LiteralControl("</div>"))



                    phDash.Controls.Add(New LiteralControl("</div>"))


                    phDash.Controls.Add(New LiteralControl("</div>"))
                Next


                phDash.Controls.Add(New LiteralControl("</div>"))


            End If



        'Catch ex As Exception
        '    AsiModel.LogIn.LogErrori(ex, "DashBoardRinnovi2", "rinnovi")
        '    Response.Redirect("../FriendlyMessage.aspx", False)

        'End Try
        For i As Integer = 1 To 5

        Next





    End Sub
    Public Function FotoS(urlFoto As String)

        ' Dim pictureURL As String = "https://93.63.195.98/fmi/xml/cnt/145_ZGQ5YmRiOTYtZjc0YS00NWFmLTgyNTAtZTIyMjRjYjgzYzg0.jpg?-db=Asi&-lay=webCorsisti&-recid=145&-field=Foto(1)"
        Dim pictureURL As String = urlFoto

        Dim wClient As WebClient = New WebClient()
        Dim nc As NetworkCredential = New NetworkCredential("enteweb", "web01")
        wClient.Credentials = nc
        Dim response As Stream = wClient.OpenRead(pictureURL)
        Dim temp = Image.FromStream(response)
        response.Close()
        Return temp

    End Function
End Class