<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="login.aspx.vb" Inherits="ASIWeb.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
   <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous" />

    <link href="Css/signin.css" rel="stylesheet" />
    <style>

        /* Modify the background color */

        .form-control-custon {
          
            border-color: #005a7c;
        }

       .form-control:focus {
            border-color: #005a7c;
            box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.075) inset, 0px 0px 8px rgba(0, 90, 124, 0.5);
    }

       
         
        .btn-custom  {
            background-color: #005a7c;
            color:white;
            box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.075) inset, 0px 0px 8px rgba(0, 90, 124, 0.5);
         
        }
        .jumbotron-image {
              background-position: center center;
              background-repeat: no-repeat;
              background-size: cover;
}  </style>
    <title>ASI - Login</title>
</head>
<body>
    <form id="form1" runat="server" class="form-signin">
        <div>
<div class="jumbotron jumbotron-fluid jumbotron text-white jumbotron-image shadow"  style="background-image: url(img/bg.png?fit=crop&w=800&q=80);">
  <div class="container text-center">
    <h2 class="display-5">ASI Nazionale</h2>
     <small>Associazioni Sportive e Sociali Italiane </small>
  </div>
</div>
  
  <h1 class="h3 mb-3 text-center font-weight-normal">Log In</h1>
  <label for="inputUserID" class="sr-only">UserID</label>
 
<asp:TextBox ID="txtUserID"  class="form-control form-control-custon" runat="server" placeholder="UserID" required="required" oninvalid="this.setCustomValidity('Inserire il proprio UserID')"
    oninput="this.setCustomValidity('')"  autofocus="autofocus" ></asp:TextBox>

  <label for="inputPassword" class="sr-only ">Password</label>
 
 <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" class="form-control form-control-custon" placeholder="Password" required="required" oninvalid="this.setCustomValidity('Inserire la password')"
    oninput="this.setCustomValidity('')"></asp:TextBox>
 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPassword" ErrorMessage=""></asp:RequiredFieldValidator>
   <asp:Button class="btn btn-lg btn-custom btn-block mt-4" ID="btnLogIn" runat="server" Text="Log in" />
<asp:Literal ID="ltlerrore" runat="server" Visible="false">
   <div class="alert alert-danger alert-dismissible fade show" role="alert">
  <strong>Attenzione!</strong><br /> database in manutenzione, riprova tra qualche minuto!
  <button type="button" class="close" data-dismiss="alert" aria-label="Close">
    <span aria-hidden="true">&times;</span>
  </button>
</div>
 </asp:Literal>
 <asp:Literal ID="ltlAvviso" runat="server" Visible="false">
                <div class="alert alert-danger alert-dismissible fade show" role="alert">
  <strong>Attenzione!</strong><br /> Accesso non consentito!
  <button type="button" class="close" data-dismiss="alert" aria-label="Close">
    <span aria-hidden="true">&times;</span>
  </button>
</div>


            </asp:Literal>

<asp:Panel ID="pnlPAssCambiata" runat="server" Visible="false">
 <div class="alert alert-warning alert-dismissible fade show" role="alert">
  <strong> Password cambiata correttamente.</strong><br />Inserire la nuova password per accedere.
               
   
 
</div>
 </asp:Panel>
            
           <asp:Panel ID="pnlPAssCambiata1" runat="server" Visible="false">
 <div class="alert alert-warning alert-dismissible fade show" role="alert">
  <strong>Password non cambiata!</strong><br /> Riprovare.
               
   
 
</div>
 </asp:Panel>

<%--<asp:Label ID="lblAvviso" class="alert alert-danger alert-dismissible fade show form-control" role="alert" runat="server" Text="" Visible="false">
     <button type="button" class="close" data-dismiss="alert" aria-label="Close">
    <span aria-hidden="true">&times;</span>
  </button>
</asp:Label>--%>
   
       
            </div>
    <%--   <div class="card" style="width: 18rem;">
  <div class="card-body">
    <h5 class="card-title">Esempio</h5>
    <h6 class="card-subtitle mb-2 text-muted">Temporaneo</h6>
    <p class="card-text">UserID/Password: admin/admin</p>
    
  </div>--%>
</div>

    </form>
<!-- Optional JavaScript -->
    <!-- jQuery first, then Popper.js, then Bootstrap JS -->
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>
  </body>
</html>