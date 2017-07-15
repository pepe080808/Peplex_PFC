using System;
using System.Text;
using System.Windows.Controls;

namespace Peplex_PFC.UI.Shared
{
    public static class WebBrowserExtensions
    {
        public static void GetYoutubeVideo(this WebBrowser webBrowser, string urlVideo)
        {
            String id = urlVideo;

            string page =
            "<!DOCTYPE html PUBLIC \" -//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\" >\r\n"
            + @"<!-- saved from url=(0022)http://www.youtube.com -->" + "\r\n"
            + "<html><body scroll=\"no\" leftmargin=\"0px\" topmargin=\"0px\" marginwidth=\"0px\" marginheight=\"0px\" >" + "\r\n"
                + GetYouTubeScript(id, webBrowser.Width, webBrowser.Height)
                + "</body></html>\r\n";

            webBrowser.NavigateToString(page);
        }

        private static string GetYouTubeScript(string id, double width, double height)
        {
            string scr = @"<object width='" + width + "' height='" + height + "'> " + "\r\n";
            scr = scr + @"<param name='movie' value='http://www.youtube.com/v/" + id + "'></param> " + "\r\n";
            scr = scr + @"<param name='allowFullScreen' value='true'></param> " + "\r\n";
            scr = scr + @"<param name='allowscriptaccess' value='always'></param> " + "\r\n";
            scr = scr + @"<embed src='http://www.youtube.com/v/" + id + "' ";
            scr = scr + @"type='application/x-shockwave-flash' allowscriptaccess='always' ";
            scr = scr + @"allowfullscreen='true' width='" + width + "' height='" + height + "'> " + "\r\n";
            scr = scr + @"</embed></object>" + "\r\n";
            return scr;
        }

        //private static string GetYouTubeVideoPlayerHTML(string videoCode)
        //{
        //    var sb = new StringBuilder();

        //    const string YOUTUBE_URL = @"http://www.youtube.com/v/";

        //    sb.Append("<html>");
        //    sb.Append("    <head>");
        //    sb.Append("        <meta name=\"viewport\" content=\"width=device-width; height=device-height;\">");
        //    sb.Append("    </head>");
        //    sb.Append("    <body marginheight=\"0\" marginwidth=\"0\" leftmargin=\"0\" topmargin=\"0\" style=\"overflow-y: hidden\">");
        //    sb.Append("        <object width=\"100%\" height=\"100%\">");
        //    sb.Append("            <param name=\"movie\" value=\"" + YOUTUBE_URL + videoCode + "?version=3&amp;rel=0\" />");
        //    sb.Append("            <param name=\"allowFullScreen\" value=\"true\" />");
        //    sb.Append("            <param name=\"allowscriptaccess\" value=\"always\" />");
        //    sb.Append("            <embed src=\"" + YOUTUBE_URL + videoCode + "?version=3&amp;rel=0\" type=\"application/x-shockwave-flash\"");
        //    sb.Append("                   width=\"100%\" height=\"100%\" allowscriptaccess=\"always\" allowfullscreen=\"true\" />");
        //    sb.Append("        </object>");
        //    sb.Append("    </body>");
        //    sb.Append("</html>");

        //    return sb.ToString();
        //}

        //public static void ShowYouTubeVideo(this WebBrowser webBrowser, string videoCode)
        //{
        //    if (webBrowser == null) throw new ArgumentNullException("webBrowser");

        //    webBrowser.NavigateToString(GetYouTubeVideoPlayerHTML(videoCode));
        //}
    }
}
