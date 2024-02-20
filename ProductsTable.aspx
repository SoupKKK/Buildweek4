<%@ Page Title="" Language="C#" MasterPageFile="~/Models/Nav.Master" AutoEventWireup="true" CodeBehind="ProductsTable.aspx.cs" Inherits="Buildweek4.ProductsTable" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Aggiungi eventuali stili o script aggiuntivi qui -->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4" id="ProductsTableContainer" runat="server">
        <h2>Tabella Prodotti Shop</h2>

        <asp:GridView ID="productsGridView" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered m-3">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" />
                <asp:BoundField DataField="Nome" HeaderText="Nome" />
                <asp:BoundField DataField="Prezzo" HeaderText="Prezzo" />
                <asp:BoundField DataField="Descrizione" HeaderText="Descrizione" />
                <asp:ImageField DataImageUrlField="Immagine" HeaderText="Immagine" ControlStyle-Width="100" />

                <asp:TemplateField>
                    <ItemTemplate>
                        <a href='<%# "EditProduct.aspx?product=" + Eval("ID") %>' class="btn btn-warning me-2">Modifica</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <a href="EditProduct.aspx" class="btn btn-primary my-3 ">AGGIUNGI PRODOTTO</a>
    </div>
</asp:Content>
