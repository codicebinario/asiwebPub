<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageAlbo.Master" CodeBehind="AnnullaCorsoMotivo.aspx.vb" Inherits="ASIWeb.AnnullaCorsoMotivo" %>
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
       <div class="jumbotron jumbotron-fluid rounded">
  <div class="container">
    <h3 class="display-5">Annullamento Corso</h3>
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
                       <h5><asp:Label ID="lblnomef" runat="server" Text=""></asp:Label></h5>
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
                      <h5>Annullamento Corso</h5>
                      <hr />
                  </div>
              </div>

          </div>
      </div>

     <div class="col-sm-12">
						<div class="row">
							
							<div class="col-sm-4">
								<div class="form-group">
								<label for="txtnote">Motivazione annullamento [*]</label>
                                         <asp:textbox id="txtNoteAnnullamento" Cssclass="form-control" runat="server"  maxlength="250" ></asp:textbox>
				
								</div>
							</div>
						
                         <div class="col-sm-8">
								<div class="form-group">
									
								</div>
							</div>
                        
                        </div>
                             
                </div>

       <div class="col-sm-12">
						<div class="row">
							
							<div class="col-sm-4">
								<div class="form-group">
									  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
										CssClass="errore" ErrorMessage="motivo annullamento" ControlToValidate="txtNoteAnnullamento" 
										EnableClientScript="true"></asp:RequiredFieldValidator>
								
								</div>
							</div>
						
                         <div class="col-sm-8">
								<div class="form-group">
							
								
								</div>
							</div>
                        
                        </div>
                             
                </div>

     <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-12">
								<div class="form-group">
                                   
						            <asp:Button ID="btnValuta" runat="server" Text="Annulla" class="btn btn-primary"    />
                                   
						</div>
							</div>
							
				</div></div>	

</asp:Content>
