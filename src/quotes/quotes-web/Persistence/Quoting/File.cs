namespace quotes_web.Persistence.Quoting;

public class File
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string FileType { get; init; }
    public string FilePath => $"/images/{this.FileName}";
    public string FileName => $"{this.Id}.{this.FileType.Split('/')[1]}";

    public async Task<string> GetImgSourceString()
    {
        var base64 = Convert.ToBase64String(await System.IO.File.ReadAllBytesAsync(this.FilePath));
        return $"data:{this.FileType};base64,{base64}";
    }
}
