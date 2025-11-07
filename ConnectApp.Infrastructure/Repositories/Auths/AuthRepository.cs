using ConnectApp.Domain.Entities.Users;
using ConnectApp.Domain.Interfaces.Auths;
using ConnectApp.Infrastructure.Sql;
using Microsoft.Data.SqlClient;
using System.Text;
using ConnectApp.Shared.SqlDataReaderShared;
using System.Data;
using ConnectApp.Application.DTOs.Auths;

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
            cm.Parameters.AddWithValue("@AccessKey", login);

            await using var reader = await cm.ExecuteReaderAsync();
            return await reader.ReadAsync();
        }
        public async Task<User> GetByUserAsync(Auth auth)
        {

            var verification = AuthParams.AccessKey;
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
                .AppendLine("WHERE [AccessKey] COLLATE SQL_Latin1_General_CP1_CS_AS  = @AccessKey  ");


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

            var x = new User
            {
                Id = reader.GetGuidValue("Id"),
                Code = reader.GetInt32("Code"),
                Name = reader.GetStringValue("Name"),
                AccessKey = reader.GetStringValue("AccessKey"),
                Password = reader.GetStringValue("Password"),
                //Emails = reader.GetStringValue("Email"),
                //Phone = reader.GetStringValue("Phone"),
                TypeCode = reader.GetNullableInt("TypeCode"),
                TypeName = reader.GetStringValue("TypeName"),
                ProfileCode = reader.GetNullableInt("ProfileCode"),
                ProfileName = reader.GetStringValue("ProfileName"),
                StatusCode = reader.GetNullableInt("StatusCode"),
                StatusName = reader.GetStringValue("StatusName"),
                LastAccess = reader.GetNullableDateTime("LastAccess"),
                AccessCount = reader.GetNullableInt("AccessCount"),
                Avatar = reader.GetStringValue("Avatar"),
                Note = reader.GetStringValue("Note"),
                BrokerId = reader.GetNullableGuid("BrokerId"),
                AccountId = reader.GetNullableGuid("AccountId"),
                CreationDate = reader.GetNullableDateTime("CreationDate"),
                CreationUserId = reader.GetNullableGuid("CreationUserId"),
                CreationUserName = reader.GetStringValue("CreationUserName"),
                ChangeDate = reader.GetNullableDateTime("ChangeDate"),
                ChangeUserId = reader.GetNullableGuid("ChangeUserId"),
                ChangeUserName = reader.GetStringValue("ChangeUserName"),
                ExclusionDate = reader.GetNullableDateTime("ExclusionDate"),
                ExclusionUserId = reader.GetNullableGuid("ExclusionUserId"),
                ExclusionUserName = reader.GetStringValue("ExclusionUserName"),
                RecordStatus = reader.GetBoolean("RecordStatus"),
                CPF = reader.GetStringValue("CPF"),
                IsActive = reader.GetBoolean("IS_ACTIVE")
            };


            return x;

        }

        
        






        /*
        public Task<User?> GetByUserAsync(string login, string password)




        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByUserAsync(Auth auth)
        {
            throw new NotImplementedException();
        }
        */
    }
}