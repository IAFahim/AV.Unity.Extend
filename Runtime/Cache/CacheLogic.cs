using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AV.Unity.Extend.Runtime.Cache
{
    /// <summary>
    /// Core logic for component caching operations.
    /// Manages dictionary lookup, stale reference detection, and component resolution.
    /// </summary>
    public static class CacheLogic
    {
        /// <summary>
        /// Attempts to retrieve a component from cache with stale reference detection.
        /// Returns true if component exists and is alive, false otherwise.
        /// Automatically purges stale references from the cache.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetFromCache<TKey, TValue>(
            TKey key,
            Dictionary<TKey, TValue> cache,
            out TValue result)
            where TValue : Component
        {
            if (cache.TryGetValue(key, out result))
            {
                // Fast path: Component exists and is alive
                if (result != null) return true;

                // Stale reference detected - purge from cache
                cache.Remove(key);
            }

            result = null;
            return false;
        }

        /// <summary>
        /// Attempts to retrieve a component from the parent hierarchy.
        /// Returns true if found, false otherwise.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetComponentInParent<T>(Component source, out T result) where T : Component
        {
            result = source.GetComponentInParent<T>();
            return result != null;
        }

        /// <summary>
        /// Combined cache lookup with parent hierarchy fallback.
        /// Updates cache with newly found components.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetComponentInParentCached<TKey, TValue>(
            Component source,
            TKey key,
            Dictionary<TKey, TValue> cache,
            out TValue result)
            where TValue : Component
        {
            // 1. Try cache first
            if (TryGetFromCache(key, cache, out result))
            {
                return true;
            }

            // 2. Cache miss - try parent hierarchy
            if (!TryGetComponentInParent(source, out result))
            {
                return false;
            }

            // 3. Update cache with found component
            cache[key] = result;
            return true;
        }
    }
}
