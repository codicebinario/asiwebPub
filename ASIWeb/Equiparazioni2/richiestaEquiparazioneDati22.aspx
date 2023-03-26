<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageEqui2.Master" CodeBehind="richiestaEquiparazioneDati22.aspx.vb" Inherits="ASIWeb.richiestaEquiparazioneDati22" %>
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
                       <h5>Fase 4: <asp:Label ID="lblnomef" runat="server" Text=""></asp:Label></h5>
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
                      <h5>Fase 4: Dati Sports</h5>
                      <hr />
                  </div>
              </div>

          </div>
      </div>


       <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Always"> 
          <Triggers>
            
          </Triggers>
    <ContentTemplate>
        <div class="col-sm-12">



          
                <div class="row">
                    <div class="col-sm-6">
                        <div class="form-group">
                            <label for="chkEA" style="padding-right: 10px; color:red; font-weight:bold">Equiparazione da Federazione </label>
                            <asp:CheckBox ID="chkDaFederazione" runat="server"  />



                            
                  </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group">



                        </div>
                    </div>



                </div>
            </div>
          

           <%-- <div class="col-sm-12">
                <div class="row">
                    <div class="col-sm-6">
                        <div class="form-group">

                            

                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group">
                      

                        </div>
                    </div>



                </div>

            </div>
--%>







            <div class="col-sm-12">



						<div class="row">
							<div class="col-sm-4">
								<div class="form-group">
								 <label for="ddlSport">Sport </label>
                           <%--  <asp:dropdownlist id="ddlSport" runat="server" Cssclass="form-control input-sm" AutoPostBack="true"></asp:dropdownlist>
	                      --%>      <asp:Label runat="server" CssClass="form-control input-sm" ID="ddlSport"></asp:Label>
                    		</div>
							</div>
							<div class="col-sm-4">
								<div class="form-group">
									<label for="ddlDisciplina">Disciplina</label>
                           <%--  <asp:dropdownlist id="ddlDisciplina" runat="server" Cssclass="form-control input-sm" AutoPostBack="true"></asp:dropdownlist>
	--%>
                                    <asp:Label runat="server" CssClass="form-control input-sm" ID="ddlDisciplina"></asp:Label>
								
								</div>
							</div>
						<div class="col-sm-4">
								<div class="form-group">
									<label for="txtCognome">Specialità</label>
                            <%--<asp:dropdownlist id="ddlSpecialita" runat="server" Cssclass="form-control input-sm"></asp:dropdownlist>
	--%>
                                    <asp:Label runat="server" CssClass="form-control input-sm" ID="ddlSpecialita"></asp:Label>
								
								</div>
							</div>
						
                         
                        
                        </div>
                             
                </div>

         <%--<div class="col-sm-12">
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
						</div>--%>

        </ContentTemplate></asp:UpdatePanel>

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




		    <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-12">
								<div class="form-group">
                                   
<%--						            <asp:Button ID="btnFase4" runat="server" Text="Concludi" class="btn btn-primary"    />--%>
                                      <asp:LinkButton ID="lnkButton1" class="btn btn-primary" Visible="true"  runat="server"><i class="bi bi-browser-chrome"> </i>Concludi</asp:LinkButton>      
                                   
						</div>
							</div>
							
				</div></div>	

</asp:Content>
