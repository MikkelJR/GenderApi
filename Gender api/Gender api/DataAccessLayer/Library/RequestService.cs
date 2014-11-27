﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gender_api.DataAccessLayer.Library
{
    public static class RequestService
    {
        public static void CreateRequest(string key, string name, string ip)
        {
            using (var db = new GenderEnt())
            {
                db.requests.Add(new requests
                                {
                                    created = DateTime.Now,
                                    ip = ip,
                                    key = key,
                                    name = name
                                });

                db.SaveChanges();
            }
        }
    }
}