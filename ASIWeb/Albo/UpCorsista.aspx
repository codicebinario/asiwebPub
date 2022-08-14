<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageAlbo.Master" CodeBehind="UpCorsista.aspx.vb" Inherits="ASIWeb.UpCorsista" %>
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
    <h3 class="display-5">Caricamento foto corsista</h3>
    <p class="lead">
  <%--<asp:Literal ID="litDenominazioneJumbo" runat="server"></asp:Literal>--%>
               <a href="javascript:history.back()" class="btn btn-success btn-sm btn-due">Torna alla pagina precedente</a>     
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
                      <h5>Caricamento Foto Corsista</h5>
                      <hr />
                  </div>
              </div>

          </div>
      </div>

  
      
       <div class="col-sm-12">

         	<asp:label id="Label3" runat="server"><b>Carica la foto del corsista</b></asp:label>
				
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
    
    <asp:Button ID="BtnUp" class="btn btn-primary" runat="server" Text="Carica" />
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
