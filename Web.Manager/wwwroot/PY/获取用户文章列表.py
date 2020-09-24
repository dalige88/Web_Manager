# -*- coding:utf-8 -*-

import requests,json
import MySQLdb
import time

# 起始分页值
# cursor=0

# 每页显示的数量 20 (必须是 20 不然会报错)
# count=20

# 用户ID
# user_id='808841720377389'

# 用户cookie,可置空，格式 ： 'tt_web_id=xxxxx;sso_user=xxxx'
COOKIE = 'MONITOR_WEB_ID=1b9ed759-d539-4de8-975d-419209a893fa; ttcid=e22294605adc40dbbe068c94befb200e65; csrftoken=54d481cf1d0c7778fb88f139a44bbf7c; WEATHER_CITY=%E5%8D%8E%E8%93%A5; sso_auth_status=919f8cd4950d363692355a63bd5b466d; sso_uid_tt=eb0a4c50c8d04ccf20fbb2666743f4a1; sso_uid_tt_ss=eb0a4c50c8d04ccf20fbb2666743f4a1; toutiao_sso_user=f8929d3005c515e4bb2c57073ddc3c9b; toutiao_sso_user_ss=f8929d3005c515e4bb2c57073ddc3c9b; passport_auth_status=d545e30c2a33c3d341307f6ab6950a2c%2C87c13728de0832f9f57e07460ca8c44a; sid_guard=d5d3c1cd09ec7d8778a73c831b5df836%7C1597918880%7C5184000%7CMon%2C+19-Oct-2020+10%3A21%3A20+GMT; uid_tt=35a1c34e5320e482d713b884b65af8b4; uid_tt_ss=35a1c34e5320e482d713b884b65af8b4; sid_tt=d5d3c1cd09ec7d8778a73c831b5df836; sessionid=d5d3c1cd09ec7d8778a73c831b5df836; sessionid_ss=d5d3c1cd09ec7d8778a73c831b5df836; s_v_web_id=verify_ke3n0831_saUE90gU_MZQM_4azi_8g4v_Op9hf6hlaS4d; __ac_signature=_02B4Z6wo00f01vh-oMgAAIBCUL8AphnL.ob4eqRAAOEuasMrWjuHbOSFRV8KA2Wz02TIxQCB0QLV4qcPvYMDyMXmOXM7yr0AkGDIRjn11DT6C0Be1LD4tUdRySTwv6i2nuQooYr-TDOyY913d1; tt_webid=6863336770638005767; tt_webid=6863336770638005767; __tasessionId=jhbmfh9fr1597997593355; tt_scid=K0G0w6b3PGtEG65E1twbrPDuciE4IuCvU85..3ZQJnREuvGsXi0g0vwNZP9TMOMV8409'


# 获取用户粉丝列表
def get_page():
    url = "https://www.toutiao.com/c/user/article/?page_type=1&user_id=808841720377389&max_behot_time=0&count=20&as=A185DF53EF788EC&cp=5F3FB858DEFCDE1&_signature=_02B4Z6wo00d01snOfVgAAIBCYQ.dNx7LzMbJy3nAAO1aJrqVxmVhQZWwgM89Bvyde3vI2wo6xbWvBPRHQXBClQFPJj4OG2g2kvObpGbf.pMSZ4IoqAOw02bilBk6YrIqx1BIjX.zkPozRQc621"
    headers = {
        "User-Agent": "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.100 Safari/537.36",
        "cookie": COOKIE,
    }
    reponse = requests.get(url, headers=headers)
    
    data = json.loads(reponse.text)
    # data = json.loads(reponse.text.replace(' ', '').replace('\n', '').replace('\r', ''))
    AddData(data)
    # return data
    
    time.sleep(600)
    get_page()


# 连接 MYSQL 数据库
def comm_DB(sql):
    # 打开数据库链接
    db = MySQLdb.connect("47.100.172.61", "root", "root", "ai_platform", charset="utf8")
    # 使用cursor()方法获取操作游标
    cursor = db.cursor()
    # 执行SQL语句
    # cursor.execute("select * from project01")
    cursor.execute(sql)
    # 获取返回结果列表
    data = cursor.fetchall()
    # 关闭数据库链接
    db.close()
    return data


# 解析JSON数据写入数据库
def AddData(dt):
    # print(dt['data'])

    # 要执行的SQL语句
    sql = ''

    for item in dt['data']:
        # 判断当前文章ID在数据库是否存在
        sql1 = 'select COUNT(*) from jrttwenzhanginfo where item_id = \''+str(item['item_id'])+'\';'
        # print(sql1)
        o = comm_DB(sql1)
        # 数据库存在的条数（判断数据是否存在）
        num = o[0][0]

        # print(str(item['image_url']))

        # 不存在就添加
        if  num < 1:
            # 新增
            sql += 'INSERT INTO `jrttwenzhanginfo` (`image_url`,`single_mode`,`abstract`,`image_list`,`more_mode`,`tag`,`tag_url`,`title`,`has_video`,`chinese_tag`,`source`,`group_source`,`comments_count`, `composition`,`media_url`,`go_detail_count`,`middle_mode`,`gallary_image_count`,`detail_play_effective_count`,`visibility`, `source_url`,`item_id`,`article_genre`,`display_url`,`behot_time`,`has_gallery`,`group_id`)  VALUES ('

            if 'image_url' not in item: sql += '\'\',' 
            else: sql += '\''+ str(item['image_url']) + '\','

            if 'single_mode' not in item: sql += '\'\',' 
            else: sql += '\''+ str(item['single_mode']) + '\','

            if 'abstract' not in item: sql += '\'\',' 
            else: sql += '\''+ str(item['abstract']) + '\','

            if 'image_list' not in item: sql += '\'\',' 
            else: sql += '\''+ str(item['image_list']) + '\','

            if 'more_mode' not in item: sql += '\'\',' 
            else: sql += '\''+ str(item['more_mode']) + '\','

            if 'tag' not in item: sql += '\'\',' 
            else: sql += '\''+ str(item['tag']) + '\','

            if 'tag_url' not in item: sql += '\'\',' 
            else: sql += '\''+ str(item['tag_url']) + '\','

            if 'title' not in item: sql += '\'\',' 
            else: sql += '\''+ str(item['title']) + '\','

            if 'has_video' not in item: sql += '\'\',' 
            else: sql += '\''+ str(item['has_video']) + '\','

            if 'chinese_tag' not in item: sql += '\'\',' 
            else: sql += '\''+ str(item['chinese_tag']) + '\','

            if 'source' not in item: sql += '\'\',' 
            else: sql += '\''+ str(item['source']) + '\','

            if 'group_source' not in item: sql += '\'\',' 
            else: sql += '\''+ str(item['group_source']) + '\','

            if 'comments_count' not in item: sql += '\'\',' 
            else: sql += '\''+ str(item['comments_count']) + '\','

            if 'composition' not in item: sql += '\'\',' 
            else: sql += '\''+ str(item['composition']) + '\','

            if 'media_url' not in item: sql += '\'\',' 
            else: sql += '\''+ str(item['media_url']) + '\','

            if 'go_detail_count' not in item: sql += '\'\',' 
            else: sql += '\''+ str(item['go_detail_count']) + '\','

            if 'middle_mode' not in item: sql += '\'\',' 
            else: sql += '\''+ str(item['middle_mode']) + '\','

            if 'gallary_image_count' not in item: sql += '\'\',' 
            else: sql += '\''+ str(item['gallary_image_count']) + '\','

            if 'detail_play_effective_count' not in item: sql += '\'\',' 
            else: sql += '\''+ str(item['detail_play_effective_count']) + '\','

            if 'visibility' not in item: sql += '\'\',' 
            else: sql += '\''+ str(item['visibility']) + '\','

            if 'source_url' not in item: sql += '\'\',' 
            else: sql += '\''+ str(item['source_url']) + '\','

            if 'item_id' not in item: sql += '\'\',' 
            else: sql += '\''+ str(item['item_id']) + '\','

            if 'article_genre' not in item: sql += '\'\',' 
            else: sql += '\''+ str(item['article_genre']) + '\','

            if 'display_url' not in item: sql += '\'\',' 
            else: sql += '\''+ str(item['display_url']) + '\','

            if 'behot_time' not in item: sql += '\'\',' 
            else: sql += '\''+ str(item['behot_time']) + '\','

            if 'has_gallery' not in item: sql += '\'\',' 
            else: sql += '\''+ str(item['has_gallery']) + '\','

            if 'group_id' not in item: sql += '\'\',' 
            else: sql += '\''+ str(item['group_id']) + '\');'


        # print(sql)
    if sql != '':
        comm_DB(sql)
        print("执行结果SQL语句："+str(sql))
        print("执行完成")
    

get_page()




















