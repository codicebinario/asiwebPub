<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm14.aspx.vb" Inherits="ASIWeb.WebForm14" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-kenU1KFdBIe4zVF0s0G1M5b4hcpxyD9F7jL+jjXkk+Q2h455rYXK/7HAuoJl+0I4" crossorigin="anonymous"></script>

    <title></title>
    <style>
        .accordion-button{
            background-color: #eeeee4;

        }

        .accordion-button:not(.collapsed) {
  color: white;
    background-color:#137dad;
/*    box-shadow: inset 0 -1px 0 rgb(0 0 0 / 13%);
    border-left: 2em solid red;*/
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:PlaceHolder ID="phDash" runat="server" Visible="true"></asp:PlaceHolder>
        </div>
    </form>
</body>
</html>
