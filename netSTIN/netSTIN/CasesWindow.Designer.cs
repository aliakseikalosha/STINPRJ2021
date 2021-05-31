
namespace netSTIN
{
    partial class CasesWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ChangeScreen = new System.Windows.Forms.Button();
            this.prevDay = new System.Windows.Forms.Button();
            this.nextDay = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.Czechia = new System.Windows.Forms.Label();
            this.WHO = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ChangeScreen
            // 
            this.ChangeScreen.Location = new System.Drawing.Point(450, 660);
            this.ChangeScreen.Name = "ChangeScreen";
            this.ChangeScreen.Size = new System.Drawing.Size(99, 23);
            this.ChangeScreen.TabIndex = 0;
            this.ChangeScreen.Text = "ChangeScreen";
            this.ChangeScreen.UseVisualStyleBackColor = true;
            this.ChangeScreen.Click += new System.EventHandler(this.ChangeScreen_Click);
            // 
            // prevDay
            // 
            this.prevDay.Location = new System.Drawing.Point(12, 25);
            this.prevDay.Name = "prevDay";
            this.prevDay.Size = new System.Drawing.Size(181, 42);
            this.prevDay.TabIndex = 1;
            this.prevDay.Text = "button1";
            this.prevDay.UseVisualStyleBackColor = true;
            this.prevDay.Click += new System.EventHandler(this.prevDay_Click);
            // 
            // nextDay
            // 
            this.nextDay.Location = new System.Drawing.Point(815, 25);
            this.nextDay.Name = "nextDay";
            this.nextDay.Size = new System.Drawing.Size(181, 42);
            this.nextDay.TabIndex = 2;
            this.nextDay.Text = "button2";
            this.nextDay.UseVisualStyleBackColor = true;
            this.nextDay.Click += new System.EventHandler(this.nextDay_Click);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(421, 12);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(146, 68);
            this.update.TabIndex = 3;
            this.update.Text = "Update";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // Czechia
            // 
            this.Czechia.AutoSize = true;
            this.Czechia.Location = new System.Drawing.Point(49, 183);
            this.Czechia.Name = "Czechia";
            this.Czechia.Size = new System.Drawing.Size(38, 15);
            this.Czechia.TabIndex = 4;
            this.Czechia.Text = "label1";
            // 
            // WHO
            // 
            this.WHO.AutoSize = true;
            this.WHO.Location = new System.Drawing.Point(577, 208);
            this.WHO.Name = "WHO";
            this.WHO.Size = new System.Drawing.Size(38, 15);
            this.WHO.TabIndex = 5;
            this.WHO.Text = "label2";
            // 
            // CasesWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.WHO);
            this.Controls.Add(this.Czechia);
            this.Controls.Add(this.update);
            this.Controls.Add(this.nextDay);
            this.Controls.Add(this.prevDay);
            this.Controls.Add(this.ChangeScreen);
            this.Name = "CasesWindow";
            this.Text = "CasesWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ChangeScreen;
        private System.Windows.Forms.Button prevDay;
        private System.Windows.Forms.Button nextDay;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.Label Czechia;
        private System.Windows.Forms.Label WHO;
    }
}

