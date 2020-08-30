namespace ArtistsConnectionsFramework
{
    partial class ArtistsConnectionsForm
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
            this.ListBox1 = new System.Windows.Forms.ListBox();
            this.ListBox2 = new System.Windows.Forms.ListBox();
            this.ListBox3 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ListBox1
            // 
            this.ListBox1.FormattingEnabled = true;
            this.ListBox1.HorizontalScrollbar = true;
            this.ListBox1.Location = new System.Drawing.Point(12, 52);
            this.ListBox1.Name = "ListBox1";
            this.ListBox1.Size = new System.Drawing.Size(204, 329);
            this.ListBox1.TabIndex = 0;
            // 
            // ListBox2
            // 
            this.ListBox2.FormattingEnabled = true;
            this.ListBox2.HorizontalScrollbar = true;
            this.ListBox2.Location = new System.Drawing.Point(222, 52);
            this.ListBox2.Name = "ListBox2";
            this.ListBox2.Size = new System.Drawing.Size(214, 329);
            this.ListBox2.TabIndex = 1;
            // 
            // ListBox3
            // 
            this.ListBox3.FormattingEnabled = true;
            this.ListBox3.HorizontalScrollbar = true;
            this.ListBox3.Location = new System.Drawing.Point(442, 52);
            this.ListBox3.Name = "ListBox3";
            this.ListBox3.Size = new System.Drawing.Size(219, 329);
            this.ListBox3.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(222, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(442, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "label3";
            // 
            // ArtistsConnectionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 393);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListBox3);
            this.Controls.Add(this.ListBox2);
            this.Controls.Add(this.ListBox1);
            this.Name = "ArtistsConnectionsForm";
            this.Text = "Artists\' Connections";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox ListBox1;
        private System.Windows.Forms.ListBox ListBox2;
        private System.Windows.Forms.ListBox ListBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

