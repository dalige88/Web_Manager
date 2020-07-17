$(function () {
    menulist.loadInfo();
    menulist.pageBind();
})


var menulist = {
    pageBind: function () {
        $('#btn_makemenu').click(function () {
            menulist.menuMake();
        });
    },
    loadInfo: function () {
        var postData = {};
        ajaxHelper.post('/WebSystem/MenuLoadList', postData, function (d) {
            var html = '<thead style="display: table-header-group;">\
                    <tr>\
                        <th width="20%">菜单标题</th>\
                        <th width="40%">菜单连接地址</th>\
                        <th width="20%">状态</th>\
                        <th width="20%">操作</th>\
                    </tr>\
                </thead>';
            html += '<tbody>';
            $.each(d, function (i) {
                var itemData = d[i];
                html += '<tr>\
                        <td><span style="display: inline-block; width:0px;"></span><span class="folder-open"></span>' + itemData.menuName + '</td>\
                        <td></td>\
                        <td align="center">'+ (itemData.menuStatus == 0 ? '<span class="label label-danger">禁用</span>' : '<span class="label label-primary">启用</span>') + '</td>\
                        <td align="center">'+ '<a href=\'/WebSystem/DelSubmenu?Id=' + itemData.menuID + '\'>删除</a>&nbsp;&nbsp;' + authHelper.createLink('/WebSystem/MenuEdit', 'menuid=' + itemData.menuID) + '</td>\
                    </tr>';
                $.each(itemData.childMenus, function (j) {
                    var itemCData = itemData.childMenus[j];
                    html += '<tr>\
                        <td><span style="display: inline-block; width:20px;"></span><span class="folder-line"></span><span class="folder-open"></span>' + itemCData.menuName + '</td>\
                        <td>' + itemCData.menuLink + '</td>\
                        <td align="center">'+ (itemCData.menuStatus == 0 ? '<span class="label label-danger">禁用</span>' : '<span class="label label-primary">启用</span>') + '</td>\
                        <td align="center">'+ '<a href=\'javascript:menulist.delMenu("' + itemData.menuID + '")\'>删除</a>&nbsp;&nbsp;' + authHelper.createLink('/WebSystem/MenuEdit', 'menuid=' + itemCData.menuID) + authHelper.createOpenPage('子页面编辑', "/WebSystem/MenuPageList", "menuid=" + itemCData.menuID) + '</td>\
                    </tr>';
                });
            });
            html += '</tbody>';
            $('#menutable').html(html);
        });
    },
    menuMake: function () {
        var url = $("#btn_makemenu").data('url');
        ajaxHelper.post(url, "", function (d) {
            msg.success("更新菜单成功！新增功能" + d, function () {
                if (d > 0)
                    window.location.reload(true);
            });
        });
    },
    delMenu: function (Id) {
        var url = "/WebSystem/DelSubmenu";
        var postData = {};
        postData.Id = Id;
        ajaxHelper.post(url, postData, function (d) {
            msg.success("删除成功！", function () {
                if (d > 0)
                    window.location.reload(true);
            });
        });
    }
};



