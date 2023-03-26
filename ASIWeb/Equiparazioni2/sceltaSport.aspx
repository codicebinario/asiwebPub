<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageEqui2.Master" CodeBehind="sceltaSport.aspx.vb" Inherits="ASIWeb.sceltaSport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../css/alertify.min.css" />
    <link rel="stylesheet" href="../css/themes/default.min.css" />
    <script type="text/javascript" src="../Scripts/alertify.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-kenU1KFdBIe4zVF0s0G1M5b4hcpxyD9F7jL+jjXkk+Q2h455rYXK/7HAuoJl+0I4" crossorigin="anonymous"></script>

    <style>

        .errore { color:red;


    }
    </style>
    <script type="text/javascript">
  function DisableButton() {
      document.getElementById("<%=lnkButton1.ClientID %>").disabled = true;
  }
  window.onbeforeunload = DisableButton;
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
    <div class="jumbotron jumbotron-fluid rounded">
        <div class="container">
            <h6 class="fs-5 text-white text-decoration-none">Dati Sport</h6>
            <p class="lead">
                <%--<asp:Literal ID="litDenominazioneJumbo" runat="server"></asp:Literal>--%>
                <a href="javascript:history.back()" class="btn btn-success btn-sm btn-due"><i class="bi bi-skip-backward-btn"></i>Torna alla pagina precedente</a>
            </p>

        </div>
    </div>




    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlSport" />
            <asp:AsyncPostBackTrigger ControlID="ddlDisciplina" />
        </Triggers>
        <ContentTemplate>
            
   
            <div class="col-sm-12">



                <div class="row">
                    <div class="col-sm-4">
                        <div class="form-group">
                            <label for="ddlSport">Sport [*] </label>
                            <asp:DropDownList ID="ddlSport" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>

                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="form-group">
                            <label for="ddlDisciplina">Disciplina [*]</label>
                            <asp:DropDownList ID="ddlDisciplina" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>


                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="form-group">
                            <label for="txtCognome">Specialità <span>[Inserire un valore oppure ND]</span></label>
                            <asp:DropDownList ID="ddlSpecialita" runat="server" CssClass="form-control input-sm"></asp:DropDownList>


                        </div>
                    </div>



                </div>

            </div>

            <div class="col-sm-12">
                <div class="row">
                    <div class="col-sm-4">
                        <div class="form-group">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSport" ErrorMessage="Sport" InitialValue="##" Display="Dynamic" CssClass="errore" EnableClientScript="true"></asp:RequiredFieldValidator>



                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="form-group">

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlDisciplina" ErrorMessage="Disciplina" InitialValue="##" Display="Dynamic" CssClass="errore" EnableClientScript="true"></asp:RequiredFieldValidator>




                        </div>

                    </div>

                    <div class="col-sm-4">
                        <div class="form-group">

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSpecialita" ErrorMessage="Specialità" InitialValue="##" Display="Dynamic" CssClass="errore" EnableClientScript="true"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidator1" Enabled="false" Display="Dynamic" runat="server" ErrorMessage="Inserire un valore oppure ND" CssClass="errore"></asp:CustomValidator>



                        </div>

                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="col-sm-12">
        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">

                    <%--						            <asp:Button ID="btnFase4" runat="server" Text="Concludi" class="btn btn-primary"    />--%>
                    <asp:LinkButton ID="lnkButton1" class="btn btn-primary" Visible="true" runat="server"><i class="bi bi-browser-chrome"> </i>Nuova Richiesta</asp:LinkButton>

                </div>
            </div>

        </div>
    </div>



</asp:Content>
