<%@ Page Title="Add Notebook" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddNotebook.aspx.cs" Inherits="WhatINoted.AddNotebook" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="AddNotebookUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div runat="server" class="margin_top_bottom">
                <h2>Create New Notebook</h2>
                <div runat="server" class="button" onclick="ToggleElementHidden('byISBNGroupContainer');">
                    By ISBN
                </div>
                <div runat="server" class="byISBNGroupContainer group_container hidden">
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
                        <input type="text" class="full_width"></input>
                    </div>
                    <br />
                    <div class="button small_button display_inline-block fix_inline" onclick="SearchForBook_Click();">
                        Search for Book
                    </div>
                    <div class="search_grid">
                        <asp:Table runat="server">
                            <asp:TableRow>
                                <asp:TableCell>Title</asp:TableCell>
                                <asp:TableCell>Author</asp:TableCell>
                                <asp:TableCell>ISBN</asp:TableCell>
                            </asp:TableRow>
                            <%--insert dynamic search results here--%>
                            <%--<asp:TableRow CssClass="search_result">
                                <asp:TableCell>Title</asp:TableCell>
                                <asp:TableCell>Author</asp:TableCell>
                                <asp:TableCell>ISBN</asp:TableCell>
                            </asp:TableRow>--%>
                        </asp:Table>
                    </div>
                </div>
            </div>
            <div runat="server" class="margin_top_bottom">
                <div runat="server" class="button" onclick="ToggleElementHidden('byBookDetailsGroupContainer');">
                    By Book Details
                </div>
                <div runat="server" class="group_container hidden byBookDetailsGroupContainer">
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
                    <div class="button small_button display_inline-block fix_inline" onclick="SearchForBook_Click();">
                        Search for Book
                    </div>
                    <div class="search_grid">
                        <asp:Table runat="server">
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
                    <div runat="server" class="grid_5_columns_right button" onclick="CreateNotebook_Click();">
                        Create Notebook
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
    </script>

</asp:Content>
