using LitJson;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace GetLrcsForSongs
{
    /// <summary>
    /// 酷狗服务器端支持的歌词格式
    /// </summary>
    public enum KugouLyricFormat
    {
        /// <summary>
        /// 普通lrc歌词格式
        /// </summary>
        Lrc,
        /// <summary>
        /// 酷狗krc歌词格式
        /// </summary>
        Krc
    }
    /// <summary>
    /// 用于从服务器获取歌词
    /// </summary>
    class DownloadKugouLyric
    {
        private const string hostName = "http://lyrics.kugou.com";//协议和主机名
        const string downloadLyricCmd = @"/download?ver=1&client=pc&id={0}&accesskey={1}&fmt={2}&charset=utf8";//资源地址和下载参数
        /// <summary>
        /// 从服务器获取歌词
        /// </summary>
        /// <param name="uri">歌词的统一资源标识符</param>
        /// <returns>歌词的UTF-8字节码</returns>
        public static byte[] DownloadLyric(string uri)
        {
            HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;
            request.Method = "GET";
            request.ProtocolVersion = new Version(1, 1);
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            byte[] buffer;
            using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                buffer = Convert.FromBase64String(JsonMapper.ToObject(sr.ReadToEnd())["content"].ToString());
            return buffer;
        }
        /// <summary>
        /// 从服务器获取歌词
        /// </summary>
        /// <param name="id">歌词id</param>
        /// <param name="accessKey">id对应的访问键</param>
        /// <param name="format">歌词格式</param>
        /// <returns>歌词字符串</returns>
        public static string DownloadLyric(string id, string accessKey, KugouLyricFormat format)
        {
            string uri = string.Format(hostName + downloadLyricCmd, id, accessKey, format.ToString());
            return Encoding.UTF8.GetString(DownloadLyric(uri));
        }
        /// <summary>
        /// 从服务器获取歌词
        /// </summary>
        /// <param name="lyricCandidate">应答的歌词对象</param>
        /// <param name="format">歌词格式</param>
        /// <returns>歌词字符串</returns>
        public static string DownloadLyric(KugouLyricCandidate lyricCandidate, KugouLyricFormat format)
        {
            string uri = string.Format(hostName + downloadLyricCmd, lyricCandidate.id, lyricCandidate.accesskey, format.ToString().ToLower());
            return Encoding.UTF8.GetString(DownloadLyric(uri));
        }
        /// <summary>
        /// 从服务器下载歌词
        /// </summary>
        /// <param name="lyricCandidate">应答的歌词对象</param>
        /// <param name="format">歌词格式</param>
        /// <param name="fileName">下载存储文件的全路径</param>
        public static void DownloadLyric(KugouLyricCandidate lyricCandidate, KugouLyricFormat format, string fileName)
        {
            string uri = string.Format(hostName + downloadLyricCmd, lyricCandidate.id, lyricCandidate.accesskey, format.ToString().ToLower());
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                byte[] buffer = DownloadLyric(uri);
                fs.Write(buffer, 0, buffer.Length);
            }
        }
    }
}
