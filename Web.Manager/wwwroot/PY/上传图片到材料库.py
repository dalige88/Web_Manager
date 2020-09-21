# -*- coding:utf-8 -*-


import requests
import json
import os
import sys

from urllib3 import encode_multipart_formdata


COOKIE = 'sso_auth_status=c747e5926c89638861a08c0cd7fe6efa; MONITOR_WEB_ID=fe057935-84b3-41f5-a1b1-0021b5d3517d; __utmz=24953151.1600074657.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); ttcid=3319e09ff98c4f22843f9f8ad8dee93810; __utma=24953151.266708958.1600074657.1600074657.1600248484.2; tt_scid=cNLOyw-qOzYDGQ4VtZLqvipWJokL8GZE0PLPZH9fESVIz9Isi7vZZVRYKiOGKLAe61b6; passport_csrf_token=78dbe1ed25befc8e089fec27e1f0f5d8; _ba=BA0.2-20200921-5110e-LfJEnxkwdLsLH6nInMxr; s_v_web_id=verify_kfc4phii_7pkPYRxV_8jfl_4DPY_AUx2_NMHdpTofRNuB; ccid=2c37c97bfc9311bbd3297e13014ee2a6; sso_uid_tt=c50f37f26f1e15f0388efc1dca066e1b; sso_uid_tt_ss=c50f37f26f1e15f0388efc1dca066e1b; toutiao_sso_user=74ee7c66a1de816b21a75e3eb05670a8; toutiao_sso_user_ss=74ee7c66a1de816b21a75e3eb05670a8; passport_auth_status=855c853d046aa5a15667e1b1c7b9a1c9%2Cd1fc5e47475b37917a98c22a51e9a846; sid_guard=41572882fa095a637a1262a56766ab4b%7C1600673400%7C5184000%7CFri%2C+20-Nov-2020+07%3A30%3A00+GMT; uid_tt=4966bbfb8a68ec0664884a9acbd0b2ee; uid_tt_ss=4966bbfb8a68ec0664884a9acbd0b2ee; sid_tt=41572882fa095a637a1262a56766ab4b; sessionid=41572882fa095a637a1262a56766ab4b; sessionid_ss=41572882fa095a637a1262a56766ab4b; tt_webid=6874839848109524488; gftoken=NDE1NzI4ODJmYXwxNjAwNjczNDMzOTl8fDAGBgYGBgY'


# 上传图片
def update_file(image_path):
    image_path=image_path.replace("/","\\")
    # image_path = r'C:\Users\Administrator\Desktop\8888.jpg'
    url = "https://mp.toutiao.com/tools/upload_picture/?type=ueditor&pgc_watermark=1&action=uploadimage&encode=utf-8"

    headers = {
        'content-type':'multipart/form-data; boundary=----WebKitFormBoundarySPGWzUNHa41APTAb',
        'origin': 'https://www.toutiao.com',
        'referer': 'https://www.toutiao.com/',
        "User-Agent": "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.100 Safari/537.36",
        "cookie" :COOKIE,
    }
    data = {}
    data['upfile'] = (image_path.rsplit(os.sep)[-1], open(image_path, 'rb').read())
    encode_data = encode_multipart_formdata(data)
    data = encode_data[0]
    headers['content-type'] = encode_data[1]


    reponse = requests.post(url=url, data=data, headers=headers)
    
    dt = json.loads(reponse.text)
    
    print(dt)
    # return dt




# update_file(r'C:\Users\Administrator\Desktop\tmp\tt.jpg')
update_file('E:\\work\\NET\\WebManager\\WebManager\\Web.Manager/wwwroot/upload/20200921/bd9b4464-2aa3-4a00-95d8-37d9db15d8dc.jpg')
# print(sys.argv)




















































