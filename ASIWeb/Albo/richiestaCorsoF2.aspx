<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageAlbo.Master" CodeBehind="richiestaCorsoF2.aspx.vb" Inherits="ASIWeb.richiestaCorsoF2" %>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="jumbotron jumbotron-fluid rounded">
  <div class="container">
    <h3 class="display-5">Nuovo Corso</h3>
    <p class="lead">
  <%--<asp:Literal ID="litDenominazioneJumbo" runat="server"></asp:Literal>--%>
          <a href="dashboardB.aspx" class="btn btn-success btn-sm btn-due">Interrompi il caricamento corso.</a>        
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
       <div class="col-sm-12">
          <div class="row">
              <div class="col-sm-12">
                  <div class="form-group">
                       <h5>Fase 1: <asp:Label ID="lblnomef" runat="server" Text=""></asp:Label></h5>
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
                      <h5>Fase 2: Dati Corso</h5>
                      <hr />
                  </div>
              </div>

          </div>
      </div>
	 <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">
								 <label for="txtNome">Denominazione [*] </label>
                                    <asp:textbox id="txtDenominazione" Cssclass="form-control" runat="server"  maxlength="250" ></asp:textbox>
							
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
										CssClass="errore" ErrorMessage="Denominazione" ControlToValidate="txtDenominazione" 
										EnableClientScript="true"></asp:RequiredFieldValidator>
  
									</div>
							</div>
							<div class="col-sm-6">
								<div class="form-group">
								
								
								</div>
							</div>
						
                         
                        
                        </div>
                             
                </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Always"> 
          <Triggers>
              <asp:AsyncPostBackTrigger  ControlID="ddlRegione" />
			 <asp:AsyncPostBackTrigger  ControlID="ddlProvincia"/>
             <%--  <asp:PostBackTrigger  ControlID="ddlProvincia"  />--%>
          </Triggers>
    <ContentTemplate> 

          <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">
                                     <label for="ddlRegione">Regione Svolgimento [*]  </label>
                                   <asp:dropdownlist id="ddlRegione" runat="server" Cssclass="form-control input-sm" AutoPostBack="true"></asp:dropdownlist>
							
						</div>
							</div>
							<div class="col-sm-6">
								<div class="form-group">
									 <label for="ddlProvincia">Provincia Svolgimento [*]  </label>
                                   <asp:dropdownlist id="ddlProvincia" runat="server" Cssclass="form-control input-sm" AutoPostBack="true" ></asp:dropdownlist>
				
								
								</div>
							</div>
						</div>
                </div>
    
             
                <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group"> 
									<asp:RequiredFieldValidator ID="reDdlRegione" runat="server" ControlToValidate="ddlRegione"  ErrorMessage="Regione Svolgimento" InitialValue="##" Display="Dynamic" CssClass="errore" EnableClientScript="true"></asp:RequiredFieldValidator>
		
             
	       
						</div>
							</div>
							<div class="col-sm-6">
								<div class="form-group">
					  
<asp:RequiredFieldValidator ID="drDdlProvincia" runat="server" ControlToValidate="ddlProvincia"  ErrorMessage="Provincia Svolgimento" InitialValue="##" Display="Dynamic" CssClass="errore" EnableClientScript="true"></asp:RequiredFieldValidator>




					</div>
								
								</div>
							</div>
						</div>


               <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">
								 <label for="txtNome">Comune Svolgimento [*] </label>
                                 
								
                                   <asp:dropdownlist id="ddlComune" runat="server" Cssclass="form-control"   maxlength="250" ></asp:dropdownlist>
					</div>
							</div>
							<div class="col-sm-6">
								<div class="form-group">
									<label for="txtCognome"></label>
                                   		 <label for="txtNome">Località Svolgimento </label>
                                    <asp:textbox id="txtLocalitaSvolgimento" Cssclass="form-control" runat="server"  maxlength="250" ></asp:textbox>
			 
								
								</div>
							</div>
						
                         
                        
                        </div>
                             
                </div>

           <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">

                                 
  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
										CssClass="errore" ErrorMessage="Comune Svolgimento" ControlToValidate="ddlComune"  InitialValue="##"
										EnableClientScript="true"></asp:RequiredFieldValidator>
									</div>
							</div>
							<div class="col-sm-6">
								<div class="form-group">
								
								
								</div>
							</div>
						
                         
                        
                        </div>
                             
                </div>
               
		    </ContentTemplate>
     </asp:UpdatePanel>  



         <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">
								 <label for="txtNome">Indirizzo Svolgimento [*] </label>
                                    <asp:textbox id="txtIndirizzo" Cssclass="form-control" runat="server"  maxlength="250" ></asp:textbox>
						</div>
							</div>
							<div class="col-sm-6">
								<div class="form-group">
									<label for="txtCognome">Cap Svolgimento [*]</label>
                                    	<asp:textbox id="txtCap" runat="server" Cssclass="form-control"   maxlength="250" ></asp:textbox>
					
								
								</div>
							</div>
						
                         
                        
                        </div>
                             
                </div>


        
           <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
										CssClass="errore" ErrorMessage="Indirizzo Svolgimento" ControlToValidate="txtIndirizzo" 
										EnableClientScript="true"></asp:RequiredFieldValidator>

									</div>
							</div>
							<div class="col-sm-6">
								<div class="form-group">
								  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
										CssClass="errore" ErrorMessage="Cap Svolgimento" ControlToValidate="txtCap" 
										EnableClientScript="true"></asp:RequiredFieldValidator>
								
								</div>
							</div>
						
                         
                        
                        </div>
                             
                </div>

	   <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">
								 <label for="txtNome">Ore previste Corso [*] </label>
                                    <asp:textbox id="txtOreCorso" Cssclass="form-control" runat="server"  maxlength="250" ></asp:textbox>
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

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"  Display="Dynamic"
										CssClass="errore" ErrorMessage="ore corso previste" ControlToValidate="txtOreCorso" 
										EnableClientScript="true"></asp:RequiredFieldValidator>
								<asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic" ControlToValidate="txtOreCorso" EnableClientScript="true" ValidationExpression="^[0-9]*$" CssClass="errore" runat="server"  ErrorMessage="solo numeri"></asp:RegularExpressionValidator>
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
								 <label for="txtNome">Data Emissione - [dd-MM-yyyy] &nbsp;[*] </label>
                                    <asp:textbox id="txtDataEmissione" Cssclass="form-control" runat="server"  maxlength="250" ></asp:textbox>
					
                                    
                                 
			<obout:Calendar ID="Calendar3"  runat="server"  
									TextBoxId="txtDataEmissione" CultureName="it-IT" DatePickerImagePath="../img/icon2.gif" 
									 DatePickerMode="True"   MonthWidth="200" MonthHeight="140"
									
								 Visible="true" 	StyleFolder="../calendar/styles/default"     >
															
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
                        TargetControlID="txtDataInizio" />           
                          
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
								 <label for="txtNome">Data Inizio - [dd-MM-yyyy] &nbsp;[*] </label>
                                    <asp:textbox id="txtDataInizio" Cssclass="form-control" runat="server"  maxlength="250" ></asp:textbox>
					
                                    
                                 
			<obout:Calendar ID="Calendar1"  runat="server"  
									TextBoxId="txtDataInizio" CultureName="it-IT" DatePickerImagePath="../img/icon2.gif" 
									 DatePickerMode="True"   MonthWidth="200" MonthHeight="140"
									
								 Visible="true" 	StyleFolder="../calendar/styles/default"     >
															
								</obout:Calendar>





								   <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4"
                        MaskType="Date"
                        runat="server"
                        CultureName="it-IT"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        UserDateFormat="DayMonthYear"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        ErrorTooltipEnabled="True"
                        TargetControlID="txtDataInizio" />           
                          
                                </div>
							</div>
							<div class="col-sm-6">
								<div class="form-group">
									<label for="txtCognome">Data Fine - [dd-MM-yyyy] [*]</label>
                                    	<asp:textbox id="txtDataFine" runat="server" Cssclass="form-control"   maxlength="250" ></asp:textbox>
						<obout:Calendar ID="Calendar2"  runat="server"   BeginDateCalendarId="Calendar1"
									 
									TextBoxId="txtDataFine" CultureName="it-IT" DatePickerImagePath="../img/icon2.gif" 
									 DatePickerMode="True"   MonthWidth="200" MonthHeight="140"
									
									 Visible="true" 	StyleFolder="../calendar/styles/default" >
															
								</obout:Calendar>
								   <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1"
                        MaskType="Date"
                        runat="server"
                        CultureName="it-IT"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        UserDateFormat="DayMonthYear"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        ErrorTooltipEnabled="True"
                        TargetControlID="txtDataFine" />           
								
								</div>
							</div>
						
                         
                        
                        </div>
                             
                </div>

              <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">  <asp:RequiredFieldValidator ID="rqDataInizio" runat="server" ControlToValidate="txtDataInizio"  ErrorMessage="Data Inizio"  Display="Dynamic" CssClass="errore" EnableClientScript="true"></asp:RequiredFieldValidator>
		
             
	       
						</div>
							</div>
							<div class="col-sm-6">
								<div class="form-group">
					  
<asp:RequiredFieldValidator ID="rqDataFine" runat="server" ControlToValidate="txtDataFine"  ErrorMessage="Data Fine"  Display="Dynamic" CssClass="errore" EnableClientScript="true"></asp:RequiredFieldValidator>
 




					</div>
								
								</div>
							</div>
						</div>

      <%--   <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">
								 <label for="txtNome">Ora Inizio - [HH:mm] [*] </label>
                                    <asp:textbox id="txtOraInizio" Cssclass="form-control" runat="server"  maxlength="250" ></asp:textbox>
					
                          <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2"
                        MaskType="Number"
                        runat="server"
                        ClearMaskOnLostFocus="False"
                        CultureName="it-IT"
                        Mask="99:99"
                        MessageValidatorTip="true"
                        UserTimeFormat="None"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        ErrorTooltipEnabled="True"
                        TargetControlID="txtOraInizio" />
                                </div>
							</div>--%>
						<%--	<div class="col-sm-6">
								<div class="form-group">
									<label for="txtCognome">Ora Fine - [HH:mm] [*]</label>
                                    	<asp:textbox id="txtOraFine" runat="server" Cssclass="form-control"   maxlength="250" ></asp:textbox>
					
								
								</div>
							</div>
						
                         
                        
                        </div>
                             
                </div>--%>

         <%-- <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group"> 
                                    <asp:RequiredFieldValidator ID="rqOraInizio" runat="server" InitialValue="__:__"  ControlToValidate="txtOraInizio"  ErrorMessage="Ora Inizio"  Display="Dynamic" CssClass="errore" EnableClientScript="true"></asp:RequiredFieldValidator>
		
               <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3"
                        MaskType="Number"
                        runat="server"
                        ClearMaskOnLostFocus="False"
                        CultureName="it-IT"
                        Mask="99:99"
                        MessageValidatorTip="true"
                        UserTimeFormat="None"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        ErrorTooltipEnabled="True"
                        TargetControlID="txtOraFine" />
	       
						</div>
							</div>
							<div class="col-sm-6">
								<div class="form-group">--%>
					  
<%--<asp:RequiredFieldValidator ID="rqOraFine" runat="server" ControlToValidate="txtOraFine" InitialValue="__:__"  ErrorMessage="Ora Fine"  Display="Dynamic" CssClass="errore" EnableClientScript="true"></asp:RequiredFieldValidator>
 
 


					</div>
								
								</div>
							</div>
						</div>--%>

     <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-12">
								<div class="form-group">
                                   
						            <asp:Button ID="btnFase3" runat="server" Text="Avanti" class="btn btn-primary"    />
                                   
						</div>
							</div>
							
				</div></div>	
</asp:Content>
