<%@ Page Title="" Language="C#" MasterPageFile="~/Models/Nav.Master" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="BuildWeek4.ProductDetails" %>
<%@ Import Namespace="Buildweek4" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <link rel="stylesheet" href="Style/ProductDetails.css">


        <section class="product"  id="ProducDetailContainer" runat="server">
    <div class="product__photo">
        <div class="photo-container">
            <div class="photo-main">

                <img id="img" runat="server" src="" alt="" class="w-50" />

            </div>
        </div>
    </div>
    <div class="product__info">
        <div class="title">
            <h1 id="ttlProduct" runat="server">Nome Prodotto</h1>


        </div>
        <div class="price">
            <p id="txtPrice" runat="server"></p>

        </div>
        <div class="description">
                    <p id="txtDescription" runat="server"></p>

        </div>

        <p id="P3" runat="server"></p>
        <div id="ProductDetailsBtns" runat="server">
        <asp:Button id="btnAddCart" runat="server" Text="Aggiungi al carrello" CssClass="btn btn-outline-secondary" OnClick="btnAddCart_Click" />
        </div>
    </div>
</section>
        


</asp:Content>

