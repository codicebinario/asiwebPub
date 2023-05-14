<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageRinnovi2.Master" CodeBehind="UpFotoRinnovo2.aspx.vb" Inherits="ASIWeb.UpFotoRinnovo2" %>
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
    .vuota {

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
    .avvisocarimento {
     width: 100%;
    padding: 16px;
    border-radius: 5px;
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
    <link href="../Css/output.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasRight" aria-labelledby="offcanvasRightLabel">
        <div class="offcanvas-header">
            <h5 class="offcanvas-title" id="offcanvasRightLabel">Caricamento foto rinnovo</h5>
            <button type="button" class="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>
        </div>
        <div class="offcanvas-body">
            <p>
               Questa è la pagina di caricamento della foto da apporre sul tesserino. La foto deve essere esclusivamente
                in formato jpg.
            </p>
         
        </div>
    </div>
    
        <div class="jumbotron jumbotron-fluid rounded">
  <div class="container">

      <h6 class="fs-5"><a class="text-white text-decoration-none" data-bs-toggle="offcanvas" href="#offcanvasRight" role="button" aria-controls="offcanvasRight">Caricamento foto rinnovo (info)
      </a></h6>
    <p class="lead">
             
 
          <asp:LinkButton class="btn btn-success btn-sm btn-due" ID="lnkDashboardTorna" CausesValidation="false" runat="server">Torna alla pagina precedente</asp:LinkButton>
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
                      <h5>Caricamento Foto Rinnovo</h5>
                         <hr />
<div class="alert alert-danger" role="alert">
 Dopo aver iniziato il caricamento attendi la fine della procedura per poter andare avanti.
</div>
                  </div>
              </div>

          </div>
      </div>



    <div class="col-sm-12">
        <output class="mt-5 mb-2"></output>


        <div class="mb-3">
            <label for="inputfile" class="form-label"><b>Carica la tua foto</b></label>
            <asp:FileUpload ID="inputfile" accept="image/jpeg, image/jpg" type="file" class="form-control" runat="server" name="filename" />

        </div>
        <div class="mb-3">
            <asp:CustomValidator ID="cvCaricaLaFoto" runat="server" ErrorMessage="" OnServerValidate="cvCaricaLaFoto_ServerValidate"></asp:CustomValidator>
            <asp:CustomValidator ID="cvTipoFile" runat="server" ErrorMessage="" OnServerValidate="cvTipoFile_ServerValidate"></asp:CustomValidator>


        </div>
        <div id="results" runat="server"></div>

        <br />
        <div class="col-sm-12">

            <asp:LinkButton ID="lnkButton1" class="btn btn-primary" Visible="true" runat="server"><i class="bi bi-upload"> </i>Carica</asp:LinkButton>


        </div>




        <br />

               <div class="col-sm-12">
                   La foto dovrà essere utilizzata per una tessera. Va quindi rispettata la giusta proporzione del formato ritratto (lato lungo verticale).


                 
					<asp:label id="Label1" runat="server" Width="484px" 
        style="font-size: medium; font-weight: 700; color: #FF0000">Min Altezza: 140 pixel - Min Larghezza: 100 pixel</asp:label><br />

				
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

 <script>

           const input = document.querySelector('#<%=inputfile.ClientID %>')
               const carica = document.querySelector('#<%=lnkButton1.ClientID%>')
     const output = document.querySelector("output")
     const messaggioErrore = document.querySelector('#<%=results.ClientID %>')
     const messaggioCaricaLaFoto = document.querySelector(".form-label")
     let imagesArray = []
           
     input.addEventListener("change", () => {
         messaggioErrore.innerHTML = "";
         messaggioErrore.removeAttribute("style") 
     /*    output.style.display = (output.style.display == 'none') ? defaultDisplay : 'none'*/

          imagesArray.forEach((image, index) => {
              /*   deleteImage(index)*/
           
             
          })
         const file = input.files
        
         imagesArray.push(file[0])
         displayImages()
          carica.addEventListener("click", () => {
              console.log("ciao")
              if (validateFile()) {
                //  avviso.style.cssText = "width: 100%;  padding: 16px; border-radius: 5px; background-color:   #f8d7da; color: #b71c1c"
                  console.log("hello")
                  messaggioErrore.style.cssText = "width: 100%;  padding: 16px; border-radius: 5px; background-color:   #f8d7da; color: #b71c1c"
                  messaggioErrore.innerHTML = "immagine in caricamento";
              }
         
        
         })
     })
          
            function validateFile() {
                var allowedExtension = ['jpeg', 'jpg'];
         var fileInput = document.querySelector('#<%=inputfile.ClientID %>')
         var fileName = fileInput.files[0].name;
         var fileExtension = fileName.split('.').pop().toLowerCase();
         var isValidFile = false;
         for (var index in allowedExtension) {
                       if (fileExtension === allowedExtension[index]) {
             isValidFile = true;
         messaggioErrore.style.cssText = "width: 100%;  padding: 16px; border-radius: 5px; background-color:   #f8d7da; color: #b71c1c"
         messaggioErrore.innerHTML = "immagine in caricamento";

         break;
                       }
                   }
         if (!isValidFile) {
             isValidFile = false;

         console.log('Allowed file types are jpeg and jpg.');
                   }
               }
         function displayImages() {
             let images = ""
                   imagesArray.forEach((image, index) => {
             images += `<div class="image">
                <img src="${URL.createObjectURL(image)}" alt="image">
                <span onclick="deleteImage(${index})">&times;</span>
              </div>`
         })
         output.innerHTML = images
               }
         function deleteImage(index) {
             imagesArray.splice(index, 1)
                   messaggioErrore.style.cssText = "width: 100%;  padding: 16px; border-radius: 5px; background-color:   #f8d7da; color: #b71c1c"
         messaggioErrore.innerHTML = "Scegli la tua foto"
         input.value = ""
         /*    output.innerHTML = ""*/
         imagesArray = []
         displayImages()
               }
 </script>
         
</asp:Content>
