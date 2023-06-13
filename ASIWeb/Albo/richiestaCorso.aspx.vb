Imports fmDotNet
Imports System.Web.Services
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed
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
Imports System.Web.Services.Description
Imports System.EnterpriseServices.CompensatingResourceManager

Public Class richiestaCorso
    Inherits System.Web.UI.Page
    Dim webserver As String = ConfigurationManager.AppSettings("webserver")
    Dim utente As String = ConfigurationManager.AppSettings("utente")
    Dim porta As String = ConfigurationManager.AppSettings("porta")
    Dim pass As String = ConfigurationManager.AppSettings("pass")
    Dim dbb As String = ConfigurationManager.AppSettings("dbb")
    Dim cultureFormat As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("it-IT")
    Dim deEnco As New Ed()
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
    Dim stileavviso As String = "width: 100%; margin-top: 4px; padding: 16px; border-radius: 5px; background-color:   #f8d7da; color: #b71c1c"


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If
        If IsNothing(Session("codice")) Then
            Response.Redirect("../login.aspx")
        End If

        '  Dim newCulture As System.Globalization.CultureInfo = System.Globalization.CultureInfo.CurrentUICulture.Clone()
        cultureFormat.NumberFormat.CurrencySymbol = "€"
        cultureFormat.NumberFormat.CurrencyDecimalDigits = 2
        cultureFormat.NumberFormat.CurrencyGroupSeparator = String.Empty
        cultureFormat.NumberFormat.CurrencyDecimalSeparator = ","
        System.Threading.Thread.CurrentThread.CurrentCulture = cultureFormat
        System.Threading.Thread.CurrentThread.CurrentUICulture = cultureFormat




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
        Dim codR As String = ""
        codR = deEnco.QueryStringDecode(Request.QueryString("codR"))
        If Not String.IsNullOrEmpty(codR) Then


            Session("IDCorso") = codR
            Dim DettaglioCorso As New DatiNuovoCorso

            DettaglioCorso = Corso.PrendiValoriNuovoCorso(Session("IDCorso"))
            Dim IDCorso As String = DettaglioCorso.IDCorso
            Dim CodiceEnteRichiedente As String = DettaglioCorso.CodiceEnteRichiedente
            Dim DescrizioneEnteRichiedente As String = DettaglioCorso.DescrizioneEnteRichiedente
            Dim TipoEnte As String = DettaglioCorso.TipoEnte
            Dim CodiceStatus As String = DettaglioCorso.CodiceStatus
            Dim DescrizioneStatus As String = DettaglioCorso.DescrizioneStatus
            HiddenIdRecord.Value = DettaglioCorso.IdRecord
            HiddenIDCorso.Value = DettaglioCorso.IDCorso
            lblIntestazioneCorso.Text = "<strong>ID Corso: </strong>" & IDCorso & "<strong> - Ente Richiedente: </strong>" & DescrizioneEnteRichiedente
        End If

        If Page.IsPostBack Then

            '  pnlFase1.Visible = False


        End If



    End Sub
    Protected Sub cvCaricaDiploma_ServerValidate(source As Object, args As ServerValidateEventArgs)
        If FileUpload1.PostedFile.ContentLength > 0 Then

            results.InnerHtml = ""
            results.Attributes.Remove("style")
            args.IsValid = True
        Else
            results.InnerHtml = "carica la documentazione nel campo [1] obbligatorio<br>"
            results.Attributes.Add("style", stileavviso)

            args.IsValid = False
        End If
    End Sub

    Protected Sub cvTipoFile_ServerValidate(source As Object, args As ServerValidateEventArgs)
        If FileUpload1.PostedFile.ContentLength > 0 Then
            Dim fileExtensions As String() = {".pdf", ".PDF", ".doc", ".docx", ".DOC", ".DOCX"}
            Dim pippo As String = FileUpload1.PostedFile.ContentType
            For i As Integer = 0 To fileExtensions.Length - 1
                If FileUpload1.PostedFile.FileName.Contains(fileExtensions(i)) Then
                    args.IsValid = True
                    results.InnerHtml = ""
                    results.Attributes.Remove("style")
                    Exit For
                Else
                    args.IsValid = False

                    results.InnerHtml = "il documento nel campo [1] deve essere in formato pdf, doc oppure docx"
                    results.Attributes.Add("style", stileavviso)
                End If
            Next

        End If
    End Sub

    Protected Sub cvTipoFile2_ServerValidate(source As Object, args As ServerValidateEventArgs)
        If FileUpload2.PostedFile.ContentLength > 0 Then
            Dim fileExtensions As String() = {".pdf", ".PDF", ".doc", ".docx", ".DOC", ".DOCX"}
            Dim pippo As String = FileUpload2.PostedFile.ContentType
            For i As Integer = 0 To fileExtensions.Length - 1
                If FileUpload2.PostedFile.FileName.Contains(fileExtensions(i)) Then
                    args.IsValid = True
                    results.InnerHtml = ""
                    results.Attributes.Remove("style")
                    Exit For
                Else
                    args.IsValid = False

                    results.InnerHtml = "il documento nel campo [2] deve essere in formato pdf, doc oppure docx altrimenti lascialo vuoto"
                    results.Attributes.Add("style", stileavviso)
                End If
            Next
        End If

    End Sub
    Protected Sub cvTipoFile3_ServerValidate(source As Object, args As ServerValidateEventArgs)
        If FileUpload3.PostedFile.ContentLength > 0 Then
            Dim fileExtensions As String() = {".pdf", ".PDF", ".doc", ".docx", ".DOC", ".DOCX"}
            Dim pippo As String = FileUpload3.PostedFile.ContentType
            For i As Integer = 0 To fileExtensions.Length - 1
                If FileUpload3.PostedFile.FileName.Contains(fileExtensions(i)) Then
                    args.IsValid = True
                    results.InnerHtml = ""
                    results.Attributes.Remove("style")

                    Exit For
                Else
                    args.IsValid = False

                    results.InnerHtml = "il documento nel campo [3] deve essere in formato pdf, doc oppure docx altrimenti lascialo vuoto"
                    results.Attributes.Add("style", stileavviso)
                End If
            Next

        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If Page.IsValid Then
            Dim whereToSave As String = "../file_storage/"
            Dim fileUpload11 As HttpPostedFile = FileUpload1.PostedFile
            Dim fileUpload21 As HttpPostedFile = FileUpload2.PostedFile
            Dim fileUpload31 As HttpPostedFile = FileUpload3.PostedFile
            Dim documentoGrande1 As Boolean = False
            Dim documentoGrande2 As Boolean = False
            Dim documentoGrande3 As Boolean = False
            Dim primoDocumento As Boolean = False
            Dim doc1 As Boolean = False
            Dim doc2 As Boolean = False
            Dim doc3 As Boolean = False


            If Not FileUpload1.PostedFile Is Nothing And FileUpload1.PostedFile.ContentLength > 0 Then
                doc1 = True

            End If

            If Not FileUpload2.PostedFile Is Nothing And FileUpload2.PostedFile.ContentLength > 0 Then
                If FileUpload2.PostedFile.ContentLength > MassimoPeso Then
                    doc2 = True
                End If


            End If

            If Not FileUpload3.PostedFile Is Nothing And FileUpload3.PostedFile.ContentLength > 0 Then
                If FileUpload3.PostedFile.ContentLength > MassimoPeso Then
                    doc3 = True
                End If

            End If

            If Not FileUpload1.PostedFile Is Nothing And FileUpload1.PostedFile.ContentLength > 0 Then
                If FileUpload1.PostedFile.ContentLength > MassimoPeso Then
                    documentoGrande1 = True
                    results.InnerHtml = "Il documento [1] era troppo grande. Massimo 3 mb<br>"
                    results.Attributes.Add("style", stileavviso)
                ElseIf (FileUpload1.PostedFile.ContentLength <= MassimoPeso And doc2 = False And doc3 = False) Then
                    tokenZ = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()))
                    ext = Path.GetExtension((fileUpload11.FileName))
                    nomefileReale = Path.GetFileName(fileUpload11.FileName)
                    nomecaricato = HiddenIdRecord.Value & "_" + tokenZ + ext
                    fileUpload11.SaveAs(MapPath(whereToSave + nomecaricato))
                    results.InnerHtml = "<b>Documento [1] caricato con successo: " & nomecaricato & "</b><br/>"
                    results.Attributes.Add("style", stileavviso)
                    Dim tokenx As String = ""
                    Dim id_att As String = ""
                    Dim tuttoRitorno As String = ""
                    tuttoRitorno = CaricaDatiDocumentoCorso(HiddenIdRecord.Value, HiddenIDCorso.Value, nomecaricato, 0)
                    Dim arrKeywords As String() = Split(tuttoRitorno, "_|_")
                    tokenx = arrKeywords(1)
                    id_att = arrKeywords(0)

                    Try
                        primoDocumento = CaricaSuFM(tokenx, id_att, nomecaricato, 0)
                    Catch ex As Exception
                        results.InnerHtml = "<b>Documento [1] non caricato: </b><br/>"
                        results.Attributes.Add("style", stileavviso)
                    End Try

                Else
                    results.InnerHtml = "<b>Carica il documento [1] </b><br/>"
                    results.Attributes.Add("style", stileavviso)


                End If


            End If

            If documentoGrande1 = False Then
                If Not FileUpload2.PostedFile Is Nothing And FileUpload2.PostedFile.ContentLength > 0 Then
                    If FileUpload2.PostedFile.ContentLength > MassimoPeso Then
                        documentoGrande2 = True
                        results.InnerHtml = "Il documento [2] era troppo grande. Massimo 3 mb<br>"
                        results.Attributes.Add("style", stileavviso)
                    ElseIf FileUpload2.PostedFile.ContentLength <= MassimoPeso Then
                        tokenZ = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()))
                        ext = Path.GetExtension((fileUpload21.FileName))
                        nomefileReale = Path.GetFileName(fileUpload21.FileName)
                        nomecaricato = HiddenIdRecord.Value & "_" + tokenZ + ext
                        fileUpload21.SaveAs(MapPath(whereToSave + nomecaricato))
                        results.InnerHtml = "<b>Documento [2] caricato con successo: " & nomecaricato & "</b><br/>"
                        results.Attributes.Add("style", stileavviso)
                        Dim tokenx As String = ""
                        Dim id_att As String = ""
                        Dim tuttoRitorno As String = ""
                        tuttoRitorno = CaricaDatiDocumentoCorso(HiddenIdRecord.Value, HiddenIDCorso.Value, nomecaricato, 1)
                        Dim arrKeywords As String() = Split(tuttoRitorno, "_|_")
                        tokenx = arrKeywords(1)
                        id_att = arrKeywords(0)

                        Try
                            CaricaSuFM(tokenx, id_att, nomecaricato, 1)
                        Catch ex As Exception
                            results.InnerHtml = "<b>Documento [2] non caricato: </b><br/>"
                            results.Attributes.Add("style", stileavviso)
                        End Try

                    Else
                        results.InnerHtml = "<b>Carica il documento [2] </b><br/>"
                        results.Attributes.Add("style", stileavviso)


                    End If


                End If



            End If

            If documentoGrande1 = False Then
                If Not FileUpload3.PostedFile Is Nothing And FileUpload3.PostedFile.ContentLength > 0 Then
                    If FileUpload3.PostedFile.ContentLength > MassimoPeso Then
                        documentoGrande3 = True
                        results.InnerHtml = "Il documento [3] era troppo grande. Massimo 3 mb<br>"
                        results.Attributes.Add("style", stileavviso)
                    ElseIf FileUpload3.PostedFile.ContentLength <= MassimoPeso Then

                        tokenZ = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()))
                        ext = Path.GetExtension((fileUpload31.FileName))
                        nomefileReale = Path.GetFileName(fileUpload31.FileName)
                        nomecaricato = HiddenIdRecord.Value & "_" + tokenZ + ext
                        fileUpload31.SaveAs(MapPath(whereToSave + nomecaricato))
                        results.InnerHtml = "<b>Documento [3] caricato con successo: " & nomecaricato & "</b><br/>"
                        results.Attributes.Add("style", stileavviso)
                        Dim tokenx As String = ""
                        Dim id_att As String = ""
                        Dim tuttoRitorno As String = ""
                        tuttoRitorno = CaricaDatiDocumentoCorso(HiddenIdRecord.Value, HiddenIDCorso.Value, nomecaricato, 2)
                        Dim arrKeywords As String() = Split(tuttoRitorno, "_|_")
                        tokenx = arrKeywords(1)
                        id_att = arrKeywords(0)

                        Try
                            CaricaSuFM(tokenx, id_att, nomecaricato, 2)
                        Catch ex As Exception
                            results.InnerHtml = "<b>Documento [3] non caricato: </b><br/>"
                            results.Attributes.Add("style", stileavviso)
                        End Try

                    Else
                        results.InnerHtml = "<b>Carica il documento [3] </b><br/>"
                        results.Attributes.Add("style", stileavviso)


                    End If



                End If


            End If

            If (documentoGrande1 = False And documentoGrande2 = False And documentoGrande3 = False And primoDocumento = True) Then


                Session("fase") = "2"
                Response.Redirect("richiestaCorsoF2.aspx?codR=" & deEnco.QueryStringEncode(Session("IDCorso")) & "&record_ID=" & deEnco.QueryStringEncode(Session("id_record")) & "&nomef=" & nomecaricato)


            End If

            If (documentoGrande1 = False And documentoGrande2 = True And documentoGrande3 = False And primoDocumento = True) Then
                results.InnerHtml = "Il documento [2] era troppo grande. Massimo 3 mb<br>"
                results.Attributes.Add("style", stileavviso)

            End If

            If (documentoGrande1 = False And documentoGrande2 = False And documentoGrande3 = True And primoDocumento = True) Then
                results.InnerHtml = "Il documento [3] era troppo grande. Massimo 3 mb<br>"
                results.Attributes.Add("style", stileavviso)

            End If




        End If


    End Sub

    Public Function CaricaSuFM(tokenx As String, id As String, nomecaricato As String, i As Integer) As Boolean



        Dim host As String = HttpContext.Current.Request.Url.Host.ToLower()

        Dim filePath As String = Server.MapPath(ResolveUrl("~/file_storage/"))

        '  Dim File As HttpPostedFile = inputfile.PostedFile
        Dim File As String = nomecaricato


        Dim dove = Path.Combine(filePath, File)
        ' File.SaveAs(dove)



        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        Dim clientUP As RestClient

        '    Try

        Select Case i
            Case 0
                clientUP = New RestClient("https://93.63.195.98/fmi/data/vLatest/databases/Asi/layouts/webCorsiRichiesta/records/" & id & "/containers/Programma_Tecnico_Didattico/1")

            Case 1
                clientUP = New RestClient("https://93.63.195.98/fmi/data/vLatest/databases/Asi/layouts/webCorsiRichiesta/records/" & id & "/containers/Programma_Tecnico_Didattico2/1")

            Case 2
                clientUP = New RestClient("https://93.63.195.98/fmi/data/vLatest/databases/Asi/layouts/webCorsiRichiesta/records/" & id & "/containers/Programma_Tecnico_Didattico3/1")
        End Select

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
        Requestx.AddParameter("upload", Server.MapPath(ResolveUrl("~/file_storage/")) & nomecaricato & "")
        '  Requestx.AddParameter("upload", "D:\Dropbox\soft\Projects\ASIWeb\ASIWeb\file_storage" & nomecaricato & "")
        '   Requestx.AddParameter("upload", "C:\file_storage\" & nomecaricato & "")
        Requestx.AddFile("upload", dove)


        Dim ResponseUP As IRestResponse = clientUP.Execute(Requestx)

        Return True

        ' Catch ex As Exception

        '  End Try


    End Function
    Public Function CaricaDatiDocumentoCorso(codR As String, IDCorso As String, nomecaricato As String, i As Integer) As String
        '  Dim litNumRichieste As Literal = DirectCast(ContentPlaceHolder1.FindControl("LitNumeroRichiesta"), Literal)


        ' Dim ds As DataSet


        Dim fmsP As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
        '  Dim ds As DataSet
        Dim risposta As String = ""
        fmsP.SetLayout("webCorsiRichiesta")
        Dim Request = fmsP.CreateEditRequest(codR)






        Select Case i
            Case 0
                Request.AddField("NomeFileOnFS", nomecaricato)


            Case 1
                Request.AddField("NomeFileOnFS2", nomecaricato)

            Case 2
                Request.AddField("NomeFileOnFS3", nomecaricato)

        End Select

        Request.AddField("NoteUploadDocumentoCorso", Data.PrendiStringaT(Server.HtmlEncode(txtNote.Text)))




        Request.AddField("Fase", "1")
        Request.AddField("Codice_Status", "51")
        Request.AddScript("SistemaEncodingNoteUpload_PianoCorso", IDCorso)
        'If qualeStatus = "3" Then
        '    Request1.AddField("Status_ID", "4")
        'Else
        '    Request1.AddField("Status_ID", "12")
        'End If
        Try
            risposta = Request.Execute()
            AsiModel.LogIn.LogCambioStatus(IDCorso, "51", Session("WebUserEnte"), "corso")
            'If qualeStatus = "3" Then
            '    AsiModel.LogIn.LogCambioStatus(codR, "4", Session("WebUserEnte"))
            'Else
            '    AsiModel.LogIn.LogCambioStatus(codR, "12", Session("WebUserEnte"))
            'End If



        Catch ex As Exception

        End Try

        Dim token = PrendiToken()

        Return codR & "_|_" & token
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

    Protected Sub btnFase2_Click(sender As Object, e As EventArgs) Handles btnFase2.Click
        Session("fase") = "2"
        Response.Redirect("richiestaCorsoF2.aspx?codR=" & deEnco.QueryStringEncode(Session("IDCorso")) & "&record_ID=" & deEnco.QueryStringEncode(Session("id_record")))
    End Sub
End Class