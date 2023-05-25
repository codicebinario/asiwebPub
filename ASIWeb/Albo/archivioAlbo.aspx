<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageAlbo.Master" CodeBehind="archivioAlbo.aspx.vb" Inherits="ASIWeb.archivioAlbo" %>
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
         width:220px;
      /*  font-size: x-small;*/


        }
         .errore { color:red;


    }
        .piccolo{

 font-size: small;
        }
       a:target {

           
    font-size: x-large;
      background: yellow;
    }
    </style>
    
    <link rel="stylesheet" href="../css/alertify.min.css" />
     <link rel="stylesheet" href="../css/themes/default.min.css" />
      <script type="text/javascript" src="../Scripts/alertify.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
 <div class="col-sm-12 mb-3 mb-md-0">
       <div class="jumbotron jumbotron-custom jumbotron-fluid rounded">
  <div class="container">
    <h3 class="display-5">Corsi Evasi</h3>
    <p class="lead">
  <asp:Literal ID="litDenominazioneJumboDash" runat="server"></asp:Literal>
               
    </p>
  </div>
</div>


 </div>




</div>
  
    <div class="col-sm-12">
        <div class="row d-flex justify-content-center">
            <div class="col-sm-6">
                <div class="form-group">
                    <label for="txtNome">Numero Corso [*] </label>
                    <asp:TextBox ID="txtNumeroCorso" CssClass="form-control" runat="server" MaxLength="250"></asp:TextBox>

                </div>
            </div>
            <div class="col-sm-2">
                <div class="form-group">
                    <label for="txtNomexxxxxxxxxxx">--- </label>
                    <br />
                    <%--   <asp:Button ID="btnCheck" runat="server" Text="Trova" class="btn btn-primary" />--%>
                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary"><i class="bi bi-person-badge"> </i>Trova</asp:LinkButton>
                </div>
            </div>


            <div class="col-sm-2">
                <div class="form-group">
                    <label for="txtNomexxxxxxxxxxx">--- </label>
                    <br />


                    <%--  <asp:Button ID="btnUltimi5" runat="server" Text="Ultimi 10.."  class="btn btn-primary"    />--%>
                    <asp:LinkButton ID="lnkLast10" runat="server"  CausesValidation="false" CssClass="btn btn-primary"><i class="bi bi-list-task"> </i>Ultimi 10..</asp:LinkButton>

                </div>
            </div>

        </div>

        <div class="row d-flex justify-content-center">
            <div class="col-sm-10">
                <div class="form-group">

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                        CssClass="errore" ErrorMessage="Numero Corso " ControlToValidate="txtNumeroCorso"
                        EnableClientScript="true"></asp:RequiredFieldValidator>

                </div>
            </div>
       



        </div>
   </div>    
  
   
 <div class="row d-flex justify-content-center">
   

     <asp:PlaceHolder ID="phDash" runat="server" Visible="false"></asp:PlaceHolder>

     <asp:PlaceHolder ID="phDash10" runat="server" Visible="false"></asp:PlaceHolder>












 </div>


      

</asp:Content>
