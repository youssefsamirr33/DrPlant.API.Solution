using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talabat.Infrastructure.Generic_Repository.Data.Migrations
{
    public partial class AddPlantDiseasesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlantDiseases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    plant_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    disease_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prevention_tips = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    treatment_methods = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    organic_options = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    chemical_options = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    diagnostic_images_url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantDiseases", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlantDiseases");
        }
    }
}
