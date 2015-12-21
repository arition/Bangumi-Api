#Bangumi 基于netaba.re的API文档
### 约定
{}表示需替换内容
### 目录
[基本API](#基本API)   
[登录](#登录)   

[收视相关API](#收视相关API)   
[正在观看的动画列表](#正在观看的动画列表)   
[条目的用户自定义信息](#条目的用户自定义信息)   
[正在观看的动画的章节信息](#正在观看的动画的章节信息)    
[更新章节状态-看到某集](#更新章节状态-看到某集)   
[更新章节状态-看过某集](#更新章节状态-看过某集)   
[更新章节状态-撤销某集状态](#更新章节状态-撤销某集状态)   
[更新条目状态](#更新条目状态)   

[条目相关API](#条目相关API)     
[搜索条目](#搜索条目)    
[获得条目信息](#获得条目信息)    
[获得当季新番](#获得当季新番)   

[用户相关API](#用户相关API)   
[用户基本信息](#用户基本信息)   
[用户观看统计](#用户观看统计)   

### 基本API
##### 登录
POST http://netaba.re/api/login   

HTTP 请求头  
Key: `Authorization`   
Value: `Basic {basic64编码的{用户名}:{密码}}`  
关于HTTP Authorization头的参考[维基](https://zh.wikipedia.org/wiki/HTTP%E5%9F%BA%E6%9C%AC%E8%AE%A4%E8%AF%81)    

HTTP 请求正文   
无   

HTTP 响应头  
无特殊信息

HTTP 响应正文   
JSON格式     
示例
```
{
  "id": 1,
  "url": "http://bgm.tv/user/sampleuser",
  "username": "d1b0cd6471d2711dc1bb1b6536510a55",  //MD5哈希值，原字符串未知
  "nickname": "sampleuser",
  "avatar": {
    "large": "http://bgm.tv/sasadf",  //大头像url
    "medium": "http://bgm.tv/sasadf",  //中头像url
    "small": "http://bgm.tv/sasadf"  //小头像url
  },
  "sign": "",
  "auth": "sampleauthtokendfweffgwrgregregegserfd",  //验证Token
  "auth_encode": "encodesampleauthtokendfweffgwrgrewfwgregegfdfwefsekykjk"  //未知作用的Token
}
```
    
说明  
此为基本的登录Api，验证方式使用的是基本的http Basic验证。响应关键需要记录`username`和`auth`这两个key，这两个key是访问其他Api时必须要提供的验证信息    

###收视相关API
#####正在观看的动画列表
POST http://netaba.re/api/collection    
HTTP 请求头  
Key: `Authorization`   
Value: `Basic {base64({登录响应正文中的username}:{登录响应正文中的auth})}`  

HTTP 请求正文   
无   

HTTP 响应头  
无特殊信息

HTTP 响应正文   
JSON格式     
示例
```
[
  {
    "name": "sola",  //动画名
    "ep_status": 4,   //当前观看到第几集
    "lasttouch": 1436345501,   //unix时间戳
    "subject": {
      "id": 798,  //动画id
      "url": "http://bgm.tv/subject/798",
      "type": 2,  //1=漫画|小说 2=动画|二次元番 3=音乐 4=游戏 6=三次元番
      "name": "sola",
      "name_cn": "",
      "summary": "",
      "eps": 15,  //总集数
      "air_date": "2007-04-06",
      "air_weekday": 1,
      "images": {
        "large": "http://lain.bgm.tv/pic/cover/l/9e/3c/798_kGsvD.jpg",
        "common": "http://lain.bgm.tv/pic/cover/c/9e/3c/798_kGsvD.jpg",
        "medium": "http://lain.bgm.tv/pic/cover/m/9e/3c/798_kGsvD.jpg",
        "small": "http://lain.bgm.tv/pic/cover/s/9e/3c/798_kGsvD.jpg",
        "grid": "http://lain.bgm.tv/pic/cover/g/9e/3c/798_kGsvD.jpg"
      },
      "collection": {  //此部分应该为想看|看过|在看等的人数，但因不明原因只有doing（在看）可以显示人数
        "wish": 0,
        "collect": 0,
        "doing": 50,
        "on_hold": 0,
        "dropped": 0
      }
    }
  },
  {
    "name": "蟲師",
    "ep_status": 8,
    "lasttouch": 1435542555,
    "subject": {
      "id": 340,
      "url": "http://bgm.tv/subject/340",
      "type": 2,
      "name": "蟲師",
      "name_cn": "虫师",
      "summary": "",
      "eps": 26,
      "air_date": "2005-10-22",
      "air_weekday": 6,
      "images": {
        "large": "http://lain.bgm.tv/pic/cover/l/40/00/340_zgE5O.jpg",
        "common": "http://lain.bgm.tv/pic/cover/c/40/00/340_zgE5O.jpg",
        "medium": "http://lain.bgm.tv/pic/cover/m/40/00/340_zgE5O.jpg",
        "small": "http://lain.bgm.tv/pic/cover/s/40/00/340_zgE5O.jpg",
        "grid": "http://lain.bgm.tv/pic/cover/g/40/00/340_zgE5O.jpg"
      },
      "collection": {
        "wish": 0,
        "collect": 0,
        "doing": 581,
        "on_hold": 0,
        "dropped": 0
      }
    }
  },
]
```
说明    
正在观看的动画列表。好像并不能获得书籍等的正在观看信息。   

#####条目的用户自定义信息  
POST http://netaba.re/api/collection/subject/{条目id}   
HTTP 请求头  
Key: `Authorization`   
Value: `Basic {base64({登录响应正文中的username}:{登录响应正文中的auth})}`  

HTTP 请求正文   
无   

HTTP 响应头  
无特殊信息

HTTP 响应正文   
JSON格式     
示例
```
//http://netaba.re/api/collection/subject/768
{
  "status": {
    "id": 2,  //1=想看 2=看过 3=在看 4=搁置 5=抛弃
    "type": "collect",  //do=在看 on_hold=搁置 dropped=弃番 wish=想看 collect=看过
    "name": "看过"
  },
  "rating": 7,   //评分
  "comment": "OAOOOAOOAOA金发双马尾萝莉prpr",    //用户吐槽（评论）
  "tag": [   //用户添加的标签
    ""
  ],
  "ep_status": 15,  //看到第几集
  "lasttouch": 1436691097,  //unix时间戳
  "user": {  //用户信息，重复提供意义不明
    "id": 85184,
    "url": "http://bgm.tv/user/arition",
    "username": "arition",
    "nickname": "arition",
    "avatar": {
      "large": "http://lain.bgm.tv/pic/user/l/000/08/51/85184.jpg?r=1340538060",
      "medium": "http://lain.bgm.tv/pic/user/m/000/08/51/85184.jpg?r=1340538060",
      "small": "http://lain.bgm.tv/pic/user/s/000/08/51/85184.jpg?r=1340538060"
    },
    "sign": ""
  }
}
```

#####正在观看的动画的章节信息   
POST http://netaba.re/api/progress    
HTTP 请求头  
Key: `Authorization`   
Value: `Basic {base64({登录响应正文中的username}:{登录响应正文中的auth})}`  

HTTP 请求正文   
无   

HTTP 响应头  
无特殊信息

HTTP 响应正文   
JSON格式     
示例
```
[
  {
    "subject_id": 340,    //动画id（此处是虫师）
    "eps": [    //章节数组
      {
        "id": 333,   //章节id（第一集）
        "status": {
          "id": 2,   //1=想看 2=看过 3=在看 4=搁置 5=抛弃
          "css_name": "Watched",
          "url_name": "watched",
          "cn_name": "看过"
        }
      },
      {
        "id": 334,  //章节id（第二集）
        "status": {
          "id": 2,
          "css_name": "Watched",
          "url_name": "watched",
          "cn_name": "看过"
        }
      }
    ]   //这里章节数组结束，表明虫师看到第二集（其他集没有任何信息）
  },
  {
    "subject_id": 798,
    "eps": [
      {
        "id": 3170,
        "status": {
          "id": 2,
          "css_name": "Watched",
          "url_name": "watched",
          "cn_name": "看过"
        }
      },
      {
        "id": 3171,
        "status": {
          "id": 2,
          "css_name": "Watched",
          "url_name": "watched",
          "cn_name": "看过"
        }
      },
      {
        "id": 3172,
        "status": {
          "id": 2,
          "css_name": "Watched",
          "url_name": "watched",
          "cn_name": "看过"
        }
      }
    ]
  }
]
```

#####更新章节状态-看到某集   
POST http://netaba.re/api/subject/{条目id}/eps/batch_update    
HTTP 请求头  
Key: `Authorization`   
Value: `Basic {base64({登录响应正文中的username}:{登录响应正文中的auth})}`  

HTTP 请求正文   
JSON格式     
示例
```
{
  eps: {
    538086: 3  //id为538086的章节，为条目中的第3集
  }
}
```

HTTP 响应头  
无特殊信息

HTTP 响应正文   
JSON格式     
示例
```
{
  "request": "/subject/120763/update/watched_eps?source=intouch",
  "code": 202,
  "error": "Accepted"
}
```

#####更新章节状态-看过某集   
POST http://netaba.re/api/subject/{条目id}/eps/watched    
HTTP 请求头  
Key: `Authorization`   
Value: `Basic {base64({登录响应正文中的username}:{登录响应正文中的auth})}`  

HTTP 请求正文   
JSON格式     
示例
```
{
  eps: {
    538086: 3,  //id为538086的章节，为条目中的第3集
    538087: 4   //id为538087的章节，为条目中的第4集
  }
}
```

HTTP 响应头  
无特殊信息

HTTP 响应正文   
JSON格式     
示例
```
[
  {
    "request": "/ep/538086/status/watched?source=intouch",
    "code": 200,
    "error": "OK"
  },
  {
    "request": "/ep/538087/status/watched?source=intouch",
    "code": 200,
    "error": "OK"
  }
]
```
说明    
支持同时看过多集

#####更新章节状态-撤销某集状态   
POST http://netaba.re/api/subject/{条目id}/eps/remove    
HTTP 请求头  
Key: `Authorization`   
Value: `Basic {base64({登录响应正文中的username}:{登录响应正文中的auth})}`  

HTTP 请求正文   
JSON格式     
示例
```
{
  eps: {
    538086: 3  //id为538086的章节，为条目中的第3集
  }
}
```

HTTP 响应头  
无特殊信息

HTTP 响应正文   
JSON格式     
示例
```
[
  {
    "request": "/ep/538086/status/watched?source=intouch",
    "code": 200,
    "error": "OK"
  }
]
```
说明    
支持同时撤销多集

#####更新条目状态
POST http://netaba.re/api/subjects/update_status/{状态字符串}   // do=在看 on_hold=搁置 dropped=弃番 wish=想看 collect=看过   
HTTP 请求头  
Key: `Authorization`   
Value: `Basic {base64({登录响应正文中的username}:{登录响应正文中的auth})}`  

HTTP 请求正文   
JSON格式     
示例
```
{
  subjects: [
    77188     //条目id
  ], 
  comment: "",     //用户吐槽（评论）
  tags: "",    //添加标签
  rating: 0   //评分
}
```

HTTP 响应头  
无特殊信息

HTTP 响应正文   
JSON格式     
示例
```
[
  {
    "status": {
      "id": 3,  //1=想看 2=看过 3=在看 4=搁置 5=抛弃
      "type": "do",  // do=在看 on_hold=搁置 dropped=弃番 wish=想看 collect=看过   
      "name": null
    },
    "rating": 0,  //评分
    "comment": "",  //用户吐槽（评论）
    "tag": [   //用户添加的标签
      ""
    ],
    "ep_status": 0,   //看到第几集
    "lasttouch": 1437134447,  //unix时间戳
    "user": {   //用户信息，重复提供意义不明
      "id": 85184,
      "url": "http://bgm.tv/user/arition",
      "username": "arition",
      "nickname": "arition",
      "avatar": {
        "large": "http://lain.bgm.tv/pic/user/l/000/08/51/85184.jpg?r=1340538060",
        "medium": "http://lain.bgm.tv/pic/user/m/000/08/51/85184.jpg?r=1340538060",
        "small": "http://lain.bgm.tv/pic/user/s/000/08/51/85184.jpg?r=1340538060"
      },
      "sign": ""
    }
  }
]
```

### 条目相关API
#####搜索条目
GET http://netaba.re/api/search/{UrlEncode后的搜索关键字}   
HTTP 请求头  
无    

HTTP 请求正文   
JSON格式     
示例    
```
{
  "list": [
    {
      "id": 43951,  //动画id
      "url": "http://bgm.tv/subject/43951"
      "type": 2,  //1=漫画|小说 2=动画|二次元番 3=音乐 4=游戏 6=三次元番
      "name": "Fate/kaleid liner プリズマ☆イリヤ",
      "name_cn": "魔法少女☆伊莉雅",
      "summary": "",
      "eps": 0,    //疑似出问题了无效
      "air_date": "",    //疑似出问题了无效
      "air_weekday": 0,    //疑似出问题了无效
      "images": {
        "large": "http://lain.bgm.tv/pic/cover/l/1f/c3/43951_TJfCH.jpg",
        "common": "http://lain.bgm.tv/pic/cover/c/1f/c3/43951_TJfCH.jpg",
        "medium": "http://lain.bgm.tv/pic/cover/m/1f/c3/43951_TJfCH.jpg",
        "small": "http://lain.bgm.tv/pic/cover/s/1f/c3/43951_TJfCH.jpg",
        "grid": "http://lain.bgm.tv/pic/cover/g/1f/c3/43951_TJfCH.jpg"
      },
      "collection": {    //疑似出问题了无效
        "wish": 0,
        "collect": 0,
        "doing": 0,
        "on_hold": 0,
        "dropped": 0
      }
    },
    {
      "id": 83402,
      "url": "http://bgm.tv/subject/83402",
      "type": 2,
      "name": "Fate/kaleid liner プリズマ☆イリヤ ツヴァイ!",
      "name_cn": "魔法少女☆伊莉雅 第二季",
      "summary": "",
      "eps": 0,
      "air_date": "",
      "air_weekday": 0,
      "images": {
        "large": "http://lain.bgm.tv/pic/cover/l/f8/d7/83402_BBeb9.jpg",
        "common": "http://lain.bgm.tv/pic/cover/c/f8/d7/83402_BBeb9.jpg",
        "medium": "http://lain.bgm.tv/pic/cover/m/f8/d7/83402_BBeb9.jpg",
        "small": "http://lain.bgm.tv/pic/cover/s/f8/d7/83402_BBeb9.jpg",
        "grid": "http://lain.bgm.tv/pic/cover/g/f8/d7/83402_BBeb9.jpg"
      },
      "collection": {
        "wish": 0,
        "collect": 0,
        "doing": 0,
        "on_hold": 0,
        "dropped": 0
      }
    }
  ]
}
```
说明   
有些字段不会返回实际内容，可以通过[获得条目信息](#获得条目信息)来查询   

#####获得条目信息
POST http://netaba.re/api/subject/{条目id}   
HTTP 请求头  
无     

HTTP 请求正文   
JSON格式     
示例    
```
{
  "id": 113134,  //条目id
  "url": "http://bgm.tv/subject/113134",   //条目url
  "type": 2,  //1=漫画|小说 2=动画|二次元番 3=音乐 4=游戏 6=三次元番
  "name": "Fate/kaleid liner プリズマ☆イリヤ ツヴァイヘルツ!",
  "name_cn": "魔法少女☆伊莉雅 第二季续篇",
  "summary": "故事将进入暑假！迎来海! 泳衣! 生日!为主题的魔法少女们的欢乐日常！",
  "eps": [
    {
      "id": 524930,  //章节id
      "url": "http://bgm.tv/ep/524930",   //章节url
      "type": 0,  //只有0?
      "sort": 1,  //章节编号（第几集）
      "name": "",   //章节名称
      "name_cn": "",   //章节中文名
      "duration": "",   //时长
      "airdate": "2015-07-24",   //放送日期
      "comment": 0,   //评论数量
      "desc": "",    //章节简介
      "status": "NA"   //Air=已放送 NA=未放送 Today=正在放送   !注意由于日本动画特殊的时间计算（25：00这样的）Bangumi这个放送状态一向不准，请自行计算
    },
    {
      "id": 524931,
      "url": "http://bgm.tv/ep/524931",
      "type": 0,
      "sort": 2,
      "name": "",
      "name_cn": "",
      "duration": "",
      "airdate": "2015-07-31",
      "comment": 0,
      "desc": "",
      "status": "NA"
    },
    //后面的集数省略
  ],
  "air_date": "2015-07-24",  //放送日期
  "air_weekday": 5,  //星期几的番
  "images": {
    "large": "http://lain.bgm.tv/pic/cover/l/0f/31/113134_20Ey8.jpg",
    "common": "http://lain.bgm.tv/pic/cover/c/0f/31/113134_20Ey8.jpg",
    "medium": "http://lain.bgm.tv/pic/cover/m/0f/31/113134_20Ey8.jpg",
    "small": "http://lain.bgm.tv/pic/cover/s/0f/31/113134_20Ey8.jpg",
    "grid": "http://lain.bgm.tv/pic/cover/g/0f/31/113134_20Ey8.jpg"
  },
  "collection": {  
    "wish": 212,  //想看人数
    "collect": 13,  //看过人数
    "doing": 161,  //在看人数
    "on_hold": 9,  //搁置人数
    "dropped": 3  //抛弃人数
  },
  "crt": [   //角色数组
    {
      "id": 3218,   //角色id
      "url": "http://bgm.tv/character/3218",   //角色url
      "name": "イリヤスフィール・フォン・アインツベルン",   //角色名称
      "name_cn": "依莉雅苏菲尔·冯·爱因兹贝仑",   //角色中文名
      "role_name": "主角",   //角色类型
      "images": {
        "large": "http://lain.bgm.tv/pic/crt/l/4b/c1/3218_crt_EWQ54.jpg?r=1424098067",
        "medium": "http://lain.bgm.tv/pic/crt/m/4b/c1/3218_crt_EWQ54.jpg?r=1424098067",
        "small": "http://lain.bgm.tv/pic/crt/s/4b/c1/3218_crt_EWQ54.jpg?r=1424098067",
        "grid": "http://lain.bgm.tv/pic/crt/g/4b/c1/3218_crt_EWQ54.jpg?r=1424098067"
      },
      "comment": 41,  //角色评论
      "collects": 220,   //角色喜欢人数
      "info": {   //角色信息
        "name_cn": "依莉雅苏菲尔·冯·爱因兹贝仑",
        "alias": {   //别名
          "zh": "伊莉雅",
          "en": "Illyasviel von Einzbern"
        },
        "gender": "女",
        "birth": "7月20日",
        "bloodtype": "",
        "height": "133cm",
        "weight": "34kg",
        "bwh": "61/47/62",
        "source": "http://anime.prisma-illya.jp/character/#illya"   //来源
      },
      "actors": [  //声优信息
        {
          "id": 4402,
          "url": "http://bgm.tv/person/4402",
          "name": "門脇舞以",
          "images": {
            "large": "http://lain.bgm.tv/pic/crt/l/d6/db/4402_prsn_o6rT5.jpg",
            "medium": "http://lain.bgm.tv/pic/crt/m/d6/db/4402_prsn_o6rT5.jpg",
            "small": "http://lain.bgm.tv/pic/crt/s/d6/db/4402_prsn_o6rT5.jpg",
            "grid": "http://lain.bgm.tv/pic/crt/g/d6/db/4402_prsn_o6rT5.jpg"
          }
        }
      ]
    },
    {
      "id": 25761,
      "url": "http://bgm.tv/character/25761",
      "name": "クロエ・フォン・アインツベルン",
      "name_cn": "克洛伊·冯·爱因兹贝伦",
      "role_name": "主角",
      "images": {
        "large": "http://lain.bgm.tv/pic/crt/l/a2/69/25761_crt_LlHPn.jpg?r=1433700737",
        "medium": "http://lain.bgm.tv/pic/crt/m/a2/69/25761_crt_LlHPn.jpg?r=1433700737",
        "small": "http://lain.bgm.tv/pic/crt/s/a2/69/25761_crt_LlHPn.jpg?r=1433700737",
        "grid": "http://lain.bgm.tv/pic/crt/g/a2/69/25761_crt_LlHPn.jpg?r=1433700737"
      },
      "comment": 14,
      "collects": 9,
      "info": {
        "name_cn": "克洛伊·冯·爱因兹贝伦",
        "alias": {
          "0": "小黑",
          "en": "Chloe von Einzbern",
          "jp": "クロエ・フォン・アインツベルン"
        },
        "gender": "女",
        "birth": "7月20日",
        "bloodtype": "",
        "height": "",
        "weight": "",
        "bwh": "",
        "source": "",
        "cv": "斋藤千和"
      },
      "actors": [
        {
          "id": 4249,
          "url": "http://bgm.tv/person/4249",
          "name": "斎藤千和",
          "images": {
            "large": "http://lain.bgm.tv/pic/crt/l/01/33/4249_prsn_FHhdz.jpg?r=1425883861",
            "medium": "http://lain.bgm.tv/pic/crt/m/01/33/4249_prsn_FHhdz.jpg?r=1425883861",
            "small": "http://lain.bgm.tv/pic/crt/s/01/33/4249_prsn_FHhdz.jpg?r=1425883861",
            "grid": "http://lain.bgm.tv/pic/crt/g/01/33/4249_prsn_FHhdz.jpg?r=1425883861"
          }
        }
      ]
    },
    //后面的角色省略
  ],
  "staff": [
    {
      "id": 2860,
      "url": "http://bgm.tv/person/2860",
      "name": "大沼心",
      "name_cn": "",
      "role_name": "",   //扮演的制作团队角色
      "images": {
        "large": "http://lain.bgm.tv/pic/crt/l/fc/fd/2860_prsn_LklZy.jpg",
        "medium": "http://lain.bgm.tv/pic/crt/m/fc/fd/2860_prsn_LklZy.jpg",
        "small": "http://lain.bgm.tv/pic/crt/s/fc/fd/2860_prsn_LklZy.jpg",
        "grid": "http://lain.bgm.tv/pic/crt/g/fc/fd/2860_prsn_LklZy.jpg"
      },
      "comment": 25,
      "collects": 0,
      "info": {
        "name_cn": "",
        "alias": {  //别名
          "kana": "おおぬま しん",
          "romaji": "Oonuma Shin"
        },
        "gender": "男",
        "birth": "1976年3月8日",
        "bloodtype": "O",
        "height": "",
        "weight": "",
        "bwh": "",
        "source": []
      },
      "jobs": [
        "导演"
      ]
    },
    {
      "id": 11738,
      "url": "http://bgm.tv/person/11738",
      "name": "神保昌登",
      "name_cn": "",
      "role_name": "",
      "images": null,
      "comment": 0,
      "collects": 0,
      "info": {
        "name_cn": "",
        "alias": [],
        "gender": "",
        "birth": "",
        "bloodtype": "",
        "height": "",
        "weight": "",
        "bwh": "",
        "source": ""
      },
      "jobs": [
        "导演"
      ]
    },
    //后面的staff省略
  ],
  "topic": [  //讨论版（由于当时伊利亚第三季还没有人在讨论版发帖，所以这里示例用的是虫师的）
    {
      "id": 5145,
      "url": "http://bgm.tv/subject/topic/5145",
      "title": "老郎中的奇幻之旅",
      "main_id": 340,  //讨论版id（等同条目id？）
      "timestamp": 1413628857,
      "lastpost": 1413628857,
      "replies": 0,
      "user": {   //发帖人信息
        "id": 60750,
        "url": "http://bgm.tv/user/nydias",
        "username": "nydias",
        "nickname": "nydias",
        "avatar": {
          "large": "http://lain.bgm.tv/pic/user/l/000/06/07/60750.jpg?r=1412659521",
          "medium": "http://lain.bgm.tv/pic/user/m/000/06/07/60750.jpg?r=1412659521",
          "small": "http://lain.bgm.tv/pic/user/s/000/06/07/60750.jpg?r=1412659521"
        },
        "sign": null
      }
    },
    {
      "id": 4626,
      "url": "http://bgm.tv/subject/topic/4626",
      "title": "这片真这么好？",
      "main_id": 340,
      "timestamp": 1403845497,
      "lastpost": 1413443316,
      "replies": 58,
      "user": {
        "id": 193500,
        "url": "http://bgm.tv/user/ylc395",
        "username": "ylc395",
        "nickname": "叡山電車",
        "avatar": {
          "large": "http://lain.bgm.tv/pic/user/l/000/19/35/193500.jpg?r=1412256366",
          "medium": "http://lain.bgm.tv/pic/user/m/000/19/35/193500.jpg?r=1412256366",
          "small": "http://lain.bgm.tv/pic/user/s/000/19/35/193500.jpg?r=1412256366"
        },
        "sign": null
      }
  ],
  "blog": [   //引用的博文信息（由于当时伊利亚第三季还没有人发文，所以这里示例用的是虫师的）
    {
      "id": 46766,
      "url": "http://bgm.tv/blog/46766",
      "title": "宅之回顾【某宅的宅路历程&amp;旧番排行】",
      "summary": "【前言：今年一月写的，从人人搬来bangumi。】\r\n\r\n之前想好的，原本想在去年写的，一不小心放在今年了，用于细数姐姐最喜欢的宅番们。\r\n\r\n起因是我的一日语老师问我十月什么新番好看，我说PSYCHO-PASS吧，他说那太沉重了，动画片就是应该像OP那样燃一下再感动一下泪一把的。我当然 ...",
      "image": "",
      "replies": 39,
      "timestamp": 1374732640,
      "dateline": "2013-7-25 06:10",
      "user": {
        "id": 46055,
        "url": "http://bgm.tv/user/lans",
        "username": "lans",
        "nickname": "lans",
        "avatar": {
          "large": "http://lain.bgm.tv/pic/user/l/000/04/60/46055.jpg?r=1311253473",
          "medium": "http://lain.bgm.tv/pic/user/m/000/04/60/46055.jpg?r=1311253473",
          "small": "http://lain.bgm.tv/pic/user/s/000/04/60/46055.jpg?r=1311253473"
        },
        "sign": null
      }
    },
    {
      "id": 32287,
      "url": "http://bgm.tv/blog/32287",
      "title": "交集",
      "summary": "《虫师》\r\n不得不说，《虫师》是令我真正眼前一亮的作品。\r\n\r\n我看了许多动画，从柯南开始，先看的是《头文字D》之类的热血作品，然后慢慢地，慢慢地，就转向治愈系了。最后我重新回到了治愈系的顶峰，冈田麿里女士的功力让我一次又一次惊叹。从《花开伊吕波》到《昔花名未闻》 ...",
      "image": "",
      "replies": 9,
      "timestamp": 1333785600,
      "dateline": "2012-4-7 08:00",
      "user": {
        "id": 66129,
        "url": "http://bgm.tv/user/grubstreet",
        "username": "grubstreet",
        "nickname": "小寒",
        "avatar": {
          "large": "http://lain.bgm.tv/pic/user/l/000/06/61/66129.jpg?r=1354175227",
          "medium": "http://lain.bgm.tv/pic/user/m/000/06/61/66129.jpg?r=1354175227",
          "small": "http://lain.bgm.tv/pic/user/s/000/06/61/66129.jpg?r=1354175227"
        },
        "sign": null
      }
    }
  ]
}
```

#####获得当季新番
POST http://netaba.re/api/schedule     
HTTP 请求头  
无    

HTTP 请求正文   
JSON格式     
示例    
```
[
  
  {
    "id": 91322,
    "url": "http://bgm.tv/subject/91322",
    "type": 0,  //好像都是0
    "name": "てーきゅう 4期",
    "name_cn": "网球少女 4期",
    "summary": "",
    "eps": 0,  //疑似出问题了无效
    "air_date": "2015-04-06",   //放送日期
    "air_weekday": 1,  //星期几放送
    "images": {
      "large": "http://lain.bgm.tv/pic/cover/l/c2/39/91322_71Rx1.jpg",
      "common": "http://lain.bgm.tv/pic/cover/c/c2/39/91322_71Rx1.jpg",
      "medium": "http://lain.bgm.tv/pic/cover/m/c2/39/91322_71Rx1.jpg",
      "small": "http://lain.bgm.tv/pic/cover/s/c2/39/91322_71Rx1.jpg",
      "grid": "http://lain.bgm.tv/pic/cover/g/c2/39/91322_71Rx1.jpg"
    },
    "collection": {
      "wish": 0,
      "collect": 0,
      "doing": 71,  //只有在看人数
      "on_hold": 0,
      "dropped": 0
    }
  },
  {
    "id": 119886,
    "url": "http://bgm.tv/subject/119886",
    "type": 0,
    "name": "ミカグラ学園組曲",
    "name_cn": "御神乐学院组曲",
    "summary": "",
    "eps": 0,
    "air_date": "2015-04-06",
    "air_weekday": 1,
    "images": {
      "large": "http://lain.bgm.tv/pic/cover/l/54/8e/119886_SJVan.jpg",
      "common": "http://lain.bgm.tv/pic/cover/c/54/8e/119886_SJVan.jpg",
      "medium": "http://lain.bgm.tv/pic/cover/m/54/8e/119886_SJVan.jpg",
      "small": "http://lain.bgm.tv/pic/cover/s/54/8e/119886_SJVan.jpg",
      "grid": "http://lain.bgm.tv/pic/cover/g/54/8e/119886_SJVan.jpg"
    },
    "collection": {
      "wish": 0,
      "collect": 0,
      "doing": 208,
      "on_hold": 0,
      "dropped": 0
    }
  },
  //后面省略
]
```
说明   
这个API好像有问题，我在制作这份文档的时候是2015-7，但是他返回的仍然是4月新番   

###用户相关API   
#####用户基本信息    
POST http://netaba.re/api/user      
HTTP 请求头  
Key: `Authorization`   
Value: `Basic {base64({登录响应正文中的username}:{登录响应正文中的auth})}`  

HTTP 请求正文   
JSON格式     
示例    
```
{
  "id": 43535,    //用户id
  "url": "http://bgm.tv/user/sample",
  "username": "sampleuser",   //用户名
  "nickname": "samplenickname",   //用户昵称
  "avatar": {
    "large": "http://lain.bgm.tv/pic/user/l/000/08/51/853384.jpg?r=1340538060",
    "medium": "http://lain.bgm.tv/pic/user/m/000/08/51/853384.jpg?r=1340538060",
    "small": "http://lain.bgm.tv/pic/user/s/000/08/51/853384.jpg?r=1340538060"
  },
  "sign": ""
}
```

#####用户观看统计
POST http://netaba.re/api/user/{用户的username}/stats   
HTTP 请求头  
Key: `Authorization`   
Value: `Basic {base64({登录响应正文中的username}:{登录响应正文中的auth})}`  

HTTP 请求正文   
JSON格式     
示例    
```
{
  "username": "sampleuser",   //用户名
  "nickname": "samplenickname @sampleuser",   //用户昵称 后面跟@用户名
  "avatar": "//lain.bgm.tv/pic/user/l/000/08/51/85184.jpg?r=1340538060",  //头像
  "wished": 26,   //想看
  "held": 14,   //搁置
  "watching": 0,  //在看，好像有问题一直是0
  "trashed": 42,  //抛弃
  "watched": 133,   //看过
  "rated": 132,   //打过分的
  "average": "8.1061",   //打分均分
  "median": 8,   //打分中位数
  "deviation": "1.0819",  //打分标准差
  "distribution": [  //打分分布
    0,
    0,
    0,
    0,
    0,
    8,
    31,
    48,
    29,
    16
  ],
  "watched_subjects": [   //看过的条目
    {
      "name": "日常",  //中文名
      "alt_name": "日常",  //日文名
      "rate": 9  //打分
    },
    {
      "name": "樱Trick", 
      "alt_name": "桜Trick",
      "rate": 8
    },
    //后面省略
  ]
}
```

<a rel="license" href="http://creativecommons.org/licenses/by-sa/4.0/"><img alt="知识共享许可协议" style="border-width:0" src="https://i.creativecommons.org/l/by-sa/4.0/88x31.png" /></a><br />本作品采用<a rel="license" href="http://creativecommons.org/licenses/by-sa/4.0/">知识共享署名-相同方式共享 4.0 国际许可协议</a>进行许可。