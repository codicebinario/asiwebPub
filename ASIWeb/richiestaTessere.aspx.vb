Imports fmDotNet
Imports System.Web.Services
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed
Public Class richiestaTessere
    Inherits System.Web.UI.Page
    Dim webserver As String = ConfigurationManager.AppSettings("webserver")
    Dim utente As String = ConfigurationManager.AppSettings("utente")
    Dim porta As String = ConfigurationManager.AppSettings("porta")
    Dim pass As String = ConfigurationManager.AppSettings("pass")
    Dim dbb As String = ConfigurationManager.AppSettings("dbb")
    Dim cultureFormat As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("it-IT")
    Dim deEnco As New Ed()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("login.aspx")
        End If
        If IsNothing(Session("codice")) Then
            Response.Redirect("login.aspx")
        End If

        '  Dim newCulture As System.Globalization.CultureInfo = System.Globalization.CultureInfo.CurrentUICulture.Clone()
        cultureFormat.NumberFormat.CurrencySymbol = "€"
        cultureFormat.NumberFormat.CurrencyDecimalDigits = 2
        cultureFormat.NumberFormat.CurrencyGroupSeparator = String.Empty
        cultureFormat.NumberFormat.CurrencyDecimalSeparator = ","
        System.Threading.Thread.CurrentThread.CurrentCulture = cultureFormat
        System.Threading.Thread.CurrentThread.CurrentUICulture = cultureFormat


        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("login.aspx")
        End If
        Dim record_ID As String = ""
        record_ID = deEnco.QueryStringDecode(Request.QueryString("record_ID"))
        If Not String.IsNullOrEmpty(record_ID) Then

            Session("id_richiesta") = record_ID

        End If

        If IsNothing(Session("id_richiesta")) Then
            Response.Redirect("login.aspx")
        End If
        Dim codR As String = ""
        codR = deEnco.QueryStringDecode(Request.QueryString("codR"))
        If Not String.IsNullOrEmpty(codR) Then


            Session("Codice_Richiesta") = codR

        End If


        'If Not IsNothing(Session("denominazione")) Then

        '    litDenominazioneJumbo.Text = "Codice: " & AsiModel.LogIn.Codice & " - " & "Tipo Ente: " & AsiModel.LogIn.TipoEnte & " - " & AsiModel.LogIn.Denominazione
        LitNumeroRichiesta.Text = Session("Codice_Richiesta")
        lblRichiestaConferma.Text = Session("Codice_Richiesta")

        'End If



        If IsNothing(Session("Codice_Richiesta")) Then
            Response.Redirect("Dashboard.aspx")
        End If



        If Not Page.IsPostBack Then


            Articoli()
            CarrelloRichieste()






        End If

    End Sub
    Protected Sub scrivi(sender As Object, e As EventArgs) Handles txtNote.TextChanged


        If Not IsNothing(Session("id_richiesta")) Or Not String.IsNullOrEmpty(Session("id_richiesta")) Then
            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            '  Dim ds As DataSet
            Dim risposta As String = ""
            fmsP.SetLayout("web_richiesta_master")
            Dim Request = fmsP.CreateEditRequest(Session("id_richiesta"))


            Request.AddField("Note_Ente", Data.PrendiStringaT(Server.HtmlEncode(txtNote.Text)))
            '    Request.AddField("Note_Ente_urlEnco", Server.UrlEncode(txtNote.Text))
            Request.AddScript("SistemaEncoding", Session("id_richiesta"))
            risposta = Request.Execute()




        Else
            Session("auth") = "0"
            Session("auth") = Nothing
            Response.Redirect("login.aspx")

        End If
        CarrelloRichieste()
    End Sub

    'Protected Sub cancellaRiga(sender As Object, e As EventArgs) Handles lnkQuantita.Click
    '    Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
    '    '  Dim ds As DataSet


    '    fmsP.SetLayout("web_richiesta_dettaglio")
    '    Dim Request = fmsP.CreateDeleteRequest(ID)


    '    Request.Execute()

    '    CarrelloRichieste()

    'End Sub

    <WebMethod>
    Public Shared Function aggiorna() As Integer

        Dim cancella As New richiestaTessere()
        cancella.CarrelloRichieste()


        Return 1

    End Function


    <WebMethod>
    Public Shared Function delete(id As Integer, idRichiesta As Integer) As Integer





        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        '  Dim ds As DataSet

        fmsP.SetLayout("web_richiesta_dettaglio")
        Dim Request = fmsP.CreateDeleteRequest(id)
        Request.AddScript("Calcola_Sconto", idRichiesta)

        Request.Execute()
        'Dim cancella As New richiestaTessere()
        'cancella.CarrelloRichieste()


        Return 1

    End Function


    Sub Articoli()

        Dim mese As Integer = DatePart(DateInterval.Month, Now)

        Dim ds As DataSet

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("web_prodotti_enti")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        RequestP.AddSearchField("Codice_Ente", Session("codice"), Enumerations.SearchOption.equals)
        RequestP.AddSearchField("Gruppo_Articoli", "Tesseramento", Enumerations.SearchOption.equals)
        'RequestP.AddSearchField("Mese_Da", mese, Enumerations.SearchOption.biggerOrEqualThan)
        'RequestP.AddSearchField("Mese_A", mese, Enumerations.SearchOption.lessOrEqualThan)
        RequestP.AddSortField("nome_articolo", Enumerations.Sort.Ascend)

        '    Try
        ds = RequestP.Execute()
        Dim nuovoDs As DataSet = New DataSet("Articoli_Settori")
        Dim table1 As DataTable = New DataTable("Articoli")
        table1.Columns.Add("nome_articolo")
        table1.Columns.Add("codice_articolo")

        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
            For Each dr In ds.Tables("main").Rows

                If Compreso(dr("Mese_Da"), dr("Mese_A"), mese) = True Then



                    table1.Rows.Add(dr("Nome_Articolo"), dr("Codice_articolo"))





                End If





            Next

            nuovoDs.Tables.Add(table1)

            dllArticoli.DataSource = nuovoDs
            dllArticoli.DataTextField = "nome_articolo"
            dllArticoli.DataValueField = "codice_articolo"
            dllArticoli.DataBind()

            dllArticoli.Items.Insert(0, New ListItem("seleziona"))

        End If

        '   Catch


        '   End Try



    End Sub
    Function Compreso(da_from As String, da_to As String, mese As Integer) As Boolean
        Dim esiste As Boolean
        Dim dada As Boolean = False
        Dim aa As Boolean = False


        If mese >= CType(da_from, Integer) Then
            dada = True
        End If
        If mese <= CType(da_to, Integer) Then
            aa = True

        End If

        If dada = True AndAlso aa = True Then
            esiste = True
        Else
            esiste = False
        End If

        Return esiste

    End Function


    Protected Sub dllArticoli_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dllArticoli.SelectedIndexChanged


        If dllArticoli.SelectedItem.Text.ToString() = "seleziona" Then


            'LitCodiceArticolo.Text = ""
            'LitNome_Articolo.Text = ""
            ''  rqQuantita.Enabled = False

        Else

            AsiModel.CodiceArticolo = dllArticoli.SelectedItem.Value.ToString
            AsiModel.NomeArticolo = dllArticoli.SelectedItem.Text.ToString()
            'LitCodiceArticolo.Text = AsiModel.CodiceArticolo
            'LitNome_Articolo.Text = AsiModel.NomeArticolo
            ''   rqQuantita.Enabled = True
        End If




    End Sub

    Protected Sub lnkQuantita_Click(sender As Object, e As EventArgs) Handles lnkQuantita.Click
        If Page.IsValid Then



            NuovaRichiestaDettaglio()

            'txtQuantita.Text = Nothing

            '   Response.Redirect(Request.Url.AbsoluteUri)



        End If
    End Sub


    Sub NuovaRichiestaDettaglio()
        '  Dim litNumRichieste As Literal = DirectCast(ContentPlaceHolder1.FindControl("LitNumeroRichiesta"), Literal)

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        ' Dim ds As DataSet


        fmsP.SetLayout("web_richiesta_dettaglio")
        Dim Request = fmsP.CreateNewRecordRequest()

        Request.AddField("Codice_Richiesta", Session("Codice_Richiesta"))
        Request.AddField("Codice_Articolo", AsiModel.CodiceArticolo)
        Request.AddField("Quantita", txtQuantita.Text)
        Request.AddScript("Calcola_Sconto", Session("Codice_Richiesta"))

        Dim codiceRichiestaDettaglio As String = ""
        codiceRichiestaDettaglio = Request.Execute()

        txtQuantita.Text = String.Empty

        dllArticoli.SelectedValue = Nothing

        Dim codiceRichiesta As String = Session("Codice_Richiesta")

        '''''  leggere anche totale e totale scontato

        MettiStatusAdUno()
        CarrelloRichieste()

    End Sub
    Sub MettiStatusAdUno()


        If Not IsNothing(Session("id_richiesta")) Or Not String.IsNullOrEmpty(Session("id_richiesta")) Then
            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            '  Dim ds As DataSet
            Dim risposta As String = ""
            fmsP.SetLayout("web_richiesta_master")
            Dim Request = fmsP.CreateEditRequest(Session("id_richiesta"))


            Request.AddField("Status_ID", "1")

            Try
                risposta = Request.Execute()
                AsiModel.LogIn.LogCambioStatus(Session("Codice_Richiesta"), "1", Session("WebUserEnte"))
            Catch ex As Exception

            End Try





        Else
            Session("auth") = "0"
            Session("auth") = Nothing
            Session.Clear()
            Response.Redirect("login.aspx")

        End If

    End Sub

    Sub CarrelloRichieste()




        Dim deTtotali As New GetTotali()

        deTtotali = GetTotaliRichiesta(Session("Codice_Richiesta"))

        Dim ImportoRighe As Decimal = deTtotali.ImportoRighe
        Dim ImportoSconto As Decimal = deTtotali.ImportoSconto
        Dim ImportoTessere As Decimal = deTtotali.ImportoTessere
        Dim totalesconto As Decimal = ImportoSconto


        '  Response.Write("importo sconto: " & ImportoSconto)

        Dim DaPagare As Decimal = CType(ImportoRighe, Decimal) - CType(ImportoSconto, Decimal)
        Dim DaPagare2 As Decimal = ImportoRighe - ImportoSconto

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        Dim ds As DataSet


        fmsP.SetLayout("web_richiesta_dettaglio")

        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        RequestP.AddSearchField("Codice_Richiesta", Session("Codice_Richiesta"), Enumerations.SearchOption.equals)

        ds = RequestP.Execute()

        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

            PnlCarrello.Visible = True
            Dim counter As Integer = 0
            Dim totale As Decimal = 0
            Dim tessereTotale As Integer = 0

            For Each dr In ds.Tables("main").Rows
                counter += 1
                totalesconto += totalesconto

                '    Dim price As Decimal = FormatCurrency(dr("Prezzo_Finale"), 2)
                Dim price As Decimal = dr("Prezzo_Finale")
                Dim prezzoUnitario As Decimal = dr("Prezzo_Unitario_Listino")
                '  Dim importoRighe As Decimal =
                Dim tessere As Integer = dr("Quantita")
                totale += price
                tessereTotale += tessere

                plCarrello.Controls.Add(New LiteralControl("<tr>"))
                plCarrello.Controls.Add(New LiteralControl("<th scope ='row'>"))

                '   plCarrello.Controls.Add(New LiteralControl("<Button type='button' onclick='return Cancella(" & Trim(dr("Record_ID")) & ");'>C</button>"))    <i class="fa fa-drivers-license-o p-2"></i>
                plCarrello.Controls.Add(New LiteralControl("<a href='#' data-toggle='tooltip' data-placement='top' title='Cancella riga' onclick='return Cancella(" & Trim(dr("Record_ID")) & ", " & Session("Codice_Richiesta") & ");'><i class=""fa fa-window-close p-2""></i></a></th>"))
                ' plCarrello.Controls.Add(New LiteralControl("<asp:LinkButton ID='lnkCancella_" & Trim(dr("Record_ID")) & " OnClick='cancellaRiga' runat='server' class='btn btn-custom align-middle text-left'>" & Trim(dr("Record_ID")) & "<i class=""fa fa-window-close p-2""></i></asp:LinkButton></th>"))







                'plCarrello.Controls.Add(New LiteralControl("<a href='can.aspx?id=" & Trim(dr("Record_ID")) & "&k=" & AsiModel.CodiceRichiesta & "' data-toggle='tooltip' data-placement='top' title='Cancella riga' > C</a></th>"))

                plCarrello.Controls.Add(New LiteralControl("<th scope ='row'>" & counter & "</th>"))
                plCarrello.Controls.Add(New LiteralControl("<td class='text-center'>" & Data.FixNull(dr("Codice_Articolo")) & "</td>"))
                plCarrello.Controls.Add(New LiteralControl("<td class='text-left'>" & Data.FixNull(dr("Nome_Articolo")) & "</td>"))
                plCarrello.Controls.Add(New LiteralControl("<td class='text-center'>" & Data.FixNull(dr("Quantita")) & "</td>"))
                plCarrello.Controls.Add(New LiteralControl("<td class='text-right'>" & prezzoUnitario.ToString("C", New System.Globalization.CultureInfo("it-IT")) & "</td>"))
                plCarrello.Controls.Add(New LiteralControl("<td class='text-right'>" & price.ToString("C", New System.Globalization.CultureInfo("it-IT")) & "</td>"))
                plCarrello.Controls.Add(New LiteralControl("</tr>"))

                txtNote.Text = Data.FixNull(dr("TBL_Richiesta_Tessere::Note_Ente"))

            Next

            If totalesconto <> 0 Then
                'totale righe

                plCarrello.Controls.Add(New LiteralControl("<tr>"))
                plCarrello.Controls.Add(New LiteralControl("<th scope ='row'></th>"))
                plCarrello.Controls.Add(New LiteralControl("<th scope ='row'></th>"))

                plCarrello.Controls.Add(New LiteralControl("<td class='text-center'></td>"))
                plCarrello.Controls.Add(New LiteralControl("<td class='text-center'></td>"))
                plCarrello.Controls.Add(New LiteralControl("<td class='text-center'></td>"))

                plCarrello.Controls.Add(New LiteralControl("<td Class='text-right'><b>Importo Totale</b></td>"))
                plCarrello.Controls.Add(New LiteralControl("<td class='text-right'><b>" & ImportoRighe.ToString("C", New System.Globalization.CultureInfo("it-IT")) & "</b></td>"))

                '  plCarrello.Controls.Add(New LiteralControl("<td class='text-right'><b>" & totale.ToString("C2", New System.Globalization.CultureInfo("it-IT")) & "</b></td>"))
                plCarrello.Controls.Add(New LiteralControl("</tr>"))

                ' totale sconto

                plCarrello.Controls.Add(New LiteralControl("<tr>"))
                plCarrello.Controls.Add(New LiteralControl("<th scope ='row'></th>"))
                plCarrello.Controls.Add(New LiteralControl("<th scope ='row'></th>"))

                plCarrello.Controls.Add(New LiteralControl("<td class='text-center'></td>"))
                plCarrello.Controls.Add(New LiteralControl("<td class='text-center'></td>"))
                plCarrello.Controls.Add(New LiteralControl("<td class='text-center'></td>"))

                plCarrello.Controls.Add(New LiteralControl("<td Class='text-right'><b>Importo Tessere</b></td>"))
                plCarrello.Controls.Add(New LiteralControl("<td class='text-right'><b>" & ImportoTessere.ToString("C", New System.Globalization.CultureInfo("it-IT")) & "</b></td>"))

                '  plCarrello.Controls.Add(New LiteralControl("<td class='text-right'><b>" & totale.ToString("C2", New System.Globalization.CultureInfo("it-IT")) & "</b></td>"))
                plCarrello.Controls.Add(New LiteralControl("</tr>"))


                ' totale sconto

                plCarrello.Controls.Add(New LiteralControl("<tr>"))
                plCarrello.Controls.Add(New LiteralControl("<th scope ='row'></th>"))
                plCarrello.Controls.Add(New LiteralControl("<th scope ='row'></th>"))

                plCarrello.Controls.Add(New LiteralControl("<td class='text-center'></td>"))
                plCarrello.Controls.Add(New LiteralControl("<td class='text-center'></td>"))
                plCarrello.Controls.Add(New LiteralControl("<td class='text-center'></td>"))

                plCarrello.Controls.Add(New LiteralControl("<td Class='text-right'><b>Sconto</b></td>"))
                plCarrello.Controls.Add(New LiteralControl("<td class='text-right'><b>" & ImportoSconto.ToString("C", New System.Globalization.CultureInfo("it-IT")) & "</b></td>"))

                '  plCarrello.Controls.Add(New LiteralControl("<td class='text-right'><b>" & totale.ToString("C2", New System.Globalization.CultureInfo("it-IT")) & "</b></td>"))
                plCarrello.Controls.Add(New LiteralControl("</tr>"))

                ' totale da pagare
            End If

            plCarrello.Controls.Add(New LiteralControl("<tr>"))
                plCarrello.Controls.Add(New LiteralControl("<th scope ='row'></th>"))
                plCarrello.Controls.Add(New LiteralControl("<th scope ='row'></th>"))

                plCarrello.Controls.Add(New LiteralControl("<td class='text-center'></td>"))
                plCarrello.Controls.Add(New LiteralControl("<td class='text-center'></td>"))
                plCarrello.Controls.Add(New LiteralControl("<td class='text-center'></td>"))

                plCarrello.Controls.Add(New LiteralControl("<td Class='text-right'><b>Importo Ordine</b></td>"))
                plCarrello.Controls.Add(New LiteralControl("<td class='text-right'><b>" & DaPagare.ToString("C", New System.Globalization.CultureInfo("it-IT")) & "</b></td>"))

                '  plCarrello.Controls.Add(New LiteralControl("<td class='text-right'><b>" & totale.ToString("C2", New System.Globalization.CultureInfo("it-IT")) & "</b></td>"))
                plCarrello.Controls.Add(New LiteralControl("</tr>"))

            Session("Importo_Totale") = DaPagare2
            Session("TessereTotali") = tessereTotale



        Else

                PnlCarrello.Visible = False

        End If



    End Sub



    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click


        If Not IsNothing(Session("id_richiesta")) Or Not String.IsNullOrEmpty(Session("id_richiesta")) Then
            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            '  Dim ds As DataSet
            Dim risposta As String = ""
            fmsP.SetLayout("web_richiesta_master")
            Dim Request = fmsP.CreateEditRequest(Session("id_richiesta"))

            Request.AddField("pre_stato_web", "1")
            '    Request.AddField("Note_Ente", txtNote.Text)
            Request.AddField("Data_Ordine", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss"))
            'Request.AddField("Importo_Ordine", Session("Importo_Totale").ToString())
            'Request.AddField("Importo_OrdineText", Session("Importo_Totale").ToString())
            '  Request.AddField("Importo_Ordine", String.Format("{0:C2}", Session("Importo_Totale")))
            Request.AddField("Importo_OrdineText", String.Format("{0:C2}", Session("Importo_Totale")))
            Request.AddField("Tessere", Session("TessereTotali"))
            If chkValutazioneSconto.Checked Then

                Request.AddField("Status_ID", "2")
            Else
                Request.AddField("Status_ID", "3")

            End If
            Request.AddField("Note_Ente", Data.PrendiStringaT(Server.HtmlEncode(txtNote.Text)))

            Request.AddScript("SistemaEncoding", Session("id_richiesta"))

            Try
                risposta = Request.Execute()
                If chkValutazioneSconto.Checked Then
                    AsiModel.LogIn.LogCambioStatus(Session("Codice_Richiesta"), "2", Session("WebUserEnte"))
                Else
                    AsiModel.LogIn.LogCambioStatus(Session("Codice_Richiesta"), "3", Session("WebUserEnte"))
                End If

            Catch ex As Exception

            End Try


            Session("Codice_Richiesta") = Nothing

            Response.Redirect("Dashboard.aspx")
        Else
            Session("auth") = "0"
            Session("auth") = Nothing
            Response.Redirect("login.aspx")

        End If




        '   Response.Redirect("ciao.aspx")
    End Sub
End Class
