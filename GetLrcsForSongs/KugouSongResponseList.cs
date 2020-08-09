namespace GetLrcsForSongs
{
    public class KugouSongResponseList
    {
        public int status;//1
        public int error_code;//0
        public KugouSongData data;
        /// <summary>
        /// 通过id获取指定歌曲
        /// </summary>
        /// <param name="id">歌曲的id</param>
        /// <returns>包含单个歌曲的信息</returns>
        public KugouSongCandidate this[string id]
        {
            get
            {
                foreach (KugouSongCandidate candidate in data.lists)
                {
                    if (candidate.ID == id)
                        return candidate;
                }
                throw new System.Exception("没有符合要求的项");
            }
        }
    }

    public class KugouSongData
    {
        public int page;//显示页数，如1
        public string tab;//"全部"
        public KugouSongCandidate[] lists;
        public int chinesecount;//5
        public int searchfull;//1
        public int correctiontype;//10
        public int subjecttype;//0
        public KugouSongAggregation[] aggregation;//Array[5]
        public int allowerr;//0
        public string correctionsubject;//""
        public int correctionforce;//1
        public int total;//搜索到的歌曲总数，如40
        public int istagresult;//0
        public int istag;//0
        public string correctiontip;//"孙燕姿 遇见"
        public int pagesize;//每页显示歌曲个数，如40

    }
    public class KugouSongAggregation
    {
        public string key;//"现场"
        public int count;//
    }
    public class KugouSongCandidate : IKugouCandidate
    {
        public string SongName;//歌名，如"<em>遇见</em>"，其中<em></em>标签表示着重显示
        public int OwnerCount;//480410
        public int MvType;//歌曲MV的种类
        public string TopicRemark;//评论
        public int SQFailProcess;//4
        public string Source;//""
        public int Bitrate;//比特率
        public string HQExtName;//HQ音质文件后缀，如"mp3"
        public int SQFileSize;//SQ音质文件大小，单位为字节，如20365969
        public int ResFileSize;//0
        public int Duration;//歌曲时长，单位为秒
        public int MvTrac;//0
        public int SQDuration;//SQ音质歌曲时长，单位为秒
        public string ExtName;//歌曲文件后缀，如"mp3"
        public string Auxiliary;//辅助说明，如"《向左走 向右走》电影主题曲"
        public string SongLabel;//""
        public int Scid;//335039
        public int FailProcess;//4
        public int SQBitrate;//SQ音质比特率，如773
        public int HQBitrate;//HQ音质比特率，如320
        public int Audioid;//335039
        public int HiFiQuality;//2
        public int AlbumPrivilege;//8
        public string TopicUrl;//""
        public string SuperFileHash;//""
        public int ASQPrivilege;//10
        public int M4aSize;//M4A格式歌曲文件大小，单位为字节，如877414
        public string AlbumName;//专辑名，如"The Moment"
        public int Privilege;//8
        public int ResBitrate;//0
        public int HQFailProcess;//4
        public int SQPayType;//3
        public int HQPrice;//HQ音质下载价格，如200
        public string Type;//"audio"
        public int SourceID;//0
        public string FileName;//歌曲文件名，如"<em>孙燕姿</em> - <em>遇见</em>"，其中<em></em>标签表示着重显示
        public string ID;//歌曲ID，如"831c9987ba4ea6440ce7fb5a5509b609"
        public int SuperFileSize;//0
        public int QualityLevel;//3
        public string SQFileHash;//SQ音质文件哈希值，如"E4C04123623F66C41667C1D4F6940D97"
        public int HQPrivilege;//10
        public int SuperBitrate;//0
        public int SuperDuration;//0
        public string ResFileHash;//""
        public int PublishAge;//255
        public int A320Privilege;//10
        public string HQFileHash;//"01A44740CB944363B15EFD50EBE8A813"
        public string AlbumID;//专辑ID，如"964325"
        public string SuperExtName;//""
        public int HQPayType;//3
        public int PayType;//3
        public int mvTotal;//MV总数，如4
        public int PkgPrice;//1
        public int SQPkgPrice;//1
        public int HQFileSize;//HQ音质文件大小，单位为字节，如8483845
        public int FileSize;//文件大小，单位为字节，如3357035
        public int HQPkgPrice;//1
        public int SQPrice;//SQ音质下载价格，如200
        public int ResDuration;//0
        public int AudioCdn;//100
        public int Price;//200
        public int Publish;//1
        public string SingerName;//歌手名，如"<em>孙燕姿</em>"，其中<em></em>标签表示着重显示
        public string SQExtName;//SQ音质文件后缀，如"flac"
        public string MvHash;//歌曲MV哈希值，如"7D0B68DC456B65853D2BF70B7FF743BE"
        public int SQPrivilege;//10
        public int HQDuration;//HQ音质文件时长，单位为秒，如212
        public string OtherName;//""
        public int HasAlbum;//1
        public int Accompany;//1
        public string FileHash;//歌曲文件哈希值，如"AF8714B2944BA1A0D6745BBE931E8C22"
    }
}