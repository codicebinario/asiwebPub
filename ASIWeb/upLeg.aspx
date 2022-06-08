<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPage.Master" CodeBehind="upLeg.aspx.vb" Inherits="ASIWeb.upLeg" %>
 <%@ Register TagPrefix="fup" Namespace="OboutInc.FileUpload" Assembly="obout_FileUpload" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
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

  </style>
     <script type="text/JavaScript">

         function Clear() {
             document.getElementById("<%= uploadedFiles.ClientID %>").innerHTML = "";
         }

         function ClearedFiles(fileNames) {
             document.getElementById("<%= uploadedFiles.ClientID %>").innerHTML = "questo file è di un tipo non autorizzato  " + fileNames;

         }

         function Rejected(fileName, size, maxSize) {
             document.getElementById("<%= uploadedFiles.ClientID %>").innerHTML = "il file " + fileName + " è stato respinto in quanto la sua dimensione (" + size + " bytes) supera i " + maxSize + " bytes / the file " ;

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
      <asp:label ID="hcodR" Visible="false" runat="server" />
    <div class="col-sm-12 mb-3 mb-md-0">
  <div class="jumbotron jumbotron-fluid rounded">
  <div class="container">
     <div class="row">
    <div class="col-sm">
        <h3 class="display-5">Ordini Attivi -  <asp:Literal ID="litRichiesta" runat="server"></asp:Literal></h3> 
       
    </div>
</div>
<div class="row">
     <div class="col-sm">
 <p> [ <a href="#" class="badge bg-tre text-dark" data-toggle="modal" data-target="#ModalUp">Info</a> ]
</p>
      
         </div>
    <div class="col-6 pb-0">
      
       
        <p>
            <a class="btn btn-lg btn-custom" href="dashboard.aspx" role="button">Torna ad Ordini Attivi</a>
           

        </p>
       

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
        <input type="file" name="myFile1" class="custom-file-input" id="customFileInput" aria-describedby="customFileInput"  required>
        <label class="custom-file-label" for="customFileInput">Carica il documento</label>
      </div>
      <div class="input-group-append">
       <%-- <button class="btn btn-primary" type="button" id="customFileInput1">Upload</button>--%>
         <asp:Button ID="Button1" runat="server" Text="Carica" Visible="true"  class="btn btn-primary"/>
        <%--  <input type="submit" id="summ" runat="server" OnClick="Upload" value="Carica" name="mySubmit" />--%>
      </div>
    </div>


    <br />
              
          <%--    <ASP:LinkButton ID="LinkButton1" runat="server" text="Cancella il caricamento" onClientClick="Cancel();" /><br/>--%>
  <br/>  <fup:FileUploadProgress
   
     ID="uploadProgress"
     
     InnerFiles="true"
      OnClientProgressStarted="Clear"
     OnClientFileRejected="Rejected"
     runat="server"
     OnClientServerException="ServerException"
     OnClientFileCleared = "ClearedFiles"
   LocalizationFile="">
    <AllowedFileFormats>
        
       
          <fup:Format Ext="jpg" MaxByteSize="1024000000"/>
         <fup:Format Ext="png" MaxByteSize="1024000000"/>
          <fup:Format Ext="pdf" MaxByteSize="1024000000"/>
     
       
                 
       
     </AllowedFileFormats>
  </fup:FileUploadProgress>
  <asp:Label runat="server" id="uploadedFiles" Text="" />
            
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

          Nel caso si fossero caricati documenti per errore, contattare affiliazioni.tesseramenti@asinazionale.it per essere abilitati a ripetere l'operazione.




        </div>
    
    </div>
  </div>
</div>
   



     



 
  

 <script>
     document.querySelector('.custom-file-input').addEventListener('change', function (e) {
         var name = document.getElementById("customFileInput").files[0].name;
         var nextSibling = e.target.nextElementSibling
         nextSibling.innerText = name
     })
 </script>

</asp:Content>
