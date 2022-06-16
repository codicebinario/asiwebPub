Imports fmDotNet
Imports System.Web.Services
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed
Imports System.Configuration
Public Class WebForm1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



    End Sub

    Function cerca(valore As String)
        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        Dim ds As DataSet

        fmsP.SetLayout("webFormatori")

        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        RequestP.AddSearchField("PrimaLettera", valore, Enumerations.SearchOption.beginsWith)

        ds = RequestP.Execute()

        Return ds


    End Function
    <WebMethod()>
    Public Shared Function SearchCustomers(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        Dim ds As DataSet

        fmsP.SetLayout("webFormatori")

        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        RequestP.AddSearchField("Prime4Lettere", prefixText, Enumerations.SearchOption.beginsWith)

        ds = RequestP.Execute()

        Dim customers As List(Of String) = New List(Of String)()
        If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
            For Each dr In ds.Tables("main").Rows

                customers.Add(dr("Cognome").ToString() & ", " & dr("nome").ToString())

            Next




        End If



        Return customers



    End Function
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Formatori As DataSet

        Formatori = cerca(txtValore.Text)


        If Not IsNothing(Formatori) AndAlso Formatori.Tables("main").Rows.Count > 0 Then

            For Each dr In Formatori.Tables("main").Rows



                Response.Write(dr("cognome") & "<br />")

            Next
            ' <data>/images/logo.jpg</data>




        End If
    End Sub
End Class