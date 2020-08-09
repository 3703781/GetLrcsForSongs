using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
namespace GetLrcsForSongs
{
    /// <summary>
    /// 包含工程中使用的扩展方法
    /// </summary>
    static class Extensions
    {
        /// <summary>
        /// listview显示歌词名单
        /// </summary>
        /// <param name="lv"></param>
        /// <param name="krcList">酷狗回应的包含歌词列表的数据</param>
        public static void DisplayLyricList(this ListView lv, KugouLyricResponseList krcList)
        {
            //挂起UI减少刷新资源开支
            lv.BeginUpdate();
            //初始化
            lv.FullRowSelect = true;
            lv.Clear();//清除内容和标题
            lv.ListViewItemSorter = null;//清除排序方法
            lv.View = View.Details;//列表样式
            //添加列
            lv.Columns.Add("", -1, HorizontalAlignment.Right);
            lv.Columns.Add("歌手", -1, HorizontalAlignment.Center);
            lv.Columns.Add("歌曲名", -1, HorizontalAlignment.Center);
            lv.Columns.Add("时长", -1, HorizontalAlignment.Center);
            lv.Columns.Add("ID", -1, HorizontalAlignment.Center);
            lv.Columns.Add("网友评分", -1, HorizontalAlignment.Center);

            //写入内容
            foreach (KugouLyricCandidate candidate in krcList.candidates)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.SubItems.Add(candidate.singer);
                lvi.SubItems.Add(candidate.song);
                lvi.SubItems.Add(new TimeSpan(candidate.duration * 10000).ToString(@"mm\:ss"));
                lvi.SubItems.Add(candidate.uid.ToString());
                lvi.SubItems.Add(candidate.score.ToString());
                lv.Items.Add(lvi);
            }

            //排序
            lv.ListViewItemSorter = new ListViewSorter(5);
            lv.Sort();

            //每行标序号
            int lineNumber = 0;
            foreach (ListViewItem item in lv.Items)
                item.Text = (++lineNumber).ToString();

            //自动列宽
            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            //更新UI
            lv.EndUpdate();
        }
        /// <summary>
        /// listview显示单曲名单
        /// </summary>
        /// <param name="lv"></param>
        /// <param name="songList">酷狗回应的包含歌曲列表的数据</param>
        public static void DisplaySongList(this ListView lv, KugouSongResponseList songList)
        {
            //挂起UI
            lv.BeginUpdate();
            //初始化
            lv.FullRowSelect = true;
            lv.Clear();//清除内容和标题
            lv.ListViewItemSorter = null;//清除排序方法
            lv.View = View.Details;//列表样式
            lv.Columns.Add("", -1, HorizontalAlignment.Right);
            lv.Columns.Add("歌曲名", -1, HorizontalAlignment.Center);
            lv.Columns.Add("专辑", -1, HorizontalAlignment.Center);
            lv.Columns.Add("时长", -1, HorizontalAlignment.Center);
            lv.Columns.Add("热度", -1, HorizontalAlignment.Center);
            lv.Columns.Add("ID", -1, HorizontalAlignment.Center);

            //写入内容
            foreach (KugouSongCandidate candidate in songList.data.lists)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.SubItems.Add(candidate.FileName);
                lvi.SubItems.Add(candidate.AlbumName != "" ? string.Format("《{0}》", candidate.AlbumName) : "");
                lvi.SubItems.Add(new TimeSpan(candidate.Duration * 10000000).ToString(@"mm\:ss"));
                lvi.SubItems.Add(candidate.AlbumPrivilege.ToString());
                lvi.SubItems.Add(candidate.ID);
                lv.Items.Add(lvi);
            }

            //标序号
            int lineNumber = 0;
            foreach (ListViewItem item in lv.Items)
                item.Text = (++lineNumber).ToString();
            //自动列宽
            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            //更新UI
            lv.EndUpdate();
        }
        /// <summary>
        /// listview排序类
        /// </summary>
        private class ListViewSorter : IComparer
        {
            int columnIndex;//指定列的索引
            /// <summary>
            /// 按照指定的列降序排序
            /// </summary>
            /// <param name="columnIndex">指定列的索引</param>
            public ListViewSorter(int columnIndex)
            {
                this.columnIndex = columnIndex;
            }
            /// <summary>
            /// 按照指定的列降序排序
            /// </summary>
            public int Compare(object x, object y)
            {
                int listViewX = Convert.ToInt32(((ListViewItem)x).SubItems[columnIndex].Text);
                int listViewY = Convert.ToInt32(((ListViewItem)y).SubItems[columnIndex].Text);
                return -listViewX.CompareTo(listViewY);
            }
        }
        /// <summary>
        /// 检查列表中是否存在指定音乐文件
        /// </summary>
        /// <param name="fileName">指定的文件</param>
        /// <returns></returns>
        public static bool Exists(this ListView lv, string fileName, int columnIndex)
        {
            foreach (ListViewItem item in lv.Items)//对于文件列表中的每一项
            {
                string tmp = item.SubItems[columnIndex].Text;//得到其文件的路径
                if (fileName.Equals(tmp, StringComparison.CurrentCultureIgnoreCase))//忽略大小写比较全路径 如果列表中已存在指定的文件
                    return true;//返回存在
            }
            return false;//否则返回不存在
        }
        /// <summary>
        /// 显示本地音乐文件列表
        /// </summary>
        /// <param name="lv"></param>
        /// <param name="songFileList">存储本地歌曲文件信息的类</param>
        public static void DisplayFileList(this ListView lv, SongFileList songFileList)
        {
            GetLrcFormState formState = GetLrcForm.FormInstance.ListViewState;
            if (formState != GetLrcFormState.MusicFileLoaded)
            {
                //初始化
                lv.FullRowSelect = true;
                lv.Clear();//清除内容和标题
                lv.ListViewItemSorter = null;//清除排序方法
                lv.View = View.Details;//列表样式
                lv.Columns.Add("", -1, HorizontalAlignment.Right);
                lv.Columns.Add("歌名", -1, HorizontalAlignment.Center);
                lv.Columns.Add("歌手", -1, HorizontalAlignment.Center);
                lv.Columns.Add("时长", -1, HorizontalAlignment.Center);
                lv.Columns.Add("文件名", -1, HorizontalAlignment.Center);
                lv.Columns.Add("后缀", -1, HorizontalAlignment.Center);
                lv.Columns.Add("路径", -1, HorizontalAlignment.Center);
            }
            lv.AddFileList(songFileList);

            GetLrcForm.FormInstance.ListViewState = GetLrcFormState.MusicFileLoaded;
        }
        /// <summary>
        /// 追加显示本地音乐文件
        /// </summary>
        /// <param name="lv"></param>
        /// <param name="songFileList">存储本地歌曲文件信息的类</param>
        private static void AddFileList(this ListView lv, SongFileList songFileList)
        {
            //挂起UI
            lv.BeginUpdate();
            foreach (SongFileCandidate candidate in songFileList)//文件集合中的每一项
            {
                if (SongFileCandidate.SupportedExtensions.Contains(candidate.Extension) && !lv.Exists(candidate.FileFullName, 6))//如果后缀已知且磁盘中的文件不存在于表中
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.SubItems.Add(candidate.SongName);
                    lvi.SubItems.Add(candidate.SingerName);
                    lvi.SubItems.Add(candidate.Duration.ToString("mm':'ss"));
                    lvi.SubItems.Add(candidate.FileNameWithoutExtension);
                    lvi.SubItems.Add(candidate.Extension.Substring(1).ToUpper());
                    lvi.SubItems.Add(candidate.FileFullName);
                    lv.Items.Add(lvi);
                }
            }
            //标序号
            int lineNumber = 0;
            foreach (ListViewItem item in lv.Items)
                item.Text = (++lineNumber).ToString();
            //自动列宽
            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            //更新UI
            lv.EndUpdate();
        }
        /// <summary>
        /// 检查数组中是否存在指定的元素
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="value">指定的元素</param>
        /// <returns></returns>
        private static bool Contains(this string[] array, string value)
        {
            foreach (string item in array)
            {
                if (item.Equals(value, StringComparison.CurrentCultureIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}
