<%@ Page Title="" Language="C#" MasterPageFile="~/Models/Nav.Master" AutoEventWireup="true" CodeBehind="EditProduct.aspx.cs" Inherits="Buildweek4.EditProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="WarningMsg"  runat="server" class="my-3"></div>

    <div class="container mt-4" ID="EditContainer" runat="server">
        <h2 id="EditProductText" runat="server">Modifica Prodotto</h2>
        <h2 id="AddProductText" runat="server">Aggiungi Prodotto</h2>

        <asp:Label ID="lblProductId" runat="server" Visible="false"></asp:Label>

        <div class="form-group">
            <label for="txtNewProductName">Nuovo Nome Prodotto:</label>
            <asp:TextBox ID="txtNewProductName" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtNewProductPrice">Nuovo Prezzo:</label>
            <asp:TextBox ID="txtNewProductPrice" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtNewProductImage">Nuova Immagine URL:</label>
            <asp:TextBox ID="txtNewProductImage" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtNewProductDescription">Nuova Descrizione:</label>
            <asp:TextBox ID="txtNewProductDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
        </div>

        <div class="my-4">
            <asp:Button ID="btnSaveChanges" runat="server" CssClass="btn btn-primary" Text="Salva Modifiche" OnClick="btnSaveChanges_Click" />
            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Elimina Prodotto" OnClick="btnDelete_Click" UseSubmitBehavior="false" />
            <asp:Button ID="btnAddChanges" runat="server" CssClass="btn btn-primary" Text="Aggiungi Prodotto" OnClick="btnSaveChanges_Click_Add" /> 
        </div>
    </div>

    

</asp:Content>
