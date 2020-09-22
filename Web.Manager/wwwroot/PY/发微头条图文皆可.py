# -*- coding:utf-8 -*-

import requests
import json
import os
import sys
import base64
from urllib3 import encode_multipart_formdata


# 再把加密后的结果解码（cookie）
COOKIE = base64.b64decode(sys.argv[1]).decode()

# 发布微头条地址 （域名）
API_PUBLISH = 'https://www.toutiao.com/c/ugc/content/publish/'

# 上传图片地址 （域名）
URL_PGC_IMG_PREFIX = "https://p3.pstatp.com/list/"

# 上传图片
def update_file(local_image_path):
    print(COOKIE)
   

    # url = "https://mp.toutiao.com/tools/upload_picture/?type=ueditor&pgc_watermark=1&action=uploadimage&encode=utf-8"
    # 
    # headers = {
    #     'content-type':'multipart/form-data; boundary=----WebKitFormBoundarySPGWzUNHa41APTAb',
    #     'origin': 'https://www.toutiao.com',
    #     'referer': 'https://www.toutiao.com/',
    #     "User-Agent": "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.100 Safari/537.36",
    #     "cookie" :COOKIE,
    # }
    # data = {}
    # data['upfile'] = (local_image_path.rsplit(os.sep)[-1], open(local_image_path, 'rb').read())
    # encode_data = encode_multipart_formdata(data)
    # data = encode_data[0]
    # headers['content-type'] = encode_data[1]
    # 
    # 
    # reponse = requests.post(url=url, data=data, headers=headers)
    # 
    # # print(reponse.text)
    # dt = json.loads(reponse.text)
    # 
    # 
    # print(dt)
    # return dt


headers = {
    "cookie" :COOKIE,
    "Host": "www.toutiao.com",
    "Connection": "close",
    "Accept": "text/javascript, text/html, application/xml, text/xml, */*",
    "Origin": "https://www.toutiao.com",
    "X-CSRFToken": "54d481cf1d0c7778fb88f139a44bbf7c",
    "X-Requested-With": "XMLHttpRequest",
    "User-Agent": "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.100 Safari/537.36",
    "Content-Type": "application/x-www-form-urlencoded",
    "Referer": "https://www.toutiao.com/",
    "Accept-Encoding": "gzip, deflate, br",
    "Accept-Language": "zh-CN,zh;q=0.9",
}


def post_weitt(content, image=None):
    # res = update_file(local_image_path)
    
    image=image.replace("/","\\")
    images=(image).split(',')

    # cont = ''
    # 
    # num = 0
    # for item in content:
    #     if num>1:
    #         cont+=item+" "
    #     num=num+1
    
    # 再把加密后的结果解码
    temp = base64.b64decode(content).decode()
    temp=temp.replace("\n"," ")
    
    
    

    image_uris = []
    web_uri = ''

    # print(COOKIE)
    for i in images:
        json_res = update_file(i)
        uri = json_res.get('web_uri')
        image_uris.append(uri)
        web_uri = ','.join(image_uris)
    
    
    data = {
        'content': temp,
        'image_uris': web_uri,
    }

    
    # print(web_uri)
    reponse = requests.post(url=API_PUBLISH, data=data, headers=headers)
    
    dt = json.loads(reponse.text)
    
    print(dt)


# post_weitt(
#     sys.argv[3],
#     sys.argv[2]
#     )

update_file('E:\work\NET\WebManager\WebManager\Web.Manager/wwwroot/upload/20200922/fc6e4bb0-0da0-4d0a-9535-8b5087f868e0.jpg')








