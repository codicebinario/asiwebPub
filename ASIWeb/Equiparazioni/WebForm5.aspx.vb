Imports ASIWeb.AsiModel
Imports fmDotNet
Imports System.Globalization
Imports System.Net

Public Class WebForm5
    Inherits System.Web.UI.Page
    Dim cultureFormat As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("it-IT")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim fms As FMSAxml = Nothing
        Dim ds As DataSet = Nothing
        Dim ritorno As Boolean = False
        Dim dataScadenza As DateTime
        fms = Conn.Connect()


        Dim it As DateTime = Date.Now.Date.ToString("dd/MM/yyyy", New CultureInfo("it-IT"))


        '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
        '     fmsB.SetDatabase(Database)
        fms.SetLayout("webRinnoviRichiesta")

        Dim RequestA = fms.CreateNewRecordRequest()

        RequestA.AddField("miaData", txtData.Text)
        RequestA.AddField("mioTesto", StripInValidXmlCharacters(TextBox1.Text))
        Session("id_record") = RequestA.Execute()

        '     RequestA.AddSearchField("Data scadenza", it, Enumerations.SearchOption.lessOrEqualThan)



        'Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
        'RequestA.AddSearchField("CodiceFiscale", TextBox1.Text, Enumerations.SearchOption.equals)
        ''     RequestA.AddSearchField("Data scadenza", it, Enumerations.SearchOption.lessOrEqualThan)


        ''  Try

        'ds = RequestA.Execute()


        'If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then
        '    For Each dr In ds.Tables("main").Rows


        '        '   dataScadenza = dr("DataScadenza")

        '        'If CDate(it) <= CDate(dr("DataScadenza")) Then
        '        If it <= dr("Data Scadenza") Then

        '            Response.Write("Oggi: " & it & " Data Scadenza: " & dr("Data scadenza") & " valido<br />")
        '            ritorno = True
        '        Else

        '            Response.Write("Oggi: " & it & " Data Scadenza: " & dr("Data scadenza") & " scaduto<br />")



        '        End If



        '    Next

        'Else
        '    ritorno = False
        'End If




        ''  Catch ex As Exception
        ''  ritorno = False
        '' End Try
        ''   Return ritorno
    End Sub
    Public Function addescape(ByVal str As String) As String
        Dim temp As String = ""

        For Each ch As Char In str

            If ch = vbNullChar Then
                temp += ""
            ElseIf ch = "'c" Then
                temp += "&#39;"
            ElseIf ch = "\c" Then
                temp += "&#34;"
            ElseIf ch = "<c" Then
                temp += "&lt;"
            ElseIf ch = ">c" Then
                temp += "&gt;"
            ElseIf ch = "\&c" Then
                temp += "&amp;"
            Else
                temp += ch
            End If
        Next

        Return (temp)
    End Function
    Public Function StripInValidXmlCharacters(ByVal pText As String) As String
        Dim sb As New StringBuilder()

        ' Used to hold the output.
        Dim current As Integer

        ' Used to reference the current character.
        If pText Is Nothing OrElse pText = String.Empty Then
            Return String.Empty
        End If

        For i As Integer = 0 To pText.Length - 1
            current = AscW(pText(i))
            If (current = &H9 OrElse current = &HA OrElse current = &HD) OrElse ((current >= &H20) AndAlso (current <= &HD7FF)) OrElse ((current >= &HE000) AndAlso (current <= &HFFFD)) OrElse ((current >= &H10000) AndAlso (current <= &H10FFFF)) Then
                sb.Append(ChrW(current))
            End If
        Next

        Return sb.ToString()

    End Function
End Class