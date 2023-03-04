using CrystalReportRenderWebAPI.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CrystalReportRenderWebAPI.Controllers
{
    public class DefaultController : ApiController
    {
        [Route("api/default/GetCustomerReport")]
        [HttpGet]
        public HttpResponseMessage GetCustomerReport()
        {
            string reportPath = "~/Reports/";
            string reportFileName = "CustomerCReport.rpt";
            string exportFilename = "CustomerCReport.doc";

            HttpResponseMessage result = CrystalReport.RenderReport(GetCustomerDTable(), reportPath, reportFileName, exportFilename);
            return result;
        }

        private DataTable GetCustomerDTable()
        {
            DataSet mDateSet = CreateCustomersTable();
            CreateOrdersTable(mDateSet);
            DataRelation dtRelation;
            DataColumn custCol = mDateSet.Tables["Customers"].Columns["id"];
            DataColumn orderCol = mDateSet.Tables["orders"].Columns["custId"];
            dtRelation = new DataRelation("CustOrderRelation ", custCol, orderCol);
            mDateSet.Tables["orders"].ParentRelations.Add(dtRelation);

            return mDateSet.Tables["Customers"];

        }



        // Create Customers table
        private DataSet CreateCustomersTable()
        {
            // Create a new DataTable.
            DataTable custTable = new DataTable("Customers");
            DataColumn dtColumn;
            DataRow myDataRow;

            // Create id column
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(Int32);
            dtColumn.ColumnName = "id";
            dtColumn.Caption = "Cust ID";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = true;
            // Add column to the DataColumnCollection.
            custTable.Columns.Add(dtColumn);

            // Create Name column.
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(String);
            dtColumn.ColumnName = "Name";
            dtColumn.Caption = "Cust Name";
            dtColumn.AutoIncrement = false;
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;
            /// Add column to the DataColumnCollection.
            custTable.Columns.Add(dtColumn);

            // Create Address column.
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(String);
            dtColumn.ColumnName = "Address";
            dtColumn.Caption = "Address";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;
            // Add column to the DataColumnCollection.
            custTable.Columns.Add(dtColumn);

            // Make id column the primary key column.
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = custTable.Columns["id"];
            custTable.PrimaryKey = PrimaryKeyColumns;

            // Create a new DataSet
            DataSet dtSet = new DataSet();

            // Add custTable to the DataSet.
            dtSet.Tables.Add(custTable);

            // Add data rows to the custTable using NewRow method
            // I add three customers with their addresses, names and ids
            myDataRow = custTable.NewRow();
            myDataRow["id"] = 1001;
            myDataRow["Address"] = "43 Lanewood Road, cito, CA";
            myDataRow["Name"] = "George Bishop";
            custTable.Rows.Add(myDataRow);
            myDataRow = custTable.NewRow();
            myDataRow["id"] = 1002;
            myDataRow["name"] = "Rock joe";
            myDataRow["Address"] = " kind of Prussia, PA";
            custTable.Rows.Add(myDataRow);
            myDataRow = custTable.NewRow();
            myDataRow["id"] = 1003;
            myDataRow["Name"] = "Miranda";
            myDataRow["Address"] = "279 P. Avenue, Bridgetown, PA";
            custTable.Rows.Add(myDataRow);
            return dtSet;
        }

        // Create Orders table
        private void CreateOrdersTable(DataSet dtSet)
        {
            // Create a DataTable
            DataTable ordersTable = new DataTable("Orders");
            DataColumn dtColumn;
            DataRow dtRow;

            // Create OrderId column
            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Int32");
            dtColumn.ColumnName = "OrderId";
            dtColumn.AutoIncrement = true;
            dtColumn.Caption = "Order ID";
            dtColumn.ReadOnly = true;
            dtColumn.Unique = true;
            ordersTable.Columns.Add(dtColumn);

            // Create Name column.
            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.String");
            dtColumn.ColumnName = "Name";
            dtColumn.Caption = "Item Name";
            ordersTable.Columns.Add(dtColumn);

            // Create CustId column which Reprence Cust Id from
            // The cust Table
            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Int32");
            dtColumn.ColumnName = "CustId";
            dtColumn.AutoIncrement = false;
            dtColumn.Caption = "CustId";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;
            ordersTable.Columns.Add(dtColumn);

            // Create Description column.
            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.String");
            dtColumn.ColumnName = "Description";
            dtColumn.Caption = "DescriptionName";
            ordersTable.Columns.Add(dtColumn);

            // Add ordersTable to DataSet
            dtSet.Tables.Add(ordersTable);

            // ADD two rows to the customer Id 1001
            dtRow = ordersTable.NewRow();
            dtRow["OrderId"] = 0;
            dtRow["Name"] = "ASP Book";
            dtRow["custId"] = 1001;
            dtRow["Description"] = "Same Day";
            ordersTable.Rows.Add(dtRow);
            dtRow = ordersTable.NewRow();
            dtRow["OrderId"] = 1;
            dtRow["Name"] = " C# Book";
            dtRow["custId"] = 1001;
            dtRow["description"] = "2 DAY AIR";
            ordersTable.Rows.Add(dtRow);
            // Add two rows to Customer id 1002
            dtRow = ordersTable.NewRow();
            dtRow["OrderId"] = 2;
            dtRow["Name"] = "Data Quest";
            dtRow["Description"] = "Monthly magazine";
            dtRow["CustId"] = 1002;
            ordersTable.Rows.Add(dtRow);
            dtRow = ordersTable.NewRow();
            dtRow["OrderId"] = 3;
            dtRow["Name"] = "PC Magazine";
            dtRow["Description"] = "Monthly Magazine";
            dtRow["CustId"] = 1003;
            ordersTable.Rows.Add(dtRow);
            // Add two rows to Customer id 1003
            dtRow = ordersTable.NewRow();
            dtRow["OrderId"] = 4;
            dtRow["Name"] = "PCMagazine";
            dtRow["Description"] = "Monthly Magazine";
            dtRow["custId"] = 1003;
            ordersTable.Rows.Add(dtRow);
            dtRow = ordersTable.NewRow();
            dtRow["OrderId"] = 5;
            dtRow["Name"] = "C# Book";
            dtRow["CustId"] = 1003;
            dtRow["Description"] = "2 Day Air ";
            ordersTable.Rows.Add(dtRow);
        }


    }
}
