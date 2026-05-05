# Orc.Csv

Orc.Csv is a small library of extensions and helper services built on top of the [CsvHelper](http://joshclose.github.io/CsvHelper) library. It provides `CsvReaderService` and `CsvWriterService` together with supporting maps, converters, and models to simplify CSV reading and writing in .NET applications.

---

## Critical Rules (Read First)

These rules are **non-negotiable**. Violating them causes broken builds, crashes, or downstream breakage.

### 1. Never Edit Generated Files

Files matching `*.generated.cs` are auto-generated.

- **NEVER** manually edit these files

### 2. ABI / API Stability

This project maintains stable ABI / API. Breaking changes break downstream apps.

| Allowed | Never |
|---------|-------|
| Add new overloads | Modify existing signatures |
| Add new methods | Remove public APIs |
| Add new classes | Change return types |

### 3. Tests Are Mandatory

**Building alone is NOT sufficient.** Run tests before claiming completion (see [Commands](#commands)).

### 4. Branch Protection (COMPLIANCE REQUIRED)

**Direct commits to protected branches are a policy violation.**

| Repository | Protected Branches |
|------------|-------------------|
| Orc.Csv | `master` |
| Orc.Csv | `develop` |

**Required workflow:**

1. **Create a feature branch FIRST** — Use naming convention: `feature/issue-NNNN-description`
2. **Make all commits on the feature branch** — Never commit directly to protected branches
3. **Submit a Pull Request** — Changes must be reviewed by a human before merging

```bash
# CORRECT — Always create a feature branch first
git checkout -b feature/issue-1234-fix-description

# NEVER DO THIS — Policy violation
git checkout develop && git commit  # FORBIDDEN

# NEVER DO THIS — Policy violation
git checkout master && git commit  # FORBIDDEN
```

The repository has protected branches that must be respected.

---

## Commands

Single source of truth for all commands:

| Task | Command |
|------|---------|
| **Build** | `dotnet cake --target=build` |
| **Test** | `dotnet cake --target=test` |
| **Build and test** | `dotnet cake --target=buildandtest` |

---

## Architecture & Directories

### Solution Overview

```
src/Orc.Csv          => Main library (services, maps, converters, models)
src/Orc.Csv.Tests    => Unit and integration tests
```

### Directory Guide

| Directory / File | Editable? | Notes |
|------------------|-----------|-------|
| `*.generated.cs` | No | Leave as-is |
| `src/Orc.Csv/Services/` | Yes | `CsvReaderService`, `CsvWriterService`, `CsvServiceBase` |
| `src/Orc.Csv/Maps/` | Yes | CsvHelper class maps |
| `src/Orc.Csv/Converters/` | Yes | Type converters |
| `src/Orc.Csv/Models/` | Yes | Data models |
| `src/Orc.Csv.Tests/` | Yes | All test code |
| `deployment/` | No | Deployment / build scripts |

---

## Writing Code

### Anti-Patterns (Never Do This)

| Anti-Pattern | Why |
|-------------|-----|
| Modifying method signatures | ABI breaking |
| Manual edits to `*.generated.cs` | Overwritten on regenerate |
| Using default parameters in public APIs | ABI breaking |
| **Skipping failing tests** | **Unacceptable — tests must pass** |

---

## Testing & Debugging

### Running Tests

```bash
dotnet cake --target=test
```

### Tests MUST Pass

> **NON-NEGOTIABLE:** Tests must PASS before claiming completion.
>
> - Do NOT skip failing tests
> - Do NOT claim completion if tests fail
> - Do NOT use `SkipException` to work around failures

### Writing Tests

1. Use NUnit to write tests
2. Create a `Facts` class for a feature (e.g., `CsvReaderServiceFacts`)
3. Use PascalCase for test methods, with `_Async` suffix for async tests (e.g., `ReadsAllRecords_Async`)

```csharp
[TestFixture]
public class CsvReaderServiceFacts
{
    [Test]
    public async Task ReadsAllRecords_Async()
    {
        // arrange
        // ...

        // act
        var records = await readerService.ReadRecordsAsync<MyModel>(filePath);

        // assert
        Assert.That(records, Has.Count.EqualTo(3));
    }
}
```

**Philosophy:** Tests FAIL when wrong, never skip (except missing hardware).

### Debugging Methodology

1. **Establish baseline** — What's the known-good state?
2. **One change at a time** — Verify each change before proceeding
3. **Track changes in a table** — Log what you changed and the result
4. **Platform differences are signals** — If X works and Y fails, the difference IS the answer
5. **Revert if worse** — Don't pile fixes on top of failures

---

## Further Reading

| Topic | Document |
|-------|---------|
| Contributing guidelines | `CONTRIBUTING.md` |
| CsvHelper documentation | https://joshclose.github.io/CsvHelper/ |
| WildGums open-source portal | http://opensource.wildgums.com |
