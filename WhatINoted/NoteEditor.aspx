<%@ Page Title="Note" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NoteEditor.aspx.cs" Inherits="WhatINoted.CreateEditNoteView" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="AddNoteUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div runat="server">
                <h2 runat="server" id="PageTitle">Create New Note</h2>
                <div runat="server" id="ByImage">
                    <img id="Image" class="display_block hidden" src="#" alt="Uploaded Image" />
                    <br />
                    <input type="file" id="ImageInput" class="small_button display_inline-block" name="ImageInput" accept="image/png, image/jpeg" />
                    <asp:UpdatePanel ID="ExtractTextUpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:HiddenField runat="server" ID="ImageInBase64" Value="" />
                            <asp:Button runat="server" ID="btnExtractText" Style="display: none" OnClick="GenerateText" />
                            <div runat="server" class="button small_button display_inline-block fix_inline" onclick="click_extractText()">
                                Extract Text
                            </div>
                            <div runat="server" class="titled_field">
                                <h4>Note Text</h4>
                                <asp:TextBox runat="server" ID="NoteText" CssClass="full_width" TextMode="MultiLine" Columns="50" Rows="5"></asp:TextBox>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="DropdownUpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div runat="server" class="titled_field display_inline-block">
                                <h4>Notebook</h4>
                                <asp:DropDownList runat="server" ID="NotebookList"></asp:DropDownList>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div runat="server" class="grid_5_columns">
                        <asp:Button runat="server" ID="HandleNoteTrigger" class="hidden" OnClick="HandleNote" />
                        <div runat="server" id="HandleNoteButton" class="grid_5_columns_right button" onclick="Button_Click();">
                            Create Note
                        </div>
                    </div>
                </div>
            </div>

            <asp:UpdatePanel runat="server" ID="HiddenUpdatePanel" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:HiddenField runat="server" ID="HandleLoginUserID" Value="" />
                    <asp:Button runat="server" class="handleLoginTrigger hidden" OnClick="UpdatePage" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/fabric.js/1.4.13/fabric.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/darkroomjs/2.0.1/darkroom.js"></script>
    <link type="text/css" rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/darkroomjs/2.0.1/darkroom.css" />

    <script>
        window.addEventListener('load', handleLoginForContentPage);
        function Button_Click() {
            var noteText = $('#<%= NoteText.ClientID %>').val();

            if (noteText === null || noteText === "") {
                alert("note text cannot be null");
                return;
            }

            document.getElementById('<%= HandleNoteTrigger.ClientID %>').click();
        }

        var file;
        var upload = document.getElementById("ImageInput");
        var darkroom;

        upload.onchange = function () {
            if (upload.files[0] != null) {
                console.log("file 0 not null");
                file = upload.files[0];
                getImageIn64(file);
            }
        }

        function getImageIn64(file) {
            var reader = new FileReader();
            reader.onload = function () {
                console.log("reader loaded");
                let newImage = createImageElement();
                newImage.src = reader.result;
                document.getElementById('<%= ImageInBase64.ClientID %>').value = reader.result;
                var ByImageContainer = document.getElementById('<%= ByImage.ClientID %>');
                if (darkroom != null) {
                    ByImageContainer.removeChild(ByImageContainer.firstElementChild);
                }
                ByImageContainer.insertBefore(newImage, ByImageContainer.firstElementChild);
                darkroom = new Darkroom('#Image', {
                    plugins: {
                        save: false
                    }
                });
            }.bind(this, darkroom);
            reader.onerror = function (error) {
                console.log('Error: ', error);
            };
            reader.readAsDataURL(file);
        }

        function createImageElement() {
            let newImage = document.createElement('img');
            newImage.id = "Image";
            newImage.className = "display_block";
            newImage.alt = "Uploaded Image";
            return newImage;
        }

        function click_extractText() {
            document.getElementById('MainContent_ImageInBase64').value = darkroom.sourceCanvas.toDataURL();
            var val = document.getElementById('<%= ImageInBase64.ClientID %>').value;
            if (val != null && val != '')
                document.getElementById('<%= btnExtractText.ClientID %>').click();
        }
    </script>
</asp:Content>
