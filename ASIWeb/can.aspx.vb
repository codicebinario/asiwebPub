Imports fmDotNet
Imports System.Web.Services
Public Class can
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("login.aspx")
        End If
        If IsNothing(Session("codice")) Then
            Response.Redirect("login.aspx")
        End If

    End Sub

    '<WebMethod>
    'Public Shared Function cancella2(id As Integer) As Integer

    '    Dim total As Integer = 0
    '    total = firstNumber + secondNumber
    '    Return total

    'End Function
    <WebMethod>
    Public Shared Function cancella2(ID As Integer) As Boolean

        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        '  Dim ds As DataSet


        fmsP.SetLayout("web_richiesta_dettaglio")
        Dim Request = fmsP.CreateDeleteRequest(ID)


        Request.Execute()


        Return True

    End Function


End Class