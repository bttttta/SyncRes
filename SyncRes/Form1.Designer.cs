using System;

namespace SyncRes {
	partial class Form1 {
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent() {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cmMusic = new System.Windows.Forms.ComboBox();
            this.RKDLButton = new System.Windows.Forms.Button();
            this.cbMulti = new System.Windows.Forms.CheckBox();
            this.cbTecpnd = new System.Windows.Forms.CheckBox();
            this.cbPandora = new System.Windows.Forms.CheckBox();
            this.cbTechnical = new System.Windows.Forms.CheckBox();
            this.cbAdvanced = new System.Windows.Forms.CheckBox();
            this.cbNormal = new System.Windows.Forms.CheckBox();
            this.cbDetail = new System.Windows.Forms.CheckBox();
            this.cbPerson = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.userIDBox = new System.Windows.Forms.TextBox();
            this.DLButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.statusLabel = new System.Windows.Forms.Label();
            this.urlTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cmMusic);
            this.splitContainer1.Panel1.Controls.Add(this.RKDLButton);
            this.splitContainer1.Panel1.Controls.Add(this.cbMulti);
            this.splitContainer1.Panel1.Controls.Add(this.cbTecpnd);
            this.splitContainer1.Panel1.Controls.Add(this.cbPandora);
            this.splitContainer1.Panel1.Controls.Add(this.cbTechnical);
            this.splitContainer1.Panel1.Controls.Add(this.cbAdvanced);
            this.splitContainer1.Panel1.Controls.Add(this.cbNormal);
            this.splitContainer1.Panel1.Controls.Add(this.cbDetail);
            this.splitContainer1.Panel1.Controls.Add(this.cbPerson);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.userIDBox);
            this.splitContainer1.Panel1.Controls.Add(this.DLButton);
            this.splitContainer1.Panel1.Controls.Add(this.progressBar);
            this.splitContainer1.Panel1.Controls.Add(this.statusLabel);
            this.splitContainer1.Panel1.Controls.Add(this.urlTextBox);
            this.splitContainer1.Panel1MinSize = 97;
            this.splitContainer1.Size = new System.Drawing.Size(684, 661);
            this.splitContainer1.SplitterDistance = 97;
            this.splitContainer1.TabIndex = 99;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.SplitContainer1_SplitterMoved);
            // 
            // cmMusic
            // 
            this.cmMusic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmMusic.FormattingEnabled = true;
            this.cmMusic.Items.AddRange(new object[] {
            "全譜面",
            "非削除",
            "レート"});
            this.cmMusic.Location = new System.Drawing.Point(604, 34);
            this.cmMusic.Name = "cmMusic";
            this.cmMusic.Size = new System.Drawing.Size(67, 20);
            this.cmMusic.TabIndex = 10;
            // 
            // RKDLButton
            // 
            this.RKDLButton.Enabled = false;
            this.RKDLButton.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.RKDLButton.Location = new System.Drawing.Point(591, 55);
            this.RKDLButton.Name = "RKDLButton";
            this.RKDLButton.Size = new System.Drawing.Size(81, 20);
            this.RKDLButton.TabIndex = 11;
            this.RKDLButton.Text = "RunkingDL!";
            this.RKDLButton.UseVisualStyleBackColor = true;
            this.RKDLButton.Click += new System.EventHandler(this.RKDLButton_Click);
            // 
            // cbMulti
            // 
            this.cbMulti.AutoSize = true;
            this.cbMulti.Location = new System.Drawing.Point(527, 78);
            this.cbMulti.Name = "cbMulti";
            this.cbMulti.Size = new System.Drawing.Size(33, 16);
            this.cbMulti.TabIndex = 9;
            this.cbMulti.Text = "M";
            this.cbMulti.UseVisualStyleBackColor = true;
            // 
            // cbTecpnd
            // 
            this.cbTecpnd.AutoSize = true;
            this.cbTecpnd.Location = new System.Drawing.Point(467, 78);
            this.cbTecpnd.Name = "cbTecpnd";
            this.cbTecpnd.Size = new System.Drawing.Size(38, 16);
            this.cbTecpnd.TabIndex = 8;
            this.cbTecpnd.Text = "TP";
            this.cbTecpnd.UseVisualStyleBackColor = true;
            // 
            // cbPandora
            // 
            this.cbPandora.AutoSize = true;
            this.cbPandora.Checked = true;
            this.cbPandora.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPandora.Location = new System.Drawing.Point(556, 59);
            this.cbPandora.Name = "cbPandora";
            this.cbPandora.Size = new System.Drawing.Size(31, 16);
            this.cbPandora.TabIndex = 7;
            this.cbPandora.Text = "P";
            this.cbPandora.UseVisualStyleBackColor = true;
            // 
            // cbTechnical
            // 
            this.cbTechnical.AutoSize = true;
            this.cbTechnical.Checked = true;
            this.cbTechnical.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTechnical.Location = new System.Drawing.Point(527, 59);
            this.cbTechnical.Name = "cbTechnical";
            this.cbTechnical.Size = new System.Drawing.Size(31, 16);
            this.cbTechnical.TabIndex = 6;
            this.cbTechnical.Text = "T";
            this.cbTechnical.UseVisualStyleBackColor = true;
            // 
            // cbAdvanced
            // 
            this.cbAdvanced.AutoSize = true;
            this.cbAdvanced.Location = new System.Drawing.Point(497, 59);
            this.cbAdvanced.Name = "cbAdvanced";
            this.cbAdvanced.Size = new System.Drawing.Size(32, 16);
            this.cbAdvanced.TabIndex = 5;
            this.cbAdvanced.Text = "A";
            this.cbAdvanced.UseVisualStyleBackColor = true;
            // 
            // cbNormal
            // 
            this.cbNormal.AutoSize = true;
            this.cbNormal.Location = new System.Drawing.Point(467, 59);
            this.cbNormal.Name = "cbNormal";
            this.cbNormal.Size = new System.Drawing.Size(32, 16);
            this.cbNormal.TabIndex = 4;
            this.cbNormal.Text = "N";
            this.cbNormal.UseVisualStyleBackColor = true;
            // 
            // cbDetail
            // 
            this.cbDetail.AutoSize = true;
            this.cbDetail.Checked = true;
            this.cbDetail.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDetail.Location = new System.Drawing.Point(527, 38);
            this.cbDetail.Name = "cbDetail";
            this.cbDetail.Size = new System.Drawing.Size(71, 16);
            this.cbDetail.TabIndex = 3;
            this.cbDetail.Text = "Summary";
            this.cbDetail.UseVisualStyleBackColor = true;
            // 
            // cbPerson
            // 
            this.cbPerson.AutoSize = true;
            this.cbPerson.Location = new System.Drawing.Point(467, 38);
            this.cbPerson.Name = "cbPerson";
            this.cbPerson.Size = new System.Drawing.Size(56, 16);
            this.cbPerson.TabIndex = 2;
            this.cbPerson.Text = "Player";
            this.cbPerson.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(110, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 103;
            this.label1.Text = "UserID ->";
            // 
            // userIDBox
            // 
            this.userIDBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userIDBox.Location = new System.Drawing.Point(184, 38);
            this.userIDBox.Multiline = true;
            this.userIDBox.Name = "userIDBox";
            this.userIDBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.userIDBox.Size = new System.Drawing.Size(277, 56);
            this.userIDBox.TabIndex = 1;
            // 
            // DLButton
            // 
            this.DLButton.Enabled = false;
            this.DLButton.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DLButton.Location = new System.Drawing.Point(591, 74);
            this.DLButton.Name = "DLButton";
            this.DLButton.Size = new System.Drawing.Size(81, 20);
            this.DLButton.TabIndex = 12;
            this.DLButton.Text = "Download!";
            this.DLButton.UseVisualStyleBackColor = true;
            this.DLButton.Click += new System.EventHandler(this.DLButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Enabled = false;
            this.progressBar.Location = new System.Drawing.Point(12, 53);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(165, 23);
            this.progressBar.TabIndex = 104;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.statusLabel.ForeColor = System.Drawing.Color.DimGray;
            this.statusLabel.Location = new System.Drawing.Point(12, 35);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(50, 15);
            this.statusLabel.TabIndex = 102;
            this.statusLabel.Text = "Unlogin";
            // 
            // urlTextBox
            // 
            this.urlTextBox.Location = new System.Drawing.Point(13, 13);
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.Size = new System.Drawing.Size(659, 19);
            this.urlTextBox.TabIndex = 101;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 661);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "SyncRes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TextBox urlTextBox;
		private System.Windows.Forms.Label statusLabel;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Button DLButton;
		private System.Windows.Forms.TextBox userIDBox;
		private System.Windows.Forms.CheckBox cbPandora;
		private System.Windows.Forms.CheckBox cbTechnical;
		private System.Windows.Forms.CheckBox cbAdvanced;
		private System.Windows.Forms.CheckBox cbNormal;
		private System.Windows.Forms.CheckBox cbDetail;
		private System.Windows.Forms.CheckBox cbPerson;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button RKDLButton;
		private System.Windows.Forms.CheckBox cbMulti;
		private System.Windows.Forms.CheckBox cbTecpnd;
        private System.Windows.Forms.ComboBox cmMusic;
    }
}

