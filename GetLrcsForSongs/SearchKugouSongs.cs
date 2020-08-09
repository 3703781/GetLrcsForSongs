using System;
using System.Net;
using System.Text;
using System.IO;
using LitJson;

namespace GetLrcsForSongs
{
    /// <summary>
    /// 用于查询酷狗单曲
    /// </summary>
    class SearchKugouSongs
    {
        /// <summary>
        /// 查询成功服务器的返回数据 否则为null
        /// </summary>
        public static KugouSongResponseList Result
        {
            get;
            set;
        }
        /// <summary>
        /// 清除这个类的数据
        /// </summary>
        public static void ClearStaticMembers()
        {
            Result = null;
        }
        /// <summary>
        /// 协议和主机名
        /// </summary>
        const string hostName = "http://songsearch.kugou.com";
        /// <summary>
        /// 资源地址和查询参数
        /// </summary>
        const string searchSongCmd = @"/song_search_v2?keyword={0}&page=1&pagesize=40&filter=0&bitrate=0&isfuzzy=0&inputtype=2&platform=PcFilter&clientver=8100&iscorrection=7";
        /// <summary>
        /// 查询歌曲
        /// </summary>
        /// <param name="singer">歌手名</param>
        /// <param name="song">歌曲名</param>
        /// <returns>服务器回应的数据对应的对象</returns>
        public static KugouSongResponseList SearchSongs(string singer, string song)
        {
            string songSearchData = Uri.EscapeDataString(singer + " - " + song);//URI编码:用%X2%X2...的形式表示中文
            string cmd = string.Format(searchSongCmd, songSearchData);
            return SearchSongs(cmd);
        }
        /// <summary>
        /// 查询歌曲
        /// </summary>
        /// <param name="command">查询命令</param>
        /// <returns>服务器回应的数据对应的对象</returns>
        public static KugouSongResponseList SearchSongs(string command)
        {
            //http协议get请求
            string uri = hostName + command;
            HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;
            request.Method = "GET";
            request.ProtocolVersion = new Version(1, 1);
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;//接收返回数据
            string content = "";
            using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(response.CharacterSet)))
                content = sr.ReadToEnd();//返回json数据存入本地变量

            #region 解析成KugouSongResponseList
            try
            {
                JsonData jsonData = JsonMapper.ToObject(content);

                //如果没有查询到歌曲直接用sonMapper.ToObject<KugouSongResponseList>方法会抛异常
                //因为没歌曲时KugouSongResponseList.lists与json中类型不匹配
                if (jsonData["data"]["lists"].IsArray)
                {
                    return Result = JsonMapper.ToObject<KugouSongResponseList>(content);//转化为对应的的歌曲列表对象并存入内存并且返回
                }
                else
                {
                    throw new Exception("没有符合条件的单曲");
                }
            }
            catch (JsonException)
            {
                throw new Exception("网络连接失败");//如果收到无法解析的http回应 则说明网络有问题
            }
            #endregion
        }
    }
}
