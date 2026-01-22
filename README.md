# AV.Unity.Extend

Caching utilities and extension methods for Unity including LazyCache for efficient component resolution.

## Features

- **LazyCache<T>** - Lazy component resolution with automatic caching
- **Extension Methods** - Convenient utilities for common Unity operations
- **Performance Optimized** - Minimize GetComponent calls with smart caching
- **Generic Support** - Works with any component type

## Installation

Install via Unity Package Manager or add to `Packages/manifest.json`:

```json
{
  "dependencies": {
    "com.av.unity.extend": "1.0.0"
  }
}
```

## Usage

### LazyCache

```csharp
using AV.Unity.Extend.Runtime.Cache;

public class ComponentCache
{
    private LazyCache<Rigidbody> _rbCache;
    private LazyCache<Renderer> _rendererCache;

    public void CacheComponents(Transform transform)
    {
        // First call: caches the component
        _rbCache.TryLazyResolveComponent(transform, out var rb);

        // Subsequent calls: uses cached value
        _rbCache.TryLazyResolveComponent(transform, out var rb2);
    }
}
```

### Cache Properties

```csharp
public struct LazyCache<T>
{
    public bool IsCached { get; set; }
    public bool Exists { get; set; }
    public T Reference { get; set; }
}
```

## Benefits

- ✅ Reduces redundant GetComponent calls
- ✅ Automatic cache invalidation support
- ✅ Type-safe generic implementation
- ✅ Burst-compatible with proper constraints

## License

MIT License - see [LICENSE.md](LICENSE.md)

## Author

IAFahim - [iafahim.dev@gmail.com](mailto:iafahim.dev@gmail.com)
