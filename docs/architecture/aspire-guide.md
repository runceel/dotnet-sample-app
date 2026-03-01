# .NET Aspire Integration Guide

## Overview

This application uses .NET Aspire for cloud-native orchestration, observability, and service discovery.

## Projects

### SampleApp.AppHost

The AppHost orchestrates all services in the application. It uses the `Aspire.AppHost.Sdk` to enable Aspire features.

```csharp
var builder = DistributedApplication.CreateBuilder(args);
builder.AddProject<Projects.SampleApp_Web_Server>("web-server");
builder.Build().Run();
```

### SampleApp.ServiceDefaults

Shared configuration applied to all services:

- **OpenTelemetry**: Distributed tracing and metrics
- **Health Checks**: `/health` and `/alive` endpoints
- **Service Discovery**: Automatic endpoint resolution
- **HTTP Resilience**: Standard retry and circuit breaker policies

## Running with Aspire

```bash
dotnet run --project src/AppHost/SampleApp.AppHost
```

This launches the Aspire dashboard at `https://localhost:15888` where you can:

- View all running services and their status
- Inspect distributed traces
- Monitor metrics and logs
- Check health check results

## Observability

The `AddServiceDefaults()` extension configures:

### OpenTelemetry
- **Traces**: ASP.NET Core + HttpClient instrumentation
- **Metrics**: ASP.NET Core + HttpClient + Runtime instrumentation
- **Logs**: Structured logging with OpenTelemetry

### OTLP Export

Set the `OTEL_EXPORTER_OTLP_ENDPOINT` environment variable to export telemetry to any OTLP-compatible backend (Jaeger, Grafana, etc.).

## Health Checks

| Endpoint | Description |
|----------|-------------|
| `/health` | All registered health checks |
| `/alive`  | Liveness probe (self check only) |

## Service Discovery

Services communicate using logical names resolved at runtime:

```csharp
builder.Services.ConfigureHttpClientDefaults(http =>
{
    http.AddStandardResilienceHandler();
    http.AddServiceDiscovery();
});
```
