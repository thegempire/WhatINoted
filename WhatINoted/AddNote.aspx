<%@ Page Title="Add Note" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddNote.aspx.cs" Inherits="WhatINoted.CreateEditNoteView" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="AddNoteUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div runat="server">
                <h2>Create New Note</h2>
                <div runat="server" class="button" onclick="ToggleElementHidden('ByImageGroupContainer');">
                    By Image
                </div>
                <div runat="server" class="ByImageGroupContainer group_container hidden">
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
                    <textarea class="full_width"></textarea>
                </div>
                <div runat="server" class="titled_field display_inline-block">
                    <h4>Tags</h4>
                    <input type="text"></input>
                </div>
                <div runat="server" class="titled_field display_inline-block">
                    <h4>Notebook</h4>
                    <input type="text"></input>
                </div>
                <div runat="server" class="grid_5_columns">
                    <div runat="server" class="grid_5_columns_right button" onclick="CreateNote_Click();">
                        Create Note
                    </div>
                </div>
            </div>
            
            <asp:HiddenField runat="server" ID="HandleLoginUserID" Value="" />
            <asp:Button runat="server" class="handleLoginTrigger hidden" OnClick="UpdateNotebooks" />

        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        window.addEventListener('load', handleLoginForContentPage);
        function CreateNote_Click() {
            $.ajax({
                type: "POST",
                url: "AddNote.aspx/CreateNote",
                data: "",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                error: function (jqXHR, textStatus, errorThrown) {
                    alert("Error - Status: " + textStatus + "\n" + "jqXHR Status: " + jqXHR.status + "\n" + "jqXHR Response Text: " + jqXHR.responseText) },
                success: function (msg) {
                    console.log(msg);
                    if (msg.d == true) {
                        //window.location.href = "Notes.aspx";
                    }
                    else {
                        //show error
                        alert('Note Creation Failed');
                        //let div = document.createElement('div');
                        //div.innerText = 'Testaddingcontent';
                        //document.getElementsByTagName('body')[0].appendChild(div);
                    }
                }
            });
        }
    </script>
</asp:Content>
