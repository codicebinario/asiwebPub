Imports fmDotNet

Public Class DashBoardRinnovi2
    Inherits System.Web.UI.Page
    Dim open As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If
        If IsNothing(Session("codice")) Then
            Response.Redirect("../login.aspx")
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

        Dim ds As DataSet

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("webRinnoviMaster")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        ' RequestP.AddSearchField("pre_stato_web", "1")
        RequestP.AddSearchField("CodiceEnteRichiedente", Session("codice"), Enumerations.SearchOption.equals)
        RequestP.AddSortField("IDRinnovoM", Enumerations.Sort.Descend)
        RequestP.AddSortField("CodiceStatus", Enumerations.Sort.Ascend)

        ds = RequestP.Execute()

        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then


            phDash.Visible = True
            'Dim counter As Integer = 0
            '   Dim counter1 As Integer = 0
            'Dim totale As Decimal = 0
            phDash.Controls.Add(New LiteralControl("<div class=""accordion"" id=""accordionDash"">"))
            For Each dr In ds.Tables("main").Rows

                phDash.Controls.Add(New LiteralControl("<div class=""accordion-item"">"))

                phDash.Controls.Add(New LiteralControl("<h2 class=""accordion-header"" id=""" & heading & "_" & Data.FixNull(dr("IDRinnovoM")) & """>"))
                If Data.FixNull(dr("IDRinnovoM")) = open Then
                    phDash.Controls.Add(New LiteralControl("<button class=""accordion-button"" type=""button"" data-bs-toggle=""collapse"" data-bs-target=""#collapse" & Data.FixNull(dr("IDRinnovoM")) & """ aria-expanded=""False"" aria-controls=""collapse" & Data.FixNull(dr("IDRinnovoM")) & """>"))

                Else
                    phDash.Controls.Add(New LiteralControl("<button class=""accordion-button collapsed"" type=""button"" data-bs-toggle=""collapse"" data-bs-target=""#collapse" & Data.FixNull(dr("IDRinnovoM")) & """ aria-expanded=""False"" aria-controls=""collapse" & Data.FixNull(dr("IDRinnovoM")) & """>"))

                End If
                '   phDash.Controls.Add(New LiteralControl("<button class=""accordion-button collapsed"" type=""button"" data-bs-toggle=""collapse"" data-bs-target=""#collapse" & Data.FixNull(dr("IDRinnovoM")) & """ aria-expanded=""False"" aria-controls=""collapse" & Data.FixNull(dr("IDRinnovoM")) & """>"))
                phDash.Controls.Add(New LiteralControl("Gruppo Rinnovi: " & Data.FixNull(dr("IDRinnovoM"))))
                phDash.Controls.Add(New LiteralControl("</button>"))
                phDash.Controls.Add(New LiteralControl("</h2>"))
                If Data.FixNull(dr("IDRinnovoM")) = open Then
                    phDash.Controls.Add(New LiteralControl("<div id=""" & collapse & Data.FixNull(dr("IDRinnovoM")) & """ class=""accordion-collapse collapse show"" aria-labelledby=""heading_" & Data.FixNull(dr("IDRinnovoM")) & """ data-bs-parent=""#accordionDash"">"))

                Else
                    phDash.Controls.Add(New LiteralControl("<div id=""" & collapse & Data.FixNull(dr("IDRinnovoM")) & """ class=""accordion-collapse collapse"" aria-labelledby=""heading_" & Data.FixNull(dr("IDRinnovoM")) & """ data-bs-parent=""#accordionDash"">"))

                End If

                phDash.Controls.Add(New LiteralControl("<div class=""accordion-body"">"))
                phDash.Controls.Add(New LiteralControl("<p>"))
                phDash.Controls.Add(New LiteralControl("<a class=""text-warning text-right"" href=""addRinnovoToGroup.aspx"">aggiungi Rinnovo al Gruppo</a>"))
                phDash.Controls.Add(New LiteralControl("</p>"))
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





                        'fine prima colonna
                        phDash.Controls.Add(New LiteralControl("</div>"))

                        'inizio seconda colonna
                        phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4  text-left"">"))

                        phDash.Controls.Add(New LiteralControl("ciao"))

                        'fine seconda colonna
                        phDash.Controls.Add(New LiteralControl("</div>"))


                        'inizio terza colonna
                        phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4  text-left"">"))

                        phDash.Controls.Add(New LiteralControl("ciao"))

                        'fine terza colonna
                        phDash.Controls.Add(New LiteralControl("</div>"))


                        ' fine row
                        phDash.Controls.Add(New LiteralControl("</div>"))





                        phDash.Controls.Add(New LiteralControl("</div>"))

                        phDash.Controls.Add(New LiteralControl("</div>"))

                        phDash.Controls.Add(New LiteralControl("</div>"))
                        phDash.Controls.Add(New LiteralControl("</div>"))



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
End Class