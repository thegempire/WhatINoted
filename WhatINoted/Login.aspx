<%@ Page Title="Login Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WhatINoted.LoginView" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="LoginUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <div id="firebaseUIAuthContainer" />

        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        // Initialize the FirebaseUI Widget using Firebase.
        var ui = new firebaseui.auth.AuthUI(firebase.auth());
        // The start method will wait until the DOM is loaded.
        ui.start('#firebaseUIAuthContainer', uiConfig);

        window.addEventListener('load', handleLoginForLoginPage());
    </script>
</asp:Content>
