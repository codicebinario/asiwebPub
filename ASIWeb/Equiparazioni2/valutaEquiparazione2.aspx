<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageEqui2.Master" CodeBehind="valutaEquiparazione2.aspx.vb" Inherits="ASIWeb.valutaEquiparazione2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../css/alertify.min.css" />
    <link rel="stylesheet" href="../css/themes/default.min.css" />
    <script type="text/javascript" src="../Scripts/alertify.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-kenU1KFdBIe4zVF0s0G1M5b4hcpxyD9F7jL+jjXkk+Q2h455rYXK/7HAuoJl+0I4" crossorigin="anonymous"></script>

    <style>
    .custom-file-input.selected:lang(en)::after {
      content: "" !important;
    }
    .custom-file {
      overflow: hidden;
    }
    .custom-file-input {
      white-space: nowrap;
    }
    .legacy{
    color:white;
}
       .moltopiccolo {
      font-size:small;
  }
    a:hover {
 color:white;
    }
  .btn-tre {
    background-color:#fff;
    border-color: #ff5308;

}
    .btn-custom  {
            background-color:darkgray;
            color:white;
          /*  box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.075) inset, 0px 0px 8px rgba(0, 90, 124, 0.5);*/
         width:50%;
        }
    .errore { color:red;


    }
	 .btn-custom2 {
            width:140px;


        }
 .Progress
 {
  position: fixed;
  top: 50%;
  left: 50%;
  /* bring your own prefixes */
  transform: translate(-50%, -50%);
  background:red;
  border-color:brown;
  border-style:solid;
  border-width:thin;
   height: 100px;
	width: 200px;
   color:white;
   z-index:1;
	display: flex;
  justify-content: center;
  align-items: center;
 }

  </style>
	<script>
$(document).ready(function(){
  $('[data-toggle="tooltip"]').tooltip();
});
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="jumbotron jumbotron-fluid rounded">
  <div class="container">
   
      <h6 class="fs-5 text-white text-decoration-none">Valutazione Equiparazione</h6>
    <p class="lead">
  <%--<asp:Literal ID="litDenominazioneJumbo" runat="server"></asp:Literal>--%>
                      <asp:LinkButton class="btn btn-success btn-sm btn-due" ID="lnkDashboardTorna" CausesValidation="false" runat="server">Torna alla pagina precedente</asp:LinkButton>

    </p>
  </div></div>
      <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-12">
								<div class="form-group">

                                 <asp:Label ID="lblIntestazioneEquiparazioni" runat="server" Text=""></asp:Label>
                                    <asp:HiddenField ID="HiddenIdRecord" runat="server" />
                                      <asp:HiddenField ID="HiddenIDEquiparazione" runat="server" />  
                                   
                                   
						</div>
							</div>
							
				</div></div>	
      
    
      <div class="col-sm-12">
          <div class="row">
              <div class="col-sm-12">
                  <div class="form-group">
                      <h5>Valutazione Equiparazione</h5>
                      <hr />
                  </div>
              </div>

          </div>
      </div>

     <div class="col-sm-12">
						<div class="row">
							
							<div class="col-sm-4">
								<div class="form-group">
									<label for="txtCognome">Valutazione [*]</label>
                                
					 <asp:dropdownlist id="ddlValutazione" runat="server" Cssclass="form-control input-sm">
                          <asp:ListItem Value="##">##</asp:ListItem>
                         <asp:ListItem Value="S">positivo</asp:ListItem>
                        <asp:ListItem Value="N">negativo</asp:ListItem>
					 </asp:dropdownlist>
								
								</div>
							</div>
						
                         <div class="col-sm-8">
								<div class="form-group">
									<label for="txtnote">Note [*]</label>
                                         <asp:textbox id="txtNote" Cssclass="form-control" runat="server"  maxlength="250" ></asp:textbox>
				
								</div>
							</div>
                        
                        </div>
                             
                </div>

       <div class="col-sm-12">
						<div class="row">
							
							<div class="col-sm-4">
								<div class="form-group">
								  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="##" 
										CssClass="errore" ErrorMessage="Valutazione" ControlToValidate="ddlValutazione" 
										EnableClientScript="true"></asp:RequiredFieldValidator>
								
								</div>
							</div>
						
                         <div class="col-sm-8">
								<div class="form-group">
								  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
										CssClass="errore" ErrorMessage="note valutazione" ControlToValidate="txtNote" 
										EnableClientScript="true"></asp:RequiredFieldValidator>
								
								</div>
							</div>
                        
                        </div>
                             
                </div>


      <div class="col-sm-12">
						<div class="row">
							
							<div class="col-sm-4">
								<div class="form-group">
									<label for="txtCognome">Diritti Segreteria [*]</label>
                                
					 <asp:dropdownlist id="ddlDirittiSegreteria" runat="server" Cssclass="form-control input-sm">
                          <asp:ListItem Value="##">##</asp:ListItem>
                         <asp:ListItem Value="0">0</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
                          <asp:ListItem Value="20">20</asp:ListItem>
                          <asp:ListItem Value="30">30</asp:ListItem>
					 </asp:dropdownlist>
								
								</div>
							</div>
						
                         <div class="col-sm-8">
								<div class="form-group">
									
								</div>
							</div>
                        
                        </div>
                             
                </div>

     <div class="col-sm-12">
						<div class="row">
							
							<div class="col-sm-4">
								<div class="form-group">
								  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="##" 
										CssClass="errore" ErrorMessage="Diritti Segreteria" ControlToValidate="ddlDirittiSegreteria" 
										EnableClientScript="true"></asp:RequiredFieldValidator>
								
								</div>
							</div>
						
                         <div class="col-sm-8">
								<div class="form-group">
								
								
								</div>
							</div>
                        
                        </div>
                             
                </div>


     <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-12">
								<div class="form-group">
                                   
<%--						            <asp:Button ID="btnValuta" runat="server" Text="Valuta" class="btn btn-primary"    />--%>
                                 <asp:LinkButton ID="lnkButton1" class="btn btn-primary" Visible="true"  runat="server"><i class="bi bi-check2-circle"> </i>Valutazione</asp:LinkButton>  
						</div>
							</div>
							
				</div></div>	
</asp:Content>
