<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageGestioneAlbo.Master" CodeBehind="DashboardAlbo.aspx.vb" Inherits="ASIWeb.DashboardAlbo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../css/alertify.min.css" />
    <link rel="stylesheet" href="../css/themes/default.min.css" />
    <script type="text/javascript" src="../Scripts/alertify.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.css" integrity="sha512-3pIirOrwegjM6erE5gPSwkUzO+3cTjpnV9lexlNZqvupR64iZBnOOTiiLPb9M36zpMScbmUNIcHUqKD47M719g==" crossorigin="anonymous" referrerpolicy="no-referrer" />

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
         width:220px;
      


        }
            .fs-5{
               color:white;
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


    
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
 <div class="col-sm-12 mb-3 mb-md-0">
       <div class="jumbotron jumbotron-custom jumbotron-fluid rounded">
  <div class="container">
    <h6 class="fs-5">Consulta Albo</h6>
    <p class="lead">
    <asp:LinkButton ID="lnkAttive" OnClientClick="showToast('OKTessereAttive');" class="btn btn-success btn-sm btn-tre" CausesValidation="false" runat="server"><i class="bi bi-cloud-arrow-down"> </i>Scarica Albo Attivi</asp:LinkButton>
    <asp:LinkButton ID="lnkScadute" OnClientClick="showToast('ScaduteTessereAttive');" class="btn btn-success btn-sm btn-tre" CausesValidation="false" runat="server"><i class="bi bi-cloud-arrow-down-fill"> </i>Scarica Albo Scaduti</asp:LinkButton>
        <asp:Label ID="lblAvviso" runat="server" Text="" Visible="true"></asp:Label>
        <br />
        <br />
        Attendere qualche secondo per avere i dati in formato <i class="bi bi-file-earmark-spreadsheet"> </i>Excel se presenti.
   
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
                                  <%--   <asp:Button ID="btnCheck" runat="server" Text="Trova" class="btn btn-primary"    />--%>
                                    <asp:LinkButton ID="lnkCheck" runat="server" CssClass="btn btn-primary"><i class="bi bi-person-badge"> </i>Trova</asp:LinkButton>
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







        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-kenU1KFdBIe4zVF0s0G1M5b4hcpxyD9F7jL+jjXkk+Q2h455rYXK/7HAuoJl+0I4" crossorigin="anonymous"></script>

    </div>
        <script>
        function showToast(value) {
            toastr.options = {
                closeButton: true,
                positionClass: 'toast-top-right',
                timeOut: 5000,
             
                showDuration: 1000
            };

            if (value == "OKTessereAttive")
                toastr.success('Albo tessere attive in download', 'ASI');
            if (value == "tesserino")
                toastr.success('Tesserino tecnico in download', 'ASI');
            if (value == "ScaduteTessereAttive")
                toastr.success('Albo tessere scadute in download', 'ASI');
        }

        if (location.hash !== null && location.hash !== "") {
            //alert("ciao");
            //alert(location.hash);
            //  $(location.hash + ".collapse").collapse("show");
            //$(".collapse44").collapse("show");
            //$(".collapse44").collapse("toggle");
            //$(".collapse44").addClass("show");
            document.addEventListener("DOMContentLoaded", function (event) {
                var home_link = document.getElementById('collapse44');
                home_link.className = home_link.className + ' active';
            });
        };
        
        </script>
</asp:Content>
