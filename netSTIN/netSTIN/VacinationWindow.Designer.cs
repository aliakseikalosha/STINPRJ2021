
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
            this.combo1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.combo2 = new System.Windows.Forms.ComboBox();
            this.combo3 = new System.Windows.Forms.ComboBox();
            this.combo4 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelCzech = new System.Windows.Forms.Label();
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
            this.nextDay.Location = new System.Drawing.Point(815, 25);
            this.nextDay.Name = "nextDay";
            this.nextDay.Size = new System.Drawing.Size(181, 42);
            this.nextDay.TabIndex = 5;
            this.nextDay.Text = "button2";
            this.nextDay.UseVisualStyleBackColor = true;
            this.nextDay.Click += new System.EventHandler(this.nextDay_Click);
            // 
            // prevDay
            // 
            this.prevDay.Location = new System.Drawing.Point(12, 25);
            this.prevDay.Name = "prevDay";
            this.prevDay.Size = new System.Drawing.Size(181, 42);
            this.prevDay.TabIndex = 4;
            this.prevDay.Text = "button1";
            this.prevDay.UseVisualStyleBackColor = true;
            this.prevDay.Click += new System.EventHandler(this.prevDay_Click);
            // 
            // combo1
            // 
            this.combo1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.combo1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combo1.FormattingEnabled = true;
            this.combo1.Location = new System.Drawing.Point(43, 252);
            this.combo1.Name = "combo1";
            this.combo1.Size = new System.Drawing.Size(121, 23);
            this.combo1.Sorted = true;
            this.combo1.TabIndex = 7;
            this.combo1.SelectedIndexChanged += new System.EventHandler(this.SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(198, 243);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 32);
            this.label1.TabIndex = 8;
            this.label1.Text = "No country selected";
            // 
            // combo2
            // 
            this.combo2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.combo2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combo2.FormattingEnabled = true;
            this.combo2.Location = new System.Drawing.Point(43, 348);
            this.combo2.Name = "combo2";
            this.combo2.Size = new System.Drawing.Size(121, 23);
            this.combo2.Sorted = true;
            this.combo2.TabIndex = 9;
            this.combo2.SelectedIndexChanged += new System.EventHandler(this.SelectionChangeCommitted);
            // 
            // combo3
            // 
            this.combo3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.combo3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combo3.FormattingEnabled = true;
            this.combo3.Location = new System.Drawing.Point(43, 454);
            this.combo3.Name = "combo3";
            this.combo3.Size = new System.Drawing.Size(121, 23);
            this.combo3.Sorted = true;
            this.combo3.TabIndex = 10;
            this.combo3.SelectedIndexChanged += new System.EventHandler(this.SelectionChangeCommitted);
            // 
            // combo4
            // 
            this.combo4.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.combo4.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combo4.FormattingEnabled = true;
            this.combo4.Location = new System.Drawing.Point(43, 548);
            this.combo4.Name = "combo4";
            this.combo4.Size = new System.Drawing.Size(121, 23);
            this.combo4.Sorted = true;
            this.combo4.TabIndex = 11;
            this.combo4.SelectedIndexChanged += new System.EventHandler(this.SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(198, 337);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(229, 32);
            this.label2.TabIndex = 12;
            this.label2.Text = "No country selected";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(198, 445);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(229, 32);
            this.label3.TabIndex = 13;
            this.label3.Text = "No country selected";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(198, 539);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(229, 32);
            this.label4.TabIndex = 14;
            this.label4.Text = "No country selected";
            // 
            // labelCzech
            // 
            this.labelCzech.AutoSize = true;
            this.labelCzech.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelCzech.Location = new System.Drawing.Point(198, 155);
            this.labelCzech.Name = "labelCzech";
            this.labelCzech.Size = new System.Drawing.Size(78, 32);
            this.labelCzech.TabIndex = 15;
            this.labelCzech.Text = "Czech";
            // 
            // VacinationWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.labelCzech);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.combo4);
            this.Controls.Add(this.combo3);
            this.Controls.Add(this.combo2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.combo1);
            this.Controls.Add(this.update);
            this.Controls.Add(this.nextDay);
            this.Controls.Add(this.prevDay);
            this.Controls.Add(this.ChangeScreen);
            this.MaximizeBox = false;
            this.Name = "VacinationWindow";
            this.Text = "VacinationWindow";
            this.Shown += new System.EventHandler(this.VacinationWindow_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ChangeScreen;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.Button nextDay;
        private System.Windows.Forms.Button prevDay;
        private System.Windows.Forms.ComboBox combo1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox combo2;
        private System.Windows.Forms.ComboBox combo3;
        private System.Windows.Forms.ComboBox combo4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelCzech;
    }
}