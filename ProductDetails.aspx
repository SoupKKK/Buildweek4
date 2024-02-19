<%@ Page Title="" Language="C#" MasterPageFile="~/Models/Nav.Master" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="BuildWeek4.ProductDetails" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="container-lg m-4">
        <h2 id="ttlProduct" runat="server">Nome Prodotto</h2>
        <img id="img" runat="server" src="" alt="" class="w-50" />
        <p id="txtDescription" runat="server"></p>
        <p id="txtPrice" runat="server"></p>
        <asp:Button id="btnAddCart" runat="server" Text="Aggiungi al carrello" CssClass="btn btn-outline-secondary" OnClick="btnAddCart_Click" />
    </div>
</asp:Content>
