Imports fmDotNet
Imports System.Web.Services
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed

Public Class annulla
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("login.aspx")
        End If
        Dim deEnco As New Ed

        Dim codiceRichiesta As String = deEnco.QueryStringDecode(Request.QueryString("codR"))
        Dim record_ID As String = deEnco.QueryStringDecode(Request.QueryString("record_ID"))
        Dim Status As String = deEnco.QueryStringDecode(Request.QueryString("status"))

        Dim risposta As String = ""

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        Dim ds As DataSet


        fmsP.SetLayout("web_richiesta_master")

        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        RequestP.AddSearchField("Codice_Richiesta", codiceRichiesta, Enumerations.SearchOption.equals)

        Try
            ds = RequestP.Execute()
            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
                For Each dr In ds.Tables("main").Rows

                    If Data.FixNull(dr("Status_ID")) = "1" Or Data.FixNull(dr("Status_ID")) = "2" Or Data.FixNull(dr("Status_ID")) = "3" Then
                        risposta = AsiModel.AnnullaOrdine(record_ID)
                        AsiModel.LogIn.LogCambioStatus(codiceRichiesta, "10", Session("WebUserEnte"))
                        Session("risultato") = "ok"
                    Else
                        risposta = "ko"
                        Session("risultato") = "ko"
                    End If

                Next



            End If
        Catch ex As Exception

        End Try





        Response.Redirect("dashboard.aspx")

    End Sub

End Class