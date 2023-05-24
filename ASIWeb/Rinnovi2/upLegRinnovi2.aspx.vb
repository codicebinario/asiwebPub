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

Public Class upLegRinnovi2
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
        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If
        If IsNothing(Session("codice")) Then
            Response.Redirect("../login.aspx")
        End If

        cultureFormat.NumberFormat.CurrencySymbol = "€"
        cultureFormat.NumberFormat.CurrencyDecimalDigits = 2
        cultureFormat.NumberFormat.CurrencyGroupSeparator = String.Empty
        cultureFormat.NumberFormat.CurrencyDecimalSeparator = ","
        System.Threading.Thread.CurrentThread.CurrentCulture = cultureFormat
        System.Threading.Thread.CurrentThread.CurrentUICulture = cultureFormat


        Dim idEqui As String = 0

        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If
        Dim record_ID As String = ""





        codR = deEnco.QueryStringDecode(Request.QueryString("codR"))
        s = Request.QueryString("s")
        If Not String.IsNullOrEmpty(codR) Then
            Session("codR") = codR

        End If

        HiddenIDRinnovo.Value = Session("codR")



        If QuantiAllegati(codR) = "no" Then

            'lnkButton1.Enabled = False
            uploadedFiles.Text = ""


            results.InnerHtml = "<b>non è possibile caricare ulteriori documenti </b>"
            results.Attributes.Add("style", stileavviso)
        Else

            results.InnerHtml = "<b>E' possibile caricare fino a 3 documenti </b>"
            results.Attributes.Add("style", stileavviso)

        End If


    End Sub
    Protected Sub cvCaricaDiploma_ServerValidate(source As Object, args As ServerValidateEventArgs)
        If FileUpload1.PostedFile.ContentLength > 0 Then


            args.IsValid = True
        Else
            results.InnerHtml = "carica la documentazione<br>"
            results.Attributes.Add("style", stileavviso)

            args.IsValid = False
        End If
    End Sub

    Protected Sub cvTipoFile_ServerValidate(source As Object, args As ServerValidateEventArgs)

        Dim fileExtensions As String() = {".pdf", ".PDF", ".jpg", ".JPG", ".jpeg", ".JPEG", ".PNG", ".png"}
        Dim pippo As String = FileUpload1.PostedFile.ContentType
        For i As Integer = 0 To fileExtensions.Length - 1
            If FileUpload1.PostedFile.FileName.Contains(fileExtensions(i)) Then
                args.IsValid = True

                Exit For
            Else
                args.IsValid = False

                results.InnerHtml = "il file deve essere in formato pdf, jpg oppure png<br>"
                results.Attributes.Add("style", stileavviso)
            End If
        Next


    End Sub
    Public Function QuantiAllegati(codR As String) As String
        Dim fmsP As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
        Dim ds As DataSet
        Dim quanti As Integer = 0
        Dim risposta As String = ""

        fmsP.SetLayout("webRinnoviAllegati2")

        Dim request = fmsP.CreateFindRequest()

        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        RequestP.AddSearchField("Codice_Rinnovo", codR, Enumerations.SearchOption.equals)

        ds = RequestP.Execute()

        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
            quanti = ds.Tables("main").Rows.Count

            If quanti >= 3 Then


                risposta = "no"





            End If





        End If
        Return risposta
    End Function
    Public Function CaricaSuFM(tokenx As String, id_att As String, nomecaricato As String) As Boolean

        Dim host As String = HttpContext.Current.Request.Url.Host.ToLower()

        Dim filePath As String = Server.MapPath(ResolveUrl("~/file_storage_RinnoviPag/"))

        '  Dim File As HttpPostedFile = inputfile.PostedFile
        Dim File As String = nomecaricato


        Dim dove = Path.Combine(filePath, File)
        ' File.SaveAs(dove)



        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12


        '    Try


        Dim clientUP As New RestClient("https://93.63.195.98/fmi/data/vLatest/databases/Asi/layouts/webRinnoviAllegati2/records/" & id_att & "/containers/Ricevuta_Pagamento_ext/1")
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

        '  Dim filePath As String = Server.MapPath(ResolveUrl("~/file_storage/"))
        Requestx.AddParameter("upload", Server.MapPath(ResolveUrl("~/file_storage_RinnoviPag/")) & nomecaricato & "")
        '  Requestx.AddParameter("upload", "D:\Dropbox\soft\Projects\ASIWeb\ASIWeb\file_storage" & nomecaricato & "")
        '   Requestx.AddParameter("upload", "C:\file_storage\" & nomecaricato & "")
        Requestx.AddFile("upload", dove)


        Dim ResponseUP As IRestResponse = clientUP.Execute(Requestx)


        If QuantiAllegati(HiddenIDRinnovo.Value) = "no" Then
            '  lnkButton1.Enabled = False
            results.InnerHtml = ""

            results.InnerHtml = "<b>Documento caricato con successo: non è possibile caricare ulteriori documenti </b>"
            results.Attributes.Add("style", stileavviso)
        Else
            results.InnerHtml = "<b>Documento caricato con successo: è possibile caricare ulteriori documenti </b>"
            results.Attributes.Add("style", stileavviso)
        End If

        deleteFile(nomecaricato)

        Return True

        ' Catch ex As Exception

        '  End Try


    End Function
    Public Function NuovaRichiestaAllegato(codR As String, nomecaricato As String) As String
        '  Dim litNumRichieste As Literal = DirectCast(ContentPlaceHolder1.FindControl("LitNumeroRichiesta"), Literal)

        Dim fmsP As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
        ' Dim ds As DataSet


        fmsP.SetLayout("webRinnoviAllegati2")
        Dim Request = fmsP.CreateNewRecordRequest()

        Request.AddField("Codice_Rinnovo", codR)
        Request.AddField("NomeFileOnFS", nomecaricato)
        Request.AddField("Ricevuta_Pagamento_Descrizione", Data.PrendiStringaT(Server.HtmlEncode(txtNote.Text)))




        Dim IdAllegato As String = Request.Execute() 'per upload

        Dim fmsP11 As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
        '  Dim ds As DataSet

        fmsP11.SetLayout("webRinnoviAllegati2")
        Dim Request11 = fmsP11.CreateEditRequest(IdAllegato)
        Request11.AddScript("SistemaEncodingNoteUpload_PagamentoRinnovo2", IdAllegato)


        Request11.Execute()






        Dim record_id As String = ASIWeb.AsiModel.GetRecord_IDbyCodREquiparazione.GetRecord_IDRinnovi2(codR) ' per aggiornare status

        Dim fmsP1 As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
        '  Dim ds As DataSet
        Dim risposta As String = ""
        fmsP1.SetLayout("webRinnoviMaster")
        Dim Request1 = fmsP1.CreateEditRequest(record_id)

        If s = 158 Then
            Request1.AddField("CodiceStatus", "159")
        Else
            Request1.AddField("CodiceStatus", "157")
        End If



        'Try
        risposta = Request1.Execute()

        If s = 158 Then
            AsiModel.LogIn.LogCambioStatus(Session("codR"), "159", Session("WebUserEnte"), "rinnovo")
            AsiModel.Rinnovi.AggiornaStatusMoltia159(Session("codR"))

        Else
            AsiModel.LogIn.LogCambioStatus(Session("codR"), "157", Session("WebUserEnte"), "rinnovo")
            AsiModel.Rinnovi.AggiornaStatusMoltia157(Session("codR"))
        End If






        ' Catch ex As Exception

        ' End Try

        Dim token = PrendiToken()



        Return IdAllegato & "_|_" & token
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



    Private Function deleteFile(nomecaricato As String) As Boolean
        Dim FileToDelete As String
        Dim ritorno As Boolean = False
        '    FileToDelete = "D:\Soft\Lisa\ASIWeb\ASIWeb\file_storage\" & nomecaricato
        FileToDelete = Server.MapPath("../file_storage_RinnoviPag/" & nomecaricato)
        Try


            If System.IO.File.Exists(FileToDelete.ToString()) = True Then
                System.IO.File.Delete(FileToDelete.ToString())
                ritorno = True
            Else
                ritorno = False

            End If

        Catch ex As Exception
            ritorno = False
        End Try
        '  Try


        '    If System.IO.File.Exists(FileToDelete) = True Then

        '        System.IO.File.Delete(FileToDelete)
        '        ritorno = True
        '    Else

        '        ritorno = False


        '    End If
        'Catch ex As Exception
        '    ritorno = False
        'End Try
        Return ritorno
    End Function

    Protected Sub lnkDashboard_Click(sender As Object, e As EventArgs) Handles lnkDashboard.Click
        Session("AnnullaREqui") = "pagamentoRin"
        Response.Redirect("dashboardRinnovi2.aspx?open=" & Session("CodR"))
        '  Response.Redirect("dashboardRinnovi2.aspx?open=" & Session("CodR") & "&ris=" & deEnco.QueryStringEncode("ok"))

    End Sub

    Protected Sub lnkButton1_Click(sender As Object, e As EventArgs) Handles lnkButton1.Click

        If Page.IsValid Then
            Dim whereToSave As String = "../file_storage_RinnoviPag/"
            Dim fileUpload As HttpPostedFile = FileUpload1.PostedFile

            If QuantiAllegati(HiddenIDRinnovo.Value) = "no" Then

                lnkButton1.Enabled = False
                lnkButton1.Text = ""
                uploadedFiles.Text = ""

                results.InnerHtml = "<b>non è possibile caricare ulteriori documenti </b><br/>"
                results.Attributes.Add("style", stileavviso)


            Else
                If Not FileUpload1.PostedFile Is Nothing And FileUpload1.PostedFile.ContentLength > 0 Then


                    If FileUpload1.PostedFile.ContentLength > MassimoPeso Then
                        results.InnerHtml = "Il file è troppo grande. Massimo 3 mb<br>"
                        results.Attributes.Add("style", stileavviso)
                    ElseIf FileUpload1.PostedFile.ContentLength <= MassimoPeso Then
                        tokenZ = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()))
                        ext = Path.GetExtension((fileUpload.FileName))
                        nomefileReale = Path.GetFileName(fileUpload.FileName)
                        nomecaricato = HiddenIDRinnovo.Value & "_" + tokenZ + ext
                        fileUpload.SaveAs(MapPath(whereToSave + nomecaricato))

                        results.InnerHtml = "<b>Documento caricato con successo: " & nomecaricato & "</b><br/>"
                        results.Attributes.Add("style", stileavviso)



                        Dim tokenx As String = ""
                        Dim id_att As String = ""
                        Dim tuttoRitorno As String = ""

                        tuttoRitorno = NuovaRichiestaAllegato(HiddenIDRinnovo.Value, nomecaricato)
                        txtNote.Text = ""
                        Dim arrKeywords As String() = Split(tuttoRitorno, "_|_")
                        tokenx = arrKeywords(1)
                        id_att = arrKeywords(0)
                        Try
                            CaricaSuFM(tokenx, id_att, nomecaricato)
                        Catch ex As Exception
                            results.InnerHtml = "<b>Documento non caricato: </b><br/>"
                            results.Attributes.Add("style", stileavviso)
                        End Try

                    Else
                        results.InnerHtml = "<b>Carica il documento </b><br/>"
                        results.Attributes.Add("style", stileavviso)

                    End If




                End If





            End If



        End If

    End Sub
End Class