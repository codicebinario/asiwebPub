<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageAA.Master" CodeBehind="homeA.aspx.vb" Inherits="ASIWeb.homeA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/chart.js@3.7.0/dist/chart.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels@2.0.0/dist/chartjs-plugin-datalabels.min.js"></script>
    <style>

           .btn-custom  {
            background-color:darkgray;
            color:white;
          /*  box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.075) inset, 0px 0px 8px rgba(0, 90, 124, 0.5);*/
         width:50%;
       
       
}
  .AccessoKey{

    text-decoration:underline;
    color: darkred;
         }
  .bianco{
      background-color:white;
      width:80%;
    margin: auto;
  }
        .posCentre{width:300px;margin:0 auto;}  
    
    </style>
    
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdateProgress runat="server" ID="PageUpdateProgress">
        <ProgressTemplate>
            <div class="posCentre alert alert-danger mb-2" role="alert">
                      Sto caricando la pagina richiesta..
            </div>
         
        </ProgressTemplate>
    </asp:UpdateProgress>
<div class="container">
  
    <asp:HiddenField  ID="HiddenQuantiCorsiAttivi" runat="server" Value="0" />
    <asp:HiddenField  ID="HiddenQuanteEquiparazioniAttive" runat="server" Value="0" />
    <asp:HiddenField ID="HiddenQuantiRinnoviAttivi" runat="server" Value="0" />
  <div class="row">
     
    <div class="col">
        <div class="jumbotron jumbotron-fluid rounded">
  <div class="container text-center">
   
   
       <a class="btn btn-lg btn-custom" href="albo/dashboardB.aspx" accesskey="C" role="button"><i class="bi bi-body-text"> </i>Corsi</a>
  
</div> 
    </div>
      </div>
  <div class="col">
           <div class="jumbotron jumbotron-fluid rounded">
  <div class="container text-center">
    
    <a class="btn btn-lg btn-custom" href="Equiparazioni/DashboardEqui.aspx" role="button" accesskey="q"><i class="bi bi-code-slash"> </i>Equiparazioni</a>
    
  </div>
</div>

  </div>
 
 </div>     


         <div class="row">
     
    <div class="col">
        <div class="jumbotron jumbotron-fluid rounded">
  <div class="container text-center">
  
<%--  <a class="btn btn-lg btn-custom" href="Rinnovi/DashboardRinnovi.aspx" role="button" accesskey="R"><i class="bi bi-wrench-adjustable-circle"> </i>Rinnovi</a>
--%>
      <asp:LinkButton CssClass="btn btn-lg btn-custom" ID="LinkButton1" runat="server"><i class="bi bi-wrench-adjustable-circle"> </i>Rinnovi 2.0</asp:LinkButton>

  </div>
</div> 
    </div>

  <div class="col">
        
         <div class="jumbotron jumbotron-fluid rounded">
  <div class="container text-center">
  <a class="btn btn-lg btn-custom" href="GestioneAlbo/DashboardAlbo.aspx" accesskey="A" role="button"><i class="bi bi-journal-text"> </i>Consulta Albo</a>
  
 
    <%--   <a class="btn btn-lg btn-custom " href="#" role="button">Ristampa</a>--%>
  </div>
</div> 

  </div>
 
 </div>


    <div class="row">

        <div class="col">
            <div class="jumbotron jumbotron-fluid rounded">
                <div class="container text-center">

                    <%--  <a class="btn btn-lg btn-custom" href="Rinnovi/DashboardRinnovi.aspx" role="button" accesskey="R"><i class="bi bi-wrench-adjustable-circle"> </i>Rinnovi</a>
                    --%>
                    <asp:LinkButton CssClass="btn btn-lg btn-custom" ID="LinkButton2" runat="server"><i class="bi bi-wrench-adjustable-circle"> </i>Equiparazioni 2.0</asp:LinkButton>

                </div>
            </div>
        </div>

        <div class="col">

         

        </div>

    </div>







    <div class="row bianco">

        <div class="col-12 bianco">
            <div class="bianco">
                <div class="container text-center bianco">
                    <canvas id="myChart"></canvas>
     
                </div>
            </div>
        </div>
</div>
    
 </div>   <script>
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
                data: [<%= HiddenQuantiCorsiAttivi.Value %>, <%= HiddenQuanteEquiparazioniAttive.Value %>, <%= HiddenQuantiRinnoviAttivi.Value %>],
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
</asp:Content>
