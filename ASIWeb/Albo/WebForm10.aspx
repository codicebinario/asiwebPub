<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm10.aspx.vb" Inherits="ASIWeb.WebForm10" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="https://cdn.jsdelivr.net/npm/chart.js@3.7.0/dist/chart.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels@2.0.0/dist/chartjs-plugin-datalabels.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="idQuantiCorsiAttivi" runat="server" Value="3" />
       
    </form>
<%--    <script>
var xValues = ["Corsi", "Equip.", "Rinnovi"];
        var yValues = ["<%= idQuantiCorsiAttivi.Value %>", 10, 4];
        var barColors = ['rgba(255, 99, 132, 0.2)',
            'rgba(75, 192, 192, 0.2)', 'rgba(201, 203, 207, 0.2)'];

new Chart("myChart", {  type: "bar",
  data: {
    labels: xValues,
      datasets: [{
        

      backgroundColor: barColors,
        data: yValues,
        borderColor: [
            'rgb(255, 99, 132)',
            'rgb(255, 159, 64)',
            'rgb(255, 205, 86)'
           
        ], borderWidth: 1
    }]
  },
  options: {
    legend: {display: false},
    title: {
      display: true,
      text: "In Stato Attivo"
      }

     
  }
  
 

});
    </script>--%>

    
</body>
</html>
