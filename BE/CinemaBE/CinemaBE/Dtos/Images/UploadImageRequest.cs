namespace CinemaBE.Dtos.Images
{
    public class UploadImageRequest
    {
        public IFormFile File { get; set; } = null!;

        public string TableName { get; set; } = null!;

        public int ObjectId { get; set; }

        public string? Name { get; set; }

        public int? SortOrder { get; set; }
    }
}
