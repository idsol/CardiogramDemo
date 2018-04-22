using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CardiogramDemo
{
	/// <summary>
	/// 心电图规格参考
	/// https://zhidao.baidu.com/question/495657795623266524.html
	/// 
	/// 1 英寸 = 2.54 厘米
	/// sample DPI: 96 像素/英寸
	/// 1 mm = {DPI} / 25.4
	/// 1 cm = {DPI} / 2.54
	/// 
	/// 96 pixel/inch DPI 下，1 mm = 96/25.4 ~= 3.7795 ~= 4
	/// </summary>
	public class CardiogramControl : Control
	{
		readonly Timer _timer = new Timer {
			Interval = 40
		};
		private int _timerIteration;

		public CardiogramControl()
		{
			// 双缓冲 \see https://msdn.microsoft.com/zh-cn/library/hwac9f5b(v=vs.80).aspx
			//
			SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

			// 调整控件大小时重绘控件
			//
            SetStyle(ControlStyles.ResizeRedraw, true);

			_timer.Tick += _timer_Tick;
		}

		private void _timer_Tick(object sender, EventArgs e)
		{
			++_timerIteration;
			Refresh();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			DrawBackground(e);
			DrawForeground(e);
			base.OnPaint(e);
		}

		/// <summary>
		/// 绘制背景
		/// </summary>
		void DrawBackground(PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			Rectangle fullRect = e.ClipRectangle;
			const float MM_PER_INCH = 25.4f;
			const float FIXED_DPI = 300; // 固定像素密度
			float DPIX = FIXED_DPI; // or g.DpiX;
			float DPIY = FIXED_DPI; // or g.DpiY;
			float xGapInMm = DPIX / MM_PER_INCH;
			float yGapInMm = DPIY / MM_PER_INCH;

			GraphicsState gstate = g.Save();
			try {
				// fill background
				g.FillRectangle(_backBrush, fullRect);

				// horizontal grid lines
				int yindex = 0;
				for (float yoffset = 0; yoffset < fullRect.Height; ++yindex, yoffset += yGapInMm) {
					g.DrawLine(_gridPen, 0, yoffset, fullRect.Right, yoffset);
					if (yindex % 5 == 0) {
						g.DrawLine(_gridPen, 0, yoffset + 1, fullRect.Right, yoffset + 1);
					}
				}

				// vertical grid lines
				int xindex = 0;
				for (float xoffset = 0; xoffset < fullRect.Width; ++xindex, xoffset += xGapInMm) {
					g.DrawLine(_gridPen, xoffset, 0, xoffset, fullRect.Bottom);
					if (xindex % 5 == 0) {
						g.DrawLine(_gridPen, xoffset + 1, 0, xoffset + 1, fullRect.Bottom);
					}
				}
			}
			finally {
				g.Restore(gstate);
			}
		}

		/// <summary>
		/// 绘制前景
		/// </summary>
		void DrawForeground(PaintEventArgs e)
		{
			if (_ecgData == null || _ecgData.Length == 0) {
				return;
			}

			Graphics g = e.Graphics;
			Rectangle fullRect = e.ClipRectangle;

			GraphicsState gstate = g.Save();
			try {
				// set up
				g.PixelOffsetMode = PixelOffsetMode.HighQuality;
				g.SmoothingMode = SmoothingMode.AntiAlias;

				// draw dots
				int countPts = _ecgData.Length;
				Point firstPt = _ecgData[0];
				for (int i = 0; i < countPts; ++i) {
					var nextPt = i+1 < countPts ? _ecgData[i+1] : Point.Empty;
					if (!nextPt.IsEmpty) {
						var pt = _ecgData[i];
						g.DrawLine(_linePen, pt.X + _timerIteration, pt.Y, nextPt.X + _timerIteration, nextPt.Y);
					}

					if (firstPt.X + _timerIteration > fullRect.Width) {
						_timerIteration = 0;
					}
				}

				// remark text
				SizeF textSize = g.MeasureString(REMARK, _remarkFont);
				RectangleF textRect = new RectangleF(
					fullRect.Right - textSize.Width - REMARK_PAD, fullRect.Bottom - textSize.Height - REMARK_PAD/2,
					textSize.Width, textSize.Height
				);
				g.DrawString(REMARK, _remarkFont, _textBrush, textRect);
			}
			finally {
				g.Restore(gstate);
			}
		}

		readonly Brush _backBrush = new SolidBrush(Color.FromArgb(255, 246, 247, 233));
		readonly Pen _gridPen = new Pen(Color.FromArgb(255, 224, 206, 194));

		readonly Pen _linePen = new Pen(Color.FromArgb(255, 39, 25, 24));
		readonly Brush _textBrush = new SolidBrush(Color.FromArgb(255, 39, 25, 24));
		readonly Font _remarkFont = new Font("Tahoma", 9);

		const string REMARK = "Grid intervals: 0.2 sec, 0.5 mV (ECG)";
		const float REMARK_PAD = 10;

		/// <summary>
		/// 心电图数据
		/// </summary>
		public Point[] EcgData
		{
			get { return _ecgData; }
			set { _ecgData = value; }
		}
		Point[] _ecgData = {};

		/// <summary>
		/// 计时器是否可用
		/// </summary>
		public bool TimerEnabled
		{
			get { return _timer.Enabled;  }
			set { _timer.Enabled = value; }
		}
	}
}
