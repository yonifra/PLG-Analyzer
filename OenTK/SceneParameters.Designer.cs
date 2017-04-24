namespace PLGAnalyzer
{
    partial class SceneParameters
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.grpObjNumber = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblFilename = new System.Windows.Forms.Label();
            this.lblVerticesCount = new System.Windows.Forms.Label();
            this.lblPolygonCount = new System.Windows.Forms.Label();
            this.lblObjectsNumber = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // grpObjNumber
            // 
            this.grpObjNumber.AutoSize = true;
            this.grpObjNumber.Location = new System.Drawing.Point(13, 16);
            this.grpObjNumber.Name = "grpObjNumber";
            this.grpObjNumber.Size = new System.Drawing.Size(131, 13);
            this.grpObjNumber.TabIndex = 0;
            this.grpObjNumber.Text = "Number of loaded objects:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Total polygon count:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Loaded filename:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Total vertices count:";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(153, 148);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblFilename
            // 
            this.lblFilename.AutoSize = true;
            this.lblFilename.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblFilename.Location = new System.Drawing.Point(150, 82);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Size = new System.Drawing.Size(30, 13);
            this.lblFilename.TabIndex = 8;
            this.lblFilename.Text = "N/A";
            // 
            // lblVerticesCount
            // 
            this.lblVerticesCount.AutoSize = true;
            this.lblVerticesCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblVerticesCount.Location = new System.Drawing.Point(150, 60);
            this.lblVerticesCount.Name = "lblVerticesCount";
            this.lblVerticesCount.Size = new System.Drawing.Size(14, 13);
            this.lblVerticesCount.TabIndex = 7;
            this.lblVerticesCount.Text = "0";
            // 
            // lblPolygonCount
            // 
            this.lblPolygonCount.AutoSize = true;
            this.lblPolygonCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblPolygonCount.Location = new System.Drawing.Point(150, 38);
            this.lblPolygonCount.Name = "lblPolygonCount";
            this.lblPolygonCount.Size = new System.Drawing.Size(14, 13);
            this.lblPolygonCount.TabIndex = 6;
            this.lblPolygonCount.Text = "0";
            // 
            // lblObjectsNumber
            // 
            this.lblObjectsNumber.AutoSize = true;
            this.lblObjectsNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblObjectsNumber.Location = new System.Drawing.Point(150, 16);
            this.lblObjectsNumber.Name = "lblObjectsNumber";
            this.lblObjectsNumber.Size = new System.Drawing.Size(14, 13);
            this.lblObjectsNumber.TabIndex = 5;
            this.lblObjectsNumber.Text = "0";
            // 
            // SceneParameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 183);
            this.ControlBox = false;
            this.Controls.Add(this.lblFilename);
            this.Controls.Add(this.lblVerticesCount);
            this.Controls.Add(this.lblPolygonCount);
            this.Controls.Add(this.lblObjectsNumber);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grpObjNumber);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SceneParameters";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scene Parameters";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label grpObjNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblFilename;
        private System.Windows.Forms.Label lblVerticesCount;
        private System.Windows.Forms.Label lblPolygonCount;
        private System.Windows.Forms.Label lblObjectsNumber;
    }
}