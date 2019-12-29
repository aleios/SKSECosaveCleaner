namespace SKSECosaveCleaner
{
    partial class MainWindow
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
            this.btnClean = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.lblTotalSize = new System.Windows.Forms.Label();
            this.lblPercentSize = new System.Windows.Forms.Label();
            this.lblDeltaSize = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbExcludedActors = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbIgnoreLoadOrder = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClean
            // 
            this.btnClean.Location = new System.Drawing.Point(6, 60);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(75, 23);
            this.btnClean.TabIndex = 0;
            this.btnClean.Text = "Clean";
            this.btnClean.UseVisualStyleBackColor = true;
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(6, 20);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Load Save";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // lblTotalSize
            // 
            this.lblTotalSize.AutoSize = true;
            this.lblTotalSize.Location = new System.Drawing.Point(82, 20);
            this.lblTotalSize.Name = "lblTotalSize";
            this.lblTotalSize.Size = new System.Drawing.Size(30, 13);
            this.lblTotalSize.TabIndex = 2;
            this.lblTotalSize.Text = "0 KB";
            // 
            // lblPercentSize
            // 
            this.lblPercentSize.AutoSize = true;
            this.lblPercentSize.Location = new System.Drawing.Point(82, 45);
            this.lblPercentSize.Name = "lblPercentSize";
            this.lblPercentSize.Size = new System.Drawing.Size(21, 13);
            this.lblPercentSize.TabIndex = 3;
            this.lblPercentSize.Text = "0%";
            // 
            // lblDeltaSize
            // 
            this.lblDeltaSize.AutoSize = true;
            this.lblDeltaSize.Location = new System.Drawing.Point(82, 70);
            this.lblDeltaSize.Name = "lblDeltaSize";
            this.lblDeltaSize.Size = new System.Drawing.Size(13, 13);
            this.lblDeltaSize.TabIndex = 4;
            this.lblDeltaSize.Text = "0";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblDeltaSize);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblPercentSize);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblTotalSize);
            this.groupBox1.Location = new System.Drawing.Point(110, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(131, 100);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File Information";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Section Size:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "% of file:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Delta Size:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnLoad);
            this.groupBox2.Controls.Add(this.btnClean);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(99, 100);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cleaning";
            // 
            // tbExcludedActors
            // 
            this.tbExcludedActors.Location = new System.Drawing.Point(12, 131);
            this.tbExcludedActors.Multiline = true;
            this.tbExcludedActors.Name = "tbExcludedActors";
            this.tbExcludedActors.Size = new System.Drawing.Size(229, 86);
            this.tbExcludedActors.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Excluded Actor FormIDs:";
            // 
            // cbIgnoreLoadOrder
            // 
            this.cbIgnoreLoadOrder.AutoSize = true;
            this.cbIgnoreLoadOrder.Location = new System.Drawing.Point(13, 224);
            this.cbIgnoreLoadOrder.Name = "cbIgnoreLoadOrder";
            this.cbIgnoreLoadOrder.Size = new System.Drawing.Size(112, 17);
            this.cbIgnoreLoadOrder.TabIndex = 9;
            this.cbIgnoreLoadOrder.Text = "Ignore Load Order";
            this.cbIgnoreLoadOrder.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(258, 243);
            this.Controls.Add(this.cbIgnoreLoadOrder);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbExcludedActors);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainWindow";
            this.Text = "SKSE Cosave Cleaner";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClean;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Label lblTotalSize;
        private System.Windows.Forms.Label lblPercentSize;
        private System.Windows.Forms.Label lblDeltaSize;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbExcludedActors;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbIgnoreLoadOrder;
    }
}

