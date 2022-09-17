﻿using System.Text.Json;

namespace ExpirationDateControl_API.Logs
{
    public class CustomLogs
    {
        const string PUT = "put";
        const string PATCH = "patch";
        const string DELETE = "delete";

        public static void SaveLog(int? id, string message, string title, string method, object? entityBefore = null, object entityAfter = null)
        {
            var now = DateTime.Now.ToString("G");

            if (method.Equals(PUT, StringComparison.InvariantCultureIgnoreCase) || method.Equals(PATCH, StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine($"{now} - {message} {id} - {title} - Alterado de {JsonSerializer.Serialize(entityBefore)} para {JsonSerializer.Serialize(entityAfter)}");
            }
            else if (method.Equals(DELETE, StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine($"{now} - {message} {id} - {title} - Removido");
            }
        }
    }
}
