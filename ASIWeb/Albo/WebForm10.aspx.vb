Imports fmDotNet
Imports Newtonsoft.Json

Public Class WebForm10
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        Dim ds As DataSet

        fmsP.SetLayout("webFormatori")

        Dim dst As New DataSet
        Dim dt As DataTable
        Dim drt As DataRow
        Dim idCoulumn As DataColumn
        Dim nameCoulumn As DataColumn
        Dim valueCoulumn As DataColumn
        Dim i As Integer
        dt = New DataTable()
        idCoulumn = New DataColumn("ID", Type.GetType("System.Int32"))
        nameCoulumn = New DataColumn("Tipo", Type.GetType("System.String"))
        valueCoulumn = New DataColumn("QuantiAttivi", Type.GetType("System.Int32"))
        dt.Columns.Add(idCoulumn)
        dt.Columns.Add(nameCoulumn)


        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        RequestP.AddSearchField("Prime4Lettere", "a", Enumerations.SearchOption.beginsWith)
        RequestP.AddSortField("NominativoControllo")
        ds = RequestP.Execute()
        Dim miods As DataSet
        Dim customers As List(Of String) = New List(Of String)()
        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
            i = 1
            For Each dr In ds.Tables("main").Rows

                Response.Write("Nominativo: " & dr("NominativoControllo") & " - Cognome: " & dr("Cognome") & " - Proper: " & dr("testProper") & "<br />")

                drt = dt.NewRow()
                drt("ID") = i
                drt("Tipo") = dr("Cognome")

                i = i + 1
                dt.Rows.Add(drt)
                'miods.Tables(0).Columns.Add("CognomeProper")
            Next

            dst.Tables.Add(dt)
        End If

        Dim json As String = JsonConvert.SerializeObject(dst, Formatting.Indented)



        Response.Write(json)




    End Sub

End Class