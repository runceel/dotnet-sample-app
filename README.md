# dotnet-sample-app

.NET/Blazor モジュラーモノリス・クリーンアーキテクチャ サンプルアプリケーション

## 概要

このリポジトリは、.NET と Blazor WebAssembly を使ったモジュラーモノリスのクリーンアーキテクチャ設計パターンのサンプルです。将来的なマイクロサービス化を見越した設計で、.NET Aspire を活用した観測可能性・サービスディスカバリを実装しています。

## 技術スタック

- **.NET 9** / **C#**
- **Blazor WebAssembly** (フロントエンド)
- **ASP.NET Core Minimal API** (バックエンド API)
- **Entity Framework Core** + **SQLite** (データ永続化)
- **MediatR** (CQRS パターン)
- **.NET Aspire** (オーケストレーション・観測可能性)
- **OpenTelemetry** (分散トレーシング・メトリクス)

## プロジェクト構成

```
src/
├── AppHost/
│   └── SampleApp.AppHost/              # .NET Aspire オーケストレーションホスト
├── ServiceDefaults/
│   └── SampleApp.ServiceDefaults/      # 共通サービス設定 (OpenTelemetry, ヘルスチェック)
├── Shared/
│   ├── SampleApp.Shared.Domain/        # 共有ドメインプリミティブ (Entity, AggregateRoot 等)
│   └── SampleApp.Shared.Infrastructure/ # 共有インフラ (DomainEventPublisher 等)
├── Modules/
│   └── Todo/                           # Todo モジュール (モジュラーモノリスの1モジュール例)
│       ├── SampleApp.Modules.Todo.Domain/         # ドメイン層
│       ├── SampleApp.Modules.Todo.Application/    # アプリケーション層 (CQRS)
│       ├── SampleApp.Modules.Todo.Infrastructure/ # インフラ層 (EF Core)
│       └── SampleApp.Modules.Todo.Api/            # API 層 (Minimal API)
└── Web/
    ├── SampleApp.Web.Client/           # Blazor WebAssembly クライアント
    └── SampleApp.Web.Server/           # ASP.NET Core ホスト
```

## アーキテクチャドキュメント

| ドキュメント | 内容 |
|---|---|
| [クリーンアーキテクチャガイド](docs/architecture/clean-architecture-guide.md) | 各レイヤーの責務と依存関係ルール |
| [Aspire 導入ガイド](docs/architecture/aspire-guide.md) | Aspire の設定・活用方法 |
| [マイクロサービス移行ガイド](docs/architecture/microservices-migration.md) | マイクロサービス化の手順と考慮点 |

## 実行方法

### Aspire を使って起動 (推奨)

```bash
dotnet run --project src/AppHost/SampleApp.AppHost
```

Aspire ダッシュボードが `https://localhost:15888` で起動し、サービスの状態・トレース・メトリクスを確認できます。

### Web サーバーを直接起動

```bash
dotnet run --project src/Web/SampleApp.Web.Server
```

### ビルド

```bash
dotnet build dotnet-sample-app.slnx
```

## クリーンアーキテクチャの概要

各モジュールは以下の4層構造に従います:

```
Presentation (Api / Web.Client)
      ↓
Application (CQRS / Use Cases)
      ↓
   Domain (Entities / Value Objects)
      ↑
Infrastructure (EF Core / Repositories)
```

依存関係は常に内側（ドメイン層）を向きます。