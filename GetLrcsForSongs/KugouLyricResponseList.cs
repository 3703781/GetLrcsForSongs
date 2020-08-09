namespace GetLrcsForSongs
{
    /// <summary>
    /// 对应查询歌词服务器的返回数据
    /// </summary>
    class KugouLyricResponseList
    {
        /// <summary>
        /// HTTP状态码
        /// 查询成功-"OK" 失败-"Not Found"
        /// </summary>
        public string info;
        /// <summary>
        /// HTTP状态码
        /// 查询成功-"200" 失败-"404"
        /// </summary>
        public int status;
        /// <summary>
        /// 建议的歌词的id
        /// </summary>
        public string proposal;
        /// <summary>
        /// 查询关键词 貌似服务器直接返回查询指令里的使用的keyword
        /// </summary>
        public string keyword;
        /// <summary>
        /// 歌词名单
        /// </summary>
        public KugouLyricCandidate[] candidates;
        /// <summary>
        /// 根据uid获取歌词名单中的指定项
        /// </summary>
        /// <param name="uid">歌词的uid</param>
        /// <returns>酷狗歌词对象</returns>
        public KugouLyricCandidate this[string uid]
        {
            get
            {
                foreach (KugouLyricCandidate candidate in candidates)
                {
                    if (candidate.uid == uid)
                        return candidate;
                }
                throw new System.Exception("没有符合要求的项");
            }
        }
        public KugouLyricCandidate this[int index]
        {
            get
            {
                return candidates[index];
            }
        }
    }
    /// <summary>
    /// 酷狗歌词对象
    /// </summary>
    class KugouLyricCandidate : IKugouCandidate
    {
        public string soundname;
        public string trctype;
        public string nickname;
        public string originame;
        /// <summary>
        /// 访问键 下载时需要提供给服务器的
        /// </summary>
        public string accesskey;

        public string origiuid;
        /// <summary>
        /// 网友评分
        /// </summary>
        public int score;
        public int hitlayer;
        /// <summary>
        /// 歌词时长 单位毫秒
        /// </summary>
        public int duration;
        public string sounduid;
        public string transname;
        /// <summary>
        /// 歌词的uid
        /// </summary>
        public string uid;
        public string transuid;
        /// <summary>
        /// 歌名
        /// </summary>
        public string song;
        /// <summary>
        /// 歌词的id
        /// </summary>
        public string id;
        /// <summary>
        /// 
        /// </summary>
        public int adjust;
        public string singer;

        /// <summary>
        /// 歌词语言:""，"国语","英语","法语","俄语","日语"等
        /// </summary>
        public string language;
    }
}
