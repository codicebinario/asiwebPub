<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageAlbo.Master" CodeBehind="UpVerbale.aspx.vb" Inherits="ASIWeb.UpVerbale" %>
 <%@ Register TagPrefix="fup" Namespace="OboutInc.FileUpload" Assembly="obout_FileUpload" %>
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
           display: block;
    /*  overflow: hidden;*/
    }
    .custom-file-input {
      white-space: nowrap;
    }
     .custom-file-label {
      
     color:red;
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
        
	
        <div class="jumbotron jumbotron-fluid rounded">
  <div class="container">
    <h6 class="fs-5 text-white text-decoration-none">Invio del Verbale Corso</h6>
    <p class="lead">
  <%--<asp:Literal ID="litDenominazioneJumbo" runat="server"></asp:Literal>--%>
        <asp:LinkButton class="btn btn-success btn-sm btn-due" ID="lnkDashboard" CausesValidation="false" runat="server">Torna a Corsi Attivi</asp:LinkButton>
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
                      <h5>Caricamento Verbale Corso</h5>
                      <hr />
                      <div class="alert alert-danger" role="alert">
                          Dopo aver iniziato il caricamento attendi la fine della procedura per poter andare avanti.<br />
                          Caricare Verbale ed eventuali altri documenti. [formato PDF, ed if formati word (DOC e DOCX)].
                          I documenti 2 e 3 non sono obbligatori.
                      </div>
                  </div>
              </div>

          </div>
      </div>





    <asp:Panel ID="pnlFase1" runat="server">
    
       
 <div class="col-sm-12">
          <div class="row">
              <div class="col-sm-12">
    <div class="form-control-plaintext"> <label class="form-check-label" for="txtNote">Note</label>
        <asp:TextBox ID="txtNote" CssClass="form-control" runat="server"></asp:TextBox>
    </div></div>
          </div>
 </div>

                 <%-- <div class="input-group">--%>
        <div class="col-sm-12">
            <div class="row">
                      <div class="custom-file mb-4">

                         <label for="FileUpload1" class="form-label mr-2 errore">[1 - obbligatorio]</label>
                          <asp:FileUpload ID="FileUpload1" runat="server"  class="form-control mb-" name="myFile1" /><br />
                      
                      </div>
               </div></div>          
        <div class="col-sm-12">
            <div class="row">
                      <div class="custom-file mb-4 mt-4">

                          <label for="FileUpload2" class="form-label mr-2">[2 - facoltativo]</label>
                          <asp:FileUpload ID="FileUpload2" runat="server" placeholder="facoltativo" class="form-control" name="myFile2" />

                      </div>
                        
                  </div></div>
        <div class="col-sm-12">
            <div class="row">
                      <div class="custom-file mb-4 mt-4">

                          <label for="customFileInput" class="form-label mr-2">[3 - facoltativo]</label>
                          <asp:FileUpload ID="FileUpload3" runat="server" placeholder="facoltativo" class="form-control" name="myFile3" />

                      </div>
                 </div></div>

        <div class="col-sm-12">
            <div class="row">
                      <div class="mt-4">
                          <asp:Button ID="Button1" runat="server" Text="Carica" Visible="true" class="btn btn-primary ml-2" />
                          <%--      <asp:LinkButton ID="lnkButton1" class="btn btn-primary ml-2" Visible="true" runat="server"><i class="bi bi-upload"> </i>Carica</asp:LinkButton>
                          --%>
                      </div>

                      <div class="mb-3">
                          <asp:CustomValidator ID="cvCaricaDiploma" runat="server" ErrorMessage="" OnServerValidate="cvCaricaDiploma_ServerValidate"></asp:CustomValidator>
                          <asp:CustomValidator ID="cvTipoFile" runat="server" ErrorMessage="" OnServerValidate="cvTipoFile_ServerValidate"></asp:CustomValidator>
                          <asp:CustomValidator ID="cvTipoFile2" runat="server" ErrorMessage="" OnServerValidate="cvTipoFile2_ServerValidate"></asp:CustomValidator>
                          <asp:CustomValidator ID="cvTipoFile3" runat="server" ErrorMessage="" OnServerValidate="cvTipoFile3_ServerValidate"></asp:CustomValidator>


                      </div>
                      <div id="results" runat="server"></div>

              <%--    </div>--%>

   </div></div>

    <br />
              
      <%--   <ASP:LinkButton ID="LinkButton1" runat="server" text="Cancella il caricamento" onClientClick="Cancel();" /><br/>--%>
  <br/>
    <div><div>
  
   <div class="col-sm-12">
          <div class="row">
              <div class="col-sm-12">
                  <div class="form-group">
                  <asp:Label runat="server" id="uploadedFiles" Text="" />
                      <hr />
                  </div>
              </div>

          </div>
      </div>
          
            
  </div>


        </div>        </asp:Panel>


   
   
    <br /><br />
  <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-12">
								<div class="form-group">
                                   
						            <asp:Button ID="btnFase2" runat="server" Text="Avanti" class="btn btn-primary"  Visible="false" CausesValidation="false" />
                                   
						</div>
							</div>
							
				</div></div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-kenU1KFdBIe4zVF0s0G1M5b4hcpxyD9F7jL+jjXkk+Q2h455rYXK/7HAuoJl+0I4" crossorigin="anonymous"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

    <script>



        const carica = document.querySelector('#<%=Button1.ClientID%>')
        const messaggioErrore = document.querySelector('#<%=results.ClientID %>')
        const inputFile1 = document.querySelector('#<%=FileUpload1.ClientID %>')
        const inputFile2 = document.querySelector('#<%=FileUpload2.ClientID %>')
        const inputFile3 = document.querySelector('#<%=FileUpload3.ClientID %>')

        carica.addEventListener('click', function () {

            if (inputFile1.files.length > 0) {
                const selectedFile = inputFile1.files[0];
                console.log(selectedFile.name);
                if (validateFile(selectedFile.name)) {

                    messaggioErrore.style.cssText = "width: 100%;  margin-top: 4px;  padding: 16px; border-radius: 5px; background-color:   #f8d7da; color: #b71c1c"
                    messaggioErrore.innerHTML = "documento in caricamento...";
                }
               
            } else {
                console.log("No file selected.");
            }

            if (inputFile2.files.length > 0) {
                const selectedFile = inputFile2.files[0];
                console.log(selectedFile.name);
                if (validateFile(selectedFile.name)) {

                    messaggioErrore.style.cssText = "width: 100%;  margin-top: 4px;  padding: 16px; border-radius: 5px; background-color:   #f8d7da; color: #b71c1c"
                    messaggioErrore.innerHTML = "documento in caricamento...";
                }

            } else {
                console.log("No file selected.");
            }

            if (inputFile3.files.length > 0) {
                const selectedFile = inputFile3.files[0];
                console.log(selectedFile.name);
                if (validateFile(selectedFile.name)) {

                    messaggioErrore.style.cssText = "width: 100%;  margin-top: 4px;  padding: 16px; border-radius: 5px; background-color:   #f8d7da; color: #b71c1c"
                    messaggioErrore.innerHTML = "documento in caricamento...";
                }

            } else {
                console.log("No file selected.");
            }




        });

        function validateFile(fileName) {
            var allowedExtensions = ['pdf', 'PDF', 'doc', 'DOC', 'docx', 'DOCX'];
            var fileExtension = fileName.split('.').pop().toLowerCase();
            var isValidFile = false;

            console.log(fileExtension);

            for (var index in allowedExtensions) {
                if (fileExtension === allowedExtensions[index]) {
                    isValidFile = true;
                    console.log(isValidFile)
                    // Handle valid file case here
                    messaggioErrore.style.cssText = "width: 100%;  margin-top: 6px; padding: 16px; border-radius: 5px; background-color:   #f8d7da; color: #b71c1c"
                    messaggioErrore.innerHTML = "documento in caricamento...";
                    break;
                }
            }

            if (!isValidFile) {
                console.log(isValidFile)
                isValidFile = false;
                // Handle invalid file case here
                console.log('Allowed file type is pdf, doc, docx');
            }
        }



    </script>
</asp:Content>
