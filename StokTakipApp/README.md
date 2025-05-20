# Stok Takip Uygulaması

Stok Takip Uygulaması, küçük ve orta ölçekli işletmeler için geliştirilmiş, Windows Forms tabanlı bir ürün, stok ve satış takip sistemidir.

## Özellikler

### Ürün Yönetimi
- Ürün Ekle / Güncelle / Sil
- Kategori ve marka bazında gruplama
- Benzersiz barkod sistemi
- Minimum stok seviyesi uyarıları

### Stok Hareketleri
- Stok giriş/çıkış işlemleri
- Kullanıcı, tarih ve saat bilgileri
- Ürün bazlı geçmiş görüntüleme

### Kategori ve Marka Yönetimi
- Kategori ve marka ekle/düzenle/sil
- Kategoriye göre ürün raporlama

### Kullanıcı Yönetimi
- Admin ve normal kullanıcı rolleri
- Güvenli parola saklama (SHA-256)
- Kullanıcı bazlı işlem takibi

### Raporlama
- Ürün listesi, stok hareketleri, satış geçmişi
- Excel/PDF dışa aktarma
- Tarih aralığına göre filtreleme

### Modern Arayüz
- Material Design tabanlı UI
- Açık/Koyu tema desteği
- Dinamik form düzeni

## Sistem Gereksinimleri

- .NET 6.0 Runtime
- SQL Server veya LocalDB
- Windows 10/11

## Kurulum

1. Projeyi indirin veya klonlayın
2. Visual Studio'da açın ve NuGet paketlerini geri yükleyin
3. `appsettings.json` dosyasında veritabanı bağlantı ayarlarını yapılandırın
4. Uygulamayı derleyin ve çalıştırın

## Mimari

Uygulama çok katmanlı bir mimaride geliştirilmiştir:

- **UI Katmanı**: Windows Forms kullanıcı arayüzü
- **BLL Katmanı**: İş mantığı ve servisler
- **DAL Katmanı**: Veri erişim ve repository pattern
- **Entities Katmanı**: Model sınıfları
- **Utilities Katmanı**: Yardımcı araçlar ve ortak işlevler

## Varsayılan Giriş Bilgileri

- Kullanıcı Adı: admin
- Şifre: admin

## Lisans

Bu proje MIT Lisansı altında lisanslanmıştır. 