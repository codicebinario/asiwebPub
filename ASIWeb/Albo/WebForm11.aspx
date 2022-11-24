<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm11.aspx.vb" Inherits="ASIWeb.WebForm11" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <!-- Bootstrap CSS -->
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Css/customAlbo.css" rel="stylesheet" />
    <!-- font awesome -->
    <link rel="stylesheet" href="font-awesome-4.7.0/css/font-awesome.min.css" />

    <!-- google fonts -->
    <link href="https://fonts.googleapis.com/css?family=Roboto" rel="stylesheet" />
    <link href="Css/sticky-footer-navbar.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.9.1/font/bootstrap-icons.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.2/jquery-confirm.min.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.2/jquery-confirm.min.js"></script>

    <script src="Scripts/popper-utils.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chart.js@3.7.0/dist/chart.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels@2.0.0/dist/chartjs-plugin-datalabels.min.js"></script>
    <style>
        .dimen{
            width: 20%;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">   
         
            <div class="dimen">
            <canvas id="myChart"></canvas>
        </div>
    </form>
    <script>
    Chart.register(ChartDataLabels);
        var barColors = ['rgba(255, 99, 132, 0.2)',
            'rgba(75, 192, 192, 0.2)', 'rgba(201, 203, 207, 0.2)'];
    const ctx = document.getElementById('myChart');
    const myChart = new Chart(ctx, {
        type: 'bar',
        data: {
          
            labels: ['Corsi', 'Equip.', 'Rinnovi'],
            datasets: [{
             /*   labels: null,*/
              /*  label:null,*/
        backgroundColor: barColors,
                data: [12, 19, 3],
                borderColor: [
                    'rgb(255, 99, 132)',
                    'rgb(255, 159, 64)',
                    'rgb(255, 205, 86)'

                ], borderWidth: 1
            }]
        },
        options: {
            legend: { display: false },
            
        maintainAspectRatio: false,
        responsive: true,
            plugins: {
                title: {
                    display: true,
                    text: "In Stato Attivo"
                },
                legend: {
                    display: false
                },
            datalabels: { // This code is used to display data values
                anchor: 'end',
                align: 'center',
                formatter: Math.round,
                font: {
                    weight: 'bold',
                    size: 16
                }
            }
        }
        }
    });
    </script>
</body>
</html>
