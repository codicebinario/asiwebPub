Imports fmDotNet
Imports System.Web.Services
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed
Imports System.Drawing
Imports System.Drawing.Imaging

Imports System.IO
Imports OboutInc.FileUpload

Imports System.Net.Mail
Imports System.Web.UI.WebControls

Imports System.Security.Cryptography

Imports System.Threading.Tasks
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Imports System.Web.Script.Serialization
Imports RestSharp

Imports System.Collections.Generic
Imports System.Net.Security
Imports System.Net
Imports Image = System.Drawing.Image
Imports RestSharp.Authenticators
Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Spreadsheet
Imports DocumentFormat.OpenXml.ExtendedProperties
Imports DocumentFormat.OpenXml.Math

Public Class WebForm7
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim ds As DataSet
        Dim path As String = ""
        path = "D:\\Soft\\Lisa\\ASIWeb\\ASIWeb\\file_storage_Excel\pippo.xlsx"

        Dim filePath As String = Server.MapPath(ResolveUrl("~/file_storage_Excel/"))
        Dim filePathV As String = Server.MapPath("~/file_storage_Excel/")
        Dim File As String = "pippo.xlsx"
        Dim dove As String = filePath & File

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("webAlboEx")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        ' RequestP.AddSearchField("pre_stato_web", "1")
        RequestP.AddSearchField("CodiceEnteAffiliante", "93", Enumerations.SearchOption.equals)
        RequestP.AddSearchField("valido", "s", Enumerations.SearchOption.equals)
        RequestP.AddSortField("Cognome", Enumerations.Sort.Ascend)

        ds = RequestP.Execute()
        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then


            SaveDataSetAsExcel(ds, dove, File)
            System.Threading.Thread.Sleep(4000)

            FotoS(dove)

        End If
    End Sub
    'Public Shared Function GetFileD(ByVal uri As String) As String
    '    Dim results As String = "N/A"

    '    '  Try
    '    Dim req As HttpWebRequest = CType(WebRequest.Create(uri), HttpWebRequest)
    '        Dim resp As HttpWebResponse = CType(req.GetResponse(), HttpWebResponse)
    '        Dim sr As StreamReader = New StreamReader(resp.GetResponseStream())
    '        results = sr.ReadToEnd()
    '        sr.Close()
    '    '  Catch ex As Exception
    '    ' results = ex.Message
    '    ' End Try

    '    Return results
    'End Function

    Public Shared Sub SaveDataSetAsExcel(ByVal dataset As DataSet, ByVal excelFilePath As String, file As String)

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
                     .Name = "Albo"
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

    Public Function FotoS(urlFoto As String)

        Dim pictureURL As String = urlFoto
        Dim nominativo As String = ""

        '    nominativo = Corso.PrendiNominativo(record_ID)

        Dim wClient As WebClient = New WebClient()
        Dim nc As NetworkCredential = New NetworkCredential("enteweb", "web01")
        wClient.Credentials = nc
        Dim response1 As Stream = wClient.OpenRead(pictureURL)
        Dim temp As Stream = response1
        Using stream As MemoryStream = New MemoryStream()
            CopyStream(temp, stream)
            Dim bytes As Byte() = stream.ToArray()
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "application/xlsx"
            Response.AddHeader("content-disposition", "attachment;filename=pippo.xlsx")
            Response.BinaryWrite(bytes)
            Response.Flush()

            Response.End()


        End Using



        Return temp

    End Function

    Public Shared Sub CopyStream(ByVal input As Stream, ByVal output As Stream)
        Dim buffer As Byte() = New Byte(4095) {}
        While True
            Dim read As Integer = input.Read(buffer, 0, buffer.Length)
            If read <= 0 Then
                Return
            End If
            output.Write(buffer, 0, read)
        End While
    End Sub


End Class

