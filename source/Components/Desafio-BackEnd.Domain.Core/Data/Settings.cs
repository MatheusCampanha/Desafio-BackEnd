﻿namespace Desafio_BackEnd.Domain.Core.Data
{
    public class Settings
    {
        public string ApplicationName { get; set; } = default!;
        public string ApplicationBasePath { get; set; } = default!;
        public bool ConsoleLog { get; set; }
        public string Token { get; set; } = default!;

        public ConnectionStrings ConnectionStrings { get; set; } = default!;
    }

    public class ConnectionStrings
    {
        public string DBApplication { get; set; } = default!;
    }
}