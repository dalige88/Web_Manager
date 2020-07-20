﻿$(function () {
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
        debugger;
        var Param = {};
        //Param.GoodsTitle = $('#KeyWord').val();
        //Param.ExamineState = $('#ExamineState').val();
        //Param.SaleState = $('#SaleState').val();
        $("#infoPage").page({
            url: '/Platforminfo/GetList',
            pageSize: 10,
            searchparam: Param,
            viewCallback: platformlist.outList
        });
    },
    outList: function (result, j) {
        debugger;
        var html = '';
        $.each(result, function (i) {
            j++;
            debugger;
            var itemData = result[i];
            html += '<tr>\
                    <td align="center">' + itemData.id + '</td>\
                    <td align="center">' + itemData.platformName + '</td>\
                    <td align="center">' + itemData.addressUrl + '</td >\
                    <td align="center">' + tools.nullToEmptyString(itemData.createTime) + '</td>\
                    <td align="center">' + itemData.remark + '</td>\
                    <td align="center"><a href=\'javascript:platformlist.delGoods(' + itemData.id + ');\'>删除</a></td>';
            
            html += '</td></tr>';
        });
        $('#goodsbody').html(html);
    },
    delGoods: function (id) {
        var Param = {};
        Param.id = id;
        var url = "/Goods/Ajax_DelGoods";

        ajaxHelper.post(url, Param, function (d) {
            msg.success('操作成功！', function () {
                platformlist.loadData();
            });
        });
    }
}




