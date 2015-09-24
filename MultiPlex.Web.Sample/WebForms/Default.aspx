<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MultiPlex.Web.Sample.WebForms.Default" MasterPageFile="~\Views\Shared\Site.Master" %>
<asp:Content ID="titleContent" ContentPlaceHolderID="head" runat="server">
    <title><asp:Literal ID="title" runat="server" /></title>
    <script type="text/javascript">
        var timeout = null;
        $(function() {
            var dlg = $('#<%= editWikiForm.ClientID %>');
            var cnt = $('#editWikiContent');
            var cntPos = cnt.position();
            dlg.dialog({ autoOpen: false,
                width: 450,
                position: [cntPos.left - 450 + cnt.outerWidth(), cntPos.top + cnt.outerHeight()],
                show: 'blind',
                hide: 'blind',
                beforeclose: function() { $('#originalWikiContent').show(); $('#previewWikiContent').hide(); }
            });
            cnt.click(function() {
                if (!dlg.dialog('isOpen')) {
                    $.ajax({
                        type: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        url: 'Wiki.asmx/GetWikiSource',
                        data: "{id: '<asp:Literal id="sourceId" runat="server" />', slug: '<asp:Literal id="sourceSlug" runat="server" />', version: '<asp:Literal id="sourceVersion" runat="server" />'}",
                        dataType: 'json',
                        success: function(data) {
                            $('#<%= Source.ClientID %>').val(data.d);
                            var original = $('#originalWikiContent');
                            original.hide();
                            $('#previewWikiContent').html(original.html()).show();
                            dlg.dialog('open');
                        }
                    });
                } else {
                    dlg.dialog('close');
                }
            });

            $('#<%= Source.ClientID %>').keyup(function(e) {
                if (timeout != null) {
                    clearTimeout(timeout);
                    timeout = null;
                }

                var self = $(this);
                timeout = setTimeout(function() {
                    $.ajax({
                        type: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        url: 'Wiki.asmx/GetWikiPreview',
                        data: "{id: '<asp:Literal id="previewId" runat="server" />', slug: '<asp:Literal id="previewSlug" runat="server" />', source: '" + self.val()  + "'}",
                        dataType: 'json',
                        success: function(data) { $('#previewWikiContent').html(data.d); }
                    });
                }, 250);
            });

            $('#cancelEdit').click(function() {
                $('#<%= editWikiForm.ClientID %>').dialog('close');
            });
        });
    </script>
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="editWiki" class="editWiki" runat="server">
        <a id="editWikiContent" href="#">Edit Content</a>
    </div>
    
    <div id="wikiHistory">
        <h3>Page History</h3>
        <ul>
            <asp:Repeater ID="pageHistory" runat="server" OnItemDataBound="BindPageHistoryItem">
                <ItemTemplate>
                    <li>
                        <asp:Literal ID="date" runat="server" />
                        <asp:HyperLink ID="versionLink" runat="server" />
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    
    <div id="wikiContent">
        <div id="originalWikiContent"><asp:Literal ID="renderedSource" runat="server" /></div>
        <div id="previewWikiContent" style="display:none;"></div>
    </div>
    
    <div class="clear"></div>
    
    <div id="editWikiForm" class="editWikiForm" runat="server">
        <form runat="server">
            <asp:HiddenField ID="Name" runat="server" />
            <fieldset>
                <label for="Source">Source:</label>
                <asp:PlaceHolder ID="NotLatestPlaceHolder" runat="server">
                    <span id="editWikiNotLatest">
                        Note: Not editing latest source
                    </span>
                </asp:PlaceHolder>
                <asp:TextBox ID="Source" TextMode="MultiLine" runat="server" />
                <asp:Button ID="SaveButton" Text="Save" OnClick="SaveWikiContent" runat="server" />
                <input id="cancelEdit" type="button" value="Cancel" />
            </fieldset>
        </form>
    </div>
</asp:Content>