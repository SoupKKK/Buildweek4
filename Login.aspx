<%@ Page Title="" Language="C#" MasterPageFile="~/Models/Nav.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Buildweek4.Login" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <link rel="stylesheet" href="Style/Login.css">
    <div id="divErrorMessage" runat="server"></div>
     <div id="LoginContainer" runat="server">
    

    <h2>Se è la tua prima volta, effettua la registrazione!</h2>
    <div>
        <div class="container" id="container">

            <div class="form-container sign-up-container">
                <h1>Registrati</h1>
                <input ID="txtRegisterUsername" runat="server"  type="text" placeholder="Name" />
                <input ID="txtRegisterPassword" runat="server"  type="password" placeholder="Password" />
                <asp:Button ID="btnRegister" runat="server" Text="REGISTRATI" OnClick="btnRegister_Click" />

            </div>

            <div class="form-container sign-in-container">

                <div runat="server">
                    <h1>Login</h1>
                    <input type="text" placeholder="Name" runat="server" id="txtUsername" />
                    <input type="password" placeholder="Password" runat="server" id="txtPassword" />                 
                    <asp:Button ID="btnLogin" runat="server" Text="ACCEDI" OnClick="btnLogin_Click" />
                </div>

            </div>

            <div class="overlay-container">
                <div class="overlay">
                    <div class="overlay-panel overlay-left">
                        <h1>Welcome Back!</h1>
                        <p>Per rimanere in contatto con noi effettua il login con i tuoi dati personali</p>
                        <button class="ghost" id="signIn">ACCEDI</button>
                    </div>
                    <div class="overlay-panel overlay-right">
                        <h1>Ciao, Utente!</h1>
                        <p>Inserisci i tuoi dati personali ed inizia la giornata con noi!</p>
                        <button class="ghost" id="signUp">REGISTRATI</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
    <script>
            const signUpButton = document.getElementById('signUp');
            const signInButton = document.getElementById('signIn');
            const container = document.getElementById('container');

            const signUpContainer = document.querySelector('.sign-up-container');
            const signInContainer = document.querySelector('.sign-in-container');

            signUpButton.addEventListener('click', (event) => {
                event.preventDefault();
                container.classList.add("right-panel-active");
                signInContainer.style.display = 'none';
            });

            signInButton.addEventListener('click', (event) => {
                event.preventDefault();
                container.classList.remove("right-panel-active");
                signInContainer.style.display = 'block'; // Riporta alla visibilità normale
            });
    </script>

</asp:Content>
