﻿namespace App
{
    partial class Start
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Start));
            this.eventList = new System.Windows.Forms.ComboBox();
            this.name = new System.Windows.Forms.TextBox();
            this.status = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.login = new System.Windows.Forms.TextBox();
            this.pass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.description = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // eventList
            // 
            this.eventList.BackColor = System.Drawing.Color.White;
            this.eventList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.eventList.ForeColor = System.Drawing.Color.Black;
            this.eventList.FormattingEnabled = true;
            this.eventList.Location = new System.Drawing.Point(15, 39);
            this.eventList.Name = "eventList";
            this.eventList.Size = new System.Drawing.Size(210, 23);
            this.eventList.TabIndex = 0;
            this.eventList.SelectedIndexChanged += new System.EventHandler(this.eventList_SelectedIndexChanged);
            this.eventList.SelectedValueChanged += new System.EventHandler(this.eventList_SelectedValueChanged);
            this.eventList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.eventList_KeyDown);
            this.eventList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.eventList_KeyPress);
            // 
            // name
            // 
            this.name.BackColor = System.Drawing.Color.AliceBlue;
            this.name.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.name.ForeColor = System.Drawing.Color.Black;
            this.name.Location = new System.Drawing.Point(243, 40);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(320, 21);
            this.name.TabIndex = 1;
            this.name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // status
            // 
            this.status.BackColor = System.Drawing.Color.AliceBlue;
            this.status.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.status.ForeColor = System.Drawing.Color.Black;
            this.status.Location = new System.Drawing.Point(317, 82);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(178, 21);
            this.status.TabIndex = 2;
            this.status.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Blue;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button1.Location = new System.Drawing.Point(327, 125);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(154, 47);
            this.button1.TabIndex = 4;
            this.button1.Text = "Подать обращение";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Blue;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button2.Location = new System.Drawing.Point(327, 189);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(155, 49);
            this.button2.TabIndex = 5;
            this.button2.Text = "Популярные обращения";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Blue;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button3.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button3.Location = new System.Drawing.Point(622, 68);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(106, 25);
            this.button3.TabIndex = 6;
            this.button3.Text = "Войти";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // login
            // 
            this.login.BackColor = System.Drawing.Color.WhiteSmoke;
            this.login.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.login.ForeColor = System.Drawing.Color.Black;
            this.login.Location = new System.Drawing.Point(590, 12);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(136, 20);
            this.login.TabIndex = 7;
            this.login.Text = "Логин";
            // 
            // pass
            // 
            this.pass.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pass.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pass.ForeColor = System.Drawing.Color.Black;
            this.pass.Location = new System.Drawing.Point(591, 38);
            this.pass.Name = "pass";
            this.pass.PasswordChar = '*';
            this.pass.Size = new System.Drawing.Size(136, 20);
            this.pass.TabIndex = 8;
            this.pass.Text = "******";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(34, 346);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 17);
            this.label1.TabIndex = 9;
            // 
            // description
            // 
            this.description.BackColor = System.Drawing.Color.AliceBlue;
            this.description.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.description.ForeColor = System.Drawing.Color.Black;
            this.description.Location = new System.Drawing.Point(12, 82);
            this.description.Multiline = true;
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(289, 251);
            this.description.TabIndex = 10;
            this.description.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Start
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(736, 381);
            this.Controls.Add(this.description);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pass);
            this.Controls.Add(this.login);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.status);
            this.Controls.Add(this.name);
            this.Controls.Add(this.eventList);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Start";
            this.Text = "Прямая линия с Владимиром Путиным";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox eventList;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.TextBox status;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox login;
        private System.Windows.Forms.TextBox pass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox description;
    }
}

