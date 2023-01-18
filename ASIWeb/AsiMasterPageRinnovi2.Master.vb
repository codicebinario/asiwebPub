Imports fmDotNet
Imports ASIWeb.Ed
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Net

Public Class AsiMasterPageRinnovi2
    Inherits System.Web.UI.MasterPage
    Dim deEnco As New Ed
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If
        If IsNothing(Session("codice")) Then
            Response.Redirect("../login.aspx")
        End If

        If Not Page.IsPostBack Then



            If Not IsNothing(Session("denominazione")) Then

                litDenominazione.Text = "<i Class=""bi bi-intersect""> </i>" & Session("denominazione")

            End If

        End If

    End Sub

    Protected Sub lnkOut_Click(sender As Object, e As EventArgs) Handles lnkOut.Click
        Session("auth") = "0"
        Session("auth") = Nothing
        Session.Clear()
        Session.Abandon()
        Response.Redirect("../login.aspx")
    End Sub
    Protected Sub lnkHome_Click(sender As Object, e As EventArgs) Handles lnkHome.Click

        Response.Redirect("../home.aspx")
    End Sub
    Protected Sub lnkNuovoRinnovo_Click(sender As Object, e As EventArgs) Handles lnkNuovoRinnovo.Click
        Dim idRinnovoM As Integer
        idRinnovoM = AsiModel.Rinnovi.NuovoRinnovo(Session("codice"))
        If idRinnovoM >= 1 Then
            Response.Redirect("checkTesseramentoRinnovi2.aspx?codR=" & deEnco.QueryStringEncode(idRinnovoM) & "&t=1")
        End If

    End Sub


    Protected Sub lnkRinnovi_Click(sender As Object, e As EventArgs) Handles lnkRinnovi.Click
        '    NuovaRichiesta()

        Response.Redirect("../HomeA.aspx")
    End Sub

    Protected Sub LinkArchivioRinnovi_Click(sender As Object, e As EventArgs) Handles LinkArchivioRinnovi.Click
        '    NuovaRichiesta()

        Response.Redirect("DashboardRinnoviEvasi.aspx")
    End Sub




    Sub NuovoRinnovo()
        '  Dim litNumRichieste As Literal = DirectCast(ContentPlaceHolder1.FindControl("LitNumeroRichiesta"), Literal)

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        Dim ds As DataSet


        fmsP.SetLayout("webRinnoviRichiesta")
        Dim Request = fmsP.CreateNewRecordRequest()

        Request.AddField("Codice_Ente_Richiedente", Session("codice"))
        Request.AddField("Codice_Status", "0")


        Session("id_record") = Request.Execute()




        'Dim risposta As String = ""
        'fmsP.SetLayout("web_richiesta_master")
        'Dim Request1 = fmsP.CreateEditRequest(Session("id_richiesta"))
        'Request1.AddField("Status_ID", "1")


        'risposta = Request1.Execute()


        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        RequestP.AddSearchField("id_record", Session("id_record"), Enumerations.SearchOption.equals)

        ds = RequestP.Execute()
        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
            For Each dr In ds.Tables("main").Rows

                '  AsiModel.DatiNuovoCorso. = Data.FixNull(dr("IDCorso"))

                '    Session("record_ID") = Data.FixNull(dr("Record_ID"))
                Session("IDRinnovo") = Data.FixNull(dr("IDRinnovo"))

            Next


            '  AsiModel.LogIn.LogCambioStatus(Session("IDRinnovo"), "0", Session("WebUserEnte"), "rinnovo")

        End If



    End Sub

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
    'Function quantiValutati(codice As String) As Boolean
    '    Dim ritorno As Boolean = False

    '    Dim ds As DataSet

    '    Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
    '    fmsP.SetLayout("webEquiparazioniRichiesta")
    '    Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
    '    ' RequestP.AddSearchField("pre_stato_web", "1")
    '    RequestP.AddSearchField("Equi_Settore_Approvazione_ID", Session("codice"), Enumerations.SearchOption.equals)
    '    RequestP.AddSortField("Codice_Status", Enumerations.Sort.Ascend)
    '    RequestP.AddSortField("IDEquiparazione", Enumerations.Sort.Descend)



    '    ds = RequestP.Execute()

    '    If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

    '        Dim counter1 As Integer = 0
    '        For Each dr In ds.Tables("main").Rows



    '            If Data.FixNull(dr("Codice_Status")) = "106" Or Data.FixNull(dr("Codice_Status")) = "107" Then
    '                counter1 += 1


    '            End If

    '        Next
    '        If counter1 >= 1 Then
    '            ritorno = True
    '        Else
    '            ritorno = False
    '        End If

    '    Else

    '        ' non si sono records
    '        ritorno = False


    '    End If


    '    Return ritorno

    'End Function
    'Function quantiDaValutare(codice As String) As Integer
    '    Dim ritorno As Integer = 0

    '    Dim ds As DataSet

    '    Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
    '    fmsP.SetLayout("webEquiparazioniRichiesta")
    '    Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
    '    ' RequestP.AddSearchField("pre_stato_web", "1")
    '    RequestP.AddSearchField("Equi_Settore_Approvazione_ID", Session("codice"), Enumerations.SearchOption.equals)
    '    RequestP.AddSortField("Codice_Status", Enumerations.Sort.Ascend)
    '    RequestP.AddSortField("IDEquiparazione", Enumerations.Sort.Descend)



    '    ds = RequestP.Execute()

    '    If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
    '        Dim counter1 As Integer = 0
    '        For Each dr In ds.Tables("main").Rows



    '            If Data.FixNull(dr("Codice_Status")) = "105" Then
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
    'Protected Sub LinkSettore_Click(sender As Object, e As EventArgs) Handles LinkSettore.Click
    '    Response.Redirect("dashboardV.aspx")
    'End Sub

    'Protected Sub LinkSettoreValutati_Click(sender As Object, e As EventArgs) Handles LinkSettoreValutati.Click
    '    Response.Redirect("archivioEquiValutati.aspx")
    'End Sub
End Class