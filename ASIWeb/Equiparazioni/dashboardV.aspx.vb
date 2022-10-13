Imports fmDotNet
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed
Public Class dashboardV1
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



        End If
        If Not Page.IsPostBack Then


            Equiparazioni()





        End If
    End Sub

    Sub Equiparazioni()
        Dim ds As DataSet
        Dim pdf As String
        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("webEquiparazioniRichiesta")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        ' RequestP.AddSearchField("pre_stato_web", "1")
        RequestP.AddSearchField("Equi_Settore_Approvazione_ID", Session("codice"), Enumerations.SearchOption.equals)
        RequestP.AddSortField("Codice_Status", Enumerations.Sort.Ascend)
        RequestP.AddSortField("IDEquiparazione", Enumerations.Sort.Descend)



        ds = RequestP.Execute()

        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then






            phDash.Visible = True
            'Dim counter As Integer = 0
            Dim counter1 As Integer = 0
            'Dim totale As Decimal = 0
            For Each dr In ds.Tables("main").Rows




                If Data.FixNull(dr("Codice_Status")) = "105" Then
                    counter1 += 1

                    Dim deEnco As New Ed()
                    Dim nomeFile As String
                    nomeFile = Data.FixNull(dr("NomeFileDiplomaFS"))

                    ' da valutare il diploma sia caricato su Fm,

                    'If String.IsNullOrWhiteSpace(nomeFile) Then
                    '    nomeFile = Data.FixNull(dr("NomeFileOnFSFromFM"))
                    'End If

                    Dim Ann As New Button

                    Ann.ID = "ann_" & counter1
                    Ann.Attributes.Add("runat", "server")
                    Ann.Text = "Valuta Equiparazione"
                    Ann.PostBackUrl = "valutaEquiparazione.aspx?codR=" & deEnco.QueryStringEncode(Data.FixNull(dr("IDEquiparazione"))) & "&record_ID=" & deEnco.QueryStringEncode(dr("id_record"))
                    Ann.CssClass = "btn btn-success btn-sm btn-uno btn-custom"
                    '    Ann.Attributes.Add("OnClick", "if(!myValuta())return false;")



                    Dim PianoCorso As New Button

                    PianoCorso.ID = "vediPianoCorso_" & counter1
                    PianoCorso.Attributes.Add("runat", "server")
                    PianoCorso.Text = "Scarica Diploma"
                    '  PianoCorso.PostBackUrl = "scaricaPianoCorso.aspx?codR=" & deEnco.QueryStringEncode(Data.FixNull(dr("IDCorso"))) & "&record_ID=" & deEnco.QueryStringEncode(dr("id_record")) & "&nomeFilePC=" & deEnco.QueryStringEncode(dr("NomeFileOnFS"))
                    PianoCorso.PostBackUrl = "scaricaDiplomaEqui.aspx?codR=" & deEnco.QueryStringEncode(Data.FixNull(dr("IDEquiparazione"))) & "&record_ID=" & deEnco.QueryStringEncode(dr("id_record")) & "&nomeFilePC=" & deEnco.QueryStringEncode(nomeFile)

                    PianoCorso.CssClass = "btn btn-success btn-sm btn-due btn-custom"
                    ' PianoCorso.Attributes.Add("OnClick", "if(!myValuta())return false;")


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
                    phDash.Controls.Add(New LiteralControl("Data Scadenza: <small>" & Data.SonoDieci(Data.FixNull(dr("Equi_DataScadenza"))) & "</small><br />"))

                    phDash.Controls.Add(New LiteralControl())

                    ' phDash.Controls.Add(New LiteralControl("</span>"))

                    phDash.Controls.Add(New LiteralControl("</div>"))


                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4  text-left"">"))

                    phDash.Controls.Add(New LiteralControl("</span><small>Status: </small><small " & Utility.statusColorTextCorsi(Data.FixNull(dr("Codice_Status"))) & ">" & Data.FixNull(dr("Descrizione_StatusWeb")) & "</small>"))

                    phDash.Controls.Add(New LiteralControl("</div>"))


                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-right"">"))

                    phDash.Controls.Add(PianoCorso)
                    phDash.Controls.Add(Ann)







                    phDash.Controls.Add(New LiteralControl("</div>"))





                    phDash.Controls.Add(New LiteralControl("</div>"))




                    phDash.Controls.Add(New LiteralControl("<hr>"))


                    phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))


                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left"">"))

                    phDash.Controls.Add(New LiteralControl("Qualifica DT:  "))

                    phDash.Controls.Add(New LiteralControl("<small>" & Data.FixNull(dr("Dicitura_Qualifica_DT")) & "</small>"))
                    phDash.Controls.Add(New LiteralControl())

                    phDash.Controls.Add(New LiteralControl("</span><br />"))


                    phDash.Controls.Add(New LiteralControl("Sport: <small>" & Data.FixNull(dr("Equi_Sport_Interessato")) & "</small><br />"))

                    phDash.Controls.Add(New LiteralControl("Disciplina: <small>" & Data.FixNull(dr("Equi_Disciplina_Interessata")) & "</small><br />"))
                    phDash.Controls.Add(New LiteralControl("Specialità: <small>" & Data.FixNull(dr("Equi_Specialita")) & "</small><br />"))
                    phDash.Controls.Add(New LiteralControl("Livello: <small>" & Data.FixNull(dr("Equi_Livello")) & "</small><br />"))
                    phDash.Controls.Add(New LiteralControl("Qualifica da Rilasciare: <small>" & Data.FixNull(dr("Equi_Qualifica_Tecnica_Da_Rilasciare")) & "</small><br />"))
                    phDash.Controls.Add(New LiteralControl())

                    ' phDash.Controls.Add(New LiteralControl("</span>"))

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