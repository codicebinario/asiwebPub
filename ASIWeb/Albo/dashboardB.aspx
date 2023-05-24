<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageAlbo.Master" CodeBehind="dashboardB.aspx.vb" Inherits="ASIWeb.dashboardB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../css/alertify.min.css" />
    <link rel="stylesheet" href="../css/themes/default.min.css" />
    <script type="text/javascript" src="../Scripts/alertify.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.css" integrity="sha512-3pIirOrwegjM6erE5gPSwkUzO+3cTjpnV9lexlNZqvupR64iZBnOOTiiLPb9M36zpMScbmUNIcHUqKD47M719g==" crossorigin="anonymous" referrerpolicy="no-referrer" />

    <style>

        .card-title{
    font-size:0.8rem;
    text-overflow: ellipsis;
    white-space: nowrap;
    overflow: hidden;
}

       a {color: #005a7c;}
        a:hover {color: darkred;}
   
        .btn-custom {
         width:220px;
      /*  font-size: small;*/


        }
         .fs-5{
               color:white;
           }
        .piccolo{

 font-size: small;
        }
          .moltopiccolo {
      font-size:small;
  }
           a:target {

           
    font-size: x-large;
      background: yellow;
    }
 
    </style>
    
  

    <script>
        function myConfirm() {

            let text = "Andando avanti non si potranno più caricare \n foto di questo corso?";
            if (confirm(text) == true) {
                return true;
                //  text = "You pressed OK!";
            } else {
                return false;
                //    text = "You canceled!";
            }
        }

        function myAnnulla() {

            let text = "Sei sicuro di voler annullare questo corso?";
            if (confirm(text) == true) {
                return true;
                //  text = "You pressed OK!";
            } else {
                return false;
                //    text = "You canceled!";
            }
        }


        //alertify.confirm('Confirm Title', 'Confirm Message', function () { alertify.success('Ok') }
        //    , function () { alertify.error('Cancel') });
      

    </script>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
          <asp:UpdateProgress ID="UpdateProgress1" runat="server"
					 DynamicLayout="false">
	<ProgressTemplate>
	  <div class="Progress">
		 <div class="btn">loading...</div>
		
		</div>
	</ProgressTemplate>
</asp:UpdateProgress>

<div class="row">
 <div class="col-sm-12 mb-3 mb-md-0">
       <div class="jumbotron jumbotron-custom jumbotron-fluid rounded">
  <div class="container">
    <h6 class="fs-5">Corsi Attivi</h6>
    <p class="lead">
  <asp:Literal ID="litDenominazioneJumboDash" runat="server"></asp:Literal>
               
    </p>
  </div>
</div>


 </div>




</div>

 <div class="row d-flex justify-content-center">
   

     <asp:PlaceHolder ID="phDash" runat="server" Visible="false"></asp:PlaceHolder>


 
  






     

</div>

        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-kenU1KFdBIe4zVF0s0G1M5b4hcpxyD9F7jL+jjXkk+Q2h455rYXK/7HAuoJl+0I4" crossorigin="anonymous"></script>

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

        //if (location.hash !== null && location.hash !== "") {
        //    //alert("ciao");
        //    //alert(location.hash);
        //    //  $(location.hash + ".collapse").collapse("show");
        //    //$(".collapse44").collapse("show");
        //    //$(".collapse44").collapse("toggle");
        //    //$(".collapse44").addClass("show");
        //    document.addEventListener("DOMContentLoaded", function (event) {
        //        var home_link = document.getElementById('collapse44');
        //        home_link.className = home_link.className + ' active';
        //    });
        //};

    </script>

</asp:Content>
