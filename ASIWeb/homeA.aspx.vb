Imports fmDotNet
Imports ASIWeb.AsiModel
Public Class homeA
    Inherits System.Web.UI.Page
    Dim quantiCorsiAttivi As Integer = 0
    Dim quanteEquiAttive As Integer = 0
    Dim quantiRinnoviAttivi As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load




        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("login.aspx")
        End If
        If IsNothing(Session("codice")) Then
            Response.Redirect("login.aspx")
        End If

        If Not Page.IsPostBack Then


            quantiCorsiAttivi = ContatoriAttivi.quantiCorsiAttivi(Session("codice"))
            quanteEquiAttive = ContatoriAttivi.quanteEquiAttive(Session("codice"))
            quantiRinnoviAttivi = ContatoriAttivi.quantiRinnoviAttivi(Session("codice"))
            If quanteEquiAttive > 0 Then
                HiddenQuanteEquiparazioniAttive.Value = quanteEquiAttive
            Else
                HiddenQuanteEquiparazioniAttive.Value = 0
            End If

            If quantiCorsiAttivi > 0 Then
                HiddenQuantiCorsiAttivi.Value = quantiCorsiAttivi
            Else
                HiddenQuantiCorsiAttivi.Value = 0
            End If
            If quantiRinnoviAttivi > 0 Then
                HiddenQuantiRinnoviAttivi.Value = quantiRinnoviAttivi
            Else
                HiddenQuantiRinnoviAttivi.Value = 0
            End If


        End If
    End Sub


    ' System.Threading.Thread.Sleep(5000)


    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Response.Redirect("Rinnovi2/DashboardRinnovi2.aspx")
    End Sub


    'Function quantiAttivi(codice As String) As Integer

    '    Dim ritorno As Integer = 0

    '    Dim ds As DataSet

    '    Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
    '    fmsP.SetLayout("webCorsiRichiesta")
    '    Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
    '    ' RequestP.AddSearchField("pre_stato_web", "1")
    '    RequestP.AddSearchField("Codice_Ente_Richiedente", Session("codice"), Enumerations.SearchOption.equals)
    '    'RequestP.AddSortField("Codice_Status", Enumerations.Sort.Ascend)
    '    ' RequestP.AddSortField("IDCorso", Enumerations.Sort.Descend)



    '    ds = RequestP.Execute()

    '    If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
    '        Dim counter1 As Integer = 0
    '        For Each dr In ds.Tables("main").Rows



    '            If Data.FixNull(dr("Codice_Status")) = "84" Or Data.FixNull(dr("Codice_Status")) = "99" _
    '                Or Data.FixNull(dr("Codice_Status")) = "60" Or Data.FixNull(dr("Codice_Status")) = "0" Then
    '            Else

    '                counter1 += 1


    '            End If

    '        Next
    '        If counter1 >= 1 Then
    '            ritorno = counter1
    '        Else
    '            ritorno = 0
    '        End If

    '    Else
    '        '  ritorno = counter1

    '    End If
    '    Return ritorno

    'End Function
End Class