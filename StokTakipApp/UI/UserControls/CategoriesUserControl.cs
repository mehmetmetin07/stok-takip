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
    public partial class CategoriesUserControl : UserControl
    {
        private readonly ICategoryService _categoryService;

        public CategoriesUserControl()
        {
            InitializeComponent();
            
            try
            {
                // Servisler
                _categoryService = Program.ServiceProvider.GetRequiredService<ICategoryService>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Servis başlatılırken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            this.dgvCategories = new System.Windows.Forms.DataGridView();
            this.btnAdd = new MaterialSkin.Controls.MaterialButton();
            this.btnEdit = new MaterialSkin.Controls.MaterialButton();
            this.btnDelete = new MaterialSkin.Controls.MaterialButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCategories
            // 
            this.dgvCategories.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCategories.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.dgvCategories.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCategories.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvCategories.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCategories.EnableHeadersVisualStyles = false;
            this.dgvCategories.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvCategories.Location = new System.Drawing.Point(20, 70);
            this.dgvCategories.Name = "dgvCategories";
            this.dgvCategories.ReadOnly = true;
            this.dgvCategories.RowHeadersVisible = false;
            this.dgvCategories.RowTemplate.Height = 28;
            this.dgvCategories.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCategories.Size = new System.Drawing.Size(760, 350);
            this.dgvCategories.TabIndex = 0;
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
            // CategoriesUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgvCategories);
            this.Name = "CategoriesUserControl";
            this.Size = new System.Drawing.Size(800, 430);
            this.Load += new System.EventHandler(this.CategoriesUserControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private DataGridView dgvCategories;
        private MaterialButton btnAdd;
        private MaterialButton btnEdit;
        private MaterialButton btnDelete;

        private void CategoriesUserControl_Load(object sender, EventArgs e)
        {
            LoadCategories();
            StyleDataGridView();
        }
        
        private void StyleDataGridView()
        {
            // DataGridView görünümünü özelleştir
            dgvCategories.BackgroundColor = Color.FromArgb(242, 242, 242); // Açık gri arka plan
            dgvCategories.GridColor = Color.FromArgb(224, 224, 224);       // Izgara çizgileri rengi
            
            // Sütun başlıkları
            dgvCategories.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50); // Koyu gri başlık
            dgvCategories.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCategories.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            dgvCategories.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCategories.ColumnHeadersHeight = 40;
            dgvCategories.ColumnHeadersDefaultCellStyle.Padding = new Padding(5);
            
            // Normal hücreler için stil
            dgvCategories.DefaultCellStyle.BackColor = Color.FromArgb(242, 242, 242); // Ana satır rengi
            dgvCategories.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvCategories.DefaultCellStyle.Padding = new Padding(5);
            dgvCategories.DefaultCellStyle.SelectionBackColor = Color.FromArgb(75, 75, 75); // Koyu gri seçim
            dgvCategories.DefaultCellStyle.SelectionForeColor = Color.White;
            
            // Alternatif satır rengi
            dgvCategories.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 230, 230); // Biraz daha koyu gri
            dgvCategories.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(75, 75, 75);
            dgvCategories.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;
            
            // Diğer ayarlar
            dgvCategories.RowTemplate.Height = 30;
            dgvCategories.RowHeadersVisible = false;
            dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCategories.BorderStyle = BorderStyle.None;
            dgvCategories.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        }

        private void LoadCategories()
        {
            try
            {
                // Servislerin doğru yüklendiğini kontrol et
                if (_categoryService == null)
                {
                    MessageBox.Show("Kategori servisi başlatılamadı.", "Hata", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kategorileri yükle
                var categories = _categoryService.GetAllCategories();
                
                // Kategorilerin null olmadığını kontrol et, ama boş olabilir
                if (categories == null)
                {
                    MessageBox.Show("Kategori verileri alınamadı.", "Hata", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // Kategori listesi boş olabilir, kullanıcıya bilgi verelim ama hata değil
                if (categories.Count == 0)
                {
                    MessageBox.Show("Henüz hiç kategori eklenmemiş.", "Bilgi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvCategories.DataSource = null;
                    return;
                }

                dgvCategories.DataSource = categories;

                // Sütun başlıklarını ve görünümü düzenle
                ConfigureDataGridColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kategoriler yüklenirken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void ConfigureDataGridColumns()
        {
            // Sütun başlıklarını ayarla
            if (dgvCategories.Columns.Contains("Id"))
                dgvCategories.Columns["Id"].HeaderText = "ID";
            
            if (dgvCategories.Columns.Contains("Name"))
                dgvCategories.Columns["Name"].HeaderText = "Kategori Adı";
            
            if (dgvCategories.Columns.Contains("Description"))
                dgvCategories.Columns["Description"].HeaderText = "Açıklama";

            // İlişkili verileri gizle
            if (dgvCategories.Columns.Contains("Products"))
                dgvCategories.Columns["Products"].Visible = false;
            
            // Ürün sayısı sütunu ekle
            if (!dgvCategories.Columns.Contains("ProductCount"))
            {
                var countColumn = new DataGridViewTextBoxColumn();
                countColumn.HeaderText = "Ürün Sayısı";
                countColumn.Name = "ProductCount";
                countColumn.DataPropertyName = "ProductCount";
                dgvCategories.Columns.Add(countColumn);

                // Ürün sayılarını güncelle
                foreach (DataGridViewRow row in dgvCategories.Rows)
                {
                    var category = row.DataBoundItem as Category;
                    if (category?.Products != null)
                    {
                        row.Cells["ProductCount"].Value = category.Products.Count;
                    }
                    else
                    {
                        row.Cells["ProductCount"].Value = 0;
                    }
                }
                
                dgvCategories.Columns["ProductCount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Kategori servisinin doğru yüklendiğini kontrol et
                if (_categoryService == null)
                {
                    MessageBox.Show("Kategori servisi başlatılamadı.", "Hata", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kategori ekleme formunu hazırla
                var addCategoryForm = new Forms.CategoryForm();
                
                // Formun başlığını ayarla
                addCategoryForm.Text = "Yeni Kategori Ekle";
                
                // Formu göster ve sonucu al
                if (addCategoryForm.ShowDialog() == DialogResult.OK)
                {
                    // Formdan kategori bilgilerini al
                    var newCategory = addCategoryForm.GetCategory();
                    
                    // Kategoriyi ekle
                    _categoryService.AddCategory(newCategory);
                    
                    // Kategori listesini yenile
                    LoadCategories();
                    
                    MessageBox.Show("Kategori başarıyla eklendi.", "Bilgi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kategori eklenirken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                // Kategori servisinin doğru yüklendiğini kontrol et
                if (_categoryService == null)
                {
                    MessageBox.Show("Kategori servisi başlatılamadı.", "Hata", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                if (dgvCategories.SelectedRows.Count > 0)
                {
                    var selectedCategory = dgvCategories.SelectedRows[0].DataBoundItem as Category;
                    if (selectedCategory == null)
                    {
                        MessageBox.Show("Seçili kategori bilgisi alınamadı.", "Hata", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                    // Düzenleme formunu oluştur
                    var editCategoryForm = new Forms.CategoryForm(selectedCategory);
                    
                    // Formun başlığını ayarla
                    editCategoryForm.Text = "Kategori Düzenle";
                    
                    // Formu göster ve sonucu al
                    if (editCategoryForm.ShowDialog() == DialogResult.OK)
                    {
                        // Formdan güncellenmiş kategori bilgilerini al
                        var updatedCategory = editCategoryForm.GetCategory();
                        
                        // Kategoriyi güncelle
                        _categoryService.UpdateCategory(updatedCategory);
                        
                        // Kategori listesini yenile
                        LoadCategories();
                        
                        MessageBox.Show("Kategori başarıyla güncellendi.", "Bilgi", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen düzenlemek için bir kategori seçin.", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kategori düzenlenirken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Kategori servisinin doğru yüklendiğini kontrol et
                if (_categoryService == null)
                {
                    MessageBox.Show("Kategori servisi başlatılamadı.", "Hata", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                if (dgvCategories.SelectedRows.Count > 0)
                {
                    var selectedCategory = dgvCategories.SelectedRows[0].DataBoundItem as Category;
                    if (selectedCategory == null)
                    {
                        MessageBox.Show("Seçili kategori bilgisi alınamadı.", "Hata", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                    // Kategoride ürün olup olmadığını kontrol et
                    bool hasProducts = _categoryService.HasProducts(selectedCategory.Id);
                    if (hasProducts)
                    {
                        MessageBox.Show("Bu kategoriye ait ürünler bulunmaktadır. Silmek için önce bu ürünleri başka bir kategoriye taşıyın veya silin.", 
                            "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    var result = MessageBox.Show($"{selectedCategory.Name} kategorisini silmek istediğinizden emin misiniz?", 
                        "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        // Kategoriyi sil
                        _categoryService.DeleteCategory(selectedCategory.Id);
                        
                        // Kategori listesini yenile
                        LoadCategories();
                        
                        MessageBox.Show("Kategori başarıyla silindi.", "Bilgi", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen silmek için bir kategori seçin.", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kategori silinirken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
} 