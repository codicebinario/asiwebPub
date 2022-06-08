<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="passM.aspx.vb" Inherits="ASIWeb.passM" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ASI Cambio Password</title>
     <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
   <!-- Bootstrap CSS -->
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Css/custom.css" rel="stylesheet" />
 <!-- font awesome -->
    <link rel="stylesheet" href="font-awesome-4.7.0/css/font-awesome.min.css" />

    <!-- google fonts -->
    <link href="https://fonts.googleapis.com/css?family=Roboto"  rel="stylesheet"/>
    <link href="Css/sticky-footer-navbar.css" rel="stylesheet" />
    <script>
        document.addEventListener('invalid', (function () {
            return function (e) {
                //prevent the browser from showing default error bubble / hint
                e.preventDefault();
                // optionally fire off some custom validation handler
                // myValidation();
            };
        })(), true);

        function ClientValidate(source, arguments) {
            if (arguments.Value % 2 == 0) {
                arguments.IsValid = true;
            } else {
                arguments.IsValid = false;
            }
        }
       
     
    </script>
    <script type="text/javascript">
        jQuery(function ($) {
            $("form").keypress(function (e) {
                //Enter key
                if (e.which == 13) {
                    return false;
                }
            });
        });

    </script> 
     <style>
       
        .bianco {
            color:white;
           }
        .centro {

            margin:0 auto;
         }
        .errore {
            color:red;
            }

        .btn-custom  {
            background-color: cornflowerblue;
            color:white;
            box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.075) inset, 0px 0px 8px rgba(0, 90, 124, 0.5);
         
        }
          .form-control-custon {
          
            border-color: #005a7c;
        }

       .form-control:focus {
            border-color: #005a7c;
            box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.075) inset, 0px 0px 8px rgba(0, 90, 124, 0.5);
    }

       
      .bd-placeholder-img {
        font-size: 1.125rem;
        text-anchor: middle;
        -webkit-user-select: none;
        -moz-user-select: none;
        -ms-user-select: none;
        user-select: none;
      }

      @media (min-width: 768px) {
        .bd-placeholder-img-lg {
          font-size: 3.5rem;
        }
      }
         /* Modify the background color */
       
        .navbar-custom  {
            background-color: #005a7c;
         
        }
        /* Modify brand and text color */
         
        .navbar-custom .navbar-brand,
        .navbar-custom .navbar-text {
            color: aliceblue;
        }
          /* Modify the background color */
         .jumbotron { 
                     margin-top: -50px;
                      padding-top:10px; 
                      padding-bottom: 10px;
                       background-color: #005a7c;
         }
         .display-5 {

               color: #fff;

         }
         .jumbotron h1{
             color: #fff;
            }

            .jumbotron p{
            color: #fff;
        }
    

    </style>
  
</head>
<body>
    <form id="form1" runat="server">
          <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true"  runat="server">
     </asp:ScriptManager>
        <asp:HiddenField runat="server" ID="nascosto" />
        <div>
            <div class="col-sm-4 centro d-flex align-items-center min-vh-100">
       <div class="jumbotron  my-auto jumbotron-fluid rounded">
  <div class="container">
    <h3 class="display-5">Cambio Password</h3>
  
     
      <div class="form-group">
    <label for="txtNewPassoword" class="bianco">Nuova Password</label>
            
             <asp:TextBox ID="txtNewPassoword" required="required" CssClass="form-control" runat="server" 
                 TextMode="password" placeholder="Password"  ></asp:TextBox>
               
 <small id="passwordHelpBlock" class="bianco">
 minimo 8 caratteri e massimo 10, almeno una lettera e un numero.
</small><br /><asp:RequiredFieldValidator ID="rqNewPassWord" Display="Dynamic" ControlToValidate="txtNewPassoword"  EnableClientScript="true" runat="server" ErrorMessage="Inserire nuova password" CssClass="errore"></asp:RequiredFieldValidator>
          <asp:regularexpressionvalidator id="rexpPassword" runat="server" Display="Dynamic" controltovalidate="txtNewPassoword"
								errormessage="password non valida" 
                                validationexpression="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,10}$"
                                CssClass="errore" EnableClientScript="true"></asp:regularexpressionvalidator><br />
        
         
          
          <asp:CustomValidator ID="checkSeDiversa" ErrorMessage="questa password risulta essere uguale a quella già utilizzata: occorre inserire una nuova password" EnableClientScript="true" ControlToValidate="txtNewPassoword" ClientValidationFunction="PassTheSame" CssClass="errore"  runat="server" Display="Dynamic" ></asp:CustomValidator><br />
     
       
          
          
          
          
          
          
          
          <label for="txtNewPasswordControllo" class="bianco">Confermare la nuova Password</label>

             <asp:TextBox ID="txtNewPasswordControllo" required="required" CssClass="form-control" runat="server" TextMode="password" placeholder="Password" ></asp:TextBox>
 <small id="passwordHelpBlock1" class="bianco">
  devi inserire la stessa password nei 2 campi.
</small><br />
          <asp:RequiredFieldValidator ID="rqCompPass" Display="Dynamic" ControlToValidate="txtNewPasswordControllo"  EnableClientScript="true" runat="server" ErrorMessage="digita due volta la stessa password" CssClass="errore"></asp:RequiredFieldValidator>
          <asp:comparevalidator id="compPass" runat="server" Display="Dynamic" cssclass="errore" errormessage="digita due volta la stessa password"
								controltovalidate="txtNewPasswordControllo" controltocompare="txtNewPassoword" 
                                CultureInvariantValues="True" EnableClientScript="true"></asp:comparevalidator>

        <br /><br />
             <asp:Button ID="btnCambia" class="btn btn-lg btn-custom btn-block mt-4" runat="server" Text="Procedi" />
      </div>
      <asp:Panel ID="pnlPAssCambiata" runat="server" Visible="false">
          <div class="alert alert-warning alert-dismissible fade show" role="alert">
  <strong>Password cambiata!</strong> Mi raccomando di non dimenticare la password appena immessa.
                <asp:Button ID="btnVaiALogin" class="btn btn-lg btn-custom btn-block mt-4" runat="server" Text="Entra in ASI Web" ValidateRequestMode="Disabled" CausesValidation="false"  /><br />
  
    <span aria-hidden="true">&times;</span>
 
</div>
      </asp:Panel>
      <div>



      </div>

          
    
           
                     
     
       
         
  </div></div>
               
  
  </div>
        </div>
    </form>
</body>
</html>

<script>


    function PassTheSame(source, arguments) {



        if (document.getElementById("txtNewPassoword").value == document.getElementById("nascosto").value) {
         //   alert("false")
            arguments.IsValid = false;

        }

        else {
        //    alert("true")
            arguments.IsValid = true;

        }

    }
</script>
