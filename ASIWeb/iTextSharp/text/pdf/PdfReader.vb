Imports System.IO

Namespace iTextSharp.text.pdf
    Friend Class PdfReader
        Private populatedPDFForm As Stream

        Public Sub New(populatedPDFForm As Stream)
            Me.populatedPDFForm = populatedPDFForm
        End Sub
    End Class
End Namespace
