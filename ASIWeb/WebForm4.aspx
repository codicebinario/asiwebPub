<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm4.aspx.vb" Inherits="ASIWeb.WebForm4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
 <script>
     function GotPDF(data) {
         alert('cia');
         // Here, data conta;ins "%PDF-1.4 ..." etc.
         var datauri = 'data:application/pdf;base64,' + Base64.encode(data);
         var win = window.open("", "Your PDF", "width=1024,height=768,resizable=yes,scrollbars=yes,toolbar=no,location=no,directories=no,status=no,menubar=no,copyhistory=no");
         win.document.location.href = datauri;
     }


 </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>   <asp:LinkButton ID="lnkPdf" runat="server" OnClientClick="poop();">LinkButton</asp:LinkButton>
            <asp:HyperLink ID="HyperLink1" runat="server">HyperLink</asp:HyperLink>
            <asp:Image ID="Image1" runat="server" /><asp:PlaceHolder ID="plPdf" runat="server"></asp:PlaceHolder>
        </div>
    </form>
    
</body>
</html>
