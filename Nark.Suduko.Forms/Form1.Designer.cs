namespace Nark.Suduko.Forms
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Gen = new System.Windows.Forms.Button();
            this.cbb_Level = new System.Windows.Forms.ComboBox();
            this.lb_Time = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_Gen
            // 
            this.btn_Gen.Location = new System.Drawing.Point(557, 40);
            this.btn_Gen.Name = "btn_Gen";
            this.btn_Gen.Size = new System.Drawing.Size(75, 23);
            this.btn_Gen.TabIndex = 0;
            this.btn_Gen.Text = "button1";
            this.btn_Gen.UseVisualStyleBackColor = true;
            this.btn_Gen.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbb_Level
            // 
            this.cbb_Level.FormattingEnabled = true;
            this.cbb_Level.Location = new System.Drawing.Point(557, 13);
            this.cbb_Level.Name = "cbb_Level";
            this.cbb_Level.Size = new System.Drawing.Size(75, 20);
            this.cbb_Level.TabIndex = 1;
            // 
            // lb_Time
            // 
            this.lb_Time.AutoSize = true;
            this.lb_Time.Font = new System.Drawing.Font("宋体", 15F);
            this.lb_Time.Location = new System.Drawing.Point(520, 79);
            this.lb_Time.Name = "lb_Time";
            this.lb_Time.Size = new System.Drawing.Size(69, 20);
            this.lb_Time.TabIndex = 2;
            this.lb_Time.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 514);
            this.Controls.Add(this.lb_Time);
            this.Controls.Add(this.cbb_Level);
            this.Controls.Add(this.btn_Gen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Gen;
        private System.Windows.Forms.ComboBox cbb_Level;
        private System.Windows.Forms.Label lb_Time;

    }
}

