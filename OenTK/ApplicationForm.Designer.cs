namespace PLGAnalyzer
{
	partial class ApplicationForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private OpenTK.GLControl glControl1;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
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
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationForm));
            this.glControl1 = new OpenTK.GLControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openPLGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.perspectiveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.perspectiveToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.orthographicToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.shadingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smoothToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wireframeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backfaceCullingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.animateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bGColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cyanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.whiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sceneParametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fpsLabel = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.showLightSource = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // glControl1
            // 
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.Cursor = System.Windows.Forms.Cursors.NoMove2D;
            this.glControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControl1.Location = new System.Drawing.Point(0, 24);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(1201, 479);
            this.glControl1.TabIndex = 0;
            this.glControl1.VSync = false;
            this.glControl1.Load += new System.EventHandler(this.glControl1_Load);
            this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
            this.glControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseMove);
            this.glControl1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.glControl1_KeyUp);
            this.glControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseDown);
            this.glControl1.Resize += new System.EventHandler(this.glControl1_Resize);
            this.glControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseUp);
            this.glControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glControl1_KeyDown);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.shaderToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1201, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openPLGToolStripMenuItem,
            this.resetToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openPLGToolStripMenuItem
            // 
            this.openPLGToolStripMenuItem.Image = global::PLGAnalyzer.Properties.Resources.blue_folder_export;
            this.openPLGToolStripMenuItem.Name = "openPLGToolStripMenuItem";
            this.openPLGToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openPLGToolStripMenuItem.Text = "&Open PLG...";
            this.openPLGToolStripMenuItem.Click += new System.EventHandler(this.openPLGToolStripMenuItem_Click);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Image = global::PLGAnalyzer.Properties.Resources.arrow_circle_315_left;
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.resetToolStripMenuItem.Text = "&Reset (Q)";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::PLGAnalyzer.Properties.Resources.cross;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "&Exit (Esc)";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // shaderToolStripMenuItem
            // 
            this.shaderToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.perspectiveToolStripMenuItem1,
            this.shadingToolStripMenuItem,
            this.toolStripMenuItem3,
            this.wireframeToolStripMenuItem,
            this.backfaceCullingToolStripMenuItem,
            this.toolStripMenuItem2,
            this.animateToolStripMenuItem});
            this.shaderToolStripMenuItem.Image = global::PLGAnalyzer.Properties.Resources.wrench_screwdriver;
            this.shaderToolStripMenuItem.Name = "shaderToolStripMenuItem";
            this.shaderToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.shaderToolStripMenuItem.Text = "&Options";
            // 
            // perspectiveToolStripMenuItem1
            // 
            this.perspectiveToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.perspectiveToolStripMenuItem2,
            this.orthographicToolStripMenuItem1});
            this.perspectiveToolStripMenuItem1.Image = global::PLGAnalyzer.Properties.Resources.eye;
            this.perspectiveToolStripMenuItem1.Name = "perspectiveToolStripMenuItem1";
            this.perspectiveToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.perspectiveToolStripMenuItem1.Text = "Pro&jection Type";
            // 
            // perspectiveToolStripMenuItem2
            // 
            this.perspectiveToolStripMenuItem2.Name = "perspectiveToolStripMenuItem2";
            this.perspectiveToolStripMenuItem2.Size = new System.Drawing.Size(137, 22);
            this.perspectiveToolStripMenuItem2.Text = "Per&spective";
            this.perspectiveToolStripMenuItem2.Click += new System.EventHandler(this.perspectiveToolStripMenuItem2_Click);
            // 
            // orthographicToolStripMenuItem1
            // 
            this.orthographicToolStripMenuItem1.Enabled = false;
            this.orthographicToolStripMenuItem1.Name = "orthographicToolStripMenuItem1";
            this.orthographicToolStripMenuItem1.Size = new System.Drawing.Size(137, 22);
            this.orthographicToolStripMenuItem1.Text = "Ortho&graphic";
            this.orthographicToolStripMenuItem1.Click += new System.EventHandler(this.orthographicToolStripMenuItem1_Click);
            // 
            // shadingToolStripMenuItem
            // 
            this.shadingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smoothToolStripMenuItem,
            this.flatToolStripMenuItem});
            this.shadingToolStripMenuItem.Image = global::PLGAnalyzer.Properties.Resources.Field;
            this.shadingToolStripMenuItem.Name = "shadingToolStripMenuItem";
            this.shadingToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.shadingToolStripMenuItem.Text = "Shading";
            // 
            // smoothToolStripMenuItem
            // 
            this.smoothToolStripMenuItem.Name = "smoothToolStripMenuItem";
            this.smoothToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.smoothToolStripMenuItem.Text = "Smooth";
            this.smoothToolStripMenuItem.Click += new System.EventHandler(this.smoothToolStripMenuItem_Click);
            // 
            // flatToolStripMenuItem
            // 
            this.flatToolStripMenuItem.Name = "flatToolStripMenuItem";
            this.flatToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.flatToolStripMenuItem.Text = "Flat";
            this.flatToolStripMenuItem.Click += new System.EventHandler(this.flatToolStripMenuItem_Click);
            // 
            // wireframeToolStripMenuItem
            // 
            this.wireframeToolStripMenuItem.CheckOnClick = true;
            this.wireframeToolStripMenuItem.Image = global::PLGAnalyzer.Properties.Resources.network;
            this.wireframeToolStripMenuItem.Name = "wireframeToolStripMenuItem";
            this.wireframeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.wireframeToolStripMenuItem.Text = "Edges Only (E)";
            this.wireframeToolStripMenuItem.Click += new System.EventHandler(this.wireframeToolStripMenuItem_Click);
            // 
            // backfaceCullingToolStripMenuItem
            // 
            this.backfaceCullingToolStripMenuItem.CheckOnClick = true;
            this.backfaceCullingToolStripMenuItem.Image = global::PLGAnalyzer.Properties.Resources.border_horizontal_all;
            this.backfaceCullingToolStripMenuItem.Name = "backfaceCullingToolStripMenuItem";
            this.backfaceCullingToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.backfaceCullingToolStripMenuItem.Text = "Backface Culling";
            this.backfaceCullingToolStripMenuItem.Click += new System.EventHandler(this.backfaceCullingToolStripMenuItem_Click);
            // 
            // animateToolStripMenuItem
            // 
            this.animateToolStripMenuItem.CheckOnClick = true;
            this.animateToolStripMenuItem.Image = global::PLGAnalyzer.Properties.Resources.clapperboard;
            this.animateToolStripMenuItem.Name = "animateToolStripMenuItem";
            this.animateToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.animateToolStripMenuItem.Text = "Animate!";
            this.animateToolStripMenuItem.Click += new System.EventHandler(this.animateToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bGColorToolStripMenuItem,
            this.sceneParametersToolStripMenuItem,
            this.toolStripMenuItem4,
            this.showLightSource});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(105, 20);
            this.toolsToolStripMenuItem.Text = "Other Parameters";
            // 
            // bGColorToolStripMenuItem
            // 
            this.bGColorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.blueToolStripMenuItem,
            this.cyanToolStripMenuItem,
            this.blackToolStripMenuItem,
            this.whiteToolStripMenuItem});
            this.bGColorToolStripMenuItem.Image = global::PLGAnalyzer.Properties.Resources.color;
            this.bGColorToolStripMenuItem.Name = "bGColorToolStripMenuItem";
            this.bGColorToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.bGColorToolStripMenuItem.Text = "Background &Color";
            this.bGColorToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // blueToolStripMenuItem
            // 
            this.blueToolStripMenuItem.Image = global::PLGAnalyzer.Properties.Resources.blue;
            this.blueToolStripMenuItem.Name = "blueToolStripMenuItem";
            this.blueToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.blueToolStripMenuItem.Text = "Blue";
            this.blueToolStripMenuItem.Click += new System.EventHandler(this.blueToolStripMenuItem_Click);
            // 
            // cyanToolStripMenuItem
            // 
            this.cyanToolStripMenuItem.Image = global::PLGAnalyzer.Properties.Resources.cyan;
            this.cyanToolStripMenuItem.Name = "cyanToolStripMenuItem";
            this.cyanToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.cyanToolStripMenuItem.Text = "Cyan";
            this.cyanToolStripMenuItem.Click += new System.EventHandler(this.cyanToolStripMenuItem_Click);
            // 
            // blackToolStripMenuItem
            // 
            this.blackToolStripMenuItem.Image = global::PLGAnalyzer.Properties.Resources.black;
            this.blackToolStripMenuItem.Name = "blackToolStripMenuItem";
            this.blackToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.blackToolStripMenuItem.Text = "Black";
            this.blackToolStripMenuItem.Click += new System.EventHandler(this.blackToolStripMenuItem_Click);
            // 
            // whiteToolStripMenuItem
            // 
            this.whiteToolStripMenuItem.Image = global::PLGAnalyzer.Properties.Resources.white;
            this.whiteToolStripMenuItem.Name = "whiteToolStripMenuItem";
            this.whiteToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.whiteToolStripMenuItem.Text = "White";
            this.whiteToolStripMenuItem.Click += new System.EventHandler(this.whiteToolStripMenuItem_Click);
            // 
            // sceneParametersToolStripMenuItem
            // 
            this.sceneParametersToolStripMenuItem.Image = global::PLGAnalyzer.Properties.Resources.application_detail;
            this.sceneParametersToolStripMenuItem.Name = "sceneParametersToolStripMenuItem";
            this.sceneParametersToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.sceneParametersToolStripMenuItem.Text = "Scene Parameters";
            this.sceneParametersToolStripMenuItem.Click += new System.EventHandler(this.sceneParametersToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = global::PLGAnalyzer.Properties.Resources.information;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // fpsLabel
            // 
            this.fpsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fpsLabel.BackColor = System.Drawing.Color.Transparent;
            this.fpsLabel.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fpsLabel.ForeColor = System.Drawing.Color.Red;
            this.fpsLabel.Location = new System.Drawing.Point(1135, 28);
            this.fpsLabel.Name = "fpsLabel";
            this.fpsLabel.Size = new System.Drawing.Size(60, 33);
            this.fpsLabel.TabIndex = 2;
            this.fpsLabel.Text = "00";
            this.fpsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusMessage});
            this.statusStrip1.Location = new System.Drawing.Point(0, 481);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(1201, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatusMessage
            // 
            this.lblStatusMessage.Name = "lblStatusMessage";
            this.lblStatusMessage.Size = new System.Drawing.Size(38, 17);
            this.lblStatusMessage.Text = "Ready";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 6);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(149, 6);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(159, 6);
            // 
            // showLightSource
            // 
            this.showLightSource.CheckOnClick = true;
            this.showLightSource.Name = "showLightSource";
            this.showLightSource.Size = new System.Drawing.Size(162, 22);
            this.showLightSource.Text = "Show Light Source";
            // 
            // ApplicationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1201, 503);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.fpsLabel);
            this.Controls.Add(this.glControl1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ApplicationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PLG Analyzer - Version 1.0 (10.3.2011)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openPLGToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.Label fpsLabel;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusMessage;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shaderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem perspectiveToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem perspectiveToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem orthographicToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem shadingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smoothToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wireframeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backfaceCullingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bGColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cyanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem whiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem animateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sceneParametersToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem showLightSource;
	}
}

