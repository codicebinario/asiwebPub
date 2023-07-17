<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm5.aspx.vb" Inherits="ASIWeb.WebForm5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtNote" CssClass="form-control" runat="server" MaxLength="1000"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="Button" />

        </div>
    </form>
</body>
</html>
