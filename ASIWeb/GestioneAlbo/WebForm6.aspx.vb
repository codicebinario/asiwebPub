Imports System.IO
Imports fmDotNet
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed
Imports System.Globalization
Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Spreadsheet

Public Class WebForm6
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim ds As DataSet
        Dim path As String = ""
        path = "D:\\Soft\\Lisa\\ASIWeb\\ASIWeb\\file_storage_Excel"

        Dim filePath As String = Server.MapPath(ResolveUrl("~//file_storage//"))

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("webAlbo")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        ' RequestP.AddSearchField("pre_stato_web", "1")
        RequestP.AddSearchField("CodiceEnteAffiliante", "93", Enumerations.SearchOption.equals)
        RequestP.AddSearchField("valido", "s", Enumerations.SearchOption.equals)
        RequestP.AddSortField("Cognome", Enumerations.Sort.Ascend)

        ds = RequestP.Execute()
        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then


            SaveDataSetAsExcel(ds, filePath)

        End If


    End Sub

    Public Shared Sub SaveDataSetAsExcel(ByVal dataset As DataSet, ByVal excelFilePath As String)

        Using document As SpreadsheetDocument = SpreadsheetDocument.Create(excelFilePath, SpreadsheetDocumentType.Workbook)
            Dim workbookPart As WorkbookPart = document.AddWorkbookPart()
            workbookPart.Workbook = New Workbook()
            Dim sheets As Sheets = workbookPart.Workbook.AppendChild(New Sheets())

            For Each table As DataTable In dataset.Tables

                Dim sheetCount As Integer = 0
                sheetCount += 1
                Dim worksheetPart As WorksheetPart = workbookPart.AddNewPart(Of WorksheetPart)()
                Dim sheetData = New SheetData()
                worksheetPart.Worksheet = New Worksheet(sheetData)
                Dim sheet As Sheet = New Sheet() With {
                    .Id = workbookPart.GetIdOfPart(worksheetPart),
                    .SheetId = sheetCount,
                    .Name = table.TableName
                }
                sheets.AppendChild(sheet)
                Dim headerRow As Row = New Row()
                Dim columns As List(Of String) = New List(Of String)()

                For Each column As System.Data.DataColumn In table.Columns
                    columns.Add(column.ColumnName)
                    Dim cell As Cell = New Cell()
                    cell.DataType = CellValues.String
                    cell.CellValue = New CellValue(column.ColumnName)
                    headerRow.AppendChild(cell)
                Next

                sheetData.AppendChild(headerRow)

                For Each dsrow As DataRow In table.Rows
                    Dim newRow As Row = New Row()

                    For Each col As String In columns
                        Dim cell As Cell = New Cell()
                        cell.DataType = CellValues.String
                        cell.CellValue = New CellValue(dsrow(col).ToString())
                        newRow.AppendChild(cell)
                    Next

                    sheetData.AppendChild(newRow)
                Next


            Next

            workbookPart.Workbook.Save()
        End Using



    End Sub
End Class