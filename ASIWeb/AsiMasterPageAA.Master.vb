Imports fmDotNet
Imports ASIWeb.Ed
Public Class AsiMasterPageAA
    Inherits System.Web.UI.MasterPage
    Dim deEnco As New Ed
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("login.aspx")
        End If
        If IsNothing(Session("codice")) Then
            Response.Redirect("login.aspx")
        End If

        If Not Page.IsPostBack Then

            'If quantiDaValutare(Session("codice")) = True Then
            '    LinkSettore.Visible = True
            'Else
            '    LinkSettore.Visible = False
            'End If

            'If quantiValutati(Session("codice")) = True Then
            '    LinkSettoreValutati.Visible = True
            'Else
            '    LinkSettoreValutati.Visible = False
            'End If



            If Not IsNothing(Session("denominazione")) Then


                '  Dim lblMasterDen As Literal = DirectCast(Master.FindControl("litDenominazione"), Literal)
                ' litDenominazione.Text = "Codice: " & AsiModel.LogIn.Codice & " - " & "Tipo Ente: " & AsiModel.LogIn.TipoEnte & " - " & AsiModel.LogIn.Denominazione
                litDenominazione.Text = Session("denominazione")

            End If

        End If

    End Sub

    Function quantiDaValutare(codice As String) As Boolean
        Dim ritorno As Boolean = False

        Dim ds As DataSet

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("webCorsiRichiesta")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        ' RequestP.AddSearchField("pre_stato_web", "1")
        RequestP.AddSearchField("Settore_Approvazione_ID", Session("codice"), Enumerations.SearchOption.equals)
        RequestP.AddSortField("Codice_Status", Enumerations.Sort.Ascend)
        RequestP.AddSortField("IDCorso", Enumerations.Sort.Descend)



        ds = RequestP.Execute()

        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
            Dim counter1 As Integer = 0
            For Each dr In ds.Tables("main").Rows



                If Data.FixNull(dr("Codice_Status")) = "63" Then
                    counter1 += 1


                End If

            Next
            If counter1 >= 1 Then
                ritorno = True
            Else
                ritorno = False
            End If

        Else
            ritorno = False

        End If
        Return ritorno

    End Function

    Function quantiValutati(codice As String) As Boolean
        Dim ritorno As Boolean = False

        Dim ds As DataSet

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("webCorsiRichiesta")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        ' RequestP.AddSearchField("pre_stato_web", "1")
        RequestP.AddSearchField("Settore_Approvazione_ID", Session("codice"), Enumerations.SearchOption.equals)
        RequestP.AddSortField("Codice_Status", Enumerations.Sort.Ascend)
        RequestP.AddSortField("IDCorso", Enumerations.Sort.Descend)



        ds = RequestP.Execute()

        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

            Dim counter1 As Integer = 0
            For Each dr In ds.Tables("main").Rows



                If Data.FixNull(dr("Codice_Status")) = "64" Or Data.FixNull(dr("Codice_Status")) = "65" Then
                    counter1 += 1


                End If

            Next
            If counter1 >= 1 Then
                ritorno = True
            Else
                ritorno = False
            End If

        Else

            ' non si sono records
            ritorno = False


        End If


        Return ritorno

    End Function

    Protected Sub lnkOut_Click(sender As Object, e As EventArgs) Handles lnkOut.Click
        Session("auth") = "0"
        Session("auth") = Nothing
        Session.Clear()
        Session.Abandon()
        Response.Redirect("login.aspx")
    End Sub
    Protected Sub lnkHome_Click(sender As Object, e As EventArgs) Handles lnkHome.Click

        Response.Redirect("home.aspx")
    End Sub
    'Protected Sub lnkNuovoCorso_Click(sender As Object, e As EventArgs) Handles lnkNuovoCorso.Click
    '    NuovoCorso()
    '    '     ?codR=" & deEnco.QueryStringEncode(Data.FixNull(dr("Codice_Richiesta"))) & "&record_ID=" & deEnco.QueryStringEncode(dr("Record_ID"))

    '    Response.Redirect("richiestaCorso.aspx?codR=" & deEnco.QueryStringEncode(Session("IDCorso")) & "&record_ID=" & deEnco.QueryStringEncode(Session("id_record")))
    'End Sub


    Protected Sub lnkAlbo_Click(sender As Object, e As EventArgs) Handles lnkAlbo.Click
        '    NuovaRichiesta()

        Response.Redirect("HomeA.aspx")
    End Sub

    'Protected Sub LinkArchivio_Click(sender As Object, e As EventArgs) Handles LinkArchivio.Click
    '    '    NuovaRichiesta()

    '    Response.Redirect("archivioAlbo.aspx")
    'End Sub




    Sub NuovoCorso()
        '  Dim litNumRichieste As Literal = DirectCast(ContentPlaceHolder1.FindControl("LitNumeroRichiesta"), Literal)

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        Dim ds As DataSet


        fmsP.SetLayout("webCorsiRichiesta")
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

                AsiModel.IDCorso = Data.FixNull(dr("IDCorso"))

                '    Session("record_ID") = Data.FixNull(dr("Record_ID"))
                Session("IDCorso") = Data.FixNull(dr("IDCorso"))

            Next


            AsiModel.LogIn.LogCambioStatus(Session("IDCorso"), "0", Session("WebUserEnte"))

        End If



    End Sub

    'Protected Sub LinkSettore_Click(sender As Object, e As EventArgs) Handles LinkSettore.Click
    '    Response.Redirect("dashboardV.aspx")
    'End Sub

    'Protected Sub LinkSettoreValutati_Click(sender As Object, e As EventArgs) Handles LinkSettoreValutati.Click
    '    Response.Redirect("archivioAlboValutati.aspx")
    'End Sub
End Class