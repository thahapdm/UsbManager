namespace UsbManagerDemo
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose (bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ()
		{
            this.PnlLeft = new System.Windows.Forms.Panel();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.BtnHome = new System.Windows.Forms.Button();
            this.txtLeftPanel = new System.Windows.Forms.TextBox();
            this.txtRightPanel = new System.Windows.Forms.TextBox();
            this.btnLeftLoad = new System.Windows.Forms.Button();
            this.BtnRightLoad = new System.Windows.Forms.Button();
            this.btnLeftrefresh = new System.Windows.Forms.Button();
            this.btnRightRefresh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PnlLeft
            // 
            this.PnlLeft.AutoScroll = true;
            this.PnlLeft.Location = new System.Drawing.Point(35, 110);
            this.PnlLeft.Name = "PnlLeft";
            this.PnlLeft.Size = new System.Drawing.Size(505, 414);
            this.PnlLeft.TabIndex = 1;
            // 
            // pnlRight
            // 
            this.pnlRight.AutoScroll = true;
            this.pnlRight.Location = new System.Drawing.Point(587, 110);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(482, 414);
            this.pnlRight.TabIndex = 2;
            // 
            // BtnHome
            // 
            this.BtnHome.Location = new System.Drawing.Point(35, 33);
            this.BtnHome.Name = "BtnHome";
            this.BtnHome.Size = new System.Drawing.Size(75, 23);
            this.BtnHome.TabIndex = 3;
            this.BtnHome.Text = "Home";
            this.BtnHome.UseVisualStyleBackColor = true;
            this.BtnHome.Click += new System.EventHandler(this.BtnHome_Click);
            // 
            // txtLeftPanel
            // 
            this.txtLeftPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLeftPanel.Location = new System.Drawing.Point(35, 84);
            this.txtLeftPanel.Name = "txtLeftPanel";
            this.txtLeftPanel.Size = new System.Drawing.Size(505, 22);
            this.txtLeftPanel.TabIndex = 4;
            // 
            // txtRightPanel
            // 
            this.txtRightPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRightPanel.Location = new System.Drawing.Point(585, 84);
            this.txtRightPanel.Name = "txtRightPanel";
            this.txtRightPanel.Size = new System.Drawing.Size(484, 22);
            this.txtRightPanel.TabIndex = 5;
            // 
            // btnLeftLoad
            // 
            this.btnLeftLoad.Location = new System.Drawing.Point(35, 55);
            this.btnLeftLoad.Name = "btnLeftLoad";
            this.btnLeftLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLeftLoad.TabIndex = 6;
            this.btnLeftLoad.Text = "Left Load";
            this.btnLeftLoad.UseVisualStyleBackColor = true;
            this.btnLeftLoad.Click += new System.EventHandler(this.btnLeftLoad_Click);
            // 
            // BtnRightLoad
            // 
            this.BtnRightLoad.Location = new System.Drawing.Point(994, 55);
            this.BtnRightLoad.Name = "BtnRightLoad";
            this.BtnRightLoad.Size = new System.Drawing.Size(75, 23);
            this.BtnRightLoad.TabIndex = 7;
            this.BtnRightLoad.Text = "Right Load";
            this.BtnRightLoad.UseVisualStyleBackColor = true;
            this.BtnRightLoad.Click += new System.EventHandler(this.BtnRightLoad_Click);
            // 
            // btnLeftrefresh
            // 
            this.btnLeftrefresh.Location = new System.Drawing.Point(116, 55);
            this.btnLeftrefresh.Name = "btnLeftrefresh";
            this.btnLeftrefresh.Size = new System.Drawing.Size(75, 23);
            this.btnLeftrefresh.TabIndex = 8;
            this.btnLeftrefresh.Text = "Left Refresh";
            this.btnLeftrefresh.UseVisualStyleBackColor = true;
            this.btnLeftrefresh.Click += new System.EventHandler(this.btnLeftrefresh_Click);
            // 
            // btnRightRefresh
            // 
            this.btnRightRefresh.Location = new System.Drawing.Point(894, 55);
            this.btnRightRefresh.Name = "btnRightRefresh";
            this.btnRightRefresh.Size = new System.Drawing.Size(94, 23);
            this.btnRightRefresh.TabIndex = 9;
            this.btnRightRefresh.Text = "Right Refresh";
            this.btnRightRefresh.UseVisualStyleBackColor = true;
            this.btnRightRefresh.Click += new System.EventHandler(this.btnRightRefresh_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1102, 556);
            this.Controls.Add(this.btnRightRefresh);
            this.Controls.Add(this.btnLeftrefresh);
            this.Controls.Add(this.BtnRightLoad);
            this.Controls.Add(this.btnLeftLoad);
            this.Controls.Add(this.txtRightPanel);
            this.Controls.Add(this.txtLeftPanel);
            this.Controls.Add(this.BtnHome);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.PnlLeft);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "My Files";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Panel PnlLeft;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Button BtnHome;
        private System.Windows.Forms.TextBox txtLeftPanel;
        private System.Windows.Forms.TextBox txtRightPanel;
        private System.Windows.Forms.Button btnLeftLoad;
        private System.Windows.Forms.Button BtnRightLoad;
        private System.Windows.Forms.Button btnLeftrefresh;
        private System.Windows.Forms.Button btnRightRefresh;
	}
}