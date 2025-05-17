# TaskManager - ASP.NET Core Web API

Bu proje, ÇözümTR firmasının staj değerlendirme süreci kapsamında geliştirilmiş bir **görev yöneticisi uygulamasıdır**. Kullanıcılar günlük, haftalık, aylık görevlerini sisteme kaydeder ve JWT Authentication ile güvenli şekilde erişir.

---

## Özellikler

- Katmanlı mimari 
- JWT Authentication & Authorization
- Swagger UI  
- MSSQL veritabanı desteği EF Core ile 

---

## Kullanılan Teknolojiler

- .NET 8.0 (ASP.NET Core Web API)
- Entity Framework Core
- Microsoft SQL Server (Local)
- Swagger / Swashbuckle
- JWT (JSON Web Token)
- LINQ & Async/Await

---

## Kurulum & Çalıştırma

### 1. Projeyi Klonla

```bash
git clone https://github.com/kullaniciadi/CozumTR-TaskManager.git
cd CozumTR-TaskManager/TaskManager
```

### 2. Veritabanı Ayarları

`appsettings.json` dosyasındaki bağlantı cümlesi yerel veritabanınıza göre güncellenmelidir:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=DESKTOP-XXXX;Database=TaskManagerDb;Trusted_Connection=True;Encrypt=false;TrustServerCertificate=true"
}
```

### 3. Migration Oluştur

```powershell
Add-Migration InitialCreate
Update-Database
```

> Bu adım, `TaskManagerDb` isimli veritabanında gerekli tabloları oluşturur.

### 4. Uygulamayı Başlat

```bash
dotnet run
```

---

## JWT Kullanımı

### 1. Kayıt Ol

```http
POST /api/Auth/register
{
  "username": "asrin",
  "password": "1234"
}
```

### 2. Giriş Yap (Token Al)

```http
POST /api/Auth/login
{
  "username": "asrin",
  "password": "1234"
}
```

### 3. Swagger'da "Authorize" Butonuna Tıkla

Gelen token'ı şu formatta gir:

```
Bearer eyJhbGciOiJIUzI1NiIsIn...
```

---

## API Endpointleri

| Yöntem | Adres | Açıklama |
|--------|-------|----------|
| POST   | `/api/Auth/register` | Kullanıcı kaydı |
| POST   | `/api/Auth/login`    | Giriş & JWT al |
| GET    | `/api/TaskItem`      | Tüm görevleri getir |
| GET    | `/api/TaskItem/{id}` | ID ile görev getir |
| POST   | `/api/TaskItem`      | Görev oluştur |
| PUT    | `/api/TaskItem/{id}` | Görev güncelle |
| DELETE | `/api/TaskItem/{id}` | Görev sil |

---

## Swagger Kullanımı

Uygulama çalıştığında Swagger'a şu adresten erişebilirsiniz:

```
https://localhost:7111/swagger
```

---

## Mimari

```
TaskManager/
│
├── Controllers
├── DTOs
├── Models
├── Repositories
├── Services
├── Migrations
├── appsettings.json
└── Program.cs
```

---

## Notlar

- Swagger’daki "Authorized" yazısı herhangi bir değer girildiğinde gösterilse de Yalnızca doğru token 
  girildiğinde kullanıcı sistemdeki fonksiyonlara erişebilir. Aksi takdirde fonksiyonlar kullanılmaya çalışıldığında 401 Hatasıyla karşılaşacaktır.
- Sadece **geçerli JWT** ile endpoint erişimi sağlanabilir.
- EF Core Migration ile MSSQL içerisine `Users` ve `TaskItems` tabloları otomatik oluşturulur.