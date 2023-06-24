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

Public Class WebForm2
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
    Dim stileavviso As String = "width: 100%; margin-top: 4px; padding: 16px; border-radius: 5px; background-color:   #f8d7da; color: #b71c1c"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub lnkButton1_Click(sender As Object, e As EventArgs) Handles lnkButton1.Click



        Dim fileUpload As FileUpload = FileUpload1
        If FileUpload1.HasFile Then
            Dim fileName As String = Path.GetFileName(FileUpload1.FileName)
            Dim filePath As String = Server.MapPath("~/provazip/") & fileName
            FileUpload1.SaveAs(filePath)
            ' Process the uploaded file as needed
            ' Create a directory to extract the files
            Dim percorso As String = Server.MapPath("~/provazip/" & "nuovacartella")
            Directory.CreateDirectory(percorso)

            Using zip As ZipFile = ZipFile.Read(filePath)
                zip.ExtractAll(percorso, ExtractExistingFileAction.OverwriteSilently)
            End Using

            Dim files As List(Of String) = GetFileNames(percorso)

            For Each item As String In files

                Dim fileExtension As String = Path.GetExtension(item)

                ' Check if the file has a .jpg extension
                If String.Compare(fileExtension, ".jpg", True) = 0 Then
                    ' Remove the file extension
                    Dim fileNameWithoutExtension As String = Path.GetFileNameWithoutExtension(item)
                    aggiungiFM(1, fileNameWithoutExtension, fileNameWithoutExtension)

                    Response.Write(fileNameWithoutExtension & "<br />")
                    ' Perform any operation with the file name without the extension
                    '   Console.WriteLine(fileNameWithoutExtension)
                End If

                ' Or perform any other operation with the file name
            Next


            ' File.Delete(filePath)
            ' Directory.Delete(percorso, True)

        End If


        ' Delete the zip file
        ' File.Delete(zipFilePath)
    End Sub

    Sub aggiungiFM(gruppo As String, id As String, nomefile As String)
        Dim fms As FMSAxml = Nothing
        Dim fms2 As FMSAxml = Nothing
        Dim ds As DataSet = Nothing
        Dim ritorno As Boolean = False
        Dim idGruppo As Integer = 0

        fms = ASIWeb.AsiModel.Conn.Connect()
        fms2 = ASIWeb.AsiModel.Conn.Connect()
        fms.SetLayout("TestAPI2")
        fms2.SetLayout("TestAPI2")
        Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
        RequestA.AddSearchField("gruppo", gruppo, Enumerations.SearchOption.equals)
        RequestA.AddSearchField("Id", id, Enumerations.SearchOption.equals)
        ds = RequestA.Execute()

        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                For Each dr In ds.Tables("main").Rows


                Dim Request1 = fms2.CreateEditRequest(dr("id"))


                Request1.AddField("nomeFileImg", nomefile)


                Request1.Execute()




                Next



            End If

        '   Catch ex As Exception

        '  End Try


    End Sub

    Protected Function GetFileNames(ByVal percorso As String) As List(Of String)
        Dim fileNames As List(Of String) = New List(Of String)()

        Dim files As String() = Directory.GetFiles(percorso)

        For Each file As String In files
            Dim fileName As String = Path.GetFileName(file)
            fileNames.Add(fileName)
        Next

        Return fileNames
    End Function
    Public Function PrendiToken() As String
        Dim urlsign As String = "https://93.63.195.98/fmi/data/vLatest/databases/Asi/sessions"
        Dim auth As String = ""
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        Using client1 = New WebClient()
            client1.Encoding = System.Text.Encoding.UTF8

            client1.Headers.Add(HttpRequestHeader.ContentType, "application/json")
            client1.UseDefaultCredentials = True
            client1.Headers.Add("Authorization", "Basic " +
           Convert.ToBase64String(Encoding.UTF8.GetBytes("enteweb:web01")))

            '   Try
            Dim reply1 As String = client1.UploadString(New Uri(urlsign), "POST", "")
            Dim risposta1 = JsonConvert.DeserializeObject(reply1)

            Dim bearer = JObject.Parse(risposta1.ToString)
            Dim token = bearer.SelectToken("response.token").ToString()
            auth = "Bearer " + token
            auth = token
            ' Response.Write(auth)
            '   Catch ex As Exception

            '   Response.Redirect("errore.aspx?esito=errore&tipo=" & " token non ricevuto")

            ' End Try
            '    Response.Write(Server.MapPath("\img\2.jpg"))
        End Using
        Return auth

    End Function

    Public Function CaricaSuFM() As Boolean

        Dim host As String = HttpContext.Current.Request.Url.Host.ToLower()
        Dim token = PrendiToken()
        Dim filePath As String = Server.MapPath(ResolveUrl("~/file_storage_RinnoviPag/"))

        '  Dim File As HttpPostedFile = inputfile.PostedFile
        Dim File As String = nomecaricato


        Dim dove = Path.Combine(filePath, File)
        ' File.SaveAs(dove)



        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12


        '    Try


        Dim clientUP As New RestClient("https://93.63.195.98/fmi/data/vLatest/databases/Asi/layouts/testAPI/records/1/containers/fileCont/1")
        clientUP.Timeout = -1
        Dim Requestx = New RestRequest(Method.POST)
        '   Requestx.AddHeader("Content-Type", "application/json")
        ' Requestx.AddHeader("Content-Type", "multipart/form-data")
        Requestx.AddHeader("Authorization", "bearer " & token.ToString())
        Requestx.AlwaysMultipartFormData = True
        Dim fileUpload As FileUpload = FileUpload1
        If fileUpload.HasFile Then
            Dim fileName As String = Path.GetFileName(fileUpload.FileName)
            Dim fileBytes As Byte() = fileUpload.FileBytes
            Requestx.AddFile("upload", fileBytes, fileName)
        End If
        Requestx.AddParameter("modId", "1")

        '  Requestx.AddParameter("upload", Server.MapPath(ResolveUrl("~/file_storage_RinnoviPag/")) & nomecaricato & "")
        '  Requestx.AddParameter("upload", "D:\Dropbox\soft\Projects\ASIWeb\ASIWeb\file_storage" & nomecaricato & "")
        '   Requestx.AddParameter("upload", "C:\file_storage\" & nomecaricato & "")
        '    Requestx.AddFile("upload", dove)


        Dim ResponseUP As IRestResponse = clientUP.Execute(Requestx)





        Return True

        ' Catch ex As Exception

        '  End Try


    End Function

End Class