<%@ Page Title="What I Noted" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="WhatINoted.Main" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="MainUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <div runat="server" id="MainNotebooks" class="mainNotebookDiv">
                <!-- Notebook images(?) go in here -->
            </div>

            <div runat="server" class="footer_2_columns fixed">
                <div runat="server" class="footer_2_columns_left button">
                    New Notebook
                </div>
                <div runat="server" class="footer_2_columns_right button">
                    New Note
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
