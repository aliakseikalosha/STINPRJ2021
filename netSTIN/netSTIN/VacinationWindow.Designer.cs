
namespace netSTIN
{
    partial class VacinationWindow
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
            this.ChangeScreen = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.nextDay = new System.Windows.Forms.Button();
            this.prevDay = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ChangeScreen
            // 
            this.ChangeScreen.Location = new System.Drawing.Point(450, 660);
            this.ChangeScreen.Name = "ChangeScreen";
            this.ChangeScreen.Size = new System.Drawing.Size(99, 23);
            this.ChangeScreen.TabIndex = 1;
            this.ChangeScreen.Text = "ChangeScreen";
            this.ChangeScreen.UseVisualStyleBackColor = true;
            this.ChangeScreen.Click += new System.EventHandler(this.ChangeScreen_Click);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(429, 12);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(146, 68);
            this.update.TabIndex = 6;
            this.update.Text = "Update";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // nextDay
            // 
            this.nextDay.Location = new System.Drawing.Point(820, 25);
            this.nextDay.Name = "nextDay";
            this.nextDay.Size = new System.Drawing.Size(181, 42);
            this.nextDay.TabIndex = 5;
            this.nextDay.Text = "button2";
            this.nextDay.UseVisualStyleBackColor = true;
            this.nextDay.Click += new System.EventHandler(this.nextDay_Click);
            // 
            // prevDay
            // 
            this.prevDay.Location = new System.Drawing.Point(17, 25);
            this.prevDay.Name = "prevDay";
            this.prevDay.Size = new System.Drawing.Size(181, 42);
            this.prevDay.TabIndex = 4;
            this.prevDay.Text = "button1";
            this.prevDay.UseVisualStyleBackColor = true;
            this.prevDay.Click += new System.EventHandler(this.prevDay_Click);
            // 
            // VacinationWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.update);
            this.Controls.Add(this.nextDay);
            this.Controls.Add(this.prevDay);
            this.Controls.Add(this.ChangeScreen);
            this.Name = "VacinationWindow";
            this.Text = "VacinationWindow";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ChangeScreen;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.Button nextDay;
        private System.Windows.Forms.Button prevDay;
    }
}