Imports fmDotNet
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Net
Public Class WebForm15
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ds As DataSet

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("webRinnoviRichiesta")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.AllRecords)
        ' RequestP.AddSearchField("pre_stato_web", "1")
        ' RequestP.AddSearchField("Codice_Ente_Richiedente", Session("codice"), Enumerations.SearchOption.equals)
        RequestP.AddSortField("Codice_Status", Enumerations.Sort.Ascend)
        RequestP.AddSortField("IDRinnovo", Enumerations.Sort.Descend)
        ds = RequestP.Execute()

        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
            Repeater1.DataSource = ds.Tables("main")
            Repeater1.DataBind()

        End If
    End Sub

End Class