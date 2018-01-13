using System.Windows.Forms;

namespace Masterplan.Controls
{
    public partial class WelcomePanel : UserControl
    {
        public WelcomePanel(bool show_headlines)
        {
            this.InitializeComponent();
            this.fShowHeadlines = show_headlines;
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.MenuBrowser.DocumentText = "";
            this.set_options();
            if (this.fShowHeadlines)
            {
                this.DownloadHeadlines("http://www.habitualindolence.net/masterplanblog/feed/");
            }

        }
    }
}
