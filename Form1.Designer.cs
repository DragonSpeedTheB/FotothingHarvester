namespace Fotothing
{
    partial class FotothingHarvester
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GoButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.UserTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PwdTxt = new System.Windows.Forms.TextBox();
            this.TargetFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.StatusBox = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // GoButton
            // 
            this.GoButton.Location = new System.Drawing.Point(68, 296);
            this.GoButton.Name = "GoButton";
            this.GoButton.Size = new System.Drawing.Size(184, 46);
            this.GoButton.TabIndex = 0;
            this.GoButton.Text = "Grab Photos";
            this.GoButton.UseVisualStyleBackColor = true;
            this.GoButton.Click += new System.EventHandler(this.GoButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(77, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Username";
            // 
            // UserTxt
            // 
            this.UserTxt.Location = new System.Drawing.Point(204, 65);
            this.UserTxt.Name = "UserTxt";
            this.UserTxt.Size = new System.Drawing.Size(200, 39);
            this.UserTxt.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(77, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 32);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // PwdTxt
            // 
            this.PwdTxt.Location = new System.Drawing.Point(204, 131);
            this.PwdTxt.Name = "PwdTxt";
            this.PwdTxt.Size = new System.Drawing.Size(200, 39);
            this.PwdTxt.TabIndex = 4;
            this.PwdTxt.UseSystemPasswordChar = true;
            // 
            // TargetFolder
            // 
            this.TargetFolder.Location = new System.Drawing.Point(204, 202);
            this.TargetFolder.Name = "TargetFolder";
            this.TargetFolder.ReadOnly = true;
            this.TargetFolder.Size = new System.Drawing.Size(445, 39);
            this.TargetFolder.TabIndex = 6;
            this.TargetFolder.TabStop = false;
            this.TargetFolder.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TargetFolder_MouseDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(77, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 32);
            this.label3.TabIndex = 5;
            this.label3.Text = "Folder";
            // 
            // StatusBox
            // 
            this.StatusBox.Location = new System.Drawing.Point(258, 278);
            this.StatusBox.Multiline = true;
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.StatusBox.Size = new System.Drawing.Size(749, 284);
            this.StatusBox.TabIndex = 7;
            this.StatusBox.TabStop = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(258, 558);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(749, 22);
            this.progressBar1.TabIndex = 8;
            // 
            // FotothingHarvester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 634);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.StatusBox);
            this.Controls.Add(this.TargetFolder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PwdTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.UserTxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GoButton);
            this.Name = "FotothingHarvester";
            this.Text = "Harvest Your Fotothing Photos";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button GoButton;
        private Label label1;
        private TextBox UserTxt;
        private Label label2;
        private TextBox PwdTxt;
        private TextBox TargetFolder;
        private Label label3;
        private TextBox StatusBox;
        private ProgressBar progressBar1;
    }
}