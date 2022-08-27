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

Public Class richiestaEquiparazioneDati2
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim fase As String = Request.QueryString("fase")
        If Not String.IsNullOrEmpty(fase) Then
            '   fase = deEnco.QueryStringDecode(Request.QueryString("fase"))
            Session("fase") = fase
        End If


        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If
        If IsNothing(Session("codice")) Then
            Response.Redirect("../login.aspx")
        End If

        If IsNothing(Session("codiceFiscale")) Then
            Response.Redirect("../login.aspx")
        End If
        If Session("procedi") <> "OK" Then

            Response.Redirect("checkTesseramento.aspx")

        End If

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
            Dim DescrizioneStatus As String = DettaglioEquiparazione.DescrizioneStatus
            '  Dim TitoloCorso As String = DettaglioEquiparazione.TitoloCorso
            HiddenIdRecord.Value = DettaglioEquiparazione.IdRecord
            HiddenIDEquiparazione.Value = DettaglioEquiparazione.IDEquiparazione
            Dim datiCF = AsiModel.getDatiCodiceFiscale(Session("codiceFiscale"))

            lblIntestazioneEquiparazione.Text = "<strong>ID Equiparazione: </strong>" & IDEquiparazione &
                "<strong> - Codice Fiscale: </strong>" & datiCF.CodiceFiscale &
                "<strong> - N.Tessera: </strong>" & datiCF.CodiceTessera & "<br />" &
                "<strong> - Nominativo: </strong>" & datiCF.Nome & " " & datiCF.Cognome &
                "<strong> - Ente Richiedente: </strong>" & DescrizioneEnteRichiedente

        End If
        If fase = 4 Then
            lblnomef.Text = "Dati Anagrafici 3 caricati"


        End If
        If Not Page.IsPostBack Then

            '  pnlFase1.Visible = False


        End If
        If Not Page.IsPostBack Then

            If Session("tipoEnte") <> "Settori" Then

                Dim ds As DataSet

                Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
                fmsP.SetLayout("webSportDNet")
                Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.AllRecords)

                RequestP.AddSortField("Sport", Enumerations.Sort.Ascend)

                ds = RequestP.Execute()

                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

                    Dim SingleSport As DataTable = ds.Tables("main").DefaultView.ToTable(True, "Sport_ID", "Sport")

                    ddlSport.DataSource = SingleSport

                    ddlSport.DataTextField = "Sport"
                    ddlSport.DataValueField = "Sport_ID"

                    ddlSport.DataBind()

                    ddlSport.Items.Insert(0, New ListItem("##", "##"))


                End If


            ElseIf Session("tipoEnte") = "Settori" Then
                Dim ds As DataSet

                Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
                fmsP.SetLayout("webDisciplineSportSettoriDNet")
                Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
                RequestP.AddSearchField("Settore_Richiedente", Session("denominazione"), Enumerations.SearchOption.equals)

                RequestP.AddSortField("Sport", Enumerations.Sort.Ascend)

                ds = RequestP.Execute()

                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

                    Dim SingleSport As DataTable = ds.Tables("main").DefaultView.ToTable(True, "Sport_ID", "Sport")

                    ddlSport.DataSource = SingleSport

                    ddlSport.DataTextField = "Sport"
                    ddlSport.DataValueField = "Sport_ID"

                    ddlSport.DataBind()

                    ddlSport.Items.Insert(0, New ListItem("##", "##"))


                End If

            End If


            QualificheCorsi()
            LivelliCorsi()

        End If

    End Sub


    Sub LivelliCorsi()

        Dim ds As DataSet

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


    End Sub


    Protected Sub ddlSport_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSport.SelectedIndexChanged



        Dim selezionato As String = ddlSport.SelectedItem.Value
        Dim ds1 As DataSet

        Dim fmsP1 As FMSAxml = AsiModel.Conn.Connect()
        fmsP1.SetLayout("webDisciplineSportSettoriDNet")

        '

        If Session("tipoEnte") = "Settori" Then

            Dim RequestP1 = fmsP1.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestP1.AddSearchField("Sport_ID", selezionato, Enumerations.SearchOption.equals)
            ' RequestP1.AddSearchField("Settore_ID", 0, Enumerations.SearchOption.biggerThan)
            RequestP1.AddSortField("Disciplina_ID", Enumerations.Sort.Ascend)

            ds1 = RequestP1.Execute()


            If Not IsNothing(ds1) AndAlso ds1.Tables("main").Rows.Count > 0 Then


                Dim Disciplina As DataTable = ds1.Tables("main").DefaultView.ToTable(True, "Disciplina_ID", "Disciplina")

                ddlDisciplina.DataSource = Disciplina

                ddlDisciplina.DataTextField = "Disciplina"
                ddlDisciplina.DataValueField = "Disciplina_ID"

                ddlDisciplina.DataBind()
                ddlDisciplina.Items.Remove(0)
                ddlDisciplina.Items.Insert(0, New ListItem("##", "##"))
                ddlSpecialita.Items.Clear()

            End If
        Else

            Dim RequestP1 = fmsP1.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestP1.AddSearchField("Sport_ID", selezionato, Enumerations.SearchOption.equals)

            RequestP1.AddSortField("Disciplina_ID", Enumerations.Sort.Ascend)

            ds1 = RequestP1.Execute()

            If Not IsNothing(ds1) AndAlso ds1.Tables("main").Rows.Count > 0 Then


                Dim Disciplina As DataTable = ds1.Tables("main").DefaultView.ToTable(True, "Disciplina_ID", "Disciplina")

                ddlDisciplina.DataSource = Disciplina

                ddlDisciplina.DataTextField = "Disciplina"
                ddlDisciplina.DataValueField = "Disciplina_ID"

                ddlDisciplina.DataBind()
                ddlDisciplina.Items.Remove(0)
                ddlDisciplina.Items.Insert(0, New ListItem("##", "##"))
                ddlSpecialita.Items.Clear()

            End If

        End If



    End Sub

    Protected Sub ddlDisciplina_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDisciplina.SelectedIndexChanged


        Dim selezionato As String = ddlDisciplina.SelectedItem.Value
        Dim ds2 As DataSet
        Dim dr As DataRow
        Dim fmsP2 As FMSAxml = AsiModel.Conn.Connect()
        fmsP2.SetLayout("webCorsiSport")

        If Session("tipoEnte") = "Settori" Then

            Dim RequestP2 = fmsP2.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestP2.AddSearchField("Disciplina_ID", selezionato, Enumerations.SearchOption.equals)

            RequestP2.AddSortField("Specialita_ID", Enumerations.Sort.Ascend)

            ds2 = RequestP2.Execute()

            If Not IsNothing(ds2) AndAlso ds2.Tables("main").Rows.Count > 0 Then

                Dim Specialita As DataTable = ds2.Tables("main").DefaultView.ToTable(True, "Specialita_ID", "Specialita")
                ddlSpecialita.Enabled = True
                ddlSpecialita.DataSource = Specialita

                ddlSpecialita.DataTextField = "Specialita"
                ddlSpecialita.DataValueField = "Specialita_ID"

                ddlSpecialita.DataBind()
                ' ddlSpecialita.Items.Insert(0, New ListItem("##", "##"))
                ddlSpecialita.Items.Insert(0, New ListItem("##", "##"))
            Else
                ddlSpecialita.Enabled = False
            End If

            CustomValidator1.Enabled = True



        Else
            Dim RequestP2 = fmsP2.CreateFindRequest(Enumerations.SearchType.Subset)
            RequestP2.AddSearchField("Disciplina_ID", selezionato, Enumerations.SearchOption.equals)
            RequestP2.AddSortField("Specialita_ID", Enumerations.Sort.Ascend)

            ds2 = RequestP2.Execute()

            If Not IsNothing(ds2) AndAlso ds2.Tables("main").Rows.Count > 0 Then

                Dim Specialita As DataTable = ds2.Tables("main").DefaultView.ToTable(True, "Specialita_ID", "Specialita")
                ddlSpecialita.Enabled = True
                ddlSpecialita.DataSource = Specialita

                ddlSpecialita.DataTextField = "Specialita"
                ddlSpecialita.DataValueField = "Specialita_ID"

                ddlSpecialita.DataBind()
                ' ddlSpecialita.Items.Insert(0, New ListItem("##", "##"))
                ddlSpecialita.Items.Insert(0, New ListItem("##", "##"))
            Else
                ddlSpecialita.Enabled = False
            End If





        End If
    End Sub





    Protected Sub btnFase4_Click(sender As Object, e As EventArgs) Handles btnFase4.Click
        If Page.IsValid Then

            CaricaDatiDocumentoCorso(Session("IDEquiparazione"), Session("id_record"))

            Session("fase") = "4"
            Response.Redirect("richiestaEquiparazioneDati2.aspx?codR=" & deEnco.QueryStringEncode(Session("IDEquiparazione")) & "&record_ID=" & deEnco.QueryStringEncode(Session("id_record")))


            Session("equiparazioneaggiunta") = "OK"
            Response.Redirect("dashboardE.aspx?ris=" & deEnco.QueryStringEncode("ok"))

        End If
    End Sub

    Public Function CaricaDatiDocumentoCorso(codR As String, IDEquiparazione As String) As Boolean
        '  Dim litNumRichieste As Literal = DirectCast(ContentPlaceHolder1.FindControl("LitNumeroRichiesta"), Literal)


        ' Dim ds As DataSet


        Dim fmsP As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
        '  Dim ds As DataSet
        Dim risposta As String = ""
        fmsP.SetLayout("webEquiparazioniRichiesta")
        Dim Request = fmsP.CreateEditRequest(IDEquiparazione)


        Request.AddField("Equi_Sport_Interessato", ddlSport.SelectedItem.Text)
        Request.AddField("Equi_Sport_Interessato_ID", ddlSport.SelectedItem.Value)
        Request.AddField("Equi_Disciplina_Interessata", ddlDisciplina.SelectedItem.Text)
        Request.AddField("Equi_Disciplina_Interessata_ID", ddlDisciplina.SelectedItem.Value)
        Request.AddField("Equi_Specialita", ddlSpecialita.SelectedItem.Text)
        Request.AddField("Equi_Specialita_ID", ddlSpecialita.SelectedItem.Value)
        Request.AddField("Equi_Qualifica_Tecnica_Da_Rilasciare", ddlQualifica.SelectedItem.Text)
        Request.AddField("Equi_Livello", ddlLivello.SelectedItem.Text)
        Request.AddField("Codice_status", "102")
        'Request.AddScript("SistemaEncodingCorsoFase3", Session("id_record"))
        Session("visto") = "ok"


        Request.AddField("Equi_Fase", "4")
        '    Request.AddScript("SistemaEncodingCorsoFase2", IDCorso)

        '   Try
        risposta = Request.Execute()


        AsiModel.LogIn.LogCambioStatus(Session("IDEquiparazione"), "102", Session("WebUserEnte"), "equiparazioni")
        Response.Redirect("dashboardEqui.aspx?ris=" & deEnco.QueryStringEncode("ok"))
        'Catch ex As Exception

        'End Try



        Return True
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

        Return mese & "/" & giorno & "/" & anno


    End Function

    Protected Sub CustomValidator1_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles CustomValidator1.ServerValidate
        If ddlSpecialita.SelectedItem.Text = "ND" Or Not String.IsNullOrEmpty(ddlSpecialita.SelectedItem.Text) Or Not String.IsNullOrWhiteSpace(ddlSpecialita.SelectedItem.Text) Then
            args.IsValid = True
        Else


            args.IsValid = False


        End If
    End Sub

End Class