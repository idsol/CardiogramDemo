namespace CardiogramDemo
{
	partial class FrmMain
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this._cardiogram = new CardiogramDemo.CardiogramControl();
			this.SuspendLayout();
			// 
			// _cardiogram
			// 
			this._cardiogram.Dock = System.Windows.Forms.DockStyle.Fill;
			this._cardiogram.EcgData = new System.Drawing.Point[0];
			this._cardiogram.Location = new System.Drawing.Point(0, 0);
			this._cardiogram.Name = "_cardiogram";
			this._cardiogram.Size = new System.Drawing.Size(638, 396);
			this._cardiogram.TabIndex = 0;
			this._cardiogram.Text = "_cardiogram";
			this._cardiogram.TimerEnabled = false;
			// 
			// FrmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(638, 396);
			this.Controls.Add(this._cardiogram);
			this.Name = "FrmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Cardiogram Demo";
			this.ResumeLayout(false);

		}

		#endregion

		private CardiogramControl _cardiogram;
	}
}

