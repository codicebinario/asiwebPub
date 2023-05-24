<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageEqui2.Master" CodeBehind="vediDocumentazione2.aspx.vb" Inherits="ASIWeb.vediDocumentazione2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../css/alertify.min.css" />
    <link rel="stylesheet" href="../css/themes/default.min.css" />
    <script type="text/javascript" src="../Scripts/alertify.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-kenU1KFdBIe4zVF0s0G1M5b4hcpxyD9F7jL+jjXkk+Q2h455rYXK/7HAuoJl+0I4" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.css" integrity="sha512-3pIirOrwegjM6erE5gPSwkUzO+3cTjpnV9lexlNZqvupR64iZBnOOTiiLPb9M36zpMScbmUNIcHUqKD47M719g==" crossorigin="anonymous" referrerpolicy="no-referrer" />


    <script>
        function getPage(url) {


            // this will make a child page popup 


            window.open(url, "MyWindow", "height=375,width=350");
            return false;

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="jumbotron jumbotron-fluid rounded">
  <div class="container">
    
      <h6 class="fs-5 text-white text-decoration-none">Diploma e Fotografia</h6>
    <p class="lead">
  <%--<asp:Literal ID="litDenominazioneJumbo" runat="server"></asp:Literal>--%>
       
    <asp:LinkButton class="nav-link text-white" ID="lnkTorna" CausesValidation="false" runat="server">Torna alla pagina precedente</asp:LinkButton>
   
            
      <%--     <a href="javascript:history.back()" class="btn btn-success btn-sm btn-due">Torna alla pagina precedente</a>--%>   
      
            
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

        <table class="table table-hover">
  <thead>
    <tr>
   <%--   <th scope="col">#</th>--%>
      <th scope="col">Nome</th>
      <th scope="col">Cognome</th>
      <th scope="col">Email</th>
     <th scope="col">C.F.</th>
         <th scope="col">N.Tessera ASI</th>
          <th scope="col">Foto</th>
          <th scope="col">Diploma</th>
       
    </tr>
  </thead>
<tbody>
    <asp:PlaceHolder ID="plTabellaEquiparazione" runat="server"></asp:PlaceHolder>

    </tbody>
            </table>


   


    </div>
    <script>
        function showToast(value) {
        
            toastr.options = {
                closeButton: true,
                positionClass: 'toast-top-right',
                timeOut: 5000,

                showDuration: 1000
            };

            if (value == "tesserino")
                toastr.success('Tesserino tecnico in download', 'ASI');
            if (value == "diploma")
            
                toastr.success('Diploma in download', 'ASI');
        }

    </script>
</asp:Content>
