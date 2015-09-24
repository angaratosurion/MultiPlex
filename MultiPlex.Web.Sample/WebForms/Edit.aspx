<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="MultiPlex.Web.Sample.WebForms.Edit" MasterPageFile="~\Views\Shared\Site.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <title>Edit Wiki</title>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>
    <form runat="server">
        <fieldset>
            <label for="Name">Title:</label>
            <asp:TextBox ID="Name" runat="server" />
            <label for="Source">Source:</label>
            <asp:TextBox ID="Source" TextMode="MultiLine" runat="server" />
            <label></label>
            <asp:Button ID="Save" Text="Save" OnClick="SaveSource" runat="server" />
            <asp:PlaceHolder ID="CancelPlaceHolder" Visible="false" runat="server">
                <asp:Button ID="Cancel" UseSubmitBehavior="false" Text="Cancel" runat="server" />
            </asp:PlaceHolder>
        </fieldset>
    </form>

</asp:Content>
