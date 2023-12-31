﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageAlbo.Master" CodeBehind="corsistiKO.aspx.vb" Inherits="ASIWeb.corsistiKO" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link rel="stylesheet" href="../css/alertify.min.css" />
     <link rel="stylesheet" href="../css/themes/default.min.css" />
      <script type="text/javascript" src="../Scripts/alertify.js"></script>
 <style>
     .colorerosso 
{
    color:red;

}
 </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        
        <div class="jumbotron jumbotron-fluid rounded">
  <div class="container">
    <h3 class="display-5">Lista Partecipanti al Corso non valida</h3>
    <p class="lead">
  <%--<asp:Literal ID="litDenominazioneJumbo" runat="server"></asp:Literal>--%>
       
         <asp:LinkButton class="btn btn-success btn-sm btn-due" ID="lnkNuovoExcel" CausesValidation="false" runat="server">Carica l'elenco corretto</asp:LinkButton>
           <asp:LinkButton class="btn btn-success btn-sm btn-due" ID="lnkDashboardTorna" CausesValidation="false" runat="server">Torna alla pagina precedente</asp:LinkButton>
        
    
            
         
      
            
    </p>
    
  </div></div>
      <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-12">
								<div class="form-group">
                                    <asp:Label ID="lblIntestazioneCorso" runat="server" Text=""></asp:Label>
                                    <asp:HiddenField ID="HiddenIdRecord" runat="server" />
                                      <asp:HiddenField ID="HiddenIDCorso" runat="server" />
						           
						</div>
							</div>
							
				</div></div>	
 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
     <ContentTemplate>
                                  
    <div class="col-sm-12">

        <table class="table table-hover">
  <thead>
    <tr>
   <%--   <th scope="col">#</th>--%>
      <th scope="col">Nome</th>
      <th scope="col">Cognome</th>
      <th scope="col">Email</th>
     <th scope="col">C.F.</th>
   <th scope="col">N.Tessera ASI</th>
      <th scope="col">Indirizzo Spedizione</th>
    <th scope="col">Motivo</th>
       
    </tr>
  </thead>
<tbody>
    <asp:PlaceHolder ID="plTabellaCorsisti" runat="server"></asp:PlaceHolder>

    </tbody>
            </table>


   


    </div>

     </ContentTemplate>
       </asp:UpdatePanel>

</asp:Content>
