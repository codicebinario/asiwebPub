<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageEqui2.Master" CodeBehind="richiestaEquiparazioneDati12.aspx.vb" Inherits="ASIWeb.richiestaEquiparazioneDati12" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
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
      document.getElementById("<%=lnkButton1.ClientID %>").disabled = true;
  }
  window.onbeforeunload = DisableButton;
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="jumbotron jumbotron-fluid rounded">
  <div class="container">
      <h6 class="fs-5 text-white text-decoration-none">Nuova Equiparazione</h6>
 
    <p class="lead">
  <%--<asp:Literal ID="litDenominazioneJumbo" runat="server"></asp:Literal>--%>
          <a href="dashboardEqui.aspx" class="btn btn-success btn-sm btn-due"><i class="bi bi-sign-stop-fill"> </i>Interrompi Equiparazione.</a>        
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
		<%--	  	 <asp:AsyncPostBackTrigger  ControlID="ddlProvinciaConsegna"/>--%>
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
							
         <label  for="chkStampaCartacea"   style="padding-left:20px">Stampa Cartacea</label>
         <asp:CheckBox ID="chkStampaCartacea"  runat="server" AutoPostBack="true" />
	
								
                            
        <label id="lblsd" for="chkStampaDiploma"   style="padding-left:20px">Stampa Diploma</label>
         <asp:CheckBox ID="chkStampaDiploma"  runat="server" />
   

       
  </div></div></div></div>

	<asp:Panel ID="pnlDatiConsegna" runat="server" Visible="false">
	
<div class="alert alert-danger" role="alert">
 <strong>Indirizzo di consegna: </strong> va compilato in caso di scelta "Stampa Cartacea". Se l'indirizzo è quello di residenza,
	copia i dati in automatico con il pulsante "Copia Dati da Residenza", oppure scegli "Spedire a E.A. e verrà caricato l'indirizzo 
	 (E.A.). In entrambi i casi i dati possono essere modificati.
</div>

		 <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">
                  
        <label for="chkCopia" style="padding-left:20px">Copia Dati da Residenza</label>
     <asp:CheckBox ID="chkCopia"  runat="server" AutoPostBack="true" />
    
                                    <label for="chkEA" style="padding-left: 20px">Spedire a E.A.</label>
                                    <asp:CheckBox ID="chkEA" runat="server" AutoPostBack="true" />


                                </div>
							</div>
           </div></div>              




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
									 <label for="txtProvincia">Provincia Consegna  </label>
                               
				<asp:TextBox id="txtProvinciaConsegna" CssClass="form-control" runat="server" MaxLength="250"></asp:TextBox>									
								
								</div>
							</div>
						
                         
                        
                        </div>
                             
                </div>

		  <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic"
										CssClass="errore" ErrorMessage="indirizzo di consegna" ControlToValidate="txtIndirizzoConsegna" 
										EnableClientScript="true" ></asp:RequiredFieldValidator>

									</div>
							</div>
							<div class="col-sm-6">
								<div class="form-group">
								  <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"  Display="Dynamic"
										CssClass="errore" ErrorMessage="provincia di consegna" ControlToValidate="txtProvinciaConsegna" 
										EnableClientScript="true" ></asp:RequiredFieldValidator>
								
								</div>
							</div>
						
                         
                        
                        </div>
                             
                </div>


	 <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">
											<label for="txtCognome">Comune Consegna</label>
                                    <asp:textbox id="txtComuneConsegna" runat="server" Cssclass="form-control"   maxlength="250" ></asp:textbox>
					
								
								
						</div>
							</div>
							<div class="col-sm-6">
								<div class="form-group">
									<label for="txtCognome">Cap Consegna</label>
                                    	<asp:textbox id="txtCapConsegna" runat="server" Cssclass="form-control"   maxlength="250" ></asp:textbox>
					
								
								</div>
							</div>
					
          
   </div>
							
							</div>
                         
		 <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="Dynamic"
										CssClass="errore" ErrorMessage="comune di consegna" ControlToValidate="txtComuneConsegna" 
										EnableClientScript="true" ></asp:RequiredFieldValidator>

									</div>
							</div>
							<div class="col-sm-6">
								<div class="form-group">
								  <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"  Display="Dynamic"
										CssClass="errore" ErrorMessage="cap di consegna" ControlToValidate="txtCapConsegna" 
										EnableClientScript="true" ></asp:RequiredFieldValidator>
								
								</div>
							</div>
						
                         
                        
                        </div>
                             
                </div>

	
	
	</asp:Panel>

					

		    <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-12">
								<div class="form-group">
                                   

                                 <asp:LinkButton ID="lnkButton1" class="btn btn-primary" Visible="true"  runat="server"><i class="bi bi-forward"> </i>Avanti</asp:LinkButton>                       
          
						</div>
							</div>
							
				</div></div>	

</ContentTemplate></asp:UpdatePanel>
</asp:Content>
