<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageRinnovi2.Master" CodeBehind="RichiestaRinnovo2.aspx.vb" Inherits="ASIWeb.RichiestaRinnovo2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

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
   
  /*   input[type="radio"] 
{
    margin-right: 10px;
      margin-left: 20px;
           display: block;
      float: left;
       
}
     label {
         margin-top: -4px;
     }*/
   /*  .custom-radio-list label {
  display: flex;
  align-items: center;
}*/
/*
.custom-radio-list input[type="radio"] {
  margin-right: 8px;
}

.custom-radio-list .custom-radio-circle {
  width: 18px;
  height: 18px;
  border-radius: 50%;
  background-color: #333;
}

.custom-radio-list .custom-radio-text {
  font-size: 14px;
}*/

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
                Se il codice fiscale passa il controllo tesseramento, si arriva alla pagina dove vengono mostrare le tessere 
                trovare e valide per quel codice fiscale. Si effettua la selezione e si va avanti nel processo.
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
     <%--  <div class="col-sm-12">
          <div class="row">
              <div class="col-sm-12">
                  <div class="form-group">
                       <h5>Fase 1: <asp:Label ID="lblnomef" runat="server" Text=""></asp:Label></h5>
                      <hr />
                  </div>
              </div>

          </div>
      </div>
    <br />--%>


	 <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Always"> 
          <Triggers>
        
		<%--	 <asp:AsyncPostBackTrigger  ControlID="ddlProvinciaResidenza"/>
			  	 <asp:AsyncPostBackTrigger  ControlID="ddlProvinciaConsegna"/>--%>
             <%--  <asp:PostBackTrigger  ControlID="ddlProvincia"  />--%>
          </Triggers>
    <ContentTemplate> 


    <div class="col-sm-12">
          <div class="row d-flex justify-content-left">
             
 <div class="col-sm-12">
       <div class="form-check">
                <div class="form-check">
  

                    <asp:RadioButtonList ID="ddlCF" runat="server" Visible="false" RepeatColumns="1">
                       
                    </asp:RadioButtonList>



                </div>
               </div>
          </div>
             

        <div class="col-sm-12">
       <div class="form-check">
                <div class="form-check">
  
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="errore" ErrorMessage="effettuare una scelta per procedere oltre" ControlToValidate="ddlCF"></asp:RequiredFieldValidator>
            
                    
</div>
               </div>
          </div>
<br />
      <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-12">
								<div class="form-group">
                                   
						            <asp:Button ID="btnCF"  runat="server" Text="Scegli" class="btn btn-primary"    />

                                  <%--   <asp:LinkButton ID="lnkCF" class="btn btn-primary" runat="server"><i class="bi bi-check-all"></i>Scegli</asp:LinkButton>    
                                   --%>
						</div>
							</div>
							
				</div></div>
       <div class="col-sm-12">
					  <div class="form-check">
                <div class="form-check">
  
								<div class="form-group">
                                    <asp:Label ID="lblScelta" runat="server" Text="" Visible="false"></asp:Label>
						           
                                   
						</div>
							</div>
							
				</div></div>	
  
   <br />
        <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-12">
								<div class="form-group">
                                   
					<%--	            <asp:Button ID="BtnAvanti"    runat="server" Text="Avanti" class="btn btn-primary"    />--%>
    <%--                            
                     <asp:LinkButton ID="lnkAvanti"  class="btn btn-primary" CausesValidation="false" runat="server"><i class="bi bi-fast-forward"> </i>Avanti</asp:LinkButton>--%>
                            <%--        <button type="button" id="fakeButton" visible="false" runat="server" class="btn btn-primary" onclick="<%=btnConcludi.ClientID %>.click()">
                                        <i class="bi bi-fast-forward"></i>Avanti
                                    </button> --%>  
                                    
                                    <asp:Button ID="btnConcludi" class="btn btn-primary" Visible="false" CausesValidation="false" runat="server" Text="Avanti"></asp:Button>

						</div>
							</div>
							
				</div></div>


           


    
        </ContentTemplate></asp:UpdatePanel>

  






     
    <script>

        //var radioInputs = document.querySelector('input[type="radio"]');
        //for (var i = 0; i < radioInputs.length; i++) {

        //}
        //radioInput.classList.add('form-check-input');

        //var targetFor = 'Conte'; // The value of the 'for' attribute you want to match
        //var labels = document.getElementsByTagName('label');

        //for (var i = 0; i < labels.length; i++) {
        //    var label = labels[i];
        //    var forValue = label.getAttribute('for');
        //    var firstFiveLetters = forValue.substring(0, 5);
        //    if (firstFiveLetters === targetFor) {
        //        label.classList.add('form-check-label');
        //      //  break; // Exit the loop once the matching label is found (optional)
        //    }
        //}

    </script>

  
</asp:Content>
