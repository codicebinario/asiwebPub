<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageRinnovi2.Master" CodeBehind="DashBoardRinnovi2.aspx.vb" Inherits="ASIWeb.DashBoardRinnovi2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <link rel="stylesheet" href="../css/alertify.min.css" />
    <link rel="stylesheet" href="../css/themes/default.min.css" />
    <script type="text/javascript" src="../Scripts/alertify.js"></script>
 <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.css" integrity="sha512-3pIirOrwegjM6erE5gPSwkUzO+3cTjpnV9lexlNZqvupR64iZBnOOTiiLPb9M36zpMScbmUNIcHUqKD47M719g==" crossorigin="anonymous" referrerpolicy="no-referrer" />

    <style>
        .accordion-button{
            background-color: #eeeee4;

        }

        .accordion-button:not(.collapsed) {
  color: black;
    background-color:#F8F4EA;
/*    box-shadow: inset 0 -1px 0 rgb(0 0 0 / 13%);
    border-left: 2em solid red;*/
}
    </style>
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
          
      /*  font-size: xx-small;*/


        }
         .fs-5{
               color:white;
           }
        .piccolo{

 font-size: small;
        }
           a:target {

           
    font-size: x-large;
      background: yellow;
    }
           .fs-5{
               color:white;
           }
 
  .moltopiccolo {
      font-size:small;
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

            let text = "Sei sicuro di voler annullare questa richiesta?";
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

    <%--    <script>
        function js_onload_code() {

            alert(" Hello, you are learning onload event in JavaScript");

        }

        window.onload = js_onload_code();
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

  
    <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasRight" aria-labelledby="offcanvasRightLabel">
        <div class="offcanvas-header">
            <h5 class="offcanvas-title" id="offcanvasRightLabel">Rinnovi Attivi</h5>
            <button type="button" class="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>
        </div>
        <div class="offcanvas-body piccolo">
            <p>I rinnovi vengono organizzati in gruppi. Ogni gruppo può contenere da 1 a n rinnovi. Lo scopo è quello
                di ottimizzare i costi di spedizione se richiesto il cartaceo. </p>
            <p>Una nuova richiesta di rinnovi si crea dal secondo menù orizzontale [Nuova Richiesta].
             
            </p>
            <p>In questa pagina le richieste sono rappresentate da voci di gruppo espandibili e comprimibili. 
                Con un click su una riga di richiesta, apriamo il contenuto disponibile.
            </p>
            <p>Si possono effettuare le seguenti azioni:</p>
          <ol>
              <li> <p class="fw-bold">aggiungere un nuovo rinnovo alla richiesta</p>
                 <P>parte il veloce processo per aggiungere un nuovo rinnovo all richiesta.</P>
                   
              </li>
              <li><p class="fw-bold">terminare la richiesta</p>
                 <p>  quando abbiamo terminato di aggiungere i rinnovi desiderati e vogliamo procedere al pagamento
                la richiesta va terminata. Una volta terminata la richiesta non si potranno aggiungere altri rinnovi e si
                potrà procedere al pagamento. La richiesta mostrerà il prezzo da pagare, diviso in costo rinnovo, costo spedizione
                e totale.<br />
                  Apparirà il pulsante per procedere al caricamento del giustificativo pagamento effettuato. I formati accettati sono
                immagini jpg, png e documenti pdf.</p>
              </li>
              <li> <p class="fw-bold">cancellare una richiesta rinnovo</p>
                <p>  ogni momento, se non ancora terminata la richiesta, si potrà eliminare
                      un singolo rinnovo e si potrà eliminare l'intera richiesta con tutti i rinnovi ad essa associati.</p>

              </li>
              <li> <p class="fw-bold">caricare una foto per la tessera (opzionale)</p>
                <p>è possibile opzionalmente caricare una foto per la tessera.</p> 
              </li>
          </ol>
           <p>

           </p>
        </div>
    </div>

    <div class="row">
        <div class="col-sm-12 mb-3 mb-md-0">
            <div class="jumbotron jumbotron-custom jumbotron-fluid rounded">
                <div class="container">
                  <h6 class="fs-5"><a class="text-white text-decoration-none" data-bs-toggle="offcanvas" href="#offcanvasRight" role="button" aria-controls="offcanvasRight">Rinnovi Attivi (help)
                    </a></h6>
                      <p class="lead">
                        <asp:Literal ID="litDenominazioneJumboDash" runat="server"></asp:Literal>

                    </p>
                </div>
            </div>
        </div>
    </div>

    <%--<div class="popover__wrapper">
        <a href="#">
            <h2 class="popover__title">Hover:me</h2>
        </a>
        <div class="popover__content">
            <p class="popover__message">Joseph Francis "Joey" Tribbiani, Jr.</p>
            <img alt="Joseph Francis Joey Tribbiani, Jr." src="https://media.giphy.com/media/11SIBu3s72Co8w/giphy.gif">
        </div>
    </div>--%>


    <div class="row d-flex justify-content-center">

        <asp:PlaceHolder ID="phDash" runat="server" Visible="false"></asp:PlaceHolder>
       
    <%--<div class="accordion" id="accordionDash">
        <asp:Repeater ID="rpDash" runat="server">
            <ItemTemplate>
            
                    <div class="accordion-item">
                        <h2 class="accordion-header" id="heading_<%#Eval("IDRinnovoM")%>">
                            <%If (Eval("IDRinnovoM") = "44") %>
                            <button class="accordion-button collapsed show" type="button" data-bs-toggle="collapse" data-bs-target="#collapse<%#Eval("IDRinnovoM")%>" aria-expanded="False" aria-controls="collapse<%#Eval("IDRinnovoM")%>">
                           <%else%>
                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapse<%#Eval("IDRinnovoM")%>" aria-expanded="False" aria-controls="collapse<%#Eval("IDRinnovoM")%>">

                                    <%end if %>   
                                <%#Eval("IDRinnovoM")%>
                                </button>
                                </h2>
                        <div id="collapse<%#Eval("IDRinnovoM")%>" class="accordion-collapse collapse" aria-labelledby="heading_<%#Eval("IDRinnovoM")%>" data-bs-parent="#accordionDash">
                         <div class="accordion-body"> 
                             Hello
                             </div></div>
                    </div>
                                

            

            
            </ItemTemplate>


        </asp:Repeater>
  </div>--%>

        </div>
    <br />
    <br />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-kenU1KFdBIe4zVF0s0G1M5b4hcpxyD9F7jL+jjXkk+Q2h455rYXK/7HAuoJl+0I4" crossorigin="anonymous"></script>

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
            if (value == "zip")
                toastr.success('Tessere (file zip) in download', 'ASI');
        }
        if (location.hash !== null && location.hash !== "") {
          
            document.addEventListener("DOMContentLoaded", function (event) {
                var home_link = document.getElementById('collapse44');
                home_link.className = home_link.className + ' active';
            });
        };
       
    </script>
   <style>
   
   </style>
</asp:Content>
