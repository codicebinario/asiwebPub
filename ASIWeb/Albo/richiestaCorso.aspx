<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageAlbo.Master" CodeBehind="richiestaCorso.aspx.vb" Inherits="ASIWeb.richiestaCorso" %>
 <%@ Register TagPrefix="fup" Namespace="OboutInc.FileUpload" Assembly="obout_FileUpload" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" href="../css/alertify.min.css" />
     <link rel="stylesheet" href="../css/themes/default.min.css" />
      <script type="text/javascript" src="../Scripts/alertify.js"></script>
       <style>
    .custom-file-input.selected:lang(en)::after {
      content: "" !important;
    }
    .custom-file {
        display: block;
     /* overflow: hidden;*/
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
     <script type="text/JavaScript">
         function Disa() {
             document.getElementById("<%= Button1.ClientID %>").disabled = true;
         }

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
        
         //$('#customFileInput').on('change', function () {
         //    var numb = $(this)[0].files[0].size / 1024 / 1024;
         //    numb = numb.toFixed(2);
         //    if (numb > 2) {
         //        alert('to big, maximum is 2MiB. You file size is: ' + numb + ' MiB');
         //    } else {
         //        alert('it okey, your file has ' + numb + 'MiB')
         //    }
         //});
         //function Upload() {
         //    var fileUpload = document.getElementById("customFileInput");
         //    if (typeof (fileUpload.files) != "undefined") {
         //        var size = parseFloat(fileUpload.files[0].size / 1024).toFixed(2);
         //        alert(size + " KB.");
         //        if (size > 52000000) {
         //            return true;
         //        }
         //        else {

         //            return false;
         //        }

         //    } else {
         //        alert("This browser does not support HTML5.");
         //    }
         //}


     </script>

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       
  
        <div class="jumbotron jumbotron-fluid rounded">
  <div class="container">
    <h3 class="display-5">Nuovo Corso</h3>
    <p class="lead">
  <%--<asp:Literal ID="litDenominazioneJumbo" runat="server"></asp:Literal>--%>
            <a href="javascript:history.back()" class="btn btn-success btn-sm btn-due">Torna a Corsi Attivi</a>     
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
                      <h5>Fase 1: Caricamento Documento Corso</h5>
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
                      <label for="z" class="titoletto">Caricare Programma Corso ed eventuali altri documenti. [formato PDF, ed i formati word (DOC e DOCX)].
                          I documenti 2 e 3 non sono obbligatori.
                      </label>
                    
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
          <%--   <div class="input-group">--%>



      <div class="custom-file mb-2">


        <input type="file" name="myFile1"   class="custom-file-input" id="customFileInput" aria-describedby="customFileInput"  required>
        <label class="custom-file-label" for="customFileInput">Carica il piano corso (obbligatorio)</label>
    
        </div>
        <div class="custom-file mb-2">
              

          <input type="file" name="myFile2" class="custom-file-input" id="customFileInput2" aria-describedby="customFileInput2">
        <label class="custom-file-label" for="customFileInput2">Carica un secondo documento (facoltativo)</label>
  </div>
      <div class="custom-file mb-2">
                  

     <input type="file" name="myFile3" class="custom-file-input " id="customFileInput3" aria-describedby="customFileInput3">
     <label class="custom-file-label" for="customFileInput3">Carica un terzo documento (facoltativo)</label>
 </div>
               
         <asp:Button ID="Button1"  runat="server" Text="Carica" Visible="true"    class="btn btn-primary ml-2 mt-2"/>

     
    <%--</div>--%>

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
      OnClientFileCleared="ClearedFiles"
      LocalizationFile="">
    <AllowedFileFormats>
        
      <%--  <fup:Format Ext="jpg" MaxByteSize="102400000"/>
         <fup:Format Ext="png" MaxByteSize="1024000000"/>--%>
          <fup:Format Ext="pdf" MaxByteSize="52400000"/>
        <fup:Format Ext="doc" MaxByteSize="52400000" />
        <fup:Format Ext="docx" MaxByteSize="52400000" />
       
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
    <script>
     
        function Disa() {
            document.getElementById("<%= Button1.ClientID %>").disabled = true;
        }
    </script>
   <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.0.0/jquery.min.js"></script>

    <script>
      

         $('#customFileInput').on('change', function () {
             var numb = $(this)[0].files[0].size / 1024 / 1024;
             numb = numb.toFixed(2);
             if (numb > 5) {
                 alertify.alert('ASI', 'Il file non deve superare i 5 mb di dimensione! ').set('resizable', true).resizeTo('20%', 200);
               

               
              /*   alert('to big, maximum is 2MiB. You file size is: ' + numb + ' MiB');*/
                 document.getElementById('customFileInput').value = "";
             } else {
               //  alert('it okey, your file has ' + numb + 'MiB')
             }
         });
    </script>
</asp:Content>
