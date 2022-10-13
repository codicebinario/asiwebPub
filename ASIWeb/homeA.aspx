<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageAA.Master" CodeBehind="homeA.aspx.vb" Inherits="ASIWeb.home" %>
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
   
   
       <a class="btn btn-lg btn-custom" href="albo/dashboardB.aspx" accesskey="C" role="button"><span class="AccessoKey">C</span>orsi</a>
  
</div> 
    </div>
      </div>
  <div class="col">
           <div class="jumbotron jumbotron-fluid">
  <div class="container text-center">
    
    <a class="btn btn-lg btn-custom" href="Equiparazioni/DashboardEqui.aspx" role="button" accesskey="q">E<span class="AccessoKey">q</span>uiparazioni</a>
    
  </div>
</div>

  </div>
 
 </div>     


         <div class="row">
     
    <div class="col">
        <div class="jumbotron jumbotron-fluid">
  <div class="container text-center">
  
  <a class="btn btn-lg btn-custom" href="Rinnovi/DashboardRinnovi.aspx" role="button" accesskey="R"><span class="AccessoKey">R</span>innovi</a>
    
     
  </div>
</div> 
    </div>

  <div class="col">
        
         <div class="jumbotron jumbotron-fluid">
  <div class="container text-center">
  <a class="btn btn-lg btn-custom" href="GestioneAlbo/DashboardAlbo.aspx" accesskey="A" role="button">Consulta <span class="AccessoKey">A</span>lbo</a>
  
 
    <%--   <a class="btn btn-lg btn-custom " href="#" role="button">Ristampa</a>--%>
  </div>
</div> 

  </div>
 
 </div>     


            <%--<div class="row">
     
    <div class="col">
        <div class="jumbotron jumbotron-fluid">
  <div class="container text-center">
  
    
     
  </div>
</div> 
    </div>

  <div class="col">
        
        

  </div>
 
 </div>--%>    



  </div>

 

      


</asp:Content>
