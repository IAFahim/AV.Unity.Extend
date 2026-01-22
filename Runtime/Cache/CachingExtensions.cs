using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AV.Unity.Extend.Runtime.Cache
{
    public static class CachingExtensions
    {
        /// <summary>
        ///     Safely retrieves a component from the cache, or fetches it from the parent hierarchy if missing.
        ///     <para>
        ///         <b>Performance Note:</b> The "Fast Path" (Cache Hit) is allocation-free.
        ///     </para>
        ///     <para>
        ///         <b>Lifecycle Safety:</b> Automatically detects and purges "Stale References" (Unity Objects
        ///         that have been Destroyed but remain in the Dictionary).
        ///     </para>
        /// </summary>
        /// <typeparam name="TKey">
        ///     The type of the cache key (typically <see cref="int" /> via <see cref="Object.GetInstanceID" />
        ///     ).
        /// </typeparam>
        /// <typeparam name="TValue">The Component type to retrieve.</typeparam>
        /// <param name="source">The component initiating the search.</param>
        /// <param name="key">The unique identifier for the lookup.</param>
        /// <param name="cache">The dictionary to read/write to.</param>
        /// <param name="result">The valid component if found and alive; otherwise null.</param>
        /// <returns>
        ///     <c>true</c> if the component exists and is actively managed by the Engine; otherwise <c>false</c>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetComponentInParentCached<TKey, TValue>(
            this Component source,
            TKey key,
            Dictionary<TKey, TValue> cache,
            out TValue result)
            where TValue : Component
        {
            if (cache.TryGetValue(key, out result))
            {
                if (result != null) return true;
                cache.Remove(key);
            }

            if (!source.TryGetComponentInParent(out result)) return false;
            cache[key] = result;
            return true;
        }

        /// <summary>
        ///     Attempts to retrieve a component in the parent hierarchy, adhering to the strict TryX pattern.
        /// </summary>
        /// <typeparam name="T">The Component type to retrieve.</typeparam>
        /// <param name="source">The component starting the search.</param>
        /// <param name="result">The component if found; otherwise null.</param>
        /// <returns><c>true</c> if found; otherwise <c>false</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetComponentInParent<T>(this Component source, out T result) where T : Component
        {
            result = source.GetComponentInParent<T>();
            return result != null;
        }
    }
}