namespace Juan_PB301EmilMusayev.Extensions
{
    public static partial class Extension
    {
        public static bool IsImage(this IFormFile file) => file.ContentType.Contains("image");
        public static bool IsCorrectSize(this IFormFile file, int size) => file.Length / 1024 > size;
        public async static Task<string> SaveFile(this IFormFile file)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img","product", fileName);
            using FileStream fileStream = new(path, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return fileName;
        }
    }
}
