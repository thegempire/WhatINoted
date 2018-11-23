<%@ Page Title="Add Notebook" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NotebookEditor.aspx.cs" Inherits="WhatINoted.NotebookCreationView" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script>
        function rowClicked(el) {
            WebForm_GetElementById("MainContent_ModalTitle").innerText = WebForm_GetElementById(el.id + "_Title").innerText;
            WebForm_GetElementById("MainContent_TitleSelection").value = encodeURIComponent(WebForm_GetElementById(el.id + "_Title").innerText);

            WebForm_GetElementById("MainContent_ModalAuthors").innerText = WebForm_GetElementById(el.id + "_Authors").innerText;
            WebForm_GetElementById("MainContent_AuthorsSelection").value = encodeURIComponent(WebForm_GetElementById(el.id + "_Authors").innerText);

            WebForm_GetElementById("MainContent_ModalPublisher").innerText = WebForm_GetElementById(el.id + "_Publisher").innerText;
            WebForm_GetElementById("MainContent_PublisherSelection").value = encodeURIComponent(WebForm_GetElementById(el.id + "_Publisher").innerText);

            WebForm_GetElementById("MainContent_ModalPublishDate").innerText = WebForm_GetElementById(el.id + "_PublishDate").innerText;
            WebForm_GetElementById("MainContent_PublishDateSelection").value = encodeURIComponent(WebForm_GetElementById(el.id + "_PublishDate").innerText);

            WebForm_GetElementById("MainContent_ModalISBN").innerText = WebForm_GetElementById(el.id + "_ISBN").innerText;
            WebForm_GetElementById("MainContent_IsbnSelection").value = encodeURIComponent(WebForm_GetElementById(el.id + "_ISBN").innerText);

            WebForm_GetElementById("MainContent_CoverUrlSelection").value = encodeURIComponent(WebForm_GetElementById(el.id + "_CoverUrl").innerText);

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
            <asp:Button ID="ShowButton" runat="server" Style="display: none" OnClientClick="showPopup()" />
            <div runat="server" class="margin_top_bottom">

                <div id="stopFlash", style="display: none">
                    <asp:Panel ID="CreationModal" runat="server" CssClass="notebook_creation_modal">
                        <asp:Table runat="server" CssClass="search_grid">
                            <asp:TableRow>
                                <asp:TableCell><i>Title</i></asp:TableCell>
                                <asp:TableCell><i>Author</i></asp:TableCell>
                                <asp:TableCell><i>Publisher</i></asp:TableCell>
                                <asp:TableCell><i>Publication Date</i></asp:TableCell>
                                <asp:TableCell><i>ISBN</i></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow runat="server" ID="ConfirmationRow">
                                <asp:TableCell ID="ModalTitle"></asp:TableCell>
                                <asp:TableCell ID="ModalAuthors"></asp:TableCell>
                                <asp:TableCell ID="ModalPublisher"></asp:TableCell>
                                <asp:TableCell ID="ModalPublishDate"></asp:TableCell>
                                <asp:TableCell ID="ModalISBN"></asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                        <asp:Button ID="CancelModal" runat="server" Text="Cancel" CssClass="modal_button" />
                        <asp:Button ID="SubmitModal" runat="server" Text="Create" CssClass="modal_button" OnClick="CreateNotebook" />
                    </asp:Panel>
                </div>
                <ajaxToolkit:ModalPopupExtender runat="server" TargetControlID="ShowButton" PopupControlID="CreationModal" OkControlID="CancelModal" BackgroundCssClass="notebook_creation_modal_bg"></ajaxToolkit:ModalPopupExtender>

                <h2>Create New Notebook</h2>
                <div runat="server" class="button" onclick="ToggleElementHidden('byISBNGroupContainer');">
                    By ISBN
                </div>
                
                <div runat="server" id="ByISBNGroupContainer" class="byISBNGroupContainer group_container">
                    <asp:Panel runat="server" DefaultButton="btnISBNPostBack">
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
                                <div runat="server" id="WorkingDiv" class="display_inline-block fix_inline">
                                    
                                </div>

                                <br />

                                <div runat="server" class="titled_field display_inline-block">
                                    <h4>ISBN</h4>
                                    <asp:TextBox runat="server" ID="IsbnBox" CssClass="full_width"></asp:TextBox>
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
                                        <asp:TableRow CssClass="search_header_row">
                                            <asp:TableCell ColumnSpan="5">
                                                Search Results
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow CssClass="search_fields_row">
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
                    </asp:Panel>
                </div>
            </div>
            <div runat="server" class="margin_top_bottom">
                <div runat="server" class="button" onclick="ToggleElementHidden('byBookDetailsGroupContainer');">
                    By Book Details
                </div>
                <div runat="server" id="ByBookDetailsGroupContainer" class="group_container byBookDetailsGroupContainer hidden">
                    <asp:Panel runat="server" DefaultButton="HiddenPanelButton">
                                <div class="titled_field display_inline-block">
                                    <h4>Title</h4>
                                    <input type="text" id="TitleEntry" class="full_width" maxlength="30" />
                                    <asp:HiddenField runat="server" ID="HiddenTitleEntry"></asp:HiddenField>
                                </div>
                                <div class="titled_field display_inline-block">
                                    <h4>Author</h4>
                                    <input type="text" id="AuthorEntry" class="full_width" maxlength="30" />
                                    <asp:HiddenField runat="server" ID="HiddenAuthorEntry"></asp:HiddenField>
                                </div>
                                <div class="titled_field display_inline-block">
                                    <h4>Publisher</h4>
                                    <input type="text" id="PublisherEntry" class="full_width" maxlength="30" />
                                    <asp:HiddenField runat="server" ID="HiddenPublisherEntry"></asp:HiddenField>
                                </div>
                                <br />
                        <asp:UpdatePanel runat="server" ID="ByDetailsGroupPanel" UpdateMode="Conditional">
                            <ContentTemplate>
                        <asp:Button runat="server" ID="HiddenPanelButton" OnClientClick="click_btnBookDetailsPostback" class="hidden"/>
                                <asp:Button runat="server" ID="btnBookDetailsPostback" Style="display: none" OnClick="SearchForBook" />
                                <div class="button small_button display_inline-block fix_inline" onclick="click_btnBookDetailsPostback()">
                                    Search for Book
                                </div>
                                <div runat="server" class="search_grid">
                                    <asp:Table runat="server" ID="SearchGridDetails">
                                        <asp:TableRow CssClass="search_header_row">
                                            <asp:TableCell ColumnSpan="5">
                                                Search Results
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow CssClass="search_fields_row">
                                            <asp:TableCell>Title</asp:TableCell>
                                            <asp:TableCell>Author</asp:TableCell>
                                            <asp:TableCell>Publisher</asp:TableCell>
                                            <asp:TableCell>Publication Date</asp:TableCell>
                                            <asp:TableCell>ISBN</asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </div>

                                <br />
                                <div runat="server" class="search_grid">
                                    <asp:Table runat="server" ID="SearchGridCustom">
                                        <asp:TableRow CssClass="search_header_row">
                                            <asp:TableCell ColumnSpan="5" >
                                                Custom Book
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow CssClass="search_fields_row">
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
                    </asp:Panel>
                </div>
            </div>
            
            <asp:UpdatePanel runat="server" ID="HiddenUpdatePanel" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:HiddenField runat="server" ID="HandleLoginUserID" Value="" />
                    <asp:Button runat="server" class="handleLoginTrigger hidden" OnClick="UpdatePage"/>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/fabric.js/1.4.13/fabric.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/darkroomjs/2.0.1/darkroom.js"></script>
    <link type="text/css" rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/darkroomjs/2.0.1/darkroom.css" />

    <script>
        window.addEventListener('load', handleLoginForContentPage);

        var path;
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
            if (val != null && val != '') {
                document.getElementById('<%= WorkingDiv.ClientID %>').innerText = 'Working...';
                document.getElementById('<%= btnExtractText.ClientID %>').click();
            }
        }
        
        function click_btnBookDetailsPostback() {
            var titleText = document.getElementById('TitleEntry').value;
            titleText = encodeURIComponent(titleText);
            document.getElementById('<%= HiddenTitleEntry.ClientID %>').value = titleText;
            
            var authorText = document.getElementById('AuthorEntry').value;
            authorText = encodeURIComponent(authorText);
            document.getElementById('<%= HiddenAuthorEntry.ClientID %>').value = authorText;
            
            var publisherText = document.getElementById('PublisherEntry').value;
            publisherText = encodeURIComponent(publisherText);
            document.getElementById('<%= HiddenPublisherEntry.ClientID %>').value = publisherText;

            document.getElementById('<%= btnBookDetailsPostback.ClientID %>').click();
        }
        
        function showPopup() {
            document.getElementById('stopFlash').style.display = 'block';
        }
    </script>
</asp:Content>
