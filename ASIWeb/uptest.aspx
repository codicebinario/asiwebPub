<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="uptest.aspx.vb" Inherits="ASIWeb.uptest" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
 
    <!-- include the style -->
<link rel="stylesheet" href="css/alertify.min.css" />
<!-- include a theme -->
<%--<link rel="stylesheet" href="css/themes/default.min.css" />--%>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" />
<link rel="stylesheet" href="css/themes/bootstrap.css" />
   
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        
        #dropSection
        {
            height: 300px;
            width: 600px;
            background-color: skyblue;
        }
        
        #btnUpload
        {
            display: none;
        }
        
        .active
        {
            background-color: yellow !important;
        }
    </style>
</head>
<body>
    <div id="dropSection">
    </div>
    <br />
    Uploaded Files:
    <hr />
    <div id="uploadedFiles">
    </div>
    <input type="button" id="btnUpload" value="Upload" />

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
            $("#dropSection").filedrop({
                fallback_id: 'btnUpload',
                fallback_dropzoneClick: true,
                url: '<%="Models/HandlerVB.ashx"%>',

              

                //allowedfiletypes: ['image/jpeg', 'image/png', 'image/gif', 'application/pdf', 'application/doc'],
                allowedfileextensions: ['.pdf', '.doc', '.pdf', '.jpg', '.jpeg', '.png', '.gif'],
                paramname: 'fileData',
                maxfiles: 2, //Maximum Number of Files allowed at a time.
                maxfilesize: 5, //Maximum File Size in MB.
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
                    $('#uploadedFiles').append(file.name + '<br />')
                },
                error: function (err, file) {
                 
                    switch (err) {
                        case 'BrowserNotSupported':
                            alertify.alert('hello').show();
                            alert('browser does not support HTML5 drag and drop')
                            break;
                        case 'TooManyFiles':
                            // user uploaded more than 'maxfiles'
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
                            break;
                        case 'FileExtensionNotAllowed':
                            // The file extension is not in the specified list 'allowedfileextensions'
                            break;
                        default:
                            break;
                    }

                },
                afterAll: function (e) {
                    //To do some task after all uploads done.
                }
              
            })


          
        })
    </script>

  <%--     <script type="text/javascript" src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
 --%>   <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>

 

</body>
</html>

