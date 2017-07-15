using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peplex_PFC.UI.Shared
{
    public static class WindowExtensions
    {
        //public static void ApplyDark(this Window window)
        //{
        //    var border = FindWindowCoveringBorder(window);

        //    if (border == null)
        //        return;

        //    border.Style = (Style)window.FindResource("WindowBorderDarkStyle");
        //}

        //public static void RemoveDark(this Window window)
        //{
        //    var border = FindWindowCoveringBorder(window);

        //    if (border == null)
        //        return;

        //    border.Style = (Style)window.FindResource("WindowBorderTransparentStyle");
        //}

        //public static bool? ShowDialogOnDarkBackground(this Window window)
        //{
        //    bool? result;

        //    try
        //    {
        //        if (window.Owner != null)
        //            window.Owner.ApplyDark();

        //        result = window.ShowDialog();
        //    }
        //    finally
        //    {
        //        if (window.Owner != null)
        //            window.Owner.RemoveDark();
        //    }

        //    return result;
        //}
    }
}
