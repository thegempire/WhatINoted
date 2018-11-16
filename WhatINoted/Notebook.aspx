<%@ Page Title="Notes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Notebook.aspx.cs" Inherits="WhatINoted.NotesView" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="LoginUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div runat="server">
                <h2 runat="server" id="NotebookTitle">Notebook Title</h2>
                <asp:Button runat="server" class="deleteNotebookTrigger hidden" OnClick="DeleteNotebook" />
                <div runat="server" class="button small_button" onclick="DeleteNotebook_Click();">
                    Delete Notebook
                </div>
                <asp:Table runat="server" ID="NotesTable"></asp:Table>
                <asp:HiddenField runat="server" ID="NoteID" Value="" />
                <asp:Button runat="server" class="editNoteTrigger hidden" OnClick="EditNote" />
                <asp:Button runat="server" class="deleteNoteTrigger hidden" OnClick="DeleteNote" />
            </div>
            <div runat="server" class="footer_1_column fixed">
                <asp:Button runat="server" class="addNoteTrigger hidden" OnClick="AddNote" />
                <div runat="server" class="footer_1_column_middle button" onclick="NewNote_Click();">
                    New Note
                </div>
            </div>

            <asp:HiddenField runat="server" ID="HandleLoginUserID" Value="" />
            <asp:Button runat="server" class="handleLoginTrigger hidden" />

        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        window.addEventListener('load', handleLoginForContentPage);
        function NewNote_Click() {
            let triggerButton = document.getElementsByClassName('addNoteTrigger')[0];
            triggerButton.click();
        }
        function EditNote_Click(noteID) {
            window.location.href = "NoteEditor.aspx?noteID=" + noteID;
        }
        function DeleteNote_Click(noteID) {
            if (confirm("Are you sure you want to delete this Note?")) {
                let hiddenField = document.getElementById('<%= NoteID.ClientID %>');
                hiddenField.value = noteID;
                let triggerButton = document.getElementsByClassName('deleteNoteTrigger')[0];
                triggerButton.click();
            }
            window.location.reload(true);
        }
        function DeleteNotebook_Click() {
            let title = document.getElementById('<%= NotebookTitle.ClientID %>').innerHTML;
            if (confirm("Are you sure you want to delete " + title + "?")) {
                let triggerButton = document.getElementsByClassName('deleteNotebookTrigger')[0];
                triggerButton.click();
            }
        }
    </script>
</asp:Content>
