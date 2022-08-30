Public Class Data
    Public Shared Function SQlEncode(ByVal sqlstring As String) As String

        Return (sqlstring.Replace("'", "''"))
    End Function
    Public Shared Function subsub(ByVal valore As String) As String
        Dim stringa As String = ""
        If valore = "http://www.mywebsite.com" Then
            stringa = valore.Replace("http://www.mywebsite.com", " ")
        End If
        Return stringa
    End Function
    Public Shared Function prendiIntero(ByVal obj As Object) As Integer
        If IsDBNull(obj) Then Return 0 Else Return CInt(obj)
    End Function
    Public Shared Function PrendiData(ByVal obj As Object) As Date
        If IsDBNull(obj) Then Return Nothing Else Return CDate(obj)
    End Function
    Public Shared Function prendiIntero2(ByVal valore As Object) As Integer
        If valore = String.Empty Then Return 0 Else Return CInt(valore)
    End Function
    Public Shared Function PrendiStringa(ByVal obj As Object) As String
        If IsDBNull(obj) Then
            Return String.Empty
        Else
            Dim data As String = obj.ToString()
            'Dim re As String = "[^\x09\x0A\x0D\x20-\uD7FF\uE000-\uFFFD\u10000-u10FFFF]"
            'data = Regex.Replace(data, re, "")

            data = data.Replace("&", " ")
            data = data.Replace("""", " ")
            data = data.Replace("'", " ")
            data = data.Replace("<", " ")
            data = data.Replace(">", " ")
            data = data.Replace("(", " ")

            data = data.Replace("$", " ")
            data = data.Replace("%", " ")
            data = data.Replace("*", " ")
            ' data = data.Replace("/", " ")
            ' data = data.Replace("\", " ")
            data = data.Replace("~", " ")


            'CleanInvalidXmlChars(data)

            Return data
        End If
    End Function
    Public Shared Function SistemaData(valore As String) As String

        Dim oDateDa As DateTime
        Dim giorno
        Dim anno
        Dim mese

        Dim miaData = valore
        oDateDa = DateTime.Parse(miaData)
        giorno = oDateDa.Day
        anno = oDateDa.Year
        mese = oDateDa.Month

        Return mese & "/" & giorno & "/" & anno


    End Function
    Public Shared Function RemoveCharacter(ByVal stringToCleanUp)
        Dim characterToRemove As String = ""
        characterToRemove = Chr(34) + "#$%&'()<>*+,-./\~"
        Dim firstThree As Char() = characterToRemove.Take(16).ToArray()
        For index = 1 To firstThree.Length - 1
            stringToCleanUp = stringToCleanUp.ToString.Replace(firstThree(index), "")
        Next
        Return stringToCleanUp
    End Function

    Public Shared Function PrendiStringa2(ByVal obj)
        If IsDBNull(obj) Then Return String.Empty Else Return CStr(obj)
    End Function
    Public Shared Function stringa(ByVal valore)
        If (valore = String.Empty) Then

            Return "n.d."
        Else
            Return valore
        End If
    End Function
    Public Shared Function stringa2(ByVal valore)
        If (valore = String.Empty) Then

            Return ""
        Else
            Return valore
        End If
    End Function




    Public Shared Function FixNull(ByVal dbvalue)
        If dbvalue Is DBNull.Value Then
            Return ""
        Else
            'NOTE: This will cast value to string if
            'it isn't a string.

            Return dbvalue.ToString
        End If

    End Function
    Public Shared Function StringToDate(ByVal datestring As String) As Date
        If IsDate(datestring) Then Return CDate(datestring)
    End Function
    Public Shared Function aggiustadata(ByVal obj As Object) As Date
        If obj = "0.00.00" Then Return "null" Else Return CDate(obj)
    End Function

    Public Shared Function DataFessa(ByVal DateIn As Date) As String
        If DateIn = "01/01/1900" Then
            Return ""
        Else
            Return "" & Format(DateIn, "dd/MM/yyyy") & ""
        End If
    End Function
    Public Shared Function DataNulla(ByVal DateIn As Date) As String
        If DateIn = Nothing Then
            Return "null"
        Else
            Return "'" & Format(DateIn, "dd/MM/yyyy") & "'"
        End If
    End Function
    Public Shared Function ControllaDataNull(ByVal checkDate As Date) As Object
        If checkDate = Nothing Then
            Return DBNull.Value
        Else
            Return checkDate
        End If
    End Function


    Public Shared Function FileExists(ByVal FileFullPath As String) As Boolean

        Dim f As New IO.FileInfo(FileFullPath)
        Return f.Exists

    End Function
    Public Shared Function PrendiStringaT(ByVal obj As Object) As String
        If IsDBNull(obj) Then
            Return String.Empty
        Else
            Dim data As String = obj.ToString()
            'Dim re As String = "[^\x09\x0A\x0D\x20-\uD7FF\uE000-\uFFFD\u10000-u10FFFF]"
            'data = Regex.Replace(data, re, "")


            data = data.Replace("""", " ")
            'data = data.Replace("'", " ")
            data = data.Replace("'", "")
            'data = data.Replace("<", " ")
            'data = data.Replace(">", " ")
            data = data.Replace("<", "&lt;")
            data = data.Replace(">", "&gt;")
            data = data.Replace("(", " ")
            data = data.Replace(")", " ")
            data = data.Replace("$", " ")
            data = data.Replace("%", " ")
            data = data.Replace("*", " ")
            data = data.Replace("€", " ")
            '   data = data.Replace("/", " ")
            '  data = data.Replace("\", " ")
            data = data.Replace("~", "&tilde;")
            data = data.Replace("&", "_|_")
            data = data.Replace(vbCr, " ")
            data = data.Replace(vbLf, " ")
            data = data.Replace(vbCrLf, " ")


            'CleanInvalidXmlChars(data)

            Return data
        End If
    End Function
End Class
