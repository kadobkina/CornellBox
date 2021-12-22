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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.buttonMirror = new System.Windows.Forms.Button();
            this.buttonMatt = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxCenter = new System.Windows.Forms.CheckBox();
            this.checkBoxLeftBack = new System.Windows.Forms.CheckBox();
            this.checkBoxRightBack = new System.Windows.Forms.CheckBox();
            this.checkBoxLeftFront = new System.Windows.Forms.CheckBox();
            this.checkBoxRightFront = new System.Windows.Forms.CheckBox();
            this.checkBoxCenterCloser = new System.Windows.Forms.CheckBox();
            this.buttonAccept = new System.Windows.Forms.Button();
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
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(726, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Сцена";
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Malgun Gothic Semilight", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 17;
            this.listBox1.Items.AddRange(new object[] {
            "Большой куб",
            "Маленький куб",
            "Большая сфера",
            "Маленькая сфера"});
            this.listBox1.Location = new System.Drawing.Point(729, 89);
            this.listBox1.Margin = new System.Windows.Forms.Padding(5);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(154, 89);
            this.listBox1.TabIndex = 2;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // buttonMirror
            // 
            this.buttonMirror.Enabled = false;
            this.buttonMirror.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonMirror.Location = new System.Drawing.Point(729, 216);
            this.buttonMirror.Name = "buttonMirror";
            this.buttonMirror.Size = new System.Drawing.Size(154, 43);
            this.buttonMirror.TabIndex = 3;
            this.buttonMirror.Text = "Сделать фигуру зеркальной";
            this.buttonMirror.UseVisualStyleBackColor = true;
            this.buttonMirror.Click += new System.EventHandler(this.buttonMirror_Click);
            // 
            // buttonMatt
            // 
            this.buttonMatt.Enabled = false;
            this.buttonMatt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonMatt.Location = new System.Drawing.Point(729, 279);
            this.buttonMatt.Name = "buttonMatt";
            this.buttonMatt.Size = new System.Drawing.Size(154, 43);
            this.buttonMatt.TabIndex = 4;
            this.buttonMatt.Text = "Сделать фигуру  матовой";
            this.buttonMatt.UseVisualStyleBackColor = true;
            this.buttonMatt.Click += new System.EventHandler(this.buttonMatt_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(726, 383);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Свет";
            // 
            // checkBoxCenter
            // 
            this.checkBoxCenter.AutoSize = true;
            this.checkBoxCenter.Location = new System.Drawing.Point(729, 413);
            this.checkBoxCenter.Name = "checkBoxCenter";
            this.checkBoxCenter.Size = new System.Drawing.Size(105, 17);
            this.checkBoxCenter.TabIndex = 6;
            this.checkBoxCenter.Text = "Центр комнаты";
            this.checkBoxCenter.UseVisualStyleBackColor = true;
            this.checkBoxCenter.CheckedChanged += new System.EventHandler(this.checkBoxCenter_CheckedChanged);
            // 
            // checkBoxLeftBack
            // 
            this.checkBoxLeftBack.AutoSize = true;
            this.checkBoxLeftBack.Location = new System.Drawing.Point(729, 459);
            this.checkBoxLeftBack.Name = "checkBoxLeftBack";
            this.checkBoxLeftBack.Size = new System.Drawing.Size(130, 17);
            this.checkBoxLeftBack.TabIndex = 7;
            this.checkBoxLeftBack.Text = "Левый дальний угол";
            this.checkBoxLeftBack.UseVisualStyleBackColor = true;
            this.checkBoxLeftBack.CheckedChanged += new System.EventHandler(this.checkBoxLeftBack_CheckedChanged);
            // 
            // checkBoxRightBack
            // 
            this.checkBoxRightBack.AutoSize = true;
            this.checkBoxRightBack.Location = new System.Drawing.Point(729, 482);
            this.checkBoxRightBack.Name = "checkBoxRightBack";
            this.checkBoxRightBack.Size = new System.Drawing.Size(136, 17);
            this.checkBoxRightBack.TabIndex = 8;
            this.checkBoxRightBack.Text = "Правый дальний угол";
            this.checkBoxRightBack.UseVisualStyleBackColor = true;
            this.checkBoxRightBack.CheckedChanged += new System.EventHandler(this.checkBoxRightBack_CheckedChanged);
            // 
            // checkBoxLeftFront
            // 
            this.checkBoxLeftFront.AutoSize = true;
            this.checkBoxLeftFront.Location = new System.Drawing.Point(729, 505);
            this.checkBoxLeftFront.Name = "checkBoxLeftFront";
            this.checkBoxLeftFront.Size = new System.Drawing.Size(132, 17);
            this.checkBoxLeftFront.TabIndex = 9;
            this.checkBoxLeftFront.Text = "Левый ближний угол";
            this.checkBoxLeftFront.UseVisualStyleBackColor = true;
            this.checkBoxLeftFront.CheckedChanged += new System.EventHandler(this.checkBoxLeftFront_CheckedChanged);
            // 
            // checkBoxRightFront
            // 
            this.checkBoxRightFront.AutoSize = true;
            this.checkBoxRightFront.Location = new System.Drawing.Point(729, 528);
            this.checkBoxRightFront.Name = "checkBoxRightFront";
            this.checkBoxRightFront.Size = new System.Drawing.Size(141, 17);
            this.checkBoxRightFront.TabIndex = 10;
            this.checkBoxRightFront.Text = "Правый ближний  угол";
            this.checkBoxRightFront.UseVisualStyleBackColor = true;
            this.checkBoxRightFront.CheckedChanged += new System.EventHandler(this.checkBoxRightFront_CheckedChanged);
            // 
            // checkBoxCenterCloser
            // 
            this.checkBoxCenterCloser.AutoSize = true;
            this.checkBoxCenterCloser.Checked = true;
            this.checkBoxCenterCloser.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCenterCloser.Location = new System.Drawing.Point(729, 436);
            this.checkBoxCenterCloser.Name = "checkBoxCenterCloser";
            this.checkBoxCenterCloser.Size = new System.Drawing.Size(142, 17);
            this.checkBoxCenterCloser.TabIndex = 11;
            this.checkBoxCenterCloser.Text = "Центр ближе к камере";
            this.checkBoxCenterCloser.UseVisualStyleBackColor = true;
            this.checkBoxCenterCloser.CheckedChanged += new System.EventHandler(this.checkBoxCenterCloser_CheckedChanged);
            // 
            // buttonAccept
            // 
            this.buttonAccept.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAccept.Location = new System.Drawing.Point(729, 551);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(154, 35);
            this.buttonAccept.TabIndex = 12;
            this.buttonAccept.Text = "Изменить свет";
            this.buttonAccept.UseVisualStyleBackColor = true;
            this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 650);
            this.Controls.Add(this.buttonAccept);
            this.Controls.Add(this.checkBoxCenterCloser);
            this.Controls.Add(this.checkBoxRightFront);
            this.Controls.Add(this.checkBoxLeftFront);
            this.Controls.Add(this.checkBoxRightBack);
            this.Controls.Add(this.checkBoxLeftBack);
            this.Controls.Add(this.checkBoxCenter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonMatt);
            this.Controls.Add(this.buttonMirror);
            this.Controls.Add(this.listBox1);
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
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button buttonMirror;
        private System.Windows.Forms.Button buttonMatt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxCenter;
        private System.Windows.Forms.CheckBox checkBoxLeftBack;
        private System.Windows.Forms.CheckBox checkBoxRightBack;
        private System.Windows.Forms.CheckBox checkBoxLeftFront;
        private System.Windows.Forms.CheckBox checkBoxRightFront;
        private System.Windows.Forms.CheckBox checkBoxCenterCloser;
        private System.Windows.Forms.Button buttonAccept;
    }
}

