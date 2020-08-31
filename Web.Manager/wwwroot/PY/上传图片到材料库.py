# -*- coding:utf-8 -*-


import requests
import json
import os
import sys

from urllib3 import encode_multipart_formdata


COOKIE = 'MONITOR_WEB_ID=1b9ed759-d539-4de8-975d-419209a893fa; ttcid=e22294605adc40dbbe068c94befb200e65; csrftoken=54d481cf1d0c7778fb88f139a44bbf7c; WEATHER_CITY=%E5%8D%8E%E8%93%A5; sso_auth_status=919f8cd4950d363692355a63bd5b466d; sso_uid_tt=eb0a4c50c8d04ccf20fbb2666743f4a1; sso_uid_tt_ss=eb0a4c50c8d04ccf20fbb2666743f4a1; toutiao_sso_user=f8929d3005c515e4bb2c57073ddc3c9b; toutiao_sso_user_ss=f8929d3005c515e4bb2c57073ddc3c9b; passport_auth_status=d545e30c2a33c3d341307f6ab6950a2c%2C87c13728de0832f9f57e07460ca8c44a; sid_guard=d5d3c1cd09ec7d8778a73c831b5df836%7C1597918880%7C5184000%7CMon%2C+19-Oct-2020+10%3A21%3A20+GMT; uid_tt=35a1c34e5320e482d713b884b65af8b4; uid_tt_ss=35a1c34e5320e482d713b884b65af8b4; sid_tt=d5d3c1cd09ec7d8778a73c831b5df836; sessionid=d5d3c1cd09ec7d8778a73c831b5df836; sessionid_ss=d5d3c1cd09ec7d8778a73c831b5df836; s_v_web_id=verify_ke3n0831_saUE90gU_MZQM_4azi_8g4v_Op9hf6hlaS4d; __ac_signature=_02B4Z6wo00f01vh-oMgAAIBCUL8AphnL.ob4eqRAAOEuasMrWjuHbOSFRV8KA2Wz02TIxQCB0QLV4qcPvYMDyMXmOXM7yr0AkGDIRjn11DT6C0Be1LD4tUdRySTwv6i2nuQooYr-TDOyY913d1; tt_webid=6863336770638005767; tt_webid=6863336770638005767; __tasessionId=jhbmfh9fr1597997593355; tt_scid=K0G0w6b3PGtEG65E1twbrPDuciE4IuCvU85..3ZQJnREuvGsXi0g0vwNZP9TMOMV8409'


# 上传图片
def update_file(image_path):
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
    return dt




# update_file(r'C:\Users\Administrator\Desktop\tmp\tt.jpg')
update_file(sys.argv[1])




















































