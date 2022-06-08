Public Class Ed
    'Function to encode the string
    Public Function QueryStringEncode(ByVal value As String) As String
        Return HttpUtility.UrlEncode(TamperProofStringEncode(value, TamperProofKey))
    End Function

    Public Function QueryStringDecode(ByVal value As String) As String
        Return TamperProofStringDecode(value, TamperProofKey)
    End Function
    Private TamperProofKey As String = "Carola-lkj54923c478"

    Public Function TamperProofStringEncode(ByVal value As String,
                           ByVal key As String) As String
        Dim mac3des As New System.Security.Cryptography.MACTripleDES
        Dim md5 As New System.Security.Cryptography.MD5CryptoServiceProvider
        mac3des.Key = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key))
        Return Convert.ToBase64String(
          System.Text.Encoding.UTF8.GetBytes(value)) & "-"c &
          Convert.ToBase64String(mac3des.ComputeHash(
          System.Text.Encoding.UTF8.GetBytes(value)))
    End Function

    'Function to decode the string
    'Throws an exception if the data is corrupt
    Public Function TamperProofStringDecode(ByVal value As String,
              ByVal key As String) As String
        Dim dataValue As String = ""
        Dim calcHash As String = ""
        Dim storedHash As String = ""

        Dim mac3des As New System.Security.Cryptography.MACTripleDES
        Dim md5 As New System.Security.Cryptography.MD5CryptoServiceProvider
        mac3des.Key = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key))

        Try
            dataValue = System.Text.Encoding.UTF8.GetString(
                    Convert.FromBase64String(value.Split("-"c)(0)))
            storedHash = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(value.Split("-"c)(1)))
            calcHash = System.Text.Encoding.UTF8.GetString(
              mac3des.ComputeHash(System.Text.Encoding.UTF8.GetBytes(dataValue)))

            If storedHash <> calcHash Then
                'Data was corrupted

                Throw New ArgumentException("Hash value does not match")
                'This error is immediately caught below
            End If
        Catch ex As Exception
            Throw New ArgumentException("Invalid TamperProofString")
        End Try

        Return dataValue

    End Function

    Public Function controllonull(ByVal dbvalue) As String
        If dbvalue Is DBNull.Value Then
            Return ""
        Else
            Return dbvalue.ToString
        End If
    End Function

    Public Function controllonull2(ByVal dbvalue) As String
        If dbvalue Is DBNull.Value Then
            Return ""
        Else
            Return dbvalue.double
        End If
    End Function
    Public Function controllonull4(ByVal dbvalue) As String
        If dbvalue Is DBNull.Value Then
            Return dbvalue.null

        End If
    End Function
    Public Function controllonull3(ByVal dbvalue) As String
        If dbvalue Is DBNull.Value Then
            Return ""
        Else
            Return dbvalue.date("dd/MM/yyyy")
        End If
    End Function

End Class
