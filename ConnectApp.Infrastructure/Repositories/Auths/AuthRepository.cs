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
                .AppendLine("SELECT")
                .AppendLine("    [Id]  ")
                .AppendLine("    ,[Code]  ")
                .AppendLine("    ,[Name]  ")
                .AppendLine("    ,[CPF]  ")
                .AppendLine("    ,[AccessKey]  ")
                .AppendLine("    ,[Password]  ")
                .AppendLine("    ,[TypeCode]  ")
                .AppendLine("    ,[TypeName]  ")
                .AppendLine("    ,[ProfileCode]  ")
                .AppendLine("    ,[ProfileName]  ")
                .AppendLine("    ,[StatusCode]  ")
                .AppendLine("    ,[StatusName]  ")
                .AppendLine("    ,[LastAccess]  ")
                .AppendLine("    ,[AccessCount]  ")
                .AppendLine("    ,[Avatar]  ")
                .AppendLine("    ,[Note]  ")
                .AppendLine("    ,[BrokerId]  ")
                .AppendLine("    ,[AccountId]  ")
                .AppendLine("    ,[AccountName]  ")
                .AppendLine("    ,[CreationDate]  ")
                .AppendLine("    ,[CreationUserId]  ")
                .AppendLine("    ,[CreationUserName]  ")
                .AppendLine("    ,[ChangeDate]  ")
                .AppendLine("    ,[ChangeUserId]  ")
                .AppendLine("    ,[ChangeUserName]  ")
                .AppendLine("    ,[ExclusionDate]  ")
                .AppendLine("    ,[ExclusionUserId]  ")
                .AppendLine("    ,[ExclusionUserName] ")
                .AppendLine("    ,[RecordStatus] ")
                .AppendLine("    ,[IS_ACTIVE] ")
                .AppendLine("     FROM [JAYTECHAPPDB].[DBO].[USER] ")
                .AppendLine("     WHERE [AccessKey] COLLATE SQL_Latin1_General_CP1_CS_AS  = @AccessKey  ");

            await using var cn = _connectionProvider.GetConnection();
            await cn.OpenAsync();
            await using var cm = cn.CreateCommand();
            cm.CommandText = commandText.ToString();
            cm.Parameters.AddWithValue("@AccessKey", login);

            await using var reader = await cm.ExecuteReaderAsync();
            return await reader.ReadAsync();
        }
        public async Task<User?> GetByUserAsync(string auth)
        {

            var verification = auth;
            var commandText = new StringBuilder()
                .AppendLine("SELECT")
                .AppendLine("    [Id]  ")
                .AppendLine("    ,[Code]  ")
                .AppendLine("    ,[Name]  ")
                .AppendLine("    ,[CPF]  ")
                .AppendLine("    ,[AccessKey]  ")
                .AppendLine("    ,[Password]  ")
                .AppendLine("    ,[TypeCode]  ")
                .AppendLine("    ,[TypeName]  ")
                .AppendLine("    ,[ProfileCode]  ")
                .AppendLine("    ,[ProfileName]  ")
                .AppendLine("    ,[StatusCode]  ")
                .AppendLine("    ,[StatusName]  ")
                .AppendLine("    ,[LastAccess]  ")
                .AppendLine("    ,[AccessCount]  ")
                .AppendLine("    ,[Avatar]  ")
                .AppendLine("    ,[Note]  ")
                .AppendLine("    ,[BrokerId]  ")
                .AppendLine("    ,[AccountId]  ")
                .AppendLine("    ,[AccountName]  ")
                .AppendLine("    ,[CreationDate]  ")
                .AppendLine("    ,[CreationUserId]  ")
                .AppendLine("    ,[CreationUserName]  ")
                .AppendLine("    ,[ChangeDate]  ")
                .AppendLine("    ,[ChangeUserId]  ")
                .AppendLine("    ,[ChangeUserName]  ")
                .AppendLine("    ,[ExclusionDate]  ")
                .AppendLine("    ,[ExclusionUserId]  ")
                .AppendLine("    ,[ExclusionUserName] ")
                .AppendLine("    ,[RecordStatus] ")
                .AppendLine("    ,[IS_ACTIVE] ")
                .AppendLine("     FROM [JAYTECHAPPDB].[DBO].[USER] ")
                .AppendLine("     WHERE [AccessKey] COLLATE SQL_Latin1_General_CP1_CS_AS  = @AccessKey  ");


            await using var cn = _connectionProvider.GetConnection();
            await cn.OpenAsync();
            await using var cm = cn.CreateCommand();
            cm.CommandText = commandText.ToString();
            cm.Parameters.AddWithValue("@AccessKey", verification);


            await using var reader = await cm.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return GetDataRecord(reader);
                 

            }
            return null;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var commandText = new StringBuilder()


                .AppendLine("SELECT")
                .AppendLine("    [Id]  ")
                .AppendLine("    ,[Code]  ")
                .AppendLine("    ,[Name]  ")
                .AppendLine("    ,[CPF]  ")
                .AppendLine("    ,[AccessKey]  ")
                .AppendLine("    ,[Password]  ")
                .AppendLine("    ,[TypeCode]  ")
                .AppendLine("    ,[TypeName]  ")
                .AppendLine("    ,[ProfileCode]  ")
                .AppendLine("    ,[ProfileName]  ")
                .AppendLine("    ,[StatusCode]  ")
                .AppendLine("    ,[StatusName]  ")
                .AppendLine("    ,[LastAccess]  ")
                .AppendLine("    ,[AccessCount]  ")
                .AppendLine("    ,[Avatar]  ")
                .AppendLine("    ,[Note]  ")
                .AppendLine("    ,[BrokerId]  ")
                .AppendLine("    ,[AccountId]  ")
                .AppendLine("    ,[AccountName]  ")
                .AppendLine("    ,[CreationDate]  ")
                .AppendLine("    ,[CreationUserId]  ")
                .AppendLine("    ,[CreationUserName]  ")
                .AppendLine("    ,[ChangeDate]  ")
                .AppendLine("    ,[ChangeUserId]  ")
                .AppendLine("    ,[ChangeUserName]  ")
                .AppendLine("    ,[ExclusionDate]  ")
                .AppendLine("    ,[ExclusionUserId]  ")
                .AppendLine("    ,[ExclusionUserName] ")
                .AppendLine("    ,[RecordStatus] ")
                .AppendLine("    ,[IS_ACTIVE] ")
                .AppendLine("     FROM [JAYTECHAPPDB].[DBO].[USER] ")
                .AppendLine("     WHERE [id] = @id;");

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

            return new User
            {
                Id = reader.GetGuidValue("Id"),
                Code = reader.GetNullableInt("Code") ?? 0,
                Name = reader.GetStringValue("Name"),
                CPF = reader.GetNullableString("CPF"),
                AccessKey = reader.GetStringValue("AccessKey"),
                Password = reader.GetStringValue("Password"),

                TypeCode = reader.GetNullableInt("TypeCode"),
                TypeName = reader.GetNullableString("TypeName"),
                ProfileCode = reader.GetNullableInt("ProfileCode"),
                ProfileName = reader.GetNullableString("ProfileName"),
                StatusCode = reader.GetNullableInt("StatusCode"),
                StatusName = reader.GetNullableString("StatusName"),

                LastAccess = reader.GetNullableDateTime("LastAccess"),
                AccessCount = reader.GetNullableInt("AccessCount"),

                Avatar = reader.GetNullableString("Avatar"),
                Note = reader.GetNullableString("Note"),

                BrokerId = reader.GetNullableGuid("BrokerId"),
                AccountId = reader.GetNullableGuid("AccountId"),
                AccountName = reader.GetNullableString("AccountName"),

                CreationDate = reader.GetNullableDateTime("CreationDate"),
                CreationUserId = reader.GetNullableGuid("CreationUserId"),
                CreationUserName = reader.GetNullableString("CreationUserName"),

                ChangeDate = reader.GetNullableDateTime("ChangeDate"),
                ChangeUserId = reader.GetNullableGuid("ChangeUserId"),
                ChangeUserName = reader.GetNullableString("ChangeUserName"),

                ExclusionDate = reader.GetNullableDateTime("ExclusionDate"),
                ExclusionUserId = reader.GetNullableGuid("ExclusionUserId"),
                ExclusionUserName = reader.GetNullableString("ExclusionUserName"),

                RecordStatus = reader.GetBoolean("RecordStatus"),
                IsActive = reader.GetBoolean("IS_ACTIVE")
            };

        }

       









      
    }
}