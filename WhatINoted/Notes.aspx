<%@ Page Title="Notes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Notes.aspx.cs" Inherits="WhatINoted.Notes" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="LoginUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div runat="server">
                <h2>Notebook Title</h2>
                <asp:Table runat="server" ID="noteTable">
                    <asp:TableRow>
                        <asp:TableCell>Note text ...</asp:TableCell>
                        <asp:TableCell><asp:Button runat="server" Text="Edit"></asp:Button></asp:TableCell>
                        <asp:TableCell><asp:Button runat="server" Text="Delete"></asp:Button></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>Note text 2...</asp:TableCell>
                        <asp:TableCell><asp:Button runat="server" Text="Edit"></asp:Button></asp:TableCell>
                        <asp:TableCell><asp:Button runat="server" Text="Delete"></asp:Button></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>Note text 3 ...</asp:TableCell>
                        <asp:TableCell><asp:Button runat="server" Text="Edit"></asp:Button></asp:TableCell>
                        <asp:TableCell><asp:Button runat="server" Text="Delete"></asp:Button></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
