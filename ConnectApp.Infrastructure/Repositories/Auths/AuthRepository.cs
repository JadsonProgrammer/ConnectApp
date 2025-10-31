using ConnectApp.Application.DTOs.Auths;
using ConnectApp.Domain.Entities.Users;
using ConnectApp.Domain.Interfaces.Auths;
using ConnectApp.Infrastructure.Sql;
using Microsoft.Data.SqlClient;
using System.Text;

namespace ConnectApp.Infrastructure.Repositories.Auths
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConnectionProvider _connectionProvider;

        public AuthRepository(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public async Task<bool> GetByUsernameAsync(string login)
        {
            var commandText = new StringBuilder()
                .AppendLine("SELECT * FROM ")
                .AppendLine("[JAYTECHAPPDB].[DBO].[USER] ")
                .AppendLine("WHERE [AccessKey] COLLATE SQL_Latin1_General_CP1_CS_AS  = @AccessKey  ");

            await using var cn = _connectionProvider.GetConnection();
            await cn.OpenAsync();
            await using var cm = cn.CreateCommand();
            cm.CommandText = commandText.ToString();
            cm.Parameters.AddWithValue("@access_key", login);

            await using var reader = await cm.ExecuteReaderAsync();
            return await reader.ReadAsync();
        }
        public async Task<User> GetByUserAsync(AuthParams authParams)
        {

            var verification = authParams.AccessKey;
            var commandText = new StringBuilder()
                .AppendLine("SELECT")
                .AppendLine("    [Id],")
                .AppendLine("    [Code],")
                .AppendLine("    [Name],")
                .AppendLine("    [AccessKey],")
                .AppendLine("    [Password],")
                .AppendLine("    [Email],")
                .AppendLine("    [Phone],")
                .AppendLine("    [TypeCode],")
                .AppendLine("    [TypeName],")
                .AppendLine("    [ProfileCode],")
                .AppendLine("    [ProfileName],")
                .AppendLine("    [StatusCode],")
                .AppendLine("    [StatusName],")
                .AppendLine("    [LastAccess],")
                .AppendLine("    [AccessCount],")
                .AppendLine("    [Avatar],")
                .AppendLine("    [Note],")
                .AppendLine("    [BrokerId],")
                .AppendLine("    [AccountId],")
                .AppendLine("    [CreationDate],")
                .AppendLine("    [CreationUserId],")
                .AppendLine("    [CreationUserName],")
                .AppendLine("    [ChangeDate],")
                .AppendLine("    [ChangeUserId],")
                .AppendLine("    [ChangeUserName],")
                .AppendLine("    [ExclusionDate],")
                .AppendLine("    [ExclusionUserId],")
                .AppendLine("    [ExclusionUserName],")
                .AppendLine("    [RecordStatus],")
                .AppendLine("    [CPF],")
                .AppendLine("    [IS_ACTIVE]")
                .AppendLine(" FROM [JAYTECHAPPDB].[DBO].[USER]")
                .AppendLine("WHERE [AccessKey] = @AccessKey");


            await using var cn = _connectionProvider.GetConnection();
            await cn.OpenAsync();
            await using var cm = cn.CreateCommand();
            cm.CommandText = commandText.ToString();
            cm.Parameters.AddWithValue("@AccessKey", verification);


            await using var reader = await cm.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                var x = GetDataRecord(reader);
                return x;

            }
            return null;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var commandText = new StringBuilder()

                .AppendLine("SELECT * FROM [JAYTECHAPPDB].[DBO].[USER] WHERE [id] = @id;");
            await using var cn = _connectionProvider.GetConnection();
            await cn.OpenAsync();
            await using var cm = cn.CreateCommand();
            cm.CommandText = commandText.ToString();
            cm.Parameters.AddWithValue("@id", id);

            await using var reader = await cm.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return GetDataRecord(reader);
            return null!;
        }

        private static User GetDataRecord(SqlDataReader reader)
        {

            var x = new User();


            x.Id = reader.GetGuidValue("Id");
            x.Code = reader.GetInt32("Code");
            x.Name = reader.GetStringValue("Name");
            x.AccessKey = reader.GetStringValue("AccessKey");
            x.Password = reader.GetStringValue("Password");
            x.Email = reader.GetStringValue("Email");
            x.Phone = reader.GetStringValue("Phone");
            x.TypeCode = reader.GetNullableInt("TypeCode");
            x.TypeName = reader.GetStringValue("TypeName");
            x.ProfileCode = reader.GetNullableInt("ProfileCode");
            x.ProfileName = reader.GetStringValue("ProfileName");
            x.StatusCode = reader.GetNullableInt("StatusCode");
            x.StatusName = reader.GetStringValue("StatusName");
            x.LastAccess = reader.GetNullableDateTime("LastAccess");
            x.AccessCount = reader.GetNullableInt("AccessCount");
            x.Avatar = reader.GetStringValue("Avatar");
            x.Note = reader.GetStringValue("Note");
            x.BrokerId = reader.GetNullableGuid("BrokerId");
            x.AccountId = reader.GetNullableGuid("AccountId");
            x.CreationDate = reader.GetNullableDateTime("CreationDate");
            x.CreationUserId = reader.GetNullableGuid("CreationUserId");
            x.CreationUserName = reader.GetStringValue("CreationUserName");
            x.ChangeDate = reader.GetNullableDateTime("ChangeDate");
            x.ChangeUserId = reader.GetNullableGuid("ChangeUserId");
            x.ChangeUserName = reader.GetStringValue("ChangeUserName");
            x.ExclusionDate = reader.GetNullableDateTime("ExclusionDate");
            x.ExclusionUserId = reader.GetNullableGuid("ExclusionUserId");
            x.ExclusionUserName = reader.GetStringValue("ExclusionUserName");
            x.RecordStatus = reader.GetBoolean("RecordStatus");
            x.CPF = reader.GetStringValue("CPF");
            x.IsActive = reader.GetInt32("IS_ACTIVE");


            return x;

        }
    }
}