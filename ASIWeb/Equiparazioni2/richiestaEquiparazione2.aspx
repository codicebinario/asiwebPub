﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageEqui2.Master" CodeBehind="richiestaEquiparazione2.aspx.vb" Inherits="ASIWeb.richiestaEquiparazione2" %>
 <%@ Register TagPrefix="fup" Namespace="OboutInc.FileUpload" Assembly="obout_FileUpload" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link rel="stylesheet" href="../css/alertify.min.css" />
     <link rel="stylesheet" href="../css/themes/default.min.css" />
  
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
  </style>
 
   <%-- <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.0.0/jquery.min.js"></script>--%>
    <script type="text/javascript" src="../Scripts/alertify.js"></script>
     <script type="text/JavaScript">

         function Clear() {
             document.getElementById("<%= uploadedFiles.ClientID %>").innerHTML = "";
         }

        

         function ClearedFiles(fileNames) {
             document.getElementById("<%= uploadedFiles.ClientID %>").innerHTML = "questo file è di un tipo non autorizzato  " + fileNames;

         }

     <%--  function Rejected(fileName, size, maxSize) {

             document.getElementById("<%= uploadedFiles.ClientID %>").innerHTML = "il file " + fileName + " è stato respinto in quanto la sua dimensione (" + size + " bytes) supera i " + maxSize + " bytes / the file " ;

         }--%>
         function Rejected(fileName, size, maxSize) {
             alert("File " + fileName + " è rifiutato dal sistema \nLa sua dimensione (" + size + " bytes) supera i " + maxSize + " bytes");
         }

function Cancel() {
    <%= uploadProgress.ClientID %>_obj.CancelRequest();
}

function ServerException(mess) {
    document.getElementById("<%= uploadedFiles.ClientID %>").innerHTML = mess;

         }
        
      

     </script>

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
        <div class="jumbotron jumbotron-fluid rounded">
  <div class="container">
      <h6 class="fs-5 text-white text-decoration-none">Nuova Equiparazione</h6>
   
    <p class="lead">
  <%--<asp:Literal ID="litDenominazioneJumbo" runat="server"></asp:Literal>--%>
     <%--   <a href="javascript:history.back()" class="btn btn-success btn-sm btn-due"><i class="bi bi-skip-backward-btn"></i>Torna alla pagina precedente</a>
    --%></p>
    
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
      <%-- <div class="col-sm-12">
          <div class="row">
              <div class="col-sm-12">
                  <div class="form-group">
                       <h5>Fase 1: <asp:Label ID="lblnomef" runat="server" Text=""></asp:Label></h5>
                      <hr />
                  </div>
              </div>

          </div>
      </div>
    <br /><br />--%>
    <asp:Panel runat="server" ID="pnlSaltaDiploma">
        <div class="col-sm-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">

                        <div class="alert alert-danger" role="alert">
                            <asp:CheckBox AutoPostBack="true" CssClass="success" ID="chkSaltaDiploma" CausesValidation="false" runat="server" />
                            <strong>Salta il caricamento diploma: </strong>se si vuole procedere senza caricare il diploma, metti il check-box ed attendi. 
  
                        </div>
                    </div>
                </div>

            </div>
        </div>
        <br />
       
    </asp:Panel>

      <div class="col-sm-12">
          <div class="row">
              <div class="col-sm-12">
                  <div class="form-group">
                      <h5>Fase 1: Caricamento Diploma Equiparazione</h5>

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
                      <label for="z" class="titoletto">Caricare Diploma [formato PDF]</label>
                    
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
          <input class="form-control" name="myFile1" type="file" id="customFileInput" required>
        </div>   
                 <div>

                <asp:LinkButton ID="lnkButton1" class="btn btn-primary ml-2" Visible="true"  runat="server"><i class="bi bi-upload"> </i>Carica</asp:LinkButton>                       
</div>
      <div class="input-group-append">
       <%-- <button class="btn btn-primary" type="button" id="customFileInput1">Upload</button>--%>

           

        <%--  <input type="submit" id="summ" runat="server" OnClick="Upload" value="Carica" name="mySubmit" />--%>
      </div>
    </div>

</div></div></div>
    <br />
              
      <%--   <ASP:LinkButton ID="LinkButton1" runat="server" text="Cancella il caricamento" onClientClick="Cancel();" /><br/>--%>
  <br/>
    <div><div>
  <fup:FileUploadProgress
    ID="uploadProgress"
      ShowUploadedFiles="true"
      InnerFiles="true"
      OnClientProgressStarted="Clear"
     OnClientFileRejected="Rejected"
     runat="server"
     OnClientServerException="ServerException"
     OnClientFileCleared = "ClearedFiles"
   LocalizationFile="">
    <AllowedFileFormats>
        
      <%--  <fup:Format Ext="jpg" MaxByteSize="102400000"/>
         <fup:Format Ext="png" MaxByteSize="1024000000"/>--%>
          <fup:Format Ext="pdf" MaxByteSize="5240000"/>
      
                 
       
     </AllowedFileFormats>
  </fup:FileUploadProgress>
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


        $('#customFileInput').on('change', function () {
            var numb = $(this)[0].files[0].size / 1024 / 1024;
            numb = numb.toFixed(2);
            if (numb > 2) {
                alertify.alert('ASI', 'Il file non deve superare i 2 mb di dimensione! ').set('resizable', true).resizeTo('20%', 200);



                /*   alert('to big, maximum is 2MiB. You file size is: ' + numb + ' MiB');*/
                document.getElementById('customFileInput').value = "";
            } else {
                //  alert('it okey, your file has ' + numb + 'MiB')
            }
        });
    </script>
   
</asp:Content>
