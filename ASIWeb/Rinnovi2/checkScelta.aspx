<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageRinnovi2.Master" CodeBehind="checkScelta.aspx.vb" Inherits="ASIWeb.checkScelta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../css/alertify.min.css" />
    <link rel="stylesheet" href="../css/themes/default.min.css" />
    <script type="text/javascript" src="../Scripts/alertify.js"></script>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-kenU1KFdBIe4zVF0s0G1M5b4hcpxyD9F7jL+jjXkk+Q2h455rYXK/7HAuoJl+0I4" crossorigin="anonymous"></script>

    <style>
    .custom-file-input.selected:lang(en)::after {
      content: "" !important;
    }
    .custom-file {
      overflow: hidden;
    }
    .custom-file-input {
      white-space: nowrap;
    }
    .legacy{
    color:white;
}
      .avviso{
    color:darkred;
}
    a:hover {
 color:white;
    }
  .btn-tre {
    background-color:#fff;
    border-color: #ff5308;

}
    .btn-custom  {
            background-color:darkgray;
            color:white;
          /*  box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.075) inset, 0px 0px 8px rgba(0, 90, 124, 0.5);*/
         width:50%;
        }
    .errore { color:red;


    }
     .Progress
 {
  position: fixed;
  top: 50%;
  left: 50%;
  /* bring your own prefixes */
  transform: translate(-50%, -50%);
  background:red;
  border-color:brown;
  border-style:solid;
  border-width:thin;
   height: 100px;
	width: 200px;
   color:white;
   z-index:1;
	display: flex;
  justify-content: center;
  align-items: center;
 }
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="jumbotron jumbotron-fluid rounded">
        <div class="container">
            <h6 class="fs-5 text-white text-decoration-none">Scelta Controllo Tesseramento</h6>

            <p class="lead">
                <%--<asp:Literal ID="litDenominazioneJumbo" runat="server"></asp:Literal>--%>
                <a href="javascript:history.back()" class="btn btn-success btn-sm btn-due"><i class="bi bi-skip-backward-btn"></i>Torna alla pagina precedente</a>
            </p>

        </div>
    </div>
    <div class="col-sm-12">
        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <asp:Label ID="lblIntestazioneRinnovo" runat="server" Text=""></asp:Label>
                    <asp:HiddenField ID="HiddenIdRecord" runat="server" />
                    <asp:HiddenField ID="HiddenIDRinnovo" runat="server" />
                </div>
            </div>

        </div>
    </div>

    <div class="col-sm-12">
        <div class="mb-3">
            <span class="form-label small fw-bold mt-2">Codice Fiscale Italiano oppure Estero</span>
            <br />
            <br />
            <div class="form-check form-check-inline">
                <asp:RadioButton ID="rbIT" runat="server" Checked="true" CssClass="radio-inline" GroupName="visita" />
                <label class="form-check-label" for="rbIT">
                    Codice Fiscale Italiano
                </label>
            </div>

            <div class="form-check form-check-inline">
                <asp:RadioButton ID="rbEE" runat="server" CssClass="radio-inline" GroupName="visita" />

                <label class="form-check-label" for="rbEE">
                    Codice Fiscale Estero
                </label>
            </div>


        </div>
    </div>



    <div class="col-sm-12">
        <div class="row">
            <div class="col-sm-6">
                <div class="form-group">
                </div>
            </div>
            <div class="col-sm-6">
                <div class="form-group">
                </div>
            </div>



        </div>

    </div>
    <div class="col-sm-12">
        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <asp:LinkButton ID="lnkCheck" class="btn btn-primary" runat="server" OnClick="lnkCheck_Click"><i class="bi bi-check-square"> </i>Avanti</asp:LinkButton>
                    <%--     <asp:Button ID="btnCheck" runat="server" Text="Controlla" class="btn btn-primary"    />--%>
                </div>
            </div>

        </div>
    </div>



    <div class="col-sm-12">
        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <hr />

                </div>
            </div>

        </div>
    </div>
    <br />
    <br />
</asp:Content>
