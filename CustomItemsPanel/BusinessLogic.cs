using FlickrNet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Caching;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;

namespace CustomItemsPanel
{
    class BusinessLogic
    {
        static readonly string flickrKey = ConfigurationManager.AppSettings["apiKey"].ToString();
       // private static  PreviousSearchData previousSearch = new PreviousSearchData();
        public static IEnumerable GetPhotos(string cacheKey)
        {
            try
            {
                ObjectCache cache = MemoryCache.Default;

                if (cache.Contains(cacheKey.ToLower()))
                    return (IEnumerable)cache.Get(cacheKey.ToLower());
                else
                {
                    int k = 10;
                    int n = k/0;
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
               // Console.WriteLine(ex);
               throw new Exception("Message", ex);
            }
        }
    }
}
