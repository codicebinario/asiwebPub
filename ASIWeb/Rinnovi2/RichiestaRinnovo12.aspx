<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageRinnovi2.Master" CodeBehind="RichiestaRinnovo12.aspx.vb" Inherits="ASIWeb.RichiestaRinnovo12" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

     input[type="radio"] 
{
    margin-right: 2px;
}
  </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasRight" aria-labelledby="offcanvasRightLabel">
        <div class="offcanvas-header">
            <h5 class="offcanvas-title" id="offcanvasRightLabel">Nuovo Rinnovo</h5>
            <button type="button" class="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>
        </div>
        <div class="offcanvas-body">
            <p>
                Questa è l'ultima pagina del processo di rinnovo, vengono mostrati i dati della tessera in modalità read-only.
				Possono essere modificati e completati i dati della residenza.
            </p>
			<p>
				Se richiesta la tessera in formato cartaceo, va messo un flag sull'apposita casella e scelto l'indirizzo di consegna.
				Esso puà essere quello di residenza oppure quello dell'EA.

			</p>
			<p>
				Una volta terminata la procedura si torna alla pagina di partenza con il gruppo relativo aperto. 
			</p>
         
        </div>
    </div>
    <div class="jumbotron jumbotron-fluid rounded">
  <div class="container">
      <h6 class="fs-5"><a class="text-white text-decoration-none" data-bs-toggle="offcanvas" href="#offcanvasRight" role="button" aria-controls="offcanvasRight">Nuovo Rinnovo (info)
      </a></h6>
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

    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">


        <ContentTemplate>
            <asp:Panel ID="pnlModificaDataEmissione" runat="server">
                <div class="col-sm-12">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <h5>Data Emissione</h5>
                                <hr />
                            </div>
                        </div>

                    </div>
                </div>

                <div class="col-sm-12">
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">

                                <label for="txtNome">Data Emissione - [dd-MM-yyyy] &nbsp;</label>
                                <asp:TextBox ID="txtDataEmissioneM" CssClass="form-control" runat="server" MaxLength="250"></asp:TextBox>
                                <asp:Label ID="avvisoData" runat="server"></asp:Label>


                                <obout:Calendar ID="Calendar1" runat="server"
                                    TextBoxId="txtDataEmissioneM" CultureName="it-IT" DatePickerImagePath="../img/icon2.gif"
                                    DatePickerMode="True" MonthWidth="200" MonthHeight="140"
                                    Visible="true" StyleFolder="../calendar/styles/default">
                                </obout:Calendar>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2"
                                    MaskType="Date"
                                    runat="server"
                                    CultureName="it-IT"
                                    Mask="99/99/9999"
                                    MessageValidatorTip="true"
                                    UserDateFormat="DayMonthYear"
                                    OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError"
                                    ErrorTooltipEnabled="True"
                                    TargetControlID="txtDataEmissioneM" />



                            </div>
                        </div>



                    </div>

                </div>
                <div class="col-sm-12">
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">

                                <div class="form-group">

                                    <asp:RequiredFieldValidator ID="rqDataInizio" runat="server" ControlToValidate="txtDataEmissioneM" ErrorMessage="Data Emissione" Display="Dynamic" CssClass="errore" EnableClientScript="true"></asp:RequiredFieldValidator>
                                    <asp:CustomValidator ID="validator33" runat="server" CssClass="errore" ControlToValidate="txtDataEmissioneM"></asp:CustomValidator>


                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlDataEmissione" runat="server">
                <div class="col-sm-12">
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label for="txtNome">Data Emissione </label>



                                <asp:TextBox ID="txtDataEmissione" CssClass="form-control" runat="server" MaxLength="250" ReadOnly="true" BackColor="#FFCCCC"></asp:TextBox>


                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                            </div>
                        </div>


                    </div>

                </div>
            </asp:Panel>
        </ContentTemplate>
        </asp:UpdatePanel>





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
            <div class="col-sm-12">
                <div class="row">
                    <div class="col-sm-6">
                        <div class="form-group">
                            <label for="txtCognome">Numero Telefono</label>
                            <asp:TextBox ID="txtTelefono" CssClass="form-control" runat="server" MaxLength="250"></asp:TextBox>


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

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Display="Dynamic"
                                CssClass="errore" ErrorMessage="numero telefono" ControlToValidate="txtTelefono"
                                EnableClientScript="true"></asp:RequiredFieldValidator>

                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group">
                        

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

    <div class="col-sm-12">
        <div class="row">
            <div class="col-sm-12">
                <div id="results" runat="server"></div>
            </div>
        </div>
    </div>


    <script>
        const messaggioAggiunto = document.querySelector('#<%=results.ClientID %>')
        const carica = document.querySelector('#<%=lnkConcludi.ClientID%>')
        carica.addEventListener('click', function () {
        
            
           
            messaggioAggiunto.style.cssText = "width: 100%;  margin-top: 3px; margin-down: 4px;  padding: 16px; border-radius: 5px; background-color:   #f8d7da; color: #b71c1c"
            messaggioAggiunto.innerHTML = "Rinnovo in caricamento...";
           


        });
    </script>
</asp:Content>
