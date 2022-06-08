Imports fmDotNet
Imports System.Web.Services
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed
Public Class WebForm1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        Dim ds As DataSet

        fmsP.SetLayout("web_richiesta_allegati")

        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        RequestP.AddSearchField("Codice_Richiesta", "1904", Enumerations.SearchOption.equals)

        ds = RequestP.Execute()

        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

            For Each dr In ds.Tables("main").Rows

                Dim nomeFileOnFS As String = dr("nomeFileOnFS")
                Dim fileAsContainer As String = dr("ricevuta_pagamento_ext")
                Response.Write("ciao")
                Response.Write("<a href='<Data>/ASI/" & fileAsContainer & "</data>'>ciao</a>")

            Next
            ' <data>/images/logo.jpg</data>




        End If
    End Sub

End Class