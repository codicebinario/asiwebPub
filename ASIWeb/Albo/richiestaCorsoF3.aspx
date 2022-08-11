<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageAlbo.Master" CodeBehind="richiestaCorsoF3.aspx.vb" Inherits="ASIWeb.richiestaCorsoF3" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

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

	   <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="Updatepanel2"
					 DynamicLayout="false">
	<ProgressTemplate>
	  <div class="Progress">
		 <div class="btn">loading</div>
		
		</div>
		

	</ProgressTemplate>
</asp:UpdateProgress>

       <div class="jumbotron jumbotron-fluid rounded">
  <div class="container">
    <h3 class="display-5">Nuovo Corso</h3>
    <p class="lead">
  <%--<asp:Literal ID="litDenominazioneJumbo" runat="server"></asp:Literal>--%>
               
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
                       <h5>Fase 2: <asp:Label ID="lblnomef" runat="server" Text=""></asp:Label></h5>
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
                      <h5>Fase 3: Dati Corso</h5>
                      <hr />
                  </div>
              </div>

          </div>
      </div>
         <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Always"> 
          <Triggers>
              <asp:AsyncPostBackTrigger  ControlID="ddlSport"   />
               <asp:AsyncPostBackTrigger  ControlID="ddlDisciplina"  />
          </Triggers>
    <ContentTemplate>
        <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-4">
								<div class="form-group">
								 <label for="ddlSport">Sport [*] </label>
                             <asp:dropdownlist id="ddlSport" runat="server" Cssclass="form-control input-sm" AutoPostBack="true"></asp:dropdownlist>
	
                    		</div>
							</div>
							<div class="col-sm-4">
								<div class="form-group">
									<label for="ddlDisciplina">Disciplina [*]</label>
                             <asp:dropdownlist id="ddlDisciplina" runat="server" Cssclass="form-control input-sm" AutoPostBack="true"></asp:dropdownlist>
	
								
								</div>
							</div>
						<div class="col-sm-4">
								<div class="form-group">
									<label for="txtCognome">Specialità <span>[Inserire un valore oppure ND]</span></label>
                            <asp:dropdownlist id="ddlSpecialita" runat="server" Cssclass="form-control input-sm"></asp:dropdownlist>
	
								
								</div>
							</div>
						
                         
                        
                        </div>
                             
                </div>

         <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-4">
								<div class="form-group">
	<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSport"  ErrorMessage="Sport" InitialValue="##" Display="Dynamic" CssClass="errore" EnableClientScript="true"></asp:RequiredFieldValidator>
		
             
	       
						</div>
							</div>
							<div class="col-sm-4">
								<div class="form-group">
					  
<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlDisciplina"  ErrorMessage="Disciplina" InitialValue="##" Display="Dynamic" CssClass="errore" EnableClientScript="true"></asp:RequiredFieldValidator>




					</div>
								
								</div>

                            <div class="col-sm-4">
								<div class="form-group">
					  
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSpecialita"  ErrorMessage="Specialità" InitialValue="##" Display="Dynamic" CssClass="errore" EnableClientScript="true"></asp:RequiredFieldValidator>
<asp:CustomValidator ID="CustomValidator1" Enabled="false" Display="Dynamic" runat="server" ErrorMessage="Inserire un valore oppure ND" CssClass="errore"></asp:CustomValidator>



					</div>
								
								</div>
							</div>
						</div>

        </ContentTemplate></asp:UpdatePanel>


   <%--   <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">
								 <label for="txtNome">Sport Interessato </label>
                                    <asp:textbox id="txtSportInteressato" Cssclass="form-control" runat="server"  maxlength="250" ></asp:textbox>
						</div>
							</div>
							<div class="col-sm-6">
								<div class="form-group">
									<label for="txtCognome">Disciplina [*]</label>
                                    	<asp:textbox id="txtDisciplina" runat="server" Cssclass="form-control"   maxlength="250" ></asp:textbox>
					
								
								</div>
							</div>
						
                         
                        
                        </div>
                             
                </div>--%>

           <%--<div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">

                                 
									</div>
							</div>
							<div class="col-sm-6">
								<div class="form-group">
								  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
										CssClass="errore" ErrorMessage="Disciplina" ControlToValidate="txtDisciplina" 
										EnableClientScript="true"></asp:RequiredFieldValidator>
								
								</div>
							</div>
						
                         
                        
                        </div>
                             
                </div>--%>


     <div class="col-sm-12">
						<div class="row">
							
							<div class="col-sm-4">
								<div class="form-group">
									<label for="txtCognome">Qualifica [*]</label>
                                
					 <asp:dropdownlist id="ddlQualifica" runat="server" Cssclass="form-control input-sm"></asp:dropdownlist>
								
								</div>
							</div>
						
                         <div class="col-sm-4">
								<div class="form-group">
									<label for="txtCognome">Livello</label>
                                    	 <asp:dropdownlist id="ddlLivello" runat="server" Cssclass="form-control input-sm"></asp:dropdownlist>
								
								</div>
							</div>
                        
                        </div>
                             
                </div>

           <div class="col-sm-12">
						<div class="row">
							
							<div class="col-sm-4">
								<div class="form-group">
								  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="##" 
										CssClass="errore" ErrorMessage="Qualifica" ControlToValidate="ddlQualifica" 
										EnableClientScript="true"></asp:RequiredFieldValidator>
								
								</div>
							</div>
						
                         <div class="col-sm-4">
								<div class="form-group">
								<%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" inizialValue="##"
										CssClass="errore" ErrorMessage="Livello" ControlToValidate="ddlLivello" 
										EnableClientScript="true"></asp:RequiredFieldValidator>
								--%>
								</div>
							</div>
                        
                        </div>
                             
                </div>


	
							
							<asp:updatepanel runat="server" ID="Updatepanel2">
							  <ContentTemplate>
<div class="col-sm-12">
						<div class="row">
 
								 
							<div class="col-sm-4">
								<div class="form-group">
									<label for="txtCognome">Elenco Docenti (max 4 caratteri del cognome) </label>
									   <asp:TextBox ID="txtDocenteCognome" Cssclass="form-control" placeholder="cognome" maxlength="100" runat="server"></asp:TextBox>
            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" ServiceMethod="SearchCustomers"
                MinimumPrefixLength="2" CompletionInterval="100"      EnableCaching="false" CompletionSetCount="100"
                TargetControlID="txtDocenteCognome" FirstRowSelected="false" >
            </ajaxToolkit:AutoCompleteExtender>
									<asp:Label ID="lblAvviso" runat="server" Text=""></asp:Label>
							<%--	<asp:textbox id="txtDocenteNome"  runat="server" Cssclass="form-control" placeholder="nome" maxlength="100"></asp:textbox>
								<asp:textbox id="txtDocenteCognome"  runat="server" Cssclass="form-control" placeholder="cognome" maxlength="100"></asp:textbox>
						--%>	</div>
								</div>
							
							<div class="col-sm-2 text-center">
								<label for="txtCognome">Aggiungi/Rimuovi</label>
								<asp:button id="btnAggiungiDocenti" onclick="AddButton_Click1" runat="server"  class="btn btn-primary btn-custom2 " data-toggle="tooltip" data-placement="top" data-html="true"  title="Per aggiungere compilare nome e cognome e poi aggiungere"
											causesvalidation="False" Text="aggiungi"></asp:button><br /><br />
								<asp:button id="btnTogliDocenti" onclick="RemoveButton_Click1" runat="server"  class="btn btn-primary btn-custom2 "
											causesvalidation="False"  Text="rimuovi" data-toggle="tooltip" data-placement="top" title="Per rimuovere selezionare un docente">
									
								</asp:button>
							  </div>
							<div class="col-sm-3">
								<div class="form-group">
									<label for="txtCognome">Docenti Selezionati [*]</label>
								<asp:listbox id="lstDocenti" runat="server" cssclass="form-control input-sm" width="90%" selectionmode="Multiple"
											rows="5">
									
								</asp:listbox>
								
								</div>
							</div>

							</div>
							
								</div>

<div class="col-sm-12">
						<div class="row">
 
								 
							<div class="col-sm-4">
								<div class="form-group">
									</div>
								</div>
							
							<div class="col-sm-2 text-center">
								  </div>
							<div class="col-sm-3">
									<div class="form-group">
					<asp:CustomValidator ID="cvDocenti" runat="server" ErrorMessage="E' necessario almeno un docente" cssclass="errore"
ClientValidationFunction = "ValidateListBoxDocenti"></asp:CustomValidator>
									

										</div>
								</div>
							</div>

							</div>
							
								



							  </ContentTemplate>
							</asp:updatepanel>



	<asp:updatepanel runat="server" ID="Updatepanel3">
							  <ContentTemplate>
<div class="col-sm-12">
						<div class="row">
 
								 
							<div class="col-sm-4">
								<div class="form-group">
									<label for="txtCognome">Componenti Commissione </label>
								<asp:textbox id="txtCommissioneNome"  runat="server" Cssclass="form-control" placeholder="nome" maxlength="100"></asp:textbox>
								<asp:textbox id="txtCommissioneCognome"  runat="server" Cssclass="form-control" placeholder="cognome" maxlength="100"></asp:textbox>
							</div>
								</div>
							
							<div class="col-sm-2 text-center">
								<label for="txtCognome">Aggiungi/Rimuovi</label>
								<asp:button id="btnAggiungiComponente" onclick="AddButton_Click2" runat="server"  class="btn btn-primary btn-custom2 " data-toggle="tooltip" data-placement="top" data-html="true"  title="Per aggiungere compilare nome e cognome e poi aggiungere"
											causesvalidation="False" Text="aggiungi"></asp:button><br /><br />
								<asp:button id="btnRimuoviComponente" onclick="RemoveButton_Click2" runat="server"  class="btn btn-primary btn-custom2 "
											causesvalidation="False"  Text="rimuovi" data-toggle="tooltip" data-placement="top" title="Per rimuovere selezionare un componente della Commissione">
									
								</asp:button>
							  </div>
							<div class="col-sm-3">
								<div class="form-group">
									<label for="txtCognome">Componenti Selezionati [*]</label>
								<asp:listbox id="lstComponenti" runat="server" cssclass="form-control input-sm" width="90%" selectionmode="Multiple"
											rows="5"></asp:listbox>
								
								</div>
							</div>

							</div>
							
								</div>

						<div class="col-sm-12">
						<div class="row">
 
								 
							<div class="col-sm-4">
								<div class="form-group">
									</div>
								</div>
							
							<div class="col-sm-2 text-center">
								  </div>
							<div class="col-sm-3">
									<div class="form-group">
				<asp:CustomValidator ID="cvComponenti" runat="server" ErrorMessage="E'necessario almeno un componente" cssclass="errore"
ClientValidationFunction = "ValidateListBoxComponenti"></asp:CustomValidator>
									

										</div>
								</div>
							</div>

							</div>
							
								



							  </ContentTemplate>
							</asp:updatepanel>


	 <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-4">
								<div class="form-group">
								 <label for="txtNome">Quota di partecipazione </label>
                                    <asp:textbox id="txtQuota" Cssclass="form-control" runat="server"  maxlength="250" ></asp:textbox>
									<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtQuota" EnableClientScript="true" Display="Dynamic" ValidationExpression="^\d{0,9}(?:[,]\d{1,2})?$" CssClass="errore" runat="server"  ErrorMessage="Solo valuta"></asp:RegularExpressionValidator>
							<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtQuota"  ErrorMessage="Quota di partecipazione"  Display="Dynamic" CssClass="errore" EnableClientScript="true"></asp:RequiredFieldValidator>
									
								</div>
							</div>
							
                         
                        
                        </div>
                             
                </div>

           <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-4">
								<div class="form-group">
									
                                 
									</div>
							</div>
							
                         
                        
                        </div>
                             
                </div>

						
	
     <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-12">
								<div class="form-group">
                                   
						            <asp:Button ID="btnFase3" runat="server" Text="Concludi" class="btn btn-primary"    />
                                   
						</div>
							</div>
							
				</div></div>	

		<script>
            $(document).ready(function () {
                $('[data-toggle="tooltip"]').tooltip();
            });
        </script>
</asp:Content>
