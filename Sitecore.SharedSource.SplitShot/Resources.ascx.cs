using Sitecore.Configuration;

namespace Sitecore.SharedSource.SplitShot
{
    using System;

    public partial class Resources : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool enabled = Settings.GetBoolSetting("Sitecore.SharedSource.SplitShot.Enabled", false);
            if (!enabled || (Request.UserAgent == null) || Request.UserAgent.Contains("PhantomJS"))
            {
                this.Visible = false;
            }
        }
    }
}