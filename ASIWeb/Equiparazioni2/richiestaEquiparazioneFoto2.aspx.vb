﻿Imports fmDotNet
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
Public Class richiestaEquiparazioneFoto2
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
    Const massimaaltezza As Integer = 1400
    Const massinalarghezza As Integer = 1000
    Const minimaAltezza As Integer = 140
    Const minimaLarghezza As Integer = 100

    Dim ext As String = " "
    Dim nomefileReale As String = " "
    Dim qualeStatus As String = ""
    Dim nomecaricato As String = ""
    Dim codR As String = ""
    Dim tokenZ As String = ""
    Dim record_ID As String = ""
    Dim pag As Integer = 0
    Dim skip As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If
        If IsNothing(Session("codice")) Then
            Response.Redirect("../login.aspx")
        End If

        Dim newCulture As System.Globalization.CultureInfo = System.Globalization.CultureInfo.CurrentUICulture.Clone()
        cultureFormat.NumberFormat.CurrencySymbol = "€"
        cultureFormat.NumberFormat.CurrencyDecimalDigits = 2
        cultureFormat.NumberFormat.CurrencyGroupSeparator = String.Empty
        cultureFormat.NumberFormat.CurrencyDecimalSeparator = ","
        System.Threading.Thread.CurrentThread.CurrentCulture = cultureFormat
        System.Threading.Thread.CurrentThread.CurrentUICulture = cultureFormat

        pag = Request.QueryString("pag")
        skip = Request.QueryString("skip")

        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If

        record_ID = deEnco.QueryStringDecode(Request.QueryString("record_id"))
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
            DettaglioEquiparazione = Equiparazione.PrendiValoriNuovaEquiparazione2(Session("id_record"))
            Dim verificato As String = DettaglioEquiparazione.EquiCF
            If verificato = "0" Then
                Response.Redirect("DashboardEqui.aspx?ris=" & deEnco.QueryStringEncode("no"))

            End If
            Dim IDEquiparazione As String = DettaglioEquiparazione.IDEquiparazione
            Dim CodiceEnteRichiedente As String = DettaglioEquiparazione.CodiceEnteRichiedente
            Dim DescrizioneEnteRichiedente As String = DettaglioEquiparazione.DescrizioneEnteRichiedente
            Dim TipoEnte As String = DettaglioEquiparazione.TipoEnte
            Dim CodiceStatus As String = DettaglioEquiparazione.CodiceStatus
            Dim DescrizioneStatus As String = DettaglioEquiparazione.DescrizioneStatus
            '  Dim TitoloCorso As String = DettaglioEquiparazione.TitoloCorso
            HiddenIdRecord.Value = DettaglioEquiparazione.IdRecord
            HiddenIDEquiparazione.Value = DettaglioEquiparazione.IDEquiparazione
            Dim codiceFiscale As String = DettaglioEquiparazione.CodiceFiscale


            Dim datiCF As Object
            If Session("CFEE") = "EE" Then




                datiCF = AsiModel.getDatiCodiceFiscaleEE(Session("nomeEE"), Session("cognomeEE"), Session("codiceTesseraEE"), Session("dataNascitaEE"))
            Else


                datiCF = AsiModel.getDatiCodiceFiscale(codiceFiscale)
                ' End If
            End If

            lblIntestazioneEquiparazione.Text =
             "<strong> - Codice Fiscale: </strong>" & datiCF.CodiceFiscale &
             "<strong> - Tessera Ass.: </strong>" & datiCF.CodiceTessera & "<br />" &
             "<strong> - Nominativo: </strong>" & datiCF.Nome & " " & datiCF.Cognome &
             "<strong> - Ente Richiedente: </strong>" & DescrizioneEnteRichiedente
        End If
        'If fase = 2 Then
        ' lblnomef.Text = "Diploma correttamente caricato"


        ' End If
        If Page.IsPostBack Then

            '  pnlFase1.Visible = False


        End If
    End Sub

    Public Function CaricaSuFM(tokenx As String, id As String, nomecaricato As String) As Boolean

        Dim host As String = HttpContext.Current.Request.Url.Host.ToLower()

        Dim filePath As String = Server.MapPath(ResolveUrl("~/fotoEquiparazioni/"))

        '  Dim File As HttpPostedFile = inputfile.PostedFile
        Dim File As String = nomecaricato


        Dim dove = Path.Combine(filePath, File)
        ' File.SaveAs(dove)



        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12


        '    Try


        Dim clientUP As New RestClient("https://93.63.195.98/fmi/data/vLatest/databases/Asi/layouts/webEquiparazioniRichiestaMolti/records/" & id & "/containers/FotoEquiparazione/1")
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
        Requestx.AddParameter("upload", Server.MapPath(ResolveUrl("~/fotoEquiparazioni/")) & nomecaricato & "")
        '  Requestx.AddParameter("upload", "D:\Dropbox\soft\Projects\ASIWeb\ASIWeb\file_storage" & nomecaricato & "")
        '   Requestx.AddParameter("upload", "C:\file_storage\" & nomecaricato & "")
        Requestx.AddFile("upload", dove)


        Dim ResponseUP As IRestResponse = clientUP.Execute(Requestx)

        Return True

        ' Catch ex As Exception

        '  End Try


    End Function
    Protected Sub cvCaricaLaFoto_ServerValidate(source As Object, args As ServerValidateEventArgs)
        If inputfile.PostedFile.ContentLength > 0 Then


            args.IsValid = True
        Else
            results.InnerHtml = "carica la tua foto<br>"
            results.Attributes.Add("style", "width: 100%;  padding: 16px; border-radius: 5px; background-color:   #f8d7da; color: #b71c1c")


            args.IsValid = False
        End If
    End Sub

    Protected Sub cvTipoFile_ServerValidate(source As Object, args As ServerValidateEventArgs)

        Dim fileExtensions As String() = {".jpg", ".jpeg", ".JPG", ".JPEG"}
        Dim pippo As String = inputfile.PostedFile.ContentType
        For i As Integer = 0 To fileExtensions.Length - 1
            If inputfile.PostedFile.FileName.Contains(fileExtensions(i)) Then
                args.IsValid = True

                Exit For
            Else
                args.IsValid = False

                results.InnerHtml = "il file deve essere in formato jpg or jpeg<br>"
                results.Attributes.Add("style", "width: 100%;  padding: 16px; border-radius: 5px; background-color:   #f8d7da; color: #b71c1c")

            End If
        Next


    End Sub


    Public Function CaricaDatiDocumentoCorso(id As String, codR As String, nomecaricato As String) As String
        '  Dim litNumRichieste As Literal = DirectCast(ContentPlaceHolder1.FindControl("LitNumeroRichiesta"), Literal)


        ' Dim ds As DataSet


        Dim fmsP As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
        '  Dim ds As DataSet
        Dim risposta As String = ""
        fmsP.SetLayout("webEquiparazioniRichiestaMolti")
        Dim Request = fmsP.CreateEditRequest(id)
        Request.AddField("NomeFileFotoFS", nomecaricato)
        ' Request.AddField("Equi_Fase", "2")
        Try
            risposta = Request.Execute()



        Catch ex As Exception

        End Try

        Dim token = PrendiToken()

        Return id & "_|_" & token
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

    Protected Sub chkSalta_CheckedChanged(sender As Object, e As EventArgs) Handles chkSalta.CheckedChanged


        If chkSalta.Checked = True Then


            'RequiredFieldValidator1.Enabled = False
            'RegularExpressionValidator1.Enabled = False
            nomecaricato = "caricamento saltato"
            CaricaDatiDocumentoCorso(record_ID, codR, nomecaricato)
            'resultsSalta.InnerHtml = "Attendi con pazienza....<br>"
            'resultsSalta.Attributes.Add("style", "width: 100%; margin-top: 4px; padding: 16px; border-radius: 5px; background-color:   #f8d7da; color: #b71c1c")

            Response.Redirect("richiestaEquiparazioneDati12.aspx?codR=" & deEnco.QueryStringEncode(Session("IDEquiparazione")) & "&record_ID=" & deEnco.QueryStringEncode(Session("id_record")) & "&nomef=" & nomecaricato)
        End If
    End Sub

    Protected Sub lnkButton1_Click(sender As Object, e As EventArgs) Handles lnkButton1.Click
        If Page.IsValid Then
            Dim img As System.Drawing.Image = System.Drawing.Image.FromStream(inputfile.PostedFile.InputStream)


            If inputfile.PostedFile.ContentLength > MassimoPeso Then
                results.InnerHtml = "Il file è troppo grande. Massimo " & MassimoPeso / 1024 & " kb.<br>"
                results.Attributes.Add("style", "width: 100%;  padding: 16px; border-radius: 5px; background-color:   #f8d7da; color: #b71c1c")

                ' controllo del tipo di file
            ElseIf Not inputfile.PostedFile.ContentType.StartsWith("image") Then
                results.InnerHtml = "Il file non è valido. Deve essere un'immagine.<br>"
                results.Attributes.Add("style", "width: 100%;  padding: 16px; border-radius: 5px; background-color:   #f8d7da; color: #b71c1c")


            ElseIf img.Width < minimaLarghezza OrElse img.Height < minimaAltezza Then


                results.InnerHtml = "Immagine con larghezza e/o altezza troppo piccole.<br>"
                results.Attributes.Add("style", "width: 100%;  padding: 16px; border-radius: 5px; background-color:   #f8d7da; color: #b71c1c")

            ElseIf img.Width > img.Height Then
                results.InnerHtml = "l'altezza deve essere maggiore della larghezza.<br>"
                results.Attributes.Add("style", "width: 100%;  padding: 16px; border-radius: 5px; background-color:   #f8d7da; color: #b71c1c")

            ElseIf img.Width > massinalarghezza OrElse img.Height > massimaaltezza Then
                'Response.Write(maggiore)
                results.InnerHtml = "Immagine con dimensioni superiori a quelle consentite"
                results.Attributes.Add("style", "width: 100%;  padding: 16px; border-radius: 5px; background-color:   #f8d7da; color: #b71c1c")
            Else
                Dim rapporto As Integer
                rapporto = img.Height / 140
                Dim img2 As Drawing.Bitmap
                img2 = New Drawing.Bitmap(img, New Drawing.Size(Math.Ceiling(img.Width / rapporto), 140))

                Dim tokenZ = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()))
                nomecaricato = record_ID & "_" & tokenZ & ".jpg"

                '   Response.Write(Server.MapPath("..\corsisti\"))
                img2.Save(Server.MapPath("..\fotoEquiparazioni\") & nomecaricato, System.Drawing.Imaging.ImageFormat.Jpeg)
                inputfile.SaveAs(Server.MapPath("..\fotoEquiparazioni\") & record_ID & "_" & tokenZ & "_originale" & ".jpg")




                img.Dispose()


                Dim tokenx As String = ""
                Dim id_att As String = ""
                Dim tuttoRitorno As String = ""

                tuttoRitorno = CaricaDatiDocumentoCorso(record_ID, codR, nomecaricato)

                Dim arrKeywords As String() = Split(tuttoRitorno, "_|_")
                tokenx = arrKeywords(1)
                id_att = arrKeywords(0)
                CaricaSuFM(tokenx, id_att, nomecaricato)

                Response.Redirect("richiestaEquiparazioneDati12.aspx?codR=" & deEnco.QueryStringEncode(Session("IDEquiparazione")) & "&record_ID=" & deEnco.QueryStringEncode(Session("id_record")) & "&nomef=" & nomecaricato)



            End If
        End If
    End Sub
End Class