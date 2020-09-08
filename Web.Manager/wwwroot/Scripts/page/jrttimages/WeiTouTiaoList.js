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
        Param.Pid = $('#pid').val();
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
            j++;
            var itemData = result[i];
            html += '<tr>\
                    <td align="center">' + itemData.id + '</td>\
                    <td align="center">' + itemData.content + '</td>\
                    <td align="center">' + itemData.createTime + '</td>\
                    <td align="center">|&nbsp;&nbsp;<a href=\'javascript:wttlist.postJRTT("' + itemData.id + '")\'>发布微头条到平台</a>|</td>';

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
                //window.location.href = "/JRTTImages/ImagesList?pid="+;
                history.go(-1);
            });
        });
    },
}




