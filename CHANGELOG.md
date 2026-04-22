# Changelog

Bu dosya, `TcknVkn` reposundaki önemli değişiklikleri güncel durum odaklı olarak takip eder.

## [Unreleased] - 2026-04-24

### Added
- Tüm çekirdek sınıf dosyalarına standart file header yorumları eklendi.
- Tüm doğrulama metotları ve yardımcı metotlar için Türkçe XML docstring açıklamaları genişletildi.
- TCKN ve VKN için ek varyasyonları kapsayan yeni unit test senaryoları eklendi.

### Changed
- NuGet paket kimliği `TcknVkn.Core` yerine `TcknVkn` olarak güncellendi.
- `Tckn.Validate` ve `Vkn.Validate` metotları `string?` girdiyi doğrudan destekleyecek şekilde iyileştirildi.
- README içeriği kullanım, bağlantılar ve kurulum akışları açısından güncellendi.
- CI/CD akışı release branch ve tag tabanlı beta/stable yayın akışını kapsayacak şekilde yeniden düzenlendi.

### Repository Snapshot
- Çekirdek kütüphane: `TcknVkn/Tckn.cs`, `TcknVkn/Vkn.cs`, `TcknVkn/ValidationResult.cs`, `TcknVkn/NumberText.cs`
- Paketleme projesi: `TcknVkn/TcknVkn.csproj`
- Çözüm dosyaları: `TcknVkn.sln` ve `TcknVkn.slnx`
- Test projesi: `tests/TcknVkn.Tests/TcknVkn.Tests.csproj`
- CI iş akışı: `.github/workflows/ci.yml`
- Dokümantasyon: `README.md`, `CHANGELOG.md`, `LICENSE`
