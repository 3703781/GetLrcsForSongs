using System;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Ape;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Reflection;

namespace GetLrcsForSongs
{
    /// <summary>
    /// GetLrcForm列表框显示状态
    /// </summary>
    public enum GetLrcFormState
    {
        /// <summary>
        /// 显示搜索的歌词
        /// </summary>
        LyricSearched,
        /// <summary>
        /// 显示搜索的歌曲
        /// </summary>
        SongSearched,
        /// <summary>
        /// 显示本地载入的歌词文件
        /// </summary>
        MusicFileLoaded,
        /// <summary>
        /// 下载完歌词
        /// </summary>
        LyricDownloaded,
        None
    }
    public partial class GetLrcForm : Form
    {
        public static GetLrcForm FormInstance;
        public GetLrcForm()
        {
            InitializeComponent();
        }

        private void textBoxes_TextChanged(object sender, EventArgs e)
        {
            if (singerTextBox.Text == "" && songTextBox.Text == "" && durationMmTextbox.Text == "" && durationSsTextbox.Text == "")
                searchButton.Enabled = false;
            else
                searchButton.Enabled = true;
        }
        /// <summary>
        /// 加载对应系统位数的native dll文件
        /// </summary>
        /// <summary>
        /// 拖拽文件到列表框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadBass()
        {
            string bassPath;
            switch (IntPtr.Size)
            {
                case 4:
                    bassPath = Path.GetFullPath(@".\x86");
                    break;
                case 8:
                    bassPath = Path.GetFullPath(@".\x64");
                    break;
                default:
                    bassPath = Path.GetFullPath(@".\x86");
                    break;
            }

            Bass.LoadMe(bassPath);
            Bass.BASS_PluginLoad(bassPath);
            BassApe.LoadMe(bassPath);
            if (!Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))
                throw new Exception(Bass.BASS_ErrorGetCode().ToString());
        }
        private GetLrcFormState listViewState = GetLrcFormState.None;
        /// <summary>
        /// 列表框显示状态
        /// </summary>
        public GetLrcFormState ListViewState
        {
            get
            {
                return listViewState;
            }

            set
            {
                listViewState = value;
                switch (value)
                {
                    case GetLrcFormState.LyricSearched:
                    case GetLrcFormState.SongSearched:
                    case GetLrcFormState.None:
                        downLoadButton.Visible = false;
                        tableLayoutPanel1.SetRowSpan(lyricListView, 2);
                        break;
                    case GetLrcFormState.LyricDownloaded:
                    case GetLrcFormState.MusicFileLoaded:
                        tableLayoutPanel1.SetRowSpan(lyricListView, 1);
                        downLoadButton.Visible = true;
                        downLoadButton.Enabled = true;
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// 搜索按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchButton_Click(object sender, EventArgs e)
        {
            searchButton.Enabled = false;//避免搜索完成前再次点击
            if (durationMmTextbox.Text == "" || durationSsTextbox.Text == "")//若没有提供时长数据
            {
                SearchAndDisplaySongs(2000, singerTextBox.Text, songTextBox.Text);//同步查询单曲并且显示
            }
            else
            {
                SearchAndDisplayLyrics(1000, singerTextBox.Text, songTextBox.Text, (durationMmTextbox.Value * 60 + durationSsTextbox.Value) * 1000);//同步查询歌词并且显示
            }
            searchButton.Enabled = true;//重新启用搜索按钮
        }
        /// <summary>
        /// 同步查询歌词
        /// </summary>
        /// <param name="timeout">查询超时时间 单位:毫秒</param>
        /// <param name="singer">歌手名</param>
        /// <param name="song">歌名</param>
        /// <param name="duration">歌曲时长</param>
        /// <returns>服务器回应数据对应的对象 发生异常返回null</returns>
        private KugouLyricResponseList SearchLyrics(int timeout, string singer, string song, TimeSpan duration)
        {
            CancellationTokenSource cts = new CancellationTokenSource();//用于外部干预Task对象
            try
            {
                Task<KugouLyricResponseList> t = Task.Run(() =>
                {
                    return SearchKugouLyrics.SearchLyrics(singer, song, duration, false);//获取歌词列表
                }, cts.Token);
                if (t.Wait(timeout))//超时前完成返回true
                {
                    return t.Result;
                }
                else//任务超时未完成
                {
                    cts.Cancel();//关闭任务
                    throw new Exception("网络连接失败");
                }
            }
            catch (Exception ex)
            {
                //SearchKugouLyrics.ClearStaticMembers();//清除状态
                if (ex is AggregateException)
                {
                    AggregateException ae = (AggregateException)ex;
                    MessageBox.Show(ae.InnerException.Message);//显示异常信息
                }
                else
                    MessageBox.Show(ex.Message);//显示异常信息
                return null;
            }
        }
        /// <summary>
        /// 自动同步查询单曲并且显示
        /// </summary>
        /// <param name="timeout">查询超时时间 单位:毫秒</param>
        /// <param name="singer">歌手名</param>
        /// <param name="song">歌名</param>
        private void SearchAndDisplaySongs(int timeout, string singer, string song)
        {
            CancellationTokenSource cts = new CancellationTokenSource();//用于外部干预Task对象
            try
            {
                Task<KugouSongResponseList> t = Task.Run(() =>
                {
                    return SearchKugouSongs.SearchSongs(singer, song);//获取歌曲列表
                }, cts.Token);

                if (t.Wait(timeout))//超时前完成返回true
                {
                    lyricListView.DisplaySongList(t.Result);//显示歌曲列表
                    ListViewState = GetLrcFormState.SongSearched;//标记为列表框正显示歌曲列表
                }
                else//任务超时未完成
                {
                    cts.Cancel();//关闭任务
                    throw new Exception("网络连接失败");
                }
            }
            catch (Exception ex)
            {
                //SearchKugouSongs.ClearStaticMembers();//清除状态
                if (ex is AggregateException)
                {
                    AggregateException ae = (AggregateException)ex;//显示异常信息
                    MessageBox.Show(ae.InnerException.Message);//显示异常信息
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        /// <summary>
        /// 同步查询歌词并且显示
        /// </summary>
        /// <param name="timeout">查询超时时间 单位:毫秒</param>
        /// <param name="singer">歌手名</param>
        /// <param name="song">歌名</param>
        /// <param name="durationMs">歌曲时长 单位:毫秒</param>
        private void SearchAndDisplayLyrics(int timeout, string singer, string song, int durationMs)
        {
            CancellationTokenSource cts = new CancellationTokenSource();//用于外部干预Task对象
            try
            {
                Task<KugouLyricResponseList> t = Task.Run(() =>
                {
                    return SearchKugouLyrics.SearchLyrics(singer, song, durationMs);//获取歌词列表
                }, cts.Token);
                if (t.Wait(timeout))//超时前完成返回true
                {
                    lyricListView.DisplayLyricList(t.Result);//显示歌词列表
                    ListViewState = GetLrcFormState.LyricSearched;//标记为列表框正显示歌词列表
                }
                else//任务超时未完成
                {
                    cts.Cancel();//关闭任务
                    throw new Exception("网络连接失败");
                }
            }
            catch (Exception ex)
            {
                //SearchKugouLyrics.ClearStaticMembers();//清除状态
                if (ex is AggregateException)
                {
                    AggregateException ae = (AggregateException)ex;
                    MessageBox.Show(ae.InnerException.Message);//显示异常信息
                }
                else
                    MessageBox.Show(ex.Message);//显示异常信息
            }
        }
        /// <summary>
        /// 列表框双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lyricListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string selectedId;
            ListViewItem selectedItem = lyricListView.SelectedItems[0];
            switch (ListViewState)//检查当前显示状态
            {
                case GetLrcFormState.LyricSearched://当前显示的是歌词列表时
                    selectedId = selectedItem.SubItems[4].Text;//获取选中项歌词id
                    KugouLyricCandidate lyricCandidate = SearchKugouLyrics.Result[selectedId];//获取搜索结果中对应的歌词对象
                    if (!Directory.Exists(@".\DownLoad"))//不存在则建立目录
                        Directory.CreateDirectory(@".\DownLoad");
                    if (DownloadLyric(2000, @".\Download\" + lyricCandidate.singer + " - " + lyricCandidate.song + ".lrc", lyricCandidate))
                        selectedItem.ForeColor = Color.Gray;//下载过的歌词项标记为为灰色的
                    break;
                case GetLrcFormState.SongSearched://当前显示的是歌曲列表时
                    selectedId = selectedItem.SubItems[5].Text;//获取选中项歌曲id
                    KugouSongCandidate songCandidate = SearchKugouSongs.Result[selectedId];//获取搜索结果中对应的歌曲对象
                    SearchAndDisplayLyrics(1000, songCandidate.SingerName, songCandidate.SongName, songCandidate.Duration * 1000);//查询并显示对应的歌词列表
                    break;
                case GetLrcFormState.LyricDownloaded://已经下载完歌曲
                case GetLrcFormState.MusicFileLoaded://显示的是本地歌曲文件
                    SongFileCandidate candidate = new SongFileCandidate(selectedItem.SubItems[6].Text);
                    SearchAndDisplaySongs(2000, candidate.SingerName, candidate.SongName);
                    break;
                case GetLrcFormState.None:
                    break;
                default:
                    break;
            }
            lyricListView.SelectedItems.Clear();//双击操作后取消选择
        }
        /// <summary>
        /// 同步下载歌词
        /// </summary>
        /// <param name="timeout">查询超时时间 单位:毫秒</param>
        /// <param name="fileName">文件存储路径</param>
        /// <param name="lyricCandidate">查询得到的歌词对应的对象</param>
        private bool DownloadLyric(int timeout, string fileName, KugouLyricCandidate lyricCandidate)
        {
            //DownloadKugouLyric.DownloadLyric(lyricCandidate, KugouLyricFormat.Lrc, string.Format(@"{0}\{1} - {2}.lrc", Environment.CurrentDirectory, lyricCandidate.singer, lyricCandidate.song));//从服务器下载歌词
            CancellationTokenSource cts = new CancellationTokenSource();//用于外部干预Task对象
            try
            {
                Task t = Task.Run(() =>
                {
                    DownloadKugouLyric.DownloadLyric(lyricCandidate, KugouLyricFormat.Lrc, fileName);//从服务器下载歌词
                }, cts.Token);
                if (!t.Wait(timeout))//任务超时未完成
                {
                    cts.Cancel();//关闭任务
                    throw new Exception(string.Format("正在下载{0}-{1},网络连接断开", lyricCandidate.singer, lyricCandidate.song));
                }
                else//没有超时
                    return true;
            }
            catch (Exception ex)
            {
                //SearchKugouLyrics.ClearStaticMembers();//清除状态
                if (ex is AggregateException)
                {
                    AggregateException ae = (AggregateException)ex;
                    MessageBox.Show(string.Format("正在下载{0}-{1},{2}", lyricCandidate.singer, lyricCandidate.song, ae.InnerException.Message));//显示异常信息
                }
                else
                    MessageBox.Show(string.Format("正在下载{0}-{1},{2}", lyricCandidate.singer, lyricCandidate.song, ex.Message));//显示异常信息
                return false;
            }
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetLrcForm_Load(object sender, EventArgs e)
        {
            FormInstance = this;//便于构建单例模式(虽然当前没有使用单例模式)
            ListViewState = GetLrcFormState.None;//初始化列表框状态
            //初始化音频组件(非托管代码 窗体关闭时释放资源 用于读取歌曲长度而不是播放)
            LoadBass();
            timer1.Interval = 60000;
            timer1.Enabled = true;
        }
        /// <summary>
        /// 窗体关闭时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetLrcForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //释放音频组件的资源
            BassApe.FreeMe();
            Bass.BASS_Stop();
            Bass.BASS_Free();
        }
        /// <summary>
        /// 拖拽文件到列表框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lyricListView_DragDrop(object sender, DragEventArgs e)
        {
            Array array = (Array)e.Data.GetData(DataFormats.FileDrop);//获取对应文件路径集合
            lyricListView.DisplayFileList(new SongFileList(array));//显示
            downLoadButton.Enabled = true;//允许下载
        }
        /// <summary>
        /// 拖拽文件到列表框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lyricListView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))//如果拖入文件
                e.Effect = DragDropEffects.Copy;//拖入时为复制效果
            else
                e.Effect = DragDropEffects.None;
        }
        /// <summary>
        /// 下载按钮click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>    
        private void downLoadButton_Click(object sender, EventArgs e)
        {
            downLoadButton.Enabled = false;//屏蔽下载按钮

            foreach (ListViewItem item in lyricListView.Items)
            {
                if (item.ForeColor != Color.Gray)//如果没被下载过则下载
                {
                    //try
                    //{
                    SongFileCandidate candidate = new SongFileCandidate(item.SubItems[6].Text);
                    KugouLyricResponseList krl = SearchLyrics(1000, candidate.SingerName, candidate.SongName, candidate.Duration);
                    if (krl != null && DownloadLyric(2000, Path.ChangeExtension(candidate.FileFullName, ".lrc"), krl[0]))//首先保证krl不为null
                        item.ForeColor = Color.Gray;//下载过的项标记为为灰色的
                    else
                        item.ForeColor = Color.DarkRed;//下载失败的项为深红的
                                                       //}
                                                       //catch (Exception ex)
                                                       //{
                                                       //MessageBox.Show("错误: " + ex.Message);//可能抛出不支持的文件后缀异常
                                                       //}
                }
            }
            ListViewState = GetLrcFormState.LyricDownloaded;
            downLoadButton.Enabled = true;
        }
        /// <summary>
        /// 每60s释放下资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            GC.Collect();//我爱gc gc爱我
        }
    }
}