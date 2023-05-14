<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageAlbo.Master" CodeBehind="corsisti.aspx.vb" Inherits="ASIWeb.corsisti" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link rel="stylesheet" href="../css/alertify.min.css" />
     <link rel="stylesheet" href="../css/themes/default.min.css" />
      <script type="text/javascript" src="../Scripts/alertify.js"></script>
    <style>

        .photo-img:hover {
    transform:scale(2);
   
}
         .celladue {
        border-bottom: 1pt solid #ff000d;
      }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        
        <div class="jumbotron jumbotron-fluid rounded">
  <div class="container">
    <h3 class="display-5">Lista Partecipanti al Corso</h3>
    <p class="lead">
  <%--<asp:Literal ID="litDenominazioneJumbo" runat="server"></asp:Literal>--%>
 <%--<asp:LinkButton class="btn btn-success btn-sm btn-due" ID="lnkTornaDashboard" CausesValidation="false" runat="server">Corsi Attivi</asp:LinkButton>
 --%>      <a href="dashboardB.aspx" class="btn btn-success btn-sm btn-due">Interrompi il caricamento foto.</a>
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

        <table class="table table-hover small">
  <thead>
    <tr>
   <%--   <th scope="col">#</th>--%>
      <th scope="col">Nome</th>
      <th scope="col">Cognome</th>
      <th scope="col">Email</th>
      <th scope="col">C.F.</th>
      <th scope="col">N.Tessera ASI</th>
    <%--  <th scope="col">Indirizzo Spedizione</th>--%>
      <th scope="col">Foto</th>
         
         <th scope="col">##</th>
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
