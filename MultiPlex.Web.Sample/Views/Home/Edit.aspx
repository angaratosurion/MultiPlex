<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<WikiPlex.Web.Sample.Models.Content>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <title>Edit Wiki</title>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>
    <% using (Html.BeginRouteForm("Act", new { action = "EditWiki", Model.Title.Slug }, FormMethod.Post)) { %>
        <fieldset>
            <label for="Name">Title:</label>
            <%= Html.TextBox("Name", Model.Title.Name) %>
            <label for="Source">Source:</label>
            <%= Html.TextArea("Source") %>
            <label></label>
            <input type="submit" value="Save" />
            <% if (Model.Title.Id > 0) { %>
            <input type="button" value="Cancel" onclick="window.location.href='<%= Url.RouteUrl("Default") %>'" />
            <% } %>
        </fieldset>
    <% } %>

</asp:Content>
