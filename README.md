
# 🚀 CryptoNewsApp – Kripto Haber Portalı

 - CryptoNewsApp, kullanıcıların güncel kripto para haberlerine ulaşmasını sağlayan, ASP.NET Core MVC ile geliştirilmiş bir web uygulamasıdır.

---

## 🎯 Temel Özellikler

- 📰 Anasayfada dış API'den alınan kripto haberleri ve yazarların kendi makaleleri
- 🧑‍💼 Admin paneli üzerinden kullanıcı ve kategori yönetimi (modal + ViewModel ile)
- 👤 Identity tabanlı kullanıcı kayıt/giriş sistemi
- 🛠️ Kullanıcı profil düzenleme ve şifre değiştirme
- 📰 Yazarlara özel içerik yönetimi imkanı (geliştirme aşamasında)



### 🛡️ Kullanıcı Yönetimi (Identity)

- 👤 Kayıt olma, giriş yapma, çıkış yapma
- 🔑 Şifre değiştirme
- 📄 Profil bilgilerini güncelleme (tek sayfada bölümlenmiş yapı)

### 🧑‍💼 Admin Paneli

- 👥 Kullanıcı listesi ve yönetimi (modal + ViewModel ile)
- 📂 Kategori işlemleri (listeleme, ekleme, güncelleme, silme)
- 📰 Haber yönetimi (yakında)
- 🛠️ Roller: Admin, Editor, Author, User

### 🔌 API Entegrasyonu

- 🌍 **NewsData.io API** ve **cryptopanic.com** üzerinden kripto haberleri çekme
- 🔄 Servis yapısı: SOLID prensiplerine uygun olarak dış API değişikliğine açık tasarım

---



### 🧠 Teknik Altyapı

- 🔄 Repository ve Unit of Work yapısı
- 🧩 ViewModel + Modal Formlar ile gelişmiş kullanıcı deneyimi
- 🛠️ FluentValidation + `ModelState` + `ValidatNever` çözümleri
- ⚠️ TempData ile kullanıcıya bilgi ve uyarı mesajları
- 🧱 Katmanlı mimari:
  - Core (Entities, Interfaces, ViewModels)
  - Infrastructure (EF Core, Repositories, Unit of Work)
  - Application (Servisler)
  - Web (MVC Arayüzü)

---

## 📦 Kullanılan Teknolojiler

- ASP.NET 6
- Entity Framework Core
- Microsoft Identity
- FluentValidation
- Bootstrap 5
- NewsData.io API
- cryptopanic.com API
- SOLID Prensipleri
- Modal + ViewModel ile form işlemleri
- SQL Server

---

## 🚧 Geliştirme Notları

- Haber API’si soyutlanmıştır, ileride farklı bir API’ye kolayca geçilebilir.
- Admin panelde işlemler modal + ViewModel ile yapılır.
- Tüm silme işlemleri `UnitOfWork` ile kontrol altındadır.
- `TempData` üzerinden Bootstrap alert ile kullanıcı geri bildirimi sağlanır.
- Kimlik doğrulama ve rol kontrolü yapılmaktadır.

---

## 🛠️ Geliştirme Durumu

Bu proje henüz tamamlanmamıştır. Bazı özellikler halen geliştirilmekte olup, ilerleyen sürümlerde aşağıdaki gibi genişletilmesi planlanmaktadır:

- Coin verilerinin anlık gösterimi
- İçerik paylaşımı yapan yazarlar için özel arayüz
- Editor paneli ve haber onay sistemi
- SEO uyumlu URL ve etiketleme sistemi

---

## ✉️ İletişim

Herhangi bir soru veya öneriniz varsa lütfen iletişime geçin:

**E-posta:** emreziyae@gmail.com
