using Microsoft.EntityFrameworkCore;

namespace EuPagoAPI.Extensions
{
    public static class DatabaseExtensions
    {
        public static string? TableName(this DbContext context, Type type)
        {
            var entityType = context.Model.FindEntityType(type);

            return entityType?.GetTableName() ?? throw new NullReferenceException($"Não pode encontrar um nome para o tipo {type.Name}");
        }
    }
}
