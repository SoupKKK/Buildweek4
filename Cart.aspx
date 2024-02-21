<%@ Page Title="" Language="C#" MasterPageFile="~/Models/Nav.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="Buildweek4.Cart" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <link rel="stylesheet" href="Style/Cart.css"/>

    <link href="https://fonts.googleapis.com/css2?family=Muli:wght@400;700&display=swap" rel="stylesheet">
    <div id="divMessage" runat="server" ></div>
    <div class="CartBody" id="cartContainer" runat="server">

 <ul id="htmlContent" runat="server">
     <asp:Repeater ID="rptCartItems" runat="server" OnItemCommand="rptCartItems_ItemCommand">
         <ItemTemplate>
             <div class="cardCart">
                 <div class="separatore1">
                 <img class="img-fluid" src='<%# ResolveUrl(Eval("immagine").ToString()) %>'/>
                     <div class="separatore15">
                 <p> <%# Eval("Nome") %></p>
                  <p class="cartQuantity" id="cartQuantity" runat="server">X<%# Eval("quantita") %></p>
                     </div>
                 </div>
                 <div>
                     <div class="separatore">
                     <p><%# Eval("Prezzo") %>€</p>
                     <asp:Button runat="server" CommandName="Delete" CommandArgument='<%# Eval("ID") %>'
                         CssClass="btn btn-danger m-3" Text="Elimina" OnClientClick="return confirm('Sei sicuro di voler eliminare questo elemento?');" />
                     </div>
                 </div>
             </div>
             </li>
         </ItemTemplate>
     </asp:Repeater>
 </ul>
        <div id="contentTot" runat="server"></div>
    </div>
</asp:Content>

