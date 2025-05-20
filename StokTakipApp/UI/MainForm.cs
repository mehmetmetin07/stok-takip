using System;
using System.Windows.Forms;
using Entities;
using MaterialSkin;
using MaterialSkin.Controls;
using BLL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using UI.UserControls;

namespace UI
{
    public partial class MainForm : MaterialForm
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IStockService _stockService;
        private readonly IBrandService _brandService;
        
        public User CurrentUser { get; set; }
        
        private ProductsUserControl productsUserControl;
        private CategoriesUserControl categoriesUserControl;
        
        public MainForm()
        {
            InitializeComponent();
            
            // Material Design temasının ayarlanması
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
            
            try
            {
                // Servisler
                _productService = Program.ServiceProvider.GetRequiredService<IProductService>();
                _categoryService = Program.ServiceProvider.GetRequiredService<ICategoryService>();
                _stockService = Program.ServiceProvider.GetRequiredService<IStockService>();
                _brandService = Program.ServiceProvider.GetRequiredService<IBrandService>();
                
                // UserControl'lerin oluşturulması
                productsUserControl = new ProductsUserControl();
                categoriesUserControl = new CategoriesUserControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Servisler başlatılırken hata oluştu: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void InitializeComponent()
        {
            this.tabControl = new MaterialSkin.Controls.MaterialTabControl();
            this.tabDashboard = new System.Windows.Forms.TabPage();
            this.tabProducts = new System.Windows.Forms.TabPage();
            this.tabCategories = new System.Windows.Forms.TabPage();
            this.tabStock = new System.Windows.Forms.TabPage();
            this.tabReports = new System.Windows.Forms.TabPage();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.materialTabSelector = new MaterialSkin.Controls.MaterialTabSelector();
            this.lblWelcome = new MaterialSkin.Controls.MaterialLabel();
            this.btnLogout = new MaterialSkin.Controls.MaterialButton();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabDashboard);
            this.tabControl.Controls.Add(this.tabProducts);
            this.tabControl.Controls.Add(this.tabCategories);
            this.tabControl.Controls.Add(this.tabStock);
            this.tabControl.Controls.Add(this.tabReports);
            this.tabControl.Controls.Add(this.tabSettings);
            this.tabControl.Depth = 0;
            this.tabControl.Location = new System.Drawing.Point(6, 135);
            this.tabControl.MouseState = MaterialSkin.MouseState.HOVER;
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(988, 459);
            this.tabControl.TabIndex = 0;
            // 
            // tabDashboard
            // 
            this.tabDashboard.Location = new System.Drawing.Point(4, 24);
            this.tabDashboard.Name = "tabDashboard";
            this.tabDashboard.Padding = new System.Windows.Forms.Padding(3);
            this.tabDashboard.Size = new System.Drawing.Size(980, 431);
            this.tabDashboard.TabIndex = 0;
            this.tabDashboard.Text = "Gösterge Paneli";
            this.tabDashboard.UseVisualStyleBackColor = true;
            // 
            // tabProducts
            // 
            this.tabProducts.Location = new System.Drawing.Point(4, 24);
            this.tabProducts.Name = "tabProducts";
            this.tabProducts.Padding = new System.Windows.Forms.Padding(3);
            this.tabProducts.Size = new System.Drawing.Size(980, 431);
            this.tabProducts.TabIndex = 1;
            this.tabProducts.Text = "Ürünler";
            this.tabProducts.UseVisualStyleBackColor = true;
            // 
            // tabCategories
            // 
            this.tabCategories.Location = new System.Drawing.Point(4, 24);
            this.tabCategories.Name = "tabCategories";
            this.tabCategories.Size = new System.Drawing.Size(980, 431);
            this.tabCategories.TabIndex = 2;
            this.tabCategories.Text = "Kategoriler";
            this.tabCategories.UseVisualStyleBackColor = true;
            // 
            // tabStock
            // 
            this.tabStock.Location = new System.Drawing.Point(4, 24);
            this.tabStock.Name = "tabStock";
            this.tabStock.Size = new System.Drawing.Size(980, 431);
            this.tabStock.TabIndex = 3;
            this.tabStock.Text = "Stok Hareketleri";
            this.tabStock.UseVisualStyleBackColor = true;
            // 
            // tabReports
            // 
            this.tabReports.Location = new System.Drawing.Point(4, 24);
            this.tabReports.Name = "tabReports";
            this.tabReports.Size = new System.Drawing.Size(980, 431);
            this.tabReports.TabIndex = 4;
            this.tabReports.Text = "Raporlar";
            this.tabReports.UseVisualStyleBackColor = true;
            // 
            // tabSettings
            // 
            this.tabSettings.Location = new System.Drawing.Point(4, 24);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Size = new System.Drawing.Size(980, 431);
            this.tabSettings.TabIndex = 5;
            this.tabSettings.Text = "Ayarlar";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // materialTabSelector
            // 
            this.materialTabSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialTabSelector.BaseTabControl = this.tabControl;
            this.materialTabSelector.Depth = 0;
            this.materialTabSelector.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialTabSelector.Location = new System.Drawing.Point(6, 100);
            this.materialTabSelector.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector.Name = "materialTabSelector";
            this.materialTabSelector.Size = new System.Drawing.Size(988, 35);
            this.materialTabSelector.TabIndex = 1;
            this.materialTabSelector.Text = "materialTabSelector1";
            // 
            // lblWelcome
            // 
            this.lblWelcome.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Depth = 0;
            this.lblWelcome.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblWelcome.Location = new System.Drawing.Point(684, 70);
            this.lblWelcome.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(95, 19);
            this.lblWelcome.TabIndex = 2;
            this.lblWelcome.Text = "Hoş Geldiniz!";
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.AutoSize = false;
            this.btnLogout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnLogout.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnLogout.Depth = 0;
            this.btnLogout.HighEmphasis = true;
            this.btnLogout.Icon = null;
            this.btnLogout.Location = new System.Drawing.Point(880, 64);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnLogout.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnLogout.Size = new System.Drawing.Size(100, 30);
            this.btnLogout.TabIndex = 3;
            this.btnLogout.Text = "ÇIKIŞ";
            this.btnLogout.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnLogout.UseAccentColor = false;
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.materialTabSelector);
            this.Controls.Add(this.tabControl);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(3, 64, 3, 3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stok Takip Sistemi";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        
        private MaterialTabControl tabControl;
        private TabPage tabDashboard;
        private TabPage tabProducts;
        private TabPage tabCategories;
        private TabPage tabStock;
        private TabPage tabReports;
        private TabPage tabSettings;
        private MaterialTabSelector materialTabSelector;
        private MaterialLabel lblWelcome;
        private MaterialButton btnLogout;
        
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Kullanıcı bilgilerini güncelle
                if (CurrentUser != null)
                {
                    lblWelcome.Text = $"Hoş Geldiniz, {CurrentUser.FullName}";
                    
                    // Admin olmayan kullanıcılar için bazı sekmeleri gizle
                    if (CurrentUser.Role != UserRole.Admin)
                    {
                        tabControl.TabPages.Remove(tabSettings);
                    }
                }
                
                // UserControl'leri sekmelere ekle
                if (productsUserControl != null && tabProducts != null)
                {
                    tabProducts.Controls.Add(productsUserControl);
                    productsUserControl.Dock = DockStyle.Fill;
                }
                
                if (categoriesUserControl != null && tabCategories != null)
                {
                    tabCategories.Controls.Add(categoriesUserControl);
                    categoriesUserControl.Dock = DockStyle.Fill;
                }
                
                // İlk kullanımda verileri yükle
                LoadInitialData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Form yüklenirken hata oluştu: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void LoadInitialData()
        {
            try
            {
                // Servis kontrolü
                if (_productService == null)
                {
                    MessageBox.Show("Ürün servisi başlatılamadı.", "Hata", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // Düşük stok uyarılarını kontrol et
                var lowStockProducts = _productService.GetLowStockProducts();
                if (lowStockProducts != null && lowStockProducts.Count > 0 && CurrentUser != null && CurrentUser.Role == UserRole.Admin)
                {
                    ShowLowStockWarning(lowStockProducts.Count);
                }
                
                // Diğer verileri yükle
                // TODO: Ana ekranda gösterilecek verileri yükle
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veriler yüklenirken hata oluştu: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void ShowLowStockWarning(int productCount)
        {
            MessageBox.Show($"{productCount} ürünün stok seviyesi minimum değerin altında! " + 
                "Lütfen Stok Hareketleri sekmesinden kontrol ediniz.", 
                "Düşük Stok Uyarısı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        
        private void btnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Çıkış yapmak istediğinizden emin misiniz?", "Çıkış Onayı", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
            if (result == DialogResult.Yes)
            {
                // Login formuna geri dön
                var loginForm = Program.ServiceProvider.GetRequiredService<LoginForm>();
                this.Hide();
                loginForm.Show();
                loginForm.FormClosed += (s, args) => this.Close();
            }
        }
    }
} 