<%@ Page Title="Add Notebook" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddNotebook.aspx.cs" Inherits="WhatINoted.NotebookCreationView" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="AddNotebookUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div runat="server" class="margin_top_bottom">
                <h2>Create New Notebook</h2>
                <div runat="server" class="button" onclick="ToggleElementHidden('byISBNGroupContainer');">
                    By ISBN
                </div>
                <div runat="server" id="ByISBNGroupContainer" class="byISBNGroupContainer group_container hidden">
                    <img id="Image" class="display_block hidden" src="#" alt="Uploaded Image" />

                    <br />

                    <input type="file" id="ImageInput" class="small_button display_inline-block" name="ImageInput" accept="image/png, image/jpeg" />

                    <asp:UpdatePanel ID="ExtractTextUpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:HiddenField runat="server" ID="ImageInBase64" Value="" />
                            <asp:Button runat="server" ID="btnExtractText" Style="display: none" OnClick="GenerateText" />
                            <div runat="server" class="button small_button display_inline-block fix_inline" onclick="click_extractText()">
                                Extract ISBN
                            </div>

                            <br />

                            <div runat="server" class="titled_field display_inline-block">
                                <h4>ISBN</h4>
                                <input runat="server" type="text" id="IsbnBox" class="full_width" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <br />

                    <asp:UpdatePanel ID="SearchForNotebookPanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Button runat="server" ID="btnISBNPostback" Style="display: none" OnClick="SearchForBook" />
                            <div class="button small_button display_inline-block fix_inline" onclick="document.getElementById('<%= btnISBNPostback.ClientID %>').click()">
                                Search for Book
                            </div>
                            <div runat="server" class="search_grid">
                                <asp:Table runat="server" ID="SearchGridISBN">
                                    <asp:TableRow>
                                        <asp:TableCell>Title</asp:TableCell>
                                        <asp:TableCell>Author</asp:TableCell>
                                        <asp:TableCell>ISBN</asp:TableCell>
                                    </asp:TableRow>
                                    <%--insert dynamic search results here--%>
                                </asp:Table>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div runat="server" class="margin_top_bottom">
                <div runat="server" class="button" onclick="ToggleElementHidden('byBookDetailsGroupContainer');">
                    By Book Details
                </div>
                <div runat="server" id="ByBookDetailsGroupContainer" class="group_container byBookDetailsGroupContainer hidden">
                    <div runat="server" class="titled_field display_inline-block">
                        <h4>Title</h4>
                        <input type="text" class="full_width"></input>
                    </div>
                    <div runat="server" class="titled_field display_inline-block">
                        <h4>Author</h4>
                        <input type="text" class="full_width"></input>
                    </div>
                    <div runat="server" class="titled_field display_inline-block">
                        <h4>Published Year</h4>
                        <input type="text" class="full_width"></input>
                    </div>
                    <div runat="server" class="titled_field display_inline-block">
                        <h4>Printed Year</h4>
                        <input type="text" class="full_width"></input>
                    </div>
                    <br />
                    <asp:Button runat="server" ID="btnBookDetailsPostback" Style="display: none" OnClick="SearchForBook" />
                    <div class="button small_button display_inline-block fix_inline" onclick="document.getElementById('<%= btnBookDetailsPostback.ClientID %>').click()">
                        Search for Book
                    </div>
                    <div runat="server" class="search_grid">
                        <asp:Table runat="server" ID="SearchGridDetails">
                            <asp:TableRow>
                                <asp:TableCell>Title</asp:TableCell>
                                <asp:TableCell>Author</asp:TableCell>
                                <asp:TableCell>ISBN</asp:TableCell>
                            </asp:TableRow>
                            <%--insert dynamic search results here--%>
                            <asp:TableRow CssClass="custom_book search_result">
                                <asp:TableCell>Custom Book</asp:TableCell>
                                <asp:TableCell></asp:TableCell>
                                <asp:TableCell></asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </div>
                </div>
                <div runat="server" class="grid_5_columns margin_top_bottom">
                    <asp:Button runat="server" ID="btnCreateNotebookPostback" Style="display: none" OnClick="CreateNotebook" />
                    <div class="grid_5_columns_right button" onclick="document.getElementById('<%= btnCreateNotebookPostback.ClientID %>').click()">
                        Create Notebook
                    </div>
                </div>
            </div>

            <script src="https://cdnjs.cloudflare.com/ajax/libs/fabric.js/1.4.13/fabric.min.js"></script>
            <script src="https://cdnjs.cloudflare.com/ajax/libs/darkroomjs/2.0.1/darkroom.js"></script>
            <link type="text/css" rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/darkroomjs/2.0.1/darkroom.css" />

            <script>
                var path;
                var file;
                var upload = document.getElementById("ImageInput");
                var darkroom;

                upload.onchange = function () {
                    if (upload.files[0] != null) {
                        file = upload.files[0];
                        getImageIn64(file);
                    }
                }

                function getImageIn64(file) {
                    var reader = new FileReader();
                    reader.onload = function () {
                        let newImage = createImageElement();
                        newImage.src = reader.result;
                        document.getElementById('<%= ImageInBase64.ClientID %>').value = reader.result;
                        var ISBNGroupContainer = document.getElementsByClassName("byISBNGroupContainer")[0];
                        if (darkroom != null) {
                            ISBNGroupContainer.removeChild(ISBNGroupContainer.firstElementChild);
                        }
                        ISBNGroupContainer.insertBefore(newImage, ISBNGroupContainer.firstElementChild);
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
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>