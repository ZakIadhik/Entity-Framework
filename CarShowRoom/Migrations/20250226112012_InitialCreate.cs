using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarShowRoom.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarBrands",
                columns: table => new
                {
                    CarBrandId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarBrands", x => x.CarBrandId);
                });

            migrationBuilder.CreateTable(
                name: "CarTypes",
                columns: table => new
                {
                    CarTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarTypes", x => x.CarTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    CarTypeId = table.Column<int>(type: "int", nullable: false),
                    CarBrandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarId);
                    table.ForeignKey(
                        name: "FK_Cars_CarBrands_CarBrandId",
                        column: x => x.CarBrandId,
                        principalTable: "CarBrands",
                        principalColumn: "CarBrandId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cars_CarTypes_CarTypeId",
                        column: x => x.CarTypeId,
                        principalTable: "CarTypes",
                        principalColumn: "CarTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cars_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    SaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    CustomerId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.SaleId);
                    table.ForeignKey(
                        name: "FK_Sales_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                    table.ForeignKey(
                        name: "FK_Sales_Customers_CustomerId1",
                        column: x => x.CustomerId1,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                    table.ForeignKey(
                        name: "FK_Sales_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceHistories",
                columns: table => new
                {
                    ServiceHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServiceDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceHistories", x => x.ServiceHistoryId);
                    table.ForeignKey(
                        name: "FK_ServiceHistories_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CarBrands",
                columns: new[] { "CarBrandId", "BrandName" },
                values: new object[,]
                {
                    { 1, "Toyota" },
                    { 2, "BMW" },
                    { 3, "Mercedes" }
                });

            migrationBuilder.InsertData(
                table: "CarTypes",
                columns: new[] { "CarTypeId", "TypeName" },
                values: new object[,]
                {
                    { 1, "Sedan" },
                    { 2, "SUV" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "john.doe@gmail.com", "John", "Doe", "1234567890" },
                    { 2, "jane.smith@yahoo.com", "Jane", "Smith", "0987654321" },
                    { 3, "michael.johnson@outlook.com", "Michael", "Johnson", "1112223333" },
                    { 4, "emily.brown@mail.com", "Emily", "Brown", "4445556666" },
                    { 5, "david.williams@gmail.com", "David", "Williams", "7778889999" },
                    { 6, "sophia.miller@live.com", "Sophia", "Miller", "5554443333" },
                    { 7, "daniel.anderson@gmail.com", "Daniel", "Anderson", "2221110000" },
                    { 8, "olivia.martinez@aol.com", "Olivia", "Martinez", "6667778888" },
                    { 9, "matthew.garcia@gmail.com", "Matthew", "Garcia", "9998887777" },
                    { 10, "emma.lopez@yahoo.com", "Emma", "Lopez", "3332221111" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "FirstName", "LastName", "Salary" },
                values: new object[,]
                {
                    { 1, "Robert", "Taylor", 5000m },
                    { 2, "Linda", "Harris", 5500m },
                    { 3, "William", "Clark", 6000m }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "CarId", "Brand", "CarBrandId", "CarTypeId", "CustomerId", "Model", "Price", "Year" },
                values: new object[,]
                {
                    { 1, "Toyota", 1, 1, null, "Camry", 30000m, 2021 },
                    { 2, "BMW", 2, 2, null, "X5", 60000m, 2022 },
                    { 3, "Mercedes", 3, 1, null, "C-Class", 45000m, 2020 },
                    { 4, "Toyota", 1, 2, null, "RAV4", 35000m, 2023 },
                    { 5, "BMW", 2, 1, null, "3 Series", 40000m, 2019 }
                });

            migrationBuilder.InsertData(
                table: "Sales",
                columns: new[] { "SaleId", "CarId", "CustomerId", "CustomerId1", "EmployeeId", "SaleDate", "SalePrice" },
                values: new object[,]
                {
                    { 1, 1, 1, null, 1, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 29000m },
                    { 2, 2, 2, null, 2, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 58000m },
                    { 3, 3, 3, null, 3, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 43000m },
                    { 4, 4, 4, null, 1, new DateTime(2024, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 34000m },
                    { 5, 5, 5, null, 2, new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 39000m }
                });

            migrationBuilder.InsertData(
                table: "ServiceHistories",
                columns: new[] { "ServiceHistoryId", "CarId", "ServiceDate", "ServiceDescription" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Routine maintenance, oil change" },
                    { 2, 2, new DateTime(2023, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brake pad replacement" },
                    { 3, 3, new DateTime(2023, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Engine diagnostics" },
                    { 4, 4, new DateTime(2023, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wheel balancing" },
                    { 5, 5, new DateTime(2023, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Spark plug replacement" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarBrandId",
                table: "Cars",
                column: "CarBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarTypeId",
                table: "Cars",
                column: "CarTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CustomerId",
                table: "Cars",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CarId",
                table: "Sales",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CustomerId",
                table: "Sales",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CustomerId1",
                table: "Sales",
                column: "CustomerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_EmployeeId",
                table: "Sales",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceHistories_CarId",
                table: "ServiceHistories",
                column: "CarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "ServiceHistories");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "CarBrands");

            migrationBuilder.DropTable(
                name: "CarTypes");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
