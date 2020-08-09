# GetLrcsForSongs

## 简介
你是否还在为手动查找和下载歌词而烦恼？
有了`GetLrcForSongs`，现在可以批量匹配和精准搜索你想要的歌词。
而且纯绿色不需要安装。


## 使用
**系统非win10请安装.Net Framework4.5运行环境**

1. 输入歌手、歌曲、时长，按搜索键可以直接搜索歌词；双击在搜索到的歌词项将自动下载到程序目录里的`Download`文件夹下，同时列表项变为灰色提示下载成功
2. 如果仅提供歌手和歌曲，则搜索歌曲；双击歌曲项目进入对应的歌词列表
3. 可以从本地拖入歌曲文件到列表；按底部出现的的下载按钮自动下载匹配的歌词到歌曲同目录中，并且与歌曲同名；下载完成的歌词变为灰色，失败则红色
4. 双击导入的本地歌曲从服务器搜索对应歌曲

## 程序说明

工程命名空间为`GetLrcsForSongs`；引用`LitJson`、`Un4seen.Bass`、`Un4seen.Bass.AddOn.Ape`，分别用于解析`JSON`、解析歌曲时长、支持`APE`歌曲解析

### 类介绍

1. `KugouLyricResponseList`：在内存里存储服务器返回的歌词列表
2. `KugouLyricCandidate`：`KugouLyricResponseList`包含的单个歌词的信息
3. `SearchKugouLyrics`：提供查询歌词的方法，使用歌词服务器*lyrics.kugou.com*，*HTTP*协议；方法从获取的数据生成`KugouLyricResponseList`对象返回
4. `KugouSongResponseList`：在内存里存储服务器返回的歌曲列表
5. `KugouSongData`：`KugouSongResponseList`包含的歌曲列表信息
6. `KugouSongCandidate`：`KugouSongData`里的单个歌曲信息
7. `SearchKugouSongs`：提供查询单曲的方法，使用歌词服务器*songsearch.kugou.com*，*HTTP*协议；方法从获取的数据生成`KugouSongResponseList`对象返回
8. `SongFileList`：包含了本地歌曲文件信息列表
9. `SongFileCandidate`：`SongFileList`里的单个本地歌曲文件的信息
10. `Extensions`：包含工程中使用的扩展方法
11. `IKugouCandidate`：歌词对象或歌曲对象支持的接口，目前此接口没有定义方法
12. `DownloadKugouLyric`：提供下载指定歌词的方法，使用歌词服务器*lyrics.kugou.com*，*HTTP*协议
13. `GetLrcForm`：窗体类
14. `GetLrcFormState`：窗体显示状态枚举
15. `KugouLyricFormat`：酷狗服务器端支持的歌词格式枚举

### HTTP请求操作
#### 搜索歌词
使用歌手、歌名、时长信息向*http://lyrics.kugou.com*查询歌词列表
GET查询命令
```c#
"/search?ver=1&man={0}&client=pc&keyword={1}&duration={2}&hash={3}"
```

| 占位符        | 值 | 描述  |
| :--------:   | :---------: | -------- |
| {0}        | `"yes"`或`"no"` |指定是否为手动查询，<br>自动查询会得到唯一最佳匹配的歌词，<br>手动查询得到20项的可选歌词列表 |
| {1}        | `"歌手 - 歌词"`|   URI编码的搜索关键词    |
| {2}        | `int`      |   单位毫秒的歌词时长    |
| {3}        |       |歌曲`hash`值         |

返回`KugouLyricResponseList`对象

#### 下载歌词
使用`id`、`accesskey`、`fmt`信息向*http://lyrics.kugou.com*下载指定歌词
GET查询命令
```c#
"/download?ver=1&client=pc&id={0}&accesskey={1}&fmt={2}&charset=utf8"
```
| 占位符        | 值 | 描述  |
| :--------:   | :---------: | -------- |
| {0}        |   | 歌词的id，由查询时获取 |
| {1}        |   |   歌词的accesskey，由查询时获取    |
| {2}        | `KugouLyricFormat`      |   歌词的格式KRC或LRC    |

返回歌词`string`或`byte[]`

#### 查询单曲
使用歌手、歌名、专辑等关键词向*http://songsearch.kugou.com*查询歌词列表
GET查询命令
```c#
"/song_search_v2?keyword={0}&page=1&pagesize=40&filter=0&bitrate=0&isfuzzy=0&inputtype=2&platform=PcFilter&clientver=8100&iscorrection=7"
```
| 占位符        | 值 | 描述  |
| :--------:   | :---------: | -------- |
| {0}        |   | 查询关键字 |
返回歌词`KugouSongResponseList`

### 程序说明
- 所有从服务器获得的数据全部用JSON解析，其中下载的歌词数据JSON解析后还要进行Base64计算
- 窗体对象中使用task并设定超时时间调用查询下载等方法，并且捕捉分析异常显示给用户
- 从本地读取歌曲文件：
  将歌手、歌名、时长、路径、后缀等解析写入`SongFileCandidate`对象，`SongFileList`封装了`SongFileCandidate`对象数组

### 注意
1. 每次获得本地歌曲时长时会生成非托管的音频流，要注意用完释放流资源；窗体加载时初始化Bass类和加载ape组件；窗体关闭时释放它们的资源
2. 32和64位系统使用不同的`Bass`和`ape`组件，故相应dll文件在`x86`和`x64`目录里，窗体加载时会自动选择
3. `KugouLyricResponseList`、`KugouLyricCandidate`、`KugouSongResponseList`、`KugouSongData`、`KugouSongCandidate`里的注释为猜测，并不是酷狗官方解释

## 写在最后
1. 老代码本人不再修改，故开源欢迎pull request
2. 感谢[LitJSON](https://github.com/LitJSON/litjson)的作者（们）
3. 感谢[BASS](http://www.un4seen.com/)的作者（们）
4. 对本软件任何形式的复制、修改、使用、分发、编译须遵循协议要求
5. 感谢酷狗歌词服务，如侵权请告知
