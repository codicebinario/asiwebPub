<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageRinnovi.Master" CodeBehind="RichiestaRinnovo1.aspx.vb" Inherits="ASIWeb.RichiestaRinnovo1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript" src="../Scripts/alertify.js"></script>
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

     input[type="radio"] 
{
    margin-right: 2px;
}
  </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="jumbotron jumbotron-fluid rounded">
  <div class="container">
    <h3 class="display-5">Nuovo Rinnovo</h3>
    <p class="lead">
  <%--<asp:Literal ID="litDenominazioneJumbo" runat="server"></asp:Literal>--%>
            <a href="javascript:history.back()" class="btn btn-success btn-sm btn-due"><i class="bi bi-skip-backward-btn"> </i>Torna alla pagina precedente</a>     
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
              <div class="col-sm-12">
                  <div class="form-group">
                       <h5><asp:Label ID="lblnomef" runat="server" Text="Se necessario aggiornare e completare i dati di <strong>Residenza</strong>"></asp:Label></h5>
                      <hr />
                  </div>
              </div>

          </div>
      </div>


       <div class="col-sm-12">
          <div class="row">
              <div class="col-sm-12">
                  <div class="form-group">
                      <h5>Dati Anagrafici</h5>
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
									<label for="txtCognome">Tessera ASI</label>
                                    	<asp:textbox id="txtCodiceTessera" runat="server" Cssclass="form-control"   maxlength="250" ReadOnly="true" BackColor="#FFCCCC"></asp:textbox>
					
								
								</div>
							</div>
						
                        
                        </div>
                             
                </div>

    <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-4">
								<div class="form-group">
								 <label for="txtCodiceFiscale">Codice Fiscale </label>
                                    <asp:textbox id="txtCodiceFiscale" Cssclass="form-control" runat="server"  maxlength="250" ReadOnly="true" BackColor="#FFCCCC" ></asp:textbox>
						</div>
							</div>
								<div class="col-sm-4">
								<div class="form-group">
									<label for="txtDataScadenza">Codice Iscrizione</label>
                                    	<asp:textbox id="txtCodiceIscrizione" runat="server" Cssclass="form-control"   maxlength="250" ReadOnly="true" BackColor="#FFCCCC" ></asp:textbox>
					
								
								</div>
							</div>
							<div class="col-sm-4">
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

	  <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">
								 <label for="txtSport">Sport </label>
                                    <asp:textbox id="txtSport" Cssclass="form-control" runat="server"  maxlength="250" ReadOnly="true" BackColor="#FFCCCC" ></asp:textbox>
						</div>
							</div>

							<div class="col-sm-6">
								<div class="form-group">
								 <label for="txtLivello">Disciplina </label>
                                    <asp:textbox id="txtDisciplina" Cssclass="form-control" runat="server"  maxlength="250" ReadOnly="true" BackColor="#FFCCCC" ></asp:textbox>
						</div>
							</div>


						
                         
                        
                        </div>
                             
                </div>
	  <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">
									<label for="txtSpecialita">Specialità</label>
                                    	<asp:textbox id="txtSpecialita" runat="server" Cssclass="form-control"   maxlength="250" ReadOnly="true" BackColor="#FFCCCC" ></asp:textbox>
					
								
								</div>
							</div>
                        
							
						
                         	<div class="col-sm-6">
								<div class="form-group">
									<label for="txtQualifica">Qualifica</label>
                                    	<asp:textbox id="txtQualifica" runat="server" Cssclass="form-control"   maxlength="250" ReadOnly="true" BackColor="#FFCCCC" ></asp:textbox>
					
								
								</div>
							</div>
							
							
                        </div>
                             
                </div>
	  <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">
								 <label for="txtLivello">Livello </label>
                                    <asp:textbox id="txtLivello" Cssclass="form-control" runat="server"  maxlength="250" ReadOnly="true" BackColor="#FFCCCC" ></asp:textbox>
						</div>
							</div>

						

							
							
						
                         
                        
                        </div>
                             
                </div>

	 <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Always"> 
          <Triggers>
        
			 <asp:AsyncPostBackTrigger  ControlID="ddlProvinciaResidenza"/>
			  	<%-- <asp:AsyncPostBackTrigger  ControlID="ddlProvinciaConsegna"/>--%>
             <%--  <asp:PostBackTrigger  ControlID="chkStampaCartacea"  />--%>
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

		
		<%--<div class="col-sm-12">
          <div class="row">
              <div class="col-sm-12">
                  <div class="form-group">
                   
                      <hr />
                  </div>
              </div>

          </div>
      </div>--%>

	 

		<div class="col-sm-6">
								<div class="form-group">
								 <div class="col-6">
                            <label for="txtNome"></label>
								  <div class="form-control-plaintext">
       
         <asp:CheckBox ID="chkStampaCartacea"   runat="server" AutoPostBack="true" />
     <label style="padding-left:20px" for="chkStampaCartacea">Stampa Cartacea</label>

           
   </div></div>
								</div>
							</div>
      



		<asp:Panel ID="pnlDatiConsegna" runat="server" Visible="false">
            <div class="alert alert-danger" role="alert">
                <strong>Indirizzo di consegna: </strong>va compilato in caso di scelta "Stampa Cartacea". Se l'indirizzo è quello di residenza,
	copia i dati in automatico con il pulsante "Copia Dati da Residenza", oppure scegli "Spedire a E.A. e verrà caricato l'indirizzo 
	 (E.A.). In entrambi i casi i dati possono essere modificati.
            </div>

				<div class="col-sm-12">
								<div class="form-group">
								 <div class="col-6">
                            <label for="txtNome"></label>
								  <div class="form-control-plaintext">
       
     <asp:CheckBox ID="chkCopia"  runat="server" AutoPostBack="true" />
     <label style="padding-left:20px"  for="chkCopia">Copia Dati da Residenza</label>
  <label for="chkEA" style="padding-left: 20px">Spedire a E.A.</label>
  <asp:CheckBox ID="chkEA" runat="server" AutoPostBack="true" />
           
   </div></div>
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
                                 <asp:textbox id="txtProvinciaConsegna" Cssclass="form-control" runat="server"  maxlength="250"  ></asp:textbox>
									
								
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
                                  <asp:textbox id="txtComuneConsegna" Cssclass="form-control" runat="server"  maxlength="250"  ></asp:textbox>
								
								
						</div>
							</div>
							<div class="col-sm-6">
								<div class="form-group">
									<label for="txtCognome">Cap Consegna</label>
                                    	<asp:textbox id="txtCapConsegna" runat="server" Cssclass="form-control"   maxlength="250" ></asp:textbox>
					
								
								</div>
							</div>
							</div></div>
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
		</ContentTemplate></asp:UpdatePanel>
		    <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-12">
								<div class="form-group">
                                   
						            <%--<asp:Button ID="btnFase3" runat="server" Text="Concludi" class="btn btn-primary"    />--%>
    <asp:LinkButton ID="lnkConcludi" class="btn btn-primary"   runat="server"><i class="bi bi-save"> </i>Concludi</asp:LinkButton>                       

                                   
						</div>
							</div>
							
				</div></div>	
</asp:Content>
