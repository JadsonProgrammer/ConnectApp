using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectApp.Shared.SqlDataReaderShared
{
    public static class SqlDataReaderExtensions
    {

        public static Guid? GetNullableGuid(this SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            // Se o valor for DBNull, retorna null. Caso contrário, retorna o GUID.
            return reader.IsDBNull(ordinal) ? (Guid?)null : reader.GetGuid(ordinal);
        }

        public static Guid GetGuidValue(this SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            // Assume que o valor não é DBNull. Se for, causará um erro.
            // Use com colunas NOT NULL no DB.
            return reader.GetGuid(ordinal);
        }

        public static int? GetNullableInt(this SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            // Se o valor for DBNull, retorna null. Caso contrário, retorna o Int32.
            return reader.IsDBNull(ordinal) ? (int?)null : reader.GetInt32(ordinal);
        }

        public static short GetShortValue(this SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            // Assume que o valor não é DBNull. Se for, causará um erro.
            // Use com colunas NOT NULL no DB.
            return reader.GetInt16(ordinal);
        }

        public static int GetIntValue(this SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            // Assume que o valor não é DBNull. Se for, causará um erro.
            // Use com colunas NOT NULL no DB.
            return reader.GetInt32(ordinal);
        }

        public static DateTime? GetNullableDateTime(this SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            // Se o valor for DBNull, retorna null. Caso contrário, retorna o DateTime.
            return reader.IsDBNull(ordinal) ? (DateTime?)null : reader.GetDateTime(ordinal);
        }

        public static string? GetNullableString(this SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            // Se o valor for DBNull, retorna null. Caso contrário, retorna a string.
            return reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
        }

        public static string GetStringValue(this SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            // Se o valor for DBNull, retorna string.Empty. Caso contrário, retorna a string.
            return reader.IsDBNull(ordinal) ? string.Empty : reader.GetString(ordinal);
        }
    }

}
