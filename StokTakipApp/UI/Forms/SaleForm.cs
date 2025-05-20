using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Entities;
using BLL.Services.Interfaces;
using MaterialSkin.Controls;
using MaterialSkin;

namespace UI.Forms
{
    public partial class SaleForm : MaterialForm
    {
        private readonly IProductService _productService;
        private readonly ISaleService _saleService;
        private List<SaleItem> _saleItems;
        private DataTable _cartTable;
        private decimal _totalAmount = 0;
        private decimal _taxAmount = 0;
        private decimal _discountAmount = 0;
        private decimal _finalAmount = 0;

        public SaleForm(IProductService productService, ISaleService saleService)
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

            _productService = productService;
            _saleService = saleService;
            _saleItems = new List<SaleItem>();

            // Sepet tablosunu hazırla
            InitializeCartTable();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            
            // Temel form özellikleri
            this.Text = "Yeni Satış";
            this.Size = new System.Drawing.Size(900, 650);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            
            // Barkod ve ürün arama kontrolü
            this.txtBarcode = new MaterialSkin.Controls.MaterialTextBox();
            this.txtBarcode.Hint = "Barkod Numarası";
            this.txtBarcode.Location = new System.Drawing.Point(20, 80);
            this.txtBarcode.Size = new System.Drawing.Size(200, 50);
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcode_KeyDown);
            
            this.btnProductSearch = new MaterialSkin.Controls.MaterialButton();
            this.btnProductSearch.Text = "ÜRÜN ARA";
            this.btnProductSearch.Location = new System.Drawing.Point(230, 85);
            this.btnProductSearch.Size = new System.Drawing.Size(120, 40);
            this.btnProductSearch.Click += new System.EventHandler(this.btnProductSearch_Click);
            
            // Miktar kontrolü
            this.lblQuantity = new MaterialSkin.Controls.MaterialLabel();
            this.lblQuantity.Text = "Miktar:";
            this.lblQuantity.Location = new System.Drawing.Point(370, 92);
            this.lblQuantity.Size = new System.Drawing.Size(60, 25);
            
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.nudQuantity.Location = new System.Drawing.Point(430, 90);
            this.nudQuantity.Size = new System.Drawing.Size(70, 30);
            this.nudQuantity.Minimum = 1;
            this.nudQuantity.Maximum = 1000;
            this.nudQuantity.Value = 1;
            
            // Sepet DataGridView
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.dgvCart.Location = new System.Drawing.Point(20, 150);
            this.dgvCart.Size = new System.Drawing.Size(860, 300);
            this.dgvCart.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCart_CellEndEdit);
            this.dgvCart.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCart_CellClick);
            
            // Sepet kontrol butonları
            this.btnRemoveItem = new MaterialSkin.Controls.MaterialButton();
            this.btnRemoveItem.Text = "ÜRÜNÜ KALDIR";
            this.btnRemoveItem.Location = new System.Drawing.Point(20, 460);
            this.btnRemoveItem.Size = new System.Drawing.Size(150, 40);
            this.btnRemoveItem.UseAccentColor = true;
            this.btnRemoveItem.Click += new System.EventHandler(this.btnRemoveItem_Click);
            
            this.btnClearCart = new MaterialSkin.Controls.MaterialButton();
            this.btnClearCart.Text = "SEPETİ TEMİZLE";
            this.btnClearCart.Location = new System.Drawing.Point(190, 460);
            this.btnClearCart.Size = new System.Drawing.Size(150, 40);
            this.btnClearCart.UseAccentColor = true;
            this.btnClearCart.Click += new System.EventHandler(this.btnClearCart_Click);
            
            // Toplam tutarlar paneli
            this.pnlTotals = new System.Windows.Forms.Panel();
            this.pnlTotals.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTotals.Location = new System.Drawing.Point(600, 450);
            this.pnlTotals.Size = new System.Drawing.Size(280, 150);
            
            this.lblTotal = new MaterialSkin.Controls.MaterialLabel();
            this.lblTotal.Text = "Toplam:";
            this.lblTotal.Location = new System.Drawing.Point(10, 10);
            this.lblTotal.Size = new System.Drawing.Size(150, 25);
            
            this.lblTotalValue = new MaterialSkin.Controls.MaterialLabel();
            this.lblTotalValue.Text = "0,00 ₺";
            this.lblTotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTotalValue.Location = new System.Drawing.Point(120, 10);
            this.lblTotalValue.Size = new System.Drawing.Size(150, 25);
            
            this.lblTax = new MaterialSkin.Controls.MaterialLabel();
            this.lblTax.Text = "KDV:";
            this.lblTax.Location = new System.Drawing.Point(10, 40);
            this.lblTax.Size = new System.Drawing.Size(150, 25);
            
            this.lblTaxValue = new MaterialSkin.Controls.MaterialLabel();
            this.lblTaxValue.Text = "0,00 ₺";
            this.lblTaxValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTaxValue.Location = new System.Drawing.Point(120, 40);
            this.lblTaxValue.Size = new System.Drawing.Size(150, 25);
            
            this.lblDiscount = new MaterialSkin.Controls.MaterialLabel();
            this.lblDiscount.Text = "İndirim:";
            this.lblDiscount.Location = new System.Drawing.Point(10, 70);
            this.lblDiscount.Size = new System.Drawing.Size(150, 25);
            
            this.lblDiscountValue = new MaterialSkin.Controls.MaterialLabel();
            this.lblDiscountValue.Text = "0,00 ₺";
            this.lblDiscountValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDiscountValue.Location = new System.Drawing.Point(120, 70);
            this.lblDiscountValue.Size = new System.Drawing.Size(150, 25);
            
            this.lblGrandTotal = new MaterialSkin.Controls.MaterialLabel();
            this.lblGrandTotal.Text = "Genel Toplam:";
            this.lblGrandTotal.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Bold);
            this.lblGrandTotal.Location = new System.Drawing.Point(10, 110);
            this.lblGrandTotal.Size = new System.Drawing.Size(150, 25);
            
            this.lblGrandTotalValue = new MaterialSkin.Controls.MaterialLabel();
            this.lblGrandTotalValue.Text = "0,00 ₺";
            this.lblGrandTotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblGrandTotalValue.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Bold);
            this.lblGrandTotalValue.Location = new System.Drawing.Point(120, 110);
            this.lblGrandTotalValue.Size = new System.Drawing.Size(150, 25);
            
            // Ödeme seçenekleri paneli ve kontroller
            this.gbPayment = new System.Windows.Forms.GroupBox();
            this.gbPayment.Text = "Ödeme Yöntemi";
            this.gbPayment.Location = new System.Drawing.Point(20, 510);
            this.gbPayment.Size = new System.Drawing.Size(550, 80);
            
            this.rbCash = new MaterialSkin.Controls.MaterialRadioButton();
            this.rbCash.Text = "Nakit";
            this.rbCash.Location = new System.Drawing.Point(20, 30);
            this.rbCash.Checked = true;
            
            this.rbCreditCard = new MaterialSkin.Controls.MaterialRadioButton();
            this.rbCreditCard.Text = "Kredi Kartı";
            this.rbCreditCard.Location = new System.Drawing.Point(150, 30);
            
            this.rbDebitCard = new MaterialSkin.Controls.MaterialRadioButton();
            this.rbDebitCard.Text = "Banka Kartı";
            this.rbDebitCard.Location = new System.Drawing.Point(280, 30);
            
            this.rbOther = new MaterialSkin.Controls.MaterialRadioButton();
            this.rbOther.Text = "Diğer";
            this.rbOther.Location = new System.Drawing.Point(410, 30);
            
            // Tamamlama butonları
            this.btnCancel = new MaterialSkin.Controls.MaterialButton();
            this.btnCancel.Text = "İPTAL";
            this.btnCancel.Location = new System.Drawing.Point(20, 600);
            this.btnCancel.Size = new System.Drawing.Size(120, 40);
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            
            this.btnComplete = new MaterialSkin.Controls.MaterialButton();
            this.btnComplete.Text = "SATIŞI TAMAMLA";
            this.btnComplete.Location = new System.Drawing.Point(720, 600);
            this.btnComplete.Size = new System.Drawing.Size(160, 40);
            this.btnComplete.UseAccentColor = true;
            this.btnComplete.Click += new System.EventHandler(this.btnComplete_Click);
            
            // Kontrolleri forma ekle
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.btnProductSearch);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.nudQuantity);
            this.Controls.Add(this.dgvCart);
            this.Controls.Add(this.btnRemoveItem);
            this.Controls.Add(this.btnClearCart);
            this.Controls.Add(this.pnlTotals);
            this.Controls.Add(this.gbPayment);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnComplete);
            
            // Panel içine kontroller ekle
            this.pnlTotals.Controls.Add(this.lblTotal);
            this.pnlTotals.Controls.Add(this.lblTotalValue);
            this.pnlTotals.Controls.Add(this.lblTax);
            this.pnlTotals.Controls.Add(this.lblTaxValue);
            this.pnlTotals.Controls.Add(this.lblDiscount);
            this.pnlTotals.Controls.Add(this.lblDiscountValue);
            this.pnlTotals.Controls.Add(this.lblGrandTotal);
            this.pnlTotals.Controls.Add(this.lblGrandTotalValue);
            
            // GroupBox içine kontroller ekle
            this.gbPayment.Controls.Add(this.rbCash);
            this.gbPayment.Controls.Add(this.rbCreditCard);
            this.gbPayment.Controls.Add(this.rbDebitCard);
            this.gbPayment.Controls.Add(this.rbOther);
        }

        private void InitializeCartTable()
        {
            _cartTable = new DataTable();
            _cartTable.Columns.Add("ID", typeof(int));
            _cartTable.Columns.Add("Ürün Kodu", typeof(string));
            _cartTable.Columns.Add("Ürün Adı", typeof(string));
            _cartTable.Columns.Add("Birim Fiyat", typeof(decimal));
            _cartTable.Columns.Add("KDV %", typeof(decimal));
            _cartTable.Columns.Add("İskonto %", typeof(decimal));
            _cartTable.Columns.Add("Miktar", typeof(int));
            _cartTable.Columns.Add("KDV Tutarı", typeof(decimal));
            _cartTable.Columns.Add("İskonto Tutarı", typeof(decimal));
            _cartTable.Columns.Add("Toplam", typeof(decimal));

            dgvCart.DataSource = _cartTable;
            StyleDataGridView();
        }

        private void StyleDataGridView()
        {
            dgvCart.BackgroundColor = Color.FromArgb(242, 242, 242);
            dgvCart.BorderStyle = BorderStyle.None;
            dgvCart.GridColor = Color.FromArgb(224, 224, 224);
            dgvCart.RowHeadersVisible = false;
            dgvCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCart.DefaultCellStyle.SelectionBackColor = Color.FromArgb(45, 125, 154);
            dgvCart.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvCart.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);

            // Sütun görünümünü özelleştir
            dgvCart.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dgvCart.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCart.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            dgvCart.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCart.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCart.ColumnHeadersHeight = 40;
            
            // Belirli sütunları gizle
            dgvCart.Columns["ID"].Visible = false;
            
            // Sayısal değerler için hizalama
            dgvCart.Columns["Birim Fiyat"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCart.Columns["KDV %"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCart.Columns["İskonto %"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCart.Columns["Miktar"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCart.Columns["KDV Tutarı"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCart.Columns["İskonto Tutarı"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCart.Columns["Toplam"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            
            // Para birimi formatı
            dgvCart.Columns["Birim Fiyat"].DefaultCellStyle.Format = "N2";
            dgvCart.Columns["KDV Tutarı"].DefaultCellStyle.Format = "N2";
            dgvCart.Columns["İskonto Tutarı"].DefaultCellStyle.Format = "N2";
            dgvCart.Columns["Toplam"].DefaultCellStyle.Format = "N2";
        }

        // Form kontrol değişkenleri
        private System.ComponentModel.IContainer components = null;
        private MaterialTextBox txtBarcode;
        private MaterialButton btnProductSearch;
        private MaterialLabel lblQuantity;
        private NumericUpDown nudQuantity;
        private DataGridView dgvCart;
        private MaterialButton btnRemoveItem;
        private MaterialButton btnClearCart;
        private Panel pnlTotals;
        private MaterialLabel lblTotal;
        private MaterialLabel lblTotalValue;
        private MaterialLabel lblTax;
        private MaterialLabel lblTaxValue;
        private MaterialLabel lblDiscount;
        private MaterialLabel lblDiscountValue;
        private MaterialLabel lblGrandTotal;
        private MaterialLabel lblGrandTotalValue;
        private GroupBox gbPayment;
        private MaterialRadioButton rbCash;
        private MaterialRadioButton rbCreditCard;
        private MaterialRadioButton rbDebitCard;
        private MaterialRadioButton rbOther;
        private MaterialButton btnCancel;
        private MaterialButton btnComplete;

        // Olay işleyicileri
        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                
                string barcode = txtBarcode.Text.Trim();
                if (string.IsNullOrEmpty(barcode))
                {
                    MessageBox.Show("Lütfen barkod numarası girin.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                try
                {
                    var product = _productService.GetProductByBarcode(barcode);
                    if (product != null)
                    {
                        AddProductToCart(product);
                        txtBarcode.Clear();
                        txtBarcode.Focus();
                    }
                    else
                    {
                        MessageBox.Show($"Barkod numarası '{barcode}' olan ürün bulunamadı.", "Uyarı",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ürün arama sırasında hata oluştu: {ex.Message}", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var productSearchForm = new ProductSearchForm(_productService);
                if (productSearchForm.ShowDialog() == DialogResult.OK && productSearchForm.SelectedProduct != null)
                {
                    AddProductToCart(productSearchForm.SelectedProduct);
                    txtBarcode.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ürün arama sırasında hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCart_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Miktar değişikliği
                if (e.ColumnIndex == dgvCart.Columns["Miktar"].Index)
                {
                    DataRow row = ((DataRowView)dgvCart.Rows[e.RowIndex].DataBoundItem).Row;
                    int productId = Convert.ToInt32(row["ID"]);
                    int quantity;

                    if (int.TryParse(dgvCart.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out quantity))
                    {
                        if (quantity <= 0)
                        {
                            MessageBox.Show("Miktar 0'dan büyük olmalıdır.", "Uyarı",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            row["Miktar"] = 1;
                            return;
                        }

                        // Miktar değişikliğini güncelle
                        UpdateCartItemQuantity(productId, quantity);
                    }
                    else
                    {
                        MessageBox.Show("Geçerli bir miktar girin.", "Uyarı",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        row["Miktar"] = 1;
                    }
                }
                // İskonto değişikliği
                else if (e.ColumnIndex == dgvCart.Columns["İskonto %"].Index)
                {
                    DataRow row = ((DataRowView)dgvCart.Rows[e.RowIndex].DataBoundItem).Row;
                    int productId = Convert.ToInt32(row["ID"]);
                    decimal discountRate;

                    if (decimal.TryParse(dgvCart.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out discountRate))
                    {
                        if (discountRate < 0 || discountRate > 100)
                        {
                            MessageBox.Show("İskonto oranı 0-100 arasında olmalıdır.", "Uyarı",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            row["İskonto %"] = 0;
                            return;
                        }

                        // İskonto değişikliğini güncelle
                        UpdateCartItemDiscount(productId, discountRate);
                    }
                    else
                    {
                        MessageBox.Show("Geçerli bir iskonto oranı girin.", "Uyarı",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        row["İskonto %"] = 0;
                    }
                }

                CalculateTotals();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Satır düzenleme sırasında hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCart_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Satır seçildi, gerekirse burada işlem yapabilirsiniz
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count > 0)
            {
                try
                {
                    DataRow row = ((DataRowView)dgvCart.SelectedRows[0].DataBoundItem).Row;
                    int productId = Convert.ToInt32(row["ID"]);
                    
                    // Sepetten ürünü kaldır
                    _saleItems.RemoveAll(item => item.ProductId == productId);
                    _cartTable.Rows.Remove(row);
                    
                    CalculateTotals();
                    
                    txtBarcode.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ürün kaldırılırken hata oluştu: {ex.Message}", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen kaldırmak istediğiniz ürünü seçin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            if (_saleItems.Count > 0)
            {
                var result = MessageBox.Show("Sepetteki tüm ürünleri kaldırmak istediğinizden emin misiniz?",
                    "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    _saleItems.Clear();
                    _cartTable.Rows.Clear();
                    CalculateTotals();
                    
                    txtBarcode.Focus();
                }
            }
            else
            {
                MessageBox.Show("Sepet zaten boş.", "Bilgi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_saleItems.Count > 0)
            {
                var result = MessageBox.Show("Satışı iptal etmek istediğinizden emin misiniz?",
                    "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            if (_saleItems.Count == 0)
            {
                MessageBox.Show("Satış yapmak için sepete ürün ekleyin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                // Yeni satış nesnesi oluştur
                var sale = new Sale
                {
                    UserId = 1, // TODO: Giriş yapmış kullanıcı ID'si alınmalı
                    SaleDate = DateTime.Now,
                    TotalAmount = _totalAmount,
                    TaxAmount = _taxAmount,
                    DiscountAmount = _discountAmount,
                    FinalAmount = _finalAmount
                };
                
                // Ödeme yöntemini belirle
                if (rbCash.Checked)
                    sale.PaymentMethod = PaymentMethod.Cash;
                else if (rbCreditCard.Checked)
                    sale.PaymentMethod = PaymentMethod.CreditCard;
                else if (rbDebitCard.Checked)
                    sale.PaymentMethod = PaymentMethod.DebitCard;
                else
                    sale.PaymentMethod = PaymentMethod.Other;
                
                // Satış işlemini gerçekleştir
                var completedSale = _saleService.CreateSale(sale, _saleItems);
                
                if (completedSale != null)
                {
                    // Fiş/fatura yazdırma eklenebilir
                    MessageBox.Show($"Satış başarıyla tamamlandı. Fatura No: {completedSale.InvoiceNo}", "Bilgi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Satış tamamlanırken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Yardımcı metodlar
        private void AddProductToCart(Product product)
        {
            try
            {
                // Ürün zaten sepette var mı?
                var existingItem = _saleItems.FirstOrDefault(item => item.ProductId == product.Id);
                
                if (existingItem != null)
                {
                    // Ürün zaten sepette, miktarı arttır
                    existingItem.Quantity += (int)nudQuantity.Value;
                    existingItem.CalculateTaxAmount();
                    existingItem.CalculateDiscountAmount();
                    existingItem.CalculateTotalAmount();
                    
                    // DataTable'ı güncelle
                    foreach (DataRow row in _cartTable.Rows)
                    {
                        if (Convert.ToInt32(row["ID"]) == product.Id)
                        {
                            row["Miktar"] = existingItem.Quantity;
                            row["KDV Tutarı"] = existingItem.TaxAmount;
                            row["İskonto Tutarı"] = existingItem.DiscountAmount;
                            row["Toplam"] = existingItem.TotalAmount;
                            break;
                        }
                    }
                }
                else
                {
                    // Yeni ürün ekle
                    var newItem = new SaleItem
                    {
                        ProductId = product.Id,
                        UnitPrice = product.SalePrice,
                        Quantity = (int)nudQuantity.Value,
                        TaxRate = product.TaxRate,
                        DiscountRate = 0 // Varsayılan iskonto oranı
                    };
                    
                    newItem.CalculateTaxAmount();
                    newItem.CalculateDiscountAmount();
                    newItem.CalculateTotalAmount();
                    
                    _saleItems.Add(newItem);
                    
                    // DataTable'a ekle
                    DataRow newRow = _cartTable.NewRow();
                    newRow["ID"] = product.Id;
                    newRow["Ürün Kodu"] = product.Barcode;
                    newRow["Ürün Adı"] = product.Name;
                    newRow["Birim Fiyat"] = product.SalePrice;
                    newRow["KDV %"] = product.TaxRate;
                    newRow["İskonto %"] = newItem.DiscountRate;
                    newRow["Miktar"] = newItem.Quantity;
                    newRow["KDV Tutarı"] = newItem.TaxAmount;
                    newRow["İskonto Tutarı"] = newItem.DiscountAmount;
                    newRow["Toplam"] = newItem.TotalAmount;
                    
                    _cartTable.Rows.Add(newRow);
                }
                
                CalculateTotals();
                
                // Miktar değerini sıfırla
                nudQuantity.Value = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ürün sepete eklenirken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateCartItemQuantity(int productId, int quantity)
        {
            var item = _saleItems.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                item.Quantity = quantity;
                item.CalculateTaxAmount();
                item.CalculateDiscountAmount();
                item.CalculateTotalAmount();
                
                // DataTable'ı güncelle
                foreach (DataRow row in _cartTable.Rows)
                {
                    if (Convert.ToInt32(row["ID"]) == productId)
                    {
                        row["KDV Tutarı"] = item.TaxAmount;
                        row["İskonto Tutarı"] = item.DiscountAmount;
                        row["Toplam"] = item.TotalAmount;
                        break;
                    }
                }
                
                CalculateTotals();
            }
        }

        private void UpdateCartItemDiscount(int productId, decimal discountRate)
        {
            var item = _saleItems.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                item.DiscountRate = discountRate;
                item.CalculateTaxAmount();
                item.CalculateDiscountAmount();
                item.CalculateTotalAmount();
                
                // DataTable'ı güncelle
                foreach (DataRow row in _cartTable.Rows)
                {
                    if (Convert.ToInt32(row["ID"]) == productId)
                    {
                        row["KDV Tutarı"] = item.TaxAmount;
                        row["İskonto Tutarı"] = item.DiscountAmount;
                        row["Toplam"] = item.TotalAmount;
                        break;
                    }
                }
                
                CalculateTotals();
            }
        }

        private void CalculateTotals()
        {
            _totalAmount = 0;
            _taxAmount = 0;
            _discountAmount = 0;
            
            foreach (var item in _saleItems)
            {
                _totalAmount += item.UnitPrice * item.Quantity;
                _taxAmount += item.TaxAmount;
                _discountAmount += item.DiscountAmount;
            }
            
            _finalAmount = _totalAmount + _taxAmount - _discountAmount;
            
            // UI güncelleme
            lblTotalValue.Text = $"{_totalAmount:N2} ₺";
            lblTaxValue.Text = $"{_taxAmount:N2} ₺";
            lblDiscountValue.Text = $"{_discountAmount:N2} ₺";
            lblGrandTotalValue.Text = $"{_finalAmount:N2} ₺";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
} 