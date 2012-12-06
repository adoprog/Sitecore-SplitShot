<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SplitShot.aspx.cs" Inherits="Sitecore.SharedSource.SplitShot.SplitShot" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SplitShot Preview</title>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.js"></script>
    <!-- The stylesheet -->
    <link rel="stylesheet" href="/sitecore modules/Web/SplitShot/assets/css/styles.css" />
    <link rel="stylesheet" href="/sitecore modules/Web/SplitShot/assets/touchTouch/touchTouch.css" />
    <meta http-Equiv="Cache-Control" Content="no-cache">
    <meta http-Equiv="Pragma" Content="no-cache">
    <meta http-Equiv="Expires" Content="0">
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="thumbs">
                <asp:Repeater ID="SplitShots" runat="server" OnItemDataBound="SplitShotsItemDataBound">
                    <ItemTemplate>
                        <asp:HyperLink ID="Link" runat="server">
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <script src="/sitecore modules/Web/SplitShot/assets/touchTouch/touchTouch.jquery.js"></script>
            <script type="text/javascript">
                $(function () {
                    $('#thumbs a').touchTouch();
                });
            </script>
        </div>
    </form>
</body>
</html>
