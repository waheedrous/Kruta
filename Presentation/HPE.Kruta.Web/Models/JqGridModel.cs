using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Trirand.Web.Mvc;

namespace HPE.Kruta.Web.Models
{
    public class JqGridModel
    {
        public JQGrid JQGridSample { get; set; }

        public JqGridModel()
        {
            JQGridSample = new JQGrid
            {
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn
                    {
                        // Always set PrimaryKey for Add,Edit,Delete operations
                        // If not set, the first column will be assumed as primary key
                        DataField = "QueueID",
                        HeaderText="ID",
                        PrimaryKey = true,
                        Editable = false,
                        Width = 50,
                        Resizable = true,
                        DataType = typeof(int)
                    },
                    new JQGridColumn
                    {
                        DataField = "ReceivedDateTime",
                        HeaderText="Received Date",
                        Editable = true,
                        Width = 150,
                        DataFormatString = "{0:yyyy/MM/dd}",
                        Resizable = true,
                        DataType = typeof(DateTime)
                    },
                    new JQGridColumn
                    {
                        DataField = "RecordedDateTime",
                        HeaderText="Recorded Date",
                        Editable = true,
                        Width = 150,
                        DataFormatString = "{0:yyyy/MM/dd}",
                        Resizable = true,
                        DataType = typeof(DateTime)
                    },
                    new JQGridColumn
                    {
                        DataField = "ParcelNumber",
                        HeaderText="Parcel Number",
                        Editable = true,
                        Width = 100,
                        Resizable = true,
                        DataType = typeof(string)
                    },
                    new JQGridColumn
                    {
                        DataField = "DocumentNumber",
                        HeaderText="Document Number",
                        Editable =  true,
                        Width = 100,
                        Resizable = true,
                        DataType = typeof(string)
                    },
                    new JQGridColumn
                    {
                        DataField = "DocumentTypeCode",
                        HeaderText="Document Type",
                        Editable =  true,
                        Width = 100,
                        Resizable = true,
                        DataType = typeof(string)
                    },
                    new JQGridColumn
                    {
                        DataField = "DepartmentName",
                        HeaderText="Queue/Department",
                        Editable =  true,
                        Width = 100,
                        Resizable = true,
                        DataType = typeof(string)
                    },
                    new JQGridColumn
                    {
                        DataField = "EmployeeName",
                        HeaderText="Assigned To",
                        Editable =  true,
                        Width = 150,
                        Resizable = true,
                        DataType = typeof(string)
                    },
                    new JQGridColumn
                    {
                        DataField = "QueueStatusDescription",
                        HeaderText="Status",
                        Editable =  true,
                        Width = 100,
                        Resizable = true,
                        DataType = typeof(string)
                    },
                    //new JQGridColumn
                    //{
                    //    DataField = "DocumentStatusDescription",
                    //    HeaderText="Document Status",
                    //    Editable =  true,
                    //    Width = 100,
                    //    Resizable = true,
                    //    DataType = typeof(string)
                    //},
                    //new JQGridColumn
                    //{
                    //    DataField = "DepartmentName",
                    //    HeaderText="Flagged",
                    //    Editable =  true,
                    //    Width = 100,
                    //    Resizable = true,
                    //    DataType = typeof(string)
                    //}
                },

                Width = Unit.Pixel(1170),
                Height = Unit.Percentage(100),
                ColumnReordering = true,

                ToolBarSettings = new ToolBarSettings
                {
                    //ShowRefreshButton = true,
                    ShowSearchToolBar = true,
                    ShowSearchButton = true
                }
            };
        }
    }
}