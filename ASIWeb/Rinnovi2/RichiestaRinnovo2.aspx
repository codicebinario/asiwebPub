<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageRinnovi2.Master" CodeBehind="RichiestaRinnovo2.aspx.vb" Inherits="ASIWeb.RichiestaRinnovo2" %>
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

<br />
    
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
  

              <asp:radiobuttonlist ID="ddlCF"  runat="server" Visible="false"></asp:radiobuttonlist>
                  

                    
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
<br /><br />
      <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-12">
								<div class="form-group">
                                   
						          <%--  <asp:Button ID="btnCF"  runat="server" Text="Scegli" class="btn btn-primary"    />--%>

                                     <asp:LinkButton ID="lnkCF" class="btn btn-primary" runat="server"><i class="bi bi-check-all"></i>Scegli</asp:LinkButton>    
                                   
						</div>
							</div>
							
				</div></div><br />
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
                                
                     <asp:LinkButton ID="lnkAvanti" class="btn btn-primary" Visible="false" CausesValidation="false" runat="server"><i class="bi bi-fast-forward"> </i>Avanti</asp:LinkButton>                       


						</div>
							</div>
							
				</div></div>

  
    
        </ContentTemplate></asp:UpdatePanel>

  






     


  
</asp:Content>
