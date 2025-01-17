﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteApp.master" CodeBehind="WebFrmCustomer.aspx.cs" Inherits="BPWEBAccessControl.WebFrmCustomer" %>

<%@ MasterType VirtualPath="~/SiteApp.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="container-fluid frmbaseContainer02" id="dataUpdatePanel" runat="server">
               <div class="row frmbaseContainer02-header">
                   <div class="col-sm-12 text-right">
                       <asp:Label ID="lblfrmName" runat="server">frmName</asp:Label>
                   </div>
               </div>
                

               <asp:MultiView ID="mvwDataVw" ActiveViewIndex="2" runat="server">
                    <%--Search Start--%>
                    <asp:View ID="vw00" runat="server">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-sm-12 text-left">
                                    <asp:Label ID="lblSearchTitle" CssClass="ctrlStyle_Label font-size-xxl ashblue-forecolor" runat="server">Search</asp:Label>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="form-group col-sm-3">
                                    <asp:Label ID="lblSearchBy" CssClass="ctrlStyle_Label" runat="server" Text="Search By"></asp:Label>
                                    <asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="form-control form-group-sm"></asp:DropDownList>
                                </div>
                                <div class="form-group col-sm-3">
                                    <asp:Label ID="lblValue" CssClass="ctrlStyle_Label" runat="server" Text="Value"></asp:Label>
                                    <asp:TextBox ID="tbValue" runat="server" CssClass="form-control form-group-sm"></asp:TextBox>
                                </div>
                                <div class="form-group col-sm-3" style="margin-top: 20px">

                                    <asp:Button ID="btnSearch" CssClass="btn btn-default" runat="server" Text="Serach" OnClick="btnSearch_Click" />
                                    <asp:Button ID="btnCancelSearch" CssClass="btn btn-default" runat="server" Text="Cancel" OnClick="btnCancelSearch_Click" />
                                </div>
                                <div class="form-group col-sm-3">
                                    <asp:Label ID="lblViewState" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblViewName" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:Label ID="lblInfoDox" CssClass="ctrlStyle_Label normal bold" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:Panel ID="panSearch" runat="server" Height="400px" CssClass="table-responsive" ScrollBars="Auto">
                                        <asp:DataGrid ID="dgSearch" runat="server" CssClass="datagrid table table-striped table-bordered table-condensed" Font-Size="10pt" Font-Names="Calibri,Tahoma,Verdana,Arial" OnItemCommand="Grid_Command"
                                            CellPadding="4" GridLines="None" ForeColor="#333333">
                                            <AlternatingItemStyle BorderWidth="1px" BorderStyle="Groove" BorderColor="White"
                                                BackColor="White" ForeColor="#284775"></AlternatingItemStyle>
                                            <EditItemStyle BackColor="#999999" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BorderStyle="None" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                            <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <Columns>
                                                <asp:ButtonColumn Text="[Select]" CommandName="EditFile" ItemStyle-Font-Underline="False"
                                                    ItemStyle-ForeColor="blue" ButtonType="LinkButton">
                                                    <ItemStyle Font-Underline="False" ForeColor="Blue" />
                                                </asp:ButtonColumn>
                                            </Columns>
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        </asp:DataGrid>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                 <%--Search End--%>

                 <%--Msg Board--%>
                    <asp:View ID="vw01" runat="server">
                        <div class="container-fluid myMsgBoxStyleContainer">
                            <div class="myMsgBoxStyle">
                                <div class="myMsgBoxStyle-header">
                                    <table style="border: none;">
                                        <tr>
                                            <td style="width: 30%;">
                                                <asp:Image ID="dlgImage" runat="server" CssClass="img-responsive" AlternateText="" ImageAlign="AbsMiddle" ImageUrl="Picture/info.png" />
                                            </td>
                                            <td style="width: 70%; padding-left: 10px;">
                                                <asp:Label ID="lblMessageBoard" runat="server" CssClass="ctrlStyle_Label grey-forecolor font-size-xxl">Message</asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="myMsgBoxStyle-body">
                                    <br />
                                    <asp:Label ID="dlgMsg" CssClass="card-text" runat="server" Style="word-wrap: break-word;"></asp:Label>
                                    <br />
                                    <br />
                                    --<asp:Button ID="dlgOk" CssClass="btn btn-default" runat="server" Text="OK" OnClick="dlgOk_Click" />
                                    <asp:Button ID="dlgCancel" CssClass="btn btn-default" runat="server" Text="Cancel" OnClick="dlgCancel_Click" />--
                                    <br />
                                    <asp:Label ID="lblDlgState" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                 <%--Msg Board End--%>

                 <%--Data Entry--%>
                    <asp:View ID="vw02" runat="server">
                        <div class="container-fluid" style="margin-top: 10px;">

                            <div class="form-row">
                                <div class="container-fluid myFrame">
                                    <div class="col-sm-3 btn-group" role="group">
                                        <asp:Button ID="Button_AddNew" CssClass="btn btn-default" runat="server" OnClick="Button_AddNew_Click" Text="Add New" />
                                        <asp:Button ID="Button_Edit" CssClass="btn btn-default" runat="server" OnClick="Button_Edit_Click" Text="Edit"></asp:Button>
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3" style="text-align: right;">
                                        <asp:Button ID="Button_LogOff" runat="server" CssClass="btn btn-default" SkinID="btnRed" TabIndex="0" OnClick="Button_LogOff_Click" Text="Close"></asp:Button>
                                    </div>
                                </div>
                            </div>

                                <div class="form-row">
                                    <div class="container-fluid myFrame">

                                   <div class="form-group col-sm-3 required">
                                        <asp:Label ID="lblCusId" CssClass="ctrlStyle_Label" runat="server" Text="Customer Id"></asp:Label>
                                         <asp:TextBox ID="txtCusId" CssClass="form-control form-group-sm" runat="server"></asp:TextBox>
                                   </div>
                                   <div class="form-group col-sm-3 required">
                                        <asp:Label ID="lblCusName" CssClass="ctrlStyle_Label" runat="server" Text="Customer Name"></asp:Label>
                                         <asp:TextBox ID="txtCusName" CssClass="form-control form-group-sm" runat="server"></asp:TextBox>
                                   </div>
                                    <div class="form-group col-sm-3 required">
                                        <asp:Label ID="lblCusType" CssClass="ctrlStyle_Label" runat="server" Text="Customer Type"></asp:Label>
                                         <asp:DropDownList ID="ddlCusType" CssClass="form-control form-group-sm" runat="server"></asp:DropDownList>
                                   </div>
                                   <div class="form-group col-sm-3 required">
                                        <asp:Label ID="lblDOJ" CssClass="ctrlStyle_Label" runat="server" Text="Date of Join"></asp:Label>
                                        <asp:TextBox ID="txtDOJ" runat="server" CssClass="form-control form-group-sm" placeholder="dd-MMM-yyyy"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender runat="server" TargetControlID="txtDOJ" Format="dd-MMM-yyyy" CssClass="ajaxcald"></ajaxToolkit:CalendarExtender>
                                   </div>
                                   <div class="form-group col-sm-3 required">
                                        <asp:Label ID="lblPhone" CssClass="ctrlStyle_Label" runat="server" Text="Phone Number"></asp:Label>
                                         <asp:TextBox ID="txtPhone" CssClass="form-control form-group-sm" runat="server"></asp:TextBox>
                                   </div>
                               </div>
                            </div>
                            
                            </div>

                            <div class="form-row">
                                <div class="container-fluid myFrame">
                                    <div class="col-sm-6 red-forecolor text-left">
                                        All the red (*) marked fields are mandatory
                                    </div>
                                    <div class="col-sm-6" style="text-align: right;">
                                        <div class="btn-group" role="group" aria-label="Basic example">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default" OnClick="Button_save_Click"></asp:Button>
                                            <asp:Button ID="Button_Delete" runat="server" Text="Delete" CssClass="btn btn-default" OnClick="Button_Delete_Click"></asp:Button>
                                            <asp:Button ID="Button_Cancel" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="Button_Cancel_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                       
                    </asp:View>
                    <%--Data Entry End--%>
                </asp:MultiView>

                <div class="row" style="margin-top: 5px;">
                    <div class="form-group col-sm-12">
                        <asp:TextBox ID="TxtMsgBox" CssClass="form-control form-group-sm ctrlStyle_FullLength red-forecolor" runat="server" TextMode="MultiLine">Common Test</asp:TextBox>
                    </div>
                </div>

            </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Content>