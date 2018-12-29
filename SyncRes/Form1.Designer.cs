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
			this.cbFast = new System.Windows.Forms.CheckBox();
			this.cbDeleted = new System.Windows.Forms.CheckBox();
			this.RKDLButton = new System.Windows.Forms.Button();
			this.cbMulti = new System.Windows.Forms.CheckBox();
			this.cbTecpnd = new System.Windows.Forms.CheckBox();
			this.MusicIDBox = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
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
			((System.ComponentModel.ISupportInitialize)(this.MusicIDBox)).BeginInit();
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
			this.splitContainer1.Panel1.Controls.Add(this.cbFast);
			this.splitContainer1.Panel1.Controls.Add(this.cbDeleted);
			this.splitContainer1.Panel1.Controls.Add(this.RKDLButton);
			this.splitContainer1.Panel1.Controls.Add(this.cbMulti);
			this.splitContainer1.Panel1.Controls.Add(this.cbTecpnd);
			this.splitContainer1.Panel1.Controls.Add(this.MusicIDBox);
			this.splitContainer1.Panel1.Controls.Add(this.label2);
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
			this.splitContainer1.Size = new System.Drawing.Size(684, 661);
			this.splitContainer1.SplitterDistance = 118;
			this.splitContainer1.TabIndex = 1;
			// 
			// cbFast
			// 
			this.cbFast.AutoSize = true;
			this.cbFast.Location = new System.Drawing.Point(545, 98);
			this.cbFast.Name = "cbFast";
			this.cbFast.Size = new System.Drawing.Size(47, 16);
			this.cbFast.TabIndex = 18;
			this.cbFast.Text = "Fast";
			this.cbFast.UseVisualStyleBackColor = true;
			// 
			// cbDeleted
			// 
			this.cbDeleted.AutoSize = true;
			this.cbDeleted.Location = new System.Drawing.Point(467, 98);
			this.cbDeleted.Name = "cbDeleted";
			this.cbDeleted.Size = new System.Drawing.Size(84, 16);
			this.cbDeleted.TabIndex = 17;
			this.cbDeleted.Text = "サヨナラ除外";
			this.cbDeleted.UseVisualStyleBackColor = true;
			// 
			// RKDLButton
			// 
			this.RKDLButton.Enabled = false;
			this.RKDLButton.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.RKDLButton.Location = new System.Drawing.Point(591, 74);
			this.RKDLButton.Name = "RKDLButton";
			this.RKDLButton.Size = new System.Drawing.Size(81, 20);
			this.RKDLButton.TabIndex = 16;
			this.RKDLButton.Text = "RunkingDL!";
			this.RKDLButton.UseVisualStyleBackColor = true;
			this.RKDLButton.Click += new System.EventHandler(this.RKDLButton_Click);
			// 
			// cbMulti
			// 
			this.cbMulti.AutoSize = true;
			this.cbMulti.Location = new System.Drawing.Point(505, 78);
			this.cbMulti.Name = "cbMulti";
			this.cbMulti.Size = new System.Drawing.Size(33, 16);
			this.cbMulti.TabIndex = 15;
			this.cbMulti.Text = "M";
			this.cbMulti.UseVisualStyleBackColor = true;
			// 
			// cbTecpnd
			// 
			this.cbTecpnd.AutoSize = true;
			this.cbTecpnd.Location = new System.Drawing.Point(467, 78);
			this.cbTecpnd.Name = "cbTecpnd";
			this.cbTecpnd.Size = new System.Drawing.Size(38, 16);
			this.cbTecpnd.TabIndex = 14;
			this.cbTecpnd.Text = "TP";
			this.cbTecpnd.UseVisualStyleBackColor = true;
			// 
			// MusicIDBox
			// 
			this.MusicIDBox.Location = new System.Drawing.Point(591, 53);
			this.MusicIDBox.Maximum = new decimal(new int[] {
            4095,
            0,
            0,
            0});
			this.MusicIDBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.MusicIDBox.Name = "MusicIDBox";
			this.MusicIDBox.Size = new System.Drawing.Size(81, 19);
			this.MusicIDBox.TabIndex = 1;
			this.MusicIDBox.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label2.Location = new System.Drawing.Point(589, 38);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(67, 12);
			this.label2.TabIndex = 12;
			this.label2.Text = "MusicIDMax";
			// 
			// cbPandora
			// 
			this.cbPandora.AutoSize = true;
			this.cbPandora.Checked = true;
			this.cbPandora.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbPandora.Location = new System.Drawing.Point(556, 59);
			this.cbPandora.Name = "cbPandora";
			this.cbPandora.Size = new System.Drawing.Size(31, 16);
			this.cbPandora.TabIndex = 11;
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
			this.cbTechnical.TabIndex = 10;
			this.cbTechnical.Text = "T";
			this.cbTechnical.UseVisualStyleBackColor = true;
			// 
			// cbAdvanced
			// 
			this.cbAdvanced.AutoSize = true;
			this.cbAdvanced.Location = new System.Drawing.Point(497, 59);
			this.cbAdvanced.Name = "cbAdvanced";
			this.cbAdvanced.Size = new System.Drawing.Size(32, 16);
			this.cbAdvanced.TabIndex = 9;
			this.cbAdvanced.Text = "A";
			this.cbAdvanced.UseVisualStyleBackColor = true;
			// 
			// cbNormal
			// 
			this.cbNormal.AutoSize = true;
			this.cbNormal.Location = new System.Drawing.Point(467, 59);
			this.cbNormal.Name = "cbNormal";
			this.cbNormal.Size = new System.Drawing.Size(32, 16);
			this.cbNormal.TabIndex = 8;
			this.cbNormal.Text = "N";
			this.cbNormal.UseVisualStyleBackColor = true;
			// 
			// cbDetail
			// 
			this.cbDetail.AutoSize = true;
			this.cbDetail.Checked = true;
			this.cbDetail.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbDetail.Location = new System.Drawing.Point(529, 38);
			this.cbDetail.Name = "cbDetail";
			this.cbDetail.Size = new System.Drawing.Size(54, 16);
			this.cbDetail.TabIndex = 7;
			this.cbDetail.Text = "Detail";
			this.cbDetail.UseVisualStyleBackColor = true;
			// 
			// cbPerson
			// 
			this.cbPerson.AutoSize = true;
			this.cbPerson.Location = new System.Drawing.Point(467, 38);
			this.cbPerson.Name = "cbPerson";
			this.cbPerson.Size = new System.Drawing.Size(56, 16);
			this.cbPerson.TabIndex = 6;
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
			this.label1.TabIndex = 5;
			this.label1.Text = "UserID ->";
			// 
			// userIDBox
			// 
			this.userIDBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.userIDBox.Location = new System.Drawing.Point(184, 38);
			this.userIDBox.Multiline = true;
			this.userIDBox.Name = "userIDBox";
			this.userIDBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.userIDBox.Size = new System.Drawing.Size(277, 77);
			this.userIDBox.TabIndex = 4;
			// 
			// DLButton
			// 
			this.DLButton.Enabled = false;
			this.DLButton.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.DLButton.Location = new System.Drawing.Point(591, 95);
			this.DLButton.Name = "DLButton";
			this.DLButton.Size = new System.Drawing.Size(81, 20);
			this.DLButton.TabIndex = 3;
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
			this.progressBar.TabIndex = 2;
			// 
			// statusLabel
			// 
			this.statusLabel.AutoSize = true;
			this.statusLabel.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.statusLabel.ForeColor = System.Drawing.Color.DimGray;
			this.statusLabel.Location = new System.Drawing.Point(12, 35);
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(50, 15);
			this.statusLabel.TabIndex = 1;
			this.statusLabel.Text = "Unlogin";
			// 
			// urlTextBox
			// 
			this.urlTextBox.Location = new System.Drawing.Point(13, 13);
			this.urlTextBox.Name = "urlTextBox";
			this.urlTextBox.Size = new System.Drawing.Size(659, 19);
			this.urlTextBox.TabIndex = 0;
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
			((System.ComponentModel.ISupportInitialize)(this.MusicIDBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TextBox urlTextBox;
		private System.Windows.Forms.Label statusLabel;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Button DLButton;
		private System.Windows.Forms.TextBox userIDBox;
		private System.Windows.Forms.NumericUpDown MusicIDBox;
		private System.Windows.Forms.Label label2;
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
		private System.Windows.Forms.CheckBox cbDeleted;
		private System.Windows.Forms.CheckBox cbFast;
	}
}

