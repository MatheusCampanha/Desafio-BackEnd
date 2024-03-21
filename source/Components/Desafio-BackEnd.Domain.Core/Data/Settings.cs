namespace Desafio_BackEnd.Domain.Core.Data
{
    public class Settings
    {
        public string ApplicationName { get; set; } = default!;
        public string ApplicationBasePath { get; set; } = default!;
        public bool ConsoleLog { get; set; }
        public string Token { get; set; } = default!;

        public ConnectionStrings ConnectionStrings { get; set; } = default!;
        public S3Settings S3Settings { get; set; } = default!;
    }

    public class ConnectionStrings
    {
        public string DBApplication { get; set; } = default!;
        public string DatabaseName { get; set; } = default!;
    }

    public class S3Settings
    {
        public string BucketName { get; set; } = default!;
        public string Key { get; set; } = default!;
    }
}