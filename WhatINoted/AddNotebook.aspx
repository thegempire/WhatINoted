<%@ Page Title="Add Notebook" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddNotebook.aspx.cs" Inherits="WhatINoted.NotebookCreationView" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script>
        function rowClicked(el) {
            WebForm_GetElementById("MainContent_ModalTitle").innerHTML = WebForm_GetElementById(el.id + "_Title").innerHTML;
            WebForm_GetElementById("MainContent_TitleSelection").value = WebForm_GetElementById(el.id + "_Title").innerHTML;

            WebForm_GetElementById("MainContent_ModalAuthors").innerHTML = WebForm_GetElementById(el.id + "_Authors").innerHTML;
            WebForm_GetElementById("MainContent_AuthorsSelection").value = WebForm_GetElementById(el.id + "_Authors").innerHTML;

            WebForm_GetElementById("MainContent_ModalPublisher").innerHTML = WebForm_GetElementById(el.id + "_Publisher").innerHTML;
            WebForm_GetElementById("MainContent_PublisherSelection").value = WebForm_GetElementById(el.id + "_Publisher").innerHTML;

            WebForm_GetElementById("MainContent_ModalPublishDate").innerHTML = WebForm_GetElementById(el.id + "_PublishDate").innerHTML;
            WebForm_GetElementById("MainContent_PublishDateSelection").value = WebForm_GetElementById(el.id + "_PublishDate").innerHTML;

            WebForm_GetElementById("MainContent_ModalISBN").innerHTML = WebForm_GetElementById(el.id + "_ISBN").innerHTML;
            WebForm_GetElementById("MainContent_IsbnSelection").value = WebForm_GetElementById(el.id + "_ISBN").innerHTML;

            WebForm_GetElementById("MainContent_CoverUrlSelection").value = WebForm_GetElementById(el.id + "_CoverUrl").innerHTML;

            WebForm_GetElementById("MainContent_ShowButton").click();
        }
    </script>
    <asp:UpdatePanel ID="AddNotebookUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField runat="server" ID="TitleSelection" Value="" />
            <asp:HiddenField runat="server" ID="AuthorsSelection" Value="" />
            <asp:HiddenField runat="server" ID="PublisherSelection" Value="" />
            <asp:HiddenField runat="server" ID="PublishDateSelection" Value="" />
            <asp:HiddenField runat="server" ID="IsbnSelection" Value="" />
            <asp:HiddenField runat="server" ID="CoverUrlSelection" Value="" />
            <asp:Button ID="ShowButton" runat="server" Style="display: none" />
            <div runat="server" class="margin_top_bottom">
                <asp:Panel ID="CreationModal" runat="server" CssClass="creation_modal">
                    <asp:Table runat="server" CssClass="search_grid">
                        <asp:TableRow>
                            <asp:TableCell>Title</asp:TableCell>
                            <asp:TableCell>Author</asp:TableCell>
                            <asp:TableCell>Publisher</asp:TableCell>
                            <asp:TableCell>Publication Date</asp:TableCell>
                            <asp:TableCell>ISBN</asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow runat="server" ID="ConfirmationRow">
                            <asp:TableCell runat="server" ID="ModalTitle"></asp:TableCell>
                            <asp:TableCell runat="server" ID="ModalAuthors"></asp:TableCell>
                            <asp:TableCell runat="server" ID="ModalPublisher"></asp:TableCell>
                            <asp:TableCell runat="server" ID="ModalPublishDate"></asp:TableCell>
                            <asp:TableCell runat="server" ID="ModalISBN"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Button ID="CancelModal" runat="server" Text="Cancel" />
                    <asp:Button ID="SubmitModal" runat="server" Text="Create" OnClick="CreateNotebook" />
                </asp:Panel>
                <ajaxToolkit:ModalPopupExtender runat="server" TargetControlID="ShowButton" PopupControlID="CreationModal" OkControlID="CancelModal"></ajaxToolkit:ModalPopupExtender>
                
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
                                </asp:Table>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <asp:HiddenField runat="server" ID="HandleLoginUserID" Value="" />
            <asp:Button runat="server" class="handleLoginTrigger hidden" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        window.addEventListener('load', handleLoginForContentPage);
    </script>

</asp:Content>
