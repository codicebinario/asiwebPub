<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageEqui2.Master" CodeBehind="archivioEquiValutati2.aspx.vb" Inherits="ASIWeb.archivioEquiValutati2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../css/alertify.min.css" />
    <link rel="stylesheet" href="../css/themes/default.min.css" />
    <script type="text/javascript" src="../Scripts/alertify.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-kenU1KFdBIe4zVF0s0G1M5b4hcpxyD9F7jL+jjXkk+Q2h455rYXK/7HAuoJl+0I4" crossorigin="anonymous"></script>
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
         width:160px;
        font-size: x-small;


        } .moltopiccolo {
      font-size:small;
  }
        .piccolo{

 font-size: small;
        }
         a:target {

           
    font-size: x-large;
      background: yellow;
    }

          .section-divider {
  height: 0;
  border-top: 1px solid #DDD;
  text-align: center;
  margin-top: 40px;
  margin-bottom: 40px;
}

.section-divider > span {
  color: #3498db;
  background: #FAFAFA;
  display: inline-block;
  position: relative;
  padding: 0 17px;
  top: -11px;
  font-size: 15px;
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
  
      <h6 class="fs-5 text-white text-decoration-none">Equiparazioni Valutate</h6>
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
                    <label for="txtNome">Codice Fiscale oppure Numero Richiesta [*] </label>
                    <asp:TextBox ID="txtCodiceFiscale" CssClass="form-control" runat="server" MaxLength="250"></asp:TextBox>

                </div>
            </div>
            <div class="col-sm-2">
                <div class="form-group">
                    <label for="txtNomexxxxxxxxxxx">--- </label>
                    <br />

                    <asp:LinkButton ID="lnkCheck" runat="server" CssClass="btn btn-primary"><i class="bi bi-person-badge"> </i>Trova</asp:LinkButton>

                </div>
            </div>


            <div class="col-sm-2">
                <div class="form-group">
                    <label for="txtNomexxxxxxxxxxx">--- </label>
                    <br />

                    <asp:LinkButton ID="lnkLast10" runat="server" CausesValidation="false" CssClass="btn btn-primary"><i class="bi bi-list-task"> </i>Ultimi 10..</asp:LinkButton>

                </div>
            </div>

        </div>

    </div>

    <div class="col-sm-12">
        <div class="row d-flex justify-content-center">
            <div class="col-sm-6">
                <div class="form-group">

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                        CssClass="errore" ErrorMessage="Codice Fiscale oppure Numero Richiesta " ControlToValidate="txtCodiceFiscale"
                        EnableClientScript="true"></asp:RequiredFieldValidator>

                </div>
            </div>
            <div class="col-sm-6">
                <div class="form-group">
                </div>
            </div>



        </div>

    </div>
   
 <div class="row d-flex justify-content-center">

   
     <asp:PlaceHolder ID="phDash" runat="server" Visible="false"></asp:PlaceHolder>


     <asp:PlaceHolder ID="phDash10" runat="server" Visible="false"></asp:PlaceHolder>
             







 </div>


</asp:Content>
