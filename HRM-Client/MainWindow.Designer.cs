namespace HRM_Client
{
    partial class MainWindow
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
            components = new System.ComponentModel.Container();
            txtLog = new TextBox();
            timer = new System.Windows.Forms.Timer(components);
            panel1 = new Panel();
            button2 = new Button();
            btnrestart = new Button();
            btnStart = new Button();
            lblState = new Label();
            label1 = new Label();
            pnlMain = new Panel();
            notifyIcon = new NotifyIcon(components);
            panel1.SuspendLayout();
            pnlMain.SuspendLayout();
            SuspendLayout();
            // 
            // txtLog
            // 
            txtLog.Location = new Point(51, 23);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Both;
            txtLog.Size = new Size(72, 44);
            txtLog.TabIndex = 0;
            // 
            // timer
            // 
            timer.Interval = 2000;
            timer.Tick += timer_Tick;
            // 
            // panel1
            // 
            panel1.Controls.Add(button2);
            panel1.Controls.Add(btnrestart);
            panel1.Controls.Add(btnStart);
            panel1.Controls.Add(lblState);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(453, 32);
            panel1.TabIndex = 1;
            // 
            // button2
            // 
            button2.Location = new Point(319, 5);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 4;
            button2.Text = "Exit";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // btnrestart
            // 
            btnrestart.Location = new Point(238, 5);
            btnrestart.Name = "btnrestart";
            btnrestart.Size = new Size(75, 23);
            btnrestart.TabIndex = 3;
            btnrestart.Text = "Restart";
            btnrestart.UseVisualStyleBackColor = true;
            btnrestart.Click += btnrestart_Click;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(157, 5);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(75, 23);
            btnStart.TabIndex = 2;
            btnStart.Text = "Stope";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // lblState
            // 
            lblState.AutoSize = true;
            lblState.Location = new Point(60, 9);
            lblState.Name = "lblState";
            lblState.Size = new Size(16, 15);
            lblState.TabIndex = 1;
            lblState.Text = "...";
            lblState.TextChanged += lblState_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 0;
            label1.Text = "State : ";
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(txtLog);
            pnlMain.Location = new Point(0, 32);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(165, 88);
            pnlMain.TabIndex = 2;
            // 
            // notifyIcon
            // 
            notifyIcon.Text = "notifyIcon1";
            notifyIcon.Visible = true;
            notifyIcon.MouseDoubleClick += notifyIcon_MouseDoubleClick;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(453, 212);
            Controls.Add(pnlMain);
            Controls.Add(panel1);
            Name = "MainWindow";
            Text = "HRM Client";
            FormClosing += MainWindow_FormClosing;
            Load += mainWindow_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            pnlMain.ResumeLayout(false);
            pnlMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox txtLog;
        private System.Windows.Forms.Timer timer;
        private Panel panel1;
        private Label lblState;
        private Label label1;
        private Button btnStart;
        private Panel pnlMain;
        private NotifyIcon notifyIcon;
        private Button button2;
        private Button btnrestart;
    }
}