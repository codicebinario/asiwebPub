<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageGestioneAlbo.Master" CodeBehind="DashboardAlbo.aspx.vb" Inherits="ASIWeb.DashboardAlbo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <style>

        .card-title{
    font-size:0.8rem;
    text-overflow: ellipsis;
    white-space: nowrap;
    overflow: hidden;
}

       a {color: #005a7c;}
        a:hover {color: darkred;}
   
        .btn-custom {
         width:160px;
        font-size: x-small;


        }
        .piccolo{

 font-size: small;
        }
           a:target {

           
    font-size: x-large;
      background: yellow;
    }
           .errore{

               color:red;
           }
 .formx {display:block;
}

.radio-inline label {
    font-size:smaller;
 margin-left: 4px;
 vertical-align:middle;
   
}
    </style>

        <script>
        function myConfirm() {

            let text = "Andando avanti non si potranno più caricare \n foto di questo corso?";
            if (confirm(text) == true) {
                return true;
                //  text = "You pressed OK!";
            } else {
                return false;
                //    text = "You canceled!";
            }
        }

        function myAnnulla() {

            let text = "Sei sicuro di voler annullare questa Equiparazione?";
            if (confirm(text) == true) {
                return true;
                //  text = "You pressed OK!";
            } else {
                return false;
                //    text = "You canceled!";
            }
        }


        //alertify.confirm('Confirm Title', 'Confirm Message', function () { alertify.success('Ok') }
        //    , function () { alertify.error('Cancel') });
      

        </script>


    
    <link rel="stylesheet" href="../css/alertify.min.css" />
     <link rel="stylesheet" href="../css/themes/default.min.css" />
      <script type="text/javascript" src="../Scripts/alertify.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
 <div class="col-sm-12 mb-3 mb-md-0">
       <div class="jumbotron jumbotron-custom jumbotron-fluid rounded">
  <div class="container">
    <h3 class="display-5">Consulta Albo</h3>
    <p class="lead">
    <asp:LinkButton  ID="lnkAttive" class="btn btn-success btn-sm btn-tre" CausesValidation="false" runat="server">Scarica Albo Attivi</asp:LinkButton>
    <asp:LinkButton  ID="lnkScadute" class="btn btn-success btn-sm btn-tre" CausesValidation="false" runat="server">Scarica Albo Scaduti</asp:LinkButton>
        <asp:Label ID="lblAvviso" runat="server" Text="" Visible="true"></asp:Label>
        <br />
        <br />
        Attendere qualche secondo per avere i dati in formato Excel se presenti.
   
    </p>
  </div>
</div>


 </div>


              <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">
								 <label for="txtNome">Inserisci un valore [*] </label>
                                    <asp:textbox id="txtValore" Cssclass="form-control" runat="server"  maxlength="250" ></asp:textbox>
							
					</div>
							</div>
							<div class="col-sm-6">
								<div class="form-group"> 

                                    <label class="formx" for="OperaPrima">scegli codice fiscale o cognome [*]</label>
 
                                 
                                   
							
                                    <asp:RadioButton cssclass="radio-inline" ID="rbCF" name="rbCF1" runat="server" Text=" codice fiscale"  GroupName="scelta" />
                                   

                                   
                                    
                                      <asp:RadioButton cssclass="radio-inline" ID="rbCognome" name="rbCognome1" runat="server" Text=" cognome"   GroupName="scelta"/>
								
                                      
                                </div>


							</div>
						
                         
                        	
						
                        </div>
                             
                </div>


            <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
										CssClass="errore" ErrorMessage="inserisci un valore a tua scelta " ControlToValidate="txtvalore" 
										EnableClientScript="true"></asp:RequiredFieldValidator>
  
									</div>
							</div>
							<div class="col-sm-6">
								<div class="form-group">
                                    <asp:CustomValidator ID="CustomValidator1" CssClass="errore" runat="server" ErrorMessage="scegli tra codice fiscale e cognome"></asp:CustomValidator>
								
								</div>
							</div>
						
                         
                        
                        </div>
                             
                </div>


          <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">

                                     <label for="txtNomexxxxxxxxxxx"></label><br />
                                     <asp:Button ID="btnCheck" runat="server" Text="Trova" class="btn btn-primary"    />
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
                                   
						      
                                   
						</div>
							</div>
							
				</div>

  <%--  </div>	--%>

   <asp:PlaceHolder ID="phDash" runat="server" Visible="false"></asp:PlaceHolder>


  

   

     

</div>
</asp:Content>
