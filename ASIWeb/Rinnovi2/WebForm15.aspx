<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm15.aspx.vb" Inherits="ASIWeb.WebForm15" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>

                        <td><%#Eval("IdRinnovo") %></td>
                    </tr>

                </ItemTemplate>
            </asp:Repeater>

            </table>
        </div>
    </form>
</body>
</html>
