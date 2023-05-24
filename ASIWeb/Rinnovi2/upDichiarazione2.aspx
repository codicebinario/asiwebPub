<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageRinnovi2.Master" CodeBehind="upDichiarazione2.aspx.vb" Inherits="ASIWeb.upDichiarazione2" %>
 <%@ Register TagPrefix="fup" Namespace="OboutInc.FileUpload" Assembly="obout_FileUpload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<link rel="stylesheet" href="../css/alertify.min.css" />
	 <link rel="stylesheet" href="../css/themes/default.min.css" />
	  <script type="text/javascript" src="../Scripts/alertify.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous">
  
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
    <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasRight" aria-labelledby="offcanvasRightLabel">
        <div class="offcanvas-header">
            <h5 class="offcanvas-title" id="offcanvasRightLabel">Invio della Dichiarazione Cambio Ente Affiliante</h5>
            <button type="button" class="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>
        </div>
        <div class="offcanvas-body">
            <p>
                Se la tessera del rinnovo non appartiene all'Ente che sta facendo la richiesta, va caricata, in formato PDF,
				la dichiarazione di Cambio Ente Affiliante. Si seleziona il documento in formato PDF e si carica. Il processo 
				può richiedere qualche momento in più. Aspettare la fine della procedura per poter andare avanti.
            </p>
          
        </div>
    </div>
	
	
		<div class="jumbotron jumbotron-fluid rounded">
  <div class="container">

      <h6 class="fs-5 text-white text-decoration-none"><a class="text-white text-decoration-none" data-bs-toggle="offcanvas" href="#offcanvasRight" role="button" aria-controls="offcanvasRight">Invio della Dichiarazione Cambio Ente Affiliante (info)
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
	 
  

	  <div class="col-sm-12">
		  <div class="row">
			  <div class="col-sm-12">
				  <div class="form-group">
					  <h5>Caricamento Dichiarazione</h5>
					  <hr />
<div class="alert alert-danger" role="alert">
 Dopo aver iniziato il caricamento attendi la fine della procedura per poter andare avanti.
</div>
				  </div>
			  </div>

		  </div>
	  </div>





	<asp:Panel ID="pnlFase1" runat="server">
	
	   <div class="col-sm-12">
		  <div class="row">
			  <div class="col-sm-12">
				  <div class="form-group">
					  <label for="z" class="titoletto">Caricamento Dichiarazione [formato PDF]</label>
					
				  </div>
			  </div>

		  </div>
	  </div>
 <div class="col-sm-12">
		  <div class="row">
			  <div class="col-sm-12">
	<div class="form-control-plaintext"> <label class="form-check-label" for="txtNote">Note</label>
		<asp:TextBox ID="txtNote" CssClass="form-control" runat="server"></asp:TextBox>
	</div>
			 <div class="input-group">

                 <div class="custom-file">

                     <label for="customFileInput" class="form-label"></label>
                     <asp:FileUpload ID="FileUpload1" runat="server" accept="document/pdf" class="form-control" name="myFile1" />

                 </div>


                 <div>

                     <asp:LinkButton ID="lnkButton1" class="btn btn-primary ml-2" Visible="true" runat="server"><i class="bi bi-upload"> </i>Carica</asp:LinkButton>
                 </div>
                 <div class="mb-3">
                     <asp:CustomValidator ID="cvCaricaDiploma" runat="server" ErrorMessage="" OnServerValidate="cvCaricaDiploma_ServerValidate"></asp:CustomValidator>
                     <asp:CustomValidator ID="cvTipoFile" runat="server" ErrorMessage="" OnServerValidate="cvTipoFile_ServerValidate"></asp:CustomValidator>


                 </div>
                 <div id="results" runat="server"></div>


</div></div></div>
	<br />
			  
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
 <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-kenU1KFdBIe4zVF0s0G1M5b4hcpxyD9F7jL+jjXkk+Q2h455rYXK/7HAuoJl+0I4" crossorigin="anonymous"></script>
   

	<script>


		//$('#customFileInput').on('change', function () {
		//	var numb = $(this)[0].files[0].size / 1024 / 1024;
		//	numb = numb.toFixed(2);
		//	if (numb > 2) {
		//		alertify.alert('ASI', 'Il file non deve superare i 2 mb di dimensione! ').set('resizable', true).resizeTo('20%', 200);



		//		/*   alert('to big, maximum is 2MiB. You file size is: ' + numb + ' MiB');*/
		//		document.getElementById('customFileInput').value = "";
		//	} else {
		//		//  alert('it okey, your file has ' + numb + 'MiB')
		//	}
		//});
	</script>
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
            var allowedExtensions = ['pdf', 'PDF'];
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
                console.log('Allowed file type is pdf.');
            }
        }



    </script>
</asp:Content>
