using System.Drawing;
using System.Windows.Forms;

namespace CardiogramDemo
{
	public partial class FrmMain : Form
	{
		public FrmMain()
		{
			InitializeComponent();
			PostConstruct();
		}

		void PostConstruct()
		{
			_cardiogram.Click += (sender, args) => {
				_cardiogram.TimerEnabled = !_cardiogram.TimerEnabled;
			};
			_cardiogram.EcgData = new Point[] {
				new Point(0, 46), new Point(60, 46), new Point(90, 90),
				new Point(110, 5), new Point(120, 50), new Point(150, 50),
				new Point(160, 55), new Point(170, 60), new Point(180, 70),
				new Point(185, 69), new Point(190, 70), new Point(195, 40),
			};
		}
	}
}
