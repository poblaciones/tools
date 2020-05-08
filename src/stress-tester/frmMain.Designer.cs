namespace stress_tester
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
			this.lw = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cmdRun = new System.Windows.Forms.Button();
			this.numParalells = new System.Windows.Forms.NumericUpDown();
			this.numClients = new System.Windows.Forms.NumericUpDown();
			this.grpInput = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.lblScriptTime = new System.Windows.Forms.Label();
			this.cmdSave = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.btnImport = new System.Windows.Forms.Button();
			this.cmdOpen = new System.Windows.Forms.Button();
			this.numRetryMs = new System.Windows.Forms.NumericUpDown();
			this.numRetries = new System.Windows.Forms.NumericUpDown();
			this.numLoops = new System.Windows.Forms.NumericUpDown();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.lblTotalTime = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lblRetries = new System.Windows.Forms.Label();
			this.lblErrors = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.lblHits = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.chFilter = new System.Windows.Forms.CheckBox();
			this.txtFilter = new System.Windows.Forms.TextBox();
			this.lblCount = new System.Windows.Forms.Label();
			this.chAutoSave = new System.Windows.Forms.CheckBox();
			this.timRefresh = new System.Windows.Forms.Timer(this.components);
			this.btnDelete = new System.Windows.Forms.Button();
			this.label11 = new System.Windows.Forms.Label();
			this.lblExecuting = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.lblTotalBytes = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.lblMaxExecuting = new System.Windows.Forms.Label();
			this.chRandom = new System.Windows.Forms.CheckBox();
			this.label14 = new System.Windows.Forms.Label();
			this.lblAvgTime = new System.Windows.Forms.Label();
			this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			((System.ComponentModel.ISupportInitialize)(this.numParalells)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numClients)).BeginInit();
			this.grpInput.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numRetryMs)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numRetries)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numLoops)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// lw
			// 
			this.lw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lw.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
			this.lw.FullRowSelect = true;
			this.lw.HideSelection = false;
			this.lw.Location = new System.Drawing.Point(12, 223);
			this.lw.Name = "lw";
			this.lw.Size = new System.Drawing.Size(741, 260);
			this.lw.TabIndex = 0;
			this.lw.UseCompatibleStateImageBehavior = false;
			this.lw.View = System.Windows.Forms.View.Details;
			this.lw.SelectedIndexChanged += new System.EventHandler(this.lw_SelectedIndexChanged);
			this.lw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lw_KeyDown);
			this.lw.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lw_KeyPress);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "URL";
			this.columnHeader1.Width = 268;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Schedule";
			this.columnHeader2.Width = 68;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Effective";
			this.columnHeader3.Width = 82;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Ellapsed";
			this.columnHeader4.Width = 85;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Response";
			this.columnHeader5.Width = 98;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Length";
			this.columnHeader6.Width = 83;
			// 
			// cmdRun
			// 
			this.cmdRun.Location = new System.Drawing.Point(480, 19);
			this.cmdRun.Name = "cmdRun";
			this.cmdRun.Size = new System.Drawing.Size(101, 46);
			this.cmdRun.TabIndex = 1;
			this.cmdRun.Text = "Run";
			this.cmdRun.UseVisualStyleBackColor = true;
			this.cmdRun.Click += new System.EventHandler(this.cmdRun_Click);
			// 
			// numParalells
			// 
			this.numParalells.Location = new System.Drawing.Point(121, 45);
			this.numParalells.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numParalells.Name = "numParalells";
			this.numParalells.Size = new System.Drawing.Size(109, 20);
			this.numParalells.TabIndex = 2;
			this.numParalells.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.numParalells.ValueChanged += new System.EventHandler(this.numParalells_ValueChanged);
			// 
			// numClients
			// 
			this.numClients.Location = new System.Drawing.Point(121, 19);
			this.numClients.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numClients.Name = "numClients";
			this.numClients.Size = new System.Drawing.Size(109, 20);
			this.numClients.TabIndex = 3;
			this.numClients.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numClients.ValueChanged += new System.EventHandler(this.numClients_ValueChanged);
			// 
			// grpInput
			// 
			this.grpInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpInput.Controls.Add(this.chRandom);
			this.grpInput.Controls.Add(this.label6);
			this.grpInput.Controls.Add(this.label5);
			this.grpInput.Controls.Add(this.label10);
			this.grpInput.Controls.Add(this.label8);
			this.grpInput.Controls.Add(this.label1);
			this.grpInput.Controls.Add(this.lblScriptTime);
			this.grpInput.Controls.Add(this.cmdSave);
			this.grpInput.Controls.Add(this.label7);
			this.grpInput.Controls.Add(this.btnImport);
			this.grpInput.Controls.Add(this.cmdOpen);
			this.grpInput.Controls.Add(this.cmdRun);
			this.grpInput.Controls.Add(this.numRetryMs);
			this.grpInput.Controls.Add(this.numRetries);
			this.grpInput.Controls.Add(this.numLoops);
			this.grpInput.Controls.Add(this.numClients);
			this.grpInput.Controls.Add(this.numParalells);
			this.grpInput.Location = new System.Drawing.Point(12, 12);
			this.grpInput.Name = "grpInput";
			this.grpInput.Size = new System.Drawing.Size(741, 99);
			this.grpInput.TabIndex = 4;
			this.grpInput.TabStop = false;
			this.grpInput.Text = "Input";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(18, 73);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(39, 13);
			this.label6.TabIndex = 4;
			this.label6.Text = "Loops:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(18, 47);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(86, 13);
			this.label5.TabIndex = 4;
			this.label5.Text = "Request / client:";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(258, 49);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(51, 13);
			this.label10.TabIndex = 4;
			this.label10.Text = "Retry ms:";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(258, 23);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(43, 13);
			this.label8.TabIndex = 4;
			this.label8.Text = "Retries:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(18, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Clients:";
			// 
			// lblScriptTime
			// 
			this.lblScriptTime.AutoSize = true;
			this.lblScriptTime.Location = new System.Drawing.Point(571, 73);
			this.lblScriptTime.Name = "lblScriptTime";
			this.lblScriptTime.Size = new System.Drawing.Size(10, 13);
			this.lblScriptTime.TabIndex = 4;
			this.lblScriptTime.Text = "-";
			// 
			// cmdSave
			// 
			this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdSave.Location = new System.Drawing.Point(658, 66);
			this.cmdSave.Name = "cmdSave";
			this.cmdSave.Size = new System.Drawing.Size(77, 20);
			this.cmdSave.TabIndex = 1;
			this.cmdSave.Text = "Save As";
			this.cmdSave.UseVisualStyleBackColor = true;
			this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(477, 73);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(84, 13);
			this.label7.TabIndex = 4;
			this.label7.Text = "Total script time:";
			// 
			// btnImport
			// 
			this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnImport.Location = new System.Drawing.Point(658, 42);
			this.btnImport.Name = "btnImport";
			this.btnImport.Size = new System.Drawing.Size(77, 20);
			this.btnImport.TabIndex = 1;
			this.btnImport.Text = "Import";
			this.btnImport.UseVisualStyleBackColor = true;
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
			// 
			// cmdOpen
			// 
			this.cmdOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOpen.Location = new System.Drawing.Point(658, 19);
			this.cmdOpen.Name = "cmdOpen";
			this.cmdOpen.Size = new System.Drawing.Size(77, 20);
			this.cmdOpen.TabIndex = 1;
			this.cmdOpen.Text = "Open";
			this.cmdOpen.UseVisualStyleBackColor = true;
			this.cmdOpen.Click += new System.EventHandler(this.cmdOpen_Click);
			// 
			// numRetryMs
			// 
			this.numRetryMs.Location = new System.Drawing.Point(350, 45);
			this.numRetryMs.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
			this.numRetryMs.Name = "numRetryMs";
			this.numRetryMs.Size = new System.Drawing.Size(100, 20);
			this.numRetryMs.TabIndex = 3;
			this.numRetryMs.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
			this.numRetryMs.ValueChanged += new System.EventHandler(this.nmRetries_ValueChanged);
			// 
			// numRetries
			// 
			this.numRetries.Location = new System.Drawing.Point(350, 19);
			this.numRetries.Name = "numRetries";
			this.numRetries.Size = new System.Drawing.Size(100, 20);
			this.numRetries.TabIndex = 3;
			this.numRetries.ValueChanged += new System.EventHandler(this.nmRetries_ValueChanged);
			// 
			// numLoops
			// 
			this.numLoops.Location = new System.Drawing.Point(121, 71);
			this.numLoops.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numLoops.Name = "numLoops";
			this.numLoops.Size = new System.Drawing.Size(109, 20);
			this.numLoops.TabIndex = 2;
			this.numLoops.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numLoops.ValueChanged += new System.EventHandler(this.numLoops_ValueChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.lblTotalBytes);
			this.groupBox2.Controls.Add(this.lblAvgTime);
			this.groupBox2.Controls.Add(this.lblTotalTime);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.Controls.Add(this.label14);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.lblRetries);
			this.groupBox2.Controls.Add(this.lblMaxExecuting);
			this.groupBox2.Controls.Add(this.lblExecuting);
			this.groupBox2.Controls.Add(this.lblErrors);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.label13);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.lblHits);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Location = new System.Drawing.Point(12, 117);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(741, 86);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Output";
			// 
			// lblTotalTime
			// 
			this.lblTotalTime.AutoSize = true;
			this.lblTotalTime.Location = new System.Drawing.Point(118, 56);
			this.lblTotalTime.Name = "lblTotalTime";
			this.lblTotalTime.Size = new System.Drawing.Size(10, 13);
			this.lblTotalTime.TabIndex = 4;
			this.lblTotalTime.Text = "-";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(18, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(82, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Total exec time:";
			// 
			// lblRetries
			// 
			this.lblRetries.AutoSize = true;
			this.lblRetries.Location = new System.Drawing.Point(278, 56);
			this.lblRetries.Name = "lblRetries";
			this.lblRetries.Size = new System.Drawing.Size(10, 13);
			this.lblRetries.TabIndex = 4;
			this.lblRetries.Text = "-";
			// 
			// lblErrors
			// 
			this.lblErrors.AutoSize = true;
			this.lblErrors.Location = new System.Drawing.Point(278, 29);
			this.lblErrors.Name = "lblErrors";
			this.lblErrors.Size = new System.Drawing.Size(10, 13);
			this.lblErrors.TabIndex = 4;
			this.lblErrors.Text = "-";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(206, 56);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(43, 13);
			this.label9.TabIndex = 4;
			this.label9.Text = "Retries:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(206, 29);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(37, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "Errors:";
			// 
			// lblHits
			// 
			this.lblHits.AutoSize = true;
			this.lblHits.Location = new System.Drawing.Point(118, 29);
			this.lblHits.Name = "lblHits";
			this.lblHits.Size = new System.Drawing.Size(10, 13);
			this.lblHits.TabIndex = 4;
			this.lblHits.Text = "-";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(18, 29);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(28, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Hits:";
			// 
			// chFilter
			// 
			this.chFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.chFilter.AutoSize = true;
			this.chFilter.Checked = true;
			this.chFilter.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chFilter.Location = new System.Drawing.Point(12, 489);
			this.chFilter.Name = "chFilter";
			this.chFilter.Size = new System.Drawing.Size(51, 17);
			this.chFilter.TabIndex = 5;
			this.chFilter.Text = "Filter:";
			this.chFilter.UseVisualStyleBackColor = true;
			this.chFilter.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// txtFilter
			// 
			this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.txtFilter.Location = new System.Drawing.Point(61, 486);
			this.txtFilter.Name = "txtFilter";
			this.txtFilter.Size = new System.Drawing.Size(181, 20);
			this.txtFilter.TabIndex = 6;
			this.txtFilter.Text = "https://mapa.poblaciones.org";
			this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
			// 
			// lblCount
			// 
			this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblCount.Location = new System.Drawing.Point(670, 489);
			this.lblCount.Name = "lblCount";
			this.lblCount.Size = new System.Drawing.Size(79, 14);
			this.lblCount.TabIndex = 7;
			this.lblCount.Text = "-";
			this.lblCount.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// chAutoSave
			// 
			this.chAutoSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.chAutoSave.AutoSize = true;
			this.chAutoSave.Checked = true;
			this.chAutoSave.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chAutoSave.Location = new System.Drawing.Point(262, 489);
			this.chAutoSave.Name = "chAutoSave";
			this.chAutoSave.Size = new System.Drawing.Size(102, 17);
			this.chAutoSave.TabIndex = 5;
			this.chAutoSave.Text = "Autosave / load";
			this.chAutoSave.UseVisualStyleBackColor = true;
			this.chAutoSave.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// timRefresh
			// 
			this.timRefresh.Tick += new System.EventHandler(this.timRefresh_Tick);
			// 
			// btnDelete
			// 
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDelete.Location = new System.Drawing.Point(458, 486);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(77, 20);
			this.btnDelete.TabIndex = 1;
			this.btnDelete.Text = "Delete";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(477, 56);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(57, 13);
			this.label11.TabIndex = 4;
			this.label11.Text = "Executing:";
			// 
			// lblExecuting
			// 
			this.lblExecuting.AutoSize = true;
			this.lblExecuting.Location = new System.Drawing.Point(571, 56);
			this.lblExecuting.Name = "lblExecuting";
			this.lblExecuting.Size = new System.Drawing.Size(10, 13);
			this.lblExecuting.TabIndex = 4;
			this.lblExecuting.Text = "-";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(347, 56);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(62, 13);
			this.label12.TabIndex = 4;
			this.label12.Text = "Total bytes:";
			// 
			// lblTotalBytes
			// 
			this.lblTotalBytes.AutoSize = true;
			this.lblTotalBytes.Location = new System.Drawing.Point(419, 56);
			this.lblTotalBytes.Name = "lblTotalBytes";
			this.lblTotalBytes.Size = new System.Drawing.Size(10, 13);
			this.lblTotalBytes.TabIndex = 4;
			this.lblTotalBytes.Text = "-";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(477, 25);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(83, 13);
			this.label13.TabIndex = 4;
			this.label13.Text = "Max. Executing:";
			// 
			// lblMaxExecuting
			// 
			this.lblMaxExecuting.AutoSize = true;
			this.lblMaxExecuting.Location = new System.Drawing.Point(573, 25);
			this.lblMaxExecuting.Name = "lblMaxExecuting";
			this.lblMaxExecuting.Size = new System.Drawing.Size(10, 13);
			this.lblMaxExecuting.TabIndex = 4;
			this.lblMaxExecuting.Text = "-";
			// 
			// chRandom
			// 
			this.chRandom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.chRandom.AutoSize = true;
			this.chRandom.Checked = true;
			this.chRandom.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chRandom.Location = new System.Drawing.Point(261, 74);
			this.chRandom.Name = "chRandom";
			this.chRandom.Size = new System.Drawing.Size(100, 17);
			this.chRandom.TabIndex = 6;
			this.chRandom.Text = "Random offsets";
			this.chRandom.UseVisualStyleBackColor = true;
			this.chRandom.CheckedChanged += new System.EventHandler(this.chRandom_CheckedChanged);
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(347, 29);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(54, 13);
			this.label14.TabIndex = 4;
			this.label14.Text = "Avg. time:";
			// 
			// lblAvgTime
			// 
			this.lblAvgTime.AutoSize = true;
			this.lblAvgTime.Location = new System.Drawing.Point(419, 29);
			this.lblAvgTime.Name = "lblAvgTime";
			this.lblAvgTime.Size = new System.Drawing.Size(10, 13);
			this.lblAvgTime.TabIndex = 4;
			this.lblAvgTime.Text = "-";
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Hits";
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(765, 512);
			this.Controls.Add(this.lblCount);
			this.Controls.Add(this.txtFilter);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.chAutoSave);
			this.Controls.Add(this.chFilter);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.grpInput);
			this.Controls.Add(this.lw);
			this.Name = "frmMain";
			this.Text = "Stress Tester";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
			this.Load += new System.EventHandler(this.frmMain_Load);
			((System.ComponentModel.ISupportInitialize)(this.numParalells)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numClients)).EndInit();
			this.grpInput.ResumeLayout(false);
			this.grpInput.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numRetryMs)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numRetries)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numLoops)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView lw;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.Button cmdRun;
		private System.Windows.Forms.NumericUpDown numParalells;
		private System.Windows.Forms.NumericUpDown numClients;
		private System.Windows.Forms.GroupBox grpInput;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblScriptTime;
		private System.Windows.Forms.Button cmdSave;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button cmdOpen;
		private System.Windows.Forms.NumericUpDown numLoops;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label lblTotalTime;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lblErrors;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label lblHits;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox chFilter;
		private System.Windows.Forms.TextBox txtFilter;
		private System.Windows.Forms.Label lblCount;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.NumericUpDown numRetries;
		private System.Windows.Forms.Label lblRetries;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.NumericUpDown numRetryMs;
		private System.Windows.Forms.Button btnImport;
		private System.Windows.Forms.CheckBox chAutoSave;
		private System.Windows.Forms.Timer timRefresh;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Label lblExecuting;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label lblTotalBytes;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label lblMaxExecuting;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.CheckBox chRandom;
		private System.Windows.Forms.Label lblAvgTime;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.ColumnHeader columnHeader7;
	}
}

