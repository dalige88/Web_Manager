$(function () {
    platformlist.pageBind();
    platformlist.loadData();
});

var platformlist = {
    pageBind: function () {
        $('#searchBtn').click(function () {
            platformlist.loadData();
        });
    },
    loadData: function () {
        var Param = {};
        Param.PlatformName = $('#KeyWord').val();
        $("#infoPage").page({
            url: '/Platforminfo/GetList',
            pageSize: 10,
            searchparam: Param,
            viewCallback: platformlist.outList
        });
    },
    outList: function (result, j) {
        var html = '';
        $.each(result, function (i) {
            j++;
            var itemData = result[i];
            html += '<tr>\
                    <td align="center">' + itemData.id + '</td>\
                    <td align="center">' + itemData.platformName + '</td>\
                    <td align="center">' + itemData.addressUrl + '</td >\
                    <td align="center">' + tools.nullToEmptyString(itemData.createTime) + '</td>\
                    <td align="center">' + itemData.remark + '</td>\
                    <td align="center">'+ authHelper.createLink('/Platforminfo/EditPlatforminfo', 'id=' + itemData.id) + '<a href=\'javascript:platformlist.delPlatform("' + itemData.id + '")\'>删除平台</a></td>';

            html += '</td></tr>';
        });
        $('#goodsbody').html(html);
    },
    delPlatform: function (Id) {
        var url = "/Platforminfo/Ajax_DelPlatforminfo";
        var postData = {};
        postData.Id = Id;
        ajaxHelper.post(url, postData, function (d) {
            msg.success("删除成功！", function () {
                window.location.reload(true);
            });
        });
    }
}




