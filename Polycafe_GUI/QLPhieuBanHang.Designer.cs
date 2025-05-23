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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvSaleInvoiceDetails = new System.Windows.Forms.DataGridView();
            this.dgvSaleInvoices = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.rdoPaid = new System.Windows.Forms.RadioButton();
            this.btnResetInvoice = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btnDeleteInvoice = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.rdoPending = new System.Windows.Forms.RadioButton();
            this.cboEmployeeId = new System.Windows.Forms.ComboBox();
            this.btnUpdateInvoice = new System.Windows.Forms.Button();
            this.btnAddInvoice = new System.Windows.Forms.Button();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.dtpCreatedDate = new System.Windows.Forms.DateTimePicker();
            this.cboCardId = new System.Windows.Forms.ComboBox();
            this.txtInvoiceId = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboProductId = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboFind = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnResetDetail = new System.Windows.Forms.Button();
            this.btnDeleteDetail = new System.Windows.Forms.Button();
            this.btnUpdateDetail = new System.Windows.Forms.Button();
            this.btnAddDetail = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.txtUnitPrice = new System.Windows.Forms.TextBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleInvoiceDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleInvoices)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvSaleInvoiceDetails);
            this.groupBox2.Controls.Add(this.dgvSaleInvoices);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox2.Location = new System.Drawing.Point(3, 409);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1646, 507);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Quản lý";
            // 
            // dgvSaleInvoiceDetails
            // 
            this.dgvSaleInvoiceDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSaleInvoiceDetails.Location = new System.Drawing.Point(846, 25);
            this.dgvSaleInvoiceDetails.Name = "dgvSaleInvoiceDetails";
            this.dgvSaleInvoiceDetails.RowHeadersWidth = 51;
            this.dgvSaleInvoiceDetails.Size = new System.Drawing.Size(794, 482);
            this.dgvSaleInvoiceDetails.TabIndex = 6;
            this.dgvSaleInvoiceDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSaleInvoiceDetails_CellClick);
            // 
            // dgvSaleInvoices
            // 
            this.dgvSaleInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSaleInvoices.Location = new System.Drawing.Point(3, 23);
            this.dgvSaleInvoices.Name = "dgvSaleInvoices";
            this.dgvSaleInvoices.RowHeadersWidth = 51;
            this.dgvSaleInvoices.Size = new System.Drawing.Size(836, 484);
            this.dgvSaleInvoices.TabIndex = 5;
            this.dgvSaleInvoices.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSaleInvoices_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.rdoPaid);
            this.groupBox1.Controls.Add(this.btnResetInvoice);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btnDeleteInvoice);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.rdoPending);
            this.groupBox1.Controls.Add(this.cboEmployeeId);
            this.groupBox1.Controls.Add(this.btnUpdateInvoice);
            this.groupBox1.Controls.Add(this.btnAddInvoice);
            this.groupBox1.Controls.Add(this.txtTotalAmount);
            this.groupBox1.Controls.Add(this.dtpCreatedDate);
            this.groupBox1.Controls.Add(this.cboCardId);
            this.groupBox1.Controls.Add(this.txtInvoiceId);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox1.Location = new System.Drawing.Point(24, 154);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(818, 258);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin phiếu";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.label10.Location = new System.Drawing.Point(450, 41);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(133, 30);
            this.label10.TabIndex = 39;
            this.label10.Text = "Thành tiền";
            // 
            // rdoPaid
            // 
            this.rdoPaid.AutoSize = true;
            this.rdoPaid.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.rdoPaid.Location = new System.Drawing.Point(594, 129);
            this.rdoPaid.Name = "rdoPaid";
            this.rdoPaid.Size = new System.Drawing.Size(197, 34);
            this.rdoPaid.TabIndex = 19;
            this.rdoPaid.TabStop = true;
            this.rdoPaid.Text = "Đã thanh toán";
            this.rdoPaid.UseVisualStyleBackColor = true;
            // 
            // btnResetInvoice
            // 
            this.btnResetInvoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnResetInvoice.Location = new System.Drawing.Point(602, 209);
            this.btnResetInvoice.Name = "btnResetInvoice";
            this.btnResetInvoice.Size = new System.Drawing.Size(153, 43);
            this.btnResetInvoice.TabIndex = 33;
            this.btnResetInvoice.Text = "Quay lại";
            this.btnResetInvoice.UseVisualStyleBackColor = true;
            this.btnResetInvoice.Click += new System.EventHandler(this.btnResetInvoice_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.label7.Location = new System.Drawing.Point(450, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 30);
            this.label7.TabIndex = 26;
            this.label7.Text = "Trạng thái";
            // 
            // btnDeleteInvoice
            // 
            this.btnDeleteInvoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnDeleteInvoice.Location = new System.Drawing.Point(427, 209);
            this.btnDeleteInvoice.Name = "btnDeleteInvoice";
            this.btnDeleteInvoice.Size = new System.Drawing.Size(133, 43);
            this.btnDeleteInvoice.TabIndex = 32;
            this.btnDeleteInvoice.Text = "Xóa";
            this.btnDeleteInvoice.UseVisualStyleBackColor = true;
            this.btnDeleteInvoice.Click += new System.EventHandler(this.btnDeleteInvoice_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label6.Location = new System.Drawing.Point(15, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 29);
            this.label6.TabIndex = 25;
            this.label6.Text = "Mã NV";
            // 
            // rdoPending
            // 
            this.rdoPending.AutoSize = true;
            this.rdoPending.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.rdoPending.Location = new System.Drawing.Point(598, 87);
            this.rdoPending.Name = "rdoPending";
            this.rdoPending.Size = new System.Drawing.Size(194, 34);
            this.rdoPending.TabIndex = 18;
            this.rdoPending.TabStop = true;
            this.rdoPending.Text = "Chờ xác nhận";
            this.rdoPending.UseVisualStyleBackColor = true;
            // 
            // cboEmployeeId
            // 
            this.cboEmployeeId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cboEmployeeId.FormattingEnabled = true;
            this.cboEmployeeId.Location = new System.Drawing.Point(160, 108);
            this.cboEmployeeId.Name = "cboEmployeeId";
            this.cboEmployeeId.Size = new System.Drawing.Size(267, 28);
            this.cboEmployeeId.TabIndex = 20;
            // 
            // btnUpdateInvoice
            // 
            this.btnUpdateInvoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnUpdateInvoice.Location = new System.Drawing.Point(223, 209);
            this.btnUpdateInvoice.Name = "btnUpdateInvoice";
            this.btnUpdateInvoice.Size = new System.Drawing.Size(149, 43);
            this.btnUpdateInvoice.TabIndex = 31;
            this.btnUpdateInvoice.Text = "Cập nhật";
            this.btnUpdateInvoice.UseVisualStyleBackColor = true;
            this.btnUpdateInvoice.Click += new System.EventHandler(this.btnUpdateInvoice_Click);
            // 
            // btnAddInvoice
            // 
            this.btnAddInvoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnAddInvoice.Location = new System.Drawing.Point(46, 209);
            this.btnAddInvoice.Name = "btnAddInvoice";
            this.btnAddInvoice.Size = new System.Drawing.Size(131, 43);
            this.btnAddInvoice.TabIndex = 30;
            this.btnAddInvoice.Text = "Thêm";
            this.btnAddInvoice.UseVisualStyleBackColor = true;
            this.btnAddInvoice.Click += new System.EventHandler(this.btnAddInvoice_Click);
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtTotalAmount.Location = new System.Drawing.Point(598, 35);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(203, 26);
            this.txtTotalAmount.TabIndex = 35;
            // 
            // dtpCreatedDate
            // 
            this.dtpCreatedDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dtpCreatedDate.Location = new System.Drawing.Point(160, 152);
            this.dtpCreatedDate.Name = "dtpCreatedDate";
            this.dtpCreatedDate.Size = new System.Drawing.Size(342, 28);
            this.dtpCreatedDate.TabIndex = 17;
            // 
            // cboCardId
            // 
            this.cboCardId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cboCardId.FormattingEnabled = true;
            this.cboCardId.Location = new System.Drawing.Point(160, 71);
            this.cboCardId.Name = "cboCardId";
            this.cboCardId.Size = new System.Drawing.Size(267, 28);
            this.cboCardId.TabIndex = 16;
            // 
            // txtInvoiceId
            // 
            this.txtInvoiceId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtInvoiceId.Location = new System.Drawing.Point(160, 35);
            this.txtInvoiceId.Name = "txtInvoiceId";
            this.txtInvoiceId.Size = new System.Drawing.Size(267, 26);
            this.txtInvoiceId.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label4.Location = new System.Drawing.Point(15, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 29);
            this.label4.TabIndex = 7;
            this.label4.Text = "Mã thẻ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.Location = new System.Drawing.Point(16, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 29);
            this.label3.TabIndex = 6;
            this.label3.Text = "Ngày tạo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(15, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 29);
            this.label2.TabIndex = 5;
            this.label2.Text = "Mã phiếu";
            // 
            // cboProductId
            // 
            this.cboProductId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cboProductId.FormattingEnabled = true;
            this.cboProductId.Location = new System.Drawing.Point(200, 66);
            this.cboProductId.Name = "cboProductId";
            this.cboProductId.Size = new System.Drawing.Size(214, 28);
            this.cboProductId.TabIndex = 21;
            this.cboProductId.SelectedIndexChanged += new System.EventHandler(this.cboProductId_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.label5.Location = new System.Drawing.Point(45, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 30);
            this.label5.TabIndex = 10;
            this.label5.Text = "Sản phẩm";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(24, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(207, 82);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(660, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(439, 48);
            this.label1.TabIndex = 7;
            this.label1.Text = "Quản lý phiếu bán hàng";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // cboFind
            // 
            this.cboFind.FormattingEnabled = true;
            this.cboFind.Location = new System.Drawing.Point(692, 95);
            this.cboFind.Name = "cboFind";
            this.cboFind.Size = new System.Drawing.Size(349, 28);
            this.cboFind.TabIndex = 30;
            this.cboFind.SelectedIndexChanged += new System.EventHandler(this.cboFind_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label11.Location = new System.Drawing.Point(540, 88);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(132, 36);
            this.label11.TabIndex = 30;
            this.label11.Text = "Tìm kiếm";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnResetDetail);
            this.groupBox3.Controls.Add(this.btnDeleteDetail);
            this.groupBox3.Controls.Add(this.btnUpdateDetail);
            this.groupBox3.Controls.Add(this.btnAddDetail);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.cboProductId);
            this.groupBox3.Controls.Add(this.nudQuantity);
            this.groupBox3.Controls.Add(this.txtUnitPrice);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox3.Location = new System.Drawing.Point(849, 154);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(800, 258);
            this.groupBox3.TabIndex = 31;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Chi tiết";
            // 
            // btnResetDetail
            // 
            this.btnResetDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnResetDetail.Location = new System.Drawing.Point(630, 176);
            this.btnResetDetail.Name = "btnResetDetail";
            this.btnResetDetail.Size = new System.Drawing.Size(154, 38);
            this.btnResetDetail.TabIndex = 40;
            this.btnResetDetail.Text = "Quay lại";
            this.btnResetDetail.UseVisualStyleBackColor = true;
            this.btnResetDetail.Click += new System.EventHandler(this.btnResetDetail_Click);
            // 
            // btnDeleteDetail
            // 
            this.btnDeleteDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnDeleteDetail.Location = new System.Drawing.Point(466, 176);
            this.btnDeleteDetail.Name = "btnDeleteDetail";
            this.btnDeleteDetail.Size = new System.Drawing.Size(137, 37);
            this.btnDeleteDetail.TabIndex = 40;
            this.btnDeleteDetail.Text = "Xóa";
            this.btnDeleteDetail.UseVisualStyleBackColor = true;
            this.btnDeleteDetail.Click += new System.EventHandler(this.btnDeleteDetail_Click);
            // 
            // btnUpdateDetail
            // 
            this.btnUpdateDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnUpdateDetail.Location = new System.Drawing.Point(630, 115);
            this.btnUpdateDetail.Name = "btnUpdateDetail";
            this.btnUpdateDetail.Size = new System.Drawing.Size(154, 40);
            this.btnUpdateDetail.TabIndex = 40;
            this.btnUpdateDetail.Text = "Cập nhật";
            this.btnUpdateDetail.UseVisualStyleBackColor = true;
            this.btnUpdateDetail.Click += new System.EventHandler(this.btnUpdateDetail_Click);
            // 
            // btnAddDetail
            // 
            this.btnAddDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnAddDetail.Location = new System.Drawing.Point(466, 115);
            this.btnAddDetail.Name = "btnAddDetail";
            this.btnAddDetail.Size = new System.Drawing.Size(137, 40);
            this.btnAddDetail.TabIndex = 40;
            this.btnAddDetail.Text = "Thêm";
            this.btnAddDetail.UseVisualStyleBackColor = true;
            this.btnAddDetail.Click += new System.EventHandler(this.btnAddDetail_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.label9.Location = new System.Drawing.Point(48, 149);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 30);
            this.label9.TabIndex = 38;
            this.label9.Text = "Đơn giá ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label8.Location = new System.Drawing.Point(442, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(135, 36);
            this.label8.TabIndex = 37;
            this.label8.Text = "Số lượng";
            // 
            // nudQuantity
            // 
            this.nudQuantity.Location = new System.Drawing.Point(594, 64);
            this.nudQuantity.Name = "nudQuantity";
            this.nudQuantity.Size = new System.Drawing.Size(190, 30);
            this.nudQuantity.TabIndex = 36;
            this.nudQuantity.ValueChanged += new System.EventHandler(this.nudQuantity_ValueChanged);
            // 
            // txtUnitPrice
            // 
            this.txtUnitPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtUnitPrice.Location = new System.Drawing.Point(200, 154);
            this.txtUnitPrice.Name = "txtUnitPrice";
            this.txtUnitPrice.ReadOnly = true;
            this.txtUnitPrice.Size = new System.Drawing.Size(214, 26);
            this.txtUnitPrice.TabIndex = 34;
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnExport.Location = new System.Drawing.Point(1224, 86);
            this.btnExport.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(170, 38);
            this.btnExport.TabIndex = 32;
            this.btnExport.Text = "Xuất Phiếu";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // QLPhieuBanHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cboFind);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Name = "QLPhieuBanHang";
            this.Size = new System.Drawing.Size(1688, 960);
            this.Load += new System.EventHandler(this.QLPhieuBanHang_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleInvoiceDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleInvoices)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox groupBox2;
        private DataGridView dgvSaleInvoices;
        private GroupBox groupBox1;
        private RadioButton rdoPaid;
        private DateTimePicker dtpCreatedDate;
        private ComboBox cboCardId;
        private Label label5;
        private TextBox txtInvoiceId;
        private Label label4;
        private Label label3;
        private Label label2;
        private PictureBox pictureBox1;
        private Label label1;
        private RadioButton rdoPending;
        private ComboBox cboProductId;
        private ComboBox cboEmployeeId;
        private Label label7;
        private Label label6;
        private ComboBox cboFind;
        private Label label11;
        private GroupBox groupBox3;
        private Label label10;
        private TextBox txtTotalAmount;
        private Label label9;
        private Label label8;
        private Button btnResetInvoice;
        private Button btnDeleteInvoice;
        private NumericUpDown nudQuantity;
        private Button btnAddInvoice;
        private Button btnUpdateInvoice;
        private TextBox txtUnitPrice;
        private Button btnAddDetail;
        private DataGridView dgvSaleInvoiceDetails;
        private Button btnResetDetail;
        private Button btnDeleteDetail;
        private Button btnUpdateDetail;
        private Button btnExport;
    }
}
