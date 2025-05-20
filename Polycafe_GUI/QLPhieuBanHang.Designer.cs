using System.Drawing;
using System.Windows.Forms;

namespace Polycafe_GUI
{
    partial class QLPhieuBanHang
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QLPhieuBanHang));
            groupBox2 = new GroupBox();
            dataGridView1 = new DataGridView();
            groupBox1 = new GroupBox();
            radioButton2 = new RadioButton();
            dateTimePicker1 = new DateTimePicker();
            comboBox2 = new ComboBox();
            label5 = new Label();
            textBox2 = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            radioButton1 = new RadioButton();
            comboBox1 = new ComboBox();
            comboBox3 = new ComboBox();
            textBox1 = new TextBox();
            textBox3 = new TextBox();
            numericUpDown1 = new NumericUpDown();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            comboBox4 = new ComboBox();
            label11 = new Label();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dataGridView1);
            groupBox2.Location = new Point(64, 332);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(758, 270);
            groupBox2.TabIndex = 10;
            groupBox2.TabStop = false;
            groupBox2.Text = "Quản lý";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 23);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(752, 244);
            dataGridView1.TabIndex = 5;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(button4);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(numericUpDown1);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(textBox3);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(comboBox3);
            groupBox1.Controls.Add(radioButton1);
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Controls.Add(dateTimePicker1);
            groupBox1.Controls.Add(comboBox2);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(21, 108);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(866, 218);
            groupBox1.TabIndex = 9;
            groupBox1.TabStop = false;
            groupBox1.Text = "Thông tin loại sản phẩm";
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(308, 177);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(124, 24);
            radioButton2.TabIndex = 19;
            radioButton2.TabStop = true;
            radioButton2.Text = "Đã thanh toán";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(110, 78);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(142, 27);
            dateTimePicker1.TabIndex = 17;
            //            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(359, 36);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(105, 28);
            comboBox2.TabIndex = 16;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(271, 82);
            label5.Name = "label5";
            label5.Size = new Size(75, 20);
            label5.TabIndex = 10;
            label5.Text = "Sản phẩm";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(110, 36);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(142, 27);
            textBox2.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(271, 39);
            label4.Name = "label4";
            label4.Size = new Size(55, 20);
            label4.TabIndex = 7;
            label4.Text = "Mã thẻ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 82);
            label3.Name = "label3";
            label3.Size = new Size(70, 20);
            label3.TabIndex = 6;
            label3.Text = "Ngày tạo";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 39);
            label2.Name = "label2";
            label2.Size = new Size(71, 20);
            label2.TabIndex = 5;
            label2.Text = "Mã phiếu";
            // 
            // button4
            // 
            button4.Location = new Point(758, 171);
            button4.Name = "button4";
            button4.Size = new Size(80, 30);
            button4.TabIndex = 15;
            button4.Text = "Quay lại";
            button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(758, 121);
            button3.Name = "button3";
            button3.Size = new Size(80, 30);
            button3.TabIndex = 14;
            button3.Text = "Xóa";
            button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(758, 78);
            button2.Name = "button2";
            button2.Size = new Size(80, 30);
            button2.TabIndex = 13;
            button2.Text = "Cập nhật";
            button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(758, 34);
            button1.Name = "button1";
            button1.Size = new Size(80, 30);
            button1.TabIndex = 12;
            button1.Text = "Thêm";
            button1.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(21, 21);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(184, 81);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            //       pictureBox1.Click += pictureBox1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Black", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(292, 21);
            label1.Name = "label1";
            label1.Size = new Size(366, 41);
            label1.TabIndex = 7;
            label1.Text = "Quản lý phiếu bán hàng";
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(308, 147);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(118, 24);
            radioButton1.TabIndex = 18;
            radioButton1.TabStop = true;
            radioButton1.Text = "Chờ xác nhận";
            radioButton1.UseVisualStyleBackColor = true;
            //      radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(110, 121);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(142, 28);
            comboBox1.TabIndex = 20;
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(359, 80);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(105, 28);
            comboBox3.TabIndex = 21;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(586, 81);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(118, 27);
            textBox1.TabIndex = 22;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(586, 134);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(118, 27);
            textBox3.TabIndex = 23;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(586, 36);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(79, 27);
            numericUpDown1.TabIndex = 24;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(15, 124);
            label6.Name = "label6";
            label6.Size = new Size(75, 20);
            label6.TabIndex = 25;
            label6.Text = "Nhân viên";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(271, 124);
            label7.Name = "label7";
            label7.Size = new Size(75, 20);
            label7.TabIndex = 26;
            label7.Text = "Trạng thái";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(490, 39);
            label8.Name = "label8";
            label8.Size = new Size(69, 20);
            label8.TabIndex = 27;
            label8.Text = "Số lượng";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(490, 83);
            label9.Name = "label9";
            label9.Size = new Size(66, 20);
            label9.TabIndex = 28;
            label9.Text = "Đơn giá ";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(490, 137);
            label10.Name = "label10";
            label10.Size = new Size(78, 20);
            label10.TabIndex = 29;
            label10.Text = "Thành tiền";
            // 
            // comboBox4
            // 
            comboBox4.FormattingEnabled = true;
            comboBox4.Location = new Point(339, 74);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new Size(275, 28);
            comboBox4.TabIndex = 30;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(244, 77);
            label11.Name = "label11";
            label11.Size = new Size(70, 20);
            label11.TabIndex = 30;
            label11.Text = "Tìm kiếm";
            // 
            // QLPhieuBanHang
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label11);
            Controls.Add(comboBox4);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Name = "QLPhieuBanHang";
            Size = new Size(943, 622);
            Load += QLPhieuBanHang_Load;
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox2;
        private DataGridView dataGridView1;
        private GroupBox groupBox1;
        private RadioButton radioButton2;
        private DateTimePicker dateTimePicker1;
        private ComboBox comboBox2;
        private Label label5;
        private TextBox textBox2;
        private Label label4;
        private Label label3;
        private Label label2;
        private Button button4;
        private Button button3;
        private Button button2;
        private Button button1;
        private PictureBox pictureBox1;
        private Label label1;
        private RadioButton radioButton1;
        private ComboBox comboBox3;
        private ComboBox comboBox1;
        private Label label7;
        private Label label6;
        private NumericUpDown numericUpDown1;
        private TextBox textBox3;
        private TextBox textBox1;
        private Label label10;
        private Label label9;
        private Label label8;
        private ComboBox comboBox4;
        private Label label11;
    }
}
