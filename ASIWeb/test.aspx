<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPage.Master" CodeBehind="test.aspx.vb" Inherits="ASIWeb.test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <script type="text/javascript">
            $(document).ready(function () {
                $("#btnCalculate").click(function () {
                    //Get values from textboxes that will be passed to function
                    var num1 = $('#txtFirstNumber').val();
                    var num2 = $('#txtSecondNumber').val();
                    $.ajax({
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        //Url is the path of our web method (Page name/function name)
                        url: "test.aspx/CalculateSum",
                        //Pass values to parameters. If function have no parameters, then we need to use data: "{ }",
                        data: "{'Num1':'" + num1 + "', 'Num2':'" + num2 + "'}",
                        //called on ajax call success        
                        success: function (result) {
                            $('#dvMsg').text("Sum of " + num1 + " and " + num2 + " = " + result.d);
                        },
                        //called on ajax call failure
                        error: function (xhr, textStatus, error) {
                            $('#dvMsg').text("Error:" + error);
                        }
                    });
                });
            });
        </script>
        <div>
            <table>
                <tr>
                    <td>Enter First Number:</td>
                    <td>
                        <asp:TextBox ID="txtFirstNumber" runat="server" /></td>
                </tr>
                <tr>
                    <td>Enter Second Number:</td>
                    <td>
                        <asp:TextBox ID="txtSecondNumber" runat="server" /></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnCalculate" Text="Calculate" runat="server" OnClientClick="return false;" />
                        <div id="dvMsg"></div>
                    </td>
                </tr>
            </table>
        </div>
</asp:Content>
