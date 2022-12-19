<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageRinnovi.Master" CodeBehind="DashBoardRinnovi.aspx.vb" Inherits="ASIWeb.DashBoardRinnovi1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../css/alertify.min.css" />
    <link rel="stylesheet" href="../css/themes/default.min.css" />
    <script type="text/javascript" src="../Scripts/alertify.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-kenU1KFdBIe4zVF0s0G1M5b4hcpxyD9F7jL+jjXkk+Q2h455rYXK/7HAuoJl+0I4" crossorigin="anonymous"></script>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12 mb-3 mb-md-0">
            <div class="jumbotron jumbotron-custom jumbotron-fluid rounded">
                <div class="container">
                    <h3 class="display-5">Rinnovi Attivi</h3>
                    <p class="lead">
                        <asp:Literal ID="litDenominazioneJumboDash" runat="server"></asp:Literal>








                    </p>
                </div>
            </div>


        </div>




    </div>

    <div class="row d-flex justify-content-center">

        <asp:PlaceHolder ID="phDash" runat="server" Visible="false"></asp:PlaceHolder>



        </div>
</asp:Content>
