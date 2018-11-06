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
                   <%-- <div class="image_field display_inline-block">--%>
                        <img id="Image" class="display_block hidden image_upload" src="#" alt="Uploaded Image" />
                    <%--</div>--%>
                    <input type="file" id="ImageInput" class="small_button display_inline-block" name="ImageInput" accept="image/png, image/jpeg" />
                    <asp:HiddenField runat="server" ID="ImageInBase64" Value="" />
                    <asp:Button runat="server" ID="btnExtractText" Style="display: none" OnClick="GenerateText" />
                    <div runat="server" class="button small_button display_inline-block fix_inline" onclick="click_openNotebook()">
                        Extract Text
                    </div>

                    <br />

                    <div runat="server" class="titled_field display_inline-block">
                        <h4>ISBN</h4>
                        <input type="text" id="IsbnBox" class="full_width"></input>
                    </div>
                    <br />
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

            <script>
                var file;
                var upload = document.getElementById("ImageInput");

                upload.onchange = function () {
                    file = upload.files[0];
                    getImageIn64(file);
                }

                function getImageIn64(file) {
                    var reader = new FileReader();
                    reader.readAsDataURL(file);
                    reader.onload = function () {
                        var image = document.getElementById("Image")
                        image.src = reader.result;
                        image.classList.remove("hidden");
                        document.getElementById("ImageInBase64").value = reader.result;
                    };
                    reader.onerror = function (error) {
                        console.log('Error: ', error);
                    };

                }

                function click_openNotebook() {
                    document.getElementById('<%= btnExtractText.ClientID %>').click();
                }

            </script>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
