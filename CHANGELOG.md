# Changelog

Bu dosya, `TcknVkn` reposundaki önemli değişiklikleri güncel durum odaklı olarak takip eder.

## [Yayımlanmamış]

### Added
- `tests/golden-dataset.json`: 19 dil kütüphanesinin ortak doğrulama sözleşmesi
  (kanonik kaynak: tcknvkn/spec). CI'da SHA-256 ile bütünlüğü doğrulanır.

## [1.0.8] - 2026-04-24

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

## [1.0.5] – [1.0.7] - 2026-04-24

NuGet'te yayımlandı. Depo geçmişi `v1.0.8` etiketiyle başladığı için bu ara
sürümlerin değişiklik ayrıntısı git geçmişinden türetilemiyor; uydurmak yerine
boş bırakıldı.

## [1.0.0] - 2026-04-24

İlk kararlı yayın.

---

**Sürüm notları.** NuGet'te yayımlanan sürümler: `0.0.1-beta.1`, `1.0.0`,
`1.0.5`, `1.0.6`, `1.0.7`, `1.0.8` — tamamı 2026-04-24. `1.0.1`–`1.0.4` hiç
yayımlanmadı. Paket sürümü CI tarafından etiketten türetilir
(`/p:PackageVersion=$VERSION`); csproj'daki `<Version>` yalnızca yerel
derlemeyi etkiler.
