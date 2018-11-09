<%@ Page Title="WhatINoted" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WhatINoted.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>    
        window.addEventListener('load', function () {
            firebase.auth().onAuthStateChanged(function (user) {
                if (user) {
                    // User is logged in
                    window.location = "./Notebooks.aspx";
                } else {
                    // User is logged out
                    window.location = "./Login.aspx";
                }
            })
        });
    </script>
</asp:Content>
