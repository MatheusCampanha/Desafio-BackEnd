namespace Desafio_BackEnd.Domain.Core.Data
{
    public class Settings
    {
        public string ApplicationName { get; set; } = default!;
        public string ApplicationBasePath { get; set; } = default!;
        public bool ConsoleLog { get; set; }
        public string Token { get; set; } = default!;

        public ConnectionStrings ConnectionStrings { get; set; } = default!;
        public RabbitMQConfigurations RabbitMQConfigurations { get; set; } = default!;
        public Jwt Jwt { get; set; } = default!;
        public S3Settings S3Settings { get; set; } = default!;
    }

    public class ConnectionStrings
    {
        public string DBApplication { get; set; } = default!;
        public string DatabaseName { get; set; } = default!;
    }

    public class RabbitMQConfigurations
    {
        public string Url { get; set; } = default!;
        public int Port { get; set; }
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string RoutingKey { get; set; } = default!;
    }

    public class Jwt
    {
        public string SecretKey { get; set; } = default!;
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
    }

    public class S3Settings
    {
        public string BucketName { get; set; } = default!;
        public string AccessId { get; set; } = default!;
        public string AccessKey { get; set; } = default!;
    }
}