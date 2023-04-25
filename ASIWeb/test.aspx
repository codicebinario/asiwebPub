<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageMain.Master" CodeBehind="test.aspx.vb" Inherits="ASIWeb.test3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>

           .btn-custom  {
            background-color:darkgray;
            color:white;
          /*  box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.075) inset, 0px 0px 8px rgba(0, 90, 124, 0.5);*/
         width:50%;
        }

           .AccessoKey{

    text-decoration:underline;
    color: darkred;
         }


    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">

        <div class="row">

            <div class="col">
                <div class="jumbotron jumbotron-fluid">
                    <div class="container text-center">


                        <a class="btn btn-lg btn-custom " href="dashboard.aspx" role="button" accesskey="T"><i class="bi bi-card-text"></i>Tesseramento</a>

                    </div>
                </div>
            </div>
            <div class="col">
                <div class="jumbotron jumbotron-fluid">
                    <div class="container text-center">


                        <a class="btn btn-lg btn-custom" href="HomeA.aspx" role="button" accesskey="A"><i class="bi bi-book"></i>Albo</a>
                    </div>
                </div>

            </div>

        </div>









    </div>

</asp:Content>
