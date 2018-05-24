using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SecurityService.Infrastructure.Migrations
{
    public partial class FixContainers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Container_Product_ProductName",
                table: "Container");

            migrationBuilder.DropForeignKey(
                name: "FK_Trucks_Container_ContainerNumber",
                table: "Trucks");

            migrationBuilder.DropIndex(
                name: "IX_Trucks_ContainerNumber",
                table: "Trucks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Container",
                table: "Container");

            migrationBuilder.DropIndex(
                name: "IX_Container_ProductName",
                table: "Container");

            migrationBuilder.DropColumn(
                name: "ContainerNumber",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Container");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Container",
                newName: "SerialShippingContainerCode");

            migrationBuilder.AddColumn<Guid>(
                name: "ContainerId",
                table: "Trucks",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Product",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Product",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ContainerId",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductType",
                table: "Product",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "SerialShippingContainerCode",
                table: "Container",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Container",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "ContainerType",
                table: "Container",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Container",
                table: "Container",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_ContainerId",
                table: "Trucks",
                column: "ContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ContainerId",
                table: "Product",
                column: "ContainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Container_ContainerId",
                table: "Product",
                column: "ContainerId",
                principalTable: "Container",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trucks_Container_ContainerId",
                table: "Trucks",
                column: "ContainerId",
                principalTable: "Container",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Container_ContainerId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Trucks_Container_ContainerId",
                table: "Trucks");

            migrationBuilder.DropIndex(
                name: "IX_Trucks_ContainerId",
                table: "Trucks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ContainerId",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Container",
                table: "Container");

            migrationBuilder.DropColumn(
                name: "ContainerId",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ContainerId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductType",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Container");

            migrationBuilder.DropColumn(
                name: "ContainerType",
                table: "Container");

            migrationBuilder.RenameColumn(
                name: "SerialShippingContainerCode",
                table: "Container",
                newName: "ProductName");

            migrationBuilder.AddColumn<string>(
                name: "ContainerNumber",
                table: "Trucks",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Product",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Product",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Container",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Container",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Container",
                table: "Container",
                column: "Number");

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_ContainerNumber",
                table: "Trucks",
                column: "ContainerNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Container_ProductName",
                table: "Container",
                column: "ProductName");

            migrationBuilder.AddForeignKey(
                name: "FK_Container_Product_ProductName",
                table: "Container",
                column: "ProductName",
                principalTable: "Product",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trucks_Container_ContainerNumber",
                table: "Trucks",
                column: "ContainerNumber",
                principalTable: "Container",
                principalColumn: "Number",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
