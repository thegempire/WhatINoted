<%@ Page Title="Login Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WhatINoted._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="LoginUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div runat="server" id="LoginSignUp" class="loginSignUp">
                <div class="loginDivButton">
                    <asp:Button ID="Login" runat="server" Text="Login" Width="200" OnClick="Login_Click" />
                </div>
                <div class="signUpDivButton">
                    <asp:Button ID="SignUp" runat="server" Text="Sign Up" Width="200" OnClick="SignUp_Click" />
                </div>
            </div>

            <div runat="server" id="LoginDiv" class="loginDiv">
                <div class="loginGoogle">
                    <asp:Button ID="LoginGoogle" runat="server" Text="Login with Google" Width="200" OnClick="Redirect_Click" /><!-- Temporary link to next page until login/signup implemented: PostBackUrl="~/Main.aspx" -->
                </div>
                <div class="loginFacebook">
                    <asp:Button ID="LoginFacebook" runat="server" Text="Login with Facebook" Width="200" />
                </div>
            </div>

            <div runat="server" id="SignUpDiv" class="signUpDiv">
                <div class="signUpGoogle">
                    <asp:Button ID="SignUpGoogle" runat="server" Text="Sign Up with Google" Width="200" />
                </div>
                <div class="signUpFacebook">
                    <asp:Button ID="SignUpFacebook" runat="server" Text="Sign Up with Facebook" Width="200" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
