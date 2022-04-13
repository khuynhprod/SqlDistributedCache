using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace SqlDistributedCache.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CacheController : ControllerBase
    {
        IDistributedCache _memoryCache;
        private readonly ILogger<CacheController> _logger;

        public CacheController(IDistributedCache memoryCache, ILogger<CacheController> logger)
        {
            _memoryCache = memoryCache;
            _logger = logger;
        }

        [Route("SetCacheData")]
        [HttpGet]
        public bool SetCacheData()
        {
            var Time = DateTime.Now.ToLocalTime().ToString();
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddYears(1)
            };
            _memoryCache.Set("Time", Encoding.UTF8.GetBytes(Time), cacheOptions);
            return true;
        }

        [Route("GetCacheData")]
        [HttpGet]
        public bool GetCacheData()
        {
            string Time = string.Empty;
            Time = Encoding.UTF8.GetString(_memoryCache.Get("Time"));
            return true;
        }

        [Route("RemoveCacheData")]
        [HttpGet]
        public bool RemoveCacheData()
        {
            _memoryCache.Remove("Time");
            return true;
        }
    }
}