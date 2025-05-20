using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Entities;
using BLL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MaterialSkin.Controls;
using UI.Forms;

namespace UI.UserControls
{
    public partial class SalesUserControl : UserControl
    {
        private readonly ISaleService _saleService;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        private readonly IUserService _userService;

        public SalesUserControl()
        {
            InitializeComponent();
            
            try
            {
                // Servisler
                _saleService = Program.ServiceProvider.GetRequiredService<ISaleService>();
                _productService = Program.ServiceProvider.GetRequiredService<IProductService>();
                
                // İsteğe bağlı servisler - uygulamada geliştirilmesi gereken kısımlar
                try {
                    _customerService = Program.ServiceProvider.GetRequiredService<ICustomerService>();
                    _userService = Program.ServiceProvider.GetRequiredService<IUserService>();
                }
                catch { /* Bu servisler henüz geliştirilmemiş olabilir */ }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Servis başlatılırken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            // Temel kontroller
            this.dgvSales = new System.Windows.Forms.DataGridView();
            this.btnNewSale = new MaterialSkin.Controls.MaterialButton();
            this.btnViewDetails = new MaterialSkin.Controls.MaterialButton();
            this.btnPrint = new MaterialSkin.Controls.MaterialButton();
            this.btnCancel = new MaterialSkin.Controls.MaterialButton();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new MaterialSkin.Controls.MaterialButton();
            this.lblDateRange = new MaterialSkin.Controls.MaterialLabel();
            
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).BeginInit();
            this.SuspendLayout();
            
            // DataGridView
            this.dgvSales.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSales.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.dgvSales.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSales.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvSales.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSales.EnableHeadersVisualStyles = false;
            this.dgvSales.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvSales.Location = new System.Drawing.Point(20, 80);
            this.dgvSales.Name = "dgvSales";
            this.dgvSales.ReadOnly = true;
            this.dgvSales.RowHeadersVisible = false;
            this.dgvSales.RowTemplate.Height = 28;
            this.dgvSales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSales.Size = new System.Drawing.Size(760, 340);
            this.dgvSales.TabIndex = 0;
            
            // Yeni Satış Butonu
            this.btnNewSale.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnNewSale.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnNewSale.Depth = 0;
            this.btnNewSale.HighEmphasis = true;
            this.btnNewSale.Icon = null;
            this.btnNewSale.Location = new System.Drawing.Point(20, 20);
            this.btnNewSale.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnNewSale.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnNewSale.Name = "btnNewSale";
            this.btnNewSale.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnNewSale.Size = new System.Drawing.Size(120, 36);
            this.btnNewSale.TabIndex = 1;
            this.btnNewSale.Text = "YENİ SATIŞ";
            this.btnNewSale.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnNewSale.UseAccentColor = true;
            this.btnNewSale.UseVisualStyleBackColor = true;
            this.btnNewSale.Click += new System.EventHandler(this.btnNewSale_Click);
            
            // Detay Görüntüleme Butonu
            this.btnViewDetails.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnViewDetails.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnViewDetails.Depth = 0;
            this.btnViewDetails.HighEmphasis = true;
            this.btnViewDetails.Icon = null;
            this.btnViewDetails.Location = new System.Drawing.Point(150, 20);
            this.btnViewDetails.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnViewDetails.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnViewDetails.Name = "btnViewDetails";
            this.btnViewDetails.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnViewDetails.Size = new System.Drawing.Size(100, 36);
            this.btnViewDetails.TabIndex = 2;
            this.btnViewDetails.Text = "DETAY";
            this.btnViewDetails.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnViewDetails.UseAccentColor = false;
            this.btnViewDetails.UseVisualStyleBackColor = true;
            this.btnViewDetails.Click += new System.EventHandler(this.btnViewDetails_Click);
            
            // Yazdırma Butonu
            this.btnPrint.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnPrint.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnPrint.Depth = 0;
            this.btnPrint.HighEmphasis = true;
            this.btnPrint.Icon = null;
            this.btnPrint.Location = new System.Drawing.Point(260, 20);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnPrint.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnPrint.Size = new System.Drawing.Size(100, 36);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "YAZDIR";
            this.btnPrint.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnPrint.UseAccentColor = false;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            
            // İptal Butonu
            this.btnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCancel.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnCancel.Depth = 0;
            this.btnCancel.HighEmphasis = true;
            this.btnCancel.Icon = null;
            this.btnCancel.Location = new System.Drawing.Point(370, 20);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnCancel.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnCancel.Size = new System.Drawing.Size(100, 36);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "İPTAL";
            this.btnCancel.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnCancel.UseAccentColor = false;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            
            // Tarih seçiciler ve etiket
            this.lblDateRange = new MaterialSkin.Controls.MaterialLabel();
            this.lblDateRange.AutoSize = true;
            this.lblDateRange.Depth = 0;
            this.lblDateRange.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblDateRange.Location = new System.Drawing.Point(20, 430);
            this.lblDateRange.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblDateRange.Name = "lblDateRange";
            this.lblDateRange.Size = new System.Drawing.Size(81, 19);
            this.lblDateRange.TabIndex = 5;
            this.lblDateRange.Text = "Tarih Aralığı:";
            
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate.Location = new System.Drawing.Point(110, 430);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(200, 23);
            this.dtpStartDate.TabIndex = 6;
            this.dtpStartDate.Value = DateTime.Now.AddDays(-30);
            
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate.Location = new System.Drawing.Point(320, 430);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(200, 23);
            this.dtpEndDate.TabIndex = 7;
            this.dtpEndDate.Value = DateTime.Now;
            
            this.btnSearch = new MaterialSkin.Controls.MaterialButton();
            this.btnSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSearch.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnSearch.Depth = 0;
            this.btnSearch.HighEmphasis = true;
            this.btnSearch.Icon = null;
            this.btnSearch.Location = new System.Drawing.Point(530, 425);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnSearch.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnSearch.Size = new System.Drawing.Size(80, 36);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "ARA";
            this.btnSearch.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnSearch.UseAccentColor = false;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            
            // SalesUserControl
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.lblDateRange);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnViewDetails);
            this.Controls.Add(this.btnNewSale);
            this.Controls.Add(this.dgvSales);
            this.Name = "SalesUserControl";
            this.Size = new System.Drawing.Size(800, 470);
            this.Load += new System.EventHandler(this.SalesUserControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private DataGridView dgvSales;
        private MaterialButton btnNewSale;
        private MaterialButton btnViewDetails;
        private MaterialButton btnPrint;
        private MaterialButton btnCancel;
        private DateTimePicker dtpStartDate;
        private DateTimePicker dtpEndDate;
        private MaterialButton btnSearch;
        private MaterialLabel lblDateRange;

        private void SalesUserControl_Load(object sender, EventArgs e)
        {
            LoadSales();
            StyleDataGridView();
        }
        
        private void StyleDataGridView()
        {
            // DataGridView görünümünü özelleştir
            dgvSales.BackgroundColor = Color.FromArgb(242, 242, 242);
            dgvSales.GridColor = Color.FromArgb(224, 224, 224);
            
            // Sütun başlıkları
            dgvSales.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dgvSales.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvSales.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            dgvSales.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSales.ColumnHeadersHeight = 40;
            dgvSales.ColumnHeadersDefaultCellStyle.Padding = new Padding(5);
            
            // Normal hücreler için stil
            dgvSales.DefaultCellStyle.BackColor = Color.FromArgb(242, 242, 242);
            dgvSales.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvSales.DefaultCellStyle.Padding = new Padding(5);
            dgvSales.DefaultCellStyle.SelectionBackColor = Color.FromArgb(75, 75, 75);
            dgvSales.DefaultCellStyle.SelectionForeColor = Color.White;
            
            // Alternatif satır rengi
            dgvSales.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 230, 230);
            dgvSales.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(75, 75, 75);
            dgvSales.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;
            
            // Diğer ayarlar
            dgvSales.RowTemplate.Height = 30;
            dgvSales.RowHeadersVisible = false;
            dgvSales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSales.BorderStyle = BorderStyle.None;
            dgvSales.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        }

        private void LoadSales()
        {
            try
            {
                if (_saleService == null)
                {
                    MessageBox.Show("Satış servisi başlatılamadı.", "Hata", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // Belirli tarih aralığındaki satışları getir
                var startDate = dtpStartDate.Value.Date;
                var endDate = dtpEndDate.Value.Date;
                
                var sales = _saleService.GetSalesByDateRange(startDate, endDate);
                
                if (sales == null)
                {
                    MessageBox.Show("Satış verileri alınamadı.", "Hata", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                if (sales.Count == 0)
                {
                    MessageBox.Show("Seçilen tarih aralığında satış bulunamadı.", "Bilgi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvSales.DataSource = null;
                    return;
                }
                
                // Verileri göster
                var salesDisplay = sales.Select(s => new {
                    s.Id,
                    s.InvoiceNo,
                    SatışTarihi = s.SaleDate,
                    Müşteri = s.Customer != null ? s.Customer.Name : "Genel Müşteri",
                    ToplamTutar = $"{s.FinalAmount:C2}",
                    ÖdemeYöntemi = s.PaymentMethod.ToString(),
                    İptalEdildi = s.IsCancelled ? "Evet" : "Hayır"
                }).ToList();
                
                dgvSales.DataSource = salesDisplay;
                ConfigureDataGridColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Satışlar yüklenirken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void ConfigureDataGridColumns()
        {
            if (dgvSales.Columns.Contains("Id"))
                dgvSales.Columns["Id"].Visible = false;
            
            if (dgvSales.Columns.Contains("InvoiceNo"))
                dgvSales.Columns["InvoiceNo"].HeaderText = "Fiş No";
            
            if (dgvSales.Columns.Contains("SatışTarihi"))
            {
                dgvSales.Columns["SatışTarihi"].HeaderText = "Satış Tarihi";
                dgvSales.Columns["SatışTarihi"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
            
            if (dgvSales.Columns.Contains("ToplamTutar"))
            {
                dgvSales.Columns["ToplamTutar"].HeaderText = "Toplam Tutar";
                dgvSales.Columns["ToplamTutar"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            
            if (dgvSales.Columns.Contains("ÖdemeYöntemi"))
            {
                dgvSales.Columns["ÖdemeYöntemi"].HeaderText = "Ödeme Yöntemi";
                // Ödeme yöntemi çevirisi
                foreach (DataGridViewRow row in dgvSales.Rows)
                {
                    if (row.Cells["ÖdemeYöntemi"].Value != null)
                    {
                        string paymentMethod = row.Cells["ÖdemeYöntemi"].Value.ToString();
                        switch (paymentMethod)
                        {
                            case "Cash": row.Cells["ÖdemeYöntemi"].Value = "Nakit"; break;
                            case "CreditCard": row.Cells["ÖdemeYöntemi"].Value = "Kredi Kartı"; break;
                            case "DebitCard": row.Cells["ÖdemeYöntemi"].Value = "Banka Kartı"; break;
                            case "Other": row.Cells["ÖdemeYöntemi"].Value = "Diğer"; break;
                        }
                    }
                }
            }
            
            if (dgvSales.Columns.Contains("İptalEdildi"))
            {
                dgvSales.Columns["İptalEdildi"].HeaderText = "İptal Edildi";
                dgvSales.Columns["İptalEdildi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                
                // İptal edilmiş satırlar için özel renklendirme
                foreach (DataGridViewRow row in dgvSales.Rows)
                {
                    if (row.Cells["İptalEdildi"].Value != null && row.Cells["İptalEdildi"].Value.ToString() == "Evet")
                    {
                        row.DefaultCellStyle.ForeColor = Color.Red;
                        row.DefaultCellStyle.SelectionForeColor = Color.Pink;
                    }
                }
            }
        }

        private void btnNewSale_Click(object sender, EventArgs e)
        {
            try
            {
                var saleForm = new SaleForm(_productService, _saleService);
                if (saleForm.ShowDialog() == DialogResult.OK)
                {
                    LoadSales();
                    MessageBox.Show("Satış başarıyla oluşturuldu.", "Bilgi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Satış formu açılırken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (dgvSales.SelectedRows.Count > 0)
            {
                try
                {
                    int saleId = Convert.ToInt32(dgvSales.SelectedRows[0].Cells["Id"].Value);
                    var sale = _saleService.GetSaleById(saleId);
                    var saleItems = _saleService.GetSaleItems(saleId);
                    
                    if (sale == null || saleItems == null || saleItems.Count == 0)
                    {
                        MessageBox.Show("Satış detayları alınamadı.", "Hata",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                    // Detay formunu göster
                    var detailForm = new SaleDetailForm(sale, saleItems);
                    detailForm.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Satış detayları gösterilirken hata oluştu: {ex.Message}", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen detaylarını görmek için bir satış seçin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvSales.SelectedRows.Count > 0)
            {
                try
                {
                    int saleId = Convert.ToInt32(dgvSales.SelectedRows[0].Cells["Id"].Value);
                    var sale = _saleService.GetSaleById(saleId);
                    var saleItems = _saleService.GetSaleItems(saleId);
                    
                    if (sale == null || saleItems == null || saleItems.Count == 0)
                    {
                        MessageBox.Show("Yazdırılacak satış detayları alınamadı.", "Hata",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                    // Yazdırma önizleme formunu göster
                    MessageBox.Show("Yazdırma özelliği geliştirilecektir. Bu örnek uygulamada " +
                        "yazdırma fonksiyonelliği henüz yoktur.", "Bilgi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Satış yazdırılırken hata oluştu: {ex.Message}", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen yazdırmak için bir satış seçin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (dgvSales.SelectedRows.Count > 0)
            {
                try
                {
                    int saleId = Convert.ToInt32(dgvSales.SelectedRows[0].Cells["Id"].Value);
                    var sale = _saleService.GetSaleById(saleId);
                    
                    if (sale == null)
                    {
                        MessageBox.Show("Satış bilgileri alınamadı.", "Hata",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                    if (sale.IsCancelled)
                    {
                        MessageBox.Show("Bu satış zaten iptal edilmiş.", "Uyarı",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    var result = MessageBox.Show("Bu satışı iptal etmek istediğinizden emin misiniz?", 
                        "İptal Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        var cancellationReason = Microsoft.VisualBasic.Interaction.InputBox(
                            "İptal sebebini giriniz:", "İptal Sebebi", "");
                        
                        if (string.IsNullOrWhiteSpace(cancellationReason))
                            cancellationReason = "Kullanıcı tarafından iptal edildi.";
                        
                        _saleService.CancelSale(saleId, cancellationReason);
                        LoadSales();
                        
                        MessageBox.Show("Satış başarıyla iptal edildi.", "Bilgi",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Satış iptal edilirken hata oluştu: {ex.Message}", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen iptal etmek için bir satış seçin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (dtpEndDate.Value < dtpStartDate.Value)
            {
                MessageBox.Show("Bitiş tarihi başlangıç tarihinden önce olamaz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            LoadSales();
        }
    }
} 