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
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Impossibile andare avanti. Dati Albo da normalizzare.<br />Codice Fiscale e/o Codice Ente Affiliante inesistenti in Albo!<br />Contattare ASI per risolvere il problema. Scrivi ad albo@asinazionale.it ' ).set('resizable', true).resizeTo('20%', 300);", True)
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



        Dim aperto As String
        aperto = Request.QueryString("aperto")
        '   rinnovi2()
        aperto = Request.QueryString("aperto")
        open = Request.QueryString("open")
        rinnovi()
    End Sub
    Sub rinnovi2()

        Dim ds As DataSet

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("webRinnoviRichiesta2")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        ' RequestP.AddSearchField("pre_stato_web", "1")
        RequestP.AddSearchField("Codice_Ente_Richiedente", Session("codice"), Enumerations.SearchOption.equals)
        RequestP.AddSortField("Codice_Status", Enumerations.Sort.Ascend)
        RequestP.AddSortField("IDRinnovo", Enumerations.Sort.Descend)
        ds = RequestP.Execute()

        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
            'rpDash.DataSource = ds.Tables("main")
            'rpDash.DataBind()
        End If
    End Sub

    Sub rinnovi()

        Dim deEnco As New Ed()
        Dim heading As String = "heading"
        Dim collapse As String = "collapse"
        Dim quantiPerGruppo As Integer = 0
        Dim ds As DataSet

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("webRinnoviMaster")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        ' RequestP.AddSearchField("pre_stato_web", "1")
        RequestP.AddSearchField("CodiceEnteRichiedente", Session("codice"), Enumerations.SearchOption.equals)
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




                counter2 += 1


                Dim addRinnovo As New LinkButton
                addRinnovo.ID = "Rin_" & counter2
                addRinnovo.Attributes.Add("runat", "server")
                addRinnovo.Text = "<i class=""bi bi-emoji-sunglasses""> </i>Nuovo Rinnovo"

                addRinnovo.PostBackUrl = "checkTesseramentoRinnovi2.aspx?codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("IDRinnovoM"))))
                ' fotoCorsistiLnk.PostBackUrl = "UpFotoRinnovo.aspx?codR=" & Data.FixNull(dr("IDRinnovo")) & "&record_ID=" & dr("id_record")

                addRinnovo.CssClass = "btn btn-success btn-sm  btn-custom  mb-1  mr-1"

                addRinnovo.Visible = True


                Dim Chiudi As New LinkButton
                Chiudi.ID = "Clo_" & counter2
                Chiudi.Attributes.Add("runat", "server")
                Chiudi.Text = "<i class=""bi bi-emoji-sunglasses""> </i>Termina questo gruppo"

                Chiudi.PostBackUrl = "closeRinnovo.aspx?idrecord=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("idrecord")))) & "&codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("IDRinnovoM"))))
                ' fotoCorsistiLnk.PostBackUrl = "UpFotoRinnovo.aspx?codR=" & Data.FixNull(dr("IDRinnovo")) & "&record_ID=" & dr("id_record")

                Chiudi.CssClass = "btn btn-success btn-sm  btn-custom  mb-1"

                Chiudi.Visible = True



                phDash.Controls.Add(New LiteralControl("<div Class=""accordion-item"">"))

                phDash.Controls.Add(New LiteralControl("<h2 Class=""accordion-header"" id=""" & heading & "_" & Data.FixNull(dr("IDRinnovoM")) & """>"))
                If Data.FixNull(dr("IDRinnovoM")) = open Then
                    phDash.Controls.Add(New LiteralControl("<button Class=""accordion-button"" type=""button"" data-bs-toggle=""collapse"" data-bs-target=""#collapse" & Data.FixNull(dr("IDRinnovoM")) & """ aria-expanded=""False"" aria-controls=""collapse" & Data.FixNull(dr("IDRinnovoM")) & """>"))

                Else
                    phDash.Controls.Add(New LiteralControl("<button Class=""accordion-button collapsed"" type=""button"" data-bs-toggle=""collapse"" data-bs-target=""#collapse" & Data.FixNull(dr("IDRinnovoM")) & """ aria-expanded=""False"" aria-controls=""collapse" & Data.FixNull(dr("IDRinnovoM")) & """>"))

                End If
                quantiPerGruppo = AsiModel.Rinnovi.quanteRichiestePerGruppo(dr("IDRinnovoM"))

                '   phDash.Controls.Add(New LiteralControl("<button Class=""accordion-button collapsed"" type=""button"" data-bs-toggle=""collapse"" data-bs-target=""#collapse" & Data.FixNull(dr("IDRinnovoM")) & """ aria-expanded=""False"" aria-controls=""collapse" & Data.FixNull(dr("IDRinnovoM")) & """>"))
                phDash.Controls.Add(New LiteralControl("Gruppo Rinnovi: " & " numero " & Data.FixNull(dr("IDRinnovoM")) & " del " & Data.FixNull(dr("CreationTimestamp")) & " - " & quantiPerGruppo & " richieste"))
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
                    If quantiPerProgetto >= 1 Then
                        phDash.Controls.Add(Chiudi)
                    End If

                    'phDash.Controls.Add(New LiteralControl("<a class=""btn btn-primary "" href=""checkTesseramentoRinnovi2.aspx?codR=" & deEnco.QueryStringEncode(dr("IDRinnovoM")) & """>aggiungi Rinnovo al Gruppo</a>"))
                    phDash.Controls.Add(New LiteralControl("</p>"))

                End If
                ' corpo del contenuto del panel
                '  phDash.Controls.Add(New LiteralControl(Data.FixNull(dr("IDRinnovoM"))))

                Dim ds1 As DataSet

                Dim fmsP1 As FMSAxml = AsiModel.Conn.Connect()
                fmsP1.SetLayout("webRinnoviRichiesta2")
                Dim RequestP1 = fmsP1.CreateFindRequest(Enumerations.SearchType.Subset)
                ' RequestP.AddSearchField("pre_stato_web", "1")
                RequestP1.AddSearchField("IDRinnovoM", Data.FixNull(dr("IDRinnovoM")), Enumerations.SearchOption.equals)
                RequestP1.AddSortField("Codice_Status", Enumerations.Sort.Ascend)
                RequestP1.AddSortField("IDRinnovo", Enumerations.Sort.Descend)
                ds1 = RequestP1.Execute()

                If Not IsNothing(ds1) AndAlso ds1.Tables("main").Rows.Count > 0 Then

                    Dim counter1 As Integer = 0
                    'Dim totale As Decimal = 0
                    'Dim codR As String
                    'Dim record_id As String
                    For Each dr1 In ds1.Tables("main").Rows

                        If Data.FixNull(dr1("Codice_Status")) = "151" Or Data.FixNull(dr1("Codice_Status")) = "152" Or Data.FixNull(dr1("Codice_Status")) = "153" _
                Or Data.FixNull(dr1("Codice_Status")) = "154" Or Data.FixNull(dr1("Codice_Status")) = "155" _
                Or Data.FixNull(dr1("Codice_Status")) = "156" Or Data.FixNull(dr1("Codice_Status")) = "156" Or Data.FixNull(dr1("Codice_Status")) = "157" _
                Or Data.FixNull(dr1("Codice_Status")) = "158" Or Data.FixNull(dr1("Codice_Status")) = "159" Then

                            counter1 += 1

                            If String.IsNullOrWhiteSpace(Data.FixNull(dr1("ASI_foto"))) Then
                                foto = "..\img\noimg.jpg"
                            Else
                                foto = "https://93.63.195.98" & Data.FixNull(dr1("ASI_foto"))
                            End If

                            Dim fotoCorsistiLnk As New LinkButton
                            fotoCorsistiLnk.ID = "Fot_" & counter1
                            fotoCorsistiLnk.Attributes.Add("runat", "server")
                            fotoCorsistiLnk.Text = "<i class=""bi bi-emoji-sunglasses""> </i>Foto Opzionale"

                            fotoCorsistiLnk.PostBackUrl = "UpFotoRinnovo2.aspx?codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr1("IDRinnovo")))) & "&record_ID=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(dr1("id_record")))
                            ' fotoCorsistiLnk.PostBackUrl = "UpFotoRinnovo.aspx?codR=" & Data.FixNull(dr("IDRinnovo")) & "&record_ID=" & dr("id_record")

                            fotoCorsistiLnk.CssClass = "btn btn-success btn-sm btn-otto btn-custom  mb-1"

                            fotoCorsistiLnk.Visible = True





                            Dim Verb As New LinkButton

                            Verb.ID = "verb_" & counter1
                            Verb.Attributes.Add("runat", "server")
                            Verb.Text = "<i class=""bi bi-file""> </i>Invia cambio E.A."
                            Verb.PostBackUrl = "upDichiarazione2.aspx?codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr1("IDRinnovoM")))) & "&record_ID=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(dr1("id_record")))
                            Verb.CssClass = "btn btn-success btn-sm btn-sei btn-custom  mb-1"
                            If Data.FixNull(dr1("Codice_Status")) = "151" Then
                                Verb.Visible = True
                            Else
                                Verb.Visible = False
                            End If




                            phDash.Controls.Add(New LiteralControl("<div class=""col-sm-12 mb-3 mb-md-0"">"))
                            'accordion card
                            phDash.Controls.Add(New LiteralControl("<div class=""card mb-2 shadow-sm rounded"">"))
                            'accordion heder
                            phDash.Controls.Add(New LiteralControl("<div class=""card-header"">"))

                            phDash.Controls.Add(New LiteralControl("<div Class=""container-fluid"">"))

                            'inizio row

                            phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))

                            ' prima colonna
                            phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left"">"))


                            phDash.Controls.Add(New LiteralControl("Rinnovo:  "))
                            phDash.Controls.Add(New LiteralControl("<span  " & Utility.statusColorCorsi(Data.FixNull(dr1("Codice_Status"))) & ">"))
                            phDash.Controls.Add(New LiteralControl("<a name=" & Data.FixNull(dr1("IDRinnovo")) & ">" & Data.FixNull(dr1("IDRinnovo")) & "</a>"))
                            phDash.Controls.Add(New LiteralControl())

                            phDash.Controls.Add(New LiteralControl("</span><br />"))

                            phDash.Controls.Add(New LiteralControl("Nominativo: <small>" & Data.FixNull(dr1("Asi_Nome")) & " " & Data.FixNull(dr1("Asi_Cognome")) & "</small><br />"))

                            phDash.Controls.Add(New LiteralControl("CF: <small>" & Data.FixNull(dr1("Asi_CodiceFiscale")) & "</small><br />"))
                            phDash.Controls.Add(New LiteralControl("Tess. ASI: <small>" & Data.FixNull(dr1("Asi_CodiceTessera")) & "</small><br />"))
                            phDash.Controls.Add(New LiteralControl("Tess.Tecnico: <small>" & Data.FixNull(dr1("Asi_CodiceIscrizione")) & "</small><br />"))
                            phDash.Controls.Add(New LiteralControl("Data Scadenza: <small>" & Data.SonoDieci(Data.FixNull(dr1("Asi_DataScadenza"))) & "</small><br />"))

                            phDash.Controls.Add(New LiteralControl("-------------------------------<br />"))
                            phDash.Controls.Add(New LiteralControl("Sport: <small>" & Data.FixNull(dr1("Asi_Sport")) & "</small><br />"))
                            phDash.Controls.Add(New LiteralControl("Disciplina: <small>" & Data.FixNull(dr1("Asi_Disciplina")) & "</small><br />"))
                            phDash.Controls.Add(New LiteralControl("Specialità: <small>" & Data.FixNull(dr1("Asi_Specialita")) & "</small><br />"))
                            phDash.Controls.Add(New LiteralControl("Qualifica: <small>" & Data.FixNull(dr1("Asi_Qualifica")) & "</small><br />"))
                            phDash.Controls.Add(New LiteralControl("Livello: <small>" & Data.FixNull(dr1("Asi_Livello")) & "</small><br />"))



                            'fine prima colonna
                            phDash.Controls.Add(New LiteralControl("</div>"))

                            'inizio seconda colonna
                            phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4  text-left"">"))

                            phDash.Controls.Add(New LiteralControl("Status:<small " & Utility.statusColorTextCorsi(Data.FixNull(dr1("Codice_Status"))) & ">" & Data.FixNull(dr1("Descrizione_StatusWeb")) & "</small><br />"))
                            If Data.FixNull(dr1("Codice_Status")) = "151" Then
                                phDash.Controls.Add(New LiteralControl("Ente di Origine:<br /> <small>" & Data.FixNull(dr1("Asi_NomeEnteEx")) & "</small><br />"))


                            End If

                            'fine seconda colonna
                            phDash.Controls.Add(New LiteralControl("</div>"))


                            'inizio terza colonna
                            phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4  text-left"">"))

                            If dr("CheckWeb") = "n" Then
                                phDash.Controls.Add(Verb)

                            End If

                            phDash.Controls.Add(New LiteralControl("<br />"))
                            phDash.Controls.Add(fotoCorsistiLnk)
                            phDash.Controls.Add(New LiteralControl("<br /><br /><span class=""text-center"">"))


                            If foto = "..\img\noimg.jpg" Then
                                phDash.Controls.Add(New LiteralControl("<img src='" & foto & "' height='70' width='50' alt='" & Data.FixNull(dr1("Asi_Nome")) & " " & Data.FixNull(dr1("Asi_Cognome")) & "'>"))

                            Else
                                Dim myImage As Image = FotoS(foto)
                                Dim base64 As String = ImageHelper.ImageToBase64String(myImage, ImageFormat.Jpeg)
                                '  Response.Write("<img alt=""Embedded Image"" src=""data:image/Jpeg;base64," & base64 & """ />")
                                phDash.Controls.Add(New LiteralControl("<img src='data:image/Jpeg;base64," & base64 & "' height='70' width='50' alt='" & Data.FixNull(dr1("Asi_Nome")) & " " & Data.FixNull(dr1("Asi_Cognome")) & "'>"))

                            End If

                            phDash.Controls.Add(New LiteralControl("</span>"))

                            'fine terza colonna
                            phDash.Controls.Add(New LiteralControl("</div>"))


                            ' fine row
                            phDash.Controls.Add(New LiteralControl("</div>"))





                            phDash.Controls.Add(New LiteralControl("</div>"))

                            phDash.Controls.Add(New LiteralControl("</div>"))

                            phDash.Controls.Add(New LiteralControl("</div>"))
                            phDash.Controls.Add(New LiteralControl("</div>"))


                        End If
                    Next

                End If




                ' corpo del contenuto del panel

                phDash.Controls.Add(New LiteralControl("</div>"))



                phDash.Controls.Add(New LiteralControl("</div>"))


                phDash.Controls.Add(New LiteralControl("</div>"))
            Next


            phDash.Controls.Add(New LiteralControl("</div>"))


        End If





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