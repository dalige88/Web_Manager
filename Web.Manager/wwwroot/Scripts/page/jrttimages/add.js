﻿
$(function () {
    JRTTImages.pageBind();
    JRTTImages.loadSubData();

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

var JRTTImages = {
    pageBind: function () {

        $('#btn_save').click(function () {
            JRTTImages.saveInfo();
        });

    },

    saveInfo: function () {
        var pics = "";
        var pic_url = document.getElementsByName("pic_url");
        for (var i = 0; i < pic_url.length; i++) {

            if (i < pic_url.length - 1) {
                pics += pic_url[i].value + ",";
            } else {
                pics += pic_url[i].value;
            }
        }
        //alert(pics);
        //选中的渠道信息
        var qd = "";
        $('input[class="qudao"]:checked').each(function () {
            qd += $(this).val() + ',';
        });
        //将字符串中最后一个元素","逗号去掉，
        qd = qd.substring(0, qd.lastIndexOf(','));


        var postData = {};
        postData.Pid = qd;
        postData.Content = $('#content').val();
        postData.Images = pics;

        var url = "/JRTTImages/Ajax_AddWTT";

        ajaxHelper.post(url, postData, function (d) {
            msg.success('操作成功！', function () {
                history.go(-1);
            });
        });

    },
    delepic: function (picid) {
        var rowmb15_img_ = "rowmb15_img_" + picid;
        var rowmb15_fwqdz_ = "rowmb15_fwqdz_" + picid;
        var box = document.getElementById(rowmb15_img_);
        box.remove();
        var box1 = document.getElementById(rowmb15_fwqdz_);
        box1.remove();
    },
    calculation: function () {
        var nMax = 2000;
        var textDom = document.getElementById("content");
        var len = textDom.value.length;
        if (len > nMax) {
            textDom.value = textDom.value.substring(0, nMax);
            return;
        }
        document.getElementById("messages").innerText = "你还可以输入" + (nMax - len) + "个字";
    },


    loadSubData: function () {
        var postData = {};
        var url = "/Subchannel/Ajax_GetAllList";
        ajaxHelper.post(url, postData, function (d) {
            console.log(d);
            if (d.length > 0) {
                JRTTImages.loadSubHtml(d);
            } else {
                $('#MenuSub').html('<option value="">--请选择渠道--</option>');
            }
        });
    },

    loadSubHtml: function (result) {
        var htmls = '';
        //var html = '';
        $.each(result, function (i) {
            var itemData = result[i];

            //html += '<option value=' + itemData.id + '> ' + itemData.subChannelName + ' </option>';
            htmls += '<label style=\"font-weight:bold;\"><input type=\"checkbox\" value=\"' + itemData.id + '\" class=\"qudao\" />' + itemData.subChannelName + '</label>&nbsp;&nbsp;&nbsp;&nbsp;';

        });
        //$('#MenuSub').html(html);
        $('#qudao').html(htmls);
    },
};


var numimg = 0;
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
            /*document.getElementById("ImgUrl_hidden_img").src = responseStr.data.urlPath;*/


            numimg++;

            var rowmb15_img_ = "rowmb15_img_" + numimg;
            var rowmb15_fwqdz_ = "rowmb15_fwqdz_" + numimg;

            var Imgids = "ImgUrl_hidden_img_" + numimg;
            var html = document.getElementById("showui_li").innerHTML;
            html += "<div class=\"row rowmb15\" id=\"" + rowmb15_img_ + "\"><div class=\"col-md-12\"><label class=\"col-sm-2 edit-group-label\"><span class=\"spanrequired\">*</span>图片" + numimg + "：</label><div class=\"col-md-7\" style=\"width:200px;\" ><div class=\"showdiv\"><img class=\"left\" src=\"/Scripts/page/jrttimages/img/Arrow_left.png\"><img class=\"center\" src=\"/Scripts/page/jrttimages/img/delete.png\" onclick=\"JRTTImages.delepic('" + numimg + "')\" ><img class=\"right\" src=\"/Scripts/page/jrttimages/img/Arrow_right.png\"></div ><img id=\"" + Imgids + "\" name=\"" + Imgids + "\" class=\"showimg\" style=\"width:200px;\" src=\"" + responseStr.data.urlPath + "\"></div></div></div></div>";



            document.getElementById("showui_li").innerHTML = html;


            var serve_img = document.getElementById("serve_URL").innerHTML;
            serve_img += "<div class=\"row rowmb15\" id=\"" + rowmb15_fwqdz_ + "\"><div class=\"col-md-12\"><label class=\"col-sm-3 edit-group-label\">服务器图片" + numimg + "：</label><div class=\"col-sm-5\"><textarea class=\"form-control\" name=\"pic_url\" style=\"height: 100px; \" disabled>" + responseStr.data.serverFilePath + "</textarea></div></ br>";
            //alert(html);
            document.getElementById("serve_URL").innerHTML = serve_img;

        }
        ,
        error: function (responseStr) {
            alert("系统建议管理(提交单)数据加载错误");
            document.getElementById("showImg").src = "/Content/images/loading/error.jpg";
        }

    });
}


