(function($){

    $.widget("webui.oemLogModal", {
            options: {
                sections: null
            },
            _create: function(){
                var thiz = this;

                this._ciLogsDialog = this.element.formModal().data("formModal");
                this._oemLogDataGrid = $("#oemLogDataGrid").datagrid({
                    columns: [
                        {title: "分支名称", width: 200, field: "branchName"},
                        {title: "构建版本", width: 100, field: "version"},
                        {title: "状态", width: 80, field: "ciStatus", render: function(row, args){
                            if(args.value == 2){
                                return "完成";
                            }
                            else if(args.value == 3){
                                return "失败";
                            }

                            return "运行中";
                        }
                        },
                        {title: "开始时间", width: 150, field: "startTime"},
                        { title: "结束时间", width: 150, field: "endTime" },
                        {
                            title: "compose下载", width: 200, field: "packagePath", render: function (row, args) {
                                var logId = args.data.id;
                                if (args.data.ciStatus == 2) {
                                    var el = $("<div></div>");
                                    return el.append("<div><a href='/home/downloadComposeFile?logId=" + logId + "&fileName=docker-compose.yml'>下载 docker-compose.yml</a></div>")
                                        .append("<div><a href='/home/downloadComposeFile?logId=" + logId + "&fileName=docker-compose-import.yml'>下载 docker-compose-import.yml</a></div>")
                                        .append("<div><a href='/home/downloadComposeFile?logId=" + logId + "&fileName=docker-compose-region.yml'>下载 docker-compose-region.yml</a></div>");
                                }

                                return "";
                            }
                        }
                    ],
                    textWrap: true,
                    singleSelect: true,
                    showNumberColumn: true,
                    showEmptyMessae: true
                }).data("datagrid");

                this._oemLogDataGridPagger = $("#oemLogDataGridPagger").pagination({
                        count: 0, size: 5, change: function (pager, args) {
                            thiz._oemLogDataGridPagger.setPageInfo(args);
                            $.extend(thiz._searchInfo, args);
                            thiz.search(thiz._searchInfo);
                        }
                    })
                    .data("pagination");
            },
            showCILogs: function(oemInfo){
                var thiz = this;
                this._ciLogsDialog.show();
                this.search({ oemId: oemInfo.id, start: 0, size: 5 });
            },
            search: function(model){
                var thiz = this;
                this._searchInfo = model;

                $.get("home/getOemLogList", this._searchInfo, function (model) {
                    thiz._oemLogDataGrid.setValue(model.oemLogs);
                    thiz._oemLogDataGridPagger.setPageInfo({
                        start: model.start,
                        count: model.total
                    });
                })
            },
            refresh: function () {
                this.search(this._searchInfo);
            }
        }
    );

})(jQuery);
