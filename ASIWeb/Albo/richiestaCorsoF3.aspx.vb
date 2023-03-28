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
Imports System.Threading
Imports DocumentFormat.OpenXml.Office2010.ExcelAc

Public Class richiestaCorsoF3
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
    Dim MostraMonteOreFormazione As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim fase As String = Request.QueryString("fase")
        If Not String.IsNullOrEmpty(fase) Then
            fase = deEnco.QueryStringDecode(Request.QueryString("fase"))
            Session("fase") = fase
        End If





        If Session("fase") <> "3" Then
            Response.Redirect("../homeA.aspx")
        End If

        If IsNothing(Session("tipoEnte")) Then
            Response.Redirect("../login.aspx")
        End If


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
            Dim indirizzoSvolgimento As String = DettaglioCorso.IndirizzoSvolgimento & " - " & DettaglioCorso.LocalitaSvolgimento _
                & DettaglioCorso.CapSvolgimento & " - " & DettaglioCorso.PRSvolgimento & " - " & DettaglioCorso.RegioneSvolgimento
            Dim DataCorsoDa As String = Left(DettaglioCorso.SvolgimentoDataDa, 10)
            Dim DataCorsoA As String = Left(DettaglioCorso.SvolgimentoDataA, 10)
            Dim OraCorsoDa As String = DettaglioCorso.OraSvolgimentoDa
            Dim OraCorsoA As String = DettaglioCorso.OraSvolgimentoA
            Dim OreCorso As String = DettaglioCorso.TotaleOre
            Dim DataEmissione As String = Left(DettaglioCorso.DataEmissione, 10)
            MostraMonteOreFormazione = DettaglioCorso.MostraMonteOreFormazione
            HiddenIdRecord.Value = DettaglioCorso.IdRecord

            lblIntestazioneCorso.Text = "<strong>ID Corso: </strong>" & IDCorso & "<strong> - Ente Richiedente: </strong>" & DescrizioneEnteRichiedente & "<br />" &
                 "<strong>Indirizzo: </strong>" & indirizzoSvolgimento & "<br />"
            If Not String.IsNullOrEmpty(DataEmissione) Then
                lblIntestazioneCorso.Text &= "<strong>Data Emissione: </strong>" & DataEmissione & " - "
            End If
            lblIntestazioneCorso.Text &= "<strong>Data Inizio: </strong>" & DataCorsoDa & " <strong>Data Fine: </strong>" & DataCorsoA &
                 " <strong>Ore: </strong>" & OreCorso

        End If



        If fase = 3 Then
            lblnomef.Text = "Dati Fase 2 precedentemente caricati"
        Else
            lblnomef.Text = "Dati Fase 2 caricati correttamente"

        End If
        If Not Page.IsPostBack Then

        End If

        If Not Page.IsPostBack Then

            If MostraMonteOreFormazione = 1 Then
                pnlMonteOreFormazione.Visible = True
                RangeMonteore.Visible = True
            End If

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
    <WebMethod()>
    Public Shared Function SearchCustomers(ByVal prefixText As String, ByVal count As Integer) As List(Of String)



        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        Dim ds As DataSet

        fmsP.SetLayout("webFormatori")

        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        RequestP.AddSearchField("Prime4Lettere", prefixText, Enumerations.SearchOption.beginsWith)
        RequestP.AddSortField("NominativoControllo")
        ds = RequestP.Execute()

        Dim customers As List(Of String) = New List(Of String)()
        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
            For Each dr In ds.Tables("main").Rows

                customers.Add(dr("Cognome").ToString() & " " & dr("nome").ToString())

            Next




        End If



        Return customers



    End Function
    Function verificaCognome(valore As String) As Boolean
        Dim ritorno As Boolean = False


        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        Dim ds As DataSet

        fmsP.SetLayout("webFormatori")

        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        RequestP.AddSearchField("NominativoControllo", valore, Enumerations.SearchOption.equals)

        ds = RequestP.Execute()


        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
            For Each dr In ds.Tables("main").Rows
                If dr("NominativoControllo") = valore Then
                    ritorno = True
                Else
                    ritorno = False


                End If



            Next




        End If

        Return ritorno







    End Function


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
    Sub AddButton_Click1(ByVal sender As Object, ByVal e As EventArgs) Handles btnAggiungiDocenti.Click
        '  System.Threading.Thread.Sleep(3000)
        Dim vero As Boolean = False
        vero = verificaCognome(txtDocenteCognome.Text)
        If vero = True Then
            lblAvviso.Text = ""




            Dim Users As ListItemCollection = New ListItemCollection
            Dim User As ListItem
            If Len(txtDocenteCognome.Text) > 0 And txtDocenteCognome.Text <> "cognome" Then

                Users.Add(Trim(txtDocenteCognome.Text))
                SetFocusControl("txtDocenteNome")
            End If
                For Each User In Users
                '    txtDocenteNome.Text = ""
                txtDocenteCognome.Text = ""
                '    Dim alreadyExist As Boolean = Users.Contains(User)
                If Not lstDocenti.Items.Contains(User) Then
                    lstDocenti.Items.Add(User)
                Else
                    txtDocenteCognome.Text = User.ToString
                    lblAvviso.Text = "nominativo già selezionato"
                End If




            Next
        Else

            lblAvviso.Text = "Nominativo non valido!!"
            '  Thread.Sleep(4000)


            '   lblAvviso.Text = "Nominativo non abilitato"

            '    lblAvviso.Text = ""



        End If
    End Sub
    Sub AddButton_Click2(ByVal sender As Object, ByVal e As EventArgs) Handles btnAggiungiComponente.Click
        '  System.Threading.Thread.Sleep(3000)
        Dim Users As ListItemCollection = New ListItemCollection
        Dim User As ListItem
        lblAvviso2.Text = ""
        If Len(txtCommissioneNome.Text) > 0 And Len(txtCommissioneCognome.Text) > 0 And txtCommissioneNome.Text <> "nome" And txtCommissioneCognome.Text <> "cognome" Then
            Users.Add(txtCommissioneNome.Text & " " & txtCommissioneCognome.Text)
            SetFocusControl("txtCommissioneNome")
        End If
        For Each User In Users


            If Not lstComponenti.Items.Contains(User) Then
                lstComponenti.Items.Add(User)
                txtCommissioneNome.Text = ""
                txtCommissioneCognome.Text = ""
            Else

                lblAvviso2.Text = "nominativo già selezionato"
            End If



        Next
    End Sub

    Sub RemoveButton_Click1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTogliDocenti.Click
        Dim Users As ListItemCollection = New ListItemCollection
        Dim User As ListItem
        For Each User In lstDocenti.Items
            If User.Selected Then
                Users.Add(User)
                SetFocusControl("txtDocenteNome")
            End If
        Next
        For Each User In Users
            lstDocenti.Items.Remove(User)
        Next
    End Sub
    Sub RemoveButton_Click2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRimuoviComponente.Click
        Dim Users As ListItemCollection = New ListItemCollection
        Dim User As ListItem
        For Each User In lstComponenti.Items
            If User.Selected Then
                Users.Add(User)
                SetFocusControl("txtCommissioneNome")
            End If
        Next
        For Each User In Users
            lstComponenti.Items.Remove(User)
        Next
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

    Protected Sub btnFase3_Click(sender As Object, e As EventArgs) Handles btnFase3.Click
        If Page.IsValid Then
            If Session("auth") = "0" Or IsNothing(Session("auth")) Then
                Response.Redirect("../login.aspx")
            End If


            Dim fmsP As FMSAxml = ASIWeb.AsiModel.Conn.Connect()
            '  Dim ds As DataSet
            Dim risposta As String = ""
            fmsP.SetLayout("webCorsiRichiesta")
            Dim Request = fmsP.CreateEditRequest(Session("id_record"))
            Dim valDocenti As String = ""
            Dim ItemDocente As ListItem
            Dim valCommissione As String = ""
            Dim ItemCommissione As ListItem


            If lstDocenti.Items.Count = 0 Then
                Request.AddField("Elenco_Docenti", "nd")
            Else
                For Each ItemDocente In lstDocenti.Items
                    valDocenti &= ItemDocente.Text.ToString & ", "
                Next
                Request.AddField("Elenco_Docenti", Data.PrendiStringaT(Server.HtmlEncode(valDocenti)))
            End If


            If lstComponenti.Items.Count = 0 Then
                Request.AddField("Elenco_Componenti_Commissione", "nd")
            Else
                For Each ItemCommissione In lstComponenti.Items
                    valCommissione &= ItemCommissione.Text.ToString & ","
                Next
                Request.AddField("Elenco_Componenti_Commissione", Data.PrendiStringaT(Server.HtmlEncode(valCommissione)))
            End If


            Request.AddField("Sport_Interessato", ddlSport.SelectedItem.Text)
            Request.AddField("Sport_Interessato_ID", ddlSport.SelectedItem.Value)
            Request.AddField("Disciplina_Interessata", ddlDisciplina.SelectedItem.Text)
            Request.AddField("Disciplina_Interessata_ID", ddlDisciplina.SelectedItem.Value)
            Request.AddField("Specialita", ddlSpecialita.SelectedItem.Text)
            Request.AddField("Specialita_ID", ddlSpecialita.SelectedItem.Value)
            Request.AddField("Qualifica_Tecnica_Da_Rilasciare", ddlQualifica.SelectedItem.Text)
            Request.AddField("Livello", ddlLivello.SelectedItem.Text)
            Request.AddField("Quota_Partecipazione", String.Format("{0:C2}", txtQuota.Text.ToString))

            Request.AddField("MonteOreFormazione", txtMonteOreFormazione.Text)
            'Request.AddField("Settore_Approvazione", Data.PrendiStringaT(Server.HtmlEncode(txtSettoreApprovante.Text)))
            Request.AddField("Fase", "3")
            Request.AddField("Codice_status", "54")
            ' Request.AddScript("SistemaEncodingCorsoFase3", Session("id_record"))
            ' Request.AddScript("PreparaMailInvioDT", Session("IDCorso"))
            Request.AddScript("RunWebMailInvioDT", Session("IDCorso"))

            Session("visto") = "ok"
            Try

                Request.Execute()
                AsiModel.LogIn.LogCambioStatus(Session("IDCorso"), "54", Session("WebUserEnte"), "corso")
                Session("corsoaggiunto") = "OK"
                Response.Redirect("dashboardB.aspx?ris=" & deEnco.QueryStringEncode("ok"))

                '   Response.Redirect("dashboardB.aspx?ris=" & deEnco.QueryStringEncode("ko"))



            Catch ex As Exception

            End Try





        End If
    End Sub

    Protected Sub cvDocenti_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles cvDocenti.ServerValidate

        If lstDocenti.Items.Count = 0 Then
            args.IsValid = False
        Else
            args.IsValid = True
        End If
    End Sub

    Protected Sub cvComponenti_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles cvComponenti.ServerValidate
        If lstComponenti.Items.Count = 0 Then
            args.IsValid = False
        Else
            args.IsValid = True
        End If
    End Sub

    Protected Sub CustomValidator1_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles CustomValidator1.ServerValidate
        If ddlSpecialita.SelectedItem.Text = "ND" Or Not String.IsNullOrEmpty(ddlSpecialita.SelectedItem.Text) Or Not String.IsNullOrWhiteSpace(ddlSpecialita.SelectedItem.Text) Then
            args.IsValid = True
        Else


            args.IsValid = False


        End If
    End Sub

    Protected Sub CustomValidator2_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles CustomValidator2.ServerValidate

        If IsNumeric(txtQuota.Text) Then

            If txtQuota.Text >= 1 Then
                args.IsValid = True
            Else
                args.IsValid = False
            End If
        Else
            args.IsValid = False

        End If

    End Sub
End Class