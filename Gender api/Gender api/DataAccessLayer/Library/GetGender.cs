using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gender_api.DataAccessLayer.Library
{
    public static class GetGender
    {
        /// <summary>
        /// Gets the single gender.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static object GetSingleGender(string name)
        {
            using (var db = new GenderEnt())
            {
                var getNames = (from a in db.names.AsNoTracking() where a.name == name select a).ToList();

                if (getNames.Any())
                {
                    if (getNames.Count == 1)
                    {
                        return new
                               {
                                   Name = name,
                                   Gender = DetermineGender(getNames[0].gender),
                                   Probability = 100,
                                   Count = getNames[0].number
                               };
                    }
                    if (getNames.Count == 2)
                    {
                        if (getNames[0].number > getNames[1].number)
                        {
                            var total = getNames[0].number + getNames[1].number;

                            return new
                                   {
                                       Name = name,
                                       Gender = DetermineGender(getNames[0].gender),
                                       Probability = (int) Math.Round((double) (100*getNames[0].number)/total),
                                       Count = getNames[0].number
                                   };
                        }
                        else
                        {
                            var total = getNames[0].number + getNames[1].number;

                            return new
                                   {
                                       Name = name,
                                       Gender = DetermineGender(getNames[1].gender),
                                       Probability = (int) Math.Round((double) (100*getNames[1].number)/total),
                                       Count = getNames[1].number
                                   };
                        }
                    }
                }

                return new
                       {
                           Name = name,
                           Gender = Genders.Unknown.ToString()
                       };
            }
        }

        /// <summary>
        /// Bulks the gender.
        /// </summary>
        /// <param name="genders">The genders.</param>
        /// <returns></returns>
        public static List<object> BulkGender(List<string> genders)
        {
            using (var db = new GenderEnt())
            {
                var list = new List<object>();

                foreach (var d in genders)
                {
                    var getNames = (from a in db.names.AsNoTracking() where a.name.Equals(d) select a).ToList();

                    if (getNames.Any())
                    {
                        if (getNames.Count == 1)
                        {
                            list.Add(new
                                     {
                                         Name = d,
                                         Gender = DetermineGender(getNames[0].gender),
                                         Probability = 100,
                                         Count = getNames[0].number
                                     });
                        }
                        if (getNames.Count == 2)
                        {
                            if (getNames[0].number > getNames[1].number)
                            {
                                var total = getNames[0].number + getNames[1].number;

                                list.Add(new
                                         {
                                             Name = d,
                                             Gender = DetermineGender(getNames[0].gender),
                                             Probability = (int) Math.Round((double) (100*getNames[0].number)/total),
                                             Count = getNames[0].number
                                         });
                            }
                            else
                            {
                                var total = getNames[0].number + getNames[1].number;

                                list.Add(new
                                         {
                                             Name = d,
                                             Gender = DetermineGender(getNames[1].gender),
                                             Probability = (int) Math.Round((double) (100*getNames[1].number)/total),
                                             Count = getNames[1].number
                                         });
                            }
                        }
                    }
                }

                return list;
            }
        }

        /// <summary>
        /// Determines the gender.
        /// </summary>
        /// <param name="gender">The gender.</param>
        /// <returns></returns>
        public static string DetermineGender(int gender)
        {
            switch (gender)
            {
                case 1:
                    return Genders.Female.ToString();
                case 2:
                    return Genders.Male.ToString();
                default:
                    return Genders.Unknown.ToString();
            }
        }

        public enum Genders
        {
            Female,
            Male,
            Unknown
        }
    }
}