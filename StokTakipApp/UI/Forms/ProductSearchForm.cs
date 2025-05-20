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
    public partial class ProductSearchForm : MaterialForm
    {
        private readonly IProductService _productService;
        private List<Product> _products;
        
        public Product SelectedProduct { get; private set; }

        public ProductSearchForm(IProductService productService)
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
            _products = new List<Product>();
        }

        private void InitializeComponent()
        {
            this.txtSearch = new MaterialSkin.Controls.MaterialTextBox();
            this.btnSearch = new MaterialSkin.Controls.MaterialButton();
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.btnSelect = new MaterialSkin.Controls.MaterialButton();
            this.btnCancel = new MaterialSkin.Controls.MaterialButton();
            
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            this.SuspendLayout();
            
            // Temel form özellikleri
            this.Text = "Ürün Arama";
            this.Size = new System.Drawing.Size(800, 500);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            
            // Arama kutusu
            this.txtSearch.Hint = "Ürün adı, barkod veya açıklama";
            this.txtSearch.Location = new System.Drawing.Point(20, 80);
            this.txtSearch.Size = new System.Drawing.Size(300, 50);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            
            // Arama butonu
            this.btnSearch.Text = "ARA";
            this.btnSearch.Location = new System.Drawing.Point(330, 85);
            this.btnSearch.Size = new System.Drawing.Size(100, 40);
            this.btnSearch.UseAccentColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            
            // Ürün listesi
            this.dgvProducts.Location = new System.Drawing.Point(20, 150);
            this.dgvProducts.Size = new System.Drawing.Size(760, 280);
            this.dgvProducts.BackgroundColor = System.Drawing.Color.FromArgb(242, 242, 242);
            this.dgvProducts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProducts.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvProducts.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProducts.EnableHeadersVisualStyles = false;
            this.dgvProducts.RowHeadersVisible = false;
            this.dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProducts_CellDoubleClick);
            this.dgvProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            this.dgvProducts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dgvProducts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            this.dgvProducts.ColumnHeadersHeight = 40;
            
            // Seçim butonu
            this.btnSelect.Text = "SEÇ";
            this.btnSelect.Location = new System.Drawing.Point(680, 440);
            this.btnSelect.Size = new System.Drawing.Size(100, 40);
            this.btnSelect.UseAccentColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            
            // İptal butonu
            this.btnCancel.Text = "İPTAL";
            this.btnCancel.Location = new System.Drawing.Point(570, 440);
            this.btnCancel.Size = new System.Drawing.Size(100, 40);
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            
            // Kontrolleri forma ekle
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dgvProducts);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnCancel);
            
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
            
            // Form yüklendiğinde
            this.Load += new System.EventHandler(this.ProductSearchForm_Load);
        }

        private void ProductSearchForm_Load(object sender, EventArgs e)
        {
            LoadAllProducts();
            txtSearch.Focus();
        }

        private void LoadAllProducts()
        {
            try
            {
                _products = _productService.GetAllProducts().ToList();
                BindProductsToGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ürünler yüklenirken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchProducts()
        {
            string searchText = txtSearch.Text.Trim().ToLower();
            
            if (string.IsNullOrEmpty(searchText))
            {
                LoadAllProducts();
                return;
            }
            
            try
            {
                var allProducts = _productService.GetAllProducts();
                
                _products = allProducts.Where(p => 
                    p.Name.ToLower().Contains(searchText) || 
                    p.Barcode.ToLower().Contains(searchText) || 
                    (p.Description != null && p.Description.ToLower().Contains(searchText)))
                    .ToList();
                
                BindProductsToGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ürün araması sırasında hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindProductsToGrid()
        {
            var productList = _products.Select(p => new
            {
                ID = p.Id,
                Barkod = p.Barcode,
                Ürün_Adı = p.Name,
                Fiyat = p.SalePrice,
                KDV = p.TaxRate,
                Stok = p.StockQuantity,
                Kategori = p.Category?.Name,
                Marka = p.Brand?.Name
            }).ToList();
            
            dgvProducts.DataSource = productList;
            
            // ID sütunu gizle
            if (dgvProducts.Columns.Contains("ID"))
                dgvProducts.Columns["ID"].Visible = false;
            
            // Belirli sütunları gizle (isteğe bağlı)
            if (dgvProducts.Columns.Contains("KDV"))
                dgvProducts.Columns["KDV"].DefaultCellStyle.Format = "%0";
            
            if (dgvProducts.Columns.Contains("Fiyat"))
                dgvProducts.Columns["Fiyat"].DefaultCellStyle.Format = "N2";
        }

        private void SelectProduct()
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                int productId = Convert.ToInt32(dgvProducts.SelectedRows[0].Cells["ID"].Value);
                SelectedProduct = _products.FirstOrDefault(p => p.Id == productId);
                
                if (SelectedProduct != null)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir ürün seçin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                SearchProducts();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchProducts();
        }

        private void dgvProducts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                SelectProduct();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectProduct();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Form kontrol değişkenleri
        private MaterialTextBox txtSearch;
        private MaterialButton btnSearch;
        private DataGridView dgvProducts;
        private MaterialButton btnSelect;
        private MaterialButton btnCancel;
    }
} 