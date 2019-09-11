(function($){

    $.widget("webui.indexPage", {
            options: {
                sections: null
            },
            _create: function(){
                var thiz = this;

                $("#searchForm").horizontalForm();
                this._searchForm = $("#searchForm").form().data("form");
                
                this._oemGrid = $("#datagrid").datagrid({
                    columns: [
                        {
                            title: "投资人", width: 150, field: "investorName"
                        },
                        {
                            title: "收益日期", width: 150, field: "incomeDate", render: $.webui.__renders.date
                        },
                        {
                            title: "投资账期", width: 150, field: "invertmentName"
                        },
                        {
                            title: "静态收益", width: 150, field: "fixIncomeMoney", render: $.webui.__renders.money
                        },
                        {
                            title: "当日成交单数", width: 150, field: "toadyQuantity"
                        },
                        {
                            title: "总成交单数", width: 150, field: "totalQuantity"
                        },
                        {
                            title: "服务收益", width: 150, field: "orderIncomeMoney", render: $.webui.__renders.money
                        },
                        {
                            title: "总收益", width: 150, field: "totalIncomeMoney", render: $.webui.__renders.money
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

                $("#btnSearch").click(function () {
                    thiz._searchInfo = thiz._searchForm.getValue();
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
                $.get("home/getIncomeList", model, function (model) {
                    thiz._oemGrid.setValue(model.incomes);
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
