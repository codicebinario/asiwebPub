
Imports fmDotNet
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed
Imports System.Net

Public Class DashboardEqui2
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
                        Session("AnnullaREqui") = Nothing
                        showScript = "toastr.success('La richiesta è stata eliminata', 'ASI');"

                    Case "newEqui"
                        Session("AnnullaREqui") = Nothing
                        showScript = "toastr.success('Equiparazione inserita', 'ASI');"

                    Case = "closeEqui"
                        Session("AnnullaREqui") = Nothing
                        showScript = "toastr.success('Richiesta terminata', 'ASI');"

                    Case "annullataEqui"
                        Session("AnnullaREqui") = Nothing
                        showScript = "toastr.success('Equiparazione annullata', 'ASI');"
                    Case "pagamentoEqui"
                        Session("AnnullaREqui") = Nothing
                        showScript = "toastr.success('Documento pagamento inviato', 'ASI');"
                    Case "TesserinoEqui"
                        Session("AnnullaREqui") = Nothing
                        showScript = "toastr.success('Tesserino tecnico in download', 'ASI');"

                End Select


                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showSuccess", customizeScript & showScript, True)
                '  ShowToastr(Page, "Message Here", "Title Here", "Info", False, "toast-bottom-full-width", True)

            End If


            If Not String.IsNullOrEmpty(ris) Then
                If Session("visto") = "ok" Then

                    ris = deEnco.QueryStringDecode(ris)

                    If ris = "ok" Then
                        If Session("equiparazioneaggiunta") = "OK" Then
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Richiesta Equiparazione caricata nel sistema! ' ).set('resizable', true).resizeTo('20%', 200);", True)
                            Session("equiparazioneaggiunta") = Nothing
                        End If
                    ElseIf ris = "valScad" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Richiesta Equiparazione non caricata nel sistema.<br />Tesseramento associativo scaduto! ' ).set('resizable', true).resizeTo('20%', 200);", True)
                        Session("equiparazioneaggiunta") = Nothing
                    ElseIf ris = "notFound" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Richiesta Equiparazione non caricata nel sistema.<br />Tesseramento associativo non trovato!<br />. ' ).set('resizable', true).resizeTo('20%', 200);", True)
                        Session("equiparazioneaggiunta") = Nothing
                    ElseIf ris = "erroreGen" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Richiesta Equiparazione non caricata nel sistema<br />Errore di Connessione!' ).set('resizable', true).resizeTo('20%', 200);", True)
                        Session("equiparazioneaggiunta") = Nothing
                    ElseIf ris = "cfInEqui" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Questo codice fiscale è già presente in questa richiesta' ).set('resizable', true).resizeTo('20%', 200);", True)
                        Session("equiparazioneaggiunta") = Nothing




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
        Dim diploma As String
        Dim esisteZip As Boolean = False
        Dim nomeZip As String = ""
        Dim ArrayZip As Array = Nothing
        Dim idrecordMaster As Integer = 0
        Dim stringaZip As String = ""
        Try

            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            fmsP.SetLayout("webEquiparazioniMaster")
            Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            ' RequestP.AddSearchField("pre_stato_web", "1")
            RequestP.AddSearchField("CodiceEnteRichiedente", Session("codice"), Enumerations.SearchOption.equals)
            ' RequestP.AddSearchField("CodiceStatus", 160, Enumerations.SearchOption.lessThan)
            RequestP.AddSearchField("CodiceStatus", "101...114.5")
            '    RequestP.AddSearchField("CodiceStatus", "104", Enumerations.SearchOption.omit)
            RequestP.AddSortField("IDEquiparazioneM", Enumerations.Sort.Descend)
            '   RequestP.AddSortField("CodiceStatus", Enumerations.Sort.Ascend)


            ds = RequestP.Execute()


            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                phDash.Visible = True
                'Dim counter As Integer = 0
                Dim counter2 As Integer = 0
                'Dim totale As Decimal = 0


                For Each dr In ds.Tables("main").Rows
                    If (Data.FixNull(dr("CodiceStatus")) = "104") Then
                    Else


                        phDash.Controls.Add(New LiteralControl("<div class=""accordion"" id=""accordionDash"">"))


                        Dim quantiPerProgetto As Integer = AsiModel.Equiparazione.QuanteEquiparazioniPerGruppo(dr("IDEquiparazioneM"))
                        '  Dim quantiPerProgettoEA As Integer = AsiModel.Rinnovi.QuantiRinnoviPerGruppoEA(dr("IDEquipaezioneM"))
                        Dim quantiPerProgettoEA As Integer = 0

                        counter2 += 1


                        Dim AnnRichiesta As New LinkButton

                        AnnRichiesta.ID = "annRI_" & counter2
                        AnnRichiesta.Attributes.Add("runat", "server")
                        AnnRichiesta.Text = "<i class=""bi bi-file-earmark-x""> </i>Cancella questa richiesta"
                        AnnRichiesta.PostBackUrl = "annullaRichiesta.aspx?record_ID=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(dr("idrecord")))
                        AnnRichiesta.CssClass = "btn btn-danger btn-sm btn-tre btn-custom mb-1  ml-1"
                        'AnnRichiesta.Attributes.Add("OnClick", "if(!myAnnulla())return false;")
                        AnnRichiesta.Attributes.Add("OnClick", "if(!myAnnulla())return false;")
                        If Data.FixNull(dr("CodiceStatus")) <= 101 Then
                            AnnRichiesta.Visible = True
                        Else
                            AnnRichiesta.Visible = False
                        End If



                        Dim hpUPPag As New LinkButton

                        hpUPPag.ID = "hpPag_" & counter2
                        hpUPPag.Attributes.Add("runat", "server")
                        'codR = WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("IDRinnovo"))))
                        'record_id = WebUtility.UrlEncode(deEnco.QueryStringEncode(WebUtility.UrlEncode(dr("id_record"))))
                        hpUPPag.PostBackUrl = "upLegEquiparazioni2.aspx?s=0&codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("IDEquiparazioneM"))))

                        hpUPPag.Text = "<i class=""bi bi-wallet""> </i>Invia Pagamento di: " & Data.FixNull(dr("costoTotaleM")) & " Euro"

                        hpUPPag.CssClass = "btn btn-success btn-sm btn-sette btn-custom  mb-1"
                        If ((Data.FixNull(dr("CodiceStatus")) = "111") And Data.FixNull(dr("checkweb")) = "s") Then
                            hpUPPag.Visible = True
                        Else
                            hpUPPag.Visible = False
                        End If

                        Dim hpUPPag1145 As New LinkButton

                        hpUPPag1145.ID = "hpPag_" & counter2
                        hpUPPag1145.Attributes.Add("runat", "server")
                        'codR = WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("IDRinnovo"))))
                        'record_id = WebUtility.UrlEncode(deEnco.QueryStringEncode(WebUtility.UrlEncode(dr("id_record"))))
                        hpUPPag1145.PostBackUrl = "upLegEquiparazioni2.aspx?s=114&codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("IDEquiparazioneM"))))

                        hpUPPag1145.Text = "<i class=""bi bi-wallet""> </i>Invia Pagamento di: " & Data.FixNull(dr("costoTotaleM")) & " Euro"

                        hpUPPag1145.CssClass = "btn btn-success btn-sm btn-sette btn-custom  mb-1"
                        If ((Data.FixNull(dr("CodiceStatus")) = "114") And Data.FixNull(dr("checkweb")) = "s") Then
                            hpUPPag1145.Visible = True
                        Else
                            hpUPPag1145.Visible = False
                        End If



                        Dim addEquiparazione As New LinkButton
                        addEquiparazione.ID = "Rin_" & counter2
                        addEquiparazione.Attributes.Add("runat", "server")
                        addEquiparazione.Text = "<i class=""bi bi-emoji-sunglasses""> </i>Aggiungi Equiparazione"

                        addEquiparazione.PostBackUrl = "sceltaCheck.aspx?codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("IDEquiparazioneM")))) & "&type=same"
                        ' fotoCorsistiLnk.PostBackUrl = "UpFotoRinnovo.aspx?codR=" & Data.FixNull(dr("IDRinnovo")) & "&record_ID=" & dr("id_record")

                        addEquiparazione.CssClass = "btn btn-success btn-sm  btn-custom  mb-1  mr-1"

                        If (Data.FixNull(dr("checkweb")) = "n") Then
                            addEquiparazione.Visible = True
                        Else
                            addEquiparazione.Visible = False
                        End If

                        Dim Chiudi As New LinkButton
                        Chiudi.ID = "Clo_" & counter2
                        Chiudi.Attributes.Add("runat", "server")
                        Chiudi.Text = "<i class=""bi bi-emoji-sunglasses""> </i>Termina questa richiesta"

                        Chiudi.PostBackUrl = "closeEquiparazione2.aspx?idrecord=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("idrecord")))) & "&codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("IDEquiparazioneM"))))
                        ' fotoCorsistiLnk.PostBackUrl = "UpFotoRinnovo.aspx?codR=" & Data.FixNull(dr("IDRinnovo")) & "&record_ID=" & dr("id_record")

                        Chiudi.CssClass = "btn btn-success btn-sm  btn-custom  mb-1"

                        If quantiPerProgetto > 0 Then
                            Chiudi.Visible = True
                        Else
                            Chiudi.Visible = False
                        End If

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

                            leggendaRinnovi = "equip/e"
                        Else
                            leggendaRinnovi = "equip/i"
                        End If

                        Dim legendaStatus As String = ""

                        If Data.FixNull(dr("CodiceStatus")) < 100 Then
                            legendaStatus = ""
                        Else
                            legendaStatus = " -  Status <b>&nbsp;" & Data.FixNull(dr("Descrizione_StatusWeb")) & "&nbsp;</b>"
                        End If
                        Dim prezzoDaPagare As String = ""
                        If ((Data.FixNull(dr("CodiceStatus")) = "111" Or Data.FixNull(dr("CodiceStatus")) = "114") And Data.FixNull(dr("checkweb")) = "s") Then

                            If quantiPerGruppo = 1 Then
                                prezzoDaPagare = "Costo Equip. (" & Data.FixNull(dr("costoEquiM")) & ") + Dir. Segr. (" & Data.FixNull(dr("costoSegreteriaM")) & ") + Costo Sped. (" & Data.FixNull(dr("costoSpedizioneM")) & ") =<b>&nbsp;Costo Tot. di " & Data.FixNull(dr("costoTotaleM")) & " &euro; &nbsp;</b>"

                            Else
                                prezzoDaPagare = "Costo Equip. (" & Data.FixNull(dr("costoEquiM")) & ") + Dir. Segr. (" & Data.FixNull(dr("costoSegreteriaM")) & ") + Costo Sped. (" & Data.FixNull(dr("costoSpedizioneM")) & ") = <b>&nbsp;Costo Tot. di " & Data.FixNull(dr("costoTotaleM")) & " &euro; &nbsp;</b>"

                            End If
                        Else
                            prezzoDaPagare = ""
                        End If

                        If Data.FixNull(dr("CodiceStatus")) = 114 Or Data.FixNull(dr("CodiceStatus")) = 114.5 Then
                            If Not String.IsNullOrEmpty(Data.FixNull(dr("ZIPMaster"))) Then
                                nomeZip = AsiModel.Equiparazioni.NomeZipRinnovi(Data.FixNull(dr("ZIPMasterContent")))
                                ArrayZip = nomeZip.Split(".")
                                idrecordMaster = Data.FixNull(dr("idRecord"))
                                stringaZip = "<a class=""btn btn-success btn-sm btn-due btn-customZip mb-2"" onclick=""showToast('zip');"" target=""_blank"" href='scaricaZipEquiparazione.aspx?codR=" _
                                         & deEnco.QueryStringEncode(Data.FixNull(dr("IDEquiparazioneM"))) & "&record_ID=" & deEnco.QueryStringEncode(idrecordMaster) & "&nomeFilePC=" _
                                         & deEnco.QueryStringEncode(ArrayZip(0)) & "'><i class=""bi bi-person-badge""> </i>" & "Scarica tutte le tessere</a>"
                            End If

                        End If


                        phDash.Controls.Add(New LiteralControl("Richiesta " & " <b>&nbsp;" & Data.FixNull(dr("IDEquiparazioneM")) & "&nbsp;</b> del " & Data.FixNull(dr("CreationTimestamp")) & " : [<strong>" & Data.FixNull(dr("Equi_Sport_Interessato")) & " - " & Data.FixNull(dr("Equi_Disciplina_Interessata")) & Left(togliND(Data.FixNull(dr("Equi_Specialita"))), 20) & "</strong>] - <b>&nbsp;" & quantiPerGruppo & "&nbsp;</b>&nbsp;" & leggendaRinnovi & legendaStatus & "&nbsp;-&nbsp;"))
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
                            phDash.Controls.Add(AnnRichiesta)



                            phDash.Controls.Add(New LiteralControl("</p>"))

                        End If

                        phDash.Controls.Add(New LiteralControl("<p>"))
                        ' If (Data.FixNull(dr("CodiceStatus")) = "111") Then
                        phDash.Controls.Add(hpUPPag)
                        phDash.Controls.Add(hpUPPag1145)
                        phDash.Controls.Add(New LiteralControl("<span class=""moltopiccolo""> - " & prezzoDaPagare & " - " & stringaZip & "</span>"))

                        'End If
                        phDash.Controls.Add(New LiteralControl("</p>"))

                        Dim ds1 As DataSet

                        Dim fmsP1 As FMSAxml = AsiModel.Conn.Connect()
                        fmsP1.SetLayout("webEquiparazioniRichiestaMolti")
                        Dim RequestP1 = fmsP1.CreateFindRequest(Enumerations.SearchType.Subset)
                        ' RequestP.AddSearchField("pre_stato_web", "1")
                        RequestP1.AddSearchField("IDEquiparazioneM", Data.FixNull(dr("IDEquiparazioneM")), Enumerations.SearchOption.equals)
                        RequestP1.AddSearchField("Codice_Status", "101...114.5")
                        RequestP1.AddSearchField("Equi_Fase", "2", Enumerations.SearchOption.equals)
                        RequestP1.AddSortField("Codice_Status", Enumerations.Sort.Ascend)
                        RequestP1.AddSortField("IDRecord", Enumerations.Sort.Descend)



                        ds1 = RequestP1.Execute()

                        If Not IsNothing(ds1) AndAlso ds1.Tables("main").Rows.Count > 0 Then


                            Dim tessera As String

                            Dim counter1 As Integer = 0

                            For Each dr1 In ds1.Tables("main").Rows


                                counter1 += 1

                                If String.IsNullOrWhiteSpace(Data.FixNull(dr1("DiplomaAsiText"))) Then
                                    diploma = "..\img\noPdf.jpg"
                                Else
                                    diploma = "https: //93.63.195.98" & Data.FixNull(dr1("DiplomaAsiText"))
                                End If


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


                                Dim AnnPrimaFase As New LinkButton

                                AnnPrimaFase.ID = "annPF_" & counter1
                                AnnPrimaFase.Attributes.Add("runat", "server")
                                AnnPrimaFase.Text = "<i class=""bi bi-file-earmark-x""> </i>Annulla Equiparazione -"
                                AnnPrimaFase.PostBackUrl = "annullaEquiparazione2.aspx?codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr1("IDEquiparazioneM")))) & "&record_ID=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(dr1("idrecord")))
                                AnnPrimaFase.CssClass = "btn btn-danger btn-sm btn-tre btn-custom mb-2"
                                AnnPrimaFase.Attributes.Add("OnClick", "if(!myAnnulla())return false;")
                                If Data.FixNull(dr1("Codice_Status")) <= 101 Then
                                    AnnPrimaFase.Visible = True
                                Else
                                    AnnPrimaFase.Visible = False
                                End If


                                Dim VediDocumentazione As New LinkButton
                                VediDocumentazione.ID = "VediDoc_" & counter1
                                VediDocumentazione.Attributes.Add("runat", "server")
                                VediDocumentazione.Text = "<i class=""bi bi-file-earmark-pdf""> </i>Documentazione Presentata"
                                VediDocumentazione.PostBackUrl = "vediDocumentazione2.aspx?codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr1("IDEquiparazioneM")))) & "&record_ID=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(dr1("idrecord")))
                                VediDocumentazione.CssClass = "btn btn-success btn-sm btn-nove btn-custom mb-2"


                                If Data.FixNull(dr1("Codice_Status") < 115) Then
                                    VediDocumentazione.Visible = True
                                Else
                                    VediDocumentazione.Visible = False
                                End If


                                phDash.Controls.Add(New LiteralControl("<div class=""col-sm-12 mb-3 mb-md-0"">"))



                                'accordion card
                                If Data.FixNull(dr1("Codice_Status")) = "104" Or Data.FixNull(dr1("Codice_Status")) = "104,5" Then
                                    phDash.Controls.Add(New LiteralControl("<div class=""card mb-2 shadow-sm rounded bg-danger"">"))
                                Else
                                    phDash.Controls.Add(New LiteralControl("<div class=""card mb-2 shadow-sm rounded "">"))

                                End If


                                'accordion heder
                                phDash.Controls.Add(New LiteralControl("<div class=""card-header"">"))

                                phDash.Controls.Add(New LiteralControl("<div Class=""container-fluid"">"))

                                '            ' inizio prima riga

                                phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))

                                If Data.FixNull(dr1("Codice_Status")) = "104" Or Data.FixNull(dr1("Codice_Status")) = "104,5" Then
                                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left moltopiccolo text-white"">"))

                                Else

                                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left moltopiccolo"">"))

                                End If




                                phDash.Controls.Add(New LiteralControl("Nominativo: <small>" & Data.FixNull(dr1("Equi_Nome")) & " " & Data.FixNull(dr1("Equi_Cognome")) & "</small><br />"))

                                phDash.Controls.Add(New LiteralControl("CF: <small>" & Data.FixNull(dr1("Equi_CodiceFiscale")) & "</small><br />"))
                                phDash.Controls.Add(New LiteralControl("Tessera Ass.: <small>" & Data.FixNull(dr1("Equi_NumeroTessera")) & "</small><br />"))
                                phDash.Controls.Add(New LiteralControl("Data Scadenza: <small>" & SonoDieci(Data.FixNull(dr1("Equi_DataScadenza"))) & "</small><br />"))
                                phDash.Controls.Add(New LiteralControl("Stampa Cartaceo: <small>" & Data.FixNull(dr1("Equi_StampaCartaceo")) & "</small><br />"))
                                phDash.Controls.Add(New LiteralControl("Stampa Diploma: <small>" & Data.FixNull(dr1("Equi_StampaDiploma")) & "</small><br />"))
                                phDash.Controls.Add(New LiteralControl("Invio EA: <small>" & ControlloEA(Data.FixNull(dr1("Equi_inviaA"))) & "</small><br />"))
                                phDash.Controls.Add(New LiteralControl("Da Federazione: <small>" & Data.FixNull(dr1("Equi_DaFederazione")) & "</small><br />"))
                                Dim spedizione As String = ""

                                If Data.FixNull(dr1("Equi_StampaCartaceo")) = "si" Then
                                    If Data.FixNull(dr1("Equi_InviaA")) = "EA" Then
                                        spedizione = " Spedizione:<small class=""text-uppercase""> EA - Equiparazione:" & Data.FixNull(dr1("Quota_Calc_Equiparazione")) & " Euro</small><br />"
                                    Else
                                        spedizione = " Spedizione: residenza - Equiparazione:" & Data.FixNull(dr1("Quota_Calc_Equiparazione")) & " Euro</small><br />"



                                    End If
                                ElseIf Data.FixNull(dr1("Equi_StampaCartaceo")) = "no" Then
                                    spedizione = " Equiparazione:" & Data.FixNull(dr1("Quota_Calc_Equiparazione")) & " Euro</small><br />"


                                End If
                                phDash.Controls.Add(New LiteralControl(spedizione))
                                phDash.Controls.Add(New LiteralControl("Diritti Segreteria: <small>" & Data.FixNull(dr1("Equi_DirittiSegreteria")) & "</small><br />"))

                                phDash.Controls.Add(New LiteralControl())



                                phDash.Controls.Add(New LiteralControl("</div>"))

                                If Data.FixNull(dr1("Codice_Status")) = "104" Or Data.FixNull(dr1("Codice_Status")) = "104,5" Then
                                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left moltopiccolo text-white"">"))

                                Else

                                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left moltopiccolo"">"))

                                End If


                                phDash.Controls.Add(New LiteralControl("</span><small>Status: </small><small " & Utility.statusColorTextCorsi(Data.FixNull(dr1("Codice_Status"))) & ">" & Data.FixNull(dr1("Descrizione_StatusWeb")) & "</small>"))

                                If Data.FixNull(dr1("Codice_Status")) = "104" Or Data.FixNull(dr1("Codice_Status")) = "104,5" Then

                                    phDash.Controls.Add(New LiteralControl("<br />Motivo: " & Data.FixNull(dr1("NoteValutazioneDT"))))

                                End If



                                phDash.Controls.Add(New LiteralControl("</div>"))


                                phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-right"">"))

                                phDash.Controls.Add(AnnPrimaFase)
                                'If Data.FixNull(dr1("Codice_Status")) < 102 Then

                                '    phDash.Controls.Add(Ann)
                                'End If


                                phDash.Controls.Add(VediDocumentazione)
                                'phDash.Controls.Add(hpUP)
                                'phDash.Controls.Add(Verb)
                                'phDash.Controls.Add(fotoCorsisti)



                                '            '   phDash.Controls.Add(stopFoto)
                                '  phDash.Controls.Add(hpUPPag)
                                If Data.FixNull(dr1("Codice_Status")) = 114 Or Data.FixNull(dr1("Codice_Status")) = 114.5 Then
                                    If tessera = "..\img\noPdf.jpg" Then
                                        '     phDash10.Controls.Add(New LiteralControl("<td><img src='" & tessera & "' height='70' width='70' alt='" & Data.FixNull(dr("Asi_Nome")) & " " & Data.FixNull(dr("Asi_Cognome")) & "'></td>"))


                                    Else
                                        phDash.Controls.Add(New LiteralControl("<a class=""btn btn-success btn-sm btn-due btn-custom mb-2 ""  onclick=""showToast('tesserino');""  target=""_blank"" href='scaricaTesseraEquiparazioneN2.aspx?record_ID=" & deEnco.QueryStringEncode(dr1("idrecord")) & "&nomeFilePC=" _
                                     & deEnco.QueryStringEncode(Data.FixNull(dr1("TesseraEquiparazioneText"))) & "&nominativo=" _
                                     & deEnco.QueryStringEncode(Data.FixNull(dr1("Equi_Cognome")) & "_" & Data.FixNull(dr1("Equi_Nome"))) & "'><i class=""bi bi-person-badge""> </i>Scarica Tess. Tecnico</a>"))
                                    End If
                                    If diploma = "..\img\noPdf.jpg" Then
                                        '     phDash10.Controls.Add(New LiteralControl("<td><img src='" & tessera & "' height='70' width='70' alt='" & Data.FixNull(dr("Asi_Nome")) & " " & Data.FixNull(dr("Asi_Cognome")) & "'></td>"))


                                    Else
                                        If Data.FixNull(dr1("Equi_StampaDiploma")) = "no" Then
                                        Else

                                            phDash.Controls.Add(New LiteralControl("<a class=""btn btn-success btn-sm btn-due btn-custom mb-2"" onclick=""showToast('diploma');"" target=""_blank"" href='scaricaDiplomaEquiparazioneN2.aspx?record_ID=" & deEnco.QueryStringEncode(dr1("idrecord")) & "&nomeFilePC=" _
                                            & deEnco.QueryStringEncode(Data.FixNull(dr1("DiplomaAsiText"))) & "&nominativo=" _
                                            & deEnco.QueryStringEncode(Data.FixNull(dr1("Equi_Cognome")) & "_" & Data.FixNull(dr1("Equi_Nome"))) & "'><i class=""bi bi-person-badge""> </i>Scarica Diploma</a>"))

                                        End If
                                    End If
                                End If

                                phDash.Controls.Add(New LiteralControl("</div>"))





                                phDash.Controls.Add(New LiteralControl("</div>"))





                                phDash.Controls.Add(New LiteralControl("<hr>"))


                                phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))


                                If Data.FixNull(dr1("Codice_Status")) = "104" Or Data.FixNull(dr1("Codice_Status")) = "104,5" Then
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
                        phDash.Controls.Add(New LiteralControl("</div>"))
                    End If
                Next



            End If
        Catch ex As Exception
            AsiModel.LogIn.LogErrori(ex, "DashboardEqui2", "equiparazioni")
            Response.Redirect("../FriendlyMessage.aspx", False)
        End Try
    End Sub
    Function ControlloEA(valore As String) As String
        Dim ritorno As String = ""
        If valore = "EA" Then
            ritorno = "Si"
        Else
            ritorno = "No"

        End If
        Return ritorno

    End Function

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