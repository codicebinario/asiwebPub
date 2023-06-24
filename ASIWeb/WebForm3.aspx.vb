Imports System.IO
Imports OboutInc.FileUpload
Imports fmDotNet
Imports System.Net.Mail
Imports System.Web.UI.WebControls
Imports ASIWeb.AsiModel
Imports System.Security.Cryptography

Imports System.Threading.Tasks
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Imports System.Web.Script.Serialization
Imports RestSharp

Imports System.Collections.Generic
Imports System.Net.Security
Imports System.Net
Imports ASIWeb.Ed

Imports Ionic.Zip
Imports OboutInc

Public Class WebForm3
    Inherits System.Web.UI.Page
    Const MassimoPeso As Integer = 3102400
    Const FileType As String = "image/*"
    ' in pixel
    Const massimaaltezza As Integer = 140
    Const massinalarghezza As Integer = 100

    Dim ext As String = " "
    Dim nomefileReale As String = " "
    Dim qualeStatus As String = ""
    Dim nomecaricato As String = ""
    Dim tokenZ As String = ""
    Dim deEnco As New Ed()
    Dim webserver As String = ConfigurationManager.AppSettings("webserver")
    Dim utente As String = ConfigurationManager.AppSettings("utente")
    Dim porta As String = ConfigurationManager.AppSettings("porta")
    Dim pass As String = ConfigurationManager.AppSettings("pass")
    Dim dbb As String = ConfigurationManager.AppSettings("dbb")
    Dim codR As String = ""
    Dim cultureFormat As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("it-IT")
    Dim CodiceEnteRichiedente As String = ""
    Dim codicefiscale As String = ""
    Dim s As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim fms As FMSAxml = Nothing
        ' Dim fms2 As FMSAxml = Nothing
        Dim ds As DataSet = Nothing
        Dim ritorno As Boolean = False
        Dim idGruppo As Integer = 0

        fms = ASIWeb.AsiModel.Conn.Connect()
        '   fms2 = ASIWeb.AsiModel.Conn.Connect()
        fms.SetLayout("TestAPI2")
        '   fms2.SetLayout("TestAPI")
        Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
        RequestA.AddSearchField("gruppo", "1", Enumerations.SearchOption.equals)
        ds = RequestA.Execute()

        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
            For Each dr In ds.Tables("main").Rows

                Response.Write("id: " & dr("id") & "<br />")
                'Dim Request1 = fms2.CreateEditRequest(dr("id"))


                'Request1.AddField("nomeFileImg", nomefile)


                'Request1.Execute()




            Next



        End If

        '   Catch ex As Exception

        '  End Try
    End Sub

End Class