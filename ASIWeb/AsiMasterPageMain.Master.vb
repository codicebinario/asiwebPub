Imports fmDotNet
Imports ASIWeb.Ed
Public Class AsiMasterPageMain
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

            If Not IsNothing(Session("denominazione")) Then


                '  Dim lblMasterDen As Literal = DirectCast(Master.FindControl("litDenominazione"), Literal)
                ' litDenominazione.Text = "Codice: " & AsiModel.LogIn.Codice & " - " & "Tipo Ente: " & AsiModel.LogIn.TipoEnte & " - " & AsiModel.LogIn.Denominazione
                litDenominazione.Text = Session("denominazione")

            End If

        End If

    End Sub

    Protected Sub lnkOut_Click(sender As Object, e As EventArgs) Handles lnkOut.Click
        Session("auth") = "0"
        Session("auth") = Nothing
        Session.Clear()
        Session.Abandon()
        Response.Redirect("login.aspx")
    End Sub

    'Protected Sub lnkNuovaRichiesta_Click(sender As Object, e As EventArgs) Handles lnkNuovaRichiesta.Click
    '    NuovaRichiesta()
    '    '     ?codR=" & deEnco.QueryStringEncode(Data.FixNull(dr("Codice_Richiesta"))) & "&record_ID=" & deEnco.QueryStringEncode(dr("Record_ID"))

    '    Response.Redirect("richiestaTessere.aspx?codR=" & deEnco.QueryStringEncode(Session("Codice_Richiesta")) & "&record_ID=" & deEnco.QueryStringEncode(Session("id_richiesta")))
    'End Sub


    'Protected Sub LinkArchivio_Click(sender As Object, e As EventArgs) Handles LinkArchivio.Click
    '    '    NuovaRichiesta()

    '    Response.Redirect("archivio.aspx")
    'End Sub




    'Sub NuovaRichiesta()
    '    '  Dim litNumRichieste As Literal = DirectCast(ContentPlaceHolder1.FindControl("LitNumeroRichiesta"), Literal)

    '    Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
    '    Dim ds As DataSet


    '    fmsP.SetLayout("web_richiesta_master")
    '    Dim Request = fmsP.CreateNewRecordRequest()

    '    Request.AddField("ente_codice", AsiModel.LogIn.Codice)
    '    Request.AddField("Status_ID", "1")


    '    Session("id_richiesta") = Request.Execute()


    '    'Dim risposta As String = ""
    '    'fmsP.SetLayout("web_richiesta_master")
    '    'Dim Request1 = fmsP.CreateEditRequest(Session("id_richiesta"))
    '    'Request1.AddField("Status_ID", "1")


    '    'risposta = Request1.Execute()


    '    Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
    '    RequestP.AddSearchField("Record_ID", "==" & Session("id_richiesta"))

    '    ds = RequestP.Execute()
    '    If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
    '        For Each dr In ds.Tables("main").Rows

    '            AsiModel.CodiceRichiesta = Data.FixNull(dr("Codice_Richiesta"))

    '            '    Session("record_ID") = Data.FixNull(dr("Record_ID"))
    '            Session("Codice_Richiesta") = Data.FixNull(dr("Codice_Richiesta"))

    '        Next

    '    End If



    'End Sub


End Class