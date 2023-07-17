Imports fmDotNet
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed
Public Class sceltaSport
    Inherits System.Web.UI.Page
    Dim deEnco As New Ed()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If
        If IsNothing(Session("codice")) Then
            Response.Redirect("../login.aspx")
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
                RequestP.AddSearchField("Settore_ID", Session("codice"), Enumerations.SearchOption.equals)

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

        End If
    End Sub
    Protected Sub ddlSport_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSport.SelectedIndexChanged



        Dim selezionato As String = ddlSport.SelectedItem.Value
        Dim ds1 As DataSet
        Try


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

        Catch ex As Exception
            AsiModel.LogIn.LogErrori(ex, "sceltaSport", "equiparazioni")
            Response.Redirect("../FriendlyMessage.aspx", False)
        End Try

    End Sub

    Protected Sub ddlDisciplina_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDisciplina.SelectedIndexChanged


        Dim selezionato As String = ddlDisciplina.SelectedItem.Value
        Dim ds2 As DataSet
        Dim dr As DataRow
        Try


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
        Catch ex As Exception
            AsiModel.LogIn.LogErrori(ex, "sceltaSport", "equiparazioni")
            Response.Redirect("../FriendlyMessage.aspx", False)
        End Try
    End Sub

    Protected Sub CustomValidator1_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles CustomValidator1.ServerValidate
        If ddlSpecialita.SelectedItem.Text = "ND" Or Not String.IsNullOrEmpty(ddlSpecialita.SelectedItem.Text) Or Not String.IsNullOrWhiteSpace(ddlSpecialita.SelectedItem.Text) Then
            args.IsValid = True
        Else


            args.IsValid = False


        End If
    End Sub

    Protected Sub lnkButton1_Click(sender As Object, e As EventArgs) Handles lnkButton1.Click
        If Page.IsValid Then
            Dim idEquiparazioneM As Integer
            Dim idRecordM As Integer
            idEquiparazioneM = AsiModel.Equiparazione.NuovaEquiparazioneFolder(Session("codice"),
               ddlSport.SelectedItem.Value, ddlDisciplina.SelectedItem.Value, ddlSpecialita.SelectedItem.Value)
            Response.Redirect("sceltaCheck.aspx?codR=" & deEnco.QueryStringEncode(idEquiparazioneM))
            '    Response.Redirect("checkTesseramento2.aspx?codR=" & deEnco.QueryStringEncode(idEquiparazioneM))


        End If
    End Sub
End Class