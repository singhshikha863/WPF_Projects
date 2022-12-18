using FlickrNet;
using System;
using System.Collections;
using System.Configuration;
using System.Runtime.Caching;

namespace CustomItemsPanel
{
    /// <summary>
    /// This class contains the logic to call the Flicker api and process the result
    /// </summary>
    public class BusinessLogic
    {
           
        #region Private member

        private IErrorLogger logger;
        private readonly string flickrKey = ConfigurationManager.AppSettings["apiKey"].ToString();
        #endregion

        #region Constructor
        public BusinessLogic(IErrorLogger logger)
        {
            this.logger = logger;
        }

        #endregion
        #region Public Method
        /// <summary>
        /// This method will call the Flicker api and retrive the available photos
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public IEnumerable GetPhotos(string cacheKey)
        {
           
            try
            {
                ObjectCache cache = MemoryCache.Default;

                if (cache.Contains(cacheKey.ToLower()))
                    return (IEnumerable)cache.Get(cacheKey.ToLower());
                else
                {
                    PhotoSearchOptions options = new PhotoSearchOptions();
                    //options.PerPage = 16;
                    options.Page = 2;
                    options.SortOrder = PhotoSearchSortOrder.DatePostedDescending;
                    options.MediaType = MediaType.Photos;
                    options.Extras = PhotoSearchExtras.All;
                    options.Tags = cacheKey;

                    Flickr flickr = new Flickr(flickrKey);
                    PhotoCollection photos = flickr.PhotosSearch(options);
                    // Store data in the cache    
                    CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
                    cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddMinutes(30);
                    cache.Add(cacheKey.ToLower(), photos, cacheItemPolicy);
                    return photos;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
                return null;
            }
        }

        #endregion
    }
}
