<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageMain.Master" CodeBehind="passC.aspx.vb" Inherits="ASIWeb.passC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-sm-6 centro d-flex align-items-center min-vh-100">
       <div class="jumbotron  my-auto jumbotron-fluid rounded">
  <div class="container">
    <h3 class="display-5">Cambio Password</h3>
  
     
      <div class="form-group">
    <label for="txtNewPassoword" class="bianco">Password</label>

             <asp:TextBox ID="txtNewPassoword" required="required" CssClass="form-control" runat="server" autofocus="autofocus"
                 TextMode="password" placeholder="Password" oninput="this.setCustomValidity('')" oninvalid="this.setCustomValidity('Inserire la nuova password')"></asp:TextBox>
 <small id="passwordHelpBlock" class="bianco">
 minimo 8 e massimo 10 caratteri, almeno un carattere maiuscolo e uno minuscolo, almeno un numero ed un carattere speciale (@$!%*?).
</small><br /><asp:RequiredFieldValidator ID="rqNewPassWord" Display="Dynamic" ControlToValidate="txtNewPassoword"  EnableClientScript="true" runat="server" ErrorMessage="[*]" CssClass="errore"></asp:RequiredFieldValidator>
          <asp:regularexpressionvalidator id="rexpPassword" runat="server" Display="Dynamic" controltovalidate="txtNewPassoword"
								errormessage="password non valida" 
                                validationexpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?])[A-Za-z\d@$!%*?]{8,}$"
                                CssClass="errore" EnableClientScript="true"></asp:regularexpressionvalidator><br />

           <label for="txtNewPasswordControllo" class="bianco">Ripeti la stessa Password</label>

             <asp:TextBox ID="txtNewPasswordControllo" required="required" CssClass="form-control" runat="server" TextMode="password" placeholder="Password" oninput="this.setCustomValidity('')" oninvalid="this.setCustomValidity('Inserire la stessa password')"></asp:TextBox>
 <small id="passwordHelpBlock1" class="bianco">
  devi inserire la stessa password nei 2 campi.
</small><br />
          <asp:RequiredFieldValidator ID="rqCompPass" Display="Dynamic" ControlToValidate="txtNewPasswordControllo"  EnableClientScript="true" runat="server" ErrorMessage="[*]" CssClass="errore"></asp:RequiredFieldValidator>
          <asp:comparevalidator id="compPass" runat="server" Display="Dynamic" cssclass="errore" errormessage="digita due volta la stessa password"
								controltovalidate="txtNewPasswordControllo" controltocompare="txtNewPassoword" 
                                CultureInvariantValues="True" EnableClientScript="true"></asp:comparevalidator>

        <br /><br />
             <asp:Button ID="Button1" class="btn btn-lg btn-custom btn-block mt-4" runat="server" Text="Cambia" />
      </div>



          
    
           
                     
     
       
         
  </div></div>
               
  
  </div>


</asp:Content>
