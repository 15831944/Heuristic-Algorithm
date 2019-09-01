namespace 木板最优切割方案
{
    partial class control
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
            this.btnGo = new System.Windows.Forms.Button();
            this.btnTry = new System.Windows.Forms.Button();
            this.rictxt = new System.Windows.Forms.RichTextBox();
            this.prbInitial = new System.Windows.Forms.ProgressBar();
            this.prbCross = new System.Windows.Forms.ProgressBar();
            this.prbIteration = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(724, 12);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(77, 97);
            this.btnGo.TabIndex = 0;
            this.btnGo.Text = "Go！";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // btnTry
            // 
            this.btnTry.Location = new System.Drawing.Point(724, 239);
            this.btnTry.Name = "btnTry";
            this.btnTry.Size = new System.Drawing.Size(77, 97);
            this.btnTry.TabIndex = 0;
            this.btnTry.Text = "try";
            this.btnTry.UseVisualStyleBackColor = true;
            this.btnTry.Click += new System.EventHandler(this.btnTry_Click);
            // 
            // rictxt
            // 
            this.rictxt.Location = new System.Drawing.Point(12, 12);
            this.rictxt.Name = "rictxt";
            this.rictxt.Size = new System.Drawing.Size(669, 324);
            this.rictxt.TabIndex = 1;
            this.rictxt.Text = "";
            // 
            // prbInitial
            // 
            this.prbInitial.Location = new System.Drawing.Point(12, 376);
            this.prbInitial.Name = "prbInitial";
            this.prbInitial.Size = new System.Drawing.Size(669, 23);
            this.prbInitial.TabIndex = 2;
            // 
            // prbCross
            // 
            this.prbCross.Location = new System.Drawing.Point(12, 426);
            this.prbCross.Name = "prbCross";
            this.prbCross.Size = new System.Drawing.Size(669, 23);
            this.prbCross.TabIndex = 2;
            // 
            // prbIteration
            // 
            this.prbIteration.Location = new System.Drawing.Point(12, 476);
            this.prbIteration.Name = "prbIteration";
            this.prbIteration.Size = new System.Drawing.Size(669, 23);
            this.prbIteration.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(734, 384);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(734, 434);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "交叉";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(734, 484);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "迭代";
            // 
            // control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 534);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.prbIteration);
            this.Controls.Add(this.prbCross);
            this.Controls.Add(this.prbInitial);
            this.Controls.Add(this.rictxt);
            this.Controls.Add(this.btnTry);
            this.Controls.Add(this.btnGo);
            this.Name = "control";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Button btnTry;
        private System.Windows.Forms.RichTextBox rictxt;
        private System.Windows.Forms.ProgressBar prbInitial;
        private System.Windows.Forms.ProgressBar prbCross;
        private System.Windows.Forms.ProgressBar prbIteration;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

