using Sitecore.Configuration;

namespace Sitecore.SharedSource.SplitShot.Media
{
    using Sitecore.Data;
    using Sitecore.Links;
    using Sitecore.Resources.Media;
    using Sitecore.Sites;
    using System.IO;
    using System.Web;

    public class PageScreenshotProcessor
    {
        public static void Process(GetMediaStreamPipelineArgs args)
        {
            bool enabled = Settings.GetBoolSetting("Sitecore.SharedSource.SplitShot.Enabled", false);
            if(!enabled)
            {
                return;
            }

            var itemId = args.Options.CustomOptions["itemId"];
            if (itemId != null)
            {
                ID targetItemId;
                if (!ID.TryParse(args.Options.CustomOptions["itemId"], out targetItemId))
                {
                    return;
                }

                GetWebsiteScripts(args, targetItemId);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        protected static void GetWebsiteScripts(GetMediaStreamPipelineArgs args, ID targetItemId)
        {
            string targetItemUrl;
            string site = string.IsNullOrEmpty(args.Options.CustomOptions["site"])
                              ? "website"
                              : args.Options.CustomOptions["site"];
            using (new SiteContextSwitcher(SiteContext.GetSite(site)))
            {
                var item = Sitecore.Context.Database.GetItem(targetItemId);
                if (item == null)
                {
                    return;
                }

                var options = new UrlOptions();
                options.LanguageEmbedding = LanguageEmbedding.Never;
                options.AlwaysIncludeServerUrl = true;
                targetItemUrl = LinkManager.GetItemUrl(item, options);
            }

            var fileName = string.Format("SplitShot_{0}.png", ID.NewID);
            string tempPath = HttpContext.Current.Server.MapPath("/temp");
            string serverPath = HttpContext.Current.Server.MapPath("/App_Data/SplitShot");
            string arguments = string.Format(" rasterize.js \"{0}\" {1}\\{2}", targetItemUrl, tempPath, fileName);
            var psi = new System.Diagnostics.ProcessStartInfo(string.Format("{0}\\phantomjs.exe", serverPath), arguments)
                          {
                              UseShellExecute = false,
                              RedirectStandardOutput = true,
                              RedirectStandardInput = true,
                              RedirectStandardError = true,
                              WorkingDirectory = serverPath
                          };

            var process = System.Diagnostics.Process.Start(psi);
            process.WaitForExit();

            var stream = new MemoryStream();
            AddFileToStream(stream, "/temp/" + fileName);

            args.OutputStream = new MediaStream(stream, "png", args.MediaData.MediaItem);
        }

        private static void AddFileToStream(MemoryStream stream, string fileName)
        {
            var fileBytes = File.ReadAllBytes(HttpContext.Current.Server.MapPath(fileName));
            stream.Write(fileBytes, 0, fileBytes.Length);
        }

    }
}