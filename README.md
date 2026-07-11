# C# Mastery Journey

This repository documents my progress mastering modern C# (v12/14) and .NET (v8/10) through daily coding challenges.

## Focus Areas
- **Modern Syntax:** Records, Pattern Matching, Collection Expressions, Primary Constructors.
- **Performance:** `Span<T>`, `Memory<T>`, `ValueTask<T>`, Zero-Allocation parsing.
- **Architecture:** Domain-Driven Design (DDD), Result Pattern, Dependency Injection.
- **Domain:** Enterprise Asset Management (EAM) & Industrial IoT (Pumps, Telemetry, Sensors).

## Progress
- [x] Week 1: Modern C# Refresh (Records, Pattern Matching)
- [x] Week 2: Advanced Initialization & Null Safety
- [x] Week 3: Intermediate .NET (Async/Await, Generics, Streaming)
- [ ] Week 4: Intermediate .NET Deep Dive (Expression Trees & Advanced Memory)

## How to Run
Requires .NET 8.0 SDK or later.
```bash
cd W01/Day03/
dotnet run
```

## CLI Helper
Use the interactive script to create a new day project or run an existing one:
```bash
bash scripts/csharp-journey.sh
```

When creating a project, the script asks whether to place it in the last week or create a new week, then picks the next available `DayXX` folder automatically.