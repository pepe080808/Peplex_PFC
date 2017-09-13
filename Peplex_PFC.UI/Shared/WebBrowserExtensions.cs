using System;
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
            + "<html><body scroll=\"no\" leftmargin=\"0px\" topmargin=\"00px\" marginwidth=\"0px\" marginheight=\"0px\" >" + "\r\n"
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
    }
}
