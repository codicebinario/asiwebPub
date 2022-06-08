Imports fmDotNet
Imports ASIWeb.Data
Imports ASIWeb.AsiModel
Public Class Utility




    Public Shared Function statusColorText(param As String) As String


        Dim stringaRitorno As String = ""


        Select Case param
            Case "1"
                stringaRitorno = "class='badge bg-uno text-dark'"
            Case "2"
                stringaRitorno = "class='badge bg-due text-dark'"

            Case "3"
                stringaRitorno = "class='badge bg-tre text-dark'"

            Case "4"
                stringaRitorno = "class='badge bg-quattro text-dark'"
            Case "5"
                stringaRitorno = "class='badge bg-cinque text-dark'"
            Case "6"
                stringaRitorno = "class='badge bg-sei text-dark'"
            Case "7"
                stringaRitorno = "class='badge bg-sette text-dark'"
            Case "8"
                stringaRitorno = "class='badge bg-otto text-dark'"
            Case "9"
                stringaRitorno = "class='badge bg-nove text-white'"
            Case "10"
                stringaRitorno = "class='badge bg-dieci text-white'"
            Case "11"
                stringaRitorno = "class='badge bg-undici text-white'"

            Case Else
                stringaRitorno = "class='badge bg-secondary text-dark'"




        End Select


        Return stringaRitorno




    End Function

    Public Shared Function statusColorTextCorsi(param As String) As String


        Dim stringaRitorno As String = ""


        Select Case param
            Case "51"
                stringaRitorno = "class='badge bg-uno text-dark'"
            Case "54"
                stringaRitorno = "class='badge bg-due text-dark'"

            Case "57"
                stringaRitorno = "class='badge bg-tre text-dark'"

            Case "60"
                stringaRitorno = "class='badge bg-quattro text-dark'"
            Case "63"
                stringaRitorno = "class='badge bg-cinque text-dark'"
            Case "64"
                stringaRitorno = "class='badge bg-sei text-dark'"
            Case "65"
                stringaRitorno = "class='badge bg-sette text-dark'"
            Case "66"
                stringaRitorno = "class='badge bg-quattro text-white'"
            Case "67"
                stringaRitorno = "class='badge bg-nove text-white'"
            Case "68"
                stringaRitorno = "class='badge bg-dieci text-white'"
            Case "69"
                stringaRitorno = "class='badge bg-undici text-white'"

            Case "71"
                stringaRitorno = "class='badge bg-otto text-dark'"

            Case "72"
                stringaRitorno = "class='badge bg-otto text-dark'"
            Case "75"
                stringaRitorno = "class='badge bg-undici text-white'"
            Case "78"
                stringaRitorno = "class='badge bg-undici text-white'"

            Case "81"
                stringaRitorno = "class='badge bg-otto text-dark'"
            Case "82"
                stringaRitorno = "class='badge bg-undici text-white'"
            Case "84"
                stringaRitorno = "class='badge bg-undici text-white'"

            Case Else
                stringaRitorno = "class='badge bg-secondary text-dark'"




        End Select


        Return stringaRitorno




    End Function
    Public Shared Function statusColor(param As String) As String


        Dim stringaRitorno As String = ""


        Select Case param
            Case "1"
                stringaRitorno = "class='badge bg-uno text-dark'"
            Case "2"
                stringaRitorno = "class='badge bg-due text-dark'"

            Case "3"
                stringaRitorno = "class='badge bg-tre text-dark'"

            Case "4"
                stringaRitorno = "class='badge bg-quattro text-dark'"
            Case "5"
                stringaRitorno = "class='badge bg-cinque text-dark'"
            Case "6"
                stringaRitorno = "class='badge bg-sei text-dark'"
            Case "7"
                stringaRitorno = "class='badge bg-sette text-dark'"
            Case "8"
                stringaRitorno = "class='badge bg-otto text-dark'"
            Case "9"
                stringaRitorno = "class='badge bg-nove text-white'"
            Case "10"
                stringaRitorno = "class='badge bg-dieci text-white'"
            Case "11"
                stringaRitorno = "class='badge bg-undici text-white'"

            Case Else
                stringaRitorno = "class='badge bg-secondary text-dark'"




        End Select


        Return stringaRitorno




    End Function


    Public Shared Function statusColorCorsi(param As String) As String


        Dim stringaRitorno As String = ""


        Select Case param
            Case "51"
                stringaRitorno = "class='badge bg-uno text-dark'"
            Case "54"
                stringaRitorno = "class='badge bg-due text-dark'"

            Case "57"
                stringaRitorno = "class='badge bg-tre text-dark'"

            Case "60"
                stringaRitorno = "class='badge bg-quattro text-dark'"
            Case "63"
                stringaRitorno = "class='badge bg-cinque text-dark'"
            Case "64"
                stringaRitorno = "class='badge bg-sei text-dark'"
            Case "65"
                stringaRitorno = "class='badge bg-sette text-dark'"
            Case "66"
                stringaRitorno = "class='badge bg-quattro text-white'"
            Case "67"
                stringaRitorno = "class='badge bg-nove text-white'"
            Case "68"
                stringaRitorno = "class='badge bg-dieci text-white'"
            Case "69"
                stringaRitorno = "class='badge bg-undici text-white'"

            Case "71"
                stringaRitorno = "class='badge bg-otto text-dark'"

            Case "72"
                stringaRitorno = "class='badge bg-otto text-dark'"
            Case "75"
                stringaRitorno = "class='badge bg-undici text-white'"
            Case "78"
                stringaRitorno = "class='badge bg-undici text-white'"

            Case "81"
                stringaRitorno = "class='badge bg-otto text-dark'"
            Case "82"
                stringaRitorno = "class='badge bg-undici text-white'"
            Case "84"
                stringaRitorno = "class='badge bg-undici text-white'"

            Case Else
                stringaRitorno = "class='badge bg-secondary text-dark'"




        End Select


        Return stringaRitorno




    End Function

    Public Shared Function RichApertaSenzaRighe(codiceRichiesta As String) As Boolean
        Dim fms As FMSAxml = Nothing
        Dim ds As DataSet = Nothing
        Dim righe As Boolean = False

        fms = Conn.Connect()

        '     Dim fmsB = New fmDotNet.FMSAxml(Webserver, Porta, Utente, Password)
        '     fmsB.SetDatabase(Database)
        fms.SetLayout("web_richiesta_dettaglio")
        Dim RequestA = fms.CreateFindRequest(Enumerations.SearchType.Subset)
        RequestA.AddSearchField("Codice_Richiesta", "==" & codiceRichiesta, Enumerations.SearchOption.equals)

        Try
            ds = RequestA.Execute()


            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then

                righe = True

            End If



        Catch ex As Exception
            righe = False
        End Try

        Return righe

    End Function

End Class



