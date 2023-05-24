<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageRinnovi2.Master" CodeBehind="upLegRinnovi2.aspx.vb" Inherits="ASIWeb.upLegRinnovi2" %>
 <%@ Register TagPrefix="fup" Namespace="OboutInc.FileUpload" Assembly="obout_FileUpload" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

  </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasRight" aria-labelledby="offcanvasRightLabel">
        <div class="offcanvas-header">
            <h5 class="offcanvas-title" id="offcanvasRightLabel">Invio del documento pagamento Rinnovo</h5>
            <button type="button" class="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>
        </div>
        <div class="offcanvas-body">
            <p>
                Pagina del caricamento del giustificativo del pagamento effettuato.
                I formati accettati sono
                immagini jpg, png e documenti pdf.
                <br />
                Una volta caricato il giustificato il focus del processo di rinnovo passa ad ASI, e si dovrà attendere
                la fine del processo.
            </p>
           
        </div>
    </div>
      <div class="jumbotron jumbotron-fluid rounded">
  <div class="container">
      <h6 class="fs-5 text-white text-decoration-none"><a class="text-white text-decoration-none" data-bs-toggle="offcanvas" href="#offcanvasRight" role="button" aria-controls="offcanvasRight">IInvio del documento pagamento Rinnovo (info)
      </a></h6>
  
  <%--<asp:Literal ID="litDenominazioneJumbo" runat="server"></asp:Literal>--%>
         
      
         <div class="col-6 pb-0">
        <p>
            
              <asp:LinkButton class="btn btn-success btn-sm btn-due" ID="lnkDashboard" CausesValidation="false" runat="server"><i class="bi bi-skip-backward-btn"> </i> Termina caricamento e torna a Rinnovi Attivi</asp:LinkButton>
    
                <%-- <a href="javascript:history.back()" class="btn btn-success btn-sm btn-due"><i class="bi bi-skip-backward-btn"> </i>Torna alla pagina precedente</a>--%>
        </p>
       
             </div>
      

    
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
     
  

      <div class="col-sm-12">
          <div class="row">
              <div class="col-sm-12">
                  <div class="form-group">
                      <h5>Caricamento Documento Rinnovo</h5>
                      <hr />
                  </div>
              </div>

          </div>
      </div>



  
        <div>
          
<div class="form-control-plaintext"> <label class="form-check-label" for="txtNote">Note</label>
        <asp:TextBox ID="txtNote" CssClass="form-control" runat="server"></asp:TextBox>
    </div>
             <div class="input-group">



                 <div class="custom-file">

                     <label for="customFileInput" class="form-label"></label>
                     <asp:FileUpload ID="FileUpload1" runat="server" class="form-control" name="myFile1" />

                 </div>
                 <div>
  <asp:LinkButton ID="lnkButton1" class="btn btn-primary ml-2" Visible="true"  runat="server"><i class="bi bi-upload"> </i>Carica</asp:LinkButton>                       
</div>

                 <div class="mb-3">
                     <asp:CustomValidator ID="cvCaricaDiploma" runat="server" ErrorMessage="" OnServerValidate="cvCaricaDiploma_ServerValidate"></asp:CustomValidator>
                     <asp:CustomValidator ID="cvTipoFile" runat="server" ErrorMessage="" OnServerValidate="cvTipoFile_ServerValidate"></asp:CustomValidator>


                 </div>
                 <div id="results" runat="server"></div>

             </div>

        </div>
  
    <br />

    <div class="col-sm-12">
        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <asp:Label runat="server" ID="uploadedFiles" Text="" />
                    <hr />
                </div>
            </div>

        </div>
    </div>


 


           <!-- Modal -->
<div class="modal fade" id="ModalUp" tabindex="-1" role="dialog" aria-labelledby="ModalUpTitle" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="ModalUpTitle">Istruzioni Caricamento Documenti</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
          Il sistema permette di caricare documenti in formato jpg, png e pdf.<br /><br />
          La modalità di caricamento avviene selezione del documento.<br /><br />
           Si possono caricare fino a 4 documenti nella stessa sessione. <br /><br />
          La dimensione massima accettata per file è 5mb
          <br /><br />

          Nel caso si fossero caricati documenti per errore, contattare xxxxxxx@asinazionale.it per essere abilitati a ripetere l'operazione.




        </div>
    
    </div>
  </div>
</div>









    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-kenU1KFdBIe4zVF0s0G1M5b4hcpxyD9F7jL+jjXkk+Q2h455rYXK/7HAuoJl+0I4" crossorigin="anonymous"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

    <script>


        const carica = document.querySelector('#<%=lnkButton1.ClientID%>')
        const messaggioErrore = document.querySelector('#<%=results.ClientID %>')
         const inputFile = document.querySelector('#<%=FileUpload1.ClientID %>')


        carica.addEventListener('click', function () {
            if (inputFile.files.length > 0) {
                const selectedFile = inputFile.files[0];
                console.log(selectedFile.name);
                if (validateFile(selectedFile.name)) {

                    messaggioErrore.style.cssText = "width: 100%;  margin-top: 4px;  padding: 16px; border-radius: 5px; background-color:   #f8d7da; color: #b71c1c"
                    messaggioErrore.innerHTML = "documento in caricamento...";
                }
                // Perform further processing with the selected file
            } else {
                console.log("No file selected.");
            }

            //var file = inputFile.files[0];
            //console.log(file)

            //var fileName = file.name;





        });

        function validateFile(fileName) {
            var allowedExtensions = ['pdf', 'PDF', 'jpg', 'JPG', 'jpeg', 'JPEG', 'png', 'PNG'];
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
                console.log('Allowed file type is pdf, jpg, png.');
            }
        }



    </script>


    <%-- <script>
     document.querySelector('.custom-file-input').addEventListener('change', function (e) {
         var name = document.getElementById("customFileInput").files[0].name;
         var nextSibling = e.target.nextElementSibling
         nextSibling.innerText = name
     })
 </script>--%>
</asp:Content>
