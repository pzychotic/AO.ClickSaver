using System.Windows.Controls;

namespace pzy.AO.ClickSaver
{
    /// <summary>
    /// Interaction logic for MissionInfo.xaml
    /// </summary>
    public partial class MissionInfo : UserControl
    {
        public MissionInfo()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            _tbLocation.Text = "";
            _tbType.Text = "";
            _tbRewardItem.Text = "";
            _tbFindItem.Text = "";
            _tbExperience.Text = "";
            _tbCash.Text = "";
            _tbValue.Text = "";
            _tbTotal.Text = "";
        }
    }
}
