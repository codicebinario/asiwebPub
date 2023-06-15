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

Public Class vediDocumentazione2
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
    Dim codR As String = ""
    Dim tokenZ As String = ""
    Dim record_ID As String = ""
    Dim nominativo As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        '      Dim fase As String = Request.QueryString("fase")
        'Dim fase = deEnco.QueryStringDecode(Request.QueryString("fase"))
        'If Not String.IsNullOrEmpty(fase) Then

        '    Session("fase") = fase
        'End If

        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If
        If IsNothing(Session("codice")) Then
            Response.Redirect("../login.aspx")
        End If

        'If IsNothing(Session("codiceFiscale")) Then
        '    Response.Redirect("../login.aspx")
        'End If
        'If Session("procedi") <> "OK" Then

        '    Response.Redirect("checkTesseramento.aspx")

        'End If

        '  Dim newCulture As System.Globalization.CultureInfo = System.Globalization.CultureInfo.CurrentUICulture.Clone()
        cultureFormat.NumberFormat.CurrencySymbol = "€"
        cultureFormat.NumberFormat.CurrencyDecimalDigits = 2
        cultureFormat.NumberFormat.CurrencyGroupSeparator = String.Empty
        cultureFormat.NumberFormat.CurrencyDecimalSeparator = ","
        System.Threading.Thread.CurrentThread.CurrentCulture = cultureFormat
        System.Threading.Thread.CurrentThread.CurrentUICulture = cultureFormat

        'pag = Request.QueryString("pag")
        'skip = Request.QueryString("skip")

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
        '    nominativo = deEnco.QueryStringDecode(Request.QueryString("nominativo"))

        Dim IDEquiparazione As String = ""
        codR = deEnco.QueryStringDecode(Request.QueryString("codR"))
        If Not String.IsNullOrEmpty(codR) Then


            Session("IDEquiparazione") = codR
            '  Dim DettaglioEquiparazione As New DatiNuovaEquiparazione


            Dim DettaglioEquiparazione As New DatiNuovaEquiparazione
            DettaglioEquiparazione = Equiparazione.PrendiValoriNuovaEquiparazione2(Session("id_record"))


            Dim verificato As String = DettaglioEquiparazione.EquiCF

            If verificato = "0" Then
                Response.Redirect("DashboardEqui2.aspx?open=" & codR & "&ris=" & deEnco.QueryStringEncode("no"))

            End If
            IDEquiparazione = DettaglioEquiparazione.IDEquiparazione
            Dim CodiceEnteRichiedente As String = DettaglioEquiparazione.CodiceEnteRichiedente
            Dim DescrizioneEnteRichiedente As String = DettaglioEquiparazione.DescrizioneEnteRichiedente
            Dim TipoEnte As String = DettaglioEquiparazione.TipoEnte
            Dim CodiceStatus As String = DettaglioEquiparazione.CodiceStatus
            Dim DescrizioneStatus As String = DettaglioEquiparazione.DescrizioneStatus

            '  Dim TitoloCorso As String = DettaglioEquiparazione.TitoloCorso
            HiddenIdRecord.Value = DettaglioEquiparazione.IdRecord
            HiddenIDEquiparazione.Value = DettaglioEquiparazione.IDEquiparazione
            Dim codiceFiscale As String = DettaglioEquiparazione.CodiceFiscale
            Dim datiCF = AsiModel.getDatiCodiceFiscale(codiceFiscale)

            lblIntestazioneEquiparazione.Text =
                "<strong> - Codice Fiscale: </strong>" & datiCF.CodiceFiscale &
                "<strong> - N.Tessera: </strong>" & datiCF.CodiceTessera & "<br />" &
                "<strong> - Nominativo: </strong>" & datiCF.Nome & " " & datiCF.Cognome &
                "<strong> - Ente Richiedente: </strong>" & DescrizioneEnteRichiedente




        End If

        If Not Page.IsPostBack Then

            '  pnlFase1.Visible = False
            caricaDiplomaFoto(Session("id_record"))

        End If



    End Sub
    Sub caricaDiplomaFoto(IDEquiparazione As String)
        Try


            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            Dim ds As DataSet

            Dim nome As String
            Dim cognome As String
            Dim foto As String
            Dim tessera As String
            Dim diploma As String
            Dim email As String
            Dim codiceFiscale As String
            Dim recordid As String
            '    Dim NumeroTesseraAsi As String
            fmsP.SetLayout("webEquiparazioniRichiestaMolti")
            Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestP.AddSearchField("idrecord", IDEquiparazione, Enumerations.SearchOption.equals)


            ds = RequestP.Execute()


            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

                For Each dr In ds.Tables("main").Rows

                    nome = Data.FixNull(dr("Equi_nome"))
                    cognome = Data.FixNull(dr("Equi_cognome"))
                    tessera = Data.FixNull(dr("Equi_NumeroTessera"))
                    email = Data.FixNull(dr("Equi_IndirizzoEmail"))
                    codiceFiscale = Data.FixNull(dr("Equi_CodiceFiscale"))
                    foto = Data.FixNull(dr("NomeFileFotoFs"))
                    recordid = Data.FixNull(dr("Idrecord"))

                    If String.IsNullOrWhiteSpace(Data.FixNull(dr("FotoEquiparazione"))) Then
                        foto = "..\img\noimg.jpg"
                    Else
                        foto = "https://93.63.195.98" & Data.FixNull(dr("FotoEquiparazione"))
                    End If


                    If String.IsNullOrWhiteSpace(Data.FixNull(dr("DiplomaEquiparazione"))) Then
                        diploma = "..\img\noPdf.jpg"
                    Else
                        diploma = "https://93.63.195.98" & Data.FixNull(dr("DiplomaEquiparazione"))
                    End If



                    plTabellaEquiparazione.Controls.Add(New LiteralControl("<tr>"))

                    plTabellaEquiparazione.Controls.Add(New LiteralControl("<td>" & nome & "</td>"))
                    plTabellaEquiparazione.Controls.Add(New LiteralControl("<td>" & cognome & "</td>"))
                    plTabellaEquiparazione.Controls.Add(New LiteralControl("<td>" & email & "</td>"))
                    plTabellaEquiparazione.Controls.Add(New LiteralControl("<td>" & codiceFiscale & "</td>"))
                    plTabellaEquiparazione.Controls.Add(New LiteralControl("<td>" & tessera & "</td>"))


                    If foto = "..\img\noimg.jpg" Then
                        plTabellaEquiparazione.Controls.Add(New LiteralControl("<td><img src='" & foto & "' height='70' width='50' alt='" & nome & " " & cognome & "'></td>"))

                    Else
                        Dim myImage As Image = FotoS(foto)
                        Dim base64 As String = ImageHelper.ImageToBase64String(myImage, ImageFormat.Jpeg)
                        '  Response.Write("<img alt=""Embedded Image"" src=""data:image/Jpeg;base64," & base64 & """ />")
                        plTabellaEquiparazione.Controls.Add(New LiteralControl("<td><img src='data:image/Jpeg;base64," & base64 & "' height='70' width='50' alt='" & nome & " " & cognome & "'></td>"))

                    End If

                    If diploma = "..\img\noPdf.jpg" Then
                        plTabellaEquiparazione.Controls.Add(New LiteralControl("<td><img src='" & diploma & "' height='70' width='70' alt='" & nome & " " & cognome & "'></td>"))


                    Else
                        plTabellaEquiparazione.Controls.Add(New LiteralControl("<td>"))

                        plTabellaEquiparazione.Controls.Add(New LiteralControl("<a class=""btn btn-success btn-sm btn-due btn-custom"" onclick=""showToast('diploma');"" target=""_blank"" href='scaricaDiplomaEqui2.aspx?nominativo=" & deEnco.QueryStringEncode(cognome & "_" & nome) & "&codR=" & deEnco.QueryStringEncode(Data.FixNull(dr("IDrecord"))) & "&record_ID=" & deEnco.QueryStringEncode(recordid) & "&nomeFilePC=" & deEnco.QueryStringEncode(Data.FixNull(dr("NomeFileFotoFS"))) & "'>Diploma</a>"))

                        plTabellaEquiparazione.Controls.Add(New LiteralControl("</td>"))

                    End If


                    plTabellaEquiparazione.Controls.Add(New LiteralControl("</tr>"))
                Next




            End If
        Catch ex As Exception
            AsiModel.LogIn.LogErrori(ex, "vediDocumentazione", "equiparazioni")
            Response.Redirect("../FriendlyMessage.aspx", False)
        End Try
    End Sub
    Public Function FotoS(urlFoto As String)

        Dim pictureURL As String = urlFoto

        Dim wClient As WebClient = New WebClient()
        Dim nc As NetworkCredential = New NetworkCredential("enteweb", "web01")
        wClient.Credentials = nc
        Dim response As Stream = wClient.OpenRead(pictureURL)
        Dim temp = Image.FromStream(response)
        response.Close()
        Return temp

    End Function

    Protected Sub lnkTorna_Click(sender As Object, e As EventArgs) Handles lnkTorna.Click
        Session("AnnullaREqui") = Nothing
        Response.Redirect("dashboardEqui2.aspx?open=" & codR)
    End Sub
End Class