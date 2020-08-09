using System;
using System.IO;
using System.Net;
using System.Text;
using LitJson;
namespace GetLrcsForSongs
{
    /// <summary>
    /// 用于查询酷狗歌词
    /// </summary>
    static class SearchKugouLyrics
    {
        /// <summary>
        /// 查询成功服务器的返回数据 否则为null
        /// </summary>
        public static KugouLyricResponseList Result
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
        private const string hostName = "http://lyrics.kugou.com";
        /// <summary>
        /// 资源地址和查询参数
        /// </summary>
        private const string searchLrcCmd = @"/search?ver=1&man={0}&client=pc&keyword={1}&duration={2}&hash={3}";
        /// <summary>
        /// 查询歌词
        /// </summary>
        /// <param name="singer">歌手名</param>
        /// <param name="song">歌名</param>
        /// <param name="duration">时长</param>
        /// <param name="hash">哈希值</param>
        /// <param name="isManual">是否是手动查询,默认为true</param>
        /// <returns>服务器回应数据对应的对象</returns>
        public static KugouLyricResponseList SearchLyrics(string singer, string song, TimeSpan duration, string hash, bool isManual)
        {
            string songSearchData = Uri.EscapeDataString(singer + " - " + song);//URI编码:用%X2%X2...的形式表示中文
            string cmd;
            if (isManual)
            {
                cmd = string.Format(searchLrcCmd, "yes", songSearchData, (duration.Ticks / 10000).ToString(), hash);
                return Result = SearchLyrics(cmd);//手动搜索则记录结果后返回
            }
            else
            {
                cmd = string.Format(searchLrcCmd, "no", songSearchData, (duration.Ticks / 10000).ToString(), hash);
                return SearchLyrics(cmd);//非手动搜索直接返回
            }
        }
        /// <summary>
        /// 查询歌词
        /// </summary>
        /// <param name="singer">歌手名</param>
        /// <param name="song">歌名</param>
        /// <param name="duration">时长</param>
        /// <param name="isManual">是否是手动查询,默认为true</param>
        /// <returns>服务器回应数据对应的对象</returns>
        public static KugouLyricResponseList SearchLyrics(string singer, string song, TimeSpan duration, bool isManual)
        {
            return SearchLyrics(singer, song, duration, "", isManual);
        }
        /// <summary>
        /// 查询歌词
        /// </summary>
        /// <param name="singer">歌手名</param>
        /// <param name="song">歌名</param>
        /// <param name="duration">时长</param>
        /// <param name="hash">哈希值</param>
        /// <returns>服务器回应数据对应的对象</returns>
        public static KugouLyricResponseList SearchLyrics(string singer, string song, TimeSpan duration, string hash)
        {
            return SearchLyrics(singer, song, duration, hash, true);
        }
        /// <summary>
        /// 查询歌词
        /// </summary>
        /// <param name="singer">歌手名</param>
        /// <param name="song">歌名</param>
        /// <param name="duration">时长</param>
        /// <returns>服务器回应数据对应的对象</returns>
        public static KugouLyricResponseList SearchLyrics(string singer, string song, TimeSpan duration)
        {
            return SearchLyrics(singer, song, duration, "", true);
        }
        /// <summary>
        /// 查询歌词
        /// </summary>
        /// <param name="singer">歌手名</param>
        /// <param name="song">歌名</param>
        /// <param name="durationMs">时长 单位:毫秒</param>
        /// <returns>服务器回应数据对应的对象</returns>
        public static KugouLyricResponseList SearchLyrics(string singer, string song, long durationMs)
        {
            return SearchLyrics(singer, song, new TimeSpan(durationMs * 10000));
        }
        /// <summary>
        /// 查询歌词
        /// </summary>
        /// <param name="command">查询命令</param>
        /// <returns>服务器回应数据对应的对象</returns>
        private static KugouLyricResponseList SearchLyrics(string command)
        {
            //http协议get请求
            HttpWebRequest request = WebRequest.Create(hostName + command) as HttpWebRequest;
            request.Method = "GET";
            request.ProtocolVersion = new Version(1, 1);
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;//接收返回数据
            string content = "";
            using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(response.CharacterSet)))
                content = sr.ReadToEnd();//返回json数据存入本地变量

            KugouLyricResponseList result = JsonMapper.ToObject<KugouLyricResponseList>(content);//转化为对应的的歌词列表对象并存入内存
            if (result.status == 404)//如果没有查到歌词
            {
                throw new Exception("没有符合条件的歌词");
            }
            else
            {
                return result;//返回结果
            }
        }
    }
}
