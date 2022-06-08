Imports System.IO
Imports System.Net
Imports System.Text
Imports fmDotNet
Imports System.Net.Mail
Imports System.Web.UI.WebControls

Imports System.Security.Cryptography

Imports System.Threading.Tasks
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Imports System.Web.Script.Serialization
Imports RestSharp

Imports System.Collections.Generic


'Imports FMdotNet__DataAPI

Imports System.Linq
Imports System.Configuration
Imports System.Drawing
Imports Image = System.Drawing.Image
Imports System.Net.Security



Public Class Uploader
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim codR As String = Request.QueryString("hcodR").ToString()
        Dim token As String = ""
        Dim ext As String = " "
        Dim nomefileReale As String = " "

        If QuantiAllegati(codR) = "no" Then
            Context.Response.StatusCode = 200
            Context.Response.ContentType = "text/plain"

            Context.Response.Write("file non caricato: massimo numero di allegati raggiunto")
        Else






            Dim nomecaricato As String = ""
            For Each key As String In Context.Request.Files
                Dim postedFile As HttpPostedFile = Context.Request.Files(key)
                Dim folderPath As String = Context.Server.MapPath("~/file_storage/")
                If Not Directory.Exists(folderPath) Then
                    Directory.CreateDirectory(folderPath)
                End If

                token = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()))
                ext = Path.GetExtension((postedFile.FileName))
                nomecaricato = codR & "_" + token + ext

                postedFile.SaveAs(folderPath + nomecaricato)
                '  postedFile.SaveAs(folderPath + postedFile.FileName)

                ' context.Response.Redirect("~/up.aspx")

            Next

            Dim tokenx As String = ""
            Dim id_att As String = ""
            Dim tuttoRitorno As String = ""

            tuttoRitorno = NuovaRichiestaAllegato(codR, nomecaricato)

            Dim arrKeywords As String() = Split(tuttoRitorno, "_|_")
            tokenx = arrKeywords(1)
            id_att = arrKeywords(0)
            CaricaSuFM(tokenx, id_att, nomecaricato)






            Context.Response.StatusCode = 200
            Context.Response.ContentType = "text/plain"

            Context.Response.Write("file caricato: " & nomecaricato)

        End If

    End Sub

    Public Function QuantiAllegati(codR As String) As String
        Dim fmsP As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
        Dim ds As DataSet
        Dim quanti As Integer = 0
        Dim risposta As String = ""

        fmsP.SetLayout("web_richiesta_allegati")

        Dim request = fmsP.CreateFindRequest()

        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        RequestP.AddSearchField("Codice_Richiesta", "==" & codR)

        ds = RequestP.Execute()

        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
            quanti = ds.Tables("main").Rows.Count

            If quanti >= 3 Then


                risposta = "no"





            End If





        End If
        Return risposta
    End Function

    Public Function NuovaRichiestaAllegato(codR As String, nomecaricato As String) As String
        '  Dim litNumRichieste As Literal = DirectCast(ContentPlaceHolder1.FindControl("LitNumeroRichiesta"), Literal)

        Dim fmsP As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
        ' Dim ds As DataSet


        fmsP.SetLayout("web_richiesta_allegati")
        Dim Request = fmsP.CreateNewRecordRequest()

        Request.AddField("Codice_Richiesta", codR)
        Request.AddField("NomeFileOnFS", nomecaricato)

        'Request.AddField("Codice_Articolo", AsiModel.CodiceArticolo)
        'Request.AddField("Nome_Articolo", AsiModel.NomeArticolo)
        'Request.AddField("Quantita", txtQuantita.Text)


        Dim IdAllegato As String = Request.Execute() 'per upload

        Dim record_id As String = ASIWeb.AsiModel.GetRecord_IDbyCodR.GetRecord_ID(codR) ' per aggiornare status

        Dim fmsP1 As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
        '  Dim ds As DataSet
        Dim risposta As String = ""
        fmsP1.SetLayout("web_richiesta_master")
        Dim Request1 = fmsP1.CreateEditRequest(record_id)


        Request1.AddField("Status_ID", "6")

        risposta = Request1.Execute()
        Dim token = PrendiToken()



        Return IdAllegato & "_|_" & token
    End Function

    Public Function CaricaSuFM(tokenx As String, id_att As String, nomecaricato As String) As Boolean

        Dim host As String = HttpContext.Current.Request.Url.Host.ToLower()

        Dim filePath As String = Server.MapPath(ResolveUrl("~/file_storage/"))

        '  Dim File As HttpPostedFile = inputfile.PostedFile
        Dim File As String = nomecaricato


        Dim dove = Path.Combine(filePath, File)
        ' File.SaveAs(dove)



        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12



        Dim clientUP As New RestClient("https://93.63.195.98/fmi/data/vLatest/databases/Asi/layouts/web_richiesta_allegati/records/" & id_att & "/containers/Ricevuta_Pagamento_ext/1")
        clientUP.Timeout = -1
        Dim Requestx = New RestRequest(Method.POST)
        Requestx.AddHeader("Content-Type", "application/json")
        Requestx.AddHeader("Content-Type", "multipart/form-data")
        Requestx.AddHeader("Authorization", "bearer " & tokenx.ToString())
        Requestx.AddParameter("modId", "1")
        'If host = "localhost" Then
        '    Requestx.AddParameter("upload", "D:\Soft\c#\webUP\webUP\file_storage\" & nomecaricato & "")
        'Else
        '    Requestx.AddParameter("upload", "D:\Soft\c#\webUP\webUP\file_storage\" & nomecaricato & "")
        'End If
        Requestx.AddParameter("upload", Server.MapPath(ResolveUrl("~/file_storage/")) & nomecaricato & "")
        Requestx.AddFile("upload", dove)


        Dim ResponseUP As IRestResponse = clientUP.Execute(Requestx)

        Return True


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






End Class