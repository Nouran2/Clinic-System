namespace ClinicSystem
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panel1 = new Panel();
            pictureBox2 = new PictureBox();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            label2 = new Label();
            label3 = new Label();
            RoleCb = new ComboBox();
            UserTb = new TextBox();
            PasswordTb = new TextBox();
            LoginBtn = new Button();
            label4 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Teal;
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(437, 112);
            panel1.TabIndex = 0;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(412, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(25, 27);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 10;
            pictureBox2.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial Rounded MT Bold", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.Window;
            label1.Location = new Point(21, 40);
            label1.Name = "label1";
            label1.Size = new Size(386, 32);
            label1.TabIndex = 1;
            label1.Text = "Clinic Management System";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(162, 103);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(101, 83);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.Teal;
            label2.Location = new Point(55, 303);
            label2.Name = "label2";
            label2.Size = new Size(111, 23);
            label2.TabIndex = 3;
            label2.Text = "Username";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.Teal;
            label3.Location = new Point(59, 373);
            label3.Name = "label3";
            label3.Size = new Size(107, 23);
            label3.TabIndex = 4;
            label3.Text = "Password";
            // 
            // RoleCb
            // 
            RoleCb.FormattingEnabled = true;
            RoleCb.Items.AddRange(new object[] { "Admin", "Doctor", "Receptionist" });
            RoleCb.Location = new Point(103, 236);
            RoleCb.Name = "RoleCb";
            RoleCb.Size = new Size(222, 28);
            RoleCb.TabIndex = 5;
            // 
            // UserTb
            // 
            UserTb.Location = new Point(188, 303);
            UserTb.Name = "UserTb";
            UserTb.Size = new Size(180, 27);
            UserTb.TabIndex = 6;
            // 
            // PasswordTb
            // 
            PasswordTb.Location = new Point(188, 369);
            PasswordTb.Name = "PasswordTb";
            PasswordTb.Size = new Size(180, 27);
            PasswordTb.TabIndex = 7;
            // 
            // LoginBtn
            // 
            LoginBtn.AutoEllipsis = true;
            LoginBtn.BackColor = Color.Teal;
            LoginBtn.BackgroundImageLayout = ImageLayout.None;
            LoginBtn.FlatStyle = FlatStyle.Flat;
            LoginBtn.Font = new Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LoginBtn.ForeColor = SystemColors.Window;
            LoginBtn.Location = new Point(114, 460);
            LoginBtn.Name = "LoginBtn";
            LoginBtn.Size = new Size(187, 42);
            LoginBtn.TabIndex = 8;
            LoginBtn.Text = "Login";
            LoginBtn.UseVisualStyleBackColor = false;
            LoginBtn.Click += LoginBtn_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Arial Rounded MT Bold", 12F, FontStyle.Underline, GraphicsUnit.Point);
            label4.ForeColor = Color.Teal;
            label4.Location = new Point(175, 505);
            label4.Name = "label4";
            label4.Size = new Size(66, 23);
            label4.TabIndex = 9;
            label4.Text = "Reset";
            label4.Click += label4_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(437, 596);
            Controls.Add(label4);
            Controls.Add(LoginBtn);
            Controls.Add(PasswordTb);
            Controls.Add(UserTb);
            Controls.Add(RoleCb);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(pictureBox1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Form1";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private PictureBox pictureBox1;
        private Label label2;
        private Label label3;
        private ComboBox RoleCb;
        private TextBox UserTb;
        private TextBox PasswordTb;
        private Button LoginBtn;
        private Label label4;
        private PictureBox pictureBox2;
    }
}