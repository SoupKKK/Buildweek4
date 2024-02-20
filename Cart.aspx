<%@ Page Title="" Language="C#" MasterPageFile="~/Models/Nav.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="Buildweek4.Cart" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divMessage" runat="server" ></div>
    <div id="contentTot" runat="server" class="my-3 mx-4"></div>
    <ul id="htmlContent" runat="server" class="m-5 w-50">
        <asp:Repeater ID="rptCartItems" runat="server" OnItemCommand="rptCartItems_ItemCommand">
    <ItemTemplate>
        <li class="d-flex justify-content-between">
            <p><%# Eval("Nome") %> (Quantità: <%# Eval("quantita") %>)</p>
            <div class="d-flex mb-2 align-items-baseline">
                <p class="d-flex me-1"><%# Eval("Prezzo") %>€</p>
                <asp:Button runat="server" CommandName="Delete" CommandArgument='<%# Eval("ID") %>'
                    CssClass="btn btn-danger w-75" Text="Elimina" OnClientClick="return confirm('Sei sicuro di voler eliminare questo elemento?');" />
            </div>
        </li>
    </ItemTemplate>
</asp:Repeater>

    </ul>
</asp:Content>
