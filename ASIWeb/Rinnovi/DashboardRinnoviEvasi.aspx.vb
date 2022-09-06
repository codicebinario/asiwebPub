Imports fmDotNet
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed
Imports System.Globalization
Public Class DashboardRinnoviEvasi
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
                ' If Session("visto") = "ok" Then

                ris = deEnco.QueryStringDecode(ris)

                    If ris = "ok" Then
                    If Session("equiparazioneaggiunta") = "OK" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Richiesta Equiparazione caricata nel sistema! ' ).set('resizable', true).resizeTo('20%', 200);", True)
                        Session("equiparazioneaggiunta") = Nothing
                    End If
                ElseIf ris = "ko" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Nessun rinnovo evaso  con questo codice fiscale ' ).set('resizable', true).resizeTo('20%', 200);", True)
                    Session("equiparazioneaggiunta") = Nothing
                    ElseIf ris = "no" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Richiesta Equiparazione senza verifica tessera.<br />Procedere con una nuova richiesta ' ).set('resizable', true).resizeTo('20%', 200);", True)
                        Session("equiparazioneaggiunta") = Nothing
                    End If
                    Session("visto") = Nothing
                '  End If
            End If


            If Session("stoCorsi") = "ok" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Ora non è più possibile aggiungere foto! ' ).set('resizable', true).resizeTo('20%', 200);", True)
                Session("stoCorsi") = Nothing
            End If



        End If
        If Not Page.IsPostBack Then



            Rinnovi()





        End If

    End Sub
    Sub rinnovi()

        Dim ds As DataSet

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("webRinnoviRichiesta")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        ' RequestP.AddSearchField("pre_stato_web", "1")
        RequestP.AddSearchField("Codice_Ente_Richiedente", Session("codice"), Enumerations.SearchOption.equals)

        RequestP.AddSortField("Codice_Status", Enumerations.Sort.Descend)


        RequestP.SetMax(10)


        ds = RequestP.Execute()

        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

            'Dim counter As Integer = 0
            Dim counter1 As Integer = 0
            Dim totale As Decimal = 0





            For Each dr In ds.Tables("main").Rows

                'Dim VediDocumentazione As New Button
                'VediDocumentazione.CausesValidation = False
                'VediDocumentazione.ID = "VediDoc_" & counter1
                'VediDocumentazione.Attributes.Add("runat", "server")
                'VediDocumentazione.Text = "Diploma e Foto"
                'VediDocumentazione.PostBackUrl = "vediDocumentazione.aspx?codR=" & deEnco.QueryStringEncode(Data.FixNull(dr("IDEquiparazione"))) & "&record_ID=" & deEnco.QueryStringEncode(dr("id_record"))
                'VediDocumentazione.CssClass = "btn btn-success btn-sm btn-nove btn-custom"
                ''  Annulla.OnClientClick = "return confirm(""ciappa"");"
                ''VediDocumentazione.Attributes.Add("OnClick", "if(!myConfirm())return false;")
                ''   StopFoto.Attributes.Add("OnClick", "if(!alertify.confirm)return false;")



                'If Data.FixNull(dr("Codice_Status")) = "159" Then
                '    vediDocumentazione.Visible = True
                'Else
                '    vediDocumentazione.Visible = False
                'End If



                If Data.FixNull(dr("Codice_Status")) = "159" Or Data.FixNull(dr("Codice_Status")) = "160" Then
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

                    phDash10.Controls.Add(New LiteralControl("Rinnovo:  "))
                    phDash10.Controls.Add(New LiteralControl("<span  " & Utility.statusColorCorsi(Data.FixNull(dr("Codice_Status"))) & ">"))
                    phDash10.Controls.Add(New LiteralControl("<a name=" & Data.FixNull(dr("IDRinnovo")) & ">" & Data.FixNull(dr("IDRinnovo")) & "</a>"))
                    phDash10.Controls.Add(New LiteralControl())

                    phDash10.Controls.Add(New LiteralControl("</span><br />"))


                    phDash10.Controls.Add(New LiteralControl("Nominativo: <small>" & Data.FixNull(dr("Asi_Nome")) & " " & Data.FixNull(dr("Asi_Cognome")) & "</small><br />"))

                    phDash10.Controls.Add(New LiteralControl("CF: <small>" & Data.FixNull(dr("Asi_CodiceFiscale")) & "</small><br />"))
                    phDash10.Controls.Add(New LiteralControl("Data Scadenza: <small>" & Data.SonoDieci(Data.FixNull(dr("Asi_DataScadenza"))) & "</small><br />"))

                    phDash10.Controls.Add(New LiteralControl())

                    ' phDash10.Controls.Add(New LiteralControl("</span>"))

                    phDash10.Controls.Add(New LiteralControl("</div>"))


                    phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-4  text-left"">"))

                    phDash10.Controls.Add(New LiteralControl("</span><small>Status: </small><small " & Utility.statusColorTextCorsi(Data.FixNull(dr("Codice_Status"))) & ">" & Data.FixNull(dr("Descrizione_Status")) & "</small>"))

                    phDash10.Controls.Add(New LiteralControl("</div>"))


                    phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-right"">"))


                    '  phDash10.Controls.Add(vediDocumentazione)

                    phDash10.Controls.Add(New LiteralControl("</div>"))

                    phDash10.Controls.Add(New LiteralControl("</div>"))

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





            Dim DettaglioRinnovo As New DatiNuovoRinnovo


            DettaglioRinnovo = AsiModel.Rinnovi.PrendiValoriNuovoRinnovoByCF(Trim(txtCodiceFiscale.Text), Session("codice"))
            '    DettaglioEquiparazione = AsiModel.Equiparazione.CaricaDatiTesseramento(txtCodiceFiscale.Text)
            '     Session("visto") = "ok"
            If DettaglioRinnovo.trovato = True Then
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

                phDash.Controls.Add(New LiteralControl("Rinnovo:  "))
                phDash.Controls.Add(New LiteralControl("<span  " & Utility.statusColorCorsi(DettaglioRinnovo.CodiceStatus) & ">"))
                phDash.Controls.Add(New LiteralControl("<a name=" & DettaglioRinnovo.IDRinnovo & ">" & DettaglioRinnovo.IDRinnovo & "</a>"))
                phDash.Controls.Add(New LiteralControl())

                phDash.Controls.Add(New LiteralControl("</span><br />"))


                phDash.Controls.Add(New LiteralControl("Nominativo: <small>" & DettaglioRinnovo.Nome & " " & DettaglioRinnovo.Cognome & "</small><br />"))

                phDash.Controls.Add(New LiteralControl("CF: <small>" & DettaglioRinnovo.CodiceFiscale & "</small><br />"))
                phDash.Controls.Add(New LiteralControl("Data Scadenza: <small>" & Data.SonoDieci(DettaglioRinnovo.DataScadenza) & "</small><br />"))

                phDash.Controls.Add(New LiteralControl())

                ' phDash.Controls.Add(New LiteralControl("</span>"))

                phDash.Controls.Add(New LiteralControl("</div>"))


                phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4  text-left"">"))

                phDash.Controls.Add(New LiteralControl("</span><small>Status: </small><small " & Utility.statusColorTextCorsi(DettaglioRinnovo.CodiceStatus) & ">" & DettaglioRinnovo.DescrizioneStatus & "</small>"))

                phDash.Controls.Add(New LiteralControl("</div>"))


                phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-right"">"))


                'phDash.Controls.Add(vediDocumentazione)


                phDash.Controls.Add(New LiteralControl("</div>"))
                phDash.Controls.Add(New LiteralControl("</div>"))


                phDash.Controls.Add(New LiteralControl("</div>"))

                phDash.Controls.Add(New LiteralControl("</div>"))

                phDash.Controls.Add(New LiteralControl("</div>"))
                phDash.Controls.Add(New LiteralControl("</div>"))


            Else

                'Response.Write("ko")

                Session("procedi") = "KO"
                Response.Redirect("DashboardRinnoviEvasi.aspx?ris=" & deEnco.QueryStringEncode("ko"))


            End If



        End If
    End Sub

    Protected Sub btnUltimi5_Click(sender As Object, e As EventArgs) Handles btnUltimi5.Click
        rinnovi()
    End Sub
End Class