<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="upc.aspx.vb" Inherits="ASIWeb.upc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
   <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="css/alertify.min.css" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
 <!-- font awesome -->
    <link rel="stylesheet" href="font-awesome-4.7.0/css/font-awesome.min.css" />

    <!-- google fonts -->
    <link href="https://fonts.googleapis.com/css?family=Roboto"  rel="stylesheet"/>
    <link href="Css/sticky-footer-navbar.css" rel="stylesheet" />    <title></title>
    <link href="Css/drop.css" rel="stylesheet" />
    <link href="Css/legacy.css" rel="stylesheet" />
</head>
<body class="d-flex flex-column h-100"> 
    <form id="form1" runat="server">
         <asp:label ID="hcodR" Visible="false" runat="server" />
        <header>
  <!-- Fixed navbar -->
  <nav class="navbar navbar-custom navbar-dark  navbar-expand-md fixed-to">
    <a class="navbar-brand" href="#">ASI</a>
    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarCollapse" aria-controls="navbarCollapse" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarCollapse">
      <ul class="navbar-nav mr-auto">
        <li class="nav-item">
          <a class="nav-link" href="Dashboard.aspx">Dashboard <span class="sr-only">(current)</span></a>
        </li>
       
       
          
         <li class="nav-item">
             <span class="nav-link">
           
             
             <asp:Literal ID="litDenominazione" runat="server"></asp:Literal>
                  </span>
         
        </li>

           <li class="nav-item">
                  <asp:LinkButton class="nav-link" ID="lnkOut" CausesValidation="false" runat="server">( Log out )</asp:LinkButton>
         
        </li>
      </ul>
    <%--  <form class="form-inline mt-2 mt-md-0">
        <input class="form-control mr-sm-2" type="text" placeholder="Search" aria-label="Search">
        <button class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
      </form>--%>
    </div>
  </nav>
</header>
        
   
        <main role="main" class="flex-shrink-0">
         <div class="container">

<div class="col-sm-12 mb-3 mb-md-0">
  <div class="jumbotron jumbotron-fluid rounded">
  <div class="container">
     <div class="row">
    <div class="col-sm">
        <h3 class="display-5">Drag and Drop caricamento documenti</h3> 
       
    </div>
</div>
<div class="row">
     <div class="col-sm">
 <p>Funziona con Chrome, Firefox 3.6+, IE10+, e Opera 12+ - [    <!-- Button trigger modal -->
        <a href="#" class="badge bg-uno" data-toggle="modal" data-target="#ModalUp">Info</a> ]
</p>
         <p><asp:LinkButton ID="lnkUpLegacy" CssClass="legacy"  runat="server" Text="[ Upload browser non compatibili ]"></asp:LinkButton>
    </p>
         </div>
   
    <div class="col-6 pb-0 text-right">
      <h6 class="display-5">
         [ <asp:Literal ID="litRichiesta" runat="server"></asp:Literal>]</h6>
        <p>

            <a href="Dashboard.aspx" class="legacy">[ Dashboard ]</a>
          
        </p>
       

        </div>


</div>

   
   
  
               
 
  </div>
</div>



   









    </div>

            


  <div id="dropSection" >
    <%-- <h3><span class="badge badge-primary">Drag and Drop</span></h3>--%>
        <span class="message">Drag and Drop il documento per caricarlo!.
            <br />
           </span>
    </div>
   <%-- <br />--%>
 <%--   File Caricati:--%>
    <hr />
    <div id="uploadedFiles">
    </div>
    <input type="button" id="btnUpload" value="Upload" />
  <asp:Label ID="filecaricati" runat="server" Text=""></asp:Label>


       
    <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
    <script type="text/javascript" src="Scripts/filedrop.js"></script>
     <script type="text/javascript" src="Scripts/alertify.js"></script>
     <script type="text/javascript">
         //override defaults
         alertify.defaults.transition = "slide";
         alertify.defaults.theme.ok = "btn btn-primary";
         alertify.defaults.theme.cancel = "btn btn-danger";
         alertify.defaults.theme.input = "form-control";
     </script>
    <script type="text/javascript">
        $(function () {
            var dropbox = $('#dropSection'),
                message = $('.message', dropbox);

            $("#dropSection").filedrop({
                maxfiles: 1,
                maxfilesize: 5, 
                fallback_id: 'btnUpload',
                fallback_dropzoneClick: true, 
                paramName: 'fileData',
                
             <%--   url: '<%="Models/HandlerVB.ashx?hcodR="%><%=hcodR.Text %>',--%>
                url: 'Uploader.aspx?hcodR=<%=hcodR.Text %>',
                allowedfiletypes: ['image/jpeg', 'image/png',  'application/pdf'],
                allowedfileextensions: [ '.jpg', '.jpeg', '.png',  'pdf'],
              
                dragOver: function () {
                    $('#dropSection').addClass('active');
                },
                dragLeave: function () {
                    $('#dropSection').removeClass('active');
                },
                drop: function () {
                    $('#dropSection').removeClass('active');
                },
               
                uploadFinished: function (i, file, response, time) {
                //    alert("response " + response);
                    alertify.alert('ASI', ' ' + response).set('resizable', true).resizeTo('20%', 200);
                    $('#uploadedFiles').append(' ' + response + '<br />')
                  /*  $('#uploadedFiles').append(file.name + '<br />')*/
                },
               
                error: function (err, file) {
                 
                    switch (err) {
                        case 'BrowserNotSupported':
                            alertify.alert('ASI', 'Il tuo browser non suppora Drag and Drop!').set('resizable', true).resizeTo('20%', 200);

                      //      alert('browser does not support HTML5 drag and drop')
                            break;
                        case 'TooManyFiles':
                            // user uploaded more than 'maxfiles'
                            alertify.alert('ASI', 'Numero massimo di file caricati!').set('resizable', true).resizeTo('20%', 200);

                            break;
                        case 'maxfilesexceeded':
                            alertify.alert('ASI', 'Numero massimo di file caricati!').set('resizable', true).resizeTo('20%', 200);

                            break;

                        case 'FileTooLarge':
                        //  alertify.alert('Il file è troppo grande!').show();
                            alertify.alert('ASI', 'Il file è troppo grande!').set('resizable', true).resizeTo('20%',200);
                            // program encountered a file whose size is greater than 'maxfilesize'
                            // FileTooLarge also has access to the file which was too large
                            // use file.name to reference the filename of the culprit file
                            break;
                        case 'FileTypeNotAllowed':
                            // The file type is not in the specified list 'allowedfiletypes'
                            alertify.alert('ASI', 'Tipo di file non supportato!').set('resizable', true).resizeTo('20%', 200);
                            break;
                        case 'FileExtensionNotAllowed':
                            alertify.alert('ASI', 'Tipo di file non supportato!').set('resizable', true).resizeTo('20%', 200);

                            // The file extension is not in the specified list 'allowedfileextensions'
                            break;
                        default:
                            break;
                    }

                },

                uploadStarted: function (i, file, len) {
               createImage(file);
                },
                progressUpdated: function (i, file, progress) {
                  $.data(file).find('.progress').width(progress);
                },


                afterAll: function (e) {
                    //To do some task after all uploads done.
                }
              
            })

            var template = '<div class="preview">' +
                '<span class="imageHolder">' +
                '<img />' +
                '<span class="uploaded"></span>' +
                '</span>' +
                '<div class="progressHolder">' +
                '<div class="progress"></div>' +
                '</div>' +
                '</div>';

            //var template = '<div class="preview">' +
            //    '<span class="imageHolder">' +
            //    '<img />' +
            //    '<span class="uploaded"></span>' +
            //    '</span>'; 
            //    '<div class="progressHolder">' +
            //    '<div class="progress"></div>' +
            //    '</div>' +
            //    '</div>';

            function createImage(file) {

         

                var preview = $(template),
                    image = $('img', preview);

                var reader = new FileReader();

                image.width = 100;
                image.height = 100;
  
                reader.onload = function (e) {
                   
                    // e.target.result holds the DataURL which
                    // can be used as a source of the image:
                 //   alert(e.target.result);
                    image.attr('src', e.target.result);
                  
                };

                // Reading the file as a DataURL. When finished,
                // this will trigger the onload function above:
                reader.readAsDataURL(file);

                message.hide();
                preview.appendTo(dropSection);
//  alert("ciao");
                // Associating a preview container
                // with the file, using jQuery's $.data():

                $.data(file, preview);
            }

            function showMessage(msg) {
                message.html(msg);
            }
          
        })
    </script>

  <%--     <script type="text/javascript" src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
 --%>   <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>


            


  </div>
            </main>

     <!-- Modal -->
<div class="modal fade" id="ModalUp" tabindex="-1" role="dialog" aria-labelledby="ModalUpTitle" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="ModalUpTitle">Istruzioni Caricamento Documenti</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
          Il sistema permette di caricare documenti in formato jpg, png e pdf.<br /><br />
          La modalità di caricamento avviene tramite il trascinamento (drag and drop) del documento nell'area rettangolare color nero centrale.<br /><br />
          Nel caso il browser utilizzato non fosse compatibile con questa funzione, si prega di utilizzare il metoto di caricamento denominato Upload
           browser non compatibili. <br /><br />
          Si possono caricare fino a 3 documenti nella stessa sessione. <br /><br />
          La dimensione massima accettata per file è: xxx mb<br /><br />

          Nel caso si fossero caricati documenti per errore, contattare info@xxx.it per essere abilitati a ripetere l'operazione.




        </div>
    
    </div>
  </div>
</div>

    </form>
        <footer class="footer mt-auto py-3">
  <div class="container">
    <span class="text-muted">ASI - Associazioni Sportive e Sociali Italiane</span>
       
  </div>
</footer>

    
    
     <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>

</body>
</html>
