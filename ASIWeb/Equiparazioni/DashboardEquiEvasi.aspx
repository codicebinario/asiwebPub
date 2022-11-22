<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageEqui.Master" CodeBehind="DashboardEquiEvasi.aspx.vb" Inherits="ASIWeb.DashboardEquiEvasi" %>
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
         width:220px;
     /*   font-size: x-small;*/


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
    <asp:UpdateProgress ID="UpdateProgress1" runat="server"
					 DynamicLayout="false">
	<ProgressTemplate>
	  <div class="Progress">
		 <div class="btn">loading...</div>
		
		</div>
	</ProgressTemplate>
</asp:UpdateProgress>
    <div class="row">
 <div class="col-sm-12 mb-3 mb-md-0">
       <div class="jumbotron jumbotron-custom jumbotron-fluid rounded">
  <div class="container">
    <h3 class="display-5">Equiparazioni Evase</h3>
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
								 <label for="txtNome">Codice Fiscale [*] </label>
                                    <asp:textbox id="txtCodiceFiscale" Cssclass="form-control" runat="server"  maxlength="250" ></asp:textbox>
							
					</div>
							</div>
							<div class="col-sm-2">
								<div class="form-group">
							 <label for="txtNomexxxxxxxxxxx">--- </label><br />
                                    <%-- <asp:Button ID="btnCheck" runat="server" Text="Trova" class="btn btn-primary"    />--%>
<asp:LinkButton ID="lnkCheck" runat="server" CssClass="btn btn-primary"><i class="bi bi-person-badge"> </i>Trova</asp:LinkButton>
								
								</div>
							</div>
						
                         
                        	<div class="col-sm-2">
								<div class="form-group">
							 <label for="txtNomexxxxxxxxxxx">--- </label><br />
                                     
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
										CssClass="errore" ErrorMessage="Codice Fiscale " ControlToValidate="txtCodiceFiscale" 
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
                                   
						       <asp:PlaceHolder ID="phDash" runat="server" Visible="false"></asp:PlaceHolder>


 <asp:PlaceHolder ID="phDash10" runat="server" Visible="false"></asp:PlaceHolder>  
                                   
						</div>
							</div>
							
				</div>

  <%--  </div>	--%>

  

   

     

  






     

</div>


       
</asp:Content>
