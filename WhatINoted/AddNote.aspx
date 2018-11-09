<%@ Page Title="Note" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddNote.aspx.cs" Inherits="WhatINoted.CreateEditNoteView" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="AddNoteUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div runat="server">
                <h2 runat="server" id="PageTitle">Create New Note</h2>
                <div runat="server" id="ByImage1" class="button" onclick="ToggleElementHidden('ByImageGroupContainer');">
                    By Image
                </div>
                <div runat="server" id="ByImage2" class="ByImageGroupContainer group_container hidden">
                    <img class="display_block" src="#" alt="Uploaded Image" />
                    <div runat="server" class="button small_button display_inline-block fix_inline">
                        Select Image
                    </div>
                    <div runat="server" class="button small_button display_inline-block fix_inline">
                        Extract Text
                    </div>
                </div>
                <div runat="server" class="titled_field">
                    <h4>Note Text</h4>
                    <textarea runat="server" class="full_width" id="NoteText"></textarea>
                </div>
                <div runat="server" class="titled_field display_inline-block">
                    <h4>Notebook</h4>
                    <asp:DropDownList runat="server" ID="NotebookList">
                    </asp:DropDownList>
                </div>
                <div runat="server" class="grid_5_columns">
                    <div runat="server" id="HandleNoteButton" class="grid_5_columns_right button" onclick="Button_Click();">
                        Create Note
                    </div>
                </div>
            </div>
            <asp:HiddenField runat="server" ID="HandleLoginUserID" Value="" />
            <asp:Button runat="server" class="handleLoginTrigger hidden" OnClick="UpdatePage" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        window.addEventListener('load', handleLoginForContentPage);
        function Button_Click() {
            var userID = $('#<%= HandleLoginUserID.ClientID %>').val();
            var notebookID = $('#<%= NotebookList.ClientID %>').val();
            var noteText = $('#<%= NoteText.ClientID %>').val();

            if (noteText === null || noteText === "") {
                alert("note text cannot be null");
                return;
            }

            $.ajax({
                type: "POST",
                url: "AddNote.aspx/HandleNote",
                data: JSON.stringify({ userID: userID, noteText: noteText }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                error: function (jqXHR, textStatus, errorThrown) {
                    alert("Error - Status: " + textStatus + "\n" + "jqXHR Status: " + jqXHR.status + "\n" + "jqXHR Response Text: " + jqXHR.responseText)
                },
                success: function () {
                    window.location.href = "Notes.aspx?notebookID=" + notebookID;
                }
            });
        }
    </script>
</asp:Content>
