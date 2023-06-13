Imports fmDotNet
Imports ASIWeb.Ed

Public Class CancellaGruppo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Dim deEnco As New Ed

        Dim codiceRinnovoM As String = deEnco.QueryStringDecode(Request.QueryString("codR"))
        Dim record_ID As String = deEnco.QueryStringDecode(Request.QueryString("record_ID"))
        '  Dim IDRecord As String = deEnco.QueryStringDecode(Request.QueryString("id"))
        Dim rispostaM As Integer = 0
        Dim dsM As DataSet = Nothing
        Dim ds As DataSet = Nothing
        If Not String.IsNullOrEmpty(record_ID) Then

            Try
                Dim fmsPM As FMSAxml = AsiModel.Conn.Connect()

                fmsPM.SetLayout("webRinnoviMaster")
                Dim RequestP = fmsPM.CreateFindRequest(Enumerations.SearchType.Subset)
                RequestP.AddSearchField("IDRinnovoM", codiceRinnovoM, Enumerations.SearchOption.equals)

                dsM = RequestP.Execute()

                If Not IsNothing(dsM) AndAlso dsM.Tables("main").Rows.Count > 0 Then
                    For Each dr In dsM.Tables("main").Rows
                        Dim RequestPM = fmsPM.CreateDeleteRequest(dr("idRecord"))
                        rispostaM = RequestPM.Execute()
                    Next
                End If

            Catch ex As Exception
                AsiModel.LogIn.LogErrori(ex, "CancellaGruppo", "rinnovi")
                Response.Redirect("../FriendlyMessage.aspx", False)
            End Try


            Dim risposta As Integer = 0
            Try
                Dim fmsP As FMSAxml = AsiModel.Conn.Connect()

                fmsP.SetLayout("webRinnoviRichiesta2")
                Dim RequestA = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
                RequestA.AddSearchField("IDRinnovoM", codiceRinnovoM, Enumerations.SearchOption.equals)

                ds = RequestA.Execute()

                If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                    For Each dr In ds.Tables("main").Rows
                        Dim RequestPD = fmsP.CreateDeleteRequest(dr("id_record"))
                        risposta = RequestPD.Execute()
                    Next
                End If
            Catch ex As Exception
                AsiModel.LogIn.LogErrori(ex, "CancellaGruppo", "rinnovi")
                Response.Redirect("../FriendlyMessage.aspx", False)
            End Try

            'Response.Redirect("dashboardRinnovi2.aspx?open=" & codiceRinnovoM & "&ris=" & deEnco.QueryStringEncode("cano"))

        End If



    End Sub

End Class