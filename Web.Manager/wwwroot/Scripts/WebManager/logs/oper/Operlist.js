$(function () {
    accountlist.pageBind();
    accountlist.loadData();
});

var accountlist = {
    pageBind: function () {
        $('#searchBtn').click(function () {
            accountlist.loadData();
        });
    },
    loadData: function () {
        var Param = {};
        Param.UserName = $('#KeyWord').val();
        $("#infoPage").page({
            url:'/WebSysLog/GetOperLogsList',
            pageSize: 20,
            searchparam: Param,
            viewCallback: accountlist.outList
        });
    },
    outList: function (result, j) {
        var html = '';
        $.each(result, function (i) {
            j++;
            var itemData = result[i];
            html += '<tr>\
                    <td align="center">' + itemData.id + '</td>\
                    <td align="center"><div class="autocut" title="' + itemData.url + '">' + tools.nullToEmptyString(itemData.url) + '</div></td>\
                    <td align="center"><div onclick="msg.alert($(this).html())" class="autocut" title="' + itemData.param + '">' + tools.nullToEmptyString(itemData.param) + '</div></td>\
                    <td align="center"><div onclick="msg.alert($(this).html())" class="autocut" title=\'' + itemData.userInfo + '\'>' + tools.nullToEmptyString(itemData.userInfo) + '</div></td>\
                    <td align="center">' + tools.formatTime(itemData.createTime) + '</td>\
                    <td align="center">' + tools.nullToEmptyString(itemData.ip) + '</td>\
                </tr>';
        });
        $('#accountbody').html(html);
    }
}