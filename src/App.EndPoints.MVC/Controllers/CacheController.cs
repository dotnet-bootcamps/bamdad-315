using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace App.EndPoints.MVC.Controllers
{
    [AllowAnonymous]
    public class CacheController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _distributedCache;

        public CacheController(IMemoryCache memoryCache,
            IDistributedCache distributedCache)
        {
            _memoryCache = memoryCache;
            _distributedCache = distributedCache;
        }


        public IActionResult Index()
        {
            var CacheKey = "CachedTime";
            CacheSampleModel model = new CacheSampleModel();

            model.CurrentDateTime = DateTime.Now;
            
            if (!_memoryCache.TryGetValue(CacheKey, out DateTime cacheValue))
            {
                cacheValue = model.CurrentDateTime;

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(3));

                _memoryCache.Set(CacheKey, cacheValue, cacheEntryOptions);
            }

            model.CacheCurrentDateTime = cacheValue;



            return View(model);
        }
    }

    public class CacheSampleModel
    {
        public DateTime CurrentDateTime { get; set; }
        public DateTime CacheCurrentDateTime { get; set; }
    }
}
