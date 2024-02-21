<%@ Page Title="" Language="C#" MasterPageFile="~/Models/Nav.Master" AutoEventWireup="true" CodeBehind="UsersCarts.aspx.cs" Inherits="Buildweek4.UsersCarts" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
        <link rel="stylesheet" href="Style/UsersCarts.css"/>

    <div id="UsersCartsWarning" runat="server"></div>

    <asp:GridView ID="gridUsersCarts" runat="server" AutoGenerateColumns="false" CssClass="center-table">
    <Columns>
        <asp:TemplateField HeaderText="Carrelli:">
            <ItemTemplate>
                <div class="userCart">
                    <div class="productName">
                        <asp:Label runat="server" Text='<%# Eval("Username") %>' />
                    </div>
                    <div>
                        <ul class="productList">
                            <asp:Literal runat="server" Text='<%# Eval("ProdottiNelCarrello") %>'></asp:Literal>
                        </ul>
                    </div>
                    <div>
                        <asp:Label runat="server" Text='<%# Eval("Quantita") %>' />
                        <span> pezzi totali</span>
                        <br />
                        <span>Totale: </span>
                        <asp:Label runat="server" ID="lblTotale" Text='<%# Eval("TotaleCarrello") %>' />
                        <span>€</span>
                    </div>
                </div>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

</asp:Content>
