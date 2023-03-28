Imports fmDotNet
Imports ASIWeb.Ed
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Net

Public Class AsiMasterPageEqui2
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
            Dim quantiVal As Integer = quantiDaValutare(Session("codice"))
            Dim quantiEvasival As Integer = quantiEvasi(Session("codice"))
            Dim quantiAttiviVal As Integer = quantiAttivi(Session("codice"))
            Dim quantiValutatiVal As Integer = quantiValutati(Session("codice"))
            ' If quantiAttiviVal >= 1 Then
            LinkEquiAttive.Text = "<i class=""bi bi-arrow-down-circle""> </i>Equip. Attive <span class=""badge badge-light text-dark""> " & quantiAttiviVal & "</span>"


            ' End If

            '  If quantiEvasival >= 1 Then
            '   LinkSettore.Visible = True
            LinkArchivioEqui.Text = "<i class=""bi bi-arrow-down-circle""> </i>Equip. Evase <span class=""badge badge-light text-dark""> " & quantiEvasival & "</span>"

            '  End If

            If quantiDaValutare(Session("codice")) >= 1 Then
                LinkSettore.Visible = True
                LinkSettore.Text = "<i class=""bi bi-arrows-angle-contract""> </i>Equip. da Valutare <span class=""badge badge-light text-dark""> " & quantiVal & "</span>"
            Else
                LinkSettore.Visible = False
            End If

            If quantiValutati(Session("codice")) >= 1 Then
                LinkSettoreValutati.Visible = True
                LinkSettoreValutati.Text = "<i class=""bi bi-arrows-angle-expand""> </i>Equip. Valutate <span class=""badge badge-light text-dark""> " & quantiValutatiVal & "</span>"

            Else
                LinkSettoreValutati.Visible = False
            End If



            If Not IsNothing(Session("denominazione")) Then


                '  Dim lblMasterDen As Literal = DirectCast(Master.FindControl("litDenominazione"), Literal)
                ' litDenominazione.Text = "Codice: " & AsiModel.LogIn.Codice & " - " & "Tipo Ente: " & AsiModel.LogIn.TipoEnte & " - " & AsiModel.LogIn.Denominazione
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
    Protected Sub lnkNuovaEquiparazione_Click(sender As Object, e As EventArgs) Handles lnkNuovaEquiparazione.Click
        '  NuovaEquiparazione()
        '     ?codR=" & deEnco.QueryStringEncode(Data.FixNull(dr("Codice_Richiesta"))) & "&record_ID=" & deEnco.QueryStringEncode(dr("Record_ID"))
        'Dim idEquiparazioneM As Integer
        '  Dim idRecordM As Integer
        '   idEquiparazioneM = AsiModel.Equiparazione.NuovaEquiparazioneFolder(Session("codice"))

        '   If idEquiparazioneM >= 1 Then
        '   idRecordM = AsiModel.Equiparazione.CercaIDRecordEquiparazioneM(idEquiparazioneM)
        '  Session("IdRecordMaster") = idRecordM
        '  Response.Redirect("sceltaSport.aspx?codR=" & deEnco.QueryStringEncode(idEquiparazioneM) & "&t=1")
        ' Response.Redirect("checkTesseramento2.aspx?codR=" & deEnco.QueryStringEncode(Session("IDEquiparazione")) & "&record_ID=" & deEnco.QueryStringEncode(Session("id_record")) & "&t=1")
        Response.Redirect("sceltaSport.aspx")


        '   End If





    End Sub


    Protected Sub lnkEqui_Click(sender As Object, e As EventArgs) Handles lnkEqui.Click
        '    NuovaRichiesta()

        Response.Redirect("../HomeA.aspx")
    End Sub

    Protected Sub LinkArchivioEqui_Click(sender As Object, e As EventArgs) Handles LinkArchivioEqui.Click
        '    NuovaRichiesta()

        Response.Redirect("DashboardEquiEvasi2.aspx")
    End Sub




    Sub NuovaEquiparazione()
        '  Dim litNumRichieste As Literal = DirectCast(ContentPlaceHolder1.FindControl("LitNumeroRichiesta"), Literal)

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        Dim ds As DataSet


        fmsP.SetLayout("webEquiparazioniRichiesta")
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
        RequestP.AddSearchField("idrecord", Session("id_record"), Enumerations.SearchOption.equals)

        ds = RequestP.Execute()
        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
            For Each dr In ds.Tables("main").Rows

                '  AsiModel.DatiNuovoCorso. = Data.FixNull(dr("IDCorso"))

                '    Session("record_ID") = Data.FixNull(dr("Record_ID"))
                Session("IDEquiparazione") = Data.FixNull(dr("IDrecord"))

            Next


            AsiModel.LogIn.LogCambioStatus(Session("IDEquiparazione"), "0", Session("WebUserEnte"), "equiparazione")

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
    Function quantiAttivi(codice As String) As Integer
        Dim ritorno As Integer = 0

        Dim ds As DataSet

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("webEquiparazioniMaster")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        ' RequestP.AddSearchField("pre_stato_web", "1")
        RequestP.AddSearchField("CodiceEnteRichiedente", codice, Enumerations.SearchOption.equals)
        RequestP.AddSearchField("CodiceStatus", "0...114")
        '  RequestP.AddSearchField("Codice_Status", "115")
        ' RequestP.AddSortField("Codice_Status", Enumerations.Sort.Ascend)
        '  RequestP.AddSortField("IDEquiparazione", Enumerations.Sort.Descend)



        ds = RequestP.Execute()

        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

            Dim counter1 As Integer = 0
            For Each dr In ds.Tables("main").Rows


                counter1 += 1




            Next
            If counter1 >= 1 Then
                ritorno = counter1
            Else
                ritorno = 0
            End If

        Else

            ' non si sono records
            'ritorno = False


        End If


        Return ritorno

    End Function

    Function quantiEvasi(codice As String) As Integer
        Dim ritorno As Integer = 0

        Dim ds As DataSet

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("webEquiparazioniRichiestaMolti")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        ' RequestP.AddSearchField("pre_stato_web", "1")
        RequestP.AddSearchField("Codice_Ente_Richiedente", codice, Enumerations.SearchOption.equals)
        RequestP.AddSearchField("Codice_Status", "115")
        ' RequestP.AddSortField("Codice_Status", Enumerations.Sort.Ascend)
        '  RequestP.AddSortField("IDEquiparazione", Enumerations.Sort.Descend)



        ds = RequestP.Execute()

        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

            Dim counter1 As Integer = 0
            For Each dr In ds.Tables("main").Rows




                counter1 += 1




            Next
            If counter1 >= 1 Then
                ritorno = counter1
            Else
                ritorno = 0
            End If

        Else

            ' non si sono records
            'ritorno = False


        End If


        Return ritorno

    End Function


    Function quantiValutati(codice As String) As Integer
        Dim ritorno As Integer = 0

        Dim ds As DataSet

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("webEquiparazioniRichiestaMolti")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        ' RequestP.AddSearchField("pre_stato_web", "1")
        RequestP.AddSearchField("Equi_Settore_Approvazione_ID", Session("codice"), Enumerations.SearchOption.equals)
        RequestP.AddSearchField("Codice_Status", "106...107")
        '    RequestP.AddSortField("Codice_Status", Enumerations.Sort.Ascend)
        '   RequestP.AddSortField("IDEquiparazione", Enumerations.Sort.Descend)



        ds = RequestP.Execute()

        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

            Dim counter1 As Integer = 0
            For Each dr In ds.Tables("main").Rows



                ' If Data.FixNull(dr("Codice_Status")) = "106" Or Data.FixNull(dr("Codice_Status")) = "107" Then
                counter1 += 1


                '   End If

            Next
            If counter1 >= 1 Then
                ritorno = counter1
            Else
                ritorno = 0
            End If

        Else

            ' non si sono records
            '  ritorno = False


        End If


        Return ritorno

    End Function
    Function quantiDaValutare(codice As String) As Integer
        Dim ritorno As Integer = 0

        Dim ds As DataSet

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("webEquiparazioniRichiestaMolti")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        ' RequestP.AddSearchField("pre_stato_web", "1")
        RequestP.AddSearchField("Equi_Settore_Approvazione_ID", Session("codice"), Enumerations.SearchOption.equals)
        RequestP.AddSearchField("Codice_Status", "115")
        RequestP.AddSortField("Codice_Status", Enumerations.Sort.Ascend)
        RequestP.AddSortField("IDRecord", Enumerations.Sort.Descend)



        ds = RequestP.Execute()

        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
            Dim counter1 As Integer = 0
            For Each dr In ds.Tables("main").Rows



                counter1 += 1




            Next
            If counter1 >= 1 Then
                ritorno = counter1
            Else
                ritorno = 0
            End If

        Else
            '  ritorno = counter1

        End If
        Return ritorno

    End Function
    Protected Sub LinkSettore_Click(sender As Object, e As EventArgs) Handles LinkSettore.Click
        Response.Redirect("dashboardV2.aspx")
    End Sub

    Protected Sub LinkSettoreValutati_Click(sender As Object, e As EventArgs) Handles LinkSettoreValutati.Click
        Response.Redirect("archivioEquiValutati2.aspx")
    End Sub

    Protected Sub LinkEquiAttive_Click(sender As Object, e As EventArgs) Handles LinkEquiAttive.Click
        Response.Redirect("DashboardEqui2.aspx")
    End Sub
End Class