﻿using Jieshai.Cache.CacheManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jieshai.Cache
{
    public class CacheContainer
    {
        public CacheContainer()
        {
            this.CacheManagers = new List<ICacheManager>();
        }

        public List<ICacheManager> CacheManagers { set; get; }

        public T CreateManager<T>(params object[] args) where T : ICacheManager
        {
            T manager = (T)Activator.CreateInstance(typeof(T), args);
            this.CacheManagers.Add(manager);

            return manager;
        }

        public ICacheManager CreateManagerByCacheType<CacheType>(params object[] args) where CacheType : class
        {
            ICacheManager manager = null;
            Type cacheManagerType = ReflectionHelper.GetSingleSubclass<CacheManager<CacheType>>(this.GetTypes());
            if (cacheManagerType != null)
            {
                manager = Activator.CreateInstance(cacheManagerType, args) as ICacheManager;
                this.CacheManagers.Add(manager);
            }

            return manager;
        }

        protected virtual Type[] GetTypes()
        {
            return this.GetType().Assembly.GetExportedTypes();
        }

        public virtual object Get(object key, Type type)
        {
            ICacheManager manager = this.GetManager(type);
            if (manager != null)
            {
                return manager.Get(key);
            }
            return null;
        }

        public bool Contains(Type type)
        {
            return this.CacheManagers.Any(l => l.IsCache(type));
        }

        public ICacheManager GetManager(Type type)
        {

            return this.CacheManagers.Find(l => l.IsCache(type));
        }

        public CacheManager<T> GetManager<T>() where T : class
        {
            return this.GetManager(typeof(T)) as CacheManager<T>;
        }
    }
}
