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
                    <asp:UpdatePanel runat="server" ID="ByISBNGroupPanel" UpdateMode="Conditional">
                        <ContentTemplate>
                        <img class="display_block" src="#" alt="Uploaded Image" />
                        <div runat="server" class="button small_button display_inline-block fix_inline">
                            Select Image
                        </div>
                        <div runat="server" class="button small_button display_inline-block fix_inline">
                            Extract Text
                        </div>
                        <br />
                        <div runat="server" class="titled_field display_inline-block">
                            <h4>ISBN</h4>
                            <asp:textBox runat="server" ID="IsbnEntry" TextChanged="ValidateIsbnField" class="full_width"></asp:textBox>
                            <asp:CustomValidator runat="server"
                                OnServerValidate="ValidateIsbnField"
                                ControlToValidate="IsbnEntry"
                                ErrorMessage="Not a valid ISBN"
                                CssClass="required">
                            </asp:CustomValidator>
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
                                    <asp:TableCell>Publisher</asp:TableCell>
                                    <asp:TableCell>Publication Date</asp:TableCell>
                                    <asp:TableCell>ISBN</asp:TableCell>
                                </asp:TableRow>
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
                    <asp:UpdatePanel runat="server" ID="ByDetailsGroupPanel" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="titled_field display_inline-block">
                                <h4>Title</h4>
                                <asp:textBox runat="server" id="TitleEntry" class="full_width"></asp:textBox>
                            </div>
                            <div class="titled_field display_inline-block">
                                <h4>Author</h4>
                                <asp:textBox runat="server" id="AuthorEntry" class="full_width"></asp:textBox>
                            </div>
                            <div class="titled_field display_inline-block">
                                <h4>Publisher</h4>
                                <asp:textBox runat="server" id="PublisherEntry" class="full_width"></asp:textBox>
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
                                        <asp:TableCell>Publisher</asp:TableCell>
                                        <asp:TableCell>Publication Date</asp:TableCell>
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div runat="server" class="grid_5_columns margin_top_bottom">
                    <asp:Button runat="server" ID="btnCreateNotebookPostback" Style="display: none" OnClick="CreateNotebook" />
                    <div class="grid_5_columns_right button" onclick="document.getElementById('<%= btnCreateNotebookPostback.ClientID %>').click()">
                        Create Notebook
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
