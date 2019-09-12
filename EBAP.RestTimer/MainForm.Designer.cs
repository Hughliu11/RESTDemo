namespace EBAP.RestTimer
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnStartAll = new System.Windows.Forms.ToolStripSplitButton();
            this.btnStartTimer1 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStartTimer2 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStartTimer3 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStartTimer4 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStopAll = new System.Windows.Forms.ToolStripSplitButton();
            this.btnStopTimer1 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStopTimer2 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStopTimer3 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStopTimer4 = new System.Windows.Forms.ToolStripMenuItem();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.timer4 = new System.Windows.Forms.Timer(this.components);
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnStartAll,
            this.btnStopAll});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(863, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // btnStartAll
            // 
            this.btnStartAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnStartAll.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnStartTimer1,
            this.btnStartTimer2,
            this.btnStartTimer3,
            this.btnStartTimer4});
            this.btnStartAll.Image = ((System.Drawing.Image)(resources.GetObject("btnStartAll.Image")));
            this.btnStartAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStartAll.Name = "btnStartAll";
            this.btnStartAll.Size = new System.Drawing.Size(72, 22);
            this.btnStartAll.Text = "全部开始";
            this.btnStartAll.ButtonClick += new System.EventHandler(this.btnStartAll_ButtonClick);
            // 
            // btnStartTimer1
            // 
            this.btnStartTimer1.Name = "btnStartTimer1";
            this.btnStartTimer1.Size = new System.Drawing.Size(189, 22);
            this.btnStartTimer1.Text = "材料出库同步_开始";
            this.btnStartTimer1.Click += new System.EventHandler(this.btnStartTimer1_Click);
            // 
            // btnStartTimer2
            // 
            this.btnStartTimer2.Name = "btnStartTimer2";
            this.btnStartTimer2.Size = new System.Drawing.Size(189, 22);
            this.btnStartTimer2.Text = "产成品入库同步_开始";
            this.btnStartTimer2.Click += new System.EventHandler(this.btnStartTimer2_Click);
            // 
            // btnStartTimer3
            // 
            this.btnStartTimer3.Name = "btnStartTimer3";
            this.btnStartTimer3.Size = new System.Drawing.Size(189, 22);
            this.btnStartTimer3.Text = "作业统计单同步_开始";
            this.btnStartTimer3.Click += new System.EventHandler(this.btnStartTimer3_Click);
            // 
            // btnStartTimer4
            // 
            this.btnStartTimer4.Name = "btnStartTimer4";
            this.btnStartTimer4.Size = new System.Drawing.Size(189, 22);
            this.btnStartTimer4.Text = "产成品出库同步_开始";
            this.btnStartTimer4.Click += new System.EventHandler(this.btnStartTimer4_Click);
            // 
            // btnStopAll
            // 
            this.btnStopAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnStopAll.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnStopTimer1,
            this.btnStopTimer2,
            this.btnStopTimer3,
            this.btnStopTimer4});
            this.btnStopAll.Image = ((System.Drawing.Image)(resources.GetObject("btnStopAll.Image")));
            this.btnStopAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStopAll.Name = "btnStopAll";
            this.btnStopAll.Size = new System.Drawing.Size(72, 22);
            this.btnStopAll.Text = "全部结束";
            this.btnStopAll.ButtonClick += new System.EventHandler(this.btnStopAll_ButtonClick);
            // 
            // btnStopTimer1
            // 
            this.btnStopTimer1.Name = "btnStopTimer1";
            this.btnStopTimer1.Size = new System.Drawing.Size(189, 22);
            this.btnStopTimer1.Text = "材料出库同步_结束";
            this.btnStopTimer1.Click += new System.EventHandler(this.btnStopTimer1_Click);
            // 
            // btnStopTimer2
            // 
            this.btnStopTimer2.Name = "btnStopTimer2";
            this.btnStopTimer2.Size = new System.Drawing.Size(189, 22);
            this.btnStopTimer2.Text = "产成品入库同步_结束";
            this.btnStopTimer2.Click += new System.EventHandler(this.btnStopTimer2_Click);
            // 
            // btnStopTimer3
            // 
            this.btnStopTimer3.Name = "btnStopTimer3";
            this.btnStopTimer3.Size = new System.Drawing.Size(189, 22);
            this.btnStopTimer3.Text = "作业统计单同步_结束";
            this.btnStopTimer3.Click += new System.EventHandler(this.btnStopTimer3_Click);
            // 
            // btnStopTimer4
            // 
            this.btnStopTimer4.Name = "btnStopTimer4";
            this.btnStopTimer4.Size = new System.Drawing.Size(189, 22);
            this.btnStopTimer4.Text = "产成品出库同步_结束";
            this.btnStopTimer4.Click += new System.EventHandler(this.btnStopTimer4_Click);
            // 
            // txtMsg
            // 
            this.txtMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMsg.Location = new System.Drawing.Point(0, 25);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.ReadOnly = true;
            this.txtMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMsg.Size = new System.Drawing.Size(863, 511);
            this.txtMsg.TabIndex = 1;
            this.txtMsg.WordWrap = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 500;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer3
            // 
            this.timer3.Interval = 500;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // timer4
            // 
            this.timer4.Tick += new System.EventHandler(this.timer4_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 536);
            this.Controls.Add(this.txtMsg);
            this.Controls.Add(this.toolStrip);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RestTimer";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ToolStripSplitButton btnStartAll;
        private System.Windows.Forms.ToolStripSplitButton btnStopAll;
        private System.Windows.Forms.ToolStripMenuItem btnStartTimer1;
        private System.Windows.Forms.ToolStripMenuItem btnStartTimer2;
        private System.Windows.Forms.ToolStripMenuItem btnStopTimer1;
        private System.Windows.Forms.ToolStripMenuItem btnStopTimer2;
        private System.Windows.Forms.ToolStripMenuItem btnStartTimer3;
        private System.Windows.Forms.ToolStripMenuItem btnStopTimer3;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.ToolStripMenuItem btnStartTimer4;
        private System.Windows.Forms.ToolStripMenuItem btnStopTimer4;
        private System.Windows.Forms.Timer timer4;
    }
}

