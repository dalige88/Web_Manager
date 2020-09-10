# -*- coding:utf-8 -*-


import requests
import json
import os
import time
import sys

from urllib3 import encode_multipart_formdata

user_id = '1674708556955652'
COOKIE = 'MONITOR_WEB_ID=1b9ed759-d539-4de8-975d-419209a893fa; ttcid=e22294605adc40dbbe068c94befb200e65; csrftoken=54d481cf1d0c7778fb88f139a44bbf7c; WEATHER_CITY=%E5%8D%8E%E8%93%A5; sso_auth_status=919f8cd4950d363692355a63bd5b466d; sso_uid_tt=eb0a4c50c8d04ccf20fbb2666743f4a1; sso_uid_tt_ss=eb0a4c50c8d04ccf20fbb2666743f4a1; toutiao_sso_user=f8929d3005c515e4bb2c57073ddc3c9b; toutiao_sso_user_ss=f8929d3005c515e4bb2c57073ddc3c9b; passport_auth_status=d545e30c2a33c3d341307f6ab6950a2c%2C87c13728de0832f9f57e07460ca8c44a; sid_guard=d5d3c1cd09ec7d8778a73c831b5df836%7C1597918880%7C5184000%7CMon%2C+19-Oct-2020+10%3A21%3A20+GMT; uid_tt=35a1c34e5320e482d713b884b65af8b4; uid_tt_ss=35a1c34e5320e482d713b884b65af8b4; sid_tt=d5d3c1cd09ec7d8778a73c831b5df836; sessionid=d5d3c1cd09ec7d8778a73c831b5df836; sessionid_ss=d5d3c1cd09ec7d8778a73c831b5df836; s_v_web_id=verify_ke3n0831_saUE90gU_MZQM_4azi_8g4v_Op9hf6hlaS4d; __ac_signature=_02B4Z6wo00f01vh-oMgAAIBCUL8AphnL.ob4eqRAAOEuasMrWjuHbOSFRV8KA2Wz02TIxQCB0QLV4qcPvYMDyMXmOXM7yr0AkGDIRjn11DT6C0Be1LD4tUdRySTwv6i2nuQooYr-TDOyY913d1; tt_webid=6863336770638005767; tt_webid=6863336770638005767; __tasessionId=jhbmfh9fr1597997593355; tt_scid=K0G0w6b3PGtEG65E1twbrPDuciE4IuCvU85..3ZQJnREuvGsXi0g0vwNZP9TMOMV8409'



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
    # print(content)
    # 发布内容
    cont = ''
   
    num = 0
    for item in content:
        if num>2:
            cont+=item+" "
        num=num+1
    
    # cont = cont.replace('`','"')

    """
    :param title: 图文作品 标题
    :param content: 图文作品 内容
    :param extern_link: 扩展链接
    :param timer_time: 定时发布的时间
    :param run_ad: 是否投放头条广告
    :param writting_race_mode: 参加 新写作大赛 的模式： 0:不参加 1:参加主竞赛单元评选 2:参加青年竞赛单元评选
    :param cover_img: 封面图，可以是图片网络地址 或是 本地图片路径
    """

    # url = "https://mp.toutiao.com/core/article/edit_article_post/?source=mp&type=article"
    url = "https://www.toutiao.com/mp/agw/article/publish/?source=toutiaoPC&type=article&app_name=toutiao.com&_signature=_02B4Z6wo00101THtXvgAAIBBmSz-lcS7g0kx6FpAABM-rY1jg1YJjYnU87.vj45feF.AZcJPBE.VWCWzrV7J6H-OYnkGSENp5INLD6Xibr4iekdB1OMY6L7V87AzCDLgw8oPo9lIUdOEN-m446"

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
        'content': cont.replace('`','"'),
        'save': 1,
        'source':'21',
        'pgc_feed_covers': _cover,
    }

    # print(cont)

   
    reponse = requests.post(url=url, data=data, headers=headers)
    dt = json.loads(reponse.text)
    
    print(dt)
    

post_article(
   sys.argv[1],
   sys.argv[2],
   sys.argv,
   run_ad=true
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




















































