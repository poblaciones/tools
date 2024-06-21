namespace addressCoder
{
	partial class frmMain
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
			this.components = new System.ComponentModel.Container();
			this.cmdStart = new System.Windows.Forms.Button();
			this.lstView = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.btnPaste = new System.Windows.Forms.Button();
			this.btnCopy = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.txtKey = new System.Windows.Forms.TextBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// cmdStart
			// 
			this.cmdStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdStart.Location = new System.Drawing.Point(650, 24);
			this.cmdStart.Name = "cmdStart";
			this.cmdStart.Size = new System.Drawing.Size(100, 56);
			this.cmdStart.TabIndex = 0;
			this.cmdStart.Text = "¡Empezar!";
			this.cmdStart.UseVisualStyleBackColor = true;
			this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
			// 
			// lstView
			// 
			this.lstView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
			this.lstView.FullRowSelect = true;
			this.lstView.HideSelection = false;
			this.lstView.Location = new System.Drawing.Point(21, 24);
			this.lstView.Name = "lstView";
			this.lstView.Size = new System.Drawing.Size(613, 375);
			this.lstView.TabIndex = 1;
			this.lstView.UseCompatibleStateImageBehavior = false;
			this.lstView.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Dirección";
			this.columnHeader1.Width = 147;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Latitud";
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Longitud";
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Dirección obtenida";
			// 
			// btnPaste
			// 
			this.btnPaste.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPaste.Location = new System.Drawing.Point(650, 259);
			this.btnPaste.Name = "btnPaste";
			this.btnPaste.Size = new System.Drawing.Size(100, 56);
			this.btnPaste.TabIndex = 0;
			this.btnPaste.Text = "Pegar";
			this.btnPaste.UseVisualStyleBackColor = true;
			this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
			// 
			// btnCopy
			// 
			this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCopy.Location = new System.Drawing.Point(650, 343);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(100, 56);
			this.btnCopy.TabIndex = 0;
			this.btnCopy.Text = "Copiar";
			this.btnCopy.UseVisualStyleBackColor = true;
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(652, 193);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Google Key:";
			// 
			// txtKey
			// 
			this.txtKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.txtKey.Location = new System.Drawing.Point(655, 212);
			this.txtKey.Name = "txtKey";
			this.txtKey.Size = new System.Drawing.Size(94, 20);
			this.txtKey.TabIndex = 3;
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// frmMain
			// 
			this.AcceptButton = this.cmdStart;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(766, 420);
			this.Controls.Add(this.txtKey);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lstView);
			this.Controls.Add(this.btnCopy);
			this.Controls.Add(this.btnPaste);
			this.Controls.Add(this.cmdStart);
			this.Name = "frmMain";
			this.Text = "Poblaciones Google GeoCoder";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cmdStart;
		private System.Windows.Forms.ListView lstView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.Button btnPaste;
		private System.Windows.Forms.Button btnCopy;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtKey;
		private System.Windows.Forms.Timer timer1;
	}
}

