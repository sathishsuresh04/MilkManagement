using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MilkManagement.Data.Migrations
{
    public partial class SeedCoreTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var createdByUserid = Guid.NewGuid();
            migrationBuilder.Sql("insert into core.productarea(Name,CreatedBy,UpdatedBy) values('Milk',"+createdByUserid+","+ createdByUserid + ")");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("DELETE FROM core.product");

            migrationBuilder
                .Sql("DELETE FROM core.milkpricetype");
            migrationBuilder
                .Sql("DELETE FROM core.productarea");
        }
    }
}
