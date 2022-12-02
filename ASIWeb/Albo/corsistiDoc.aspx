<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageAlbo.Master" CodeBehind="corsistiDoc.aspx.vb" Inherits="ASIWeb.corsistiDoc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link rel="stylesheet" href="../css/alertify.min.css" />
     <link rel="stylesheet" href="../css/themes/default.min.css" />
      <script type="text/javascript" src="../Scripts/alertify.js"></script>
 
    <script>
        function getPage(url) {


            // this will make a child page popup 


            window.open(url, "MyWindow", "height=375,width=350");
            return false;

        }

    </script>
    <style>

        .photo-img:hover {
    transform:scale(1.05);
   
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        
        <div class="jumbotron jumbotron-fluid rounded">
  <div class="container">
    <h3 class="display-5">Lista Partecipanti al Corso - Documenti</h3>
    <p class="lead">
  <%--<asp:Literal ID="litDenominazioneJumbo" runat="server"></asp:Literal>--%>
       
    <%--     <asp:LinkButton class="nav-link text-white" ID="lnkNuovoExcel" CausesValidation="false" runat="server">Carica l'elenco corretto</asp:LinkButton>
    --%>  <a href="javascript:history.back()" class="btn btn-success btn-sm btn-due">Torna alla pagina precedente</a>
        <%-- <asp:LinkButton class="btn btn-success btn-sm btn-due" ID="lnkDashboardTorna" CausesValidation="false" runat="server">Torna alla pagina precedente</asp:LinkButton>
            --%> 
      
            
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
    <th scope="col">Foto</th>
         <th scope="col">Tessera</th>
         <th scope="col">Diploma</th>
       
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
