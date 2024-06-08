namespace Lab10OOP
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
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            button8 = new Button();
            richtextbox1 = new RichTextBox();
            panel1 = new PictureBox();
            panel2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)panel1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panel2).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(45, 319);
            button1.Name = "button1";
            button1.Size = new Size(230, 29);
            button1.TabIndex = 3;
            button1.Text = "Запуск 1 Потоку";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(45, 363);
            button2.Name = "button2";
            button2.Size = new Size(230, 29);
            button2.TabIndex = 4;
            button2.Text = "Зупинка 1 Потоку";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(425, 319);
            button3.Name = "button3";
            button3.Size = new Size(230, 29);
            button3.TabIndex = 5;
            button3.Text = "Запуск 2 Потоку";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(425, 354);
            button4.Name = "button4";
            button4.Size = new Size(230, 29);
            button4.TabIndex = 6;
            button4.Text = "Зупинка 2 Потоку";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(789, 319);
            button5.Name = "button5";
            button5.Size = new Size(230, 29);
            button5.TabIndex = 7;
            button5.Text = "Запуск 3 Потоку";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.Location = new Point(789, 354);
            button6.Name = "button6";
            button6.Size = new Size(230, 29);
            button6.TabIndex = 8;
            button6.Text = "Зупинка 3 Потоку";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button7
            // 
            button7.Location = new Point(27, 451);
            button7.Name = "button7";
            button7.Size = new Size(1003, 49);
            button7.TabIndex = 9;
            button7.Text = "Запуск всіх потоків";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // button8
            // 
            button8.Location = new Point(27, 506);
            button8.Name = "button8";
            button8.Size = new Size(1003, 49);
            button8.TabIndex = 10;
            button8.Text = "Зупинка всіх потоків";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // richtextbox1
            // 
            richtextbox1.Location = new Point(763, 12);
            richtextbox1.Name = "richtextbox1";
            richtextbox1.Size = new Size(316, 291);
            richtextbox1.TabIndex = 11;
            richtextbox1.Text = "";
            // 
            // panel1
            // 
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(313, 292);
            panel1.TabIndex = 12;
            panel1.TabStop = false;
            // 
            // panel2
            // 
            panel2.Location = new Point(381, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(326, 292);
            panel2.TabIndex = 13;
            panel2.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1091, 562);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(richtextbox1);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Form1";
            Text = "3 поточна программа";
            ((System.ComponentModel.ISupportInitialize)panel1).EndInit();
            ((System.ComponentModel.ISupportInitialize)panel2).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private RichTextBox richtextbox1;
        private PictureBox panel1;
        private PictureBox panel2;
    }
}
