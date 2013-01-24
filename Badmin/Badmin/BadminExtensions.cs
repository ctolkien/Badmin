using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Badmin
{
    public static class BadminExtensions
    {
        public static DataConfiguration<T> Name<T>(this DataConfiguration<T> config, string name) 
        {
            config.Name = name;
            return config;
        }

        public static DataConfiguration<T> VisibleInMenu<T>(this DataConfiguration<T> config, bool visibleInMenu) 
        {
            config.VisibleInMenu = visibleInMenu;
            return config;
        }
    }

    public static class PaginiationHelperExtensions
    {
        /// <summary>
        /// Renders a bootstrap standard pagination bar
        /// </summary>
        /// <remarks>
        /// http://twitter.github.com/bootstrap/components.html#pagination
        /// </remarks>
        /// <param name="helper">The html helper</param>
        /// <param name="currentPage">Zero-based page number of the page on which the pagination bar should be rendered</param>
        /// <param name="totalPages">The total number of pages</param>
        /// <param name="pageUrl">
        ///     Expression to construct page url (e.g.: x => Url.Action("Index", new {page = x}))
        /// </param>
        /// <param name="additionalPagerCssClass">Additional classes for the navigation div (e.g. "pagination-right pagination-mini")</param>
        /// <returns></returns>
        public static MvcHtmlString Pager(this HtmlHelper helper,
            int currentPage, int totalPages,
            Func<int, string> pageUrl,
            string additionalPagerCssClass = "")
        {
            if (totalPages <= 1)
                return MvcHtmlString.Empty;

            var div = new TagBuilder("div");
            div.AddCssClass("pagination");
            div.AddCssClass(additionalPagerCssClass);

            var ul = new TagBuilder("ul");

            for (var i = 1; i < totalPages + 1; i++)
            {
                var li = new TagBuilder("li");
                if (i == (currentPage + 1))
                    li.AddCssClass("active");

                var a = new TagBuilder("a");
                a.MergeAttribute("href", pageUrl(i));
                a.SetInnerText(i.ToString());

                li.InnerHtml = a.ToString();
                
                ul.InnerHtml += li;
            }

            div.InnerHtml = ul.ToString();

            return MvcHtmlString.Create(div.ToString());
        }
    }

    public static class PagingExtensions
    {
        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> query, int page, int pageSize)
        {
            return new PagedList<T>(query, page - 1, pageSize);
        }

        public static IEnumerable<T> GetPage<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
        {
            return source.Skip(pageIndex * pageSize).Take(pageSize);
        }
        // You can create your own paging extension that delegates to your
        // persistence layer such as NHibernate or Entity Framework.
        // This is an example how an `IPagedList<T>` can be created from 
        // an `IRavenQueryable<T>`:        
        /*
        public static IPagedList<T> ToPagedList<T>(this IRavenQueryable<T> query, int page, int pageSize)
        {
        RavenQueryStatistics stats;
        var q2 = query.Statistics(out stats)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToList();
        var list = new PagedList<T>(
        q2,
        page - 1,
        pageSize,
        stats.TotalResults
        );
        return list;
        }
        */
    }

    public static class DefaultScaffoldingExtensions
    {
        public static string GetControllerName(this Type controllerType)
        {
            return controllerType.Name.Replace("Controller", String.Empty);
        }

        public static string GetActionName(this LambdaExpression actionExpression)
        {
            return ((MethodCallExpression)actionExpression.Body).Method.Name;
        }

        public static PropertyInfo[] VisibleProperties(this IQueryable Model)
        {
            var elementType = Model.GetType().GetElementType();
            if (elementType == null)
            {
                elementType = Model.GetType().GetGenericArguments()[0];
            }
            return elementType.GetProperties().Where(info => info.Name != elementType.IdentifierPropertyName()).ToArray();
        }

        public static PropertyInfo[] VisibleProperties(this Object model)
        {
            return model.GetType().GetProperties().Where(info => info.Name != model.IdentifierPropertyName()).ToArray();
        }

        public static RouteValueDictionary GetIdValue(this object model)
        {
            var v = new RouteValueDictionary();
            v.Add(model.IdentifierPropertyName(), model.GetId());
            return v;
        }

        public static RouteValueDictionary GetIdAndTypeValue(this object model, string type)
        {
            var v = model.GetIdValue();
            v.Add(type, GetTypeName(model));
            return v;
        }

        public static string GetTypeName(this object model)
        {
            return model.GetType().Name.ToLower();
        }

        public static object GetId(this object model)
        {
            return model.GetType().GetProperty(model.IdentifierPropertyName()).GetValue(model, new object[0]);
        }

        public static string IdentifierPropertyName(this Object model)
        {
            return IdentifierPropertyName(model.GetType());
        }

        public static string IdentifierPropertyName(this Type type)
        {
            if (type.GetProperties().Any(info => info.PropertyType.AttributeExists<System.ComponentModel.DataAnnotations.KeyAttribute>()))
            {
                return
                type.GetProperties()
                    .First(
                    info => info.PropertyType.AttributeExists<System.ComponentModel.DataAnnotations.KeyAttribute>())
                    .Name;
            }
            else if (type.GetProperties().Any(p => p.Name.Equals("id", StringComparison.CurrentCultureIgnoreCase)))
            {
                return
                type.GetProperties().First(
                    p => p.Name.Equals("id", StringComparison.CurrentCultureIgnoreCase)).Name;
            }
            return "";
        }

        public static string GetLabel(this PropertyInfo propertyInfo)
        {
            
            try
            {
                var meta = ModelMetadataProviders.Current.GetMetadataForProperty(null, propertyInfo.DeclaringType, propertyInfo.Name);
                return meta.GetDisplayName();
            }
            catch (Exception)
            {
                return "unknown";
                throw;
            }
            
        }

        public static string ToSeparatedWords(this string value)
        {
            return Regex.Replace(value, "([A-Z][a-z])", " $1").Trim();
        }
    }

    public static class PropertyInfoExtensions
    {
        public static bool AttributeExists<T>(this PropertyInfo propertyInfo) where T : class
        {
            var attribute = propertyInfo.GetCustomAttributes(typeof(T), false)
                                        .FirstOrDefault() as T;
            if (attribute == null)
            {
                return false;
            }
            return true;
        }

        public static bool AttributeExists<T>(this Type type) where T : class
        {
            var attribute = type.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
            if (attribute == null)
            {
                return false;
            }
            return true;
        }

        public static T GetAttribute<T>(this Type type) where T : class
        {
            return type.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
        }

        public static T GetAttribute<T>(this PropertyInfo propertyInfo) where T : class
        {
            return propertyInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
        }

        public static string LabelFromType(Type @type)
        {
            var att = GetAttribute<DisplayNameAttribute>(@type);
            return att != null ? att.DisplayName
                   : @type.Name.ToSeparatedWords();
        }

        public static string GetLabel(this Object Model)
        {
            return LabelFromType(Model.GetType());
        }

        public static string GetLabel(this IEnumerable Model)
        {
            var elementType = Model.GetType().GetElementType();
            if (elementType == null)
            {
                elementType = Model.GetType().GetGenericArguments()[0];
            }
            return LabelFromType(elementType);
        }
    }

    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString TryPartial(this HtmlHelper helper, string viewName, object model)
        {
            try
            {
                return helper.Partial(viewName, model);
            }
            catch (Exception)
            {
            }
            return MvcHtmlString.Empty;
        }
    }
}
