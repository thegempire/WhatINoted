<%@ Page Title="Notes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Notes.aspx.cs" Inherits="WhatINoted.NotesView" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="LoginUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div runat="server">
                <h2>Notebook Title</h2>
                <asp:Table runat="server" ID="NotesTable">
                        <!-- Notes go here -->
                </asp:Table>
            </div>
            <div runat="server" class="footer_1_column fixed">
                <div runat="server" class="footer_1_column_middle button" onclick="NewNote_Click();">
                    New Note
                </div>
            </div>
            <asp:HiddenField runat="server" ID="HandleLoginUserID" Value="" />
            <asp:Button runat="server" class="handleLoginTrigger hidden" OnClick="UpdateNotes" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        window.addEventListener('load', handleLoginForContentPage);
        function NewNote_Click() {
            window.location.href = "AddNote.aspx";
        }
    </script>
</asp:Content>