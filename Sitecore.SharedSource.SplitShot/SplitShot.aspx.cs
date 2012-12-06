namespace Sitecore.SharedSource.SplitShot
{
    using System;
    using System.Linq;
    using System.Web.UI.WebControls;
    using Sitecore.Configuration;
    using Sitecore.Sites;
    using Sitecore.StringExtensions;

    public partial class SplitShot : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var enabledSites = Settings.GetSetting("Sitecore.SharedSource.SplitShot.Sites");
            var sites = SiteManager.GetSites();
            SplitShots.DataSource = sites.Where(x => enabledSites.Contains(x.Name));
            SplitShots.DataBind();
        }

        protected void SplitShotsItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var site = (Site)e.Item.DataItem;
            var link = (HyperLink)e.Item.FindControl("Link");

            link.NavigateUrl = "/~/media/Images/SplitShot/Image.ashx?itemId={0}&site={1}&salt={2}"
                .FormatWith(Request["id"], site.Name, Sitecore.Data.ID.NewID.ToString());
            link.ToolTip = site.Name;
            link.Style.Add("background-image", "url({0})".FormatWith(link.NavigateUrl));
        }
    }
}