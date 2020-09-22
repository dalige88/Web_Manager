# -*- coding:utf-8 -*-


import requests
import json
import os
import time
import sys
import base64

from urllib3 import encode_multipart_formdata

user_id = '1674708556955652'
COOKIE = 'sso_auth_status=c747e5926c89638861a08c0cd7fe6efa; MONITOR_WEB_ID=fe057935-84b3-41f5-a1b1-0021b5d3517d; __utmz=24953151.1600074657.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); ttcid=3319e09ff98c4f22843f9f8ad8dee93810; __utma=24953151.266708958.1600074657.1600074657.1600248484.2; tt_scid=cNLOyw-qOzYDGQ4VtZLqvipWJokL8GZE0PLPZH9fESVIz9Isi7vZZVRYKiOGKLAe61b6; passport_csrf_token=78dbe1ed25befc8e089fec27e1f0f5d8; _ba=BA0.2-20200921-5110e-LfJEnxkwdLsLH6nInMxr; s_v_web_id=verify_kfc4phii_7pkPYRxV_8jfl_4DPY_AUx2_NMHdpTofRNuB; ccid=2c37c97bfc9311bbd3297e13014ee2a6; sso_uid_tt=c50f37f26f1e15f0388efc1dca066e1b; sso_uid_tt_ss=c50f37f26f1e15f0388efc1dca066e1b; toutiao_sso_user=74ee7c66a1de816b21a75e3eb05670a8; toutiao_sso_user_ss=74ee7c66a1de816b21a75e3eb05670a8; passport_auth_status=855c853d046aa5a15667e1b1c7b9a1c9%2Cd1fc5e47475b37917a98c22a51e9a846; sid_guard=41572882fa095a637a1262a56766ab4b%7C1600673400%7C5184000%7CFri%2C+20-Nov-2020+07%3A30%3A00+GMT; uid_tt=4966bbfb8a68ec0664884a9acbd0b2ee; uid_tt_ss=4966bbfb8a68ec0664884a9acbd0b2ee; sid_tt=41572882fa095a637a1262a56766ab4b; sessionid=41572882fa095a637a1262a56766ab4b; sessionid_ss=41572882fa095a637a1262a56766ab4b; tt_webid=6874839848109524488; gftoken=NDE1NzI4ODJmYXwxNjAwNjczNDMzOTl8fDAGBgYGBgY'



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


# 转换时间格式
def time_to_date(timestamp,format="%Y-%m-%d %H:%M:%S"):
	"""
	:usage:
		时间戳转换为日期
	:param data:
		@timestamp		：时间戳，int类型，如：1537535021
	:return:
		@otherStyleTime ：转换结果日期，格式： 年-月-日 时:分:秒
	"""
	timearr = time.localtime(timestamp)
	otherStyleTime = time.strftime(format, timearr)
	return  otherStyleTime



# 发布文章
def post_article(local_image_path,title,content,timer_time=None,run_ad=True,writting_race_mode=0,extern_link=None):

    # 发布内容
    cont = ''
    
    num = 0
    for item in content:
        if num>2:
            cont+=item+" "
        num=num+1
    
    # 再把加密后的结果解码
    temp = base64.b64decode(cont).decode()

    # print(title)

    """
    :param title: 图文作品 标题
    :param content: 图文作品 内容
    :param extern_link: 扩展链接
    :param timer_time: 定时发布的时间
    :param run_ad: 是否投放头条广告
    :param writting_race_mode: 参加 新写作大赛 的模式： 0:不参加 1:参加主竞赛单元评选 2:参加青年竞赛单元选
    :param cover_img: 封面图，可以是图片网络地址 或是 本地图片路径
    """
    
    url = "https://www.toutiao.com/mp/agw/article/publish/#source=toutiaoPC&type=article&app_name=toutiao.com&_signature=_02B4Z6wo00101THtXvgAAIBBmSz#lcS7g0kx6FpAABM-rY1jg1YJjYnU87.vj45feF.AZcJPBE.VWCWzrV7J6H#OYnkGSENp5INLD6Xibr4iekdB1OMY6L7V87AzCDLgw8oPo9lIUdOEN-m446"
    
    headers = {
        "cookie" :COOKIE,
        "Host": "www.toutiao.com",
        "Connection": "close",
        "Accept": "text/javascript, text/html, application/xml, text/xml, */*",
        "Origin": "https://www.toutiao.com",
        "X-CSRFToken": "54d481cf1d0c7778fb88f139a44bbf7c",
        "X-Requested-With": "XMLHttpRequest",
        "User-Agent": "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko)#Chrome/75.0.3770.10 #Safari/537.36",
        "Content-Type": "application/x-www-form-urlencoded",
        "Referer": "https://www.toutiao.com/",
        "Accept-Encoding": "gzip, deflate",
        "Accept-Language": "zh-CN,zh;q=0.9",
    }
    
    _time = time_to_date(time.time() + 3600) if not timer_time else timer_time
    cover = {}
    
    res = update_file(local_image_path)
    
    width = res.get('width')
    height = res.get('height')
    cover = {
        "id": 1,
        "url": URL_PGC_IMG_PREFIX + res.get('web_uri'),
        "uri": res.get('web_uri'),
        "origin_uri": res.get('original'),
        "ic_uri": "",
        "thumb_width": f'{width}',
        "thumb_height": f'{height}'
    }
    _cover = '[{"id":2,"url":"' + cover['url'] + '","uri":"' + cover['uri'] + \
                 '","origin_uri":"' + cover['origin_uri'] + \
                 '","ic_uri":"","thumb_width":' + cover['thumb_width'] + \
                 ',"thumb_height":' + cover['thumb_height'] + '}]' if cover else '[]'
    
    data = {
        'article_type': 0,
        'title': title,
        'content': temp,
        'save': 1,
        'source':'21',
        'pgc_feed_covers': _cover,
        'article_ad_type': [2, 3][run_ad],
    }
    
    
    reponse = requests.post(url=url, data=data, headers=headers)
    dt = json.loads(reponse.text)
    
    print(dt)
    

post_article(
   sys.argv[1],
   sys.argv[2],
   sys.argv,
   run_ad=True
   )






# post_article(
#    "德克萨斯州“灾难性”第4类风暴",
#    content,
#    run_ad=False
#    )
    # """
    # :param title: 图文作品 标题
    # :param content: 图文作品 内容
    # :param extern_link: 扩展链接
    # :param timer_time: 定时发布的时间
    # :param run_ad: 是否投放头条广告
    # :param writting_race_mode: 参加 新写作大赛 的模式： 0:不参加 1:参加主竞赛单元评选 2:参加青年竞赛单元评选
    # :param cover_img: 封面图，可以是图片网络地址 或是 本地图片路径
    # """




















































