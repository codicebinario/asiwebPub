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

                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Richiesta Equiparazione caricata nel sistema! ' ).set('resizable', true).resizeTo('20%', 200);", True)


                ElseIf ris = "ko" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Nessun rinnovo evaso con questo codice fiscale ' ).set('resizable', true).resizeTo('20%', 200);", True)

                ElseIf ris = "no" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Richiesta Equiparazione senza verifica tessera.<br />Procedere con una nuova richiesta ' ).set('resizable', true).resizeTo('20%', 200);", True)

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


                    phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-6 text-left"">"))

                    phDash10.Controls.Add(New LiteralControl("Rinnovo:  "))
                    phDash10.Controls.Add(New LiteralControl("<span  " & Utility.statusColorCorsi(Data.FixNull(dr("Codice_Status"))) & ">"))
                    phDash10.Controls.Add(New LiteralControl("<a name=" & Data.FixNull(dr("IDRinnovo")) & ">" & Data.FixNull(dr("IDRinnovo")) & "</a>"))
                    phDash10.Controls.Add(New LiteralControl())

                    phDash10.Controls.Add(New LiteralControl("</span><br />"))


                    phDash10.Controls.Add(New LiteralControl("Nominativo: <small>" & Data.FixNull(dr("Asi_Nome")) & " " & Data.FixNull(dr("Asi_Cognome")) & "</small><br />"))

                    phDash10.Controls.Add(New LiteralControl("CF: <small>" & Data.FixNull(dr("Asi_CodiceFiscale")) & "</small><br />"))
                    phDash10.Controls.Add(New LiteralControl("Data Scadenza: <small>" & Data.SonoDieci(Data.FixNull(dr("Asi_DataScadenza"))) & "</small><br />"))
                    phDash10.Controls.Add(New LiteralControl("Codice Iscrizione: <small>" & Data.FixNull(dr("Asi_CodiceIscrizione")) & "</small><br />"))
                    phDash10.Controls.Add(New LiteralControl("Tessera ASI: <small>" & Data.FixNull(dr("Asi_CodiceTessera")) & "</small><br />"))
                    phDash10.Controls.Add(New LiteralControl("-------------------------------<br />"))
                    phDash10.Controls.Add(New LiteralControl("Sport: <small>" & Data.FixNull(dr("Asi_Sport")) & "</small><br />"))
                    phDash10.Controls.Add(New LiteralControl("Disciplina: <small>" & Data.FixNull(dr("Asi_Disciplina")) & "</small><br />"))
                    phDash10.Controls.Add(New LiteralControl("Specialità: <small>" & Data.FixNull(dr("Asi_Specialita")) & "</small><br />"))
                    phDash10.Controls.Add(New LiteralControl("Livello: <small>" & Data.FixNull(dr("Asi_Livello")) & "</small><br />"))
                    phDash10.Controls.Add(New LiteralControl("Qualifica: <small>" & Data.FixNull(dr("Asi_Qualifica")) & "</small><br />"))


                    phDash10.Controls.Add(New LiteralControl())

                    ' phDash10.Controls.Add(New LiteralControl("</span>"))

                    phDash10.Controls.Add(New LiteralControl("</div>"))


                    phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-4  text-left"">"))

                    phDash10.Controls.Add(New LiteralControl("</span><small>Status: </small><small " & Utility.statusColorTextCorsi(Data.FixNull(dr("Codice_Status"))) & ">" & Data.FixNull(dr("Descrizione_StatusWeb")) & "</small>"))

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

            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing
            Dim DatiRinnovo As New DatiNuovoRinnovo

            fms = Conn.Connect()

            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fms.SetLayout("webRinnoviRichiesta")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestA.AddSearchField("Asi_CodiceFiscale", Trim(txtCodiceFiscale.Text), Enumerations.SearchOption.equals)
            RequestA.AddSearchField("Codice_Ente_Richiedente", Session("codice"), Enumerations.SearchOption.equals)

            Dim Counter1 = 0

            Try


                ds = RequestA.Execute()
                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    phDash.Visible = True
                    For Each dr In ds.Tables("main").Rows
                        Counter1 += 1
                        If Data.FixNull(dr("Codice_Status")) = "159" Or Data.FixNull(dr("Codice_Status")) = "160" Then

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

                            phDash.Controls.Add(New LiteralControl("</span><small>Status: </small><small " & Utility.statusColorTextCorsi(Data.FixNull(dr("Codice_Status"))) & ">" & Data.FixNull(dr("Descrizione_StatusWeb")) & "</small>"))

                            phDash.Controls.Add(New LiteralControl("</div>"))


                            phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-right"">"))

                            '  phDash.Controls.Add(hpUPPag)

                            phDash.Controls.Add(New LiteralControl("</div>"))

                            phDash.Controls.Add(New LiteralControl("</div>"))

                            Counter1 += 1
                            phDash.Controls.Add(New LiteralControl("</div>"))

                            phDash.Controls.Add(New LiteralControl("</div>"))

                            phDash.Controls.Add(New LiteralControl("</div>"))
                            phDash.Controls.Add(New LiteralControl("</div>"))



                        Else




                        End If

                    Next
                Else
                    'Response.Write("ko")
                    phDash.Visible = False
                    Session("procedi") = "KO"
                    Response.Redirect("DashboardRinnoviEvasi.aspx?ris=" & deEnco.QueryStringEncode("ko"))
                End If

            Catch ex As Exception

            End Try
        End If
    End Sub

    Protected Sub btnUltimi5_Click(sender As Object, e As EventArgs) Handles btnUltimi5.Click
        rinnovi()
    End Sub
End Class