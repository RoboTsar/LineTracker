namespace WeDo_Line_Tracker
{
    partial class Simple_Form
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
            this.Button_Start = new System.Windows.Forms.Button();
            this.Label_Left = new System.Windows.Forms.Label();
            this.Label_Straight = new System.Windows.Forms.Label();
            this.Label_Right = new System.Windows.Forms.Label();
            this.Button_Stop = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // Button_Start
            // 
            this.Button_Start.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold);
            this.Button_Start.Location = new System.Drawing.Point(6, 214);
            this.Button_Start.Margin = new System.Windows.Forms.Padding(0);
            this.Button_Start.Name = "Button_Start";
            this.Button_Start.Size = new System.Drawing.Size(146, 78);
            this.Button_Start.TabIndex = 0;
            this.Button_Start.Text = "Start";
            this.Button_Start.UseVisualStyleBackColor = true;
            this.Button_Start.Click += new System.EventHandler(this.Button_Start_Click);
            // 
            // Label_Left
            // 
            this.Label_Left.AutoSize = true;
            this.Label_Left.Font = new System.Drawing.Font("Microsoft Sans Serif", 110.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Left.ForeColor = System.Drawing.Color.Gray;
            this.Label_Left.Location = new System.Drawing.Point(9, 48);
            this.Label_Left.Margin = new System.Windows.Forms.Padding(0);
            this.Label_Left.Name = "Label_Left";
            this.Label_Left.Size = new System.Drawing.Size(174, 166);
            this.Label_Left.TabIndex = 2;
            this.Label_Left.Tag = "Arrow_Left";
            this.Label_Left.Text = "⇦";
            // 
            // Label_Straight
            // 
            this.Label_Straight.AutoSize = true;
            this.Label_Straight.Font = new System.Drawing.Font("Microsoft Sans Serif", 110.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Straight.ForeColor = System.Drawing.Color.Gray;
            this.Label_Straight.Location = new System.Drawing.Point(99, -32);
            this.Label_Straight.Margin = new System.Windows.Forms.Padding(0);
            this.Label_Straight.Name = "Label_Straight";
            this.Label_Straight.Size = new System.Drawing.Size(151, 166);
            this.Label_Straight.TabIndex = 3;
            this.Label_Straight.Tag = "Arrow_Up";
            this.Label_Straight.Text = "⇧";
            // 
            // Label_Right
            // 
            this.Label_Right.AutoSize = true;
            this.Label_Right.Font = new System.Drawing.Font("Microsoft Sans Serif", 110.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Right.ForeColor = System.Drawing.Color.Gray;
            this.Label_Right.Location = new System.Drawing.Point(154, 48);
            this.Label_Right.Margin = new System.Windows.Forms.Padding(0);
            this.Label_Right.Name = "Label_Right";
            this.Label_Right.Size = new System.Drawing.Size(174, 166);
            this.Label_Right.TabIndex = 4;
            this.Label_Right.Tag = "Arrow_Right";
            this.Label_Right.Text = "⇨";
            // 
            // Button_Stop
            // 
            this.Button_Stop.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold);
            this.Button_Stop.Location = new System.Drawing.Point(163, 214);
            this.Button_Stop.Margin = new System.Windows.Forms.Padding(0);
            this.Button_Stop.Name = "Button_Stop";
            this.Button_Stop.Size = new System.Drawing.Size(157, 78);
            this.Button_Stop.TabIndex = 5;
            this.Button_Stop.Text = "Stop";
            this.Button_Stop.UseVisualStyleBackColor = true;
            this.Button_Stop.Click += new System.EventHandler(this.Button_Stop_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(331, 15);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(219, 277);
            this.listBox1.TabIndex = 6;
            this.listBox1.Tag = "listbox";
            // 
            // Simple_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 294);
            this.Controls.Add(this.Label_Straight);
            this.Controls.Add(this.Label_Right);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.Button_Stop);
            this.Controls.Add(this.Label_Left);
            this.Controls.Add(this.Button_Start);
            this.Name = "Simple_Form";
            this.Text = "Simple_Form";
            this.Load += new System.EventHandler(this.Simple_Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Button_Start;
        private System.Windows.Forms.Label Label_Left;
        private System.Windows.Forms.Label Label_Straight;
        private System.Windows.Forms.Label Label_Right;
        private System.Windows.Forms.Button Button_Stop;
        private System.Windows.Forms.ListBox listBox1;
    }
}