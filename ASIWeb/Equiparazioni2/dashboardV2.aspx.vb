﻿Imports fmDotNet
Imports ASIWeb.AsiModel
Imports ASIWeb.Ed
Imports System.Net

Public Class dashboardV2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("auth") = "0" Or IsNothing(Session("auth")) Then
            Response.Redirect("../login.aspx")
        End If
        If IsNothing(Session("codice")) Then
            Response.Redirect("../login.aspx")
        End If
        Dim webserver As String = ConfigurationManager.AppSettings("webserver")
        Dim utente As String = ConfigurationManager.AppSettings("utente")
        Dim porta As String = ConfigurationManager.AppSettings("porta")
        Dim pass As String = ConfigurationManager.AppSettings("pass")
        Dim dbb As String = ConfigurationManager.AppSettings("dbb")
        Dim cultureFormat As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("it-IT")
        Dim deEnco As New Ed()

        Dim ris As String = Request.QueryString("ris")

        Dim showScript As String = ""
        Dim customizeScript As String = " 
            toastr.options = {
              'closeButton': true,
              'debug': false,
              'newestOnTop': false,
              'progressBar': false,
              'positionClass': 'toast-top-right',
              'preventDuplicates': true,   
              'onclick': null,
              'timeOut': 5000,
              'showDuration': 1000,
              'hideDuration': 1000,
              'extendedTimeOut': 1000,
              'showEasing': 'swing',
              'hideEasing': 'linear',
              'showMethod': 'fadeIn',
              'hideMethod': 'fadeOut'
        };
        "
        If Not Page.IsPostBack Then

            If Not Session("AnnullaREqui") Is Nothing Then

                If Session("AnnullaREqui") = "valSettoreS" Then

                    showScript = "toastr.success('Valutazione positiva effettuata', 'ASI');"
                    Session("AnnullaREqui") = Nothing
                ElseIf Session("AnnullaREqui") = "valSettoreN" Then
                    showScript = "toastr.success('Valutazione negativa effettuata', 'ASI');"
                    Session("AnnullaREqui") = Nothing
                End If

                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "showSuccess", customizeScript & showScript, True)


            End If




            Equiparazioni()





        End If
    End Sub

    Sub Equiparazioni()
        Dim ds As DataSet
        Dim pdf As String
        Dim fmsP As FMSAxml = AsiModel.Conn.Connect()
        fmsP.SetLayout("webEquiparazioniRichiestaMolti")
        Dim RequestP = fmsP.CreateFindRequest(Enumerations.SearchType.Subset)
        ' RequestP.AddSearchField("pre_stato_web", "1")
        RequestP.AddSearchField("Equi_Settore_Approvazione_ID", Session("codice"), Enumerations.SearchOption.equals)
        RequestP.AddSortField("IDEquiparazioneM", Enumerations.Sort.Descend)

        RequestP.AddSortField("IDrecord", Enumerations.Sort.Descend)

        Try



            ds = RequestP.Execute()

            If Not IsNothing(ds) AndAlso ds.Tables("main").Rows.Count > 0 Then






                phDash.Visible = True
                'Dim counter As Integer = 0
                Dim counter1 As Integer = 0
                'Dim totale As Decimal = 0
                For Each dr In ds.Tables("main").Rows




                    If Data.FixNull(dr("Codice_Status")) = "105" Then
                        counter1 += 1

                        Dim deEnco As New Ed()
                        Dim nomeFile As String
                        Dim diploma As String


                        nomeFile = Data.FixNull(dr("NomeFileDiplomaFS"))

                        If String.IsNullOrWhiteSpace(Data.FixNull(dr("DiplomaEquiparazione"))) Then
                            diploma = "..\img\noPdf.jpg"
                        Else
                            diploma = "https://93.63.195.98" & Data.FixNull(dr("DiplomaEquiparazione"))
                        End If


                        ' da valutare il diploma sia caricato su Fm,

                        'If String.IsNullOrWhiteSpace(nomeFile) Then
                        '    nomeFile = Data.FixNull(dr("NomeFileOnFSFromFM"))
                        'End If

                        Dim Ann As New LinkButton

                        Ann.ID = "ann_" & counter1
                        Ann.Attributes.Add("runat", "server")
                        Ann.Text = "<i class=""bi bi-bookmark-check""> </i>Valuta Equiparazione"
                        Ann.PostBackUrl = "valutaEquiparazione2.aspx?IdEquiparazioneM=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("IDEquiparazioneM")))) & "&codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("IDrecord")))) & "&record_ID=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(dr("idrecord")))
                        Ann.CssClass = "btn btn-success btn-sm btn-uno btn-custom mb-2"
                        '    Ann.Attributes.Add("OnClick", "if(!myValuta())return false;")





                        'Dim PianoCorso As New LinkButton

                        'PianoCorso.ID = "vediPianoCorso_" & counter1
                        'PianoCorso.Attributes.Add("runat", "server")
                        'PianoCorso.Text = "<i class=""bi bi-box-arrow-down""> </i>Scarica Diploma"
                        ''  PianoCorso.PostBackUrl = "scaricaPianoCorso.aspx?codR=" & deEnco.QueryStringEncode(Data.FixNull(dr("IDCorso"))) & "&record_ID=" & deEnco.QueryStringEncode(dr("id_record")) & "&nomeFilePC=" & deEnco.QueryStringEncode(dr("NomeFileOnFS"))
                        'PianoCorso.PostBackUrl = "scaricaDiplomaEqui.aspx?codR=" & WebUtility.UrlEncode(deEnco.QueryStringEncode(Data.FixNull(dr("IDEquiparazione"))) & "&record_ID=" & deEnco.QueryStringEncode(dr("id_record")) & "&nomeFilePC=" & deEnco.QueryStringEncode(nomeFile))

                        'PianoCorso.CssClass = "btn btn-success btn-sm btn-due btn-custom mb-2"
                        ' PianoCorso.Attributes.Add("OnClick", "if(!myValuta())return false;")


                        phDash.Controls.Add(New LiteralControl("<div class=""col-sm-10 mb-3 mb-md-0"">"))



                        'accordion card
                        phDash.Controls.Add(New LiteralControl("<div class=""card mb-2 shadow-sm rounded"">"))
                        'accordion heder
                        phDash.Controls.Add(New LiteralControl("<div class=""card-header"">"))

                        phDash.Controls.Add(New LiteralControl("<div Class=""container-fluid"">"))

                        ' inizio prima riga

                        phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))


                        phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left moltopiccolo"">"))

                        phDash.Controls.Add(New LiteralControl("Equiparazione:  "))
                        phDash.Controls.Add(New LiteralControl("<span  " & Utility.statusColorCorsi(Data.FixNull(dr("Codice_Status"))) & ">"))
                        phDash.Controls.Add(New LiteralControl("<a name=" & Data.FixNull(dr("IDRecord")) & ">" & Data.FixNull(dr("IDRecord")) & "</a>"))
                        phDash.Controls.Add(New LiteralControl(" della Richiesta: " & Data.FixNull(dr("IDEquiparazioneM"))))

                        phDash.Controls.Add(New LiteralControl("</span><br />"))


                        phDash.Controls.Add(New LiteralControl("Nominativo: <small>" & Data.FixNull(dr("Equi_Nome")) & " " & Data.FixNull(dr("Equi_Cognome")) & "</small><br />"))

                        phDash.Controls.Add(New LiteralControl("CF: <small>" & Data.FixNull(dr("Equi_CodiceFiscale")) & "</small><br />"))
                        phDash.Controls.Add(New LiteralControl("Data Scadenza: <small>" & Data.SonoDieci(Data.FixNull(dr("Equi_DataScadenza"))) & "</small><br />"))

                        'Dim dataMan As DateTime = Data.FixNull(dr("DataOraTrasmessoSettore"))
                        'Dim dataGiorni = dataMan.AddDays(5)

                        'Response.Write(dataGiorni - dataMan)


                        phDash.Controls.Add(New LiteralControl("Data Trasmissione: <small>" & Data.FixNull(dr("DataOraTrasmessoSettore")) & "</small><br />"))
                        phDash.Controls.Add(New LiteralControl("<span  " & Utility.statusColorCorsi(Data.FixNull(dr("Codice_Status"))) & ">Scad. val.: <small>" & Data.FixNull(dr("ScadenzaSettoreQuantoMancaOre")) & "</small></span><br />"))

                        phDash.Controls.Add(New LiteralControl())

                        ' phDash.Controls.Add(New LiteralControl("</span>"))

                        phDash.Controls.Add(New LiteralControl("</div>"))


                        phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4  text-left moltopiccolo"">"))

                        phDash.Controls.Add(New LiteralControl("</span><small>Status: </small><small " & Utility.statusColorTextCorsi(Data.FixNull(dr("Codice_Status"))) & ">" & Data.FixNull(dr("Descrizione_StatusWeb")) & "</small>"))

                        phDash.Controls.Add(New LiteralControl("</div>"))


                        phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-right"">"))

                        '   phDash.Controls.Add(PianoCorso)
                        phDash.Controls.Add(Ann)

                        If diploma = "..\img\noPdf.jpg" Then
                            '     phDash10.Controls.Add(New LiteralControl("<td><img src='" & tessera & "' height='70' width='70' alt='" & Data.FixNull(dr("Asi_Nome")) & " " & Data.FixNull(dr("Asi_Cognome")) & "'></td>"))


                        Else
                            phDash.Controls.Add(New LiteralControl("<a class=""btn btn-success btn-sm btn-due btn-custom mb-2"" onclick=""showToast();"" target=""_blank"" href='scaricaDiplomaEqui2.aspx?codR=" & deEnco.QueryStringEncode(Data.FixNull(dr("IDRecord"))) & "&record_ID=" & deEnco.QueryStringEncode(dr("idrecord")) & "&nomeFilePC=" _
                                 & deEnco.QueryStringEncode(Data.FixNull(dr("NomeFileDiplomaFS"))) & "&nominativo=" _
                                 & deEnco.QueryStringEncode(Data.FixNull(dr("Equi_Cognome")) & "_" & Data.FixNull(dr("Equi_Nome"))) & "'><i class=""bi bi-person-badge""> </i>Documentazione Presentata</a>"))


                        End If





                        phDash.Controls.Add(New LiteralControl("</div>"))





                        phDash.Controls.Add(New LiteralControl("</div>"))




                        phDash.Controls.Add(New LiteralControl("<hr>"))


                        phDash.Controls.Add(New LiteralControl("<div Class=""row"">"))


                        phDash.Controls.Add(New LiteralControl("<div Class=""col-sm-4 text-left moltopiccolo"">"))





                        phDash.Controls.Add(New LiteralControl("Sport: <small>" & Data.FixNull(dr("Equi_Sport_Interessato")) & "</small><br />"))

                        phDash.Controls.Add(New LiteralControl("Disciplina: <small>" & Data.FixNull(dr("Equi_Disciplina_Interessata")) & "</small><br />"))
                        phDash.Controls.Add(New LiteralControl("Specialità: <small>" & Data.FixNull(dr("Equi_Specialita")) & "</small><br />"))
                        phDash.Controls.Add(New LiteralControl("Livello: <small>" & Data.FixNull(dr("Equi_Livello")) & "</small><br />"))
                        phDash.Controls.Add(New LiteralControl("Qualifica da Rilasciare: <small>" & Data.FixNull(dr("Equi_Qualifica_Tecnica_Da_Rilasciare")) & "</small><br />"))
                        phDash.Controls.Add(New LiteralControl("Qualifica DT:  "))

                        phDash.Controls.Add(New LiteralControl("<small>" & Data.FixNull(dr("Dicitura_Qualifica_DT")) & "</small><br />"))

                        ' phDash.Controls.Add(New LiteralControl("</span>"))

                        phDash.Controls.Add(New LiteralControl("</div>"))






                        phDash.Controls.Add(New LiteralControl("</div>"))






                        counter1 += 1
                        phDash.Controls.Add(New LiteralControl("</div>"))

                        phDash.Controls.Add(New LiteralControl("</div>"))

                        phDash.Controls.Add(New LiteralControl("</div>"))
                        phDash.Controls.Add(New LiteralControl("</div>"))


                    End If




                Next

            End If
        Catch ex As Exception
            AsiModel.LogIn.LogErrori(ex, "dashboardV2", "equiparazioni")
            Response.Redirect("../FriendlyMessage.aspx", False)
        End Try
    End Sub

End Class