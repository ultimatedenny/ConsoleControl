namespace ConsoleControlSample
{
  partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.poisonStyleManager1 = new ReaLTaiizor.Manager.PoisonStyleManager(this.components);
            this.metroTabPage1 = new ReaLTaiizor.Child.Metro.MetroTabPage();
            this.poisonLabel1 = new ReaLTaiizor.Controls.PoisonLabel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.poisonLabel3 = new ReaLTaiizor.Controls.PoisonLabel();
            this.poisonLabel4 = new ReaLTaiizor.Controls.PoisonLabel();
            this.poisonTabControl1 = new ReaLTaiizor.Controls.PoisonTabControl();
            this.poisonTabPage1 = new ReaLTaiizor.Controls.PoisonTabPage();
            this.poisonTabPage2 = new ReaLTaiizor.Controls.PoisonTabPage();
            this.poisonLabel5 = new ReaLTaiizor.Controls.PoisonLabel();
            this.poisonLabel2 = new ReaLTaiizor.Controls.PoisonLabel();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.poisonStyleManager1)).BeginInit();
            this.poisonTabControl1.SuspendLayout();
            this.poisonTabPage1.SuspendLayout();
            this.poisonTabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // poisonStyleManager1
            // 
            this.poisonStyleManager1.Owner = this;
            this.poisonStyleManager1.Style = ReaLTaiizor.Enum.Poison.ColorStyle.Orange;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.BaseColor = System.Drawing.Color.White;
            this.metroTabPage1.Font = null;
            this.metroTabPage1.ImageIndex = 0;
            this.metroTabPage1.ImageKey = null;
            this.metroTabPage1.IsDerivedStyle = true;
            this.metroTabPage1.Location = new System.Drawing.Point(0, 0);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(200, 100);
            this.metroTabPage1.Style = ReaLTaiizor.Enum.Metro.Style.Light;
            this.metroTabPage1.StyleManager = null;
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = null;
            this.metroTabPage1.ThemeAuthor = "Taiizor";
            this.metroTabPage1.ThemeName = "MetroLight";
            this.metroTabPage1.ToolTipText = null;
            // 
            // poisonLabel1
            // 
            this.poisonLabel1.AutoSize = true;
            this.poisonLabel1.Location = new System.Drawing.Point(4, 572);
            this.poisonLabel1.Name = "poisonLabel1";
            this.poisonLabel1.Size = new System.Drawing.Size(88, 19);
            this.poisonLabel1.TabIndex = 17;
            this.poisonLabel1.Text = "Status: Online";
            this.poisonLabel1.Theme = ReaLTaiizor.Enum.Poison.ThemeStyle.Light;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoScroll = true;
            this.flowLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 250);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(444, 171);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 31);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(444, 189);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // poisonLabel3
            // 
            this.poisonLabel3.AutoSize = true;
            this.poisonLabel3.Location = new System.Drawing.Point(3, 9);
            this.poisonLabel3.Name = "poisonLabel3";
            this.poisonLabel3.Size = new System.Drawing.Size(70, 19);
            this.poisonLabel3.TabIndex = 21;
            this.poisonLabel3.Text = "Queue List";
            this.poisonLabel3.Theme = ReaLTaiizor.Enum.Poison.ThemeStyle.Light;
            // 
            // poisonLabel4
            // 
            this.poisonLabel4.AutoSize = true;
            this.poisonLabel4.Location = new System.Drawing.Point(3, 228);
            this.poisonLabel4.Name = "poisonLabel4";
            this.poisonLabel4.Size = new System.Drawing.Size(75, 19);
            this.poisonLabel4.TabIndex = 22;
            this.poisonLabel4.Text = "Process List";
            this.poisonLabel4.Theme = ReaLTaiizor.Enum.Poison.ThemeStyle.Light;
            // 
            // poisonTabControl1
            // 
            this.poisonTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.poisonTabControl1.Controls.Add(this.poisonTabPage1);
            this.poisonTabControl1.Controls.Add(this.poisonTabPage2);
            this.poisonTabControl1.Location = new System.Drawing.Point(0, 72);
            this.poisonTabControl1.Name = "poisonTabControl1";
            this.poisonTabControl1.SelectedIndex = 0;
            this.poisonTabControl1.Size = new System.Drawing.Size(458, 497);
            this.poisonTabControl1.Style = ReaLTaiizor.Enum.Poison.ColorStyle.Orange;
            this.poisonTabControl1.TabIndex = 23;
            this.poisonTabControl1.UseSelectable = true;
            // 
            // poisonTabPage1
            // 
            this.poisonTabPage1.Controls.Add(this.poisonLabel4);
            this.poisonTabPage1.Controls.Add(this.flowLayoutPanel2);
            this.poisonTabPage1.Controls.Add(this.poisonLabel3);
            this.poisonTabPage1.Controls.Add(this.flowLayoutPanel1);
            this.poisonTabPage1.HorizontalScrollbarBarColor = true;
            this.poisonTabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.poisonTabPage1.HorizontalScrollbarSize = 10;
            this.poisonTabPage1.Location = new System.Drawing.Point(4, 38);
            this.poisonTabPage1.Name = "poisonTabPage1";
            this.poisonTabPage1.Size = new System.Drawing.Size(450, 455);
            this.poisonTabPage1.TabIndex = 0;
            this.poisonTabPage1.Text = "Main";
            this.poisonTabPage1.VerticalScrollbarBarColor = true;
            this.poisonTabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.poisonTabPage1.VerticalScrollbarSize = 10;
            // 
            // poisonTabPage2
            // 
            this.poisonTabPage2.Controls.Add(this.poisonLabel5);
            this.poisonTabPage2.HorizontalScrollbarBarColor = true;
            this.poisonTabPage2.HorizontalScrollbarHighlightOnWheel = false;
            this.poisonTabPage2.HorizontalScrollbarSize = 10;
            this.poisonTabPage2.Location = new System.Drawing.Point(4, 38);
            this.poisonTabPage2.Name = "poisonTabPage2";
            this.poisonTabPage2.Size = new System.Drawing.Size(450, 455);
            this.poisonTabPage2.TabIndex = 1;
            this.poisonTabPage2.Text = "About";
            this.poisonTabPage2.VerticalScrollbarBarColor = true;
            this.poisonTabPage2.VerticalScrollbarHighlightOnWheel = false;
            this.poisonTabPage2.VerticalScrollbarSize = 10;
            // 
            // poisonLabel5
            // 
            this.poisonLabel5.AutoSize = true;
            this.poisonLabel5.Location = new System.Drawing.Point(0, 0);
            this.poisonLabel5.Name = "poisonLabel5";
            this.poisonLabel5.Size = new System.Drawing.Size(45, 19);
            this.poisonLabel5.TabIndex = 20;
            this.poisonLabel5.Text = "About";
            this.poisonLabel5.Theme = ReaLTaiizor.Enum.Poison.ThemeStyle.Light;
            // 
            // poisonLabel2
            // 
            this.poisonLabel2.AutoSize = true;
            this.poisonLabel2.Location = new System.Drawing.Point(7, 13);
            this.poisonLabel2.Name = "poisonLabel2";
            this.poisonLabel2.Size = new System.Drawing.Size(92, 38);
            this.poisonLabel2.TabIndex = 24;
            this.poisonLabel2.Text = "Assembly Title\r\n1.0";
            this.poisonLabel2.Theme = ReaLTaiizor.Enum.Poison.ThemeStyle.Light;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 595);
            this.Controls.Add(this.poisonLabel2);
            this.Controls.Add(this.poisonTabControl1);
            this.Controls.Add(this.poisonLabel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(457, 595);
            this.MinimumSize = new System.Drawing.Size(457, 595);
            this.Name = "Form1";
            this.Style = ReaLTaiizor.Enum.Poison.ColorStyle.Orange;
            this.TextAlign = ReaLTaiizor.Enum.Poison.FormTextAlignType.Right;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.poisonStyleManager1)).EndInit();
            this.poisonTabControl1.ResumeLayout(false);
            this.poisonTabPage1.ResumeLayout(false);
            this.poisonTabPage1.PerformLayout();
            this.poisonTabPage2.ResumeLayout(false);
            this.poisonTabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion
        private ReaLTaiizor.Manager.PoisonStyleManager poisonStyleManager1;
        private ReaLTaiizor.Child.Metro.MetroTabPage metroTabPage1;
        private ReaLTaiizor.Controls.PoisonLabel poisonLabel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private ReaLTaiizor.Controls.PoisonTabControl poisonTabControl1;
        private ReaLTaiizor.Controls.PoisonTabPage poisonTabPage1;
        private ReaLTaiizor.Controls.PoisonLabel poisonLabel4;
        private ReaLTaiizor.Controls.PoisonLabel poisonLabel3;
        private ReaLTaiizor.Controls.PoisonTabPage poisonTabPage2;
        private ReaLTaiizor.Controls.PoisonLabel poisonLabel5;
        private ReaLTaiizor.Controls.PoisonLabel poisonLabel2;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

