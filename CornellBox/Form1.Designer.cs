namespace CornellBox
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.cornellBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cornellBox)).BeginInit();
            this.SuspendLayout();
            // 
            // cornellBox
            // 
            this.cornellBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cornellBox.Location = new System.Drawing.Point(1, 0);
            this.cornellBox.Margin = new System.Windows.Forms.Padding(2);
            this.cornellBox.Name = "cornellBox";
            this.cornellBox.Size = new System.Drawing.Size(692, 650);
            this.cornellBox.TabIndex = 0;
            this.cornellBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(765, 286);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Будущие кнопочки";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6, 13);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 650);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cornellBox);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Cornell Box";
            ((System.ComponentModel.ISupportInitialize)(this.cornellBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox cornellBox;
        private System.Windows.Forms.Label label1;
    }
}

