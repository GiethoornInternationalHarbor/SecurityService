using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SecurityService.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Container",
                columns: table => new
                {
                    Number = table.Column<string>(nullable: false),
                    ProductName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Container", x => x.Number);
                    table.ForeignKey(
                        name: "FK_Container_Product_ProductName",
                        column: x => x.ProductName,
                        principalTable: "Product",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trucks",
                columns: table => new
                {
                    LicensePlate = table.Column<string>(nullable: false),
                    ContainerNumber = table.Column<string>(nullable: true),
                    SecurityStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trucks", x => x.LicensePlate);
                    table.ForeignKey(
                        name: "FK_Trucks_Container_ContainerNumber",
                        column: x => x.ContainerNumber,
                        principalTable: "Container",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Container_ProductName",
                table: "Container",
                column: "ProductName");

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_ContainerNumber",
                table: "Trucks",
                column: "ContainerNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trucks");

            migrationBuilder.DropTable(
                name: "Container");

            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
