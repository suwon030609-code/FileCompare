namespace FileCompare
{
    partial class Form1
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
            splitContainer1 = new SplitContainer();
            pnlLeftList = new Panel();
            lvwLeftDir = new ListView();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            pnlLeftPath = new Panel();
            btnLeftDir = new Button();
            txtLeftDir = new TextBox();
            pnlLeftTop = new Panel();
            btnCopyFromLeft = new Button();
            lblAppName = new Label();
            pnlRightList = new Panel();
            lvwRightDir = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            pnlRightPath = new Panel();
            btnRightDir = new Button();
            txtRightDir = new TextBox();
            pnlRightTop = new Panel();
            btnCopyFromRight = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            pnlLeftList.SuspendLayout();
            pnlLeftPath.SuspendLayout();
            pnlLeftTop.SuspendLayout();
            pnlRightList.SuspendLayout();
            pnlRightPath.SuspendLayout();
            pnlRightTop.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.BorderStyle = BorderStyle.FixedSingle;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(10, 10);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(pnlLeftList);
            splitContainer1.Panel1.Controls.Add(pnlLeftPath);
            splitContainer1.Panel1.Controls.Add(pnlLeftTop);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(pnlRightList);
            splitContainer1.Panel2.Controls.Add(pnlRightPath);
            splitContainer1.Panel2.Controls.Add(pnlRightTop);
            splitContainer1.Size = new Size(2054, 909);
            splitContainer1.SplitterDistance = 1029;
            splitContainer1.SplitterWidth = 10;
            splitContainer1.TabIndex = 0;
            // 
            // pnlLeftList
            // 
            pnlLeftList.Controls.Add(lvwLeftDir);
            pnlLeftList.Dock = DockStyle.Fill;
            pnlLeftList.Location = new Point(0, 356);
            pnlLeftList.Name = "pnlLeftList";
            pnlLeftList.Size = new Size(1027, 551);
            pnlLeftList.TabIndex = 2;
            // 
            // lvwLeftDir
            // 
            lvwLeftDir.Columns.AddRange(new ColumnHeader[] { columnHeader4, columnHeader5, columnHeader6 });
            lvwLeftDir.Dock = DockStyle.Fill;
            lvwLeftDir.FullRowSelect = true;
            lvwLeftDir.GridLines = true;
            lvwLeftDir.Location = new Point(0, 0);
            lvwLeftDir.Name = "lvwLeftDir";
            lvwLeftDir.Size = new Size(1027, 551);
            lvwLeftDir.TabIndex = 0;
            lvwLeftDir.UseCompatibleStateImageBehavior = false;
            lvwLeftDir.View = View.Details;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "이름";
            columnHeader4.Width = 500;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "크기";
            columnHeader5.Width = 200;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "수정일";
            columnHeader6.Width = 360;
            // 
            // pnlLeftPath
            // 
            pnlLeftPath.Controls.Add(btnLeftDir);
            pnlLeftPath.Controls.Add(txtLeftDir);
            pnlLeftPath.Dock = DockStyle.Top;
            pnlLeftPath.Location = new Point(0, 262);
            pnlLeftPath.Name = "pnlLeftPath";
            pnlLeftPath.Size = new Size(1027, 94);
            pnlLeftPath.TabIndex = 1;
            // 
            // btnLeftDir
            // 
            btnLeftDir.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnLeftDir.Font = new Font("맑은 고딕", 10.125F);
            btnLeftDir.Location = new Point(870, 24);
            btnLeftDir.Name = "btnLeftDir";
            btnLeftDir.Size = new Size(150, 46);
            btnLeftDir.TabIndex = 3;
            btnLeftDir.Text = "폴더선택";
            btnLeftDir.UseVisualStyleBackColor = true;
            btnLeftDir.Click += btnLeftDir_Click;
            // 
            // txtLeftDir
            // 
            txtLeftDir.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtLeftDir.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            txtLeftDir.Location = new Point(12, 21);
            txtLeftDir.Name = "txtLeftDir";
            txtLeftDir.ReadOnly = true;
            txtLeftDir.Size = new Size(824, 50);
            txtLeftDir.TabIndex = 1;
            // 
            // pnlLeftTop
            // 
            pnlLeftTop.Controls.Add(btnCopyFromLeft);
            pnlLeftTop.Controls.Add(lblAppName);
            pnlLeftTop.Dock = DockStyle.Top;
            pnlLeftTop.Location = new Point(0, 0);
            pnlLeftTop.Name = "pnlLeftTop";
            pnlLeftTop.Size = new Size(1027, 262);
            pnlLeftTop.TabIndex = 0;
            // 
            // btnCopyFromLeft
            // 
            btnCopyFromLeft.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCopyFromLeft.Font = new Font("맑은 고딕", 12F);
            btnCopyFromLeft.Location = new Point(870, 33);
            btnCopyFromLeft.Name = "btnCopyFromLeft";
            btnCopyFromLeft.Size = new Size(150, 46);
            btnCopyFromLeft.TabIndex = 2;
            btnCopyFromLeft.Text = ">>>";
            btnCopyFromLeft.UseVisualStyleBackColor = true;
            btnCopyFromLeft.Click += btnCopyFromLeft_Click;
            // 
            // lblAppName
            // 
            lblAppName.AutoSize = true;
            lblAppName.Font = new Font("맑은 고딕", 19.875F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblAppName.ForeColor = SystemColors.HotTrack;
            lblAppName.Location = new Point(3, 9);
            lblAppName.Name = "lblAppName";
            lblAppName.Size = new Size(364, 71);
            lblAppName.TabIndex = 0;
            lblAppName.Text = "File Compare";
            // 
            // pnlRightList
            // 
            pnlRightList.Controls.Add(lvwRightDir);
            pnlRightList.Dock = DockStyle.Fill;
            pnlRightList.Location = new Point(0, 356);
            pnlRightList.Name = "pnlRightList";
            pnlRightList.Size = new Size(1013, 551);
            pnlRightList.TabIndex = 3;
            // 
            // lvwRightDir
            // 
            lvwRightDir.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3 });
            lvwRightDir.Dock = DockStyle.Fill;
            lvwRightDir.FullRowSelect = true;
            lvwRightDir.GridLines = true;
            lvwRightDir.Location = new Point(0, 0);
            lvwRightDir.Name = "lvwRightDir";
            lvwRightDir.Size = new Size(1013, 551);
            lvwRightDir.TabIndex = 1;
            lvwRightDir.UseCompatibleStateImageBehavior = false;
            lvwRightDir.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "이름";
            columnHeader1.Width = 500;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "크기";
            columnHeader2.Width = 200;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "수정일";
            columnHeader3.Width = 360;
            // 
            // pnlRightPath
            // 
            pnlRightPath.Controls.Add(btnRightDir);
            pnlRightPath.Controls.Add(txtRightDir);
            pnlRightPath.Dock = DockStyle.Top;
            pnlRightPath.Location = new Point(0, 262);
            pnlRightPath.Name = "pnlRightPath";
            pnlRightPath.Size = new Size(1013, 94);
            pnlRightPath.TabIndex = 2;
            // 
            // btnRightDir
            // 
            btnRightDir.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnRightDir.Font = new Font("맑은 고딕", 10.125F);
            btnRightDir.Location = new Point(851, 21);
            btnRightDir.Name = "btnRightDir";
            btnRightDir.Size = new Size(150, 46);
            btnRightDir.TabIndex = 5;
            btnRightDir.Text = "폴더선택";
            btnRightDir.UseVisualStyleBackColor = true;
            btnRightDir.Click += btnRightDir_Click;
            // 
            // txtRightDir
            // 
            txtRightDir.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtRightDir.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            txtRightDir.Location = new Point(7, 20);
            txtRightDir.Name = "txtRightDir";
            txtRightDir.ReadOnly = true;
            txtRightDir.Size = new Size(826, 50);
            txtRightDir.TabIndex = 2;
            // 
            // pnlRightTop
            // 
            pnlRightTop.Controls.Add(btnCopyFromRight);
            pnlRightTop.Dock = DockStyle.Top;
            pnlRightTop.Location = new Point(0, 0);
            pnlRightTop.Name = "pnlRightTop";
            pnlRightTop.Size = new Size(1013, 262);
            pnlRightTop.TabIndex = 1;
            // 
            // btnCopyFromRight
            // 
            btnCopyFromRight.Font = new Font("맑은 고딕", 12F);
            btnCopyFromRight.Location = new Point(17, 34);
            btnCopyFromRight.Name = "btnCopyFromRight";
            btnCopyFromRight.Size = new Size(150, 46);
            btnCopyFromRight.TabIndex = 4;
            btnCopyFromRight.Text = "<<<";
            btnCopyFromRight.UseVisualStyleBackColor = true;
            btnCopyFromRight.Click += btnCopyFromRight_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(14F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2074, 929);
            Controls.Add(splitContainer1);
            Name = "Form1";
            Padding = new Padding(10);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "File Compare v1.0";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            pnlLeftList.ResumeLayout(false);
            pnlLeftPath.ResumeLayout(false);
            pnlLeftPath.PerformLayout();
            pnlLeftTop.ResumeLayout(false);
            pnlLeftTop.PerformLayout();
            pnlRightList.ResumeLayout(false);
            pnlRightPath.ResumeLayout(false);
            pnlRightPath.PerformLayout();
            pnlRightTop.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private Panel pnlLeftList;
        private Panel pnlLeftPath;
        private Panel pnlLeftTop;
        private Panel pnlRightList;
        private Panel pnlRightPath;
        private Panel pnlRightTop;
        private Label lblAppName;
        private ListView lvwLeftDir;
        private Button btnLeftDir;
        private TextBox txtLeftDir;
        private Button btnCopyFromLeft;
        private ListView lvwRightDir;
        private Button btnRightDir;
        private TextBox txtRightDir;
        private Button btnCopyFromRight;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
    }
}
