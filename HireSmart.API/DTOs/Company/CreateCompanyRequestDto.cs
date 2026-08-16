namespace HireSmart.API.DTOs.Company
{
    public class CreateCompanyRequestDto
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Website { get; set; } = string.Empty;

        public string Industry { get; set; } = string.Empty;

    }
}