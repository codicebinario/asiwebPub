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

Public Class corsistiKO
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
    Dim pagine As Integer

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

        If Not Page.IsPostBack Then
            pagine = AsiModel.GetPaging()
        End If


        Dim record_ID As String = ""
        record_ID = deEnco.QueryStringDecode(Request.QueryString("record_ID"))
        Dim oldStatus As String
        oldStatus = deEnco.QueryStringDecode(Request.QueryString("oldStatus"))
        Dim skip As Integer = 0
        skip = Request.QueryString("skip")
        If Not String.IsNullOrEmpty(record_ID) Then

            Session("id_record") = record_ID

        End If

        If Not String.IsNullOrEmpty(oldStatus) Then

            Session("oldStatus") = oldStatus

        End If



        'pagg = Request.QueryString("pag")
        Dim pag = Request.QueryString("pag")

        If String.IsNullOrEmpty(pag) Then
            pag = 1


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
            Dim TitoloCorso As String = DettaglioCorso.TitoloCorso
            HiddenIdRecord.Value = DettaglioCorso.IdRecord
            HiddenIDCorso.Value = DettaglioCorso.IDCorso
            lblIntestazioneCorso.Text = "<strong>ID Corso: </strong>" & IDCorso & " - " & TitoloCorso & "<strong> - Ente Richiedente: </strong>" & DescrizioneEnteRichiedente
        End If

        If Not Page.IsPostBack Then
            Dim esistonoCorsisti As Boolean
            esistonoCorsisti = AsiModel.Corso.QuantiCorsisti(codR)
            '  pnlFase1.Visible = False

            If esistonoCorsisti = True Then
                caricaCorsisti(pag, record_ID, codR, skip, pagine)
            End If



        End If

        If Not IsPostBack Then
            '    BindRepeater()
        End If
    End Sub

    Sub caricaCorsisti(pag As Integer, record_ID As String, codR As String, skip As Integer, pagine As Integer)

        If Not String.IsNullOrEmpty(skip) Then
        Else
            skip = 0
        End If






        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        Dim ds As DataSet
        Dim dsTot As DataSet
        Dim nome As String
        Dim cognome As String
        Dim foto As String
        Dim email As String
        Dim codiceFiscale As String
        Dim NumeroTesseraAsi As String
        Dim IndirizzoSpedizione As String
        Dim ProvinciaSpedizione As String
        Dim ComuneSpedizione As String
        Dim CapSpedizione As String
        Dim MotivoKO As String
        Dim id As Integer
        Dim quantiTot As Integer = 0
        Dim quantePagine As Decimal = 0
        Dim pagina As Integer = 0
        fmsP.SetLayout("webCorsisti")

        Dim RequestPTot = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        RequestPTot.AddSearchField("IDCorso", Session("IDCorso"), Enumerations.SearchOption.equals)
        RequestPTot.AddSearchField("Corsista_OK_KO", "KO", Enumerations.SearchOption.equals)
        dsTot = RequestPTot.Execute()

        If Not IsNothing(dsTot) AndAlso dsTot.Tables("main").Rows.Count > 0 Then

            quantiTot = dsTot.Tables("main").Rows.Count

            'If quantiTot <= pagine Then
            '    pagine = quantiTot
            'End If
            quantePagine = Math.Ceiling(quantiTot / pagine)


        End If



        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        RequestP.AddSearchField("IDCorso", Session("IDCorso"), Enumerations.SearchOption.equals)
        RequestP.AddSearchField("Corsista_OK_KO", "KO", Enumerations.SearchOption.equals)
        'fmsP.GetPictureReference("", ""

        ds = RequestP.Execute()


        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

            Dim MyDataPage As IEnumerable(Of DataRow) = ds.Tables("main").AsEnumerable().Skip(skip).Take(pagine)


            Dim counter As Integer = 0
            Dim ChePagina As Integer = 0

            For Each dr In MyDataPage
                counter += 1


                nome = Data.FixNull(dr("Nome"))
                cognome = Data.FixNull(dr("Cognome"))
                email = Data.FixNull(dr("email"))
                codiceFiscale = Data.FixNull(dr("CodiceFiscale"))
                IndirizzoSpedizione = Data.FixNull(dr("IndirizzoSpedizione"))
                CapSpedizione = Data.FixNull(dr("CapSpedizione"))
                ComuneSpedizione = Data.FixNull(dr("ComuneSpedizione"))
                ProvinciaSpedizione = Data.FixNull(dr("ProvinciaSpedizione"))
                NumeroTesseraAsi = Data.FixNull(dr("NumeroTesseraASI"))
                MotivoKO = Data.FixNull(dr("MotivazioneKO"))
                id = dr("idcorsista")

                'If String.IsNullOrWhiteSpace(Data.FixNull(dr("foto"))) Then
                '    foto = "..\img\noimg.jpg"
                'Else
                '    foto = "https://93.63.195.98" & Data.FixNull(dr("foto"))
                'End If


                plTabellaCorsisti.Controls.Add(New LiteralControl("<tr>"))
                'plTabellaCorsisti.Controls.Add(New LiteralControl("<th scope=""row"">" & counter & "</td>"))
                plTabellaCorsisti.Controls.Add(New LiteralControl("<td>" & nome & "</td>"))
                plTabellaCorsisti.Controls.Add(New LiteralControl("<td>" & cognome & "</td>"))
                plTabellaCorsisti.Controls.Add(New LiteralControl("<td>" & email & "</td>"))
                plTabellaCorsisti.Controls.Add(New LiteralControl("<td>" & codiceFiscale & "</td>"))

                plTabellaCorsisti.Controls.Add(New LiteralControl("<td>" & NumeroTesseraAsi & "</td>"))
                plTabellaCorsisti.Controls.Add(New LiteralControl("<td>" & IndirizzoSpedizione & " " & CapSpedizione &
                                                                    "<br /> " & ComuneSpedizione & " " & ProvinciaSpedizione & "</td>"))

                plTabellaCorsisti.Controls.Add(New LiteralControl("<td><span class=""colorerosso"">" & MotivoKO & "</span></td>"))




                'If foto = "..\img\noimg.jpg" Then
                '    plTabellaCorsisti.Controls.Add(New LiteralControl("<td><img src='" & foto & "' height='70' width='50' alt='" & nome & " " & cognome & "'></td>"))

                'Else
                '    Dim myImage As Image = FotoS(foto)
                '    Dim base64 As String = ImageHelper.ImageToBase64String(myImage, ImageFormat.Jpeg)
                '    '  Response.Write("<img alt=""Embedded Image"" src=""data:image/Jpeg;base64," & base64 & """ />")
                '    plTabellaCorsisti.Controls.Add(New LiteralControl("<td><img src='data:image/Jpeg;base64," & base64 & "' height='70' width='50' alt='" & nome & " " & cognome & "'></td>"))

                'End If
                '  plTabellaCorsisti.Controls.Add(New LiteralControl("<td><a href='upCorsistaKO.aspx?skip=" & skip & "&pag=" & pag & "&codR=" & deEnco.QueryStringEncode(Session("IDCorso")) & "&id=" & deEnco.QueryStringEncode(id) & "'>Carica Foto</a></td>"))


                plTabellaCorsisti.Controls.Add(New LiteralControl("</tr>"))
            Next

            'If pag = 1 Then
            '    pag = 1
            'ElseMoret
            '    pag += 1

            'End If




            plTabellaCorsisti.Controls.Add(New LiteralControl("<tfoot>"))

            plTabellaCorsisti.Controls.Add(New LiteralControl("<tr>"))

            plTabellaCorsisti.Controls.Add(New LiteralControl("<td colspan='6'>pagina " & pag & " / di " & quantePagine & " - " & quantiTot & " corsisti</td>"))

            plTabellaCorsisti.Controls.Add(New LiteralControl("</tr>"))

            plTabellaCorsisti.Controls.Add(New LiteralControl("<tr>"))

            If skip = 0 Then
                plTabellaCorsisti.Controls.Add(New LiteralControl("<td>---</td>"))
            Else

                plTabellaCorsisti.Controls.Add(New LiteralControl("<td><a class=""link-primary"" href='corsistiKO.aspx?pag=" & pag - 1 & "&codR=" & deEnco.QueryStringEncode(codR) & "&record_ID=" & deEnco.QueryStringEncode(record_ID) & "&skip=" & skip - pagine & "'>indietro</a></td>"))
            End If
            If pagiAvanti(skip + pagine, quantiTot) = False Then
                plTabellaCorsisti.Controls.Add(New LiteralControl("<td>---</td>"))
            Else
                plTabellaCorsisti.Controls.Add(New LiteralControl("<td><a class=""link-primary"" href='corsistiKO.aspx?pag=" & pag + 1 & "&codR=" & deEnco.QueryStringEncode(codR) & "&record_ID=" & deEnco.QueryStringEncode(record_ID) & "&skip=" & skip + pagine & "'>avanti</a></td>"))

            End If
            plTabellaCorsisti.Controls.Add(New LiteralControl("<td></td>"))
            plTabellaCorsisti.Controls.Add(New LiteralControl("<td></td>"))
            plTabellaCorsisti.Controls.Add(New LiteralControl("</tr>"))

            plTabellaCorsisti.Controls.Add(New LiteralControl("</tfoot>"))

        End If
    End Sub
    Function pagiIndietro(numero As Integer, quantiTot As Integer) As Boolean
        Dim stoppa As Boolean = True
        If numero < quantiTot Then

            stoppa = False

        End If
        Return stoppa
    End Function
    Function pagiAvanti(numero As Integer, quantiTot As Integer) As Boolean
        Dim stoppa As Boolean = True
        If numero >= quantiTot Then

            stoppa = False

        End If
        Return stoppa
    End Function

    Public Function FotoS(urlFoto As String)

        ' Dim pictureURL As String = "https://93.63.195.98/fmi/xml/cnt/145_ZGQ5YmRiOTYtZjc0YS00NWFmLTgyNTAtZTIyMjRjYjgzYzg0.jpg?-db=Asi&-lay=webCorsisti&-recid=145&-field=Foto(1)"
        Dim pictureURL As String = urlFoto

        Dim wClient As WebClient = New WebClient()
        Dim nc As NetworkCredential = New NetworkCredential("enteweb", "web01")
        wClient.Credentials = nc
        Dim response As Stream = wClient.OpenRead(pictureURL)
        Dim temp = Image.FromStream(response)
        response.Close()
        Return temp

    End Function

    Protected Sub lnkNuovoExcel_Click(sender As Object, e As EventArgs) Handles lnkNuovoExcel.Click
        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If
        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()

        fmsP.SetLayout("webCorsiRichiesta")

        Dim RequestP = fmsP.CreateEditRequest(Session("IDCorso"))
        RequestP.AddField("Codice_Status", Session("oldStatus"))

        '   Try
        RequestP.Execute()
            AsiModel.LogIn.LogCambioStatus(Session("IDCorso"), Session("oldStatus"), Session("WebUserEnte"), "corso")
        '   AsiModel.LogIn.LogCambioStatus(CodiceRichiesta, "10", Session("WebUserEnte"))
        '  Session("annullaCorso") = "ok"

        '     Catch ex As Exception

        '  End Try
        Response.Redirect("dashboardB.aspx#" & Session("IDCorso"))
    End Sub

    Protected Sub lnkDashboardTorna_Click(sender As Object, e As EventArgs) Handles lnkDashboardTorna.Click
        Response.Redirect("dashboardB.aspx#" & Session("IDCorso"))
    End Sub
End Class