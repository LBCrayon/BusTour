using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using NetTopologySuite.Algorithm;
using System.Reflection;
using System.Collections;

namespace DataAccess.Utilities
{
    public static class Utils
    {
        public static string GenerateRandomCode()
        {
            var randomCode = new Random();

            string chars = "0123456789zxcvbnmasdfghjklqwertyuiop";
            int length = 10;
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[randomCode.Next(s.Length)]).ToArray());
        }

        public static byte[] GetHash(string password, string fineSugar)
        {
            byte[] byteCode = Encoding.Unicode.GetBytes(string.Concat(password, fineSugar));

            SHA256Managed hashCode = new SHA256Managed();
            byte[] pass = hashCode.ComputeHash(byteCode);

            return pass;
        }

        public static bool CompareHash(string attemptedPassword, byte[] hash, string salt)
        {
            string base64Hash = Convert.ToBase64String(hash);
            string base64AttemptedHash = Convert.ToBase64String(GetHash(attemptedPassword, salt));

            var result = base64Hash == base64AttemptedHash;
            return result;
        }

        public static string RandomPassword()
        {
            Random pass = new Random();
            var chars = "1234567890qwertyuiopasdfghjklzxcvbnm";
            var length = 10;
            return new string(Enumerable.Repeat(chars, length)
                                        .Select(s => s[pass.Next(s.Length)]).ToArray());
        }

        public static string ToSnakeCase(this string o) => Regex.Replace(o, @"(\w)([A-Z])", "$1-$2").ToLower();

        public static DateTime GetCurrentDatetime()
        {
            return DateTime.UtcNow.AddHours(7);
        }

        public static bool CheckVNPhone(string phoneNumber)
        {
            string strRegex = @"(^(0)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$)";
            Regex re = new Regex(strRegex);
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            if (re.IsMatch(phoneNumber))
            {
                return true;
            }
            else
                return false;
        }

        //public static bool CheckTimeSlot(TimeSlot timeSlot)
        //{
        //    var currentTime = GetCurrentDatetime().TimeOfDay;
        //    var rangeCheck = timeSlot.ArriveTime.Hours - currentTime.Hours;

        //    var preOrderTime = new DateTime(GetCurrentDatetime().Year, GetCurrentDatetime().Month, GetCurrentDatetime().Day, 15, 00, 00);

        //    if (rangeCheck > 0 || (rangeCheck == 0
        //        && (timeSlot.ArriveTime.Minutes - currentTime.Minutes) >= 45) || currentTime.Hours >= preOrderTime.TimeOfDay.Hours)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //public static (DateTime, DateTime) GetLastAndFirstDateInCurrentMonth()
        //{
        //    var now = DateTime.Now;
        //    var first = new DateTime(now.Year, now.Month, 1);
        //    var last = first.AddMonths(1).AddDays(-1);
        //    return (first, last);
        //}

        //public static DateTime GetStartOfDate(this DateTime value)
        //{
        //    return new DateTime(value.Year, value.Month, value.Day, 0, 0, 0);
        //}

        //public static DateTime GetEndOfDate(this DateTime value)
        //{
        //    return new DateTime(value.Year, value.Month, value.Day, 23, 59, 59);
        //}

        //public static IQueryable<TEntity> DynamicFilter<TEntity>(this IQueryable<TEntity> source, TEntity entity)
        //{
        //    var properties = entity.GetType().GetProperties();
        //    foreach (var item in properties)
        //    {
        //        if (entity.GetType().GetProperty(item.Name) == null) continue;
        //        var propertyVal = entity.GetType().GetProperty(item.Name).GetValue(entity, null);
        //        if (propertyVal == null) continue;
        //        if (item.CustomAttributes.Any(a => a.AttributeType == typeof(SkipAttribute))) continue;
        //        bool isDateTime = item.PropertyType == typeof(DateTime);
        //        if (isDateTime)
        //        {
        //            DateTime dt = (DateTime)propertyVal;
        //            source = source.Where($"{item.Name} >= @0 && {item.Name} < @1", dt.Date, dt.Date.AddDays(1));
        //        }
        //        else if (item.CustomAttributes.Any(a => a.AttributeType == typeof(ContainAttribute)))
        //        {
        //            var array = (IList)propertyVal;
        //            source = source.Where($"{item.Name}.Any(a=> @0.Contains(a))", array);
        //            //source = source.Where($"{item.Name}.Intersect({array}).Any()",);
        //        }
        //        else if (item.CustomAttributes.Any(a => a.AttributeType == typeof(ChildAttribute)))
        //        {
        //            var childProperties = item.PropertyType.GetProperties();
        //            foreach (var childProperty in childProperties)
        //            {
        //                var childPropertyVal = propertyVal.GetType().GetProperty(childProperty.Name)
        //                    .GetValue(propertyVal, null);
        //                if (childPropertyVal != null && childProperty.PropertyType.Name.ToLower() == "string")
        //                    source = source.Where($"{item.Name}.{childProperty.Name} = \"{childPropertyVal}\"");
        //            }
        //        }
        //        else if (item.CustomAttributes.Any(a => a.AttributeType == typeof(ExcludeAttribute)))
        //        {
        //            var childProperties = item.PropertyType.GetProperties();
        //            var field = item.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(ExcludeAttribute))
        //                .NamedArguments.FirstOrDefault().TypedValue.Value;
        //            var array = ((List<int>)propertyVal).Cast<int?>();
        //            source = source.Where($"!@0.Contains(it.{field})", array);

        //        }
        //        else if (item.CustomAttributes.Any(a => a.AttributeType == typeof(SortAttribute)))
        //        {
        //            string[] sort = propertyVal.ToString().Split(", ");
        //            if (sort.Length == 2)
        //            {
        //                if (sort[1].Equals("asc"))
        //                {
        //                    source = source.OrderBy(sort[0]);
        //                }

        //                if (sort[1].Equals("desc"))
        //                {
        //                    source = source.OrderBy(sort[0] + " descending");
        //                }
        //            }
        //            else
        //            {
        //                source = source.OrderBy(sort[0]);
        //            }
        //        }
        //        else if (item.CustomAttributes.Any(a => a.AttributeType == typeof(StringAttribute)))
        //        {
        //            source = source.Where($"{item.Name}.ToLower().Contains(@0)", propertyVal.ToString().ToLower());
        //        }
        //        else if (item.PropertyType == typeof(string))
        //        {
        //            source = source.Where($"{item.Name} = \"{((string)propertyVal).Trim()}\"");
        //        }
        //        else
        //        {
        //            source = source.Where($"{item.Name} = {propertyVal}");
        //        }
        //    }
        //    return source;
        //}

        //public static IQueryable<TEntity> DynamicSort<TEntity>(this IQueryable<TEntity> source, TEntity entity)
        //{
        //    if (entity.GetType().GetProperties()
        //            .Any(x => x.CustomAttributes.Any(a => a.AttributeType == typeof(SortDirectionAttribute))) &&
        //        entity.GetType().GetProperties()
        //            .Any(x => x.CustomAttributes.Any(a => a.AttributeType == typeof(SortPropertyAttribute))))
        //    {
        //        var sortDirection = entity.GetType().GetProperties().SingleOrDefault(x =>
        //                x.CustomAttributes.Any(a => a.AttributeType == typeof(SortDirectionAttribute)))?
        //            .GetValue(entity, null);
        //        var sortBy = entity.GetType().GetProperties().SingleOrDefault(x =>
        //                x.CustomAttributes.Any(a => a.AttributeType == typeof(SortPropertyAttribute)))?
        //            .GetValue(entity, null);

        //        if (sortDirection != null && sortBy != null)
        //        {
        //            if ((string)sortDirection == "asc")
        //            {
        //                source = source.OrderBy((string)sortBy);
        //            }
        //            else if ((string)sortDirection == "desc")
        //            {
        //                source = source.OrderBy((string)sortBy + " descending");
        //            }
        //        }
        //    }

        //    return source;
        //}

        public static (int, IQueryable<TResult>) PagingQueryable<TResult>(this IQueryable<TResult> source, int page,
           int size, int limitPaging = 50, int defaultPaging = 1)
        {
            if (size > limitPaging)
            {
                size = limitPaging;
            }

            if (size < 1)
            {
                size = defaultPaging;
            }

            if (page < 1)
            {
                page = 1;
            }

            int total = source.Count();
            IQueryable<TResult> results = source.Skip((page - 1) * size).Take(size);
            return (total, results);
        }
    }
}

