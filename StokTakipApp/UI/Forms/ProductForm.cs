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
using MaterialSkin;
using MaterialSkin.Controls;

namespace UI.Forms
{
    public partial class ProductForm : MaterialForm
    {
        private readonly Product _product;
        private readonly bool _isEditMode;
        
        public ProductForm()
        {
            InitializeComponent();
            
            // Material tasarım ayarları
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue800,
                Primary.Blue900,
                Primary.Blue500,
                Accent.LightBlue200,
                TextShade.WHITE
            );
            
            _product = new Product();
            _isEditMode = false;
        }
        
        public ProductForm(Product product) : this()
        {
            _product = product ?? throw new ArgumentNullException(nameof(product));
            _isEditMode = true;
            
            // Mevcut ürün bilgilerini form kontrollerine yükle
            txtName.Text = _product.Name;
            txtBarcode.Text = _product.Barcode;
            txtDescription.Text = _product.Description;
            txtPurchasePrice.Text = _product.PurchasePrice.ToString("N2");
            txtSalePrice.Text = _product.SalePrice.ToString("N2");
            txtStockQuantity.Text = _product.StockQuantity.ToString();
            txtMinimumStockLevel.Text = _product.MinimumStockLevel.ToString();
            
            // Barkod düzenlenemez yapılsın (eşsiz alan olduğu için)
            if (_isEditMode)
            {
                txtBarcode.Enabled = false;
            }
        }
        
        private void InitializeComponent()
        {
            this.txtName = new MaterialSkin.Controls.MaterialTextBox();
            this.txtBarcode = new MaterialSkin.Controls.MaterialTextBox();
            this.txtDescription = new MaterialSkin.Controls.MaterialMultiLineTextBox();
            this.txtPurchasePrice = new MaterialSkin.Controls.MaterialTextBox();
            this.txtSalePrice = new MaterialSkin.Controls.MaterialTextBox();
            this.txtStockQuantity = new MaterialSkin.Controls.MaterialTextBox();
            this.txtMinimumStockLevel = new MaterialSkin.Controls.MaterialTextBox();
            this.cmbCategory = new MaterialSkin.Controls.MaterialComboBox();
            this.cmbBrand = new MaterialSkin.Controls.MaterialComboBox();
            this.btnSave = new MaterialSkin.Controls.MaterialButton();
            this.btnCancel = new MaterialSkin.Controls.MaterialButton();
            this.lblName = new MaterialSkin.Controls.MaterialLabel();
            this.lblBarcode = new MaterialSkin.Controls.MaterialLabel();
            this.lblDescription = new MaterialSkin.Controls.MaterialLabel();
            this.lblCategory = new MaterialSkin.Controls.MaterialLabel();
            this.lblBrand = new MaterialSkin.Controls.MaterialLabel();
            this.lblPurchasePrice = new MaterialSkin.Controls.MaterialLabel();
            this.lblSalePrice = new MaterialSkin.Controls.MaterialLabel();
            this.lblStockQuantity = new MaterialSkin.Controls.MaterialLabel();
            this.lblMinimumStockLevel = new MaterialSkin.Controls.MaterialLabel();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.AnimateReadOnly = false;
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.Depth = 0;
            this.txtName.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtName.LeadingIcon = null;
            this.txtName.Location = new System.Drawing.Point(30, 120);
            this.txtName.MaxLength = 50;
            this.txtName.MouseState = MaterialSkin.MouseState.OUT;
            this.txtName.Multiline = false;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(250, 50);
            this.txtName.TabIndex = 0;
            this.txtName.Text = "";
            this.txtName.TrailingIcon = null;
            // 
            // txtBarcode
            // 
            this.txtBarcode.AnimateReadOnly = false;
            this.txtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBarcode.Depth = 0;
            this.txtBarcode.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtBarcode.LeadingIcon = null;
            this.txtBarcode.Location = new System.Drawing.Point(320, 120);
            this.txtBarcode.MaxLength = 20;
            this.txtBarcode.MouseState = MaterialSkin.MouseState.OUT;
            this.txtBarcode.Multiline = false;
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(250, 50);
            this.txtBarcode.TabIndex = 1;
            this.txtBarcode.Text = "";
            this.txtBarcode.TrailingIcon = null;
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDescription.Depth = 0;
            this.txtDescription.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtDescription.Location = new System.Drawing.Point(30, 210);
            this.txtDescription.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(540, 80);
            this.txtDescription.TabIndex = 2;
            this.txtDescription.Text = "";
            // 
            // cmbCategory
            // 
            this.cmbCategory.AutoResize = false;
            this.cmbCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbCategory.Depth = 0;
            this.cmbCategory.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbCategory.DropDownHeight = 174;
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.DropDownWidth = 121;
            this.cmbCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.IntegralHeight = false;
            this.cmbCategory.ItemHeight = 43;
            this.cmbCategory.Location = new System.Drawing.Point(30, 330);
            this.cmbCategory.MaxDropDownItems = 4;
            this.cmbCategory.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(250, 49);
            this.cmbCategory.StartIndex = 0;
            this.cmbCategory.TabIndex = 3;
            // 
            // cmbBrand
            // 
            this.cmbBrand.AutoResize = false;
            this.cmbBrand.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbBrand.Depth = 0;
            this.cmbBrand.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbBrand.DropDownHeight = 174;
            this.cmbBrand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBrand.DropDownWidth = 121;
            this.cmbBrand.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbBrand.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbBrand.FormattingEnabled = true;
            this.cmbBrand.IntegralHeight = false;
            this.cmbBrand.ItemHeight = 43;
            this.cmbBrand.Location = new System.Drawing.Point(320, 330);
            this.cmbBrand.MaxDropDownItems = 4;
            this.cmbBrand.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbBrand.Name = "cmbBrand";
            this.cmbBrand.Size = new System.Drawing.Size(250, 49);
            this.cmbBrand.StartIndex = 0;
            this.cmbBrand.TabIndex = 4;
            // 
            // txtPurchasePrice
            // 
            this.txtPurchasePrice.AnimateReadOnly = false;
            this.txtPurchasePrice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPurchasePrice.Depth = 0;
            this.txtPurchasePrice.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtPurchasePrice.LeadingIcon = null;
            this.txtPurchasePrice.Location = new System.Drawing.Point(30, 420);
            this.txtPurchasePrice.MaxLength = 10;
            this.txtPurchasePrice.MouseState = MaterialSkin.MouseState.OUT;
            this.txtPurchasePrice.Multiline = false;
            this.txtPurchasePrice.Name = "txtPurchasePrice";
            this.txtPurchasePrice.Size = new System.Drawing.Size(120, 50);
            this.txtPurchasePrice.TabIndex = 5;
            this.txtPurchasePrice.Text = "0";
            this.txtPurchasePrice.TrailingIcon = null;
            // 
            // txtSalePrice
            // 
            this.txtSalePrice.AnimateReadOnly = false;
            this.txtSalePrice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSalePrice.Depth = 0;
            this.txtSalePrice.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtSalePrice.LeadingIcon = null;
            this.txtSalePrice.Location = new System.Drawing.Point(160, 420);
            this.txtSalePrice.MaxLength = 10;
            this.txtSalePrice.MouseState = MaterialSkin.MouseState.OUT;
            this.txtSalePrice.Multiline = false;
            this.txtSalePrice.Name = "txtSalePrice";
            this.txtSalePrice.Size = new System.Drawing.Size(120, 50);
            this.txtSalePrice.TabIndex = 6;
            this.txtSalePrice.Text = "0";
            this.txtSalePrice.TrailingIcon = null;
            // 
            // txtStockQuantity
            // 
            this.txtStockQuantity.AnimateReadOnly = false;
            this.txtStockQuantity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStockQuantity.Depth = 0;
            this.txtStockQuantity.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtStockQuantity.LeadingIcon = null;
            this.txtStockQuantity.Location = new System.Drawing.Point(320, 420);
            this.txtStockQuantity.MaxLength = 5;
            this.txtStockQuantity.MouseState = MaterialSkin.MouseState.OUT;
            this.txtStockQuantity.Multiline = false;
            this.txtStockQuantity.Name = "txtStockQuantity";
            this.txtStockQuantity.Size = new System.Drawing.Size(120, 50);
            this.txtStockQuantity.TabIndex = 7;
            this.txtStockQuantity.Text = "0";
            this.txtStockQuantity.TrailingIcon = null;
            // 
            // txtMinimumStockLevel
            // 
            this.txtMinimumStockLevel.AnimateReadOnly = false;
            this.txtMinimumStockLevel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMinimumStockLevel.Depth = 0;
            this.txtMinimumStockLevel.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtMinimumStockLevel.LeadingIcon = null;
            this.txtMinimumStockLevel.Location = new System.Drawing.Point(450, 420);
            this.txtMinimumStockLevel.MaxLength = 5;
            this.txtMinimumStockLevel.MouseState = MaterialSkin.MouseState.OUT;
            this.txtMinimumStockLevel.Multiline = false;
            this.txtMinimumStockLevel.Name = "txtMinimumStockLevel";
            this.txtMinimumStockLevel.Size = new System.Drawing.Size(120, 50);
            this.txtMinimumStockLevel.TabIndex = 8;
            this.txtMinimumStockLevel.Text = "0";
            this.txtMinimumStockLevel.TrailingIcon = null;
            // 
            // btnSave
            // 
            this.btnSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSave.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnSave.Depth = 0;
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.HighEmphasis = true;
            this.btnSave.Icon = null;
            this.btnSave.Location = new System.Drawing.Point(470, 500);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnSave.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSave.Name = "btnSave";
            this.btnSave.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnSave.Size = new System.Drawing.Size(100, 36);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "KAYDET";
            this.btnSave.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnSave.UseAccentColor = false;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCancel.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnCancel.Depth = 0;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.HighEmphasis = true;
            this.btnCancel.Icon = null;
            this.btnCancel.Location = new System.Drawing.Point(350, 500);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnCancel.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnCancel.Size = new System.Drawing.Size(100, 36);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "İPTAL";
            this.btnCancel.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            this.btnCancel.UseAccentColor = false;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Depth = 0;
            this.lblName.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblName.Location = new System.Drawing.Point(30, 90);
            this.lblName.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(62, 19);
            this.lblName.TabIndex = 11;
            this.lblName.Text = "Ürün Adı";
            // 
            // lblBarcode
            // 
            this.lblBarcode.AutoSize = true;
            this.lblBarcode.Depth = 0;
            this.lblBarcode.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblBarcode.Location = new System.Drawing.Point(320, 90);
            this.lblBarcode.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Size = new System.Drawing.Size(48, 19);
            this.lblBarcode.TabIndex = 12;
            this.lblBarcode.Text = "Barkod";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Depth = 0;
            this.lblDescription.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblDescription.Location = new System.Drawing.Point(30, 180);
            this.lblDescription.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(67, 19);
            this.lblDescription.TabIndex = 13;
            this.lblDescription.Text = "Açıklama";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Depth = 0;
            this.lblCategory.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblCategory.Location = new System.Drawing.Point(30, 300);
            this.lblCategory.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(58, 19);
            this.lblCategory.TabIndex = 14;
            this.lblCategory.Text = "Kategori";
            // 
            // lblBrand
            // 
            this.lblBrand.AutoSize = true;
            this.lblBrand.Depth = 0;
            this.lblBrand.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblBrand.Location = new System.Drawing.Point(320, 300);
            this.lblBrand.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblBrand.Name = "lblBrand";
            this.lblBrand.Size = new System.Drawing.Size(44, 19);
            this.lblBrand.TabIndex = 15;
            this.lblBrand.Text = "Marka";
            // 
            // lblPurchasePrice
            // 
            this.lblPurchasePrice.AutoSize = true;
            this.lblPurchasePrice.Depth = 0;
            this.lblPurchasePrice.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblPurchasePrice.Location = new System.Drawing.Point(30, 390);
            this.lblPurchasePrice.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblPurchasePrice.Name = "lblPurchasePrice";
            this.lblPurchasePrice.Size = new System.Drawing.Size(66, 19);
            this.lblPurchasePrice.TabIndex = 16;
            this.lblPurchasePrice.Text = "Alış Fiyatı";
            // 
            // lblSalePrice
            // 
            this.lblSalePrice.AutoSize = true;
            this.lblSalePrice.Depth = 0;
            this.lblSalePrice.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblSalePrice.Location = new System.Drawing.Point(160, 390);
            this.lblSalePrice.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblSalePrice.Name = "lblSalePrice";
            this.lblSalePrice.Size = new System.Drawing.Size(75, 19);
            this.lblSalePrice.TabIndex = 17;
            this.lblSalePrice.Text = "Satış Fiyatı";
            // 
            // lblStockQuantity
            // 
            this.lblStockQuantity.AutoSize = true;
            this.lblStockQuantity.Depth = 0;
            this.lblStockQuantity.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblStockQuantity.Location = new System.Drawing.Point(320, 390);
            this.lblStockQuantity.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblStockQuantity.Name = "lblStockQuantity";
            this.lblStockQuantity.Size = new System.Drawing.Size(86, 19);
            this.lblStockQuantity.TabIndex = 18;
            this.lblStockQuantity.Text = "Stok Miktarı";
            // 
            // lblMinimumStockLevel
            // 
            this.lblMinimumStockLevel.AutoSize = true;
            this.lblMinimumStockLevel.Depth = 0;
            this.lblMinimumStockLevel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblMinimumStockLevel.Location = new System.Drawing.Point(450, 390);
            this.lblMinimumStockLevel.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblMinimumStockLevel.Name = "lblMinimumStockLevel";
            this.lblMinimumStockLevel.Size = new System.Drawing.Size(86, 19);
            this.lblMinimumStockLevel.TabIndex = 19;
            this.lblMinimumStockLevel.Text = "Min. Stok";
            // 
            // ProductForm
            // 
            this.AcceptButton = this.btnSave;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(600, 550);
            this.Controls.Add(this.lblMinimumStockLevel);
            this.Controls.Add(this.lblStockQuantity);
            this.Controls.Add(this.lblSalePrice);
            this.Controls.Add(this.lblPurchasePrice);
            this.Controls.Add(this.lblBrand);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblBarcode);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtMinimumStockLevel);
            this.Controls.Add(this.txtStockQuantity);
            this.Controls.Add(this.txtSalePrice);
            this.Controls.Add(this.txtPurchasePrice);
            this.Controls.Add(this.cmbBrand);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.txtName);
            this.MaximizeBox = false;
            this.Name = "ProductForm";
            this.ShowInTaskbar = false;
            this.Sizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ürün";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        
        private MaterialTextBox txtName;
        private MaterialTextBox txtBarcode;
        private MaterialMultiLineTextBox txtDescription;
        private MaterialComboBox cmbCategory;
        private MaterialComboBox cmbBrand;
        private MaterialTextBox txtPurchasePrice;
        private MaterialTextBox txtSalePrice;
        private MaterialTextBox txtStockQuantity;
        private MaterialTextBox txtMinimumStockLevel;
        private MaterialButton btnSave;
        private MaterialButton btnCancel;
        private MaterialLabel lblName;
        private MaterialLabel lblBarcode;
        private MaterialLabel lblDescription;
        private MaterialLabel lblCategory;
        private MaterialLabel lblBrand;
        private MaterialLabel lblPurchasePrice;
        private MaterialLabel lblSalePrice;
        private MaterialLabel lblStockQuantity;
        private MaterialLabel lblMinimumStockLevel;
        
        // Kategori ve marka listelerini yükler
        public void LoadCategoriesAndBrands(ICategoryService categoryService, IBrandService brandService)
        {
            try
            {
                // Kategori listesini yükle
                var categories = categoryService.GetAllCategories();
                cmbCategory.DataSource = categories;
                cmbCategory.DisplayMember = "Name";
                cmbCategory.ValueMember = "Id";
                
                // Marka listesini yükle
                var brands = brandService.GetAllBrands();
                cmbBrand.DataSource = brands;
                cmbBrand.DisplayMember = "Name";
                cmbBrand.ValueMember = "Id";
                
                // Düzenleme modunda ise, seçili kategori ve markayı ayarla
                if (_isEditMode)
                {
                    cmbCategory.SelectedValue = _product.CategoryId;
                    cmbBrand.SelectedValue = _product.BrandId;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kategori ve marka bilgileri yüklenirken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Ürün adı boş olamaz.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult = DialogResult.None;
                    return;
                }
                
                if (string.IsNullOrWhiteSpace(txtBarcode.Text))
                {
                    MessageBox.Show("Barkod boş olamaz.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult = DialogResult.None;
                    return;
                }
                
                if (cmbCategory.SelectedValue == null)
                {
                    MessageBox.Show("Lütfen bir kategori seçin.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult = DialogResult.None;
                    return;
                }
                
                if (cmbBrand.SelectedValue == null)
                {
                    MessageBox.Show("Lütfen bir marka seçin.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult = DialogResult.None;
                    return;
                }
                
                // Sayısal değerlerin doğruluğunu kontrol et
                if (!decimal.TryParse(txtPurchasePrice.Text, out decimal purchasePrice) || purchasePrice < 0)
                {
                    MessageBox.Show("Alış fiyatı geçerli bir sayı olmalıdır.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult = DialogResult.None;
                    return;
                }
                
                if (!decimal.TryParse(txtSalePrice.Text, out decimal salePrice) || salePrice < 0)
                {
                    MessageBox.Show("Satış fiyatı geçerli bir sayı olmalıdır.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult = DialogResult.None;
                    return;
                }
                
                if (!int.TryParse(txtStockQuantity.Text, out int stockQuantity) || stockQuantity < 0)
                {
                    MessageBox.Show("Stok miktarı geçerli bir sayı olmalıdır.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult = DialogResult.None;
                    return;
                }
                
                if (!int.TryParse(txtMinimumStockLevel.Text, out int minimumStockLevel) || minimumStockLevel < 0)
                {
                    MessageBox.Show("Minimum stok seviyesi geçerli bir sayı olmalıdır.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult = DialogResult.None;
                    return;
                }
                
                // Ürün nesnesini güncelle
                _product.Name = txtName.Text.Trim();
                _product.Barcode = txtBarcode.Text.Trim();
                _product.Description = txtDescription.Text.Trim();
                _product.CategoryId = Convert.ToInt32(cmbCategory.SelectedValue);
                _product.BrandId = Convert.ToInt32(cmbBrand.SelectedValue);
                _product.PurchasePrice = purchasePrice;
                _product.SalePrice = salePrice;
                _product.StockQuantity = stockQuantity;
                _product.MinimumStockLevel = minimumStockLevel;
                
                // Dialog sonucunu OK olarak ayarla
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ürün kaydedilirken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
            }
        }
        
        public Product GetProduct()
        {
            return _product;
        }
    }
} 