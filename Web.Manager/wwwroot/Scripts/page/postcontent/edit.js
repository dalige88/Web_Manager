
//实例化编辑器
var um = UM.getEditor('myEditor');
$(function () {
    
    PostContent.createEditor();
    PostContent.loadPlaData();
    PostContent.pageBind();
    //PostContent.insertHtml($('#msgcontent').val());


    //点击图片去触发input file框
    $('#showImg').click(function () {
        $('#ImgUrl').click();
    });
    //鼠标移入改变透明度
    $('#showImg').hover(function () {
        $("#imgspan").css("display", "block");
        $("#showImg").css("opacity", "0.3");
    }, function () {
        //改变img的透明度
        $("#showImg").css("opacity", "0.5");
        $("#imgspan").css("display", "none");
    });
    var $input = $("#ImgUrl");
    // 为input设定change事件
    $input.change(function () {
        // 如果value不为空，调用文件加载方法
        if ($(this).val() != "") {
            fileLoad(this);
        }
    });



});


var PostContent = {
    pageBind: function () {
        
        $('#btn_save').click(function () {
            PostContent.saveInfo();
        });
        $('#btn_post_jrtt').click(function () {
            PostContent.postJRTT();
        });

    }, createEditor: function () {
        um = UM.getEditor('myEditor');
    },

    saveInfo: function () {
        
        var postData = {};
        postData.ID = $('#id').val();
        postData.MsgTitle = $('#MsgTitle').val();
        postData.MsgAuthor = $('#MsgAuthor').val();
        postData.MsgContent = um.getContent();
        postData.MsgType = 0;
        postData.PlatformID = $('#MenuPid').val();
        postData.SubChannelID = $('#MenuSub').val();
        postData.HeadImg = $('#serverFilePath').val();
        postData.HeadImgServer = $('#ImgUrl_hidden').val();
        if ($('.RoleStatus:checked').length > 0) {
            postData.MsgType = $('.RoleStatus:checked').eq(0).val();
        }

        postData.OpenStatus = 0;
        if ($('.OpenStatus:checked').length > 0) {
            postData.OpenStatus = $('.OpenStatus:checked').eq(0).val();
        }


        var url = "/PostContent/Ajax_EditPostcontent";

        ajaxHelper.post(url, postData, function (d) {
            msg.success('操作成功！', function () {
                window.location.href = "/PostContent/Index";
            });
        });

    },



    loadPlaData: function () {
        var postData = {};
        var url = "/PostContent/Ajax_GetAllList";
        ajaxHelper.post(url, postData, function (d) {
            if (d.length > 0) {
                PostContent.loadPlaHtml(d);
            } else {
                $('#MenuPid').html('<option value="">--请选择渠道--</option>');
            }
        });
    },
    loadPlaHtml: function (result) {
        var Pid = $('#pid').val();
        PostContent.loadSubData(Pid);
        var html = '';
        $.each(result, function (i) {
            var itemData = result[i];
            if (Pid == itemData.id) {
                html += '<option value=' + itemData.id + ' selected=\'selected\'> ' + itemData.platformName + ' </option>';
            } else {
                html += '<option value=' + itemData.id + '> ' + itemData.platformName + ' </option>';
            }
            
        });
        $('#MenuPid').html(html);
    },





    loadSubData: function (Pid) {
        var postData = {};
        postData.pid = Pid;
        var url = "/Subchannel/Ajax_GetAllList";
        ajaxHelper.post(url, postData, function (d) {
            if (d.length > 0) {
                PostContent.loadSubHtml(d);
            } else {
                $('#MenuSub').html('<option value="">--请选择渠道--</option>');
            }
        });
    },

    loadSubHtml: function (result) {
        var html = '';
        $.each(result, function (i) {
            var itemData = result[i];
            html += '<option value=' + itemData.id + '> ' + itemData.subChannelName + ' </option>';
        });
        $('#MenuSub').html(html);
    },

    //按钮的操作
    insertHtml: function (value) {
        um.execCommand('insertHtml', value)
    },

    postJRTT: function () {
        var postData = {}; 
        postData.id = $('#id').val();
        postData.PYScript = $('#PYScript').val();
        postData.HeadImg = $('#serverFilePath').val();
        var url = "/PostContent/Ajax_PostJRTTWenZhang";
        ajaxHelper.post(url, postData, function (d) {
            msg.success('操作成功！', function () {
                window.location.href = "/PostContent/Index";
            });
        });





    },
};





//上传文件
function fileLoad(ele) {
    var name = $(ele).val();
    //判断上传文件格式
    var _name, _fileName, personsFile;
    personsFile = document.getElementById("ImgUrl");
    _name = personsFile.value;
    _fileName = _name.substring(_name.lastIndexOf(".") + 1).toLowerCase();
    if (_fileName !== "png" && _fileName !== "jpg" && _fileName !== "jpeg") {
        alert("上传图片格式不正确，请重新上传");
        return;
    }
    //创建一个formData对象
    var formData = new FormData();

    //获取传入元素的val

    //获取files
    var files = $(ele)[0].files[0];
    //将name 和 files 添加到formData中，键值对形式
    formData.append("file", files);
    formData.append("name", name);
    $.ajax({
        url: "/Tool/UploadFile",
        type: 'POST',
        data: formData,
        processData: false,// 告诉jQuery不要去处理发送的数据
        contentType: false, // 告诉jQuery不要去设置Content-Type请求头
        beforeSend: function () {
            document.getElementById("showImg").src = "/Content/images/loading/loadimg.gif";
        },
        success: function (responseStr) {
            document.getElementById("showImg").src = responseStr.data.urlPath;
            document.getElementById("ImgUrl_hidden").value = responseStr.data.urlPath;
            document.getElementById("serverFilePath").value = responseStr.data.serverFilePath;

            //var src = responseStr.data.urlPath;
            //$list.append('<li style="border:solid red px; margin:5px 5px;" class="file-item"><img src="' + src + '" alt="" height="70"><span class="file-del">删除</span></li>').children(':last').hide().fadeIn(2500);
        }
        ,
        error: function (responseStr) {
            alert("系统建议管理(提交单)数据加载错误");
            document.getElementById("showImg").src = "/Content/images/loading/error.jpg";
        }

    });
}


