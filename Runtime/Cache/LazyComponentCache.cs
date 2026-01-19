using System;
using System.Runtime.InteropServices;

namespace AV.Unity.Extend.Runtime.Cache
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct LazyCache<T>
    {
        public T Reference;
        public bool IsCached;
        public bool Exists;
    }
}