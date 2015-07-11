#Bangumi 基于netaba.re的API文档
### 约定
{}表示需替换内容
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

###收视相关Api
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
      "type": 2,  //0想看 1看过 2在看 3搁置 4抛弃
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
      "collection": {  //此部分不明
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


<a rel="license" href="http://creativecommons.org/licenses/by-sa/4.0/"><img alt="知识共享许可协议" style="border-width:0" src="https://i.creativecommons.org/l/by-sa/4.0/88x31.png" /></a><br />本作品采用<a rel="license" href="http://creativecommons.org/licenses/by-sa/4.0/">知识共享署名-相同方式共享 4.0 国际许可协议</a>进行许可。