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

Public Class upLegEquiparazioni
    Inherits System.Web.UI.Page

    ' 100  kb=100*1024
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
        record_ID = deEnco.QueryStringDecode(Request.QueryString("record_ID"))
        If Not String.IsNullOrEmpty(record_ID) Then

            Session("id_record") = record_ID

        End If

        If IsNothing(Session("id_record")) Then
            Response.Redirect("../login.aspx")
        End If

        codR = deEnco.QueryStringDecode(Request.QueryString("codR"))


        If Not String.IsNullOrEmpty(codR) Then


            Session("IDEquiparazione") = codR
            Dim DettaglioEquiparazione As New DatiNuovaEquiparazione

            DettaglioEquiparazione = Equiparazione.PrendiValoriNuovaEquiparazione(Session("IDEquiparazione"))
            Dim IDEquiparazione As String = DettaglioEquiparazione.IDEquiparazione
            Dim CodiceEnteRichiedente As String = DettaglioEquiparazione.CodiceEnteRichiedente
            Dim DescrizioneEnteRichiedente As String = DettaglioEquiparazione.DescrizioneEnteRichiedente
            Dim TipoEnte As String = DettaglioEquiparazione.TipoEnte
            Dim CodiceStatus As String = DettaglioEquiparazione.CodiceStatus
            '    Session("codiceStatus") = DettaglioCorso.CodiceStatus
            Dim DescrizioneStatus As String = DettaglioEquiparazione.DescrizioneStatus
            'Dim indirizzoSvolgimento As String = DettaglioCorso.IndirizzoSvolgimento & " - " & DettaglioCorso.LocalitaSvolgimento _
            '     & DettaglioCorso.CapSvolgimento & " - " & DettaglioCorso.PRSvolgimento & " - " & DettaglioCorso.RegioneSvolgimento
            Dim nominativo As String = DettaglioEquiparazione.Nome & " " & DettaglioEquiparazione.Cognome
            Dim codiceFiscale As String = DettaglioEquiparazione.CodiceFiscale
            Dim codiceTessera As String = DettaglioEquiparazione.CodiceTessera
            Dim dataScadenza As String = DettaglioEquiparazione.DataScadenza
            Dim pagamento As Decimal = DettaglioEquiparazione.PagamentoTotale
            idEqui = DettaglioEquiparazione.IDEquiparazione
            HiddenIDEquiparazione.Value = DettaglioEquiparazione.IDEquiparazione
            HiddenIdRecord.Value = DettaglioEquiparazione.IdRecord

            lblIntestazioneEquiparazioni.Text = "<strong>ID Equiparazione: </strong>" & IDEquiparazione & "<strong> - Ente Richiedente: </strong>" & DescrizioneEnteRichiedente & "<br />" &
                 "<strong>Nominativo: </strong>" & nominativo & "<br />" &
                 "<strong>Codice Fiscale: </strong>" & codiceFiscale & " <strongTessera ASI: </strong>" & codiceTessera &
                 " <strong>Data Scadenza: </strong>" & dataScadenza & " <strong>Pagamento: </strong>" & pagamento & "€"

        End If




        If QuantiAllegati(idEqui) = "no" Then

            Button1.Enabled = False
            uploadedFiles.Text = ""

            uploadedFiles.Text = "<b>non è possibile caricare ulteriori documenti </b>"

        End If



    End Sub

    Public Function QuantiAllegati(codR As String) As String
        Dim fmsP As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
        Dim ds As DataSet
        Dim quanti As Integer = 0
        Dim risposta As String = ""

        fmsP.SetLayout("webEquiAllegati")

        Dim request = fmsP.CreateFindRequest()

        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        RequestP.AddSearchField("Codice_Equiparazione", codR, Enumerations.SearchOption.equals)

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

        Dim filePath As String = Server.MapPath(ResolveUrl("~/file_storage_EquiPag/"))

        '  Dim File As HttpPostedFile = inputfile.PostedFile
        Dim File As String = nomecaricato


        Dim dove = Path.Combine(filePath, File)
        ' File.SaveAs(dove)



        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12


        '    Try


        Dim clientUP As New RestClient("https://93.63.195.98/fmi/data/vLatest/databases/Asi/layouts/webEquiAllegati/records/" & id_att & "/containers/Ricevuta_Pagamento_ext/1")
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
        Requestx.AddParameter("upload", Server.MapPath(ResolveUrl("~/file_storage_EquiPag/")) & nomecaricato & "")
        '  Requestx.AddParameter("upload", "D:\Dropbox\soft\Projects\ASIWeb\ASIWeb\file_storage" & nomecaricato & "")
        '   Requestx.AddParameter("upload", "C:\file_storage\" & nomecaricato & "")
        Requestx.AddFile("upload", dove)


        Dim ResponseUP As IRestResponse = clientUP.Execute(Requestx)


        If QuantiAllegati(HiddenIDEquiparazione.Value) = "no" Then
            Button1.Enabled = False
            uploadedFiles.Text = ""

            uploadedFiles.Text = "<b>File caricato con successo: </b><br/><b>non è possibile caricare ulteriori documenti </b>"
        Else

            uploadedFiles.Text += "<b>File caricato con successo: </b><br/><b>è possibile caricare ulteriori documenti </b>"
        End If



        Return True

        ' Catch ex As Exception

        '  End Try


    End Function
    Public Function NuovaRichiestaAllegato(codR As String, nomecaricato As String) As String
        '  Dim litNumRichieste As Literal = DirectCast(ContentPlaceHolder1.FindControl("LitNumeroRichiesta"), Literal)

        Dim fmsP As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
        ' Dim ds As DataSet


        fmsP.SetLayout("webEquiAllegati")
        Dim Request = fmsP.CreateNewRecordRequest()

        Request.AddField("Codice_Equiparazione", codR)
        Request.AddField("NomeFileOnFS", nomecaricato)
        Request.AddField("Ricevuta_Pagamento_Descrizione", Data.PrendiStringaT(Server.HtmlEncode(txtNote.Text)))



        'Request.AddField("Codice_Articolo", AsiModel.CodiceArticolo)
        'Request.AddField("Nome_Articolo", AsiModel.NomeArticolo)
        'Request.AddField("Quantita", txtQuantita.Text)


        Dim IdAllegato As String = Request.Execute() 'per upload

        Dim fmsP11 As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
        '  Dim ds As DataSet

        fmsP11.SetLayout("webEquiAllegati")
        Dim Request11 = fmsP11.CreateEditRequest(IdAllegato)
        '   Request.AddScript("SistemaEncodingNoteUpload_Pagamento", IdAllegato)

        Request11.Execute()


        Dim record_id As String = ASIWeb.AsiModel.GetRecord_IDbyCodREquiparazione.GetRecord_ID(codR) ' per aggiornare status

        Dim fmsP1 As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
        '  Dim ds As DataSet
        Dim risposta As String = ""
        fmsP1.SetLayout("webEquiparazioniRichiesta")
        Dim Request1 = fmsP1.CreateEditRequest(record_id)

        'If qualeStatus = "3" Then
        Request1.AddField("Codice_Status", "112")
        'Else
        '    Request1.AddField("Status_ID", "12")
        'End If
        Try
            risposta = Request1.Execute()
            AsiModel.LogIn.LogCambioStatus(Session(codR), "112", Session("WebUserEnte"), "equiparazione")
            'If qualeStatus = "3" Then
            '    AsiModel.LogIn.LogCambioStatus(codR, "4", Session("WebUserEnte"))
            'Else
            '    AsiModel.LogIn.LogCambioStatus(codR, "12", Session("WebUserEnte"))
            'End If

            deleteFile(nomecaricato)

        Catch ex As Exception

        End Try

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

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        If QuantiAllegati(HiddenIDEquiparazione.Value) = "no" Then

            Button1.Enabled = False
            uploadedFiles.Text = ""

            uploadedFiles.Text = "<b>non è possibile caricare ulteriori documenti </b>"


        Else




            If uploadProgress.Files.Count > 0 Then





                '****************************************************
                Dim files As OboutFileCollection = uploadProgress.Files
                Dim i As Integer

                uploadedFiles.Text = ""

                For i = 0 To files.Count - 1 Step 1
                    Dim file As OboutPostedFile = files(i)



                    Dim whereToSave As String = "../file_storage_equiPag/"

                    tokenZ = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()))
                    ext = Path.GetExtension((file.FileName))
                    nomefileReale = Path.GetFileName(file.FileName)
                    nomecaricato = HiddenIDEquiparazione.Value & "_" + tokenZ + ext





                    '   nomecaricato = "cv_" + Session("codiceProvvisorio") + ext

                    file.SaveAs(MapPath(whereToSave + nomecaricato))




                    If uploadedFiles.Text.Length = 0 Then

                        'If QuantiAllegati(HiddenIDCorso.Value) = "no" Then
                        '    Button1.Enabled = False
                        '    uploadedFiles.Text = ""

                        '    uploadedFiles.Text = "<b>File caricato con successo: " & nomecaricato & "</b><br/><b>non è possibile caricare ulteriori documenti </b>"
                        'Else

                        '    uploadedFiles.Text += "<b>File caricato con successo: " & nomecaricato & "</b><br/><b>è possibile caricare ulteriori documenti </b>"
                        'End If




                    End If

                    '        file.SaveAs(MapPath(whereToSave + Path.GetFileName(file.FileName)))

                    'If uploadedFiles.Text.Length = 0 Then
                    '    '        uploadedFiles.Text += "<b>File loaded:</b><table border=0 cellspacing=0>"

                    'Else

                    'End If

                    'uploadedFiles.Text += "<tr>"
                    'uploadedFiles.Text += "<td class='option2'>" + Path.GetFileName(file.FileName) + "</td>"
                    'uploadedFiles.Text += "<td style='font:11px Verdana;'>&nbsp;&nbsp;" + file.ContentLength.ToString() + " bytes </td>"
                    'uploadedFiles.Text += "<td class='option2'>&nbsp;&nbsp;(" + file.ContentType + ")</td>"
                    'uploadedFiles.Text += "<td style='font:11px Verdana;'>&nbsp;&nbsp;<b>a</b>: " + whereToSave + "</td>"
                    'uploadedFiles.Text += "</tr>"



                Next

                Dim tokenx As String = ""
                Dim id_att As String = ""
                Dim tuttoRitorno As String = ""

                tuttoRitorno = NuovaRichiestaAllegato(HiddenIDEquiparazione.Value, nomecaricato)
                txtNote.Text = ""
                Dim arrKeywords As String() = Split(tuttoRitorno, "_|_")
                tokenx = arrKeywords(1)
                id_att = arrKeywords(0)
                CaricaSuFM(tokenx, id_att, nomecaricato)



            End If

        End If
        'Else
        'End If
    End Sub
    Private Function deleteFile(nomecaricato As String) As Boolean
        Dim FileToDelete As String
        Dim ritorno As Boolean = False
        '    FileToDelete = "D:\Soft\Lisa\ASIWeb\ASIWeb\file_storage\" & nomecaricato
        FileToDelete = Server.MapPath("../file_storage_equiPag/" & nomecaricato)
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
        Response.Redirect("dashboardEqui.aspx#" & codR)
    End Sub
End Class