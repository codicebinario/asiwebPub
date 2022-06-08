<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPage.Master" CodeBehind="Dashboard.aspx.vb" Inherits="ASIWeb.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            width:180px;


        }
 
    </style>
       <link rel="stylesheet" href="css/alertify.min.css" />
     <link rel="stylesheet" href="css/themes/default.min.css" />
      <script type="text/javascript" src="Scripts/alertify.js"></script>
  
    <script>
        //function myConfirm() {
        //    var result = alertify.confirm("Want to delete?");
        //    if (result == true) {
        //        return true;
        //    } else {
        //        return false;
        //    }
        //    return false;
        //}

        function myConfirm() {
            
            let text = "Sicuro di voler \n annullare questo ordine?";
            if (confirm(text) == true) {
                return true;
            } else {
                return false;
            }
        }

        //function getConfirmation() {
        //    var r = confirm("Press a button!");
        //    if (r == true) {
        //        return true;
        //    } else {
        //        return false;
        //    }
        //}

      
    //        function CallConfirmBox() {
    //    if (confirm("Confirm Proceed Further?")) {
    //            //OK - Do your stuff or call any callback method here..
    //            alert("You pressed OK!");
    //    } else {
    //            //CANCEL - Do your stuff or call any callback method here..
    //            alert("You pressed Cancel!");
    //    }
    //}
   

      
    

    </script>




     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        //alertify.minimalDialog || alertify.dialog('minimalDialog', function () {
        //    return {
        //        main: function (content) {
        //            this.setContent(content);
        //        }
        //    };
        //});

      //  alertify.alert("Minimal button-less dialog.");
     //   alertify.alert('ASI', 'File Caricato! ' ).set('resizable', true).resizeTo('20%', 200);
      //  alertify.alert('ASI', 'File Caricato! ' + response).set('resizable', true).resizeTo('20%', 200);
    //  alertify.alert('Alert Title', 'Alert Message!', function () { alertify.success('Ok'); });

    </script>
  


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
    <h3 class="display-5">Ordini Attivi</h3>
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





    </div>
</asp:Content>
