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
Imports Font = DocumentFormat.OpenXml.Spreadsheet.Font
Imports FontSize = DocumentFormat.OpenXml.Spreadsheet.FontSize
Imports Color = DocumentFormat.OpenXml.Spreadsheet.Color
Imports System.ServiceModel.Security

Public Class scaricaTessereS
    Inherits System.Web.UI.Page
    Dim IDEnte As String
    Dim deEnco As New Ed
    Dim tipo As String
    Dim tokenz As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If

        IDEnte = Request.QueryString("IDEnte")

        If Not String.IsNullOrEmpty(IDEnte) Then
            IDEnte = deEnco.QueryStringDecode(Request.QueryString("IDEnte"))


            '   Response.Write(IDEnte)


            Dim ds As DataSet
            Dim path As String = ""
            '  path = "D:\\Soft\\Lisa\\ASIWeb\\ASIWeb\\file_storage_Excel\pippo.xlsx"

            Dim filePath As String = Server.MapPath(ResolveUrl("~/file_storage_Excel/"))
            Dim filePathV As String = Server.MapPath("~/file_storage_Excel/")
            tokenz = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()))

            Dim File As String = IDEnte & "_TessereScadute_" & tokenz & ".xlsx"
            Dim dove As String = filePath & File

            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            fmsP.SetLayout("webAlboEx")
            Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            ' RequestP.AddSearchField("pre_stato_web", "1")
            RequestP.AddSearchField("CodiceEnteAffiliante", IDEnte, Enumerations.SearchOption.equals)

            RequestP.AddSearchField("valido", "n", Enumerations.SearchOption.equals)


            RequestP.AddSortField("DATA di RILASCIO", Enumerations.Sort.Ascend)

            ds = RequestP.Execute()
            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                ds.Tables(0).Columns.Remove("modID")
                ds.Tables(0).Columns.Remove("IDRecord")
                ds.Tables(0).Columns.Remove("CodiceEnteAffiliante")
                ds.Tables(0).Columns.Remove("valido")
                ds.Tables(0).Columns.Remove("Tessera")
                ds.Tables(0).Columns.Remove("TesseraNomeFile")
                Dim dsNuovo As DataSet = New DataSet("Tessere")
                dsNuovo.Tables.Add(ds.Tables(0).Copy)

                dsNuovo.Tables(0).Columns(0).ColumnName = "ID_Records"
                dsNuovo.Tables(0).Columns(1).ColumnName = "Codice_Fiscale"
                dsNuovo.Tables(0).Columns(2).ColumnName = "Cognome"
                dsNuovo.Tables(0).Columns(3).ColumnName = "Nome"
                dsNuovo.Tables(0).Columns(4).ColumnName = "Email"
                dsNuovo.Tables(0).Columns(5).ColumnName = "Indirizzo_Residenza"
                dsNuovo.Tables(0).Columns(6).ColumnName = "Data_Nascita"
                dsNuovo.Tables(0).Columns(7).ColumnName = "Comune_Residenza"
                dsNuovo.Tables(0).Columns(8).ColumnName = "Comune_Nascita"
                dsNuovo.Tables(0).Columns(9).ColumnName = "Cap_Residenza"
                dsNuovo.Tables(0).Columns(10).ColumnName = "Livello_grado"
                dsNuovo.Tables(0).Columns(11).ColumnName = "Tessera_ASI"
                dsNuovo.Tables(0).Columns(12).ColumnName = "Qualifica"
                dsNuovo.Tables(0).Columns(13).ColumnName = "Scadenza"
                dsNuovo.Tables(0).Columns(14).ColumnName = "Specialita"
                dsNuovo.Tables(0).Columns(15).ColumnName = "Sport"
                dsNuovo.Tables(0).Columns(16).ColumnName = "Telefono"
                dsNuovo.Tables(0).Columns(17).ColumnName = "Provincia_Residenza"
                dsNuovo.Tables(0).Columns(18).ColumnName = "Codice_Iscrizione"
                dsNuovo.Tables(0).Columns(19).ColumnName = "Disciplina"
                dsNuovo.Tables(0).Columns(20).ColumnName = "Dicitura_Qualifica_DT"
                dsNuovo.Tables(0).Columns(21).ColumnName = "Data_Rilascio"

                Dim dt As DataTable = dsNuovo.Tables.Item(0)

                dt.Columns("ID_Records").SetOrdinal(0)
                dt.Columns("Codice_Fiscale").SetOrdinal(1)
                dt.Columns("Cognome").SetOrdinal(2)
                dt.Columns("Nome").SetOrdinal(3)
                dt.Columns("Email").SetOrdinal(4)
                dt.Columns("Data_Nascita").SetOrdinal(5)
                dt.Columns("Comune_Nascita").SetOrdinal(6)
                dt.Columns("Indirizzo_Residenza").SetOrdinal(7)
                dt.Columns("Comune_Residenza").SetOrdinal(8)
                dt.Columns("Provincia_Residenza").SetOrdinal(9)
                dt.Columns("Cap_Residenza").SetOrdinal(10)
                dt.Columns("Telefono").SetOrdinal(11)
                dt.Columns("Tessera_ASI").SetOrdinal(12)
                dt.Columns("Data_Rilascio").SetOrdinal(13)
                dt.Columns("Codice_Iscrizione").SetOrdinal(14)
                dt.Columns("Scadenza").SetOrdinal(15)
                dt.Columns("Livello_grado").SetOrdinal(16)
                dt.Columns("Sport").SetOrdinal(17)
                dt.Columns("Disciplina").SetOrdinal(18)
                dt.Columns("Specialita").SetOrdinal(19)
                dt.Columns("Qualifica").SetOrdinal(20)
                dt.Columns("Dicitura_Qualifica_DT").SetOrdinal(21)



                SaveDataSetAsExcel(dsNuovo, dove, File)
                '   System.Threading.Thread.Sleep(4000)

                FotoS(dove, File)

            End If




        End If

    End Sub
    Private Sub ProcessDataTable(ByVal dt As DataTable)
        dt.Columns("ID_Records").SetOrdinal(0)
        dt.Columns("Codice_Fiscale").SetOrdinal(1)
        dt.Columns("Cognome").SetOrdinal(2)
        dt.Columns("Nome").SetOrdinal(2)
    End Sub
    Private Sub CreateDataset()
        Dim table1 As DataTable = New DataTable("Dati")
        table1.Columns.Add("ID_Record")
        table1.Columns.Add("Codice_Fiscale")

        ' Create DataSet and add datatable
        Dim ds As DataSet = New DataSet("Tessere")
        ds.Tables.Add(table1)
    End Sub

    Public Shared Sub SaveDataSetAsExcel(ByVal dataset As DataSet, ByVal excelFilePath As String, file As String)

        Using document As SpreadsheetDocument = SpreadsheetDocument.Create(excelFilePath, SpreadsheetDocumentType.Workbook)


            Dim workbookPart As WorkbookPart = document.AddWorkbookPart()
            workbookPart.Workbook = New Workbook()

            AddStyleSheet(document)


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
                     .Name = "Albo_TessereScadute"
                            }
                sheets.AppendChild(sheet)
                Dim headerRow As Row = New Row()
                Dim columns As List(Of String) = New List(Of String)()

                For Each column As System.Data.DataColumn In table.Columns
                    columns.Add(column.ColumnName)
                    Dim cell As Cell = New Cell()
                    cell.DataType = CellValues.String
                    cell.CellValue = New CellValue(column.ColumnName)
                    cell.StyleIndex = Convert.ToUInt32(1)


                    headerRow.AppendChild(cell)
                Next

                sheetData.AppendChild(headerRow)

                For Each dsrow As DataRow In table.Rows
                    Dim newRow As Row = New Row()
                    For Each col As String In columns
                        Dim cell As Cell = New Cell()
                        cell.DataType = CellValues.String
                        If col = "Data_Rilascio" Or col = "Data_Nascita" Or col = "Scadenza" Then
                            cell.CellValue = New CellValue(Left(dsrow(col).ToString(), 10))
                        Else
                            cell.CellValue = New CellValue(dsrow(col).ToString())
                        End If
                        newRow.AppendChild(cell)
                    Next

                    sheetData.AppendChild(newRow)
                Next


            Next
            document.PackageProperties.Creator = "ASI Nazionale"
            document.PackageProperties.Created = DateTime.UtcNow
            workbookPart.Workbook.Save()





        End Using





    End Sub
    Private Shared Function AddStyleSheet(ByVal spreadsheet As SpreadsheetDocument) As WorkbookStylesPart
        Dim stylesheet As WorkbookStylesPart = spreadsheet.WorkbookPart.AddNewPart(Of WorkbookStylesPart)()
        Dim workbookstylesheet As Stylesheet = New Stylesheet()
        Dim font0 As Font = New Font()
        Dim font1 As Font = New Font()
        Dim bold As Bold = New Bold()
        font1.Append(bold)
        Dim fonts As Fonts = New Fonts()
        fonts.Append(font0)
        fonts.Append(font1)
        Dim fill0 As Fill = New Fill()
        Dim fills As Fills = New Fills()
        fills.Append(fill0)
        Dim border0 As Border = New Border()
        Dim borders As Borders = New Borders()
        borders.Append(border0)
        Dim cellformat0 As CellFormat = New CellFormat() With {
        .FontId = 0,
        .FillId = 0,
        .BorderId = 0
    }
        Dim cellformat1 As CellFormat = New CellFormat() With {
        .FontId = 1
    }
        Dim cellformats As CellFormats = New CellFormats()
        cellformats.Append(cellformat0)
        cellformats.Append(cellformat1)
        workbookstylesheet.Append(fonts)
        workbookstylesheet.Append(fills)
        workbookstylesheet.Append(borders)
        workbookstylesheet.Append(cellformats)
        stylesheet.Stylesheet = workbookstylesheet
        stylesheet.Stylesheet.Save()
        Return stylesheet
    End Function
    'Private Shared Function CreateStylesheet() As Stylesheet
    '    Dim ss As Stylesheet = New Stylesheet()
    '    Dim fts As Fonts = New Fonts()
    '    Dim ft As DocumentFormat.OpenXml.Spreadsheet.Font = New DocumentFormat.OpenXml.Spreadsheet.Font()
    '    Dim fbld As Bold = New Bold()
    '    Dim ftn As FontName = New FontName()
    '    ftn.Val = "Calibri"
    '    Dim ftsz As DocumentFormat.OpenXml.Spreadsheet.FontSize = New DocumentFormat.OpenXml.Spreadsheet.FontSize()
    '    ftsz.Val = 11
    '    ft.FontName = ftn
    '    ft.FontSize = ftsz
    '    ft.Bold = fbld
    '    fts.Append(ft)
    '    fts.Count = CUInt(fts.ChildElements.Count)
    '    ss.Append(fts)
    '    Return ss
    'End Function



    Public Function FotoS(urlFoto As String, nomefile As String)

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
            Response.AddHeader("content-disposition", "attachment;filename=" & nomefile)
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