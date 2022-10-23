<%@ Page Title="" Language="vb" ValidateRequest="false" AutoEventWireup="false" MasterPageFile="~/AsiMasterPage.Master" CodeBehind="richiestaTessere.aspx.vb" Inherits="ASIWeb.richiestaTessere" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
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
    <script type = "text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnSave.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>
    <style>

           a {color: #005a7c;}
        a:hover {color: darkred;}

        .btn-custom {
            background-color: #005a7c;
            color: white;
            box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.075) inset, 0px 0px 8px rgba(0, 90, 124, 0.5);
        }
        .badge-custom {
            background-color: #005a7c;
            color: white;
        /*    box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.075) inset, 0px 0px 8px rgba(0, 90, 124, 0.5);*/
        }
          .form-control-custon {
          
            border-color: #005a7c;
        }

       .form-control:focus {
            border-color: #005a7c;
            box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.075) inset, 0px 0px 8px rgba(0, 90, 124, 0.5);
    }

    </style>
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
       <div class="jumbotron jumbotron-fluid rounded">
  <div class="container">
    <h3 class="display-5">Nuovo Ordine</h3>
    <p class="lead">
  <asp:Literal ID="litDenominazioneJumbo" runat="server"></asp:Literal>
               
    </p>
  </div>
</div>



   









    </div>
  
</div>



       <div class="row">
            <div class="col-sm">
           <h4>Numero Ordine 
             <span class="badge badge-custom ">
  <asp:Literal ID="LitNumeroRichiesta" runat="server"></asp:Literal>
            </span></h4>
         </div>

       </div>

       	<asp:updatepanel runat="server" ID="upPanel1">
							  <ContentTemplate>

        <!-- first row -->
        <div class="row">
            <!-- prima colonna -->

                    <div class="col-sm-3">
                        <div class="form-group">
                            <label for="idArticoli">Tipo Articolo</label>
                            <asp:DropDownList ID="dllArticoli" class="custom-select" runat="server"  CausesValidation="True" ></asp:DropDownList><br />
                        <div>
                            <asp:RequiredFieldValidator ID="rqArticoli" class="alert-danger " runat="server" ControlToValidate="dllArticoli" ErrorMessage="Seleziona un prodotto" InitialValue="seleziona"></asp:RequiredFieldValidator>
                        </div>


                    </div>
                    </div>

            <!-- fine prima colonna -->

       
                 <!-- quarta colonna -->
                <div class="col-sm-3">
                    <div class="form-group align-middle text-left">
                         <label for="txtQuantita">Quantità</label><br />
                        <asp:TextBox ID="txtQuantita" CssClass="form-control" runat="server"></asp:TextBox>
                           <asp:RequiredFieldValidator Display="Dynamic" ID="rqQuantita" class="alert-danger " runat="server" ControlToValidate="txtQuantita" ErrorMessage="Inserisci le quantità" ></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="rgQuantitaNum" runat="server" class="alert-danger " Display="Dynamic" MinimumValue="1" MaximumValue="100000" ControlToValidate="txtQuantita" type="Integer"  ErrorMessage="Inserisci un numero"></asp:RangeValidator>
                    </div>
                </div>
            <!-- fine quarta colonna -->

                   <!-- quinta colonna -->
                <div class="col-sm-1">
                    <div class="form-group align-middle text-center">
                         <label for=""> - </label><br />
                        <asp:LinkButton ID="lnkQuantita" runat="server" class="btn btn-custom align-middle text-left">
                           <i class="fa fa-angle-double-down"></i>
                        </asp:LinkButton>
                    
                    </div>
                </div>
            <!-- fine quinta colonna -->
        
        </div>

        <!-- fine first row -->
       </ContentTemplate>
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="lnkQuantita" EventName="Click" />
</Triggers>
		</asp:updatepanel>

	<asp:updatepanel runat="server" ID="UpPnlCarrello" >
				  <ContentTemplate>
<asp:Panel ID="PnlCarrello" runat="server" Visible="false">

 <div class="row">
            <div class="col-sm-8">
           <div class="table-responsive-md rounded">
<table class="table table-striped table-fit">
 
  <thead class="thead-dark">
    <tr>
           <th scope="col">-</th>
      <th scope="col">#</th>
      <th scope="col" class="text-center">Cod. Art.</th>
      <th scope="col" class="text-left">Art.</th>
      <th scope="col" class="text-center">Q.</th>
     <th scope="col" class="text-right">Pr. Un.</th>
      <th scope="col" class="text-right">Pr. Fi.</th>
    </tr>
  </thead>
    
  <tbody>

<asp:PlaceHolder ID="plCarrello" runat="server"></asp:PlaceHolder>
   
  
      

  </tbody>
</table>
 
           
           </div>
</div></div>


    <div class="row">
  <div class="col-sm-8">

      <div class="card mb-4">
  <h5 class="card-header">Conferma
         
             <span class="badge badge-custom">
  <asp:Literal ID="lblRichiestaConferma" runat="server"></asp:Literal>
            </span></h5>
  <div class="card-body">
    <h5 class="card-title">Conferma Ordine Tessere e/o Nulla Osta</h5>
    <p class="card-text">Opzionalmente utilizzare il checkbox sottostante per chiedere la verifica del prezzo!</p>
     

      <div class="col-8">
    <div class="form-control-plaintext">
       
         <asp:CheckBox ID="chkValutazioneSconto" class="form-check-input" runat="server" />
     <label class="form-check-label" for="chkValutazioneSconto">Verifica prezzo!</label>
           
   </div></div>

        <div class="col-8">
    <div class="form-control-plaintext"> <label class="form-check-label" for="txtNote">Note</label>
    
           <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <Triggers>
                 <asp:AsyncPostBackTrigger ControlID="txtNote" EventName="TextChanged" />
            </Triggers>
            <ContentTemplate>
                     
        <asp:TextBox ID="txtNote" CssClass="form-control" runat="server" AutoPostBack="true" ></asp:TextBox>
       
          </ContentTemplate>
        </asp:UpdatePanel>
    
           
   </div></div>


      
   
     <%-- <asp:Button ID="Button1" runat="server" CausesValidation="false" Text="Button" class="btn btn-primary   align-left m-2" data-toggle="modal" data-target="#ModalCenterSave"/>
      
        <button type="submit" class="btn btn-primary   align-left m-2" data-toggle="modal" data-target="#ModalCenterSave">
            Conferma Richiesta<i class="fa fa-drivers-license-o p-2"></i></button>--%>

      <asp:Button ID="btnSave" runat="server"   Text="Conferma" CausesValidation="false" class="btn btn-custom" />


     </div>
      </div>

   
  </div>
      
    
  </div>
</div>

        
      </div></div>

    <%--<!-- Modal -->



<div class="modal fade" id="ModalCenterSave" tabindex="-1" role="dialog" aria-labelledby="ModalCenterSaveTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="ModalCenterSaveLongTitle">Invia Richiesta</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        Click su <b>Conferma</b> per inviare la richiesta, click su <b>Chiudi</b> per non inviare oppure ulteriori modifiche!!
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Chiudi</button>
<asp:Button ID="btnSave" runat="server"  Text="Conferma" CausesValidation="false" class="btn btn-primary" UseSubmitBehavior="false" data-dismiss="modal"/>
     
       --%>

            
     

       
      </div>
    </div>
  </div>
</div>


    </asp:Panel>
             </ContentTemplate>
    </asp:updatepanel>



    

  


    



        </div>

     <script>

         //$('#button').submit(function (e) {
         //    e.preventDefault();
         //    // Coding
         //    $('#IDModal').modal('toggle'); //or  $('#IDModal').modal('hide');
         //    return false;
         //});
         // Add two numbers
         function Cancella(numero, numero1) {
        
        //     var firstNumber = document.getElementById('txtFirstNumber');
            // alert(numero)
             //var secondNumber = document.getElementById('txtSecondNumber');
           // alert(PageMethods.cancella2(parseInt(num.value), onSucceed, onError));
             PageMethods.delete(parseInt(numero), parseInt(numero1), onSucceed, onError);
            // PageMethods.AddTwoNumbers(parseInt(num.value), onSucceed, onError);
         }

         // On Success
         function onSucceed(results, currentContext, methodName) {
             if (results !== null) {
           //   document.getElementById('txtTotal').value = results;
               document.location.reload();
               //  PageMethods.aggiorna();
             }
         }

         // On Error
         function onError(results, currentContext, methodName) {
             console.log(results);
         }
     </script>
   
     
</asp:Content>
