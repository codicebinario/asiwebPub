<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageAA.Master" CodeBehind="homeA.aspx.vb" Inherits="ASIWeb.home" %>
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
   
   
       <a class="btn btn-lg btn-custom" href="albo/dashboardB.aspx" role="button">Corsi</a>
  
</div> 
    </div>
      </div>
  <div class="col">
           <div class="jumbotron jumbotron-fluid">
  <div class="container text-center">
    
    <a class="btn btn-lg btn-custom" href="Equiparazioni/DashboardEqui.aspx" role="button">Equiparazioni</a>
    
  </div>
</div>

  </div>
 
 </div>     


         <div class="row">
     
    <div class="col">
        <div class="jumbotron jumbotron-fluid">
  <div class="container text-center">
  
  <a class="btn btn-lg btn-custom" href="Rinnovi/DashboardRinnovi.aspx" role="button">Rinnovi</a>
    
     
  </div>
</div> 
    </div>

  <div class="col">
        
         <div class="jumbotron jumbotron-fluid">
  <div class="container text-center">
  
 
       <a class="btn btn-lg btn-custom " href="#" role="button">Ristampa</a>
  </div>
</div> 

  </div>
 
 </div>     


            <div class="row">
     
    <div class="col">
        <div class="jumbotron jumbotron-fluid">
  <div class="container text-center">
  
  <a class="btn btn-lg btn-custom" href="GestioneAlbo/DashboardAlbo.aspx" role="button">Consulta Albo</a>
    
     
  </div>
</div> 
    </div>

  <div class="col">
        
        

  </div>
 
 </div>    



  </div>

 

      


</asp:Content>
