<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm18.aspx.vb" Inherits="ASIWeb.WebForm18" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="styles.css" rel="stylesheet" />
</head>
<body>
            <main class="app">
                <div class="header">
                    <h2>Upload</h2>
                    <div class="server-message">
                    </div>
                </div>
                <div class="input-div">
                    <p>Drag or <span class="browse">Browse</span></p>
                    <input type="file" class="file" multiple="multiple" accept="image/png,image/jpeg, image/jpg" />
                </div>
            </main>
         <form id="savedForm">
       
            <div class="header">
                <h3>Saved In Server</h3>
                <button type="submit">Delete</button>
            </div>
            <div class="saved-div"></div>

        </form>

       
  <form id="queuedForm">
      <div class="header">
          <h3>Queued in FrontEnd</h3>
          <button type="submit">Upload</button>
      </div>

      <div class="queued-div"></div>
  </form>

   
   
     <script>
         let queuedImagesArray = [],
             saveForm = document.querySelector("#savedForm"),
             queuedForm = document.querySelector("#queuedForm"),
             saveDiv = document.querySelector(".saved-div"),
             queuedDiv = document.querySelector(".queued-div"),
             inputDiv = document.querySelector(".input-div"),
             input = document.querySelector(".input-div input"),
             serverMessage = document.querySelector(".server-message"),
             delteImages = [];

             // saved in server


             //Queued in fronted
         input.addEventListener("change", () => {
             const files = input.files
            // console.log(files)
             for (let i = 0; i < files.length; i++) {
                 queuedImagesArray.push(files[i])
             }
             queuedForm.reset()
             displayQueuedImages()
         })

         inputDiv.addEventListener("drop", (e) => {
             e.preventDefault()
             const files = e.dataTransfer.files
            // console.log(files)
             for (let i = 0; i < files.length; i++) {
                 if (!files[i].type.match("image")) continue

                 if (queuedImagesArray.every(image => image.name !== files[i].name))
                 queuedImagesArray.push(files[i])
             }
             displayQueuedImages()

         })
         function displayQueuedImages() {
             let images = ""
             queuedImagesArray.forEach((image, index) => {
                 images += ` <div class="image">
                            <img src="${URL.createObjectURL(image)}" alt="image" />
                            <span onclick="deleteQueuedImage(${index})">&times;</span>
                            </div>`
             })
             queuedDiv.innerHTML = images
         }

         function deleteQueuedImage(index) {
             queuedImagesArray.splice(index, 1)
             displayQueuedImages()
         }
         queuedForm.addEventListener("submit", (e) => {
             e.preventDefault()
             sendQueuedImagesToServer()

         })
         function sendQueuedImagesToServer() {
             const formData = new FormData(queuedForm)
          
             queuedImagesArray.forEach((image, index) => {
                 formData.append(`file[${index}]`, image)
             })
             console.log(formData.values())
             fetch("upload", {
                 method: "Post",
                 body: formData
             })
                 .then(response => {
                     console.log(response.status)
                     if (response.status !== 200) throw Error(response.statusText)
                     location.reload()
                 })

                 .catch(error => {
                     serverMessage.innerHTML = error
                     serverMessage.style.cssText = "background-color: #f8d7da; color: #b71c1c"
                 })
         }
     </script>
</body>
</html>
