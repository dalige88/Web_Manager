$(function () {
    wttlist.pageBind();
    wttlist.loadData();
});

var wttlist = {
    pageBind: function () {
        $('#searchBtn').click(function () {
            wttlist.loadData();
        });
    },
    loadData: function () {
        var Param = {};
        Param.Content = $('#KeyWord').val();
        Param.Status = $('#Status').val();
        url = '/JRTTImages/Ajax_WTTGetList';


        //原片
        if (Param.Status == 0) {
            $("#infoPage").page({
                url: url,
                pageSize: 10,
                searchparam: Param,
                viewCallback: wttlist.yp_outList
            });
        }
        //已发布
        if (Param.Status == 1) {
            $("#infoPage").page({
                url: url,
                pageSize: 10,
                searchparam: Param,
                viewCallback: wttlist.outList
            });
        }

       
    },
    yp_outList: function (result, j) {
        var PYScript = $('#PYScript').val();
        var html = '';
        $.each(result, function (i) {
            var fb = "";
            j++;
            var itemData = result[i];
            html += '<tr>\
                    <td align="center">' + itemData.id + '</td>';

            /*html += '<td align="center"><a href=\'' + itemData.sourceLink + '\'>' + itemData.content + '</a></td>';*/
            if (itemData.sourceLink != "" && itemData.sourceLink != null) {
                html += '<td align="center"><a href=\'' + itemData.sourceLink + '\' target=\'_blank\'>' + itemData.content + '</a></td>';
            } else {
                html += '<td align="center">' + itemData.content + '</td>';
            }

            html += '<td align = "center"> ' + "(" + wttlist.statusFun(itemData.status) + ")" + itemData.createTime + '</td> ';

            if (wttlist.statusFun(itemData.status) == "已发布") {
                html += '<td align="center" style="min-width:200px;">|&nbsp;&nbsp;<a href=\'javascript:wttlist.postJRTT("' + itemData.id + '")\' style="color:rgb(255, 189, 29);">重新发布</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href=\'/JRTTImages/EditWttPage?id=' + itemData.id + '\'>编辑</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href=\'javascript:wttlist.del("' + itemData.id + '")\' style="color:red;" >删除</a>&nbsp;&nbsp;|</td>';
            } else {
                html += '<td align="center" style="min-width:200px;">|&nbsp;&nbsp;<a href=\'javascript:wttlist.postJRTT("' + itemData.id + '")\' style="color:green;">发布短文</a>|&nbsp;&nbsp;<a href=\'/JRTTImages/EditWttPage?id=' + itemData.id + '\'>编辑</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href=\'javascript:wttlist.del("' + itemData.id + '")\' style="color:red;" >删除</a>&nbsp;&nbsp;|</td>';
            }
            html += '</td></tr>';
        });
        $('#goodsbody').html(html);
    },
    outList: function (result, j) {
        var html = '';
        $.each(result, function (i) {
            j++;
            var itemData = result[i];
            html += '<tr>\
                    <td align="center">' + itemData.id + '</td>\
                    <td align="center">' + itemData.content + '</td>\
                    <td align="center">' + wttlist.format(itemData.publish_Time) + '</td >\
                    <td align="center"></td>';

            html += '</td></tr>';
        });
        $('#goodsbody').html(html);
    },
    postJRTT: function (id) {
        var postData = {};
        postData.id = id;
        /*postData.PYScript = $('#PYScript').val();*/
        var url = "/JRTTImages/Ajax_PostWTT";
        ajaxHelper.post(url, postData, function (d) {
            msg.success('操作成功！', function () {
                window.location.reload();
                //window.location.href = "/JRTTImages/ImagesList?pid="+;
                //history.go(-1);
            });
        });
    },
    statusFun: function (status)
    {
        if (status == 0) {
            return "未发布";
        }
        if (status == 1) {
            return "已发布"
        }

    },
    //删除
    del: function (id) {
        var postData = {};
        postData.id = id;
        var url = "/JRTTImages/Ajax_DelWTT";
        ajaxHelper.post(url, postData, function (d) {
            msg.success('操作成功！', function () {
                window.location.reload();
            });
        });
    },
    //时间戳转时间格式 （//2018-06-08 18:00:00）
    format: function (timestamp)
    {
        var date = new Date(timestamp * 1000);//时间戳为10位需*1000，时间戳为13位的话不需乘1000

        var Y = date.getFullYear() + '-';

        var M = (date.getMonth() + 1 < 10 ? '0' + (date.getMonth() + 1) : date.getMonth() + 1) + '-';

        var D = date.getDate() + ' ';

        var h = date.getHours() + ':';

        var m = date.getMinutes() + ':';

        var s = date.getSeconds();

        return Y + M + D + h + m + s;
    },
}




