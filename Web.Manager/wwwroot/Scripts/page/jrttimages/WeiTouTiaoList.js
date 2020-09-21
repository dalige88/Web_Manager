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
                html += '<td align="center"><a href=\'' + itemData.sourceLink + '\'>' + itemData.content + '</a></td>';
            } else {
                html += '<td align="center">' + itemData.content + '</td>';
            }

            html += '<td align = "center"> ' + "(" + wttlist.statusFun(itemData.status) + ")" + itemData.createTime + '</td> ';

            if (wttlist.statusFun(itemData.status) == "已发布") {
                html += '<td align="center">|&nbsp;&nbsp;<a href=\'javascript:wttlist.postJRTT("' + itemData.id + '")\' style="color:green;">重新发布</a>&nbsp;&nbsp;|<br />|&nbsp;&nbsp;<a href=\'/JRTTImages/EditWttPage?id=' + itemData.id +'\'>编辑</a>&nbsp;&nbsp;|</td>';
            } else {
                html += '<td align="center">|&nbsp;&nbsp;<a href=\'javascript:wttlist.postJRTT("' + itemData.id + '")\' style="color:red;">发布微头条</a>|<br />|&nbsp;&nbsp;<a href=\'/JRTTImages/EditWttPage?id=' + itemData.id +'\'>编辑</a>&nbsp;&nbsp;|</td>';
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
                    <td align="center">' + itemData.ugc_U13_Cut_Image_List + '</td >\
                    <td align="center">' + itemData.publish_Time + '</td >\
                    <td align="center">|&nbsp;&nbsp;|</td>';

            html += '</td></tr>';
        });
        $('#goodsbody').html(html);
    },
    postJRTT: function (id) {
        var postData = {};
        postData.id = id;
        postData.PYScript = $('#PYScript').val();
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
}




