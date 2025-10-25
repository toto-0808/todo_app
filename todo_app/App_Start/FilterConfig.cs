using System.Web;
using System.Web.Mvc;

namespace todo_app
{
    /// <summary>
    /// フィルターの登録クラス
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// グローバルフィルターを登録します。
        /// </summary>
        /// <param name="filters">グローバルフィルターコレクション</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
