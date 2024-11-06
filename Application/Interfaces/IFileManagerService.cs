namespace Application.Interfaces;

public interface IFileManagerService
{
    void CreateFolder(string path);

    Task<string> SaveFile(Stream file, string path);

    Task DeleteFile(Guid fileId);
}