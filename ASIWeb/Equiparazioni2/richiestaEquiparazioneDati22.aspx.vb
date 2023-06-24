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
Imports System.Globalization

Public Class richiestaEquiparazioneDati22
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
    Dim codiceFiscale As String
    Dim MostraMonteOreFormazione As String
    Dim IdEquiparazione As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load





        ' Dim fase As String = Request.QueryString("fase")
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

        'If Session("auth") = "0" Or IsNothing(Session("auth")) Then
        '    Response.Redirect("../login.aspx")
        'End If

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
            '    Dim DettaglioEquiparazione As New DatiNuovaEquiparazione





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
            ' Dim MostraMonteOreFormazione As String = DettaglioEquiparazione.MostraMonteOreFormazione
            '  Dim TitoloCorso As String = DettaglioEquiparazione.TitoloCorso
            HiddenIdRecord.Value = DettaglioEquiparazione.IdRecord
            HiddenIDEquiparazione.Value = DettaglioEquiparazione.IDEquiparazione
            codiceFiscale = DettaglioEquiparazione.CodiceFiscale
            IDEquiparazione = DettaglioEquiparazione.IdEquiparazioneM
            ddlSport.Text = DettaglioEquiparazione.Sport
            ddlSpecialita.Text = DettaglioEquiparazione.Specialita
            ddlDisciplina.Text = DettaglioEquiparazione.Disciplina
            Dim datiCF As Object
            If Session("CFEE") = "EE" Then




                datiCF = AsiModel.getDatiCodiceFiscaleEE(Session("nomeEE"), Session("cognomeEE"), Session("codiceTesseraEE"), Session("dataNascitaEE"))
            Else


                datiCF = AsiModel.getDatiCodiceFiscale(Session("cf"))
                ' End If
            End If

            lblIntestazioneEquiparazione.Text =
                "<strong> - Codice Fiscale: </strong>" & datiCF.CodiceFiscale &
                "<strong> - Tessera Ass.: </strong>" & datiCF.CodiceTessera & "<br />" &
                "<strong> - Nominativo: </strong>" & datiCF.Nome & " " & datiCF.Cognome &
                "<strong> - Ente Richiedente: </strong>" & DescrizioneEnteRichiedente


        End If
        '   If fase = 4 Then
        '   lblnomef.Text = "Dati Anagrafici 3 caricati"


        '   End If

        If Not Page.IsPostBack Then

            'If MostraMonteOreFormazione = "1" Then

            '    pnlMonteOreFormazione.Visible = True


            'End If



            QualificheCorsi()
            LivelliCorsi()

        End If

    End Sub


    Sub LivelliCorsi()

        Dim ds As DataSet
        Try


            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            fmsP.SetLayout("webLivelliCorsi")
            Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.AllRecords)

            RequestP.AddSortField("Livello", Enumerations.Sort.Ascend)

            ds = RequestP.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then



                ddlLivello.DataSource = ds

                ddlLivello.DataTextField = "Livello"
                ddlLivello.DataValueField = "Livello"

                ddlLivello.DataBind()
                'ddlSport.DataValueField = "Sport"
                ddlLivello.Items.Insert(0, New ListItem("##", "##"))

                'ddlSpecialita.Items.Clear()
                'ddlDisciplina.Items.Clear()
                'Next
            End If

        Catch ex As Exception
            AsiModel.LogIn.LogErrori(ex, "richiestaEquiparazione22", "equiparazioni")
            Response.Redirect("../FriendlyMessage.aspx", False)
        End Try
    End Sub
    Protected Overloads Sub setFocus(ByVal ctrl As System.Web.UI.Control)
        Dim s As String = "<SCRIPT language='javascript'>document.getElementById('" + ctrl.ID + "').focus() </SCRIPT>"
        Me.ClientScript.RegisterStartupScript(GetType(String), "SetFocusScript", s)
    End Sub
    Public Sub SetFocusControl(ByVal ControlName As String)
        ' character 34 = "

        Dim script As String =
        "<script language=" + Chr(34) + "javascript" + Chr(34) _
        + ">" +
        " var control = document.getElementById(" + Chr(34) +
        ControlName + Chr(34) + ");" +
        " if( control != null ){control.focus();}" +
        "</script>"

        Page.ClientScript.RegisterStartupScript(GetType(String), "SetFocusScript", script)
    End Sub
    Sub QualificheCorsi()

        Dim ds As DataSet
        Try


            Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
            fmsP.SetLayout("webQualificheCorsi")
            Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.AllRecords)

            RequestP.AddSortField("Qualifica", Enumerations.Sort.Ascend)

            ds = RequestP.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then



                ddlQualifica.DataSource = ds

                ddlQualifica.DataTextField = "Qualifica"
                ddlQualifica.DataValueField = "Qualifica"

                ddlQualifica.DataBind()
                'ddlSport.DataValueField = "Sport"
                ddlQualifica.Items.Insert(0, New ListItem("##", "##"))

                'ddlSpecialita.Items.Clear()
                'ddlDisciplina.Items.Clear()
                'Next
            End If
        Catch ex As Exception
            AsiModel.LogIn.LogErrori(ex, "richiestaEquiparazione22", "equiparazioni")
            Response.Redirect("../FriendlyMessage.aspx", False)
        End Try

    End Sub












    Public Function CaricaDatiDocumentoCorso(codR As String, IDEquiparazione As String) As Boolean
        '  Dim litNumRichieste As Literal = DirectCast(ContentPlaceHolder1.FindControl("LitNumeroRichiesta"), Literal)


        ' Dim ds As DataSet

        Try


            Dim fmsP As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
            '  Dim ds As DataSet
            Dim risposta As String = ""
            fmsP.SetLayout("webEquiparazioniRichiestaMolti")
            Dim Request = fmsP.CreateEditRequest(IDEquiparazione)



            Request.AddField("Equi_Qualifica_Tecnica_Da_Rilasciare", ddlQualifica.SelectedItem.Text)
            Request.AddField("Equi_Livello", ddlLivello.SelectedItem.Text)
            Request.AddField("Equi_Fase", "2")
            If chkDaFederazione.Checked = True Then
                Request.AddField("Equi_DaFederazione", "si")
            End If
            Request.AddField("Codice_status", "101")
            'Request.AddScript("SistemaEncodingCorsoFase3", Session("id_record"))
            Session("visto") = "ok"



            '   Try
            risposta = Request.Execute()



            AsiModel.LogIn.LogCambioStatus(Session("IDEquiparazione"), "101", Session("WebUserEnte"), "equiparazione")

            Session("AnnullaREqui") = "newEqui"
            Response.Redirect("dashboardEqui2.aspx?ris=" & deEnco.QueryStringEncode("ok") & "&open=" & codR, False)
            Return True
        Catch ex As Exception
            AsiModel.LogIn.LogErrori(ex, "richiestaEquiparazione22", "equiparazioni")
            Response.Redirect("../FriendlyMessage.aspx", False)
        End Try






        'Catch ex As Exception

        'End Try




    End Function

    Function SistemaData(valore As String) As String

        Dim oDateDa As DateTime
        Dim giorno
        Dim anno
        Dim mese

        Dim miaData = valore
        oDateDa = DateTime.Parse(miaData)
        giorno = oDateDa.Day
        anno = oDateDa.Year
        mese = oDateDa.Month

        Return giorno & "/" & mese & "/" & anno


    End Function



    Protected Sub lnkButton1_Click(sender As Object, e As EventArgs) Handles lnkButton1.Click
        If Page.IsValid Then

            CaricaDatiDocumentoCorso(Session("IDEquiparazione"), Session("id_record"))

            '   Session("fase") = "4"
            ' Response.Redirect("richiestaEquiparazioneDati22.aspx?codR=" & deEnco.QueryStringEncode(Session("IDEquiparazione")) & "&record_ID=" & deEnco.QueryStringEncode(Session("id_record")))


            'Session("equiparazioneaggiunta") = "OK"
            'Response.Redirect("dashboardEqui2.aspx?ris=" & deEnco.QueryStringEncode("ok"))

        End If
    End Sub
End Class