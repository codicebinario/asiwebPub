
Imports fmDotNet
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed
Public Class DashboardEqui
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
        RequestP.AddSortField("Codice_Status", Enumerations.Sort.Ascend)
        RequestP.AddSortField("IDEquiparazione", Enumerations.Sort.Descend)



        ds = RequestP.Execute()

        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then






            phDash.Visible = True
            'Dim counter As Integer = 0
            Dim counter1 As Integer = 0
            'Dim totale As Decimal = 0
            For Each dr In ds.Tables("main").Rows



                If Data.FixNull(dr("Codice_Status")) = "101" Or Data.FixNull(dr("Codice_Status")) = "102" _
                Or Data.FixNull(dr("Codice_Status")) = "103" Or Data.FixNull(dr("Codice_Status")) = "104" _
                Or Data.FixNull(dr("Codice_Status")) = "105" Or Data.FixNull(dr("Codice_Status")) = "106" _
                Or Data.FixNull(dr("Codice_Status")) = "107" Or Data.FixNull(dr("Codice_Status")) = "108" _
                Or Data.FixNull(dr("Codice_Status")) = "109" Or Data.FixNull(dr("Codice_Status")) = "110" _
                Or Data.FixNull(dr("Codice_Status")) = "111" _
                Or Data.FixNull(dr("Codice_Status")) = "112" Or Data.FixNull(dr("Codice_Status")) = "113" _
                Or Data.FixNull(dr("Codice_Status")) = "114" Then


                    counter1 += 1




                    Dim deEnco As New Ed()

                    Dim btnFase2 As New Button

                    btnFase2.ID = "btnFase2_" & counter1
                    btnFase2.Attributes.Add("runat", "server")
                    btnFase2.Text = "Completa l'Equiparazione"
                    btnFase2.PostBackUrl = "richiestaEquiparazioneFoto.aspx?fase=" & deEnco.QueryStringEncode("1") & "&codR=" & deEnco.QueryStringEncode(Data.FixNull(dr("IDEquiparazione"))) & "&record_ID=" & deEnco.QueryStringEncode(dr("id_record"))
                    btnFase2.CssClass = "btn btn-success btn-sm btn-uno btn-custom"
                    'btnFase2.Visible = True
                    'btnFase3.Visible = False
                    'hpUP.Visible = False

                    If (Data.FixNull(dr("Codice_Status")) = "101" And Data.FixNull(dr("Equi_Fase")) = "1") Then
                        btnFase2.Visible = True
                    Else
                        btnFase2.Visible = False
                    End If


                    Dim btnFase3 As New Button

                    btnFase3.ID = "btnFase3_" & counter1
                    btnFase3.Attributes.Add("runat", "server")
                    btnFase3.Text = "Completa l'Equiparazione"
                    btnFase3.PostBackUrl = "richiestaEquiparazioneDati1.aspx?fase=" & deEnco.QueryStringEncode("2") & "&codR=" & deEnco.QueryStringEncode(Data.FixNull(dr("IDEquiparazione"))) & "&record_ID=" & deEnco.QueryStringEncode(dr("id_record"))
                    btnFase3.CssClass = "btn btn-success btn-sm btn-due btn-custom"
                    'btnFase3.Visible = True
                    'btnFase3.Visible = False
                    'hpUP.Visible = False

                    If (Data.FixNull(dr("Codice_Status")) = "101" And Data.FixNull(dr("Equi_Fase")) = "2") Then
                        btnFase3.Visible = True
                    Else
                        btnFase3.Visible = False
                    End If

                    Dim btnFase4 As New Button

                    btnFase4.ID = "btnFase4_" & counter1
                    btnFase4.Attributes.Add("runat", "server")
                    btnFase4.Text = "Completa l'Equiparazione"
                    btnFase4.PostBackUrl = "richiestaEquiparazioneDati2.aspx?fase=" & deEnco.QueryStringEncode("3") & "&codR=" & deEnco.QueryStringEncode(Data.FixNull(dr("IDEquiparazione"))) & "&record_ID=" & deEnco.QueryStringEncode(dr("id_record"))
                    btnFase4.CssClass = "btn btn-success btn-sm btn-tre btn-custom"
                    'btnFase4.Visible = True
                    'btnFase4.Visible = False
                    'hpUP.Visible = False

                    If (Data.FixNull(dr("Codice_Status")) = "101" And Data.FixNull(dr("Equi_Fase")) = "3") Then
                        btnFase4.Visible = True
                    Else
                        btnFase4.Visible = False
                    End If


                    'colore assegnato
                    Dim AnnPrimaFase As New Button

                    AnnPrimaFase.ID = "annPF_" & counter1
                    AnnPrimaFase.Attributes.Add("runat", "server")
                    AnnPrimaFase.Text = "Annulla Equiparazione -"
                    AnnPrimaFase.PostBackUrl = "annullaEquiparazione.aspx?codR=" & deEnco.QueryStringEncode(Data.FixNull(dr("IDEquiparazione"))) & "&record_ID=" & deEnco.QueryStringEncode(dr("id_record"))
                    AnnPrimaFase.CssClass = "btn btn-success btn-sm btn-tre btn-custom"
                    AnnPrimaFase.Attributes.Add("OnClick", "if(!myAnnulla())return false;")
                    If Data.FixNull(dr("Codice_Status")) = "101" Then
                        AnnPrimaFase.Visible = True
                    Else
                        AnnPrimaFase.Visible = False
                    End If

                    'colore assegnato
                    Dim Ann As New Button

                    Ann.ID = "ann_" & counter1
                    Ann.Attributes.Add("runat", "server")
                    Ann.Text = "Annulla Equiparazione"
                    Ann.PostBackUrl = "annullaEquiparazioneMotivo.aspx?codR=" & deEnco.QueryStringEncode(Data.FixNull(dr("IDEquiparazione"))) & "&record_ID=" & deEnco.QueryStringEncode(dr("id_record"))
                    Ann.CssClass = "btn btn-success btn-sm btn-quattro btn-custom"
                    '  Ann.Attributes.Add("OnClick", "if(!myAnnulla())return false;")

                    If (Data.FixNull(dr("Codice_Status")) = "102" _
                Or Data.FixNull(dr("Codice_Status")) = "103" Or Data.FixNull(dr("Codice_Status")) = "104" _
                Or Data.FixNull(dr("Codice_Status")) = "105" Or Data.FixNull(dr("Codice_Status")) = "106" _
                Or Data.FixNull(dr("Codice_Status")) = "107" Or Data.FixNull(dr("Codice_Status")) = "108" _
                Or Data.FixNull(dr("Codice_Status")) = "109" Or Data.FixNull(dr("Codice_Status")) = "110" _
                Or Data.FixNull(dr("Codice_Status")) = "111" _
                Or Data.FixNull(dr("Codice_Status")) = "112" Or Data.FixNull(dr("Codice_Status")) = "113" _
                   Or Data.FixNull(dr("Codice_Status")) = "114") Then
                        'And Data.FixNull(dr("corsoAnnullabile")) = "s" Then

                        '  Or Data.FixNull(dr("Codice_Status")) = "82"
                        Ann.Visible = True
                    Else
                        Ann.Visible = False
                    End If

                    Dim hpUPPag As New Button

                    hpUPPag.ID = "hpPag_" & counter1
                    hpUPPag.Attributes.Add("runat", "server")
                    hpUPPag.Text = "Invia Pagamento"
                    hpUPPag.PostBackUrl = "upLegEquiparazioni.aspx?codR=" & deEnco.QueryStringEncode(Data.FixNull(dr("IDEquiparazione"))) & "&record_ID=" & deEnco.QueryStringEncode(dr("id_record"))
                    hpUPPag.CssClass = "btn btn-success btn-sm btn-sette btn-custom"
                    If (Data.FixNull(dr("Codice_Status")) = "111" Or (Data.FixNull(dr("Codice_Status")) = "114") And Data.FixNull(dr("Equi_fase")) = "4") Then
                        hpUPPag.Visible = True
                    Else
                        hpUPPag.Visible = False
                    End If



                    Dim VediDocumentazione As New Button
                    VediDocumentazione.ID = "VediDoc_" & counter1
                    VediDocumentazione.Attributes.Add("runat", "server")
                    VediDocumentazione.Text = "Diploma e Foto"
                    VediDocumentazione.PostBackUrl = "vediDocumentazione.aspx?codR=" & deEnco.QueryStringEncode(Data.FixNull(dr("IDEquiparazione"))) & "&record_ID=" & deEnco.QueryStringEncode(dr("id_record"))
                    VediDocumentazione.CssClass = "btn btn-success btn-sm btn-nove btn-custom"
                    '  Annulla.OnClientClick = "return confirm(""ciappa"");"
                    'VediDocumentazione.Attributes.Add("OnClick", "if(!myConfirm())return false;")
                    '   StopFoto.Attributes.Add("OnClick", "if(!alertify.confirm)return false;")



                    If (Data.FixNull(dr("Codice_Status")) = "102" _
                Or Data.FixNull(dr("Codice_Status")) = "103" Or Data.FixNull(dr("Codice_Status")) = "104" _
                Or Data.FixNull(dr("Codice_Status")) = "105" Or Data.FixNull(dr("Codice_Status")) = "106" _
                Or Data.FixNull(dr("Codice_Status")) = "107" Or Data.FixNull(dr("Codice_Status")) = "108" _
                Or Data.FixNull(dr("Codice_Status")) = "109" Or Data.FixNull(dr("Codice_Status")) = "110" _
                Or Data.FixNull(dr("Codice_Status")) = "111" _
                Or Data.FixNull(dr("Codice_Status")) = "112" Or Data.FixNull(dr("Codice_Status")) = "113" _
                   Or Data.FixNull(dr("Codice_Status")) = "114") Then
                        VediDocumentazione.Visible = True
                    Else
                        VediDocumentazione.Visible = False
                    End If




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

                    phDash.Controls.Add(AnnPrimaFase)
                    phDash.Controls.Add(Ann)
                    phDash.Controls.Add(btnFase2)
                    phDash.Controls.Add(btnFase3)
                    phDash.Controls.Add(btnFase4)
                    phDash.Controls.Add(VediDocumentazione)
                    'phDash.Controls.Add(hpUP)
                    'phDash.Controls.Add(Verb)
                    'phDash.Controls.Add(fotoCorsisti)



                    '   phDash.Controls.Add(stopFoto)
                    phDash.Controls.Add(hpUPPag)






                    phDash.Controls.Add(New LiteralControl("</div>"))





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