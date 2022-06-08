
Imports System.Web.Services
Public Class test
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


    End Sub

    <WebMethod>
    Public Shared Function CalculateSum(Num1 As Int32, Num2 As Int32) As String
        Dim Result As Int32 = Num1 + Num2
        Return Result.ToString()
    End Function


End Class