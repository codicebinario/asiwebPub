<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageEqui.Master" CodeBehind="richiestaEquiparazioneDati1.aspx.vb" Inherits="ASIWeb.richiestaEquiparazioneDati1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
  </style>
	 <script type = "text/javascript">
  function DisableButton() {
      document.getElementById("<%=btnFase3.ClientID %>").disabled = true;
  }
  window.onbeforeunload = DisableButton;
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="jumbotron jumbotron-fluid rounded">
  <div class="container">
    <h3 class="display-5">Nuova Equiparazione</h3>
    <p class="lead">
  <%--<asp:Literal ID="litDenominazioneJumbo" runat="server"></asp:Literal>--%>
          <a href="dashboardEqui.aspx" class="btn btn-success btn-sm btn-due">Interrompi Equiparazione.</a>        
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
       
    <br /><br />
	  <div class="col-sm-12">
          <div class="row">
              <div class="col-sm-12">
                  <div class="form-group">
                       <h5>Fase 3: <asp:Label ID="lblnomef" runat="server" Text=""></asp:Label></h5>
                      <hr />
                  </div>
              </div>

          </div>
      </div>
    <br /><br />
  

   
      <div class="col-sm-12">
          <div class="row">
              <div class="col-sm-12">
                  <div class="form-group">
                      <h5>Fase 3: Dati Anagrafici</h5>
                      <hr />
                  </div>
              </div>

          </div>
      </div>


    
       <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-4">
								<div class="form-group">
								 <label for="txtNome">Nome </label>
                                    <asp:textbox id="txtNome" Cssclass="form-control" runat="server"  maxlength="250" ReadOnly="true" BackColor="#FFCCCC" ></asp:textbox>
						</div>
							</div>
							<div class="col-sm-4">
								<div class="form-group">
									<label for="txtCognome">Cognome</label>
                                    	<asp:textbox id="txtCognome" runat="server" Cssclass="form-control"   maxlength="250" ReadOnly="true" BackColor="#FFCCCC"></asp:textbox>
					
								
								</div>
							</div>
						
                         <div class="col-sm-4">
								<div class="form-group">
									<label for="txtCognome">Codice tessera</label>
                                    	<asp:textbox id="txtCodiceTessera" runat="server" Cssclass="form-control"   maxlength="250" ReadOnly="true" BackColor="#FFCCCC"></asp:textbox>
					
								
								</div>
							</div>
						
                        
                        </div>
                             
                </div>

    <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">
								 <label for="txtCodiceFiscale">Codice Fiscale </label>
                                    <asp:textbox id="txtCodiceFiscale" Cssclass="form-control" runat="server"  maxlength="250" ReadOnly="true" BackColor="#FFCCCC" ></asp:textbox>
						</div>
							</div>
							<div class="col-sm-6">
								<div class="form-group">
									<label for="txtDataScadenza">Data Scadenza</label>
                                    	<asp:textbox id="txtDataScadenza" runat="server" Cssclass="form-control"   maxlength="250" ReadOnly="true" BackColor="#FFCCCC" ></asp:textbox>
					
								
								</div>
							</div>
						
                         
                        
                        </div>
                             
                </div>
     <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">
								 <label for="txtCodiceFiscale">Data Nascita </label>
                                    <asp:textbox id="txtDataNascita" Cssclass="form-control" runat="server"  maxlength="250" ReadOnly="true" BackColor="#FFCCCC" ></asp:textbox>
						</div>
							</div>
							<div class="col-sm-6">
								<div class="form-group">
									<label for="txtDataScadenza">Comune Nascita</label>
                                    	<asp:textbox id="txtComuneNascita" runat="server" Cssclass="form-control"   maxlength="250" ReadOnly="true" BackColor="#FFCCCC" ></asp:textbox>
					
								
								</div>
							</div>
						
                         
                        
                        </div>
                             
                </div>

	 <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Always"> 
          <Triggers>
        
			 <asp:AsyncPostBackTrigger  ControlID="ddlProvinciaResidenza"/>
			  	 <asp:AsyncPostBackTrigger  ControlID="ddlProvinciaConsegna"/>
             <%--  <asp:PostBackTrigger  ControlID="ddlProvincia"  />--%>
          </Triggers>
    <ContentTemplate> 



     <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">
								 <label for="txtNome">Indirizzo Residenza [*] </label>
                                    <asp:textbox id="txtIndirizzoResidenza" Cssclass="form-control" runat="server"  maxlength="250"  ></asp:textbox>
						</div>
							</div>
							<div class="col-sm-6">
								<div class="form-group">
									 
					
										 <label for="ddlProvincia">Provincia Residenza [*]  </label>
                                   <asp:dropdownlist id="ddlProvinciaResidenza" runat="server" Cssclass="form-control input-sm" AutoPostBack="true" ></asp:dropdownlist>
				
								
								</div>
							</div>
						
                         
                        
                        </div>
                             
                </div>

        
           <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" 
										CssClass="errore" ErrorMessage="indirizzo residenza" ControlToValidate="txtIndirizzoResidenza" 
										EnableClientScript="true"></asp:RequiredFieldValidator>

									</div>
							</div>
							<div class="col-sm-6">
								<div class="form-group">
								  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"   Display="Dynamic"
										CssClass="errore" ErrorMessage="provincia residenza" InitialValue="##" ControlToValidate="ddlProvinciaResidenza" 
										EnableClientScript="true"></asp:RequiredFieldValidator>
								
								</div>
							</div>
						
                         
                        
                        </div>
                             
                </div>

      <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">
									<label for="txtCognome">Comune Residenza [*]</label>
                                   
                                 
								
                                   <asp:dropdownlist id="ddlComuneResidenza" runat="server" Cssclass="form-control"   maxlength="250" ></asp:dropdownlist>
					
									</div>
							</div>
							<div class="col-sm-6">
								<div class="form-group">
									<label for="txtCognome">Cap Residenza [*]</label>
                                    	<asp:textbox id="txtCapResidenza" runat="server" Cssclass="form-control"   maxlength="250" ></asp:textbox>
					
								
								</div>
							</div>
						
                         
                        
                        </div>
                             
                </div>

        
           <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" InitialValue="##"
										CssClass="errore" ErrorMessage="provincia residenza" ControlToValidate="ddlComuneResidenza" 
										EnableClientScript="true"></asp:RequiredFieldValidator>

									</div>
							</div>
							<div class="col-sm-6">
								<div class="form-group">
								  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"  Display="Dynamic"
										CssClass="errore" ErrorMessage="cap residenza" ControlToValidate="txtCapResidenza" 
										EnableClientScript="true"></asp:RequiredFieldValidator>
								
								</div>
							</div>
						
                         
                        
                        </div>
                             
                </div>


	  <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">
								 <label for="txtNome">Indirizzo Email [*] </label>
                                    <asp:textbox id="txtEmail" Cssclass="form-control" runat="server"  maxlength="250"  ></asp:textbox>
						</div>
							</div>
							<div class="col-sm-6">
								<div class="form-group">
									<label for="txtCognome">Telefono/Cellulare [*]</label>
                                    	<asp:textbox id="txtTelefonoCellulare" runat="server" Cssclass="form-control"   maxlength="250" ></asp:textbox>
					
								
								</div>
							</div>
						
                         
                        
                        </div>
                             
                </div>

	  <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic"
										CssClass="errore" ErrorMessage="email" ControlToValidate="txtEmail" 
										EnableClientScript="true"></asp:RequiredFieldValidator>

									</div>
							</div>
							<div class="col-sm-6">
								<div class="form-group">
								  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"  Display="Dynamic"
										CssClass="errore" ErrorMessage="telefono/cellulare" ControlToValidate="txtTelefonoCellulare" 
										EnableClientScript="true"></asp:RequiredFieldValidator>
								
								</div>
							</div>
						
                         
                        
                        </div>
                             
                </div>


	  <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">
								 <label for="txtNome">Indirizzo Consegna</label>
                                    <asp:textbox id="txtIndirizzoConsegna" Cssclass="form-control" runat="server"  maxlength="250"  ></asp:textbox>
						</div>
							</div>
							<div class="col-sm-6">
								<div class="form-group">
									 <label for="ddlProvincia">Provincia Consegna  </label>
                                   <asp:dropdownlist id="ddlProvinciaConsegna" runat="server" Cssclass="form-control input-sm" AutoPostBack="true" ></asp:dropdownlist>
				
									
								
								</div>
							</div>
						
                         
                        
                        </div>
                             
                </div>


	 <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-3">
								<div class="form-group">
											<label for="txtCognome">Comune Consegna</label>
                                    <asp:dropdownlist id="ddlComuneConsegna" runat="server" Cssclass="form-control"   maxlength="250" ></asp:dropdownlist>
					
								
								
						</div>
							</div>
							<div class="col-sm-3">
								<div class="form-group">
									<label for="txtCognome">Cap Consegna</label>
                                    	<asp:textbox id="txtCapConsegna" runat="server" Cssclass="form-control"   maxlength="250" ></asp:textbox>
					
								
								</div>
							</div>
						<div class="col-sm-6">
								<div class="form-group">
								 <div class="col-6">
                            <label for="txtNome"></label>
								  <div class="form-control-plaintext">
       
         <asp:CheckBox ID="chkStampaCartacea" class="form-check-input" runat="server" />
     <label class="form-check-label" for="chkStampaCartacea">Stampa Cartacea</label>
           
   </div></div>
								</div>
							</div>
                         
                        
                        </div>
                             
                </div>

		    <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-12">
								<div class="form-group">
                                   
						            <asp:Button ID="btnFase3" runat="server" Text="Avanti" class="btn btn-primary"    />
                                   
						</div>
							</div>
							
				</div></div>	

</ContentTemplate></asp:UpdatePanel>
</asp:Content>
