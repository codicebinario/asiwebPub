﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageRinnovi2.Master" CodeBehind="DashboardRinnoviEvasi2.aspx.vb" Inherits="ASIWeb.DashboardRinnoviEvasi2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-kenU1KFdBIe4zVF0s0G1M5b4hcpxyD9F7jL+jjXkk+Q2h455rYXK/7HAuoJl+0I4" crossorigin="anonymous"></script>
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
       .btn-customZip {
         width:270px;
          
      /*  font-size: xx-small;*/


        }
         .btn-custom {
         width:220px;
          
      /*  font-size: xx-small;*/


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
 .fs-5{
               color:white;
           }
 .section-divider {
  height: 0;
  border-top: 1px solid #DDD;
  text-align: center;
  margin-top: 40px;
  margin-bottom: 40px;
}

.section-divider > span {
  color: #3498db;
  background: #FAFAFA;
  display: inline-block;
  position: relative;
  padding: 0 17px;
  top: -11px;
  font-size: 15px;
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

            let text = "Sei sicuro di voler annullare questo Rinnovo?";
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
 
    <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasRight" aria-labelledby="offcanvasRightLabel">
        <div class="offcanvas-header">
            <h5 class="offcanvas-title" id="offcanvasRightLabel">Rinnovi Evasi</h5>
            <button type="button" class="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>
        </div>
        <div class="offcanvas-body">
            <p>
               Come si apre la pagina vengono mostrati se ci sono, gli ultimi 10 rinnovi nello status di evaso.<br />
                Ogni rinnovo è sotto forma di scheda ed possibile scaricare il tesserino in formato PDF.

            </p> 
            <p>
                In alternativa si può cercare un rinnovo per codice fiscale o per numero richiesta.
            </p>

            <p>
               In alternativa è possibile scaricare tutte le tessere di ogni richiesta in formato zip.
            </p>
            
        </div>
    </div>




    <div class="row">
 <div class="col-sm-12 mb-3 mb-md-0">
       <div class="jumbotron jumbotron-custom jumbotron-fluid rounded">
  <div class="container">
      <h6 class="fs-5 text-white text-decoration-none"><a class="text-white text-decoration-none" data-bs-toggle="offcanvas" href="#offcanvasRight" role="button" aria-controls="offcanvasRight">Rinnovi Evasi (help)
      </a></h6>

      <p class="lead">
  <asp:Literal ID="litDenominazioneJumboDash" runat="server"></asp:Literal>
               
    </p>
  </div>
</div>


 </div>




</div>

 <%--<div class="row d-flex justify-content-center">--%>

      <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">
								 <label for="txtNome">Codice Fiscale oppure Numero Richiesta [*] </label>
                                    <asp:textbox id="txtCodiceFiscale" Cssclass="form-control" runat="server"  maxlength="250" ></asp:textbox>
							
					</div>
							</div>
							<div class="col-sm-2">
								<div class="form-group">
							 <label for="txtNomexxxxxxxxxxx">--- </label><br />
                                  <%--   <asp:Button ID="btnCheck" runat="server" Text="Trova" class="btn btn-primary" />--%>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary"><i class="bi bi-person-badge"> </i>Trova</asp:LinkButton>
								</div>
							</div>
						
                         
                        	<div class="col-sm-2">
								<div class="form-group">
							 <label for="txtNomexxxxxxxxxxx">--- </label><br />
                                 

                                   <%--  <asp:Button ID="btnUltimi5" runat="server" Text="Ultimi 10.."  class="btn btn-primary"    />--%>
								      <asp:LinkButton ID="lnkLast10" runat="server" CausesValidation="false" CssClass="btn btn-primary"><i class="bi bi-list-task"> </i>Ultimi 10..</asp:LinkButton>
			
								</div>
							</div>
						
                        </div>
                             
                </div>

           <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
										CssClass="errore" ErrorMessage="Codice Fiscale oppure Numero Richiesta " ControlToValidate="txtCodiceFiscale" 
										EnableClientScript="true"></asp:RequiredFieldValidator>
  
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


 <asp:PlaceHolder ID="phDash10" runat="server" Visible="false"></asp:PlaceHolder>  

   

     

  






     

</div>
    <script>
        function showToast(value) {
            toastr.options = {
                closeButton: true,
                positionClass: 'toast-top-right',
                timeOut: 5000,

                showDuration: 1000
            };

            if (value == "tesserino")
                toastr.success('Tesserino tecnico in download', 'ASI');
            if (value == "diploma")
                toastr.success('Diploma in download', 'ASI');
            if (value == "zip")
                toastr.success('Tessere (file zip) in download', 'ASI');
        }
    </script>
</asp:Content>
