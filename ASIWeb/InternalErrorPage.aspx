<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiErrorMasterPage.Master" CodeBehind="InternalErrorPage.aspx.vb" Inherits="ASIWeb.InternalErrorPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex align-items-center mt-5 justify-content-center ">
        <div class="text-center">
            <h1 class="display-1 fw-bold">Generic Error</h1>
            <p class="fs-3"><span class="text-danger">Opps!</span> Generic Error Happens</p>
            <p class="lead">
                Try again later
            </p>
            <a href="home.aspx" class="btn btn-primary">Go to Start</a>
        </div>
    </div>



</asp:Content>
