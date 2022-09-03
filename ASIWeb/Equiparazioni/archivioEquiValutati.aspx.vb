Imports fmDotNet
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed
Public Class archivioEquiValutati
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


            'If Not String.IsNullOrEmpty(ris) Then
            '    If Session("visto") = "ok" Then

            '        ris = deEnco.QueryStringDecode(ris)

            '        If ris = "ok" Then
            '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Richiesta Corso caricata nel sistema! ' ).set('resizable', true).resizeTo('20%', 200);", True)

            '        ElseIf ris = "ko" Then
            '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Richiesta Corso non caricata nel sistema ' ).set('resizable', true).resizeTo('20%', 200);", True)

            '        End If
            '        Session("visto") = Nothing
            '    End If
            'End If


            'If Session("stoCorsi") = "ok" Then
            '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "alertify.alert('ASI', 'Ora non è più possibile aggiungere foto! ' ).set('resizable', true).resizeTo('20%', 200);", True)
            '    Session("stoCorsi") = Nothing
            'End If



        End If
        If Not Page.IsPostBack Then


            Equiparazioni()





        End If
    End Sub

    Sub Equiparazioni()

        Dim ds As DataSet

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("webCorsiRichiesta")
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

                If Data.FixNull(dr("Codice_Status")) = "106" Or Data.FixNull(dr("Codice_Status")) = "107" _
                  Then


                    counter1 += 1




                    Dim deEnco As New Ed()

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

                    phDash.Controls.Add(New LiteralControl("</span><small>Status: </small><small " & Utility.statusColorTextCorsi(Data.FixNull(dr("Codice_Status"))) & ">" & Data.FixNull(dr("Descrizione_Status")) & "</small>"))

                    phDash.Controls.Add(New LiteralControl("</div>"))


                    phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-right"">"))








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