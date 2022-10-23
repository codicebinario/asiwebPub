Imports fmDotNet

Public Class WebForm9
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Dim ds As DataSet

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("webRinnoviRichiesta")

        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        ' RequestP.AddSearchField("pre_stato_web", "1")
        RequestP.AddSearchField("Codice_Ente_Richiedente", "291", Enumerations.SearchOption.equals)
        RequestP.AddSearchField("Codice_Status", 160, Enumerations.SearchOption.biggerOrEqualThan)
        '   RequestP.AddSearchField("Codice_Status", "161", Enumerations.SearchOption.equals)
        RequestP.SetMax(10)
        RequestP.AddSortField("IDRinnovo", Enumerations.Sort.Descend)
        ' RequestP.AddSortField("IDRinnovo", Enumerations.Sort.Descend)
        ds = RequestP.Execute()

        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then


            Response.Write("Quanti: " & ds.Tables("main").Rows.Count)

        End If

    End Sub

End Class