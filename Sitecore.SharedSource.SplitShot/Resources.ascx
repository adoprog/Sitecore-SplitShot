<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Resources.ascx.cs" Inherits="Sitecore.SharedSource.SplitShot.Resources" %>
<%@ Assembly Name="Sitecore.Kernel" %>
    <style type="text/css">
        .splitshotIcon{position:fixed; top:50%; left:10px; z-index:9999;}
    </style>

    <script src="/sitecore modules/Web/SplitShot/assets/colorbox/jquery.colorbox.js"></script>
    <link rel="stylesheet" href="/sitecore modules/Web/SplitShot/assets/colorbox/colorbox.css"/>

    <script type="text/javascript">
        $(document).ready(function () {
            $('body').append('<a href="/sitecore modules/Web/SplitShot/SplitShot.aspx?id=<%= Sitecore.Context.Item.ID %>" class="splitshotIcon"><img src="/sitecore modules/Web/SplitShot/assets/img/button.png"/></a>');
            $(".splitshotIcon").colorbox({ iframe: true, innerWidth: 1050, innerHeight: 700 });
        });
    </script>
  