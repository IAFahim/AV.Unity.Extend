# AV.Unity.Extend

![Header](documentation_header.svg)

[![Unity](https://img.shields.io/badge/Unity-2022.3%2B-000000.svg?style=flat-square&logo=unity)](https://unity.com)
[![License](https://img.shields.io/badge/License-MIT-blue.svg?style=flat-square)](LICENSE.md)

Caching utilities and performance extensions.

## ✨ Features

- **LazyCache<T>**: Struct that resolves and caches `GetComponent` calls. Handles nulls and missing components efficiently.
- **Extensions**: Optimizations for common Unity operations.

## 📦 Installation

Install via Unity Package Manager (git URL).

## 🚀 Usage

```csharp
using AV.Unity.Extend.Runtime.Cache;

public struct MySystem
{
    private LazyCache<Rigidbody> _rb;

    public void Update(Transform t)
    {
        // Resolves once, then uses cached reference
        if (_rb.TryLazyResolveComponent(t, out var rb))
        {
            rb.AddForce(Vector3.up);
        }
    }
}
```

## ⚠️ Status

- 🧪 **Tests**: Missing.
- 📘 **Samples**: None.
