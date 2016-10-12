namespace LumberJacker
{
    partial class GUI
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
            this.headPositionSelector = new System.Windows.Forms.Button();
            this.headPos = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // headPositionSelector
            // 
            this.headPositionSelector.Location = new System.Drawing.Point(13, 13);
            this.headPositionSelector.Name = "headPositionSelector";
            this.headPositionSelector.Size = new System.Drawing.Size(143, 36);
            this.headPositionSelector.TabIndex = 0;
            this.headPositionSelector.Text = "Select head position";
            this.headPositionSelector.UseVisualStyleBackColor = true;
            this.headPositionSelector.Click += new System.EventHandler(this.headPositionSelector_Click);
            // 
            // headPos
            // 
            this.headPos.AutoSize = true;
            this.headPos.Location = new System.Drawing.Point(13, 56);
            this.headPos.Name = "headPos";
            this.headPos.Size = new System.Drawing.Size(59, 13);
            this.headPos.TabIndex = 1;
            this.headPos.Text = "Head pos: ";
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.headPos);
            this.Controls.Add(this.headPositionSelector);
            this.Name = "GUI";
            this.Text = "LumberJacker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button headPositionSelector;
        private System.Windows.Forms.Label headPos;
    }
}

