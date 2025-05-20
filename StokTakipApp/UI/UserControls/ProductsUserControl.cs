using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entities;
using BLL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MaterialSkin.Controls;

namespace UI.UserControls
{
    public partial class ProductsUserControl : UserControl
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;

        public ProductsUserControl()
        {
            InitializeComponent();
            
            try
            {
                // Servisler
                _productService = Program.ServiceProvider.GetRequiredService<IProductService>();
                _categoryService = Program.ServiceProvider.GetRequiredService<ICategoryService>();
                _brandService = Program.ServiceProvider.GetRequiredService<IBrandService>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Servis başlatılırken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.btnAdd = new MaterialSkin.Controls.MaterialButton();
            this.btnEdit = new MaterialSkin.Controls.MaterialButton();
            this.btnDelete = new MaterialSkin.Controls.MaterialButton();
            this.txtSearch = new MaterialSkin.Controls.MaterialTextBox();
            this.btnSearch = new MaterialSkin.Controls.MaterialButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvProducts
            // 
            this.dgvProducts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProducts.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.dgvProducts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProducts.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvProducts.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProducts.EnableHeadersVisualStyles = false;
            this.dgvProducts.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvProducts.Location = new System.Drawing.Point(20, 70);
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.ReadOnly = true;
            this.dgvProducts.RowHeadersVisible = false;
            this.dgvProducts.RowTemplate.Height = 28;
            this.dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.Size = new System.Drawing.Size(760, 350);
            this.dgvProducts.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAdd.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnAdd.Depth = 0;
            this.btnAdd.HighEmphasis = true;
            this.btnAdd.Icon = null;
            this.btnAdd.Location = new System.Drawing.Point(20, 20);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnAdd.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnAdd.Size = new System.Drawing.Size(100, 36);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "EKLE";
            this.btnAdd.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnAdd.UseAccentColor = false;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnEdit.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnEdit.Depth = 0;
            this.btnEdit.HighEmphasis = true;
            this.btnEdit.Icon = null;
            this.btnEdit.Location = new System.Drawing.Point(130, 20);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnEdit.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnEdit.Size = new System.Drawing.Size(100, 36);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "DÜZENLE";
            this.btnEdit.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnEdit.UseAccentColor = false;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDelete.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnDelete.Depth = 0;
            this.btnDelete.HighEmphasis = true;
            this.btnDelete.Icon = null;
            this.btnDelete.Location = new System.Drawing.Point(240, 20);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnDelete.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnDelete.Size = new System.Drawing.Size(100, 36);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "SİL";
            this.btnDelete.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnDelete.UseAccentColor = false;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Depth = 0;
            this.txtSearch.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtSearch.Hint = "Ürün Ara...";
            this.txtSearch.Location = new System.Drawing.Point(470, 20);
            this.txtSearch.MaxLength = 50;
            this.txtSearch.MouseState = MaterialSkin.MouseState.OUT;
            this.txtSearch.Multiline = false;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(200, 50);
            this.txtSearch.TabIndex = 4;
            this.txtSearch.Text = "";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSearch.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnSearch.Depth = 0;
            this.btnSearch.HighEmphasis = true;
            this.btnSearch.Icon = null;
            this.btnSearch.Location = new System.Drawing.Point(680, 20);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnSearch.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnSearch.Size = new System.Drawing.Size(100, 36);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "ARA";
            this.btnSearch.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnSearch.UseAccentColor = false;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // ProductsUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgvProducts);
            this.Name = "ProductsUserControl";
            this.Size = new System.Drawing.Size(800, 430);
            this.Load += new System.EventHandler(this.ProductsUserControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private DataGridView dgvProducts;
        private MaterialButton btnAdd;
        private MaterialButton btnEdit;
        private MaterialButton btnDelete;
        private MaterialTextBox txtSearch;
        private MaterialButton btnSearch;

        private void ProductsUserControl_Load(object sender, EventArgs e)
        {
            LoadProducts();
            StyleDataGridView();
        }
        
        private void StyleDataGridView()
        {
            // DataGridView görünümünü özelleştir
            dgvProducts.BackgroundColor = Color.FromArgb(242, 242, 242); // Açık gri arka plan
            dgvProducts.GridColor = Color.FromArgb(224, 224, 224);       // Izgara çizgileri rengi
            
            // Sütun başlıkları
            dgvProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50); // Koyu gri başlık
            dgvProducts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProducts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            dgvProducts.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProducts.ColumnHeadersHeight = 40;
            dgvProducts.ColumnHeadersDefaultCellStyle.Padding = new Padding(5);
            
            // Normal hücreler için stil
            dgvProducts.DefaultCellStyle.BackColor = Color.FromArgb(242, 242, 242); // Ana satır rengi
            dgvProducts.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvProducts.DefaultCellStyle.Padding = new Padding(5);
            dgvProducts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(75, 75, 75); // Koyu gri seçim
            dgvProducts.DefaultCellStyle.SelectionForeColor = Color.White;
            
            // Alternatif satır rengi
            dgvProducts.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 230, 230); // Biraz daha koyu gri
            dgvProducts.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(75, 75, 75);
            dgvProducts.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;
            
            // Diğer ayarlar
            dgvProducts.RowTemplate.Height = 30;
            dgvProducts.RowHeadersVisible = false;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.BorderStyle = BorderStyle.None;
            dgvProducts.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        }

        private void LoadProducts()
        {
            try
            {
                // Servislerin doğru yüklendiğini kontrol et
                if (_productService == null)
                {
                    MessageBox.Show("Ürün servisi başlatılamadı.", "Hata", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Ürünleri yükle
                var products = _productService.GetAllProducts();
                
                // Ürünlerin null olmadığını kontrol et, ama boş olabilir
                if (products == null)
                {
                    MessageBox.Show("Ürün verileri alınamadı.", "Hata", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // Ürün listesi boş olabilir, kullanıcıya bilgi verelim ama hata değil
                if (products.Count == 0)
                {
                    MessageBox.Show("Henüz hiç ürün eklenmemiş.", "Bilgi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvProducts.DataSource = null;
                    return;
                }

                dgvProducts.DataSource = products;

                // Sütun başlıklarını ve görünümü düzenle
                ConfigureDataGridColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ürünler yüklenirken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void ConfigureDataGridColumns()
        {
            // Sütun başlıklarını ayarla
            if (dgvProducts.Columns.Contains("Id"))
                dgvProducts.Columns["Id"].HeaderText = "ID";
            
            if (dgvProducts.Columns.Contains("Name"))
                dgvProducts.Columns["Name"].HeaderText = "Ürün Adı";
            
            if (dgvProducts.Columns.Contains("Barcode"))
                dgvProducts.Columns["Barcode"].HeaderText = "Barkod";
            
            if (dgvProducts.Columns.Contains("Description"))
                dgvProducts.Columns["Description"].HeaderText = "Açıklama";
            
            if (dgvProducts.Columns.Contains("PurchasePrice"))
                dgvProducts.Columns["PurchasePrice"].HeaderText = "Alış Fiyatı";
            
            if (dgvProducts.Columns.Contains("SalePrice"))
                dgvProducts.Columns["SalePrice"].HeaderText = "Satış Fiyatı";
            
            if (dgvProducts.Columns.Contains("StockQuantity"))
                dgvProducts.Columns["StockQuantity"].HeaderText = "Stok Miktarı";
            
            if (dgvProducts.Columns.Contains("MinimumStockLevel"))
                dgvProducts.Columns["MinimumStockLevel"].HeaderText = "Min. Stok";

            // İlişkili verileri gizle
            if (dgvProducts.Columns.Contains("Category"))
                dgvProducts.Columns["Category"].Visible = false;
            
            if (dgvProducts.Columns.Contains("Brand"))
                dgvProducts.Columns["Brand"].Visible = false;
            
            if (dgvProducts.Columns.Contains("StockMovements"))
                dgvProducts.Columns["StockMovements"].Visible = false;
            
            if (dgvProducts.Columns.Contains("CategoryId"))
                dgvProducts.Columns["CategoryId"].Visible = false;
            
            if (dgvProducts.Columns.Contains("BrandId"))
                dgvProducts.Columns["BrandId"].Visible = false;

            // Kategori adını görüntülemek için bir sütun ekle
            if (!dgvProducts.Columns.Contains("CategoryName"))
            {
                var categoryColumn = new DataGridViewTextBoxColumn();
                categoryColumn.HeaderText = "Kategori";
                categoryColumn.Name = "CategoryName";
                categoryColumn.DataPropertyName = "CategoryName";
                dgvProducts.Columns.Add(categoryColumn);

                // Kategori adlarını güncelle
                foreach (DataGridViewRow row in dgvProducts.Rows)
                {
                    var product = row.DataBoundItem as Product;
                    if (product?.Category != null)
                    {
                        row.Cells["CategoryName"].Value = product.Category.Name;
                    }
                }
            }
            
            // Marka adını görüntülemek için bir sütun ekle
            if (!dgvProducts.Columns.Contains("BrandName"))
            {
                var brandColumn = new DataGridViewTextBoxColumn();
                brandColumn.HeaderText = "Marka";
                brandColumn.Name = "BrandName";
                brandColumn.DataPropertyName = "BrandName";
                dgvProducts.Columns.Add(brandColumn);

                // Marka adlarını güncelle
                foreach (DataGridViewRow row in dgvProducts.Rows)
                {
                    var product = row.DataBoundItem as Product;
                    if (product?.Brand != null)
                    {
                        row.Cells["BrandName"].Value = product.Brand.Name;
                    }
                }
            }
            
            // Para birimi formatını ayarla
            if (dgvProducts.Columns.Contains("PurchasePrice"))
            {
                dgvProducts.Columns["PurchasePrice"].DefaultCellStyle.Format = "N2";
                dgvProducts.Columns["PurchasePrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            
            if (dgvProducts.Columns.Contains("SalePrice"))
            {
                dgvProducts.Columns["SalePrice"].DefaultCellStyle.Format = "N2";
                dgvProducts.Columns["SalePrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            
            // Stok miktarı hücre formatını ayarla
            if (dgvProducts.Columns.Contains("StockQuantity"))
            {
                dgvProducts.Columns["StockQuantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            
            if (dgvProducts.Columns.Contains("MinimumStockLevel"))
            {
                dgvProducts.Columns["MinimumStockLevel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Kategori ve marka servislerinin doğru yüklendiğini kontrol et
                if (_categoryService == null)
                {
                    MessageBox.Show("Kategori servisi başlatılamadı.", "Hata", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                if (_brandService == null)
                {
                    MessageBox.Show("Marka servisi başlatılamadı.", "Hata", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Ürün ekleme formunu hazırla
                var addProductForm = new Forms.ProductForm();
                
                // Formun başlığını ayarla
                addProductForm.Text = "Yeni Ürün Ekle";
                
                // Kategori ve marka listelerini yükle
                addProductForm.LoadCategoriesAndBrands(_categoryService, _brandService);
                
                // Formu göster ve sonucu al
                if (addProductForm.ShowDialog() == DialogResult.OK)
                {
                    // Formdan ürün bilgilerini al
                    var newProduct = addProductForm.GetProduct();
                    
                    // Ürünü ekle
                    _productService.AddProduct(newProduct);
                    
                    // Ürün listesini yenile
                    LoadProducts();
                    
                    MessageBox.Show("Ürün başarıyla eklendi.", "Bilgi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ürün eklenirken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                // Servislerin doğru yüklendiğini kontrol et
                if (_productService == null || _categoryService == null || _brandService == null)
                {
                    MessageBox.Show("Gerekli servisler başlatılamadı.", "Hata", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                if (dgvProducts.SelectedRows.Count > 0)
                {
                    var selectedProduct = dgvProducts.SelectedRows[0].DataBoundItem as Product;
                    if (selectedProduct == null)
                    {
                        MessageBox.Show("Seçili ürün bilgisi alınamadı.", "Hata", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                    // Düzenleme formunu oluştur
                    var editProductForm = new Forms.ProductForm(selectedProduct);
                    
                    // Formun başlığını ayarla
                    editProductForm.Text = "Ürün Düzenle";
                    
                    // Kategori ve marka listelerini yükle
                    editProductForm.LoadCategoriesAndBrands(_categoryService, _brandService);
                    
                    // Formu göster ve sonucu al
                    if (editProductForm.ShowDialog() == DialogResult.OK)
                    {
                        // Formdan güncellenmiş ürün bilgilerini al
                        var updatedProduct = editProductForm.GetProduct();
                        
                        // Ürünü güncelle
                        _productService.UpdateProduct(updatedProduct);
                        
                        // Ürün listesini yenile
                        LoadProducts();
                        
                        MessageBox.Show("Ürün başarıyla güncellendi.", "Bilgi", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen düzenlemek için bir ürün seçin.", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ürün düzenlenirken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Ürün servisinin doğru yüklendiğini kontrol et
                if (_productService == null)
                {
                    MessageBox.Show("Ürün servisi başlatılamadı.", "Hata", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                if (dgvProducts.SelectedRows.Count > 0)
                {
                    var selectedProduct = dgvProducts.SelectedRows[0].DataBoundItem as Product;
                    if (selectedProduct == null)
                    {
                        MessageBox.Show("Seçili ürün bilgisi alınamadı.", "Hata", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                    var result = MessageBox.Show($"{selectedProduct.Name} ürününü silmek istediğinizden emin misiniz?", 
                        "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        // Ürünü sil
                        _productService.DeleteProduct(selectedProduct.Id);
                        
                        // Ürün listesini yenile
                        LoadProducts();
                        
                        MessageBox.Show("Ürün başarıyla silindi.", "Bilgi", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen silmek için bir ürün seçin.", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ürün silinirken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchText))
            {
                try
                {
                    var products = _productService.SearchProducts(searchText);
                    dgvProducts.DataSource = products;
                    StyleDataGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Arama sırasında hata oluştu: {ex.Message}", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                LoadProducts();
            }
        }
    }
} 