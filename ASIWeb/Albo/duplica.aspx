<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AsiMasterPageAlbo.Master" CodeBehind="duplica.aspx.vb" Inherits="ASIWeb.duplica" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <div class="jumbotron jumbotron-fluid rounded">
  <div class="container">
    <h3 class="display-5">Processo Duplicazione Corso</h3>
    <p class="lead">
  <%--<asp:Literal ID="litDenominazioneJumbo" runat="server"></asp:Literal>--%>
         <a href="javascript:history.back()" class="btn btn-success btn-sm btn-due">Torna alla pagina precedente</a>      
    </p>
  </div></div>
      <div class="col-sm-12">
						<div class="row">
							<div class="col-sm-12">
								<div class="form-group">
                                    <asp:Label ID="lblIntestazioneCorso" runat="server" Text=""></asp:Label>
                                    <asp:HiddenField ID="HiddenIdRecord" runat="server" />
                                      <asp:HiddenField ID="HiddenIDCorso" runat="server" />
						</div>
							</div>
							
				</div></div>	
       <div class="col-sm-12">
          <div class="row">
              <div class="col-sm-12">
                  <div class="form-group">
                       <h5>Completa i dati mancanti: <asp:Label ID="lblnomef" runat="server" Text=""></asp:Label></h5>
                      <hr />
                  </div>
              </div>

          </div>
      </div>
    <br /><br />


    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
        <Triggers>

            <%--   <asp:AsyncPostBackTrigger ControlID="ddlProvinciaResidenza" />--%>
            <%--	  	 <asp:AsyncPostBackTrigger  ControlID="ddlProvinciaConsegna"/>--%>
            <%--  <asp:PostBackTrigger  ControlID="ddlProvincia"  />--%>
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="pnlDataEmissione" runat="server">
                <div class="col-sm-12">
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="txtNome">Data Emissione - [dd-MM-yyyy] &nbsp;</label>
                                <asp:TextBox ID="txtDataEmissione" CssClass="form-control" runat="server" MaxLength="250"></asp:TextBox>



                                <obout:Calendar ID="Calendar3" runat="server"
                                    TextBoxId="txtDataEmissione" CultureName="it-IT" DatePickerImagePath="../img/icon2.gif"
                                    DatePickerMode="True" MonthWidth="200" MonthHeight="140"
                                    Visible="true" StyleFolder="../calendar/styles/default">
                                </obout:Calendar>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2"
                                    MaskType="Date"
                                    runat="server"
                                    CultureName="it-IT"
                                    Mask="99/99/9999"
                                    MessageValidatorTip="true"
                                    UserDateFormat="DayMonthYear"
                                    OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError"
                                    ErrorTooltipEnabled="True"
                                    TargetControlID="txtDataEmissione" />

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
                        <div class="col-sm-6">
                            <div class="form-group">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDataEmissione" ErrorMessage="Data Emissione" Display="Dynamic" CssClass="errore" EnableClientScript="true"></asp:RequiredFieldValidator>


                                <asp:CustomValidator ID="CustomValidator1" ControlToValidate="txtDataEmissione" Enabled="true" Display="Dynamic" runat="server" ErrorMessage="Inserire una data dell'anno corrente" CssClass="errore"></asp:CustomValidator>



                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                            </div>

                        </div>
                    </div>
                </div>
            </asp:Panel>


            <div class="col-sm-12">
                <div class="row">
                    <div class="col-sm-6">
                        <div class="form-group">
                            <label for="txtNome">Data Inizio - [dd-MM-yyyy] &nbsp;[*] </label>
                            <asp:TextBox ID="txtDataInizio" CssClass="form-control" runat="server" MaxLength="250"></asp:TextBox>



                            <obout:Calendar ID="Calendar1" runat="server"
                                TextBoxId="txtDataInizio" CultureName="it-IT" DatePickerImagePath="../img/icon2.gif"
                                DatePickerMode="True" MonthWidth="200" MonthHeight="140"
                                Visible="true" StyleFolder="../calendar/styles/default">
                            </obout:Calendar>





                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4"
                                MaskType="Date"
                                runat="server"
                                CultureName="it-IT"
                                Mask="99/99/9999"
                                MessageValidatorTip="true"
                                UserDateFormat="DayMonthYear"
                                OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError"
                                ErrorTooltipEnabled="True"
                                TargetControlID="txtDataInizio" />

                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group">
                            <label for="txtCognome">Data Fine - [dd-MM-yyyy] [*]</label>
                            <asp:TextBox ID="txtDataFine" runat="server" CssClass="form-control" MaxLength="250"></asp:TextBox>
                            <obout:Calendar ID="Calendar2" runat="server" BeginDateCalendarId="Calendar1"
                                TextBoxId="txtDataFine" CultureName="it-IT" DatePickerImagePath="../img/icon2.gif"
                                DatePickerMode="True" MonthWidth="200" MonthHeight="140"
                                Visible="true" StyleFolder="../calendar/styles/default">
                            </obout:Calendar>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1"
                                MaskType="Date"
                                runat="server"
                                CultureName="it-IT"
                                Mask="99/99/9999"
                                MessageValidatorTip="true"
                                UserDateFormat="DayMonthYear"
                                OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError"
                                ErrorTooltipEnabled="True"
                                TargetControlID="txtDataFine" />

                        </div>
                    </div>



                </div>

            </div>

            <div class="col-sm-12">
                <div class="row">
                    <div class="col-sm-6">
                        <div class="form-group">
                            <asp:RequiredFieldValidator ID="rqDataInizio" runat="server" ControlToValidate="txtDataInizio" ErrorMessage="Data Inizio" Display="Dynamic" CssClass="errore" EnableClientScript="true"></asp:RequiredFieldValidator>



                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group">

                            <asp:RequiredFieldValidator ID="rqDataFine" runat="server" ControlToValidate="txtDataFine" ErrorMessage="Data Fine" Display="Dynamic" CssClass="errore" EnableClientScript="true"></asp:RequiredFieldValidator>





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
                                   
						            <asp:Button ID="btnFase3" runat="server" Text="Duplica" class="btn btn-primary"    />
                                   
						</div>
							</div>
							
				</div></div>	

</asp:Content>
