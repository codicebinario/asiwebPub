<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageEqui2.Master" CodeBehind="checkTesseramento3.aspx.vb" Inherits="ASIWeb.checkTesseramento3" %>
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
     <div class="jumbotron jumbotron-fluid rounded">
  <div class="container">
      <h6 class="fs-5 text-white text-decoration-none">Controllo Tesseramento CF Estero</h6>

    <p class="lead">
  <%--<asp:Literal ID="litDenominazioneJumbo" runat="server"></asp:Literal>--%>
            <a href="javascript:history.back()" class="btn btn-success btn-sm btn-due"><i class="bi bi-skip-backward-btn"> </i>Torna alla pagina precedente</a>     
    </p>
    
  </div></div>

      <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-12">
								<div class="form-group">
                                    <asp:Label ID="lblIntestazioneEquiparazione" runat="server" Text=""></asp:Label>
                                    <asp:HiddenField ID="HiddenIdRecord" runat="server" />
                                      <asp:HiddenField ID="HiddenIDEquiparazione" runat="server" />
						</div>
							</div>
							
				</div></div>	
    
	                <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-3">
								<div class="form-group">
								 <label for="txtNome">Nome [*] </label>
                                    <asp:textbox id="txtNome" Cssclass="form-control" runat="server"  maxlength="250" ></asp:textbox>
						        </div>
						          	</div>
							<div class="col-sm-3">
								<div class="form-group">
                                    <label for="txtNome">Cognome [*] </label>
                                    <asp:TextBox ID="txtCognome" CssClass="form-control" runat="server" MaxLength="250"></asp:TextBox>
                                </div>
							</div>
                            <div class="col-sm-3">
                                <div class="form-group">
                                 <label for="txtNome">Data di Nascita [*] </label>
                                <asp:TextBox ID="txtDataNascita" CssClass="form-control" placeholder="dd/mm/yyyy" runat="server" MaxLength="250"></asp:TextBox>
                            </div>  
						</div>
                            <div class="col-sm-3">
                                <div class="form-group">
                                    <label for="txtNome">Numero Tess. ASI [*] </label>
                                    <asp:TextBox ID="txtNumeroTessera" CssClass="form-control" runat="server" MaxLength="250"></asp:TextBox>
                                </div>
                            </div>
                        </div>          
                    </div>

           <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-3">
								<div class="form-group">

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
										CssClass="errore" ErrorMessage="Nome" ControlToValidate="txtNome" 
										EnableClientScript="true"></asp:RequiredFieldValidator>
  
									</div>
							</div>
							<div class="col-sm-3">
								<div class="form-group">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                        CssClass="errore" ErrorMessage="Cognome" ControlToValidate="txtCognome"
                                        EnableClientScript="true"></asp:RequiredFieldValidator>
								
								</div>
							</div>
                            <div class="col-sm-3">
                                <div class="form-group">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                        CssClass="errore" Display="Dynamic" ErrorMessage="Data di Nascita" ControlToValidate="txtDataNascita"
                                        EnableClientScript="true"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataNascita" Display="Dynamic" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/(19|20)\d\d$" ErrorMessage="dd/mm/yyyy format" CssClass="errore"></asp:RegularExpressionValidator>

                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                        CssClass="errore" ErrorMessage="Numero tessera" ControlToValidate="txtNumeroTessera"
                                        EnableClientScript="true"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                        </div>
          </div>
    <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-12">
								<div class="form-group">
                           <asp:LinkButton ID="lnkCheck" class="btn btn-primary" runat="server"><i class="bi bi-check-square"> </i>Controlla</asp:LinkButton>             
						   
                                   
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
