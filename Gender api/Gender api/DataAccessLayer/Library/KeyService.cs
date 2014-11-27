using System;
using System.Linq;

namespace Gender_api.DataAccessLayer.Library
{
    public static class KeyService
    {
        public static bool VerifyKey(string key)
        {
            using (var db = new GenderEnt())
            {
                return (from a in db.keys where a.key == key select a.id).Any();
            }
        }

        public static void CreateKey(int userId)
        {
            using (var db = new GenderEnt())
            {
                db.keys.Add(new keys
                            {
                                user_id = userId,
                                key = Guid.NewGuid().ToString(),
                                requests = 0,
                                requests_allowed = 1000000
                            });

                db.SaveChanges();
            }
        }

        public static void ResetRequests()
        {
            using (var db = new GenderEnt())
            {
                db.Database.ExecuteSqlCommand("UPDATE keys SET requests = 0;");
            }
        }

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