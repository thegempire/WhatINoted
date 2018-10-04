<%@ Page Title="Add Notebook" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddNotebook.aspx.cs" Inherits="WhatINoted.AddNotebook" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="AddNotebookUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div runat="server">
                <h2>Create New Notebook</h2>
                <div runat="server" class="button" onclick="ByISBN_Click">
                    By ISBN
                </div>
                <div runat="server" class="group_container hidden_disabled">
                    <img class="display_block" src="#" alt="Uploaded Image" />
                    <div runat="server" class="button small_button display_inline-block fix_inline">
                        Select Image
                    </div>
                    <div runat="server" class="button small_button display_inline-block fix_inline">
                        Extract Text
                    </div>
                    <div runat="server" class="titled_field">
                        <h4>ISBN</h4>
                        <input type="text" class="full_width"></input>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
