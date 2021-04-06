using Human_Resource_Management_Libraly.BaseModel;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Human_Resource_Management_Libraly.Helper
{
    public static class HTMLHelper
    {
        public static IHtmlContent GeneratePagingFooter(this IHtmlHelper htmlHelper, int totalPage, int currentPage, int itemsPerPageingFooter, string cssClass, Func<int, string> pageUrl)
        {
            var sb = new StringBuilder();
            sb.Append("<nav aria-label='Page navigation example'>");
            sb.Append("<ul class='pagination nav-tabs-scroll is-scrollable' style='float:right'>");
            if (currentPage == 1)
            {
                sb.Append("<li class='page-item mr-2 previous disabled' aria-controls='dynamic-table' tabindex='0' id='dynamic-table_first'><a href='javascript:void(0);' class='h-5 pt-25 page-link rounded-0 border-0 mr-2 text-95' rel='nofollow'>Đầu</a></li>");
                sb.Append("<li class='page-item mr-2 previous disabled' aria-controls='dynamic-table' tabindex='0' id='dynamic-table_previous'><a href='javascript:void(0);' class='h-5 pt-25 page-link rounded-0 border-0 mr-2 text-95' rel='nofollow'>Trước</a></li>");
            }
            else
            {
                sb.Append("<li class='page-item mr-2 previous' aria-controls='dynamic-table' tabindex='0' id='dynamic-table_first'><a href='" + pageUrl(1) + "' class='h-5 pt-25 page-link rounded-0 border-0 mr-2 text-95' >Đầu</a></li>");
                sb.Append("<li class='page-item mr-2 previous' aria-controls='dynamic-table' tabindex='0' id='dynamic-table_previous'><a href='" + pageUrl(currentPage - 1) + "' class='h-5 pt-25 page-link rounded-0 border-0 mr-2 text-95' >Trước</a></li>");
            }

            //gọi vào các hàm phân trang trong controller 
            const int pageHold = 10;
            var totalHold = totalPage / pageHold + 1;
            var currentHold = currentPage / pageHold >= 1 && currentPage % pageHold >= 1 ?
                currentPage / pageHold + 1 : currentPage / pageHold;
            currentHold = currentHold == 0 ? 1 : currentHold;

            var pointStart = 1;

            if (currentPage / pageHold >= 1 && currentPage % pageHold >= 1)
                pointStart = currentPage / pageHold * pageHold + 1;
            else if (currentPage / pageHold > 0)
                pointStart = (currentPage / pageHold - 1) * pageHold + 1;

            if (currentHold == 1)
            {
                //sb.Append("<div class='t-numeric'>");
                for (var i = pointStart; i <= ((totalPage < pageHold) ? totalPage : pointStart + pageHold - 1); i++)
                {
                    if (i == currentPage)
                    {
                        sb.AppendFormat("<li class='page-item mr-2 active' aria-controls='dynamic-table' tabindex='0'><a href='javascript:void(0);' class='w-5 h-5 pt-2 page-link text-center rounded-circle'>{0}</a></li>", i);
                    }
                    else
                    {
                        sb.AppendFormat("<li class='page-item mr-2' aria-controls='dynamic-table' tabindex='0'><a href='" + pageUrl(i) + "' class='w-5 h-5 pt-2 page-link text-center rounded-circle'>{0}</a></li>", i);
                    }
                }
                if (totalHold > 1)
                    sb.Append("<li class='page-item mr-2' aria-controls='dynamic-table' tabindex='0'><a href='" + pageUrl(pageHold * currentHold + 1) + "' class='w-5 h-5 pt-2 page-link text-center rounded-circle'>...</a></li>");
                //sb.Append("</div>");
            }
            else if (currentHold == totalHold)
            {
                //sb.Append("<div class='t-numeric'>");
                sb.Append("<li class='page-item mr-2' aria-controls='dynamic-table' tabindex='0'><a href='" + pageUrl(pageHold * (currentHold - 1)) + "' class='w-5 h-5 pt-2 page-link text-center rounded-circle'>...</a></li>");
                for (var i = pointStart; i <= totalPage; i++)
                {
                    if (i == currentPage)
                    {
                        sb.AppendFormat("<li class='page-item mr-2 active' aria-controls='dynamic-table' tabindex='0'><a href='javascript:void(0);' class='w-5 h-5 pt-2 page-link text-center rounded-circle'>{0}</a></li>", i);
                    }
                    else
                    {
                        sb.AppendFormat("<li class='page-item mr-2' aria-controls='dynamic-table' tabindex='0'><a href='" + pageUrl(i) + "' class='w-5 h-5 pt-2 page-link text-center rounded-circle'>{0}</a></li>", i);
                    }
                }
                //sb.Append("</div>");
            }
            else
            {
                //sb.Append("<div class='t-numeric'>");
                sb.Append("<li class='page-item mr-2' aria-controls='dynamic-table' tabindex='0'><a href='" + pageUrl(pageHold * (currentHold - 1)) + "' class='w-5 h-5 pt-2 page-link text-center rounded-circle'>...</a></li>");
                for (var i = pointStart; i <= pointStart + pageHold - 1; i++)
                {
                    if (i == currentPage)
                    {
                        sb.AppendFormat("<li class='page-item mr-2 active' aria-controls='dynamic-table' tabindex='0'><a href='javascript:void(0);' class='w-5 h-5 pt-2 page-link text-center rounded-circle'>{0}</a></li>", i);
                    }
                    else
                    {
                        sb.AppendFormat("<li class='page-item mr-2' aria-controls='dynamic-table' tabindex='0'><a href='" + pageUrl(i) + "'>{0}</a></li>", i);
                    }
                }
                sb.Append("<li class='page-item mr-2' aria-controls='dynamic-table' tabindex='0'><a href='" + pageUrl(pageHold * currentHold + 1) + "'>...</a></li>");
                //sb.Append("</div>");
            }

            if (currentPage == totalPage)
            {
                sb.Append("<li class='page-item mr-2 next disabled' aria-controls='dynamic-table' tabindex='0' id='dynamic-table_next'><a href='javascript:void(0);' class='h-5 pt-25 page-link rounded-0 border-0 mr-2 text-95' rel='nofollow'>Tiếp</a></li>");
                sb.Append("<li class='page-item mr-2 next disabled' aria-controls='dynamic-table' tabindex='0' id='dynamic-table_last'><a href='javascript:void(0);' class='h-5 pt-25 page-link rounded-0 border-0 mr-2 text-95' rel='nofollow'>Cuối</a></li>");
            }
            else
            {
                sb.Append("<li class='page-item mr-2 next' aria-controls='dynamic-table' tabindex='0' id='dynamic-table_next'><a href='" + pageUrl(currentPage + 1) + "' class='h-5 pt-25 page-link rounded-0 border-0 mr-2 text-95'>Tiếp</a></li>");
                sb.Append("<li class='page-item mr-2 next' aria-controls='dynamic-table' tabindex='0' id='dynamic-table_last'><a href='" + pageUrl(totalPage) + "' class='h-5 pt-25 page-link rounded-0 border-0 mr-2 text-95'>Cuối</a></li");
            }

            sb.Append("</ul>");
            sb.Append("</nav>");
            //sb.Append("</div>");
            //sb.Append("<div class='t-pager-size'><div class='t-pager-size-chosen'>10</div><ul>" +
            //          "<li>10</li>" +
            //          "<li>15</li>" +
            //          "<li>20</li>" +
            //          "<li>50</li>" +
            //          "<li>100</li>" +
            //          "</ul>" +
            //          "<div class='sprite t-icon-arrow-bottom'></div></div>");
            //sb.Append("</div>");

            return new HtmlString(sb.ToString());
        }

        public static IHtmlContent GeneratePagingFooterBootstrap(this IHtmlHelper htmlHelper, int totalPage, int currentPage, int itemsPerPageingFooter, int totalItem, string cssClass, Func<int, string> pageUrl)
        {
            var sb = new StringBuilder();
            sb.Append("<div class='fixed-table-pagination'>");
            sb.Append("<div class='float-left pagination-detail'>");

            sb.Append("<span class='pagination-info'>");
            sb.Append(string.Format("Showing {0} to {1} of {2} rows", (currentPage - 1) * itemsPerPageingFooter + 1, (currentPage - 1) * itemsPerPageingFooter + itemsPerPageingFooter, totalItem));
            sb.Append("</span>");

            sb.Append("</div>");

            sb.Append("<div class='float-right pagination'>");
            sb.Append("<ul class='pagination nav-tabs-scroll is-scrollable'>");
            if (currentPage == 1)
            {
                sb.Append("<li class='page-item mr-2 previous disabled' aria-controls='dynamic-table' tabindex='0' id='dynamic-table_first'><a href='javascript:void(0);' class='h-5 pt-25 page-link rounded-0 border-0 mr-2 text-95' rel='nofollow'>Đầu</a></li>");
                sb.Append("<li class='page-item mr-2 previous disabled' aria-controls='dynamic-table' tabindex='0' id='dynamic-table_previous'><a href='javascript:void(0);' class='h-5 pt-25 page-link rounded-0 border-0 mr-2 text-95' rel='nofollow'>Trước</a></li>");
            }
            else
            {
                sb.Append("<li class='page-item mr-2 previous' aria-controls='dynamic-table' tabindex='0' id='dynamic-table_first'><a href='" + pageUrl(1) + "' class='h-5 pt-25 page-link rounded-0 border-0 mr-2 text-95' >Đầu</a></li>");
                sb.Append("<li class='page-item mr-2 previous' aria-controls='dynamic-table' tabindex='0' id='dynamic-table_previous'><a href='" + pageUrl(currentPage - 1) + "' class='h-5 pt-25 page-link rounded-0 border-0 mr-2 text-95' >Trước</a></li>");
            }

            const int pageHold = 10;
            var totalHold = totalPage / pageHold + 1;
            var currentHold = currentPage / pageHold >= 1 && currentPage % pageHold >= 1 ?
                currentPage / pageHold + 1 : currentPage / pageHold;
            currentHold = currentHold == 0 ? 1 : currentHold;

            var pointStart = 1;

            if (currentPage / pageHold >= 1 && currentPage % pageHold >= 1)
                pointStart = currentPage / pageHold * pageHold + 1;
            else if (currentPage / pageHold > 0)
                pointStart = (currentPage / pageHold - 1) * pageHold + 1;

            if (currentHold == 1)
            {
                //sb.Append("<div class='t-numeric'>");
                for (var i = pointStart; i <= ((totalPage < pageHold) ? totalPage : pointStart + pageHold - 1); i++)
                {
                    if (i == currentPage)
                    {
                        sb.AppendFormat("<li class='page-item mr-2 active' aria-controls='dynamic-table' tabindex='0'><a href='javascript:void(0);' class='w-5 h-5 pt-2 page-link text-center rounded-circle'>{0}</a></li>", i);
                    }
                    else
                    {
                        sb.AppendFormat("<li class='page-item mr-2' aria-controls='dynamic-table' tabindex='0'><a href='" + pageUrl(i) + "' class='w-5 h-5 pt-2 page-link text-center rounded-circle'>{0}</a></li>", i);
                    }
                }
                if (totalHold > 1)
                    sb.Append("<li class='page-item mr-2' aria-controls='dynamic-table' tabindex='0'><a href='" + pageUrl(pageHold * currentHold + 1) + "' class='w-5 h-5 pt-2 page-link text-center rounded-circle'>...</a></li>");
                //sb.Append("</div>");
            }
            else if (currentHold == totalHold)
            {
                //sb.Append("<div class='t-numeric'>");
                sb.Append("<li class='page-item mr-2' aria-controls='dynamic-table' tabindex='0'><a href='" + pageUrl(pageHold * (currentHold - 1)) + "' class='w-5 h-5 pt-2 page-link text-center rounded-circle'>...</a></li>");
                for (var i = pointStart; i <= totalPage; i++)
                {
                    if (i == currentPage)
                    {
                        sb.AppendFormat("<li class='page-item mr-2 active' aria-controls='dynamic-table' tabindex='0'><a href='javascript:void(0);' class='w-5 h-5 pt-2 page-link text-center rounded-circle'>{0}</a></li>", i);
                    }
                    else
                    {
                        sb.AppendFormat("<li class='page-item mr-2' aria-controls='dynamic-table' tabindex='0'><a href='" + pageUrl(i) + "' class='w-5 h-5 pt-2 page-link text-center rounded-circle'>{0}</a></li>", i);
                    }
                }
                //sb.Append("</div>");
            }
            else
            {
                //sb.Append("<div class='t-numeric'>");
                sb.Append("<li class='page-item mr-2' aria-controls='dynamic-table' tabindex='0'><a href='" + pageUrl(pageHold * (currentHold - 1)) + "' class='w-5 h-5 pt-2 page-link text-center rounded-circle'>...</a></li>");
                for (var i = pointStart; i <= pointStart + pageHold - 1; i++)
                {
                    if (i == currentPage)
                    {
                        sb.AppendFormat("<li class='page-item mr-2 active' aria-controls='dynamic-table' tabindex='0'><a href='javascript:void(0);' class='w-5 h-5 pt-2 page-link text-center rounded-circle'>{0}</a></li>", i);
                    }
                    else
                    {
                        sb.AppendFormat("<li class='page-item mr-2' aria-controls='dynamic-table' tabindex='0'><a href='" + pageUrl(i) + "'>{0}</a></li>", i);
                    }
                }
                sb.Append("<li class='page-item mr-2' aria-controls='dynamic-table' tabindex='0'><a href='" + pageUrl(pageHold * currentHold + 1) + "'>...</a></li>");
                //sb.Append("</div>");
            }

            if (currentPage == totalPage)
            {
                sb.Append("<li class='page-item mr-2 next disabled' aria-controls='dynamic-table' tabindex='0' id='dynamic-table_next'><a href='javascript:void(0);' class='h-5 pt-25 page-link rounded-0 border-0 mr-2 text-95' rel='nofollow'>Tiếp</a></li>");
                sb.Append("<li class='page-item mr-2 next disabled' aria-controls='dynamic-table' tabindex='0' id='dynamic-table_last'><a href='javascript:void(0);' class='h-5 pt-25 page-link rounded-0 border-0 mr-2 text-95' rel='nofollow'>Cuối</a></li>");
            }
            else
            {
                sb.Append("<li class='page-item mr-2 next' aria-controls='dynamic-table' tabindex='0' id='dynamic-table_next'><a href='" + pageUrl(currentPage + 1) + "' class='h-5 pt-25 page-link rounded-0 border-0 mr-2 text-95'>Tiếp</a></li>");
                sb.Append("<li class='page-item mr-2 next' aria-controls='dynamic-table' tabindex='0' id='dynamic-table_last'><a href='" + pageUrl(totalPage) + "' class='h-5 pt-25 page-link rounded-0 border-0 mr-2 text-95'>Cuối</a></li");
            }

            sb.Append("</ul>");
            sb.Append("</div>");

            sb.Append("</div>");

            return new HtmlString(sb.ToString());
        }

        public static IHtmlContent GenerateBootstrapTable(this IHtmlHelper htmlHelper, TableModel model, Func<int, string> pageUrl)
        {
            var sb = new StringBuilder();
            sb.Append("<div id='tableBootstrap' class='bootstrap-table bootstrap4'>");

            sb.Append(string.Format("<form action='{0}' method='get'>", model.UrlFormSubmit));

            sb.Append("<input name='export' type='hidden' value='0'/>");

            sb.Append(string.Format("<input name='key' type='hidden' value='{0}'/>", model.KeySearch));
            sb.Append(string.Format("<input name='sort' type='hidden' value='{0}'/>", model.Sort));
            sb.Append(string.Format("<input name='pagesize' type='hidden' value='{0}'/>", model.PageSize));
            sb.Append(string.Format("<input name='more' type='hidden' value='{0}'/>", model.MoreSearch));
            sb.Append(string.Format("<input name='time' type='hidden' value='{0}'/>", model.TimeSearch));
            sb.Append(string.Format("<input name='range' type='hidden' value='{0}'/>", model.RangeSearch));
            sb.Append(string.Format("<input name='autocomplete' type='hidden' value='{0}'/>", model.AutoCompleteSearch));

            sb.Append("</form>");

            //toolbar
            GenerateBootstrapTable_Toolbar(sb, model);

            //container
            GenerateBootstrapTable_Container(sb, model);

            //pagination
            GenerateBootstrapTable_Pagination(sb, model, pageUrl);

            sb.Append("</div>");

            //modalSearch
            GenerateBootstrapTable_ModalSearch(sb, model);

            return new HtmlString(sb.ToString());
        }

        private static void GenerateBootstrapTable_Toolbar(StringBuilder sb, TableModel model)
        {
            #region toolbar

            sb.Append("<div class='fixed-table-toolbar'>");

            sb.Append("<div class='bs-bars float-left'>");

            sb.Append("<div id='table-toolbar'>");

            // MultiDelete
            if (model.AuthAction.MultiDelete_Auth == 1)
            {
                if (model.IsDisplayMultiDeleteButton)
                {
                    sb.Append(string.Format("<button {0} autocomplete='off' id='remove-btn' class='btn btn-xs py-2 px-25 mr-3 bgc-white btn-outline-warning btn-h-outline-danger btn-a-outline-danger radius-round' idata='{1}' idata1='{2}' idata2='{3}' type='button' title='Delete'><i class='fa fa-trash text-125'></i></button>", model.SelectedIds.Any() ? "" : "disabled=''", model.ControllerName, model.UrlMultiDelete, model.UrlReloadPage));
                }
            }

            sb.Append("</div>");

            sb.Append("</div>");

            sb.Append("<div class='columns columns-right btn-group float-right'>");

            // FullScreen button
            if (model.IsFullScreen)
            {
                sb.Append("<button class='btn btn-outline-default btn-smd bgc-white btn-h-light-primary btn-a-outline-primary py-1' type='button' name='fullscreen' aria-label='Fullscreen' title='Fullscreen'><i class='fa fa fa-expand'></i></button>");
            }

            //Đặc biệt với account
            if (model.ControllerName == "Account")
            {
                sb.Append("<div class='btn-group'>");

                sb.Append(string.Format("<button {0} autocomplete='off' id='roleupdate-btn' class='btn btn-outline-default btn-smd bgc-white btn-h-light-primary btn-a-outline-primary py-1' data-toggle='modal' title='RoleConfigs' data-target='#modalRole' idata='{1}' idata1='{2}' idata2='{3}'> <i class='fa fa-users-cog text-pink-d1'></i> </button>", model.SelectedIds.Any() ? "" : "disabled=''", model.ControllerName, "/Account/UpdateRoles", model.UrlReloadPage));

                sb.Append("</div>");
            }

            //Tìm kiếm nâng cao
            if (model.IsAdvanceSearch)
            {
                sb.Append("<div class='btn-group'>");

                sb.Append("<button class='btn btn-outline-default btn-smd bgc-white btn-h-light-primary btn-a-outline-primary py-1' data-toggle='modal' title='Filter' data-target='#modalSearch'> <i class='fa fa-filter text-info-d1'></i> </button>");

                sb.Append("</div>");
            }

            // Columns button (chọn cột hiển thị)
            sb.Append("<div class='btn-group dropdown'>");

            sb.Append("<button class='btn btn-outline-default btn-smd bgc-white btn-h-light-primary btn-a-outline-primary py-1 dropdown-toggle' data-toggle='dropdown' aria-haspopup='true' aria-expanded='false' title='Columns'> <i class='fa fa-th-list text-orange-d1'></i> </button>");

            sb.Append("<div class='dropdown-menu dropdown-menu-right'>");

            foreach (var item in model.Displays)
            {
                sb.Append(string.Format("<label class='dropdown-item dropdown-item-marker {0}'>", item.IsDefault ? "disabled" : ""));
                sb.Append(string.Format("<input type='checkbox' name='fieldDisplay' data-field='{0}' value='{0}' {2} {3}> <span>{1}</span>", item.FieldName, item.DisplayName, item.IsDisplay ? "checked" : "", item.IsDefault ? "disabled" : ""));
                sb.Append("</label>");
            }

            sb.Append("<div class='dropdown-divider'></div>");

            sb.Append(string.Format("<button class='btn btn-sm btn-secondary btn-h-purple btn-a-purple btn-block btnSaveChangeDisplay' idata='{0}' idata1='{1}' idata2='{2}'><i class='fa fa-save text-110 mr-1'></i> Save </button>", model.ControllerName, model.UrlReloadPage, model.AppCode));

            sb.Append("</div>");
            sb.Append("</div>");

            // Export data button
            if (model.IsImport)
            {
                sb.Append(string.Format("<button class='btn btn-outline-default btn-smd bgc-white btn-h-light-primary btn-a-outline-primary py-1 bs-export btnImportExcelFile' type='button' title='Import data' idata='{0}' idata1='{1}'><i class='fa fa-file-import text-primary-d1'></i></button>", model.ModalImportId, model.ModalImportUrl));
            }

            // Export data button
            if (model.IsExport)
            {
                sb.Append("<button class='btn btn-outline-default btn-smd bgc-white btn-h-light-primary btn-a-outline-primary py-1 bs-export btnExcelFile' type='button' title='Export data'><i class='fa fa-download text-blue-d1'></i></button>");
            }

            // Print button
            if (model.IsPrint)
            {
                sb.Append(string.Format("<button class='btn btn-outline-default btn-smd bgc-white btn-h-light-primary btn-a-outline-primary py-1 bs-print btnPrintFile' type='button' title='Print' idata='{0}' idata1='{1}'><i class='fa fa-print text-purple-d1'></i></button>", model.ControllerName, model.UrlReloadPage));
            }


            sb.Append("</div>");

            // Search button
            sb.Append("<div class='float-left search btn-group'>");
            sb.Append("<div class='input-group' title='Search'>");
            sb.Append(string.Format("<input class='form-control form-control search-input' type='text' name='keySearch' placeholder='Search' autocomplete='off' value='{0}'>", model.KeySearch));
            sb.Append("<div class='input-group-append'><button class='btn btn-secondary btnSearch' type='button'><i class='fa fa-search mr-1'></i></button></div>");
            sb.Append("</div>");
            sb.Append("</div>");

            sb.Append("</div>");
            #endregion
        }

        private static void GenerateBootstrapTable_Container(StringBuilder sb, TableModel model)
        {
            var sorts = model.Sort.Split('$', StringSplitOptions.RemoveEmptyEntries);

            //Kiểm tra nếu display ko có gì tạo mặc định Id
            if (model.Displays.Any() == false)
            {
                model.Displays.Add(new DisplayModel()
                {
                    DisplayName = "",
                    FieldName = "Id",
                    IsDefault = false,
                    IsDisplay = false,
                    IsSortable = true
                });
            }

            #region container

            sb.Append("<div class='fixed-table-container' style='padding-bottom: 0px;'>");

            sb.Append("<div class='fixed-table-header' style='display: none;'><table></table></div>");

            sb.Append("<div class='fixed-table-body'>");

            sb.Append("<table class='table text-dark-m2 text-95 table-bordered table-hover' id='table'>");

            sb.Append("<thead class='bgc-white text-grey text-uppercase text-80'><tr>");

            if (model.IsHaveQuickDetail)
            {
                sb.Append("<th class='detail' rowspan='1'><div class='fht-cell'></div></th>");
            }

            sb.Append(string.Format("<th class='bs-checkbox' style='width: 36px;' data-field='state'><div class='th-inner'><label><input name= 'btSelectAll' type='checkbox' idata='{0}'><span></span></label></div><div class='fht-cell'></div></th>", model.ControllerName));

            foreach (var itemDisplay in model.Displays.Where(n => n.IsDisplay))
            {
                if (!string.IsNullOrWhiteSpace(itemDisplay.DisplayName))
                {

                    //HNG: hiển thị từ Ipad trở lên
                    sb.Append(string.Format("<th class='{1} d-none d-md-table-cell' data-field='{0}'>", itemDisplay.FieldName, itemDisplay.IsSortable ? "thSortable" : ""));

                    if (itemDisplay.IsSortable)
                    {
                        sb.Append(string.Format("<div class='th-inner sortable both {1}'>{0}</div><div class='fht-cell'></div>", itemDisplay.DisplayName, sorts[0] == itemDisplay.FieldName ? sorts[1] : ""));
                    }
                    else
                    {
                        sb.Append(string.Format("<div class='th-inner'>{0}</div><div class='fht-cell'></div>", itemDisplay.DisplayName));
                    }

                    sb.Append("</th>");

                }
            }
            //HNG: Hiển thị trên mobile
            sb.Append("<th class='d-none d-table-cell d-md-none'><div class='th-inner sortable both '>Thông tin</div></th>");


            if (model.IsDisplayFunctionCog)
            {
                sb.Append("<th style='text-align: center; width: 75px;' data-field='tools'><div class='th-inner'><i class='fa fa-cog text-secondary-d1 text-130'></i></div><div class='fht-cell'></div></th>");
            }

            sb.Append("</tr></thead>");

            sb.Append("<tbody>");

            foreach (System.Data.DataRow itemValue in model.Content.Rows)
            {
                sb.Append(string.Format("<tr data-index='{0}' data-has-detail-view='true'>", itemValue[model.Displays[0].FieldName]));

                if (model.IsHaveQuickDetail || model.IsHaveModalPreview)
                {
                    sb.Append("<td style='text-align: center; width: 80px;'>");

                    sb.Append("<div class='action-buttons'>");

                    if (model.IsHaveQuickDetail)
                    {
                        sb.Append(string.Format("<a class='detail-icon quickDetail' href='javascript:void(0)' idata='{0}' idata1='{1}'><i class='fa fa-plus text-blue'></i></a>", itemValue[model.Displays[0].FieldName], model.UrlQuickPreview));
                    }

                    if (model.IsHaveModalPreview)
                    {
                        sb.Append(string.Format("<a class='detail-icon modalDetail' href='javascript:void(0)' idata='{0}' idata1='{1}' idata2='{2}'><i class='fa fa-eye text-purple'></i></a>", itemValue[model.Displays[0].FieldName], model.UrlModalPreview, model.ModalDetailId));
                    }

                    sb.Append("</div>");

                    sb.Append("</td>");
                }

                sb.Append(string.Format("<td class='bs-checkbox' style='width: 36px;'><label><input data-index='0' name='btSelectItem' type='checkbox' idata='{0}' idata1='{1}' {2}><span></span></label></td>", itemValue[model.Displays[0].FieldName], model.ControllerName, model.SelectedIds.Any(n => n == itemValue[model.Displays[0].FieldName].ToString()) ? "checked" : ""));

                //HNG: Hiển thị các cột dữ liệu từ DB cho màn hình Ipad trở lên-----------------------------------------------------------
                foreach (var itemTd in model.Displays.Where(n => n.IsDisplay))
                {
                    var columnValue = itemValue.Table.Columns[itemTd.FieldName];
                    if (columnValue != null)
                    {
                        switch (Type.GetTypeCode(columnValue.DataType))
                        {
                            //Dữ liệu kiểu datetime
                            case TypeCode.DateTime:
                                sb.Append("<td class='text-left d-none d-md-table-cell'>");

                                switch (itemTd.FieldName)
                                {
                                    case "DateCreated":

                                        var DateCreated = Convert.ToDateTime(itemValue[itemTd.FieldName]);

                                        sb.Append(DateCreated.ToString("dd/MM/yyyy HH:mm:ss"));

                                        break;

                                    case "DateUpdated":

                                        var DateUpdated = Convert.ToDateTime(itemValue[itemTd.FieldName]);

                                        sb.Append(DateUpdated.ToString("dd/MM/yyyy HH:mm:ss"));

                                        break;

                                    case "DateStart":

                                        var DateStart = Convert.ToDateTime(itemValue[itemTd.FieldName]);

                                        sb.Append(DateStart.ToString("dd/MM/yyyy HH:mm"));

                                        break;

                                    case "DateEnd":

                                        var DateEnd = Convert.ToDateTime(itemValue[itemTd.FieldName]);

                                        sb.Append(FunctionHelper.GetStatusDateByDay(DateEnd));

                                        break;

                                    case "FromDate":

                                        var FromDate = Convert.ToDateTime(itemValue[itemTd.FieldName]);

                                        sb.Append(FunctionHelper.GetStatusDateByDay(FromDate));

                                        break;

                                    case "ToDate":

                                        var ToDate = Convert.ToDateTime(itemValue[itemTd.FieldName]);

                                        sb.Append(FunctionHelper.GetStatusDateByDay(ToDate));

                                        break;

                                    case "DateCheckIn":

                                        var DateCheckIn = Convert.ToDateTime(itemValue[itemTd.FieldName]);

                                        sb.Append(DateCheckIn.ToString("dd/MM/yyyy HH:mm:ss"));

                                        break;

                                    case "DateCheckOut":

                                        var DateCheckOut = Convert.ToDateTime(itemValue[itemTd.FieldName]);

                                        sb.Append(DateCheckOut.ToString("dd/MM/yyyy HH:mm:ss"));

                                        break;

                                    case "WarrantyEndDate":

                                        var WarrantyEndDate = Convert.ToDateTime(itemValue[itemTd.FieldName]);

                                        sb.Append(FunctionHelper.GetStatusDateByDay(WarrantyEndDate));

                                        break;

                                    default:
                                        sb.Append(Convert.ToDateTime(itemValue[itemTd.FieldName]).ToString("dd/MM/yyyy"));
                                        break;
                                }

                                sb.Append("</td>");

                                break;

                            //Dữ liệu kiểu boolean
                            case TypeCode.Boolean:
                                sb.Append("<td class='text-left d-none d-md-table-cell'>");

                                switch (itemTd.FieldName)
                                {
                                    case "Active":

                                        var Active = Convert.ToBoolean(itemValue[itemTd.FieldName]);

                                        if (Active)
                                        {
                                            sb.Append("<span class='badge badge badge-success'>Hoạt động</span>");
                                        }
                                        else
                                        {
                                            sb.Append("<span class='badge badge badge-warning'>Ngừng hoạt động</span>");
                                        }

                                        break;

                                    case "isAdmin":

                                        var isAdmin = Convert.ToBoolean(itemValue[itemTd.FieldName]);

                                        if (isAdmin)
                                        {
                                            sb.Append("<span class='badge badge badge-danger'>ADMIN</span>");
                                        }
                                        else
                                        {
                                            sb.Append("<span class='badge badge badge-info'>USER</span>");
                                        }

                                        break;

                                    case "IsMain":

                                        var IsMain = Convert.ToBoolean(itemValue[itemTd.FieldName]);

                                        if (IsMain)
                                        {
                                            sb.Append("<span class='badge badge badge-info'>Chung</span>");
                                        }

                                        break;

                                    case "IsCompleted":

                                        var IsCompleted = Convert.ToBoolean(itemValue[itemTd.FieldName]);

                                        if (IsCompleted)
                                        {
                                            sb.Append("<span class='badge badge badge-success'>Hoàn thành</span>");
                                        }
                                        else
                                        {
                                            sb.Append("<span class='badge badge badge-info'>Đang tiến hành</span>");
                                        }

                                        break;

                                    case "IsApproved":

                                        var IsApproved = Convert.ToBoolean(itemValue[itemTd.FieldName]);

                                        if (IsApproved)
                                        {
                                            sb.Append("<span class='badge badge badge-success'>Đã duyệt</span>");
                                        }
                                        else
                                        {
                                            sb.Append("<span class='badge badge badge-warning'>Chưa duyệt</span>");
                                        }

                                        break;

                                    case "IsExpire":

                                        var IsExpire = Convert.ToBoolean(itemValue[itemTd.FieldName]);

                                        if (IsExpire)
                                        {
                                            sb.Append("<span class='badge badge badge-success'>Check hạn</span>");
                                        }
                                        else
                                        {
                                            sb.Append("<span class='badge badge badge-warning'>Không check hạn</span>");
                                        }

                                        break;

                                    case "IsStore":

                                        var IsStore = Convert.ToBoolean(itemValue[itemTd.FieldName]);

                                        if (IsStore)
                                        {
                                            sb.Append("<span class='badge badge badge-success'>Lưu trữ</span>");
                                        }


                                        break;
                                }

                                sb.Append("</td>");

                                break;

                            case TypeCode.String:

                                sb.Append("<td class='d-none d-md-table-cell'>");

                                switch (itemTd.FieldName)
                                {
                                    case "AccountStatus":

                                        var AccountStatus = Convert.ToString(itemValue[itemTd.FieldName]);

                                        if (AccountStatus == "0")
                                        {
                                            sb.Append("<span class='badge badge badge-info'>Chờ duyệt</span>");
                                        }
                                        else if (AccountStatus == "1")
                                        {
                                            sb.Append("<span class='badge badge badge-success'>Hoạt động</span>");
                                        }
                                        else if (AccountStatus == "2")
                                        {
                                            sb.Append("<span class='badge badge badge-warning'>Khóa</span>");
                                        }
                                        else if (AccountStatus == "3")
                                        {
                                            sb.Append("<span class='badge badge badge-default'>Dừng hoạt động</span>");
                                        }

                                        break;

                                    case "InvoiceStatus":

                                        var InvoiceStatus = Convert.ToString(itemValue[itemTd.FieldName]);

                                        var existed = StaticList.InvoiceStatus().FirstOrDefault(n => n.ItemValue == InvoiceStatus);

                                        if (existed != null)
                                        {
                                            sb.Append(string.Format("<span class='badge badge {0}'>{1}</span>", existed.ItemColor, existed.ItemText));
                                        }

                                        break;

                                    case "SALES_ProjectStatus":

                                        var ProjectStatus = Convert.ToString(itemValue[itemTd.FieldName]);

                                        var existedP = StaticList.SALES_ProjectStatus().FirstOrDefault(n => n.ItemValue == ProjectStatus);

                                        if (existedP != null)
                                        {
                                            sb.Append(string.Format("<span>{0}</span>", existedP.ItemText));
                                        }

                                        break;

                                    case "SALES_ProjectResult":

                                        var ProjectResult = Convert.ToString(itemValue[itemTd.FieldName]);

                                        var existedR = StaticList.SALES_ProjectResult().FirstOrDefault(n => n.ItemValue == ProjectResult);

                                        if (existedR != null)
                                        {
                                            sb.Append(string.Format("<span>{0}</span>", existedR.ItemText));
                                        }

                                        break;

                                    case "WorkProjectStatus":

                                        var WorkProjectStatus = Convert.ToString(itemValue[itemTd.FieldName]);

                                        var existedPW = StaticList.WorkProjectStatus().FirstOrDefault(n => n.ItemValue == WorkProjectStatus);

                                        if (existedPW != null)
                                        {
                                            sb.Append(string.Format("<span class='badge badge {0}'>{1}</span>", existedPW.ItemColor, existedPW.ItemText));
                                        }

                                        break;

                                    case "StageName":
                                        var StageName = Convert.ToString(itemValue[itemTd.FieldName]);
                                        var StageColor = Convert.ToString(itemValue["StageColor"]);

                                        sb.Append(string.Format("<span class='badge badge {0}'>{1}</span>", StageColor, StageName));

                                        break;

                                    case "DayOffStatus":
                                        var DayOffStatus = Convert.ToString(itemValue[itemTd.FieldName]);

                                        if (DayOffStatus == "1")
                                        {
                                            sb.Append("<span class='badge badge badge-info'>Chờ duyệt</span>");
                                        }
                                        else if (DayOffStatus == "2")
                                        {
                                            sb.Append("<span class='badge badge badge-success'>Đã duyệt</span>");
                                        }
                                        else if (DayOffStatus == "3")
                                        {
                                            sb.Append("<span class='badge badge badge-danger'>Không duyệt</span>");
                                        }
                                        else if (DayOffStatus == "4")
                                        {
                                            sb.Append("<span class='badge bgc-purple brc-purple text-white'>Đã xử lý</span>");
                                        }
                                        break;
                                    case "OTReportStatus":
                                        var OTReportStatus = Convert.ToString(itemValue[itemTd.FieldName]);

                                        if (OTReportStatus == "1")
                                        {
                                            sb.Append("<span class='badge badge badge-info'>Chờ duyệt</span>");
                                        }
                                        else if (OTReportStatus == "2")
                                        {
                                            sb.Append("<span class='badge badge badge-success'>Đã duyệt</span>");
                                        }
                                        else if (OTReportStatus == "3")
                                        {
                                            sb.Append("<span class='badge badge badge-danger'>Không duyệt</span>");
                                        }
                                        else if (OTReportStatus == "4")
                                        {
                                            sb.Append("<span class='badge bgc-purple brc-purple text-white'>Đã chấp thuận</span>");
                                        }
                                        break;

                                    default:

                                        sb.Append(itemValue[itemTd.FieldName]);

                                        break;
                                }

                                sb.Append("</td>");

                                break;

                            case TypeCode.Decimal:

                                sb.AppendLine("<td style= ''>");

                                switch (itemTd.FieldName)
                                {
                                    case "TotalContractAmount":
                                        sb.AppendLine(string.Format("<span class='text-brown'>{0}</span>", Convert.ToDecimal(itemValue[itemTd.FieldName]).ToString("###,###.##")));
                                        break;

                                    case "TotalFinishAmount":
                                        sb.AppendLine(string.Format("<span class='text-primary'>{0}</span>", Convert.ToDecimal(itemValue[itemTd.FieldName]).ToString("###,###.##")));
                                        break;

                                    case "TotalReceiptAmount":
                                        sb.AppendLine(string.Format("<span class='text-purple'>{0}</span>", Convert.ToDecimal(itemValue[itemTd.FieldName]).ToString("###,###.##")));
                                        break;

                                    case "TotalRemainAmount":
                                        sb.AppendLine(string.Format("<span class='text-danger'>{0}</span>", Convert.ToDecimal(itemValue[itemTd.FieldName]).ToString("###,###.##")));
                                        break;

                                    default:
                                        sb.AppendLine(string.Format("{0}", Convert.ToDecimal(itemValue[itemTd.FieldName]).ToString("###,###.##")));
                                        break;
                                }

                                sb.AppendLine("</td>");

                                break;

                            default:
                                sb.Append("<td class='d-none d-md-table-cell'>");
                                sb.Append(itemValue[itemTd.FieldName]);
                                sb.Append("</td>");
                                break;
                        }
                    }
                    else
                    {
                        sb.Append("<td class='text-left d-none d-md-table-cell'>");
                        sb.Append("</td>");
                    }
                }

                //HNG: Hiển thị dữ liệu từ màn hình Ipad trở xuống
                // sb.Append("<td class='d-none d-table-cell d-md-none'>");
                // foreach (var itemTd in model.Displays.Where(n => n.IsDisplay))
                // {
                //     var columnValue = itemValue.Table.Columns[itemTd.FieldName];
                //     if (columnValue != null)
                //     {
                //         switch (Type.GetTypeCode(columnValue.DataType))
                //         {
                //             //Dữ liệu kiểu datetime
                //             case TypeCode.DateTime:
                //                 sb.Append(string.Format("<p class='text-left mgb-5'><strong>{0}</strong>: ", itemTd.DisplayName));

                //                 switch (itemTd.FieldName)
                //                 {
                //                     case "DateCreated":

                //                         var DateCreated = Convert.ToDateTime(itemValue[itemTd.FieldName]);

                //                         sb.Append(DateCreated.ToString("dd/MM/yyyy HH:mm:ss"));

                //                         break;

                //                     case "DateUpdated":

                //                         var DateUpdated = Convert.ToDateTime(itemValue[itemTd.FieldName]);

                //                         sb.Append(DateUpdated.ToString("dd/MM/yyyy HH:mm:ss"));

                //                         break;
                //                 }

                //                 sb.Append("</p>");

                //                 break;

                //             //Dữ liệu kiểu boolean
                //             case TypeCode.Boolean:
                //                 sb.Append(string.Format("<p class='text-left mgb-5'><strong>{0}</strong>: ", itemTd.DisplayName));

                //                 switch (itemTd.FieldName)
                //                 {
                //                     case "Active":

                //                         var Active = Convert.ToBoolean(itemValue[itemTd.FieldName]);

                //                         if (Active)
                //                         {
                //                             sb.Append("<span class='badge badge badge-success'>Hoạt động</span>");
                //                         }
                //                         else
                //                         {
                //                             sb.Append("<span class='badge badge badge-warning'>Ngừng hoạt động</span>");
                //                         }

                //                         break;

                //                     case "isAdmin":

                //                         var isAdmin = Convert.ToBoolean(itemValue[itemTd.FieldName]);

                //                         if (isAdmin)
                //                         {
                //                             sb.Append("<span class='badge badge badge-danger'>ADMIN</span>");
                //                         }
                //                         else
                //                         {
                //                             sb.Append("<span class='badge badge badge-info'>USER</span>");
                //                         }

                //                         break;
                //                 }

                //                 sb.Append("</p>");

                //                 break;

                //             case TypeCode.String:

                //                 sb.Append(string.Format("<p class='text-left mgb-5'><strong>{0}</strong>: ", itemTd.DisplayName));

                //                 switch (itemTd.FieldName)
                //                 {
                //                     case "AccountStatus":

                //                         var AccountStatus = Convert.ToString(itemValue[itemTd.FieldName]);

                //                         if (AccountStatus == "0")
                //                         {
                //                             sb.Append("<span class='badge badge badge-info'>Chờ duyệt</span>");
                //                         }
                //                         else if (AccountStatus == "1")
                //                         {
                //                             sb.Append("<span class='badge badge badge-success'>Hoạt động</span>");
                //                         }
                //                         else if (AccountStatus == "2")
                //                         {
                //                             sb.Append("<span class='badge badge badge-warning'>Khóa</span>");
                //                         }
                //                         else if (AccountStatus == "3")
                //                         {
                //                             sb.Append("<span class='badge badge badge-default'>Dừng hoạt động</span>");
                //                         }

                //                         break;

                //                     case "DayOffStatus":
                //                         var DayOffStatus = Convert.ToString(itemValue[itemTd.FieldName]);

                //                         if (DayOffStatus == "1")
                //                         {
                //                             sb.Append("<span class='badge badge badge-info'>Chờ duyệt</span>");
                //                         }
                //                         else if (DayOffStatus == "2")
                //                         {
                //                             sb.Append("<span class='badge badge badge-success'>Đã duyệt</span>");
                //                         }
                //                         else if (DayOffStatus == "3")
                //                         {
                //                             sb.Append("<span class='badge badge badge-danger'>Không duyệt</span>");
                //                         }
                //                         break;

                //                     default:

                //                         sb.Append(itemValue[itemTd.FieldName]);

                //                         break;
                //                 }

                //                 sb.Append("</p>");

                //                 break;

                //             default:
                //                 sb.Append(string.Format("<p class='text-left mgb-5'><strong>{0}</strong>: ", itemTd.DisplayName));
                //                 sb.Append(itemValue[itemTd.FieldName]);
                //                 sb.Append("</p>");
                //                 break;
                //         }
                //     }
                // }

                // sb.Append("</td>");

                //Có một trong quyền này thì hiển thị ra luôn
                if (model.IsDisplayFunctionCog)
                {
                    sb.Append("<td style='text-align: center; width: 75px;'>");

                    sb.Append("<div class='action-buttons'>");

                    if (model.AuthAction.Update_Auth == 1)
                    {
                        sb.Append(string.Format("<a class='text-success mx-2px' href='{0}/{1}'><i class='fa fa-pencil-alt text-105'></i></a>", model.UrlUpdate, itemValue[model.Displays[0].FieldName]));
                    }

                    if (model.AuthAction.Delete_Auth == 1)
                    {
                        sb.Append(string.Format(" <a class='text-danger-m1 mx-2px btnDelete' href='javascript:void(0)' idata='{0}' idata1='{1}'><i class='fa fa-trash-alt text-105'></i></a>", itemValue[model.Displays[0].FieldName], model.UrlDelete));
                    }

                    if (model.IsCopyData)
                    {
                        sb.Append(string.Format("<a class='text-info mx-2px btnCopyRecord' href='javascript:void(0)' idata='{0}' idata1='{1}' idata2='{2}'><i class='fa fa-copy text-105'></i></a>", itemValue[model.Displays[0].FieldName], model.ModalCopyId, model.ModalCopyUrl));
                    }

                    if (model.ControllerName == "Account")
                    {
                        sb.Append(string.Format("<a class='text-info mx-2px btnResetPassword' href='javascript:void(0)' idata='{0}' idata1='{1}'><i class='fa fa-sync text-105'></i></a>", itemValue[model.Displays[0].FieldName], "/Account/ResetPassword"));
                    }

                    sb.Append("</div>");

                    sb.Append("</td>");
                }

                sb.Append("</tr>");
            }

            sb.Append("</tbody>");

            sb.Append("</table>");

            sb.Append("</div>");

            sb.Append(" <div class='fixed-table-footer'><table><thead><tr></tr></thead></table></div>");

            sb.Append("</div>");
            #endregion
        }

        private static void GenerateBootstrapTable_Pagination(StringBuilder sb, TableModel model, Func<int, string> pageUrl)
        {
            #region pagination

            sb.Append("<div class='fixed-table-pagination'>");
            sb.Append("<div class='float-left pagination-detail'>");

            sb.Append("<span class='pagination-info'>");
            sb.Append(string.Format("Showing {0} to {1} of {2} rows", (model.PageIndex - 1) * model.PageSize + 1, (model.PageIndex - 1) * model.PageSize + model.PageSize, model.TotalItem));
            sb.Append("</span>");

            sb.Append("<span class='page-list'>");

            sb.Append("<select class='pageSizeSelectList'>");

            sb.Append(string.Format("<option value='10' {0}>10</option>", model.PageSize == 10 ? "selected" : ""));

            sb.Append(string.Format("<option value='20' {0}>20</option>", model.PageSize == 20 ? "selected" : ""));

            sb.Append(string.Format("<option value='50' {0}>50</option>", model.PageSize == 50 ? "selected" : ""));

            sb.Append(string.Format("<option value='100' {0}>100</option>", model.PageSize == 100 ? "selected" : ""));

            sb.Append("</select>");

            sb.Append(" rows per page</span>");

            sb.Append("</div>");

            sb.Append("<div class='float-right pagination'>");
            sb.Append("<ul class='pagination nav-tabs-scroll is-scrollable'>");

            if (model.TotalPage > 1) // Nếu số trang = 1 thì không hiển thị điều hướng trang
            {
                if (model.PageIndex == 1)
                {
                    sb.Append("<li class='page-item mr-2 previous disabled' aria-controls='dynamic-table' tabindex='0' id='dynamic-table_first'><a href='javascript:void(0);' class='h-5 pt-25 page-link rounded-0 border-0 mr-2 text-95' rel='nofollow'><i class='fa fa-angle-double-left'></i></a></li>");
                    sb.Append("<li class='page-item mr-2 previous disabled' aria-controls='dynamic-table' tabindex='0' id='dynamic-table_previous'><a href='javascript:void(0);' class='h-5 pt-25 page-link rounded-0 border-0 mr-2 text-95' rel='nofollow'><i class='fa fa-angle-left'></i></a></li>");
                }
                else
                {
                    sb.Append("<li class='page-item mr-2 previous' aria-controls='dynamic-table' tabindex='0' id='dynamic-table_first'><a href='" + pageUrl(1) + "' class='h-5 pt-25 page-link rounded-0 border-0 mr-2 text-95' ><i class='fa fa-angle-double-left'></i></a></li>");
                    sb.Append("<li class='page-item mr-2 previous' aria-controls='dynamic-table' tabindex='0' id='dynamic-table_previous'><a href='" + pageUrl(model.PageIndex - 1) + "' class='h-5 pt-25 page-link rounded-0 border-0 mr-2 text-95' ><i class='fa fa-angle-left'></i></a></li>");
                }

                const int pageHold = 10;
                var totalHold = model.TotalPage / pageHold + 1;
                var currentHold = model.PageIndex / pageHold >= 1 && model.PageIndex % pageHold >= 1 ?
                    model.PageIndex / pageHold + 1 : model.PageIndex / pageHold;
                currentHold = currentHold == 0 ? 1 : currentHold;

                var pointStart = 1;

                if (model.PageIndex / pageHold >= 1 && model.PageIndex % pageHold >= 1)
                    pointStart = model.PageIndex / pageHold * pageHold + 1;
                else if (model.PageIndex / pageHold > 0)
                    pointStart = (model.PageIndex / pageHold - 1) * pageHold + 1;

                if (currentHold == 1)
                {
                    //sb.Append("<div class='t-numeric'>");
                    for (var i = pointStart; i <= ((model.TotalPage < pageHold) ? model.TotalPage : pointStart + pageHold - 1); i++)
                    {
                        if (i == model.PageIndex)
                        {
                            sb.AppendFormat("<li class='page-item mr-2 active' aria-controls='dynamic-table' tabindex='0'><a href='javascript:void(0);' class='w-5 h-5 pt-2 page-link text-center rounded-circle'>{0}</a></li>", i);
                        }
                        else
                        {
                            sb.AppendFormat("<li class='page-item mr-2' aria-controls='dynamic-table' tabindex='0'><a href='" + pageUrl(i) + "' class='w-5 h-5 pt-2 page-link text-center rounded-circle'>{0}</a></li>", i);
                        }
                    }
                    if (totalHold > 1)
                        sb.Append("<li class='page-item mr-2' aria-controls='dynamic-table' tabindex='0'><a href='" + pageUrl(pageHold * currentHold + 1) + "' class='w-5 h-5 pt-2 page-link text-center rounded-circle'>...</a></li>");
                    //sb.Append("</div>");
                }
                else if (currentHold == totalHold)
                {
                    //sb.Append("<div class='t-numeric'>");
                    sb.Append("<li class='page-item mr-2' aria-controls='dynamic-table' tabindex='0'><a href='" + pageUrl(pageHold * (currentHold - 1)) + "' class='w-5 h-5 pt-2 page-link text-center rounded-circle'>...</a></li>");
                    for (var i = pointStart; i <= model.TotalPage; i++)
                    {
                        if (i == model.PageIndex)
                        {
                            sb.AppendFormat("<li class='page-item mr-2 active' aria-controls='dynamic-table' tabindex='0'><a href='javascript:void(0);' class='w-5 h-5 pt-2 page-link text-center rounded-circle'>{0}</a></li>", i);
                        }
                        else
                        {
                            sb.AppendFormat("<li class='page-item mr-2' aria-controls='dynamic-table' tabindex='0'><a href='" + pageUrl(i) + "' class='w-5 h-5 pt-2 page-link text-center rounded-circle'>{0}</a></li>", i);
                        }
                    }
                    //sb.Append("</div>");
                }
                else
                {
                    //sb.Append("<div class='t-numeric'>");
                    sb.Append("<li class='page-item mr-2' aria-controls='dynamic-table' tabindex='0'><a href='" + pageUrl(pageHold * (currentHold - 1)) + "' class='w-5 h-5 pt-2 page-link text-center rounded-circle'>...</a></li>");
                    for (var i = pointStart; i <= pointStart + pageHold - 1; i++)
                    {
                        if (i == model.PageIndex)
                        {
                            sb.AppendFormat("<li class='page-item mr-2 active' aria-controls='dynamic-table' tabindex='0'><a href='javascript:void(0);' class='w-5 h-5 pt-2 page-link text-center rounded-circle'>{0}</a></li>", i);
                        }
                        else
                        {
                            sb.AppendFormat("<li class='page-item mr-2' aria-controls='dynamic-table' tabindex='0'><a href='" + pageUrl(i) + "'>{0}</a></li>", i);
                        }
                    }
                    sb.Append("<li class='page-item mr-2' aria-controls='dynamic-table' tabindex='0'><a href='" + pageUrl(pageHold * currentHold + 1) + "'>...</a></li>");
                    //sb.Append("</div>");
                }

                if (model.PageIndex == model.TotalPage)
                {
                    sb.Append("<li class='page-item mr-2 next disabled' aria-controls='dynamic-table' tabindex='0' id='dynamic-table_next'><a href='javascript:void(0);' class='h-5 pt-25 page-link rounded-0 border-0 mr-2 text-95' rel='nofollow'><i class='fa fa-angle-right'></i></a></li>");
                    sb.Append("<li class='page-item mr-2 next disabled' aria-controls='dynamic-table' tabindex='0' id='dynamic-table_last'><a href='javascript:void(0);' class='h-5 pt-25 page-link rounded-0 border-0 mr-2 text-95' rel='nofollow'><i class='fa fa-angle-double-right'></i></a></li>");
                }
                else
                {
                    sb.Append("<li class='page-item mr-2 next' aria-controls='dynamic-table' tabindex='0' id='dynamic-table_next'><a href='" + pageUrl(model.PageIndex + 1) + "' class='h-5 pt-25 page-link rounded-0 border-0 mr-2 text-95'><i class='fa fa-angle-right'></i></a></li>");
                    sb.Append("<li class='page-item mr-2 next' aria-controls='dynamic-table' tabindex='0' id='dynamic-table_last'><a href='" + pageUrl(model.TotalPage) + "' class='h-5 pt-25 page-link rounded-0 border-0 mr-2 text-95'><i class='fa fa-angle-double-right'></i></a></li");
                }
            }

            sb.Append("</ul>");
            sb.Append("</div>");

            sb.Append("</div>");
            #endregion
        }

        private static void GenerateBootstrapTable_ModalSearch(StringBuilder sb, TableModel model)
        {
            #region modal search
            if (model.IsAdvanceSearch)
            {
                sb.Append("<div class='modal fade' id='modalSearch' tabindex='-1' role='dialog' aria-hidden='true'>");

                sb.Append("<div class='modal-dialog' role='document'>");

                sb.Append("<div class='modal-content'>");

                sb.Append("<div class='modal-header'><h5 class='modal-title text-blue'>Tìm kiếm nâng cao</h5><button type = 'button' class='close' data-dismiss='modal' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div>");

                sb.Append("<div class='modal-body'>");

                sb.Append("<form>");

                //Dữ liệu dạng select list
                if (model.SelectList_Multi != null && model.SelectList_Multi.Any())
                {
                    foreach (var item in model.SelectList_Multi)
                    {
                        var selectIds = item.Data.Selecteds.Split(',').ToList();

                        //Dữ liệu là datetime
                        if (item.IsDatetimeSelect)
                        {
                            var fromDate = "";
                            var toDate = "";

                            //Lấy thời gian
                            if (!string.IsNullOrWhiteSpace(model.TimeSearch))
                            {
                                var times = model.TimeSearch.Split('$', StringSplitOptions.RemoveEmptyEntries);
                                if (times.Length == 2)
                                {
                                    fromDate = times[0];
                                    toDate = times[1];
                                }
                                else
                                {
                                    fromDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                                    toDate = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy HH:mm");
                                }
                            }
                            else
                            {
                                fromDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                                toDate = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy HH:mm");
                            }

                            sb.Append("<hr />");

                            sb.Append("<div class='form-group row'>");

                            #region MyRegion

                            sb.Append("<div class='col-sm-3 col-form-label text-sm-right pr-0'>");

                            sb.Append(string.Format("<select {2} id='{0}' name='{0}' class='chosen-select form-control' data-placeholder='{1}'>", item.Data.IdSelectList, item.Data.Placeholder, item.Data.isMultiSelect ? "multiple" : ""));

                            foreach (var option in item.Data.Data)
                            {
                                sb.Append(string.Format("<option value='{0}' {2}>{1}</option>", option.ItemValue, option.ItemText, selectIds.Any(n => n.Replace("'", "").Equals(option.ItemValue)) ? "selected" : ""));
                            }

                            sb.Append("</select>");

                            sb.Append("</div>");
                            #endregion

                            #region MyRegion

                            sb.Append("<div class='col-sm-9'>");

                            #region FromDate

                            sb.Append("<div class='form-group row'>");

                            sb.Append("<div class='col-sm-4 control-label-right'>");

                            sb.Append("<label for= 'fromdate' class='mb-0'> Từ ngày </label>");

                            sb.Append("</div>");

                            sb.Append("<div class='col-sm-8'>");

                            sb.Append("<div class='input-group date input-datetime' id='inputDateEnd'>");

                            sb.Append(string.Format("<input class='form-control' id='fromdate' name='fromdate' placeholder='Từ ngày' type='text' value='{0}'>", fromDate));

                            sb.Append("<div class='input-group-addon input-group-append'><div class='input-group-text'><i class='far fa-clock'></i></div></div>");

                            sb.Append("</div>");

                            sb.Append("</div>");

                            sb.Append("</div>");
                            #endregion

                            #region ToDate

                            sb.Append("<div class='form-group row'>");

                            sb.Append("<div class='col-sm-4 control-label-right'>");

                            sb.Append("<label for= 'todate' class='mb-0'> Đến ngày </label>");

                            sb.Append("</div>");

                            sb.Append("<div class='col-sm-8'>");

                            sb.Append("<div class='input-group date input-datetime' id='inputDateEnd'>");

                            sb.Append(string.Format("<input class='form-control' id='todate' name='todate' placeholder='Đến ngày' type='text' value='{0}'>", toDate));

                            sb.Append("<div class='input-group-addon input-group-append'><div class='input-group-text'><i class='far fa-clock'></i></div></div>");

                            sb.Append("</div>");

                            sb.Append("</div>");

                            sb.Append("</div>");
                            #endregion

                            sb.Append("</div>");
                            #endregion

                            sb.Append("</div>");
                        }
                        else
                        {
                            sb.Append("<div class='form-group row'>");

                            sb.Append("<div class='col-sm-3 col-form-label text-sm-right pr-0'>");

                            sb.Append(string.Format("<label for= '{0}' class='mb-0'> {1} </label>", item.Data.IdSelectList, item.Name));

                            sb.Append("</div>");

                            sb.Append("<div class='col-sm-9'>");

                            sb.Append(string.Format("<select {2} id='{0}' name='{0}' class='chosen-select form-control' data-placeholder='{1}'>", item.Data.IdSelectList, item.Data.Placeholder, item.Data.isMultiSelect ? "multiple" : ""));

                            foreach (var option in item.Data.Data)
                            {
                                sb.Append(string.Format("<option value='{0}' {2}>{1}</option>", option.ItemValue, option.ItemText, selectIds.Any(n => n.Replace("'", "").Equals(option.ItemValue)) ? "selected" : ""));
                            }


                            sb.Append("</select>");

                            sb.Append("</div>");

                            sb.Append("</div>");
                        }
                    }
                }

                //Dữ liệu dạng input
                if (model.List_Input != null && model.List_Input.Any())
                {
                    foreach (var item in model.List_Input)
                    {
                        sb.Append("<div class='form-group row'>");

                        sb.Append("<div class='col-sm-3 col-form-label text-sm-right pr-0'>");
                        sb.Append(item.Input_Text);
                        sb.Append("</div>");

                        sb.Append("<div class='col-sm-9'>");
                        sb.Append(string.Format("<input name='{0}' class='form-control input-search' value='{1}'/>", item.Input_Name, item.Input_Value));
                        sb.Append("</div>");

                        sb.Append("</div>");
                    }
                }

                if (model.AutoComplete_Input != null && model.AutoComplete_Input.Any())
                {
                    sb.Append("<hr />");

                    foreach (var item in model.AutoComplete_Input)
                    {
                        sb.Append("<div class='form-group row input-autocomplete'>");

                        sb.Append("<div class='col-sm-3 col-form-label text-sm-right pr-0'>");
                        sb.Append(item.Name);
                        sb.Append("</div>");

                        sb.Append("<div class='col-sm-9'>");
                        sb.Append(string.Format("<input name='{0}' class='form-control autosearch' value='{1}' idata='{2}' placeholder='{3}' idatahidden='{4}'/>", item.Input_Name, item.Input_Display, item.Url, item.Input_Placeholder, item.Input_Hidden));
                        sb.Append("</div>");

                        sb.Append("</div>");
                    }
                }

                //Dữ liệu dạng slide range
                if (model.Slide_Input != null && model.Slide_Input.Any())
                {
                    sb.Append("<hr />");

                    foreach (var item in model.Slide_Input)
                    {
                        sb.Append("<div class='form-group row input-slide'>");

                        sb.Append("<div class='col-sm-3 control-label-right'>");
                        sb.Append("<label>");
                        sb.Append(string.Format("<input type='checkbox' class='using-slide' name='{1}' {2}/> {0}", item.Div_Name, item.Div_Id, item.IsUsing ? "checked" : ""));
                        sb.Append("</label>");
                        sb.Append("</div>");

                        sb.Append("<div class='col-sm-9'>");
                        sb.Append(string.Format("<div id='{0}' class='noUi-toggle-tooltip slider-sm slider-tooltip-right slider-tooltip-caret'></div>", item.Div_Id));
                        sb.Append(string.Format("<p id='dis_{0}' style='margin-top:10px'></p>", item.Div_Id));

                        sb.Append(string.Format("<input id='{0}' class='input-slide-from' type='hidden' />", item.Div_hidFromValue));
                        sb.Append(string.Format("<input id='{0}' class='input-slide-to' type='hidden' />", item.Div_hidToValue));

                        sb.Append("</div>");

                        sb.Append("</div>");
                    }
                }

                sb.Append("</form>");

                sb.Append("</div>");

                sb.Append("<div class='modal-footer'>");

                sb.Append("<button type='button' class='btn btn-secondary' data-dismiss='modal'>Close</button>");

                //sb.Append(string.Format("<button type='button' class='btn btn-info btnModalResetSearch' idata='{0}'>Reset</button>", model.UrlReloadPage));

                sb.Append("<button type='button' class='btn btn-primary btnModalSearch'>Search</button>");

                sb.Append("</div>");

                sb.Append("</div>");

                sb.Append("</div>");

                sb.Append("</div>");
            }


            #endregion
        }
    }
}
