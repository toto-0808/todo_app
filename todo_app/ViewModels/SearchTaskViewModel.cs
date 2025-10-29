using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using todo_app.Models;

namespace todo_app.ViewModels
{
    /// <summary>
    /// タスク検索用のViewModel
    /// </summary>
    public class SearchTaskViewModel
    {
        /// <summary>
        /// タスクカテゴリ
        /// </summary>
        public long? TaskCategoryId { get; set; } = null;

        /// <summary>
        /// 概要検索（部分一致）
        /// </summary>
        [DisplayName("概要検索")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 詳細検索（部分一致）
        /// </summary>
        [DisplayName("詳細検索")]
        public string Detail { get; set; } = string.Empty;

        /// <summary>
        /// 期日範囲（開始日）
        /// </summary>
        [DisplayName("期限日の範囲（開始日）")]
        public DateTimeOffset? DueDateFrom { get; set; } = null;

        /// <summary>
        /// 期日範囲（修了日）
        /// </summary>
        [DisplayName("期限日の範囲（修了日）")]
        public DateTimeOffset? DueDateTo { get; set; } = null;

        /// <summary>
        /// 着手済みか
        /// </summary>
        [DisplayName("着手ステータス")]
        public IsStartedSearchType IsStarted { get; set; } = IsStartedSearchType.未設定;

        /// <summary>
        /// 完了済みか
        /// </summary>
        /// Toso:選択肢を「未完了」「完了済み」「指定なし」としたい
        [DisplayName("完了ステータス")]
        public bool? IsCompleted { get; set; } = null;

        /// <summary>
        /// タスク一覧
        /// </summary>
        public List<Task> TaskList { get; set; } = new List<Task>();

        /// <summary>
        /// タスクカテゴリ一覧
        /// </summary>
        [DisplayName("カテゴリ")]
        public List<SelectListItem> TaskCategoryList { get; set; } = new List<SelectListItem>();
    }

    /// <summary>
    /// 着手ステータス検索区分
    /// </summary>
    public enum IsStartedSearchType : byte
    {
        未設定 = 0,
        未着手 = 1,
        着手済み = 2
    }
}