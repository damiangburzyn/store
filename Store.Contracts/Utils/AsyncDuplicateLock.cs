using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GC5.Application.Utils
{
    public sealed class AsyncDuplicateLock
    {
        private static readonly object ConstructorLock = new object();
        private static readonly Dictionary<object, RefCounted<SemaphoreSlim>> SemaphoreSlims = new Dictionary<object, RefCounted<SemaphoreSlim>>();
        private static long _lastKeyPrefix;

        private readonly long _keyPrefix;

        public AsyncDuplicateLock()
        {
            lock (ConstructorLock)
            {
                _keyPrefix = ++_lastKeyPrefix;
            }
        }

        public IDisposable Lock(long? numericKey = null)
        {
            var key = GetKey(numericKey);
            GetOrCreate(key).Wait();

            return new Releaser { Key = key };
        }

        public async Task<IDisposable> LockAsync(long? numericKey = null)
        {
            var key = GetKey(numericKey);
            await GetOrCreate(key).WaitAsync().ConfigureAwait(false);

            return new Releaser { Key = key };
        }

        private string GetKey(long? numericKey)
        {
            return string.Format($"{_keyPrefix}-{(numericKey.HasValue ? numericKey.Value.ToString() : "Core")}");
        }

        private SemaphoreSlim GetOrCreate(string key)
        {
            RefCounted<SemaphoreSlim> item;

            lock (SemaphoreSlims)
            {
                if (SemaphoreSlims.TryGetValue(key, out item))
                {
                    ++item.RefCount;
                }
                else
                {
                    item = new RefCounted<SemaphoreSlim>(new SemaphoreSlim(1, 1));
                    SemaphoreSlims[key] = item;
                }
            }

            return item.Value;
        }

        private sealed class RefCounted<T>
        {
            public RefCounted(T value)
            {
                RefCount = 1;
                Value = value;
            }

            public int RefCount { get; set; }

            public T Value { get; }
        }

        private sealed class Releaser : IDisposable
        {
            public string Key { private get; set; }

            public void Dispose()
            {
                RefCounted<SemaphoreSlim> item;

                lock (SemaphoreSlims)
                {
                    item = SemaphoreSlims[Key];
                    --item.RefCount;

                    if (item.RefCount == 0)
                    {
                        SemaphoreSlims.Remove(Key);
                    }
                }

                item.Value.Release();
            }
        }
    }
}