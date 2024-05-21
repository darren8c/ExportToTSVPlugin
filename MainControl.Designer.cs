
namespace ExportToTSV
{
	partial class MainControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.exportButton = new System.Windows.Forms.Button();
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.progressBar = new CustomProgressBar();
            progressBar.DisplayStyle = ProgressBarDisplayText.CustomText;
            this.SuspendLayout();
			// 
			// getButton
			// 
			this.exportButton.Location = new System.Drawing.Point(0, 70);
			this.exportButton.Name = "exportButton";
			this.exportButton.Size = new System.Drawing.Size(209, 36);
			this.exportButton.TabIndex = 0;
			this.exportButton.Text = "Export Project";
			this.exportButton.UseVisualStyleBackColor = true;
			this.exportButton.Click += new System.EventHandler(this.ExportScripture);
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel.AutoScroll = true;
			this.tableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.21159F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.78841F));
			this.tableLayoutPanel.Location = new System.Drawing.Point(14, 150);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 1;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.Size = new System.Drawing.Size(656, 129);
			this.tableLayoutPanel.TabIndex = 8;
			// 
			// progressBar
			// 
            this.progressBar.Location = new System.Drawing.Point(313, 70);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(304, 36);
			this.progressBar.TabIndex = 9;
			this.progressBar.Text = "Export Progress";
			this.progressBar.Visible = false;
			this.progressBar.Minimum = 0;
			// 
			// MainControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.exportButton);
			this.Controls.Add(this.tableLayoutPanel);
			this.Name = "MainControl";
			this.Size = new System.Drawing.Size(670, 279);
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private System.Windows.Forms.Button exportButton;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private CustomProgressBar progressBar;
	}
}
