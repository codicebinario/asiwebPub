<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm5.aspx.vb" Inherits="ASIWeb.WebForm5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
            <asp:RadioButton ID="RadioButton1" runat="server" GroupName="CF" Text="A" />
            <asp:RadioButton ID="RadioButton2" runat="server" GroupName="CF" Text="B" />
            <asp:RadioButton ID="RadioButton3" runat="server" GroupName="CF" Text="C" />

        </div>
    </form>
</body>
</html>
<asp:CheckBox runat="server" OnCheckedChanged="Unnamed1_CheckedChanged"></asp:CheckBox>
