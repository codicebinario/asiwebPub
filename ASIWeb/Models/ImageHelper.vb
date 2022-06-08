Imports System.Drawing
Imports System.Drawing.Imaging

Imports System.IO
Public Class ImageHelper

    ' Convert an image to a Base64 string by using a MemoryStream. Save the
    ' image to the MemoryStream and use Convert.ToBase64String to convert
    ' the content of that MemoryStream to a Base64 string.
    Public Shared Function ImageToBase64String(ByVal image As Image, ByVal imageFormat As ImageFormat)

        Using memStream As New MemoryStream

            image.Save(memStream, imageFormat)

            Dim result As String = Convert.ToBase64String(memStream.ToArray())

            memStream.Close()

            Return result

        End Using

    End Function

    ' Convert a Base64 string back to an image. Fill a MemorySTream based
    ' on the Base64 string and call the Image.FromStream() methode to
    ' convert the content of the MemoryStream to an image.
    Public Shared Function ImageFromBase64String(ByVal base64 As String)

        Using memStream As New MemoryStream(Convert.FromBase64String(base64))

            Dim result As Image = Image.FromStream(memStream)

            memStream.Close()

            Return result

        End Using

    End Function
End Class