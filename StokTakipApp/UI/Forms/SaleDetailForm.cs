using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Entities;
using MaterialSkin.Controls;
using MaterialSkin;

namespace UI.Forms
{
    public partial class SaleDetailForm : MaterialForm
    {
        private readonly Sale _sale;
        private readonly List<SaleItem> _saleItems;
        
        public SaleDetailForm(Sale sale, List<SaleItem> saleItems)
        {
            InitializeComponent();
            
            // Material skin
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue800,
                Primary.Blue900,
                Primary.Blue500,
                Accent.LightBlue200,
                TextShade.WHITE);
            
            _sale = sale;
            _saleItems = saleItems;
        }
        
        private void InitializeComponent()
        {
            this.lblInvoiceNo = new MaterialSkin.Controls.MaterialLabel();
            this.lblInvoiceNoValue = new MaterialSkin.Controls.MaterialLabel();
            this.lblSaleDate = new MaterialSkin.Controls.MaterialLabel();
            this.lblSaleDateValue = new MaterialSkin.Controls.MaterialLabel();
            this.lblCustomer = new MaterialSkin.Controls.MaterialLabel();
            this.lblCustomerValue = new MaterialSkin.Controls.MaterialLabel();
            this.lblPaymentMethod = new MaterialSkin.Controls.MaterialLabel();
            this.lblPaymentMethodValue = new MaterialSkin.Controls.MaterialLabel();
            this.lblTotalAmount = new MaterialSkin.Controls.MaterialLabel();
            this.lblTotalAmountValue = new MaterialSkin.Controls.MaterialLabel();
            this.lblTaxAmount = new MaterialSkin.Controls.MaterialLabel();
            this.lblTaxAmountValue = new MaterialSkin.Controls.MaterialLabel();
            this.lblDiscountAmount = new MaterialSkin.Controls.MaterialLabel();
            this.lblDiscountAmountValue = new MaterialSkin.Controls.MaterialLabel();
            this.lblFinalAmount = new MaterialSkin.Controls.MaterialLabel();
            this.lblFinalAmountValue = new MaterialSkin.Controls.MaterialLabel();
            this.dgvSaleItems = new System.Windows.Forms.DataGridView();
            this.lblCancellationInfo = new MaterialSkin.Controls.MaterialLabel();
            this.btnClose = new MaterialSkin.Controls.MaterialButton();
            this.btnPrint = new MaterialSkin.Controls.MaterialButton();
            
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleItems)).BeginInit();
            this.SuspendLayout();
            
            // Temel form özellikleri
            this.Text = "Satış Detayları";
            this.Size = new System.Drawing.Size(800, 600);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            
            // Üst kısım - fiş no ve tarih
            this.lblInvoiceNo = new MaterialSkin.Controls.MaterialLabel();
            this.lblInvoiceNo.AutoSize = true;
            this.lblInvoiceNo.Depth = 0;
            this.lblInvoiceNo.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblInvoiceNo.Location = new System.Drawing.Point(20, 80);
            this.lblInvoiceNo.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblInvoiceNo.Size = new System.Drawing.Size(80, 19);
            this.lblInvoiceNo.TabIndex = 0;
            this.lblInvoiceNo.Text = "Fiş No:";
            
            this.lblInvoiceNoValue = new MaterialSkin.Controls.MaterialLabel();
            this.lblInvoiceNoValue.AutoSize = true;
            this.lblInvoiceNoValue.Depth = 0;
            this.lblInvoiceNoValue.Font = new System.Drawing.Font("Roboto Medium", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblInvoiceNoValue.Location = new System.Drawing.Point(120, 80);
            this.lblInvoiceNoValue.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblInvoiceNoValue.Size = new System.Drawing.Size(200, 19);
            this.lblInvoiceNoValue.TabIndex = 1;
            this.lblInvoiceNoValue.Text = "...";
            
            this.lblSaleDate = new MaterialSkin.Controls.MaterialLabel();
            this.lblSaleDate.AutoSize = true;
            this.lblSaleDate.Depth = 0;
            this.lblSaleDate.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblSaleDate.Location = new System.Drawing.Point(20, 110);
            this.lblSaleDate.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblSaleDate.Size = new System.Drawing.Size(80, 19);
            this.lblSaleDate.TabIndex = 2;
            this.lblSaleDate.Text = "Tarih:";
            
            this.lblSaleDateValue = new MaterialSkin.Controls.MaterialLabel();
            this.lblSaleDateValue.AutoSize = true;
            this.lblSaleDateValue.Depth = 0;
            this.lblSaleDateValue.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblSaleDateValue.Location = new System.Drawing.Point(120, 110);
            this.lblSaleDateValue.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblSaleDateValue.Size = new System.Drawing.Size(200, 19);
            this.lblSaleDateValue.TabIndex = 3;
            this.lblSaleDateValue.Text = "...";
            
            // Müşteri bilgisi
            this.lblCustomer = new MaterialSkin.Controls.MaterialLabel();
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Depth = 0;
            this.lblCustomer.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblCustomer.Location = new System.Drawing.Point(20, 140);
            this.lblCustomer.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblCustomer.Size = new System.Drawing.Size(80, 19);
            this.lblCustomer.TabIndex = 4;
            this.lblCustomer.Text = "Müşteri:";
            
            this.lblCustomerValue = new MaterialSkin.Controls.MaterialLabel();
            this.lblCustomerValue.AutoSize = true;
            this.lblCustomerValue.Depth = 0;
            this.lblCustomerValue.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblCustomerValue.Location = new System.Drawing.Point(120, 140);
            this.lblCustomerValue.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblCustomerValue.Size = new System.Drawing.Size(200, 19);
            this.lblCustomerValue.TabIndex = 5;
            this.lblCustomerValue.Text = "Genel Müşteri";
            
            // Ödeme yöntemi
            this.lblPaymentMethod = new MaterialSkin.Controls.MaterialLabel();
            this.lblPaymentMethod.AutoSize = true;
            this.lblPaymentMethod.Depth = 0;
            this.lblPaymentMethod.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblPaymentMethod.Location = new System.Drawing.Point(400, 80);
            this.lblPaymentMethod.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblPaymentMethod.Size = new System.Drawing.Size(80, 19);
            this.lblPaymentMethod.TabIndex = 6;
            this.lblPaymentMethod.Text = "Ödeme:";
            
            this.lblPaymentMethodValue = new MaterialSkin.Controls.MaterialLabel();
            this.lblPaymentMethodValue.AutoSize = true;
            this.lblPaymentMethodValue.Depth = 0;
            this.lblPaymentMethodValue.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblPaymentMethodValue.Location = new System.Drawing.Point(500, 80);
            this.lblPaymentMethodValue.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblPaymentMethodValue.Size = new System.Drawing.Size(150, 19);
            this.lblPaymentMethodValue.TabIndex = 7;
            this.lblPaymentMethodValue.Text = "...";
            
            // Tutar bilgileri
            this.lblTotalAmount = new MaterialSkin.Controls.MaterialLabel();
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Depth = 0;
            this.lblTotalAmount.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblTotalAmount.Location = new System.Drawing.Point(500, 110);
            this.lblTotalAmount.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblTotalAmount.Size = new System.Drawing.Size(110, 19);
            this.lblTotalAmount.TabIndex = 8;
            this.lblTotalAmount.Text = "Toplam:";
            this.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.lblTotalAmountValue = new MaterialSkin.Controls.MaterialLabel();
            this.lblTotalAmountValue.AutoSize = true;
            this.lblTotalAmountValue.Depth = 0;
            this.lblTotalAmountValue.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblTotalAmountValue.Location = new System.Drawing.Point(680, 110);
            this.lblTotalAmountValue.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblTotalAmountValue.Size = new System.Drawing.Size(100, 19);
            this.lblTotalAmountValue.TabIndex = 9;
            this.lblTotalAmountValue.Text = "0,00 ₺";
            this.lblTotalAmountValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.lblTaxAmount = new MaterialSkin.Controls.MaterialLabel();
            this.lblTaxAmount.AutoSize = true;
            this.lblTaxAmount.Depth = 0;
            this.lblTaxAmount.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblTaxAmount.Location = new System.Drawing.Point(500, 130);
            this.lblTaxAmount.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblTaxAmount.Size = new System.Drawing.Size(110, 19);
            this.lblTaxAmount.TabIndex = 10;
            this.lblTaxAmount.Text = "KDV:";
            this.lblTaxAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.lblTaxAmountValue = new MaterialSkin.Controls.MaterialLabel();
            this.lblTaxAmountValue.AutoSize = true;
            this.lblTaxAmountValue.Depth = 0;
            this.lblTaxAmountValue.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblTaxAmountValue.Location = new System.Drawing.Point(680, 130);
            this.lblTaxAmountValue.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblTaxAmountValue.Size = new System.Drawing.Size(100, 19);
            this.lblTaxAmountValue.TabIndex = 11;
            this.lblTaxAmountValue.Text = "0,00 ₺";
            this.lblTaxAmountValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.lblDiscountAmount = new MaterialSkin.Controls.MaterialLabel();
            this.lblDiscountAmount.AutoSize = true;
            this.lblDiscountAmount.Depth = 0;
            this.lblDiscountAmount.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblDiscountAmount.Location = new System.Drawing.Point(500, 150);
            this.lblDiscountAmount.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblDiscountAmount.Size = new System.Drawing.Size(110, 19);
            this.lblDiscountAmount.TabIndex = 12;
            this.lblDiscountAmount.Text = "İndirim:";
            this.lblDiscountAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.lblDiscountAmountValue = new MaterialSkin.Controls.MaterialLabel();
            this.lblDiscountAmountValue.AutoSize = true;
            this.lblDiscountAmountValue.Depth = 0;
            this.lblDiscountAmountValue.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblDiscountAmountValue.Location = new System.Drawing.Point(680, 150);
            this.lblDiscountAmountValue.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblDiscountAmountValue.Size = new System.Drawing.Size(100, 19);
            this.lblDiscountAmountValue.TabIndex = 13;
            this.lblDiscountAmountValue.Text = "0,00 ₺";
            this.lblDiscountAmountValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.lblFinalAmount = new MaterialSkin.Controls.MaterialLabel();
            this.lblFinalAmount.AutoSize = true;
            this.lblFinalAmount.Depth = 0;
            this.lblFinalAmount.Font = new System.Drawing.Font("Roboto Medium", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblFinalAmount.Location = new System.Drawing.Point(500, 180);
            this.lblFinalAmount.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblFinalAmount.Size = new System.Drawing.Size(110, 19);
            this.lblFinalAmount.TabIndex = 14;
            this.lblFinalAmount.Text = "Genel Toplam:";
            this.lblFinalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.lblFinalAmountValue = new MaterialSkin.Controls.MaterialLabel();
            this.lblFinalAmountValue.AutoSize = true;
            this.lblFinalAmountValue.Depth = 0;
            this.lblFinalAmountValue.Font = new System.Drawing.Font("Roboto Medium", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblFinalAmountValue.Location = new System.Drawing.Point(680, 180);
            this.lblFinalAmountValue.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblFinalAmountValue.Size = new System.Drawing.Size(100, 19);
            this.lblFinalAmountValue.TabIndex = 15;
            this.lblFinalAmountValue.Text = "0,00 ₺";
            this.lblFinalAmountValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            // Satış kalemlerini gösteren DataGridView
            this.dgvSaleItems = new System.Windows.Forms.DataGridView();
            this.dgvSaleItems.Location = new System.Drawing.Point(20, 220);
            this.dgvSaleItems.Size = new System.Drawing.Size(760, 300);
            this.dgvSaleItems.ReadOnly = true;
            this.dgvSaleItems.BackgroundColor = System.Drawing.Color.FromArgb(242, 242, 242);
            this.dgvSaleItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSaleItems.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvSaleItems.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvSaleItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSaleItems.EnableHeadersVisualStyles = false;
            this.dgvSaleItems.RowHeadersVisible = false;
            this.dgvSaleItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSaleItems.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            this.dgvSaleItems.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dgvSaleItems.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            this.dgvSaleItems.ColumnHeadersHeight = 40;
            
            // İptal bilgisi etiketi
            this.lblCancellationInfo = new MaterialSkin.Controls.MaterialLabel();
            this.lblCancellationInfo.AutoSize = true;
            this.lblCancellationInfo.Depth = 0;
            this.lblCancellationInfo.Font = new System.Drawing.Font("Roboto Medium", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblCancellationInfo.ForeColor = System.Drawing.Color.Red;
            this.lblCancellationInfo.Location = new System.Drawing.Point(20, 530);
            this.lblCancellationInfo.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblCancellationInfo.Size = new System.Drawing.Size(400, 19);
            this.lblCancellationInfo.TabIndex = 16;
            this.lblCancellationInfo.Text = "";
            this.lblCancellationInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCancellationInfo.Visible = false;
            
            // Butonlar
            this.btnClose = new MaterialSkin.Controls.MaterialButton();
            this.btnClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClose.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnClose.Depth = 0;
            this.btnClose.HighEmphasis = true;
            this.btnClose.Icon = null;
            this.btnClose.Location = new System.Drawing.Point(680, 530);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnClose.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnClose.Name = "btnClose";
            this.btnClose.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnClose.Size = new System.Drawing.Size(100, 36);
            this.btnClose.TabIndex = 17;
            this.btnClose.Text = "KAPAT";
            this.btnClose.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnClose.UseAccentColor = false;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            
            this.btnPrint = new MaterialSkin.Controls.MaterialButton();
            this.btnPrint.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnPrint.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnPrint.Depth = 0;
            this.btnPrint.HighEmphasis = true;
            this.btnPrint.Icon = null;
            this.btnPrint.Location = new System.Drawing.Point(570, 530);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnPrint.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnPrint.Size = new System.Drawing.Size(100, 36);
            this.btnPrint.TabIndex = 18;
            this.btnPrint.Text = "YAZDIR";
            this.btnPrint.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnPrint.UseAccentColor = true;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            
            // Kontrolleri forma ekle
            this.Controls.Add(this.lblInvoiceNo);
            this.Controls.Add(this.lblInvoiceNoValue);
            this.Controls.Add(this.lblSaleDate);
            this.Controls.Add(this.lblSaleDateValue);
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.lblCustomerValue);
            this.Controls.Add(this.lblPaymentMethod);
            this.Controls.Add(this.lblPaymentMethodValue);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.lblTotalAmountValue);
            this.Controls.Add(this.lblTaxAmount);
            this.Controls.Add(this.lblTaxAmountValue);
            this.Controls.Add(this.lblDiscountAmount);
            this.Controls.Add(this.lblDiscountAmountValue);
            this.Controls.Add(this.lblFinalAmount);
            this.Controls.Add(this.lblFinalAmountValue);
            this.Controls.Add(this.dgvSaleItems);
            this.Controls.Add(this.lblCancellationInfo);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnPrint);
            
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
            
            // Form yüklendiğinde
            this.Load += new System.EventHandler(this.SaleDetailForm_Load);
        }

        private void SaleDetailForm_Load(object sender, EventArgs e)
        {
            LoadSaleDetails();
        }
        
        private void LoadSaleDetails()
        {
            try
            {
                // Fatura bilgilerini doldur
                lblInvoiceNoValue.Text = _sale.InvoiceNo;
                lblSaleDateValue.Text = _sale.SaleDate.ToString("dd.MM.yyyy HH:mm");
                
                // Müşteri bilgisi
                lblCustomerValue.Text = _sale.Customer != null ? _sale.Customer.Name : "Genel Müşteri";
                
                // Ödeme yöntemi
                string paymentMethod = "";
                switch (_sale.PaymentMethod)
                {
                    case PaymentMethod.Cash: paymentMethod = "Nakit"; break;
                    case PaymentMethod.CreditCard: paymentMethod = "Kredi Kartı"; break;
                    case PaymentMethod.DebitCard: paymentMethod = "Banka Kartı"; break;
                    case PaymentMethod.Other: paymentMethod = "Diğer"; break;
                }
                lblPaymentMethodValue.Text = paymentMethod;
                
                // Tutarlar
                lblTotalAmountValue.Text = $"{_sale.TotalAmount:N2} ₺";
                lblTaxAmountValue.Text = $"{_sale.TaxAmount:N2} ₺";
                lblDiscountAmountValue.Text = $"{_sale.DiscountAmount:N2} ₺";
                lblFinalAmountValue.Text = $"{_sale.FinalAmount:N2} ₺";
                
                // Satış kalemleri
                var saleItemsDisplay = _saleItems.Select(si => new
                {
                    ÜrünKodu = si.Product?.Barcode ?? "?",
                    ÜrünAdı = si.Product?.Name ?? "Ürün Bulunamadı",
                    BirimFiyat = si.UnitPrice,
                    KDV = si.TaxRate,
                    İskonto = si.DiscountRate,
                    Miktar = si.Quantity,
                    KDVTutarı = si.TaxAmount,
                    İskontoTutarı = si.DiscountAmount,
                    Toplam = si.TotalAmount
                }).ToList();
                
                dgvSaleItems.DataSource = saleItemsDisplay;
                StyleDataGridView();
                
                // İptal bilgisi varsa göster
                if (_sale.IsCancelled)
                {
                    lblCancellationInfo.Text = $"Bu satış iptal edilmiştir. İptal Tarihi: {_sale.CancellationDate:dd.MM.yyyy HH:mm}";
                    lblCancellationInfo.ForeColor = Color.Red;
                    lblCancellationInfo.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Satış detayları yüklenirken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void StyleDataGridView()
        {
            // Sütun stili
            if (dgvSaleItems.Columns.Contains("BirimFiyat"))
            {
                dgvSaleItems.Columns["BirimFiyat"].HeaderText = "Birim Fiyat";
                dgvSaleItems.Columns["BirimFiyat"].DefaultCellStyle.Format = "N2";
                dgvSaleItems.Columns["BirimFiyat"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            
            if (dgvSaleItems.Columns.Contains("KDV"))
            {
                dgvSaleItems.Columns["KDV"].DefaultCellStyle.Format = "N2";
                dgvSaleItems.Columns["KDV"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            
            if (dgvSaleItems.Columns.Contains("İskonto"))
            {
                dgvSaleItems.Columns["İskonto"].DefaultCellStyle.Format = "N2";
                dgvSaleItems.Columns["İskonto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            
            if (dgvSaleItems.Columns.Contains("Miktar"))
            {
                dgvSaleItems.Columns["Miktar"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            
            if (dgvSaleItems.Columns.Contains("KDVTutarı"))
            {
                dgvSaleItems.Columns["KDVTutarı"].HeaderText = "KDV Tutarı";
                dgvSaleItems.Columns["KDVTutarı"].DefaultCellStyle.Format = "N2";
                dgvSaleItems.Columns["KDVTutarı"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            
            if (dgvSaleItems.Columns.Contains("İskontoTutarı"))
            {
                dgvSaleItems.Columns["İskontoTutarı"].HeaderText = "İskonto Tutarı";
                dgvSaleItems.Columns["İskontoTutarı"].DefaultCellStyle.Format = "N2";
                dgvSaleItems.Columns["İskontoTutarı"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            
            if (dgvSaleItems.Columns.Contains("Toplam"))
            {
                dgvSaleItems.Columns["Toplam"].DefaultCellStyle.Format = "N2";
                dgvSaleItems.Columns["Toplam"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            
            // Genel stil düzenlemeleri
            dgvSaleItems.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dgvSaleItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        
        private void btnPrint_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Yazdırma özelliği geliştirilecektir. Bu örnek uygulamada " +
                "yazdırma fonksiyonelliği henüz yoktur.", "Bilgi",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        // Form kontrol değişkenleri
        private MaterialLabel lblInvoiceNo;
        private MaterialLabel lblInvoiceNoValue;
        private MaterialLabel lblSaleDate;
        private MaterialLabel lblSaleDateValue;
        private MaterialLabel lblCustomer;
        private MaterialLabel lblCustomerValue;
        private MaterialLabel lblPaymentMethod;
        private MaterialLabel lblPaymentMethodValue;
        private MaterialLabel lblTotalAmount;
        private MaterialLabel lblTotalAmountValue;
        private MaterialLabel lblTaxAmount;
        private MaterialLabel lblTaxAmountValue;
        private MaterialLabel lblDiscountAmount;
        private MaterialLabel lblDiscountAmountValue;
        private MaterialLabel lblFinalAmount;
        private MaterialLabel lblFinalAmountValue;
        private DataGridView dgvSaleItems;
        private MaterialLabel lblCancellationInfo;
        private MaterialButton btnClose;
        private MaterialButton btnPrint;
    }
} 