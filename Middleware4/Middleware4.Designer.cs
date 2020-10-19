namespace Middleware4
{
    partial class Middleware4
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
            this.Label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SendButton = new System.Windows.Forms.Button();
            this.SendBox = new System.Windows.Forms.ListBox();
            this.ReceivedBox = new System.Windows.Forms.ListBox();
            this.ReadyBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(47, 87);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(47, 20);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Send";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(335, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Received";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(604, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Ready";
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(51, 35);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(89, 30);
            this.SendButton.TabIndex = 3;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.sendButton__Click);
            // 
            // SendBox
            // 
            this.SendBox.FormattingEnabled = true;
            this.SendBox.HorizontalScrollbar = true;
            this.SendBox.ItemHeight = 12;
            this.SendBox.Location = new System.Drawing.Point(51, 119);
            this.SendBox.Name = "SendBox";
            this.SendBox.Size = new System.Drawing.Size(164, 388);
            this.SendBox.TabIndex = 4;
            // 
            // ReceivedBox
            // 
            this.ReceivedBox.FormattingEnabled = true;
            this.ReceivedBox.HorizontalScrollbar = true;
            this.ReceivedBox.ItemHeight = 12;
            this.ReceivedBox.Location = new System.Drawing.Point(339, 119);
            this.ReceivedBox.Name = "ReceivedBox";
            this.ReceivedBox.Size = new System.Drawing.Size(164, 388);
            this.ReceivedBox.TabIndex = 5;
            // 
            // ReadyBox
            // 
            this.ReadyBox.FormattingEnabled = true;
            this.ReadyBox.HorizontalScrollbar = true;
            this.ReadyBox.ItemHeight = 12;
            this.ReadyBox.Location = new System.Drawing.Point(608, 119);
            this.ReadyBox.Name = "ReadyBox";
            this.ReadyBox.Size = new System.Drawing.Size(164, 388);
            this.ReadyBox.TabIndex = 6;
            // 
            // Middleware4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 518);
            this.Controls.Add(this.ReadyBox);
            this.Controls.Add(this.ReceivedBox);
            this.Controls.Add(this.SendBox);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Label1);
            this.Name = "Middleware4";
            this.Text = "MiddleWare4";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.ListBox SendBox;
        private System.Windows.Forms.ListBox ReceivedBox;
        private System.Windows.Forms.ListBox ReadyBox;
    }
}

