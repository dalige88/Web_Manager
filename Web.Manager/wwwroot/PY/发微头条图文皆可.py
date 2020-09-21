# -*- coding:utf-8 -*-

import requests
import json
import os
import sys
import base64
from urllib3 import encode_multipart_formdata



# COOKIE = 'MONITOR_WEB_ID=1b9ed759-d539-4de8-975d-419209a893fa; ttcid=e22294605adc40dbbe068c94befb200e65; csrftoken=54d481cf1d0c7778fb88f139a44bbf7c; WEATHER_CITY=%E5%8D%8E%E8%93%A5; sso_auth_status=919f8cd4950d363692355a63bd5b466d; sso_uid_tt=eb0a4c50c8d04ccf20fbb2666743f4a1; sso_uid_tt_ss=eb0a4c50c8d04ccf20fbb2666743f4a1; toutiao_sso_user=f8929d3005c515e4bb2c57073ddc3c9b; toutiao_sso_user_ss=f8929d3005c515e4bb2c57073ddc3c9b; passport_auth_status=d545e30c2a33c3d341307f6ab6950a2c%2C87c13728de0832f9f57e07460ca8c44a; sid_guard=d5d3c1cd09ec7d8778a73c831b5df836%7C1597918880%7C5184000%7CMon%2C+19-Oct-2020+10%3A21%3A20+GMT; uid_tt=35a1c34e5320e482d713b884b65af8b4; uid_tt_ss=35a1c34e5320e482d713b884b65af8b4; sid_tt=d5d3c1cd09ec7d8778a73c831b5df836; sessionid=d5d3c1cd09ec7d8778a73c831b5df836; sessionid_ss=d5d3c1cd09ec7d8778a73c831b5df836; s_v_web_id=verify_ke3n0831_saUE90gU_MZQM_4azi_8g4v_Op9hf6hlaS4d; __ac_signature=_02B4Z6wo00f01vh-oMgAAIBCUL8AphnL.ob4eqRAAOEuasMrWjuHbOSFRV8KA2Wz02TIxQCB0QLV4qcPvYMDyMXmOXM7yr0AkGDIRjn11DT6C0Be1LD4tUdRySTwv6i2nuQooYr-TDOyY913d1; tt_webid=6863336770638005767; tt_webid=6863336770638005767; __tasessionId=jhbmfh9fr1597997593355; tt_scid=K0G0w6b3PGtEG65E1twbrPDuciE4IuCvU85..3ZQJnREuvGsXi0g0vwNZP9TMOMV8409'
COOKIE = 'sso_auth_status=c747e5926c89638861a08c0cd7fe6efa; MONITOR_WEB_ID=fe057935-84b3-41f5-a1b1-0021b5d3517d; __utmz=24953151.1600074657.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); ttcid=3319e09ff98c4f22843f9f8ad8dee93810; __utma=24953151.266708958.1600074657.1600074657.1600248484.2; tt_scid=cNLOyw-qOzYDGQ4VtZLqvipWJokL8GZE0PLPZH9fESVIz9Isi7vZZVRYKiOGKLAe61b6; passport_csrf_token=78dbe1ed25befc8e089fec27e1f0f5d8; _ba=BA0.2-20200921-5110e-LfJEnxkwdLsLH6nInMxr; s_v_web_id=verify_kfc4phii_7pkPYRxV_8jfl_4DPY_AUx2_NMHdpTofRNuB; ccid=2c37c97bfc9311bbd3297e13014ee2a6; sso_uid_tt=c50f37f26f1e15f0388efc1dca066e1b; sso_uid_tt_ss=c50f37f26f1e15f0388efc1dca066e1b; toutiao_sso_user=74ee7c66a1de816b21a75e3eb05670a8; toutiao_sso_user_ss=74ee7c66a1de816b21a75e3eb05670a8; passport_auth_status=855c853d046aa5a15667e1b1c7b9a1c9%2Cd1fc5e47475b37917a98c22a51e9a846; sid_guard=41572882fa095a637a1262a56766ab4b%7C1600673400%7C5184000%7CFri%2C+20-Nov-2020+07%3A30%3A00+GMT; uid_tt=4966bbfb8a68ec0664884a9acbd0b2ee; uid_tt_ss=4966bbfb8a68ec0664884a9acbd0b2ee; sid_tt=41572882fa095a637a1262a56766ab4b; sessionid=41572882fa095a637a1262a56766ab4b; sessionid_ss=41572882fa095a637a1262a56766ab4b; tt_webid=6874839848109524488; gftoken=NDE1NzI4ODJmYXwxNjAwNjczNDMzOTl8fDAGBgYGBgY'


# 发布微头条地址 （域名）
API_PUBLISH = 'https://www.toutiao.com/c/ugc/content/publish/'

# 上传图片地址 （域名）
URL_PGC_IMG_PREFIX = "https://p3.pstatp.com/list/"

# 上传图片
def update_file(local_image_path):
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
    data['upfile'] = (local_image_path.rsplit(os.sep)[-1], open(local_image_path, 'rb').read())
    encode_data = encode_multipart_formdata(data)
    data = encode_data[0]
    headers['content-type'] = encode_data[1]


    reponse = requests.post(url=url, data=data, headers=headers)
    
    dt = json.loads(reponse.text)
    # AddData(data)
    
    # print(dt)
    return dt


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

    cont = ''
    
    num = 0
    for item in content:
        if num>1:
            cont+=item+" "
        num=num+1
    
    # 再把加密后的结果解码
    temp = base64.b64decode(cont).decode()
    temp=temp.replace("\n","      ")
    
    
    image_uris = []
    web_uri = ''
    for i in images:
        json_res = update_file(i)
        uri = json_res.get('web_uri')
        image_uris.append(uri)
        web_uri = ','.join(image_uris)
    
    
    data = {
        'content': temp,
        'image_uris': web_uri,
    }

    
    
    reponse = requests.post(url=API_PUBLISH, data=data, headers=headers)
    
    dt = json.loads(reponse.text)
    
    print(dt)


contents="有的新娘礼服通常是用买的，但这样真的划算吗？ 有别于亚洲普遍的婚纱租借服务，在美国，新娘婚纱都是要掏钱购买的，因此婚纱往往会是婚礼中的一笔不小投资(注意，这是投资…不是开销XD)。而且在美国婚礼上，新娘也不会像华人婚礼中的一整晚换好几套礼服轮番登场。(当然你也可以来个中西合并换装登场～反正婚礼当天新娘最大！)"
images=[r'C:\Users\Administrator\Desktop\tmp\1234.png',r'C:\Users\Administrator\Desktop\tmp\123456.png']
# post_weitt(contents,images)



post_weitt(
    sys.argv,
    sys.argv[1]
    )








