

Public Class Rootobject

    Public Property response As Response

    Public Property messages() As Message
End Class

Public Class Response
    Public Property dataInfo As Datainfo
    Public Property data() As Datum
End Class

Public Class Datainfo
    Public Property database As String
    Public Property layout As String
    Public Property table As String
    Public Property totalRecordCount As Integer
    Public Property foundCount As Integer
    Public Property returnedCount As Integer
End Class

Public Class Datum
    ' Public Property fieldData As List(Of Dictionary(Of String, Object))
    ' Public Property fieldData As Object
    Public Property fieldData As Fielddata
    Public Property portalData As Portaldata
    Public Property recordId As String
    Public Property modId As String
End Class

Public Class Fielddata
    Public Property Codice_Status As Integer
    Public Property IDCorso As String
    Public Property Descrizione_StatusWeb As String
End Class

Public Class Portaldata
End Class

Public Class Message
    Public Property code As String
    Public Property message As String
End Class
