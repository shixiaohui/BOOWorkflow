namespace BOODemo.View
{
    partial class WaiterForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(497, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "This client is used by waiters to create order and sent to kitchen or make paymen" +
    "t";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Location = new System.Drawing.Point(14, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(357, 312);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Orders";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(7, 20);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(179, 280);
            this.listBox1.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(193, 21);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(158, 206);
            this.textBox1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(220, 232);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 31);
            this.button1.TabIndex = 3;
            this.button1.Text = "Add Item";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(220, 269);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 31);
            this.button2.TabIndex = 4;
            this.button2.Text = "Make Payment";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(393, 48);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(104, 31);
            this.button3.TabIndex = 5;
            this.button3.Text = "Create Order";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBox2.Location = new System.Drawing.Point(387, 87);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(122, 125);
            this.textBox2.TabIndex = 6;
            this.textBox2.TabStop = false;
            this.textBox2.Text = "Create order button will create a new guest order using a Order Create Form, whic" +
    "h is able to have multi-instance to simulate several waiters making up order in " +
    "the same time";
            // 
            // WaiterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 349);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WaiterForm";
            this.ShowIcon = false;
            this.Text = "WaiterForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox2;
    }
}