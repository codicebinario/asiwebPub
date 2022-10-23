<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageEqui.Master" CodeBehind="richiestaEquiparazioneFoto.aspx.vb" Inherits="ASIWeb.richiestaEquiparazioneFoto" %>
 <%@ Register TagPrefix="fup" Namespace="OboutInc.FileUpload" Assembly="obout_FileUpload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <link rel="stylesheet" href="../css/alertify.min.css" />
     <link rel="stylesheet" href="../css/themes/default.min.css" />
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

  </style>
  
      <style>
  
        .success {
         
            color: white;
           
            padding:5px;
        }
        .salta{
            color: white;
           
            padding:5px;

        }
       

       

    </style>
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
    
        <div class="jumbotron jumbotron-fluid rounded">
  <div class="container">
    <h3 class="display-5">Caricamento foto equiparazione</h3>
      <label id="lbl" for="chkSalta" Class="success text-white"  style="padding-left:20px">Salta il caricamento foto!!!</label>
       <asp:CheckBox AutoPostBack="true" CssClass="success text-white" ID="chkSalta" CausesValidation="false"  runat="server" />
       
     
 
     
    <p class="lead">
  <%--<asp:Literal ID="litDenominazioneJumbo" runat="server"></asp:Literal>--%>
                <a href="dashboardEqui.aspx" class="btn btn-success btn-sm btn-due"><i class="bi bi-sign-stop-fill"> </i>Interrompi il caricamento corso.</a>       
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
                      <h5>Caricamento Foto Equiparazione</h5>
                      <hr />
                  </div>
              </div>

          </div>
      </div>

  
      
       <div class="col-sm-12">

         	<asp:label id="Label3" runat="server"><b>Carica la foto della persona per l'equiparazione</b></asp:label>
				
           <div class="custom-file mb-3">
       <asp:FileUpload ID="inputfile" class="custom-file-input" runat="server" name="filename"/>
      <label class="custom-file-label" for="inputfile">Scegli la foto</label>
    </div>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" 
                            ErrorMessage="carica la foto" 
                            ControlToValidate="inputfile" CssClass="errore"></asp:RequiredFieldValidator>

             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"  ControlToValidate="inputfile"
            ErrorMessage="il file deve essere in formato jpg" 
            ValidationExpression="(.*?)\.(jpg|JPG)$" Display="Dynamic" CssClass="errore"></asp:RegularExpressionValidator>
     </div>
        <br />
 <div class="col-sm-12">
<asp:LinkButton ID="lnkButton1" class="btn btn-primary" Visible="true"  runat="server"><i class="bi bi-upload"> </i>Carica</asp:LinkButton>                       

<%--    <asp:Button ID="BtnUp" class="btn btn-primary" runat="server" Text="Carica" />--%>
    </div>


     
     <br />

               <div class="col-sm-12">
                   La foto dovrà essere utilizzata per una tessera. Va quindi rispettata la giusta proporzione del formato ritratto (lato lungo verticale).


                 
					<asp:label id="Label1" runat="server" Width="484px" 
        style="font-size: medium; font-weight: 700; color: #FF0000">Min Altezza: 140 pixel - Min Larghezza: 100 pixel</asp:label><br />

					<div class="errore" id="results"  runat="server"><br /></div>
  </div>

         <div class="col-sm-12">
             Dovresti caricare una foto recente. Sotto ci sono alcuni esempi corretti e non
                  corretti.<br />
                


         </div>

         <br />
       <div class="col-sm-12">
             <ul>
                 <li><img src="../img/2.jpg" alt="esempio" /> - corretto</li>
              
           <li> <img src="../img/1.jpg" alt="esempio" /> - errato</li> 
            
              <li><img src="../img/3.jpg" alt="esempio" />  -  errato
              </li>
              <li><img src="../img/4.jpg" alt="esempio" /> -  errato</li>
             
</ul>

         </div>

</asp:Content>
