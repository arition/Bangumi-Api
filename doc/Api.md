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

### 基本API
##### 登录
POST http://netaba.re/api/login   

HTTP 请求头  
Key: `Authorization`   
Value: `Basic {basic64编码的{用户名}:{密码}}`  

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
Value: `Basic {basic64编码的{登录响应正文中的username}:{登录响应正文中的auth}}`  

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
Value: `Basic {basic64编码的{登录响应正文中的username}:{登录响应正文中的auth}}`  

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
Value: `Basic {basic64编码的{登录响应正文中的username}:{登录响应正文中的auth}}`  

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
Value: `Basic {basic64编码的{登录响应正文中的username}:{登录响应正文中的auth}}`  

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
Value: `Basic {basic64编码的{登录响应正文中的username}:{登录响应正文中的auth}}`  

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
Value: `Basic {basic64编码的{登录响应正文中的username}:{登录响应正文中的auth}}`  

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
Value: `Basic {basic64编码的{登录响应正文中的username}:{登录响应正文中的auth}}`  

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

<a rel="license" href="http://creativecommons.org/licenses/by-sa/4.0/"><img alt="知识共享许可协议" style="border-width:0" src="https://i.creativecommons.org/l/by-sa/4.0/88x31.png" /></a><br />本作品采用<a rel="license" href="http://creativecommons.org/licenses/by-sa/4.0/">知识共享署名-相同方式共享 4.0 国际许可协议</a>进行许可。