using System;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BLL.Services;
using BLL.Services.Interfaces;
using DAL;
using DAL.Repositories;

namespace UI
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                // Bağımlılık enjeksiyonu için servis koleksiyonu oluşturulması
                var services = new ServiceCollection();
                ConfigureServices(services);

                // Servis sağlayıcının oluşturulması
                ServiceProvider = services.BuildServiceProvider();

                // Veritabanını oluştur
                using (var scope = ServiceProvider.CreateScope())
                {
                    try 
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                        dbContext.Database.EnsureCreated();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Veritabanı oluşturulurken hata oluştu: {ex.Message}", "Veritabanı Hatası", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // Ana formun açılması
                Application.Run(new LoginForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Uygulama başlatılırken beklenmeyen bir hata oluştu: {ex.Message}", "Kritik Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            // DbContext'in kaydedilmesi
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=StokTakipDb;Trusted_Connection=True;MultipleActiveResultSets=true;",
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                }));

            // Repository'lerin kaydedilmesi
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // Servislerin kaydedilmesi
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<ICustomerService, CustomerService>();

            // Form'ların kaydedilmesi
            services.AddTransient<LoginForm>();
            services.AddTransient<MainForm>();
        }
    }
}