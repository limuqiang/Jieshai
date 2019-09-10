(function($){

    $.widget("webui.oemForm", {
            options: {
                sections: null
            },
            _create: function(){
                var thiz = this;
                
                this.element.horizontalForm();
                this._form = this.element.form().data("form");
                
            },
            getValue: function(){
                return this._form.getValue();
            },
            setValue: function(value){
                this._form.setValue(value);
            },
            validate: function(){
                return this._form.validate();
            }
        }
    );

})(jQuery);

(function($){

    $.widget("webui.imageUpload", $.webui.input, {
            options: {
                name: null
            },
            _create: function(){
                var name = this.element.attr("name");
                var accept = this.element.attr("accept");
                if(!accept){
                    accept = "image/png";
                }
                var imageWidth = this.element.data("imageWidth");
                var imageHeight = this.element.data("imageHeight");

                var thiz = this;
                var uploadEl = $('<div style="margin-bottom: 10px;">' +
                    '<img />' +
                '</div>' +
                '<span class="btn btn-default webui-file-button">' +
                    '<span>选择文件</span><input type="file" data-sequential-uploads="true"/>' +
                '</span>');
                uploadEl.find("input").attr("accept", accept)
                this._image = uploadEl.find("img");
                if(imageWidth && imageHeight){
                    this._image.css({
                        width: imageWidth + "px",
                        height: imageHeight + "px",
                    })
                }
                this.element.append(uploadEl);
                this.element.fileupload({
                    dropZone: null,
                    url: "ParseImage",
                    add: function(args, data){
                        var jqXHR = data.submit()
                            .success(function (model, textStatus, jqXHR) {
                                thiz.setValue(model.imgBase64);
                            })
                            .error(function (jqXHR, textStatus, errorThrown) {
                                $.messageBox.error(textStatus);
                            });
                    }
                });
                this._initOptions();
            },
            setValue: function(value){
                this._image.attr("src", "data:image/png;base64," +value);
                this._value = value;
            },
            getValue: function(){
                return this._value;
            },
        }
    );

})(jQuery);
(function($){

    $.widget("webui.fileUpload", $.webui.input, {
            options: {
                name: null
            },
            _create: function(){
                var name = this.element.attr("name");
                var accept = this.element.attr("accept");

                var thiz = this;
                var uploadEl = $('<div>' +
                    '<span class="btn btn-default webui-file-button">' +
                        '<span>选择文件</span><input type="file" data-sequential-uploads="true"/>' +
                    '</span>' +
                    '<div class="fileName"></div>'+
                '</div>');
                uploadEl.find("input").attr("accept", accept);
                this._fileNameSpan = uploadEl.find(".fileName");
                this.element.append(uploadEl);
                this.element.fileupload({
                    url: "ParseImage",
                    add: function(args, data){
                        var jqXHR = data.submit()
                            .success(function (model, textStatus, jqXHR) {
                                thiz._fileNameSpan.text("上传成功")
                                thiz.setValue(model.imgBase64);
                            })
                            .error(function (jqXHR, textStatus, errorThrown) {
                                $.messageBox.error(textStatus);
                            });
                    }
                });
                this._initOptions();
            },
            setValue: function(value){
                this._value = value;
            },
            getValue: function(){
                return this._value;
            },
        }
    );

})(jQuery);

function  programKeywordValidator(input, value){
    var result = false;
    result = /^[0-9a-zA-Z]+$/.test(value);
    if(result){
        input.hidePopMessage();
    }
    else{
        input.popMessage("格式错误只能为英文和数字");
    }
    return result;
}