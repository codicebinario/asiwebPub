<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPage.Master" CodeBehind="Archivio.aspx.vb" Inherits="ASIWeb.Archivio" %>
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
    <h3 class="display-5">Ordini Evasi</h3>
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
