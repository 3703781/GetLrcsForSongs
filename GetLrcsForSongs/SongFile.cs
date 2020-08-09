using System;
using System.Collections.Generic;
using System.Collections;
using Un4seen.Bass.AddOn.Ape;
using Un4seen.Bass;
using System.IO;
using System.Text.RegularExpressions;

namespace GetLrcsForSongs
{
    class SongFileList : IEnumerable
    {
        /// <summary>
        /// 本地歌曲列表
        /// </summary>
        public List<SongFileCandidate> SongFileCandidates;
        /// <summary>
        /// 从数组生成歌曲列表
        /// </summary>
        /// <param name="array">包含文件全路径的数组</param>
        public SongFileList(IList array)
        {
            SongFileCandidates = new List<SongFileCandidate>(array.Count);
            foreach (string item in array)//这里的item就是文件全路径
            {
                try//如果后缀不支持会抛异常 所以捕获以便继续添加支持的歌曲
                {
                    SongFileCandidates.Add(new SongFileCandidate(item));
                }
                catch { }
            }
        }
        /// <summary>
        /// 遍历SongFileCandidates列表
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return SongFileCandidates.GetEnumerator();
        }
    }
    class SongFileCandidate
    {
        /// <summary>
        /// 支持解析的后缀
        /// </summary>
        public static string[] SupportedExtensions = { ".MP3", ".MP2", ".MP1", ".OGG", ".WAV", ".AIFF", ".XM", ".IT", ".S3M", ".MOD", ".MTM", ".UMX", ".FLAC", ".APE" };
        /// <summary>
        /// 获取歌曲时长
        /// </summary>
        /// <param name="filePath">歌曲文件</param>
        /// <returns>歌曲时长</returns>
        /// <exception cref="Exception">后缀不支持</exception>
        private TimeSpan GetMusicDuration(string filePath)
        {

            int stream;
            //按后缀区分格式
            switch (Path.GetExtension(filePath).ToUpper())
            {
                case ".APE":
                    stream = BassApe.BASS_APE_StreamCreateFile(filePath, 0L, 0L, BASSFlag.BASS_DEFAULT);
                    break;
                case ".MP3":
                case ".MP2":
                case ".MP1":
                case ".OGG":
                case ".WAV":
                case ".AIFF":
                case ".XM":
                case ".IT":
                case ".S3M":
                case ".MOD":
                case ".MTM":
                case ".UMX":
                case ".FLAC":
                    stream = Bass.BASS_StreamCreateFile(filePath, 0L, 0L, BASSFlag.BASS_DEFAULT);
                    break;
                default:
                    throw new Exception("不支持" + filePath);
            }
            //返回歌曲时长
            TimeSpan result = new TimeSpan((long)(Bass.BASS_ChannelBytes2Seconds(stream, Bass.BASS_ChannelGetLength(stream)) * 10000000));
            Bass.BASS_StreamFree(stream);
            return result;
        }
        /// <summary>
        /// 歌曲名
        /// </summary>
        public string SongName { get; private set; }
        /// <summary>
        /// 歌手名
        /// </summary>
        public string SingerName { get; private set; }
        /// <summary>
        /// 文件全路径
        /// </summary>
        public string FileFullName { get; private set; }
        /// <summary>
        /// 无后缀文件名
        /// </summary>
        public string FileNameWithoutExtension { get; private set; }
        /// <summary>
        /// 后缀
        /// </summary>
        public string Extension { get; private set; }
        /// <summary>
        /// 歌曲时长
        /// </summary>
        public TimeSpan Duration { get; private set; }
        /// <summary>
        /// 文件后缀是否合法
        /// </summary>
        public bool IsSupported { get; private set; }
        /// <summary>
        /// 载入歌曲信息
        /// </summary>
        /// <param name="fileFullName">文件名全路径</param>
        public SongFileCandidate(string fileFullName)
        {
            FileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileFullName);
            FileFullName = fileFullName;
            Extension = Path.GetExtension(fileFullName);


            #region 检查后缀是否合法
            IsSupported = false;
            foreach (string extension in SupportedExtensions)
            {
                if (Extension.Equals(extension, StringComparison.CurrentCultureIgnoreCase))
                {
                    IsSupported = true;
                    break;
                }
            }
            if (!IsSupported)
                throw new Exception("后缀不支持:" + FileFullName);//不支持则抛异常 
            #endregion


            #region 从文件名获得歌手名和歌曲名
            //如文件名"金贵晟 - 虹之间": 歌手为"金贵晟";歌曲名为"虹之间"
            Regex reg = new Regex(@"^.+\s*-\s*.+$");
            if (reg.IsMatch(FileNameWithoutExtension))
            {
                string[] tmp = FileNameWithoutExtension.Split(new string[] { "-", " " }, StringSplitOptions.RemoveEmptyEntries);
                SingerName = tmp[0];
                SongName = tmp[1];
            }
            else//如果歌曲名不是"歌手名 - 歌曲名"的格式
            {
                SingerName = "";
                SongName = FileNameWithoutExtension;//把文件名作为歌名
            }
            #endregion

            //文件合法时才能读取时长
            Duration = GetMusicDuration(fileFullName);

        }
    }
}
