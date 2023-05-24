Imports fmDotNet
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed
Imports System.Globalization
Imports DocumentFormat.OpenXml.Spreadsheet

Public Class DashboardRinnoviEvasi2
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
        Dim tipo As String = Request.QueryString("tipo")

        If Not Page.IsPostBack Then


            If Not String.IsNullOrEmpty(ris) Then
                ' If Session("visto") = "ok" Then

                ris = deEnco.QueryStringDecode(ris)

                If ris = "ok" Then

                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Richiesta Equiparazione caricata nel sistema! ' ).set('resizable', true).resizeTo('20%', 200);", True)


                ElseIf ris = "ko" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Nessun rinnovo evaso con" & DettaglioTipo(tipo) & " ' ).set('resizable', true).resizeTo('20%', 200);", True)

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



            rinnovi()





        End If

    End Sub
    Function DettaglioTipo(valore As String) As String
        Dim risultato As String
        If valore = "cF" Then
            risultato = " questo codice fiscale"
        ElseIf valore = "nR" Then
            risultato = " questa numero richiesta"
        Else
            risultato = " questo ente"
        End If
        Return risultato
    End Function

    Sub rinnovi()

        Dim ds As DataSet

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("webRinnoviRichiesta2")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        ' RequestP.AddSearchField("pre_stato_web", "1")
        RequestP.AddSearchField("Codice_Ente_Richiedente", Session("codice"), Enumerations.SearchOption.equals)
        RequestP.AddSearchField("Codice_Status", 160, Enumerations.SearchOption.biggerOrEqualThan)
        RequestP.SetMax(10)
        RequestP.AddSortField("IDRinnovo", Enumerations.Sort.Descend)
        ' RequestP.AddSortField("IDRinnovo", Enumerations.Sort.Descend)
        ds = RequestP.Execute()







        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

            'Dim counter As Integer = 0
            Dim counter1 As Integer = 0
            Dim totale As Decimal = 0
            Dim tessera As String
            Dim nominativo As String
            Dim rompiStatus As Integer
            Dim cambiato As String
            Dim esisteZip As Boolean
            Dim fileZip As String
            Dim nomeZip As String = ""
            Dim idrecordMaster As Integer = 0
            Dim ArrayZip As Array = Nothing
            For Each dr In ds.Tables("main").Rows


                esisteZip = AsiModel.Rinnovi.EsisteZipRinnovi(Data.FixNull(dr("IDRinnovoM")))
                If esisteZip Then
                    nomeZip = AsiModel.Rinnovi.NomeZipRinnovi(Data.FixNull(dr("IDRinnovoM")))
                    ArrayZip = nomeZip.Split(".")

                    idrecordMaster = AsiModel.Rinnovi.GetIdRecordRinnovi(Data.FixNull(dr("IDRinnovoM")))
                End If

                ' Response.Write("esite: " & esisteZip & "<br />")

                If rompiStatus = Data.FixNull(dr("IDRinnovoM")) Then
                    cambiato = ""
                Else
                    cambiato = "ok"
                End If



                'If counter1 <= 10 Then

                phDash10.Visible = True

                    If String.IsNullOrWhiteSpace(Data.FixNull(dr("tessera"))) Then
                        tessera = "..\img\noPdf.jpg"
                    Else
                        tessera = "https://93.63.195.98" & Data.FixNull(dr("tessera"))
                    End If



                    phDash10.Controls.Add(New LiteralControl("<div class=""col-sm-12 mb-3 mb-md-0"">"))

                    If cambiato = "ok" Then


                        phDash10.Controls.Add(New LiteralControl("<div Class=""section-divider"">"))
                        phDash10.Controls.Add(New LiteralControl("<span>"))


                        If esisteZip Then
                            phDash10.Controls.Add(New LiteralControl("<a class=""btn btn-success btn-sm btn-due btn-customZip mb-2"" onclick=""showToast('zip');"" target=""_blank"" href='scaricaZipRinnovo.aspx?codR=" _
                             & deEnco.QueryStringEncode(Data.FixNull(dr("IDRinnovoM"))) & "&record_ID=" & deEnco.QueryStringEncode(idrecordMaster) & "&nomeFilePC=" _
                             & deEnco.QueryStringEncode(ArrayZip(0)) & "'><i class=""bi bi-person-badge""> </i>" & "Richiesta " & Data.FixNull(dr("IDRinnovoM")) & " - Scarica tutte le tessere</a>"))
                        Else
                            phDash10.Controls.Add(New LiteralControl("Richiesta " & Data.FixNull(dr("IDRinnovoM"))))

                        End If
                        phDash10.Controls.Add(New LiteralControl("</span>"))


                        phDash10.Controls.Add(New LiteralControl("</div>"))
                    End If


                    'accordion card
                    phDash10.Controls.Add(New LiteralControl("<div class=""card mb-2 shadow-sm rounded"">"))
                    'accordion heder
                    phDash10.Controls.Add(New LiteralControl("<div class=""card-header"">"))

                    phDash10.Controls.Add(New LiteralControl("<div Class=""container-fluid"">"))

                    ' inizio prima riga

                    phDash10.Controls.Add(New LiteralControl("<div Class=""row"">"))


                    phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-8 text-left"">"))

                    'phDash10.Controls.Add(New LiteralControl("Rinnovo:  "))
                    'phDash10.Controls.Add(New LiteralControl("<span  " & Utility.statusColorCorsi(Data.FixNull(dr("Codice_Status"))) & ">"))
                    'phDash10.Controls.Add(New LiteralControl("<a name=" & Data.FixNull(dr("IDRinnovo")) & ">" & Data.FixNull(dr("IDRinnovo")) & "</a>"))
                    'phDash10.Controls.Add(New LiteralControl())

                    'phDash10.Controls.Add(New LiteralControl("</span><br />"))
                    phDash10.Controls.Add(New LiteralControl("Codice Richiesta: <small><b>" & Data.FixNull(dr("IDRinnovoM")) & "</b></small><br />"))


                    phDash10.Controls.Add(New LiteralControl("Nominativo: <small>" & Data.FixNull(dr("Asi_Nome")) & " " & Data.FixNull(dr("Asi_Cognome")) & "</small><br />"))

                    'phDash10.Controls.Add(New LiteralControl("CF: <small>" & Data.FixNull(dr("Asi_CodiceFiscale")) & "</small><br />"))
                    'phDash10.Controls.Add(New LiteralControl("Data Scadenza: <small>" & Data.SonoDieci(Data.FixNull(dr("Asi_DataScadenza"))) & "</small><br />"))
                    'phDash10.Controls.Add(New LiteralControl("Codice Iscrizione: <small>" & Data.FixNull(dr("Asi_CodiceIscrizione")) & "</small><br />"))
                    'phDash10.Controls.Add(New LiteralControl("Tessera ASI: <small>" & Data.FixNull(dr("Asi_CodiceTessera")) & "</small><br />"))
                    'phDash10.Controls.Add(New LiteralControl("-------------------------------<br />"))
                    'phDash10.Controls.Add(New LiteralControl("Sport: <small>" & Data.FixNull(dr("Asi_Sport")) & "</small><br />"))
                    'phDash10.Controls.Add(New LiteralControl("Disciplina: <small>" & Data.FixNull(dr("Asi_Disciplina")) & "</small><br />"))
                    'phDash10.Controls.Add(New LiteralControl("Specialità: <small>" & Data.FixNull(dr("Asi_Specialita")) & "</small><br />"))
                    'phDash10.Controls.Add(New LiteralControl("Livello: <small>" & Data.FixNull(dr("Asi_Livello")) & "</small><br />"))
                    'phDash10.Controls.Add(New LiteralControl("Qualifica: <small>" & Data.FixNull(dr("Asi_Qualifica")) & "</small><br />"))
                    phDash10.Controls.Add(New LiteralControl("CF: <small>" & Data.FixNull(dr("Asi_CodiceFiscale")) & "</small><br />"))
                    phDash10.Controls.Add(New LiteralControl("Tess. ASI: <small>" & Data.FixNull(dr("Asi_CodiceTessera")) & "</small><br />"))
                    phDash10.Controls.Add(New LiteralControl("Tess. Tecnico: <small>" & Data.FixNull(dr("Asi_CodiceIscrizione")) & "</small><br />"))
                    phDash10.Controls.Add(New LiteralControl("Data Scadenza: <small>" & Data.SonoDieci(Data.FixNull(dr("Asi_DataScadenza"))) & "</small><br />"))
                    phDash10.Controls.Add(New LiteralControl("Nuovo Tess. Tecnico: <small><b>" & Data.FixNull(dr("Asi_CodiceIscrizioneNuovo")) & "</b></small><br />"))
                    phDash10.Controls.Add(New LiteralControl("Nuova Data Scadenza: <small><b>" & Data.SonoDieci(Data.FixNull(dr("Asi_DataScadenzaNuovo"))) & "</b></small><br />"))

                    phDash10.Controls.Add(New LiteralControl("-------------------------------<br />"))
                    phDash10.Controls.Add(New LiteralControl("Sport: <small>" & Data.FixNull(dr("Asi_Sport")) & "</small><br />"))
                    phDash10.Controls.Add(New LiteralControl("Disciplina: <small>" & Data.FixNull(dr("Asi_Disciplina")) & "</small><br />"))
                    phDash10.Controls.Add(New LiteralControl("Specialità: <small>" & Data.FixNull(dr("Asi_Specialita")) & "</small><br />"))
                    phDash10.Controls.Add(New LiteralControl("Qualifica: <small>" & Data.FixNull(dr("Asi_Qualifica")) & "</small><br />"))
                    phDash10.Controls.Add(New LiteralControl("Livello: <small>" & Data.FixNull(dr("Asi_Livello")) & "</small><br />"))


                    phDash10.Controls.Add(New LiteralControl())

                    ' phDash10.Controls.Add(New LiteralControl("</span>"))

                    phDash10.Controls.Add(New LiteralControl("</div>"))


                    phDash10.Controls.Add(New LiteralControl("<div Class=""col-sm-4  text-left"">"))

                    phDash10.Controls.Add(New LiteralControl("</span><small>Status: </small><small " & Utility.statusColorTextCorsi(Data.FixNull(dr("Codice_Status"))) & ">" & Data.FixNull(dr("Descrizione_StatusWeb")) & "</small><br /><br />"))
                    If Not String.IsNullOrEmpty(Data.FixNull(dr("Asi_codiceEnteEx"))) Then
                        phDash10.Controls.Add(New LiteralControl("Ente di Origine:<br /> <small>" & Data.FixNull(dr("Asi_NomeEnteEx")) & "</small><br /><br />"))


                    End If

                    '  phDash10.Controls.Add(New LiteralControl("</span><small>Tessera: </small><small></small><br /><br />"))

                    If tessera = "..\img\noPdf.jpg" Then
                        '     phDash10.Controls.Add(New LiteralControl("<td><img src='" & tessera & "' height='70' width='70' alt='" & Data.FixNull(dr("Asi_Nome")) & " " & Data.FixNull(dr("Asi_Cognome")) & "'></td>"))


                    Else
                        phDash10.Controls.Add(New LiteralControl("<a class=""btn btn-success btn-sm btn-due btn-custom"" onclick=""showToast('tesserino');"" target=""_blank"" href='scaricaTesseraRinnovo2.aspx?codR=" _
                             & deEnco.QueryStringEncode(Data.FixNull(dr("IDRinnovo"))) & "&record_ID=" & deEnco.QueryStringEncode(dr("id_record")) & "&nomeFilePC=" _
                             & deEnco.QueryStringEncode(Data.FixNull(dr("StringaNomeFile"))) & "&nominativo=" _
                             & deEnco.QueryStringEncode(Data.FixNull(dr("Asi_Cognome")) & "_" & Data.FixNull(dr("Asi_Nome"))) & "'><i class=""bi bi-person-badge""> </i>Scarica Tess. Tecnico</a>"))


                    End If
                    phDash10.Controls.Add(New LiteralControl("</div>"))





                    phDash10.Controls.Add(New LiteralControl("</div>"))

                    counter1 += 1
                    phDash10.Controls.Add(New LiteralControl("</div>"))

                    phDash10.Controls.Add(New LiteralControl("</div>"))

                    phDash10.Controls.Add(New LiteralControl("</div>"))
                    phDash10.Controls.Add(New LiteralControl("</div>"))


                    ' End If
                    rompiStatus = Data.FixNull(dr("IDRinnovoM"))
            Next

        End If

    End Sub



    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        If Page.IsValid Then
            Dim codiceFiscaleInserito As String
            Dim numeroRichiestaInserita As Integer
            Dim tipoInserito As String
            If Integer.TryParse(Trim(txtCodiceFiscale.Text), vbNull) Then
                numeroRichiestaInserita = Trim(txtCodiceFiscale.Text)
                tipoInserito = "nR"
            Else
                tipoInserito = "cF"
                codiceFiscaleInserito = Trim(txtCodiceFiscale.Text)
            End If




            Dim fms As FMSAxml = Nothing
            Dim ds As DataSet = Nothing
            Dim DatiRinnovo As New DatiNuovoRinnovo
            Dim tessera As String
            Dim esisteZip As Boolean
            Dim fileZip As String
            Dim nomeZip As String = ""
            Dim idrecordMaster As Integer = 0
            Dim ArrayZip As Array = Nothing
            Dim rompiStatus As Integer
            Dim cambiato As String

            fms = Conn.Connect()

            '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
            '     fmsB.SetDatabase(Database)
            fms.SetLayout("webRinnoviRichiesta2")
            Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)


            If tipoInserito = "cF" Then
                RequestA.AddSearchField("Asi_CodiceFiscale", Trim(txtCodiceFiscale.Text), Enumerations.SearchOption.equals)
            Else
                RequestA.AddSearchField("IDRinnovoM", Trim(txtCodiceFiscale.Text), Enumerations.SearchOption.equals)

            End If


            RequestA.AddSearchField("Codice_Ente_Richiedente", Session("codice"), Enumerations.SearchOption.equals)



            RequestA.AddSearchField("Codice_Status", 160, Enumerations.SearchOption.biggerOrEqualThan)
            Dim Counter1 = 0

            Try


                ds = RequestA.Execute()
                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

                    phDash.Visible = True

                    For Each dr In ds.Tables("main").Rows
                        Dim numerorecord = ds.Tables("main").Rows.IndexOf(dr)

                        If rompiStatus = Data.FixNull(dr("IDRinnovoM")) Then
                            cambiato = ""
                        Else
                            cambiato = "ok"
                        End If
                        esisteZip = AsiModel.Rinnovi.EsisteZipRinnovi(Data.FixNull(dr("IDRinnovoM")))
                        If esisteZip Then
                            nomeZip = AsiModel.Rinnovi.NomeZipRinnovi(Data.FixNull(dr("IDRinnovoM")))
                            ArrayZip = nomeZip.Split(".")

                            idrecordMaster = AsiModel.Rinnovi.GetIdRecordRinnovi(Data.FixNull(dr("IDRinnovoM")))
                        End If
                        Counter1 += 1
                        If String.IsNullOrWhiteSpace(Data.FixNull(dr("tessera"))) Then
                            tessera = "..\img\noPdf.jpg"
                        Else
                            tessera = "https://93.63.195.98" & Data.FixNull(dr("tessera"))
                        End If
                        Counter1 += 1
                        '  If Data.FixNull(dr("Codice_Status")) = "160" Or Data.FixNull(dr("Codice_Status")) = "161" Then

                        phDash.Controls.Add(New LiteralControl("<div class=""col-sm-12 mb-3 mb-md-0"">"))
                        If numerorecord = 0 And tipoInserito = "nR" Then

                            phDash.Controls.Add(New LiteralControl("<div Class=""section-divider"">"))
                            phDash.Controls.Add(New LiteralControl("<span>"))

                            '****zip
                            If esisteZip Then
                                phDash.Controls.Add(New LiteralControl("<a class=""btn btn-success btn-sm btn-due btn-customZip mb-2"" onclick=""showToast('zip');"" target=""_blank"" href='scaricaZipRinnovo.aspx?codR=" _
                                 & deEnco.QueryStringEncode(Data.FixNull(dr("IDRinnovoM"))) & "&record_ID=" & deEnco.QueryStringEncode(idrecordMaster) & "&nomeFilePC=" _
                                 & deEnco.QueryStringEncode(ArrayZip(0)) & "'><i class=""bi bi-person-badge""> </i>" & "Richiesta " & Data.FixNull(dr("IDRinnovoM")) & " - Scarica tutte le tessere</a>"))
                            Else
                                phDash.Controls.Add(New LiteralControl("Richiesta " & Data.FixNull(dr("IDRinnovoM"))))

                            End If
                            phDash.Controls.Add(New LiteralControl("</span>"))


                            phDash.Controls.Add(New LiteralControl("</div>"))

                            '***
                        End If

                        'accordion card
                        phDash.Controls.Add(New LiteralControl("<div class=""card mb-2 shadow-sm rounded"">"))
                        'accordion heder
                        phDash.Controls.Add(New LiteralControl("<div class=""card-header"">"))

                        phDash.Controls.Add(New LiteralControl("<div Class=""container-fluid"">"))

                        ' inizio prima riga

                        phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))


                        phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-8 text-left"">"))

                        'phDash.Controls.Add(New LiteralControl("Rinnovo:  "))
                        'phDash.Controls.Add(New LiteralControl("<span  " & Utility.statusColorCorsi(Data.FixNull(dr("Codice_Status"))) & ">"))
                        'phDash.Controls.Add(New LiteralControl("<a name=" & Data.FixNull(dr("IDRinnovo")) & ">" & Data.FixNull(dr("IDRinnovo")) & "</a>"))
                        'phDash.Controls.Add(New LiteralControl())

                        'phDash.Controls.Add(New LiteralControl("</span><br />"))

                        phDash.Controls.Add(New LiteralControl("Codice Richiesta: <small><b>" & Data.FixNull(dr("IDRinnovoM")) & "</b></small><br />"))




                        phDash.Controls.Add(New LiteralControl("Nominativo: <small>" & Data.FixNull(dr("Asi_Nome")) & " " & Data.FixNull(dr("Asi_Cognome")) & "</small><br />"))

                        'phDash.Controls.Add(New LiteralControl("CF: <small>" & Data.FixNull(dr("Asi_CodiceFiscale")) & "</small><br />"))
                        'phDash.Controls.Add(New LiteralControl("Data Scadenza: <small>" & Data.SonoDieci(Data.FixNull(dr("Asi_DataScadenza"))) & "</small><br />"))
                        'phDash.Controls.Add(New LiteralControl("Codice Iscrizione: <small>" & Data.FixNull(dr("Asi_CodiceIscrizione")) & "</small><br />"))
                        'phDash.Controls.Add(New LiteralControl("Tessera ASI: <small>" & Data.FixNull(dr("Asi_CodiceTessera")) & "</small><br />"))
                        'phDash.Controls.Add(New LiteralControl("-------------------------------<br />"))
                        'phDash.Controls.Add(New LiteralControl("Sport: <small>" & Data.FixNull(dr("Asi_Sport")) & "</small><br />"))
                        'phDash.Controls.Add(New LiteralControl("Disciplina: <small>" & Data.FixNull(dr("Asi_Disciplina")) & "</small><br />"))
                        'phDash.Controls.Add(New LiteralControl("Specialità: <small>" & Data.FixNull(dr("Asi_Specialita")) & "</small><br />"))
                        'phDash.Controls.Add(New LiteralControl("Livello: <small>" & Data.FixNull(dr("Asi_Livello")) & "</small><br />"))
                        'phDash.Controls.Add(New LiteralControl("Qualifica: <small>" & Data.FixNull(dr("Asi_Qualifica")) & "</small><br />"))

                        phDash.Controls.Add(New LiteralControl("CF: <small>" & Data.FixNull(dr("Asi_CodiceFiscale")) & "</small><br />"))
                        phDash.Controls.Add(New LiteralControl("Tess. ASI: <small>" & Data.FixNull(dr("Asi_CodiceTessera")) & "</small><br />"))
                        phDash.Controls.Add(New LiteralControl("Tess. Tecnico: <small>" & Data.FixNull(dr("Asi_CodiceIscrizione")) & "</small><br />"))
                        phDash.Controls.Add(New LiteralControl("Data Scadenza: <small>" & Data.SonoDieci(Data.FixNull(dr("Asi_DataScadenza"))) & "</small><br />"))
                        phDash.Controls.Add(New LiteralControl("Nuovo Tess. Tecnico: <small><b>" & Data.FixNull(dr("Asi_CodiceIscrizioneNuovo")) & "</b></small><br />"))
                        phDash.Controls.Add(New LiteralControl("Nuova Data Scadenza: <small><b>" & Data.SonoDieci(Data.FixNull(dr("Asi_DataScadenzaNuovo"))) & "</b></small><br />"))

                        phDash.Controls.Add(New LiteralControl("-------------------------------<br />"))
                        phDash.Controls.Add(New LiteralControl("Sport: <small>" & Data.FixNull(dr("Asi_Sport")) & "</small><br />"))
                        phDash.Controls.Add(New LiteralControl("Disciplina: <small>" & Data.FixNull(dr("Asi_Disciplina")) & "</small><br />"))
                        phDash.Controls.Add(New LiteralControl("Specialità: <small>" & Data.FixNull(dr("Asi_Specialita")) & "</small><br />"))
                        phDash.Controls.Add(New LiteralControl("Qualifica: <small>" & Data.FixNull(dr("Asi_Qualifica")) & "</small><br />"))
                        phDash.Controls.Add(New LiteralControl("Livello: <small>" & Data.FixNull(dr("Asi_Livello")) & "</small><br />"))


                        phDash.Controls.Add(New LiteralControl())

                        ' phDash.Controls.Add(New LiteralControl("</span>"))

                        phDash.Controls.Add(New LiteralControl("</div>"))


                        phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4  text-left"">"))

                        phDash.Controls.Add(New LiteralControl("</span><small>Status: </small><small " & Utility.statusColorTextCorsi(Data.FixNull(dr("Codice_Status"))) & ">" & Data.FixNull(dr("Descrizione_StatusWeb")) & "</small><br /><br />"))
                        If Not String.IsNullOrEmpty(dr("Asi_codiceEnteEx")) Then
                            phDash.Controls.Add(New LiteralControl("Ente di Origine:<br /> <small>" & Data.FixNull(dr("Asi_NomeEnteEx")) & "</small><br /><br />"))


                        End If

                        '  phDash.Controls.Add(New LiteralControl("</span><small>Tessera: </small><small></small><br /><br />"))

                        If tessera = "..\img\noPdf.jpg" Then
                            '     phDash.Controls.Add(New LiteralControl("<td><img src='" & tessera & "' height='70' width='70' alt='" & Data.FixNull(dr("Asi_Nome")) & " " & Data.FixNull(dr("Asi_Cognome")) & "'></td>"))


                        Else
                            phDash.Controls.Add(New LiteralControl("<a class=""btn btn-success btn-sm btn-due btn-custom"" onclick="" showToast('tesserino');"" target=""_blank"" href='scaricaTesseraRinnovo2.aspx?codR=" _
                             & deEnco.QueryStringEncode(Data.FixNull(dr("IDRinnovo"))) & "&record_ID=" & deEnco.QueryStringEncode(dr("id_record")) & "&nomeFilePC=" _
                             & deEnco.QueryStringEncode(Data.FixNull(dr("StringaNomeFile"))) & "&nominativo=" _
                             & deEnco.QueryStringEncode(Data.FixNull(dr("Asi_Cognome")) & "_" & Data.FixNull(dr("Asi_Nome"))) & "'><i class=""bi bi-person-badge""> </i>Scarica Tessera</a>"))


                        End If
                        phDash.Controls.Add(New LiteralControl("</div>"))





                        phDash.Controls.Add(New LiteralControl("</div>"))

                        Counter1 += 1
                        phDash.Controls.Add(New LiteralControl("</div>"))

                        phDash.Controls.Add(New LiteralControl("</div>"))

                        phDash.Controls.Add(New LiteralControl("</div>"))
                        phDash.Controls.Add(New LiteralControl("</div>"))



                        ' Else




                        ' End If
                        rompiStatus = Data.FixNull(dr("IDRinnovoM"))
                    Next
                Else
                    'Response.Write("ko")
                    phDash.Visible = False
                    Session("procedi") = "KO"
                    Response.Redirect("DashboardRinnoviEvasi2.aspx?tipo=" & tipoInserito & "&ris=" & deEnco.QueryStringEncode("ko"))
                End If

            Catch ex As Exception

            End Try
        End If
    End Sub

    Protected Sub lnkLast10_Click(sender As Object, e As EventArgs) Handles lnkLast10.Click
        rinnovi()
    End Sub
End Class