<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm2.aspx.vb" Inherits="ASIWeb.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-control-plaintext">
            <label class="form-check-label" for="txtNote">Note</label>
            <asp:TextBox ID="txtNote" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">



            <div class="custom-file">

                <label for="customFileInput" class="form-label"></label>
                <asp:FileUpload ID="FileUpload1" runat="server" class="form-control" name="myFile1" />

            </div>
            <div>
                <asp:LinkButton ID="lnkButton1" class="btn btn-primary ml-2" Visible="true" runat="server"><i class="bi bi-upload"> </i>Carica</asp:LinkButton>
            </div>

            <div class="mb-3">
            </div>
            <div id="results" runat="server"></div>

        </div>



        <br />

        <div class="col-sm-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <asp:Label runat="server" ID="uploadedFiles" Text="" />
                        <hr />
                    </div>
                </div>

            </div>
        </div>
    </form>
</body>
</html>
