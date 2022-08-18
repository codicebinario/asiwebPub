<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageMain.Master" CodeBehind="home.aspx.vb" Inherits="ASIWeb.home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>

           .btn-custom  {
            background-color:darkgray;
            color:white;
          /*  box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.075) inset, 0px 0px 8px rgba(0, 90, 124, 0.5);*/
         width:50%;
        }

          

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container">
  
  <div class="row">
     
    <div class="col">
        <div class="jumbotron jumbotron-fluid">
  <div class="container text-center">
   
   
       <a class="btn btn-lg btn-custom " href="dashboard.aspx" role="button">Tesseramento</a>
  
</div> 
    </div>
      </div>
  <div class="col">
           <div class="jumbotron jumbotron-fluid">
  <div class="container text-center">
    
  
      <a class="btn btn-lg btn-custom" href="HomeA.aspx" role="button">Albo</a>
  </div>
</div>

  </div>
 
 </div>     









  </div>

 

      


</asp:Content>
