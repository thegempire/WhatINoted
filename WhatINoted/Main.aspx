﻿<%@ Page Title="What I Noted" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="WhatINoted.NotebooksView" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="MainUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <asp:Button runat="server" ID="btnOpenNotebookPostback" Style="display: none" OnClick="OpenNotebook" />
            <div runat="server" id="MainNotebooks" class="mainNotebookDiv">
                <!-- Notebook images(?) go in here -->
            </div>

            <div runat="server" class="footer_2_columns fixed">
                <asp:Button runat="server" ID="btnCreateNotebookPostback" Style="display: none" OnClick="CreateNotebook" />
                <div class="footer_2_columns_left button" onclick="document.getElementById('<%= btnCreateNotebookPostback.ClientID %>').click()">
                    New Notebook
                </div>
                <asp:Button runat="server" ID="btnCreateNotePostback" Style="display: none" OnClick="CreateNote" />
                <div class="footer_2_columns_right button" onclick="document.getElementById('<%= btnCreateNotePostback.ClientID %>').click()">
                    New Note
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        window.addEventListener('load', handleLoginForContentPage());
        function click_openNotebook() {
            document.getElementById('<%= btnOpenNotebookPostback.ClientID %>').click();
        }
    </script>
</asp:Content>
