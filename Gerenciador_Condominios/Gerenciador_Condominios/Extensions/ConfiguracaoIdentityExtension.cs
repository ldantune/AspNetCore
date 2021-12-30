﻿using Microsoft.AspNetCore.Identity;

namespace Gerenciador_Condominios.Extensions
{
    public static class ConfiguracaoIdentityExtension
    {
        public static void ConfigurarNomeUsuario(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(opcoes =>
            {
                opcoes.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                opcoes.User.RequireUniqueEmail = true;
            });
        }

        public static void ConfigurarSenhaUsuario(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(opcoes =>
            {
                opcoes.Password.RequireDigit = false;
                opcoes.Password.RequireLowercase = false;
                opcoes.Password.RequiredLength = 4;
                opcoes.Password.RequireNonAlphanumeric = false;
                opcoes.Password.RequireUppercase = false;
                opcoes.Password.RequiredUniqueChars = 0;
            });
        }
    }
}
