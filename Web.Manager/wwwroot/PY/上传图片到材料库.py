# -*- coding:utf-8 -*-


import requests
import json
import os
import sys
import base64

from urllib3 import encode_multipart_formdata



# 上传图片
def update_file(cookie, image_path):
    url = "https://mp.toutiao.com/tools/upload_picture/?type=ueditor&pgc_watermark=1&action=uploadimage&encode=utf-8"

    # 再把加密后的结果解码
    ck = base64.b64decode(cookie).decode()

    headers = {
        'content-type':'multipart/form-data; boundary=----WebKitFormBoundarySPGWzUNHa41APTAb',
        'origin': 'https://www.toutiao.com',
        'referer': 'https://www.toutiao.com/',
        "User-Agent": "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.100 Safari/537.36",
        "cookie" :ck,
    }
    data = {}
    data['upfile'] = (image_path.rsplit(os.sep)[-1], open(image_path, 'rb').read())
    encode_data = encode_multipart_formdata(data)
    data = encode_data[0]
    headers['content-type'] = encode_data[1]


    reponse = requests.post(url=url, data=data, headers=headers)
    
    # print(reponse.text)
    dt = json.loads(reponse.text)
    
    print(dt)
    # return dt




update_file(sys.argv[1],sys.argv[2])




















































