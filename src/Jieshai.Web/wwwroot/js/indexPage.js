(function($){

    $.widget("webui.indexPage", {
            options: {
                sections: null
            },
            _create: function(){
                var thiz = this;

                $("#oemCIForm").horizontalForm();
                this._oemCIForm = $("#oemCIForm").form().data("form");
                
                this._oemGrid = $("#datagrid").datagrid({
                    columns: [
                        {
                            title: "代理商", width: "100%", field: "companyFullNameZhcn", render: function (row, args) {
                                return "<a href='/home/edit?id=" + args.data.id + "'>" + args.value + "</a>";
                            }
                        },
                        {
                            title: "状态", width: 80, field: "ciStatus", render: function (row, args) {
                                if (args.value == 1) {
                                    return "运行中";
                                }
                                else if (args.value == 2) {
                                    return "完成";
                                }
                                else if (args.value == 3) {
                                    return "失败";
                                }

                                return "";
                            }
                        },
                        {
                            title: "操作", width: 250, field: "id", render: function (row, args) {
                                var el = $("<div ></div>");
                                var activeCIEl = $("<a href='#' class='activeCI' style='margin-right: 10px;display:inline-block'>启动CI</a>")
                                    .click(function () {
                                        thiz.activeCI(args.data);
                                        return false;
                                    });

                                var editEl = $("<a href='#' style='margin-right: 10px;display:inline-block'>编辑</a>")
                                    .attr("href", "/home/edit?id=" + args.data.id);

                                var deleteEl = $("<a href='#' style='margin-right: 10px;display:inline-block'>删除</a>")
                                    .click(function () {
                                        thiz.deleteOem(args.data);
                                        return false;
                                    });
                                
                                var oemLogEl = $("<a href='#' class='oem-log'>oem日志</a>")
                                    .click(function () {
                                        thiz._ciLogsDialog.showCILogs(args.data);
                                        return false;
                                    });

                                el.append(activeCIEl)
                                    .append(editEl)
                                    .append(deleteEl)
                                    .append(oemLogEl)

                                return el;
                            }
                        }
                    ],
                    singleSelect: true,
                    showNumberColumn: true,
                    showEmptyMessae: true
                }).data("datagrid");

                this._datagridPagger = $("#datagridPagger").pagination({
                        count: 0, size: 20, change: function (pager, args) {
                            thiz._datagridPagger.setPageInfo(args);
                            $.extend(thiz._searchInfo, args);
                            thiz.search(args);
                        }
                    })
                    .data("pagination");

                $("#btnSearch").click(function(){
                    thiz._searchInfo.keyword = $("#txtKeyword").val();
                    thiz._searchInfo.start = 0;

                    thiz.search(thiz._searchInfo);

                    return false;
                });
                
                $("#btnActiveCI").click(function () {
                    var oemInfo = thiz._oemInfo;
                    var oemCIValue = thiz._oemCIForm.getValue();
                    oemCIValue.oemId = oemInfo.id;
                    $("#btnActiveCI").text("运行中....").prop("disabled", true);
                    $.post("home/ActiveCI", oemCIValue, function (model) {
                        if (model.result) {
                            thiz._activeCIDialog.hide();
                            $.messageBox.success(model.message);
                        }
                        else {
                            $.messageBox.error(model.message);
                        }
                        $("#btnActiveCI").text("确定").prop("disabled", false);
                    })
                })

                this._activeCIDialog = $("#activeCIDialog").formModal().data("formModal");
                this._ciLogsDialog = $("#ciLogsDialog").oemLogModal().data("oemLogModal");

                this.search({start: 0, size: 20});
            },
            search: function(model){
                var thiz = this;
                this._searchInfo = model;
                $.get("home/getOemList", model, function (model) {
                    thiz._oemGrid.setValue(model.oems);
                    thiz._datagridPagger.setPageInfo({
                        start: model.start,
                        count: model.total
                    });
                })
            },
            activeCI: function(oemInfo){
                this._activeCIDialog.show();
                this._oemInfo = oemInfo;
                return;
            },
            deleteOem:function(oemInfo) {
                var thiz = this;
                if (confirm("确定删除吗？")) {
                    $.post("home/DeleteOem", { id: oemInfo.id }, function (model) {
                        if (model.result) {
                            thiz.search({ start: 0, size: 20 });
                        }
                        else {
                            $.messageBox.error(model.message);
                        }
                    })
                }
            }
        }
    );

})(jQuery);
