Imports RestSharp

Public Class WebForm4
    Inherits System.Web.UI.Page

    Protected Async Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim risposta As String = getToken()

        Response.Write(risposta)

    End Sub
    Function getToken() As String
        Dim client As New RestClient("http://www.infocar-repair.it")
        Dim request As New RestRequest("/Gateway/PersistentrequestServiceJEstimate.asmx", Method.POST)
        request.AddHeader("Password", "Rws23-b4LsYst3")
        request.AddHeader("Content-Type", "application/soap+xml; charset=utf-8")
        request.AddHeader("host", "www.infocar-repair.it")
        request.AddHeader("Cookie", "domu06web_cookie=rd1o00000000000000000000ffff0a2e104ao80")
        Dim body As String =
            "<?xml version=""1.0"" encoding=""utf-8""?>" + vbCrLf +
            "<soap12:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">" + vbCrLf +
            "  <soap12:Header>" + vbCrLf +
            "    <AuthHeader xmlns=""http://jestimategateway.services.edidomus/"">" + vbCrLf +
            "      <UserName>repairws01@9101451777.ed</UserName>" + vbCrLf +
            "      <Password>Rws23-b4LsYst3</Password>" + vbCrLf +
            "      <Product>REPAIR</Product>" + vbCrLf +
            "    </AuthHeader>" + vbCrLf +
            "  </soap12:Header>" + vbCrLf +
            "  <soap12:Body>" + vbCrLf +
            "    <GetAuthenticationToken xmlns=""http://jestimategateway.services.edidomus/"">" + vbCrLf +
            "      <id>1</id>" + vbCrLf +
            "      <xml>" + vbCrLf +
            "       <![CDATA[<flussoUC1Input>" + vbCrLf +
            "<numeroSinistro>A1123</numeroSinistro>" + vbCrLf +
            "<idMessaggio>123456</idMessaggio>" + vbCrLf +
             "<idPreventivo>876565</idPreventivo>" + vbCrLf +
            "<versionePreventivo>1</versionePreventivo>" + vbCrLf +
            "<userOwner>NOME_PORTALE</userOwner>" + vbCrLf +
            "<actualUser>carrozzeriarossi@rossi.net</actualUser>" + vbCrLf +
            "<parametriServizio>" + vbCrLf +
            "<callBackUrl>http://www.url-di-callback.it</callBackUrl>" + vbCrLf +
            "<webServiceUrl>http://www.servizio-di-callback.it/ResponseService.asmx</webServiceUrl>" + vbCrLf +
            "</parametriServizio>" + vbCrLf +
            "</flussoUC1Input>]]>" + vbCrLf +
            "      </xml>" + vbCrLf +
            "    </GetAuthenticationToken>" + vbCrLf +
            "  </soap12:Body>" + vbCrLf +
            "</soap12:Envelope>"
        request.AddParameter("application/soap+xml; charset=utf-8", body, ParameterType.RequestBody)
        Dim response As RestResponse = client.Execute(request)

        Return response.Content
        '  Console.WriteLine(response.Content)






    End Function
End Class