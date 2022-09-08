Imports fmDotNet
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed

Public Class DashboardRinnovi
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
                        If Session("rinnovoAggiunto") = "OK" Then
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Richiesta Rinnovo caricata nel sistema! ' ).set('resizable', true).resizeTo('20%', 200);", True)
                            Session("rinnovoAggiunto") = Nothing
                        End If
                    ElseIf ris = "ko" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Richiesta Rinnovo non caricata nel sistema<br />CF inesistente o riferito ad una tessera scaduta! ' ).set('resizable', true).resizeTo('20%', 200);", True)
                        Session("rinnovoAggiunto") = Nothing
                    ElseIf ris = "pr" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Richiesta Rinnovo non caricata nel sistema<br />Si è verificato un problema tecnico durante la procedura! ' ).set('resizable', true).resizeTo('20%', 200);", True)
                        Session("rinnovoAggiunto") = Nothing
                    ElseIf ris = "koCFAlbo" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Impossibile andare avanti. Dati Albo da normalizzare.<br />Codice Fiscale e/o Codice Ente Affiliante inesistenti in Albo!<br />Contattare ASI per risolvere il problema. ' ).set('resizable', true).resizeTo('20%', 300);", True)
                        Session("rinnovoAggiunto") = Nothing
                    ElseIf ris = "no" Then
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
        If Not Page.IsPostBack Then


            Rinnovi()





        End If
    End Sub

    Sub Rinnovi()


        Dim ds As DataSet

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("webRinnoviRichiesta")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        ' RequestP.AddSearchField("pre_stato_web", "1")
        RequestP.AddSearchField("Codice_Ente_Richiedente", Session("codice"), Enumerations.SearchOption.equals)
        RequestP.AddSortField("Codice_Status", Enumerations.Sort.Ascend)
        RequestP.AddSortField("IDRinnovo", Enumerations.Sort.Descend)


        ds = RequestP.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then



                Dim deEnco As New Ed()


                phDash.Visible = True
                'Dim counter As Integer = 0
                Dim counter1 As Integer = 0
                'Dim totale As Decimal = 0
                For Each dr In ds.Tables("main").Rows


                If Data.FixNull(dr("Codice_Status")) = "151" Or Data.FixNull(dr("Codice_Status")) = "152" Or Data.FixNull(dr("Codice_Status")) = "153" _
                Or Data.FixNull(dr("Codice_Status")) = "154" Or Data.FixNull(dr("Codice_Status")) = "155" _
                Or Data.FixNull(dr("Codice_Status")) = "156" Or Data.FixNull(dr("Codice_Status")) = "156" _
                Or Data.FixNull(dr("Codice_Status")) = "158" Then

                    counter1 += 1


                    Dim Verb As New Button

                    Verb.ID = "verb_" & counter1
                    Verb.Attributes.Add("runat", "server")
                    Verb.Text = "Invia dichiarazione cambio E.A."
                    Verb.PostBackUrl = "upDichiarazione.aspx?codR=" & deEnco.QueryStringEncode(Data.FixNull(dr("IDRinnovo"))) & "&record_ID=" & deEnco.QueryStringEncode(dr("id_record"))
                    Verb.CssClass = "btn btn-success btn-sm btn-sei btn-custom"
                    If Data.FixNull(dr("Codice_Status")) = "151" Then
                        Verb.Visible = True
                    Else
                        Verb.Visible = False
                    End If




                    Dim hpUPPag As New Button

                    hpUPPag.ID = "hpPag_" & counter1
                    hpUPPag.Attributes.Add("runat", "server")
                    hpUPPag.Text = "Invia Pagamento di: " & Data.FixNull(dr("Rin_CostoRinnovo")) & " €"
                    hpUPPag.PostBackUrl = "upLegRinnovi.aspx?codR=" & deEnco.QueryStringEncode(Data.FixNull(dr("IDRinnovo"))) & "&record_ID=" & deEnco.QueryStringEncode(dr("id_record"))
                    hpUPPag.CssClass = "btn btn-success btn-sm btn-sette btn-custom"
                    If (Data.FixNull(dr("Codice_Status")) = "158" Or (Data.FixNull(dr("Codice_Status")) = "155")) Then
                        hpUPPag.Visible = True
                    Else
                        hpUPPag.Visible = False
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

                    phDash.Controls.Add(New LiteralControl("Rinnovo:  "))
                    phDash.Controls.Add(New LiteralControl("<span  " & Utility.statusColorCorsi(Data.FixNull(dr("Codice_Status"))) & ">"))
                    phDash.Controls.Add(New LiteralControl("<a name=" & Data.FixNull(dr("IDRinnovo")) & ">" & Data.FixNull(dr("IDRinnovo")) & "</a>"))
                    phDash.Controls.Add(New LiteralControl())

                    phDash.Controls.Add(New LiteralControl("</span><br />"))


                    phDash.Controls.Add(New LiteralControl("Nominativo: <small>" & Data.FixNull(dr("Asi_Nome")) & " " & Data.FixNull(dr("Asi_Cognome")) & "</small><br />"))

                    phDash.Controls.Add(New LiteralControl("CF: <small>" & Data.FixNull(dr("Asi_CodiceFiscale")) & "</small><br />"))
                    phDash.Controls.Add(New LiteralControl("Data Scadenza: <small>" & Data.SonoDieci(Data.FixNull(dr("Asi_DataScadenza"))) & "</small><br />"))
                    phDash.Controls.Add(New LiteralControl("Codice Iscrizione: <small>" & Data.FixNull(dr("Asi_CodiceIscrizione")) & "</small><br />"))
                    phDash.Controls.Add(New LiteralControl("Tessera ASI: <small>" & Data.FixNull(dr("Asi_CodiceTessera")) & "</small><br />"))
                    phDash.Controls.Add(New LiteralControl("-------------------------------<br />"))
                    phDash.Controls.Add(New LiteralControl("Sport: <small>" & Data.FixNull(dr("Asi_Sport")) & "</small><br />"))
                    phDash.Controls.Add(New LiteralControl("Disciplina: <small>" & Data.FixNull(dr("Asi_Disciplina")) & "</small><br />"))
                    phDash.Controls.Add(New LiteralControl("Specialità: <small>" & Data.FixNull(dr("Asi_Specialita")) & "</small><br />"))
                    phDash.Controls.Add(New LiteralControl("Livello: <small>" & Data.FixNull(dr("Asi_Livello")) & "</small><br />"))
                    phDash.Controls.Add(New LiteralControl("Qualifica: <small>" & Data.FixNull(dr("Asi_Qualifica")) & "</small><br />"))

                    phDash.Controls.Add(New LiteralControl())

                    ' phDash.Controls.Add(New LiteralControl("</span>"))

                    phDash.Controls.Add(New LiteralControl("</div>"))


                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4  text-left"">"))

                    phDash.Controls.Add(New LiteralControl("</span><small>Status: </small><small " & Utility.statusColorTextCorsi(Data.FixNull(dr("Codice_Status"))) & ">" & Data.FixNull(dr("Descrizione_Status")) & "</small>"))

                    phDash.Controls.Add(New LiteralControl("</div>"))


                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-right"">"))

                    phDash.Controls.Add(hpUPPag)
                    phDash.Controls.Add(Verb)

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

End Class