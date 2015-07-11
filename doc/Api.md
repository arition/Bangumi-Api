#Bangumi 基于netaba.re的API文档
### 约定
{}表示需替换内容
### 基本API
##### 登录
`POST` `http://netaba.re/api/login`   
HTTP 请求头  
Key: `Authorization`   
Value: `Basic {basic64编码的{用户名}:{密码}}`  

HTTP 请求正文   
无   

HTTP 响应头  
无特殊信息

HTTP 响应正文   
JSON格式
```
{
  "id": 1,
  "url": "http://bgm.tv/user/sampleuser",
  "username": "MD5哈希值，原字符串未知",
  "nickname": "sampleuser",
  "avatar": {
    "large": "大头像url",
    "medium": "中头像url",
    "small": "小头像url"
  },
  "sign": "",
  "auth": "验证Token",
  "auth_encode": "未知作用的Token"
}
```
    
说明  
此为基本的登录Api，验证方式使用的是基本的http Basic验证。响应关键需要记录`username`和`auth`这两个key，这两个key是访问其他Api时必须要提供的验证信息