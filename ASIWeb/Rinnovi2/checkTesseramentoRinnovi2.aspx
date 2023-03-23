<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageRinnovi2.Master" CodeBehind="checkTesseramentoRinnovi2.aspx.vb" Inherits="ASIWeb.checkTesseramentoRinnovi2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
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
	  .avviso{
	color:darkred;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasRight" aria-labelledby="offcanvasRightLabel">
        <div class="offcanvas-header">
            <h5 class="offcanvas-title" id="offcanvasRightLabel">Controllo Tesseramento</h5>
            <button type="button" class="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>
        </div>
        <div class="offcanvas-body">
            <p>
              Questa è la prima pagina del veloce processo del rinnovo. In questa fase si effettua il controllo
				del codice fiscale a fini assicurativi sul contenitore tesseramenti.
            </p>
			<p>
				Se il tesserato appartiene ad un altro Ente, sarà richiesto di caricare la documentazione di cambio EA. Se si è
				a conoscenza di questa evenienza, è meglio preparare il documento (formato PDF) da caricare e poi tornare al processo.
			</p>
           
        </div>
    </div>
	 <div class="jumbotron jumbotron-fluid rounded">
  <div class="container">
      <h6 class="fs-5"><a class="text-white text-decoration-none" data-bs-toggle="offcanvas" href="#offcanvasRight" role="button" aria-controls="offcanvasRight">Controllo Tesseramento (info)
      </a></h6>
	<p class="lead">
  <%--<asp:Literal ID="litDenominazioneJumbo" runat="server"></asp:Literal>--%>

        <asp:LinkButton class="btn btn-success btn-sm btn-due" ID="lnkDashboardTorna" CausesValidation="false" runat="server">Torna alla pagina precedente</asp:LinkButton>
	
	</p>
	
  </div></div>

	  <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-12">
								<div class="form-group">
									<asp:Label ID="lblIntestazioneRinnovo" runat="server" Text=""></asp:Label>
									<asp:HiddenField ID="HiddenIdRecord" runat="server" />
									  <asp:HiddenField ID="HiddenIDRinnovo" runat="server" />
						</div>
							</div>
							
				</div></div>
    <div class="col-sm-12">
        <div class="row">
    <div class="alert alert-danger" role="alert">
        Se il tesserato appartiene ad altro Ente, sarà richiesto di caricare la documentazione di cambio EA.
    </div>
			</div>
	</div>
	 <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">
								 <label for="txtNome">Codice Fiscale [*] </label>
									<asp:textbox id="txtCodiceFiscale" Cssclass="form-control" runat="server"  maxlength="250" ></asp:textbox>
							
					</div>
							</div>
							<div class="col-sm-6">
								<div class="form-group">
							
									
								
								</div>
							</div>
						
						 
						
						</div>
							 
				</div>

		   <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">

									<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
										CssClass="errore" ErrorMessage="Codice Fiscale " ControlToValidate="txtCodiceFiscale" 
										EnableClientScript="true"></asp:RequiredFieldValidator>
  
									</div>
							</div>
							<div class="col-sm-6">
								<div class="form-group">
							
								</div>
							</div>
						
						 
						
						</div>
							 
				</div>
	<div class="col-sm-12">
						<div class="row">
							<div class="col-sm-12">
								<div class="form-group">
								   
									<asp:Button ID="btnCheck" runat="server" Text="Controlla" class="btn btn-primary"/>
								
						</div>
							</div>
							
				</div></div>	



	 <div class="col-sm-12">
		  <div class="row">
			  <div class="col-sm-12">
				  <div class="form-group">
						<hr />
					   <h5><asp:Label ID="lblRisultato" runat="server" Text=""></asp:Label></h5>
					  <hr />
				  </div>
			  </div>

		  </div>
	  </div>
	<br /><br />


</asp:Content>
