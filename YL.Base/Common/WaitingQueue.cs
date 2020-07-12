using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace YL.Base.Common
{
    /// <summary>
    /// 默认排队叫号机制,基于内存,不能用于集群环境
    /// </summary>
    public static class WaitingQueue
    {
        private static readonly ConcurrentDictionary<string, AsyncLock> queues = new ConcurrentDictionary<string, AsyncLock>();

        /// <summary>
        /// 排队执行
        /// </summary>
        /// <param name="queueKey">队列唯一标识key</param>
        /// <returns></returns>
        public static Waiter Waiting(string queueKey)
        {
            var obj = queues.GetOrAdd(queueKey.Trim().ToUpper(), new AsyncLock());
            var releaser = obj.Lock();
            return new Waiter(releaser);
        }
    }

    public class Waiter : IDisposable
    {
        AsyncLock.Releaser asyncLock { get; }
        public Waiter(AsyncLock.Releaser asyncLock)
        {
            this.asyncLock = asyncLock;
        }

        public void Dispose()
        {
            asyncLock.Dispose();
        }
    }
}
