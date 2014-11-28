using System;
using System.Linq;
using Gender_api.Models;

namespace Gender_api.DataAccessLayer.Library
{
    public static class KeyService
    {
        public static ViewKeyModel GetKeyInfo(int id)
        {
            using (var db = new GenderEnt())
            {
                return (from a in db.keys
                    where a.id == id
                    select new ViewKeyModel
                           {
                               Company = a.users.company,
                               Email = a.users.email,
                               Firstname = a.users.firstname,
                               Key = a.key,
                               Lastname = a.users.lastname,
                               RequestLimit = a.requests_allowed,
                               Requests = a.requests
                           }).SingleOrDefault();
            }
        }

        /// <summary>
        /// Verifies the key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static bool VerifyKey(string key)
        {
            using (var db = new GenderEnt())
            {
                return (from a in db.keys where a.key == key select a.id).Any();
            }
        }

        /// <summary>
        /// Creates the key.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="requests"></param>
        public static int CreateKey(int userId, int requests)
        {
            using (var db = new GenderEnt())
            {
                var newKey = new keys
                             {
                                 user_id = userId,
                                 key = Guid.NewGuid().ToString(),
                                 requests = 0,
                                 requests_allowed = requests
                             };

                db.keys.Add(newKey);

                db.SaveChanges();

                return newKey.id;
            }
        }

        /// <summary>
        /// Resets the requests.
        /// </summary>
        public static void ResetRequests()
        {
            using (var db = new GenderEnt())
            {
                db.Database.ExecuteSqlCommandAsync("UPDATE keys SET requests = 0;");
            }
        }

        /// <summary>
        /// Adds the request.
        /// </summary>
        /// <param name="key">The key.</param>
        public static void AddRequest(string key)
        {
            using (var db = new GenderEnt())
            {
                var getKey = (from a in db.keys where a.key == key select a).SingleOrDefault();

                if (getKey != null)
                {
                    getKey.requests = getKey.requests + 1;

                    db.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Requests the limit.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static bool RequestLimit(string key)
        {
            using (var db = new GenderEnt())
            {
                var getKey = (from a in db.keys
                    where a.key == key
                    select new
                           {
                               a.requests,
                               a.requests_allowed
                           }).SingleOrDefault();

                if (getKey != null)
                {
                    return getKey.requests < getKey.requests_allowed;
                }

                return true;
            }
        }
    }
}