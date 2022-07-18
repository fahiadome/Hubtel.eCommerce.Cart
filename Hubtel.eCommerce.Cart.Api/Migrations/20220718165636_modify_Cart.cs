using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hubtel.eCommerce.Cart.Api.Migrations
{
    public partial class modify_Cart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("d02bc9c4-ae75-465d-bb9d-faf637b0d913"));

            migrationBuilder.DeleteData(
                table: "ItemVendors",
                keyColumn: "Id",
                keyValue: new Guid("bf87fbbb-a950-4c75-b47d-93712d6d3204"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("dbbd1c85-3aeb-4e45-8f63-739394008671"));

            migrationBuilder.DropColumn(
                name: "ItemName",
                table: "Carts");

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "AddedBy", "AddedDate", "AddressLine1", "AddressLine2", "AddressLine3", "AddressLine4", "AddressType", "City", "Country", "CustomerId", "DateModified", "Email", "ItemVendorId", "ModifiedBy", "PhoneNumber", "State", "ZipCode" },
                values: new object[] { new Guid("085626ed-d51c-41a4-a1fc-e605c958c03e"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "P.O. Box 22", "Lowcost", "", "", 1, "Tarkwa", "Ghana", new Guid("9de9254d-c3dc-4af2-810c-e924682dc173"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "abc@abc.com", null, null, "0244444444", "Western", "233" });

            migrationBuilder.InsertData(
                table: "ItemVendors",
                columns: new[] { "Id", "AddedBy", "AddedDate", "Code", "DateModified", "Description", "ModifiedBy", "Name" },
                values: new object[] { new Guid("e331dc75-3f47-425c-9931-acb73b2b7cd0"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "IT102", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Some other description", null, "Vendor two" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "AddedBy", "AddedDate", "DateModified", "Description", "ItemCategoryId", "ItemName", "ModifiedBy", "Quantity", "SKU", "Status", "UnitPrice", "VendorId" },
                values: new object[] { new Guid("a6651de2-3ddc-43a1-8d79-07f91f835fd3"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new Guid("27c3ccd2-90bf-4a36-a424-b0d47feb257f"), "Item one", null, 30, "sku101", 1, 12m, new Guid("f2562115-0b80-479b-94fb-71d03319d574") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("085626ed-d51c-41a4-a1fc-e605c958c03e"));

            migrationBuilder.DeleteData(
                table: "ItemVendors",
                keyColumn: "Id",
                keyValue: new Guid("e331dc75-3f47-425c-9931-acb73b2b7cd0"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("a6651de2-3ddc-43a1-8d79-07f91f835fd3"));

            migrationBuilder.AddColumn<string>(
                name: "ItemName",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "AddedBy", "AddedDate", "AddressLine1", "AddressLine2", "AddressLine3", "AddressLine4", "AddressType", "City", "Country", "CustomerId", "DateModified", "Email", "ItemVendorId", "ModifiedBy", "PhoneNumber", "State", "ZipCode" },
                values: new object[] { new Guid("d02bc9c4-ae75-465d-bb9d-faf637b0d913"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "P.O. Box 22", "Lowcost", "", "", 1, "Tarkwa", "Ghana", new Guid("9de9254d-c3dc-4af2-810c-e924682dc173"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "abc@abc.com", null, null, "0244444444", "Western", "233" });

            migrationBuilder.InsertData(
                table: "ItemVendors",
                columns: new[] { "Id", "AddedBy", "AddedDate", "Code", "DateModified", "Description", "ModifiedBy", "Name" },
                values: new object[] { new Guid("bf87fbbb-a950-4c75-b47d-93712d6d3204"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "IT102", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Some other description", null, "Vendor two" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "AddedBy", "AddedDate", "DateModified", "Description", "ItemCategoryId", "ItemName", "ModifiedBy", "Quantity", "SKU", "Status", "UnitPrice", "VendorId" },
                values: new object[] { new Guid("dbbd1c85-3aeb-4e45-8f63-739394008671"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new Guid("27c3ccd2-90bf-4a36-a424-b0d47feb257f"), "Item one", null, 30, "sku101", 1, 12m, new Guid("f2562115-0b80-479b-94fb-71d03319d574") });
        }
    }
}
