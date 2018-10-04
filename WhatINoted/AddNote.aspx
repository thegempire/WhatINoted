<%@ Page Title="Add Note" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddNote.aspx.cs" Inherits="WhatINoted.AddNote" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="LoginUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div runat="server">
                <h2>Create New Note</h2>
                <div runat="server" class="button" OnClick="ByImage_Click">
                    By Image
                </div>
                <div runat="server" id="ByImageGroupContainer" class="group_container">
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
                    <div runat="server" class="grid_5_columns_right button">
                        Create Note
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
