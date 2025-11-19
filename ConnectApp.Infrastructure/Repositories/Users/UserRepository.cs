using ConnectApp.Domain.Entities.Users;
using ConnectApp.Domain.Interfaces.Users;
using ConnectApp.Infrastructure.Sql;
using ConnectApp.Shared.Results;
using ConnectApp.Shared.SqlDataReaderShared;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Text;

namespace ConnectApp.Infrastructure.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly IConnectionProvider _connectionProvider;

        public UserRepository(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        //---------------------------Get-----------------------------//
        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            var commandText = new StringBuilder()
               .AppendLine("SELECT")
               .AppendLine("    [Id]")
               .AppendLine("    ,[Code]")
               .AppendLine("    ,[Name]")
               .AppendLine("    ,[CPF]")
               .AppendLine("    ,[AccessKey]")
               .AppendLine("    ,[Password]")
               .AppendLine("    ,[TypeCode]")
               .AppendLine("    ,[TypeName]")
               .AppendLine("    ,[ProfileCode]")
               .AppendLine("    ,[ProfileName]")
               .AppendLine("    ,[StatusCode]")
               .AppendLine("    ,[StatusName]")
               .AppendLine("    ,[LastAccess]")
               .AppendLine("    ,[AccessCount]")
               .AppendLine("    ,[Avatar]")
               .AppendLine("    ,[Note]")
               .AppendLine("    ,[BrokerId]")
               .AppendLine("    ,[AccountId]")
               .AppendLine("    ,[AccountName]")
               .AppendLine("    ,[CreationDate]")
               .AppendLine("    ,[CreationUserId]")
               .AppendLine("    ,[CreationUserName]")
               .AppendLine("    ,[ChangeDate]")
               .AppendLine("    ,[ChangeUserId]")
               .AppendLine("    ,[ChangeUserName]")
               .AppendLine("    ,[ExclusionDate]")
               .AppendLine("    ,[ExclusionUserId]")
               .AppendLine("    ,[ExclusionUserName]")
               .AppendLine("    ,[RecordStatus]")
               .AppendLine("    ,[IS_ACTIVE]")
               .AppendLine("FROM [JAYTECHAPPDB].[DBO].[USER]")
               .AppendLine("WHERE [Id] = @Id");

            await using var cn = _connectionProvider.GetConnection();
            await cn.OpenAsync();
            await using var cm = cn.CreateCommand();
            cm.CommandText = commandText.ToString();
            cm.Parameters.AddWithValue("@Id", id);

            await using var reader = await cm.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return GetDataRecord(reader);
            }

            return null;
        }

        public async Task<IList<User>> GetAllUserAsync()
        {
            var users = new List<User>();
            var commandText = new StringBuilder()
                .AppendLine("SELECT")
                .AppendLine("    [Id]")
                .AppendLine("    ,[Code]")
                .AppendLine("    ,[Name]")
                .AppendLine("    ,[CPF]")
                .AppendLine("    ,[AccessKey]")
                .AppendLine("    ,[Password]")
                .AppendLine("    ,[TypeCode]")
                .AppendLine("    ,[TypeName]")
                .AppendLine("    ,[ProfileCode]")
                .AppendLine("    ,[ProfileName]")
                .AppendLine("    ,[StatusCode]")
                .AppendLine("    ,[StatusName]")
                .AppendLine("    ,[LastAccess]")
                .AppendLine("    ,[AccessCount]")
                .AppendLine("    ,[Avatar]")
                .AppendLine("    ,[Note]")
                .AppendLine("    ,[BrokerId]")
                .AppendLine("    ,[AccountId]")
                .AppendLine("    ,[AccountName]")
                .AppendLine("    ,[CreationDate]")
                .AppendLine("    ,[CreationUserId]")
                .AppendLine("    ,[CreationUserName]")
                .AppendLine("    ,[ChangeDate]")
                .AppendLine("    ,[ChangeUserId]")
                .AppendLine("    ,[ChangeUserName]")
                .AppendLine("    ,[ExclusionDate]")
                .AppendLine("    ,[ExclusionUserId]")
                .AppendLine("    ,[ExclusionUserName]")
                .AppendLine("    ,[RecordStatus]")
                .AppendLine("    ,[IS_ACTIVE]")
                .AppendLine("FROM [JAYTECHAPPDB].[DBO].[USER]")
                .AppendLine("WHERE [RecordStatus] = 1");

            await using var cn = _connectionProvider.GetConnection();
            await cn.OpenAsync();
            await using var cm = cn.CreateCommand();
            cm.CommandText = commandText.ToString();

            await using var reader = await cm.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                users.Add(GetDataRecord(reader));
            }

            return users;
        }

        public async Task<bool?> GetUserByAccessKeyAsync(string accessKey)
        {
            var commandText = new StringBuilder()
                .AppendLine("SELECT")
                .AppendLine("    [Id]")
                .AppendLine("    ,[Code]")
                .AppendLine("    ,[Name]")
                .AppendLine("    ,[CPF]")
                .AppendLine("    ,[AccessKey]")
                .AppendLine("    ,[Password]")
                .AppendLine("    ,[TypeCode]")
                .AppendLine("    ,[TypeName]")
                .AppendLine("    ,[ProfileCode]")
                .AppendLine("    ,[ProfileName]")
                .AppendLine("    ,[StatusCode]")
                .AppendLine("    ,[StatusName]")
                .AppendLine("    ,[LastAccess]")
                .AppendLine("    ,[AccessCount]")
                .AppendLine("    ,[Avatar]")
                .AppendLine("    ,[Note]")
                .AppendLine("    ,[BrokerId]")
                .AppendLine("    ,[AccountId]")
                .AppendLine("    ,[AccountName]")
                .AppendLine("    ,[CreationDate]")
                .AppendLine("    ,[CreationUserId]")
                .AppendLine("    ,[CreationUserName]")
                .AppendLine("    ,[ChangeDate]")
                .AppendLine("    ,[ChangeUserId]")
                .AppendLine("    ,[ChangeUserName]")
                .AppendLine("    ,[ExclusionDate]")
                .AppendLine("    ,[ExclusionUserId]")
                .AppendLine("    ,[ExclusionUserName]")
                .AppendLine("    ,[RecordStatus]")
                .AppendLine("    ,[IS_ACTIVE]")
                .AppendLine("FROM [JAYTECHAPPDB].[DBO].[USER]")

                .AppendLine("WHERE 1 = 1 ")
                .AppendLine("AND [AccessKey] COLLATE SQL_Latin1_General_CP1_CS_AS = @AccessKey")
                .AppendLine("AND [RecordStatus] = 1");
            //.AppendLine("WHERE [AccessKey] COLLATE Latin1_General_CS_AS = @AccessKey")
            //.AppendLine("AND [RecordStatus] = 1");

            await using var cn = _connectionProvider.GetConnection();
            await cn.OpenAsync();
            await using var cm = cn.CreateCommand();
            cm.CommandText = commandText.ToString();
            cm.Parameters.AddWithValue("@AccessKey", accessKey);

            await using var reader = await cm.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                GetDataRecord(reader);
            }

            return null;
        }

        //---------------------------Create-----------------------------//
        public async Task<User> CreateUserAsync(User user)
        {
            var commandText = new StringBuilder()
              .AppendLine("INSERT INTO [JAYTECHAPPDB].[DBO].[USER]")
              .AppendLine("(")
              .AppendLine("    [Id],")
              .AppendLine("    [Code],")
              .AppendLine("    [Name],")
              .AppendLine("    [CPF],")
              .AppendLine("    [AccessKey],")
              .AppendLine("    [Password],")
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
              .AppendLine("    [AccountName],")
              .AppendLine("    [CreationDate],")
              .AppendLine("    [CreationUserId],")
              .AppendLine("    [CreationUserName],")
              .AppendLine("    [RecordStatus],")
              .AppendLine("    [IS_ACTIVE]")
              .AppendLine(") VALUES (")
              .AppendLine("    @Id,")
              .AppendLine("    @Code,")
              .AppendLine("    @Name,")
              .AppendLine("    @CPF,")
              .AppendLine("    @AccessKey,")
              .AppendLine("    @Password,")
              .AppendLine("    @TypeCode,")
              .AppendLine("    @TypeName,")
              .AppendLine("    @ProfileCode,")
              .AppendLine("    @ProfileName,")
              .AppendLine("    @StatusCode,")
              .AppendLine("    @StatusName,")
              .AppendLine("    @LastAccess,")
              .AppendLine("    @AccessCount,")
              .AppendLine("    @Avatar,")
              .AppendLine("    @Note,")
              .AppendLine("    @BrokerId,")
              .AppendLine("    @AccountId,")
              .AppendLine("    @AccountName,")
              .AppendLine("    @CreationDate,")
              .AppendLine("    @CreationUserId,")
              .AppendLine("    @CreationUserName,")
              .AppendLine("    @RecordStatus,")
              .AppendLine("    @IS_ACTIVE")
              .AppendLine(")");

            try
            {
               await using var cn = _connectionProvider.GetConnection();
                await cn.OpenAsync();
                await using var cm = cn.CreateCommand();
                cm.CommandText = commandText.ToString();

                AddUserParameters(cm, user);

                var rowsAffected = await cm.ExecuteNonQueryAsync();
                if (rowsAffected > 0)
                {
                    return user;
                }

                throw new Exception("Nenhuma linha foi afetada durante a inserção.");
            }
            catch (DbException ex)
            {
                throw new Exception($"Erro de banco de dados ao criar usuário: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar usuário: {ex.Message}", ex);
            }
        }

        //---------------------------Update-----------------------------//
        public async Task<User?> UpdateUserAsync(User user)
        {
            var commandText = new StringBuilder()
                .AppendLine("UPDATE [JAYTECHAPPDB].[DBO].[USER]")
                .AppendLine("SET")
                .AppendLine("    [Code] = @Code,")
                .AppendLine("    [Name] = @Name,")
                .AppendLine("    [CPF] = @CPF,")
                .AppendLine("    [AccessKey] = @AccessKey,")
                .AppendLine("    [Password] = @Password,")
                .AppendLine("    [TypeCode] = @TypeCode,")
                .AppendLine("    [TypeName] = @TypeName,")
                .AppendLine("    [ProfileCode] = @ProfileCode,")
                .AppendLine("    [ProfileName] = @ProfileName,")
                .AppendLine("    [StatusCode] = @StatusCode,")
                .AppendLine("    [StatusName] = @StatusName,")
                .AppendLine("    [LastAccess] = @LastAccess,")
                .AppendLine("    [AccessCount] = @AccessCount,")
                .AppendLine("    [Avatar] = @Avatar,")
                .AppendLine("    [Note] = @Note,")
                .AppendLine("    [BrokerId] = @BrokerId,")
                .AppendLine("    [AccountId] = @AccountId,")
                .AppendLine("    [AccountName] = @AccountName,")
                .AppendLine("    [ChangeDate] = @ChangeDate,")
                .AppendLine("    [ChangeUserId] = @ChangeUserId,")
                .AppendLine("    [ChangeUserName] = @ChangeUserName,")
                .AppendLine("    [RecordStatus] = @RecordStatus,")
                .AppendLine("    [IS_ACTIVE] = @IS_ACTIVE")
                .AppendLine("WHERE [Id] = @Id");

            try
            {
                //if (user == null)
                //    throw new ArgumentNullException(nameof(user), "O objeto User é nulo.");

                await using var cn = _connectionProvider.GetConnection();
                await cn.OpenAsync();
                await using var cm = cn.CreateCommand();
                cm.CommandText = commandText.ToString();

                AddUserUpdateParameters(cm, user);

                int rowsAffected = await cm.ExecuteNonQueryAsync();
                if (rowsAffected > 0)
                {
                    return user;
                }

                return null;
            }
            catch (DbException ex)
            {
                throw new Exception($"Erro de banco de dados ao atualizar usuário: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar usuário: {ex.Message}", ex);
            }
        }

        //---------------------------Private Methods-----------------------------//
        private static void AddUserParameters(SqlCommand cm, User user)
        {
            cm.Parameters.AddWithValue("@Id", user.Id);
            cm.Parameters.AddWithValue("@Code", user.Code);
            cm.Parameters.AddWithValue("@Name", user.Name ?? (object)DBNull.Value);
            cm.Parameters.AddWithValue("@CPF", user.CPF ?? (object)DBNull.Value);
            cm.Parameters.AddWithValue("@AccessKey", user.AccessKey ?? (object)DBNull.Value);
            cm.Parameters.AddWithValue("@Password", user.Password ?? (object)DBNull.Value);

            cm.Parameters.AddWithValue("@TypeCode", user.TypeCode ?? (object)DBNull.Value);
            cm.Parameters.AddWithValue("@TypeName", user.TypeName ?? (object)DBNull.Value);
            cm.Parameters.AddWithValue("@ProfileCode", user.ProfileCode ?? (object)DBNull.Value);
            cm.Parameters.AddWithValue("@ProfileName", user.ProfileName ?? (object)DBNull.Value);
            cm.Parameters.AddWithValue("@StatusCode", user.StatusCode ?? (object)DBNull.Value);
            cm.Parameters.AddWithValue("@StatusName", user.StatusName ?? (object)DBNull.Value);

            cm.Parameters.AddWithValue("@LastAccess", user.LastAccess ?? (object)DBNull.Value);
            cm.Parameters.AddWithValue("@AccessCount", user.AccessCount ?? (object)DBNull.Value);

            cm.Parameters.AddWithValue("@Avatar", user.Avatar ?? (object)DBNull.Value);
            cm.Parameters.AddWithValue("@Note", user.Note ?? (object)DBNull.Value);

            cm.Parameters.AddWithValue("@BrokerId", user.BrokerId ?? (object)DBNull.Value);
            cm.Parameters.AddWithValue("@AccountId", user.AccountId ?? (object)DBNull.Value);
            cm.Parameters.AddWithValue("@AccountName", user.AccountName ?? (object)DBNull.Value);

            cm.Parameters.AddWithValue("@CreationDate", user.CreationDate ?? DateTime.UtcNow);
            cm.Parameters.AddWithValue("@CreationUserId", user.CreationUserId ?? (object)DBNull.Value);
            cm.Parameters.AddWithValue("@CreationUserName", user.CreationUserName ?? (object)DBNull.Value);

            cm.Parameters.AddWithValue("@RecordStatus", user.RecordStatus);
            cm.Parameters.AddWithValue("@IS_ACTIVE", user.IsActive);
        }

        private static void AddUserUpdateParameters(SqlCommand cm, User user)
        {
            cm.Parameters.AddWithValue("@Id", user.Id);
            cm.Parameters.AddWithValue("@Code", user.Code);
            cm.Parameters.AddWithValue("@Name", user.Name ?? (object)DBNull.Value);
            cm.Parameters.AddWithValue("@CPF", user.CPF ?? (object)DBNull.Value);
            cm.Parameters.AddWithValue("@AccessKey", user.AccessKey ?? (object)DBNull.Value);
            cm.Parameters.AddWithValue("@Password", user.Password ?? (object)DBNull.Value);

            cm.Parameters.AddWithValue("@TypeCode", user.TypeCode ?? (object)DBNull.Value);
            cm.Parameters.AddWithValue("@TypeName", user.TypeName ?? (object)DBNull.Value);
            cm.Parameters.AddWithValue("@ProfileCode", user.ProfileCode ?? (object)DBNull.Value);
            cm.Parameters.AddWithValue("@ProfileName", user.ProfileName ?? (object)DBNull.Value);
            cm.Parameters.AddWithValue("@StatusCode", user.StatusCode ?? (object)DBNull.Value);
            cm.Parameters.AddWithValue("@StatusName", user.StatusName ?? (object)DBNull.Value);

            cm.Parameters.AddWithValue("@LastAccess", user.LastAccess ?? (object)DBNull.Value);
            cm.Parameters.AddWithValue("@AccessCount", user.AccessCount ?? (object)DBNull.Value);

            cm.Parameters.AddWithValue("@Avatar", user.Avatar ?? (object)DBNull.Value);
            cm.Parameters.AddWithValue("@Note", user.Note ?? (object)DBNull.Value);

            cm.Parameters.AddWithValue("@BrokerId", user.BrokerId ?? (object)DBNull.Value);
            cm.Parameters.AddWithValue("@AccountId", user.AccountId ?? (object)DBNull.Value);
            cm.Parameters.AddWithValue("@AccountName", user.AccountName ?? (object)DBNull.Value);

            cm.Parameters.AddWithValue("@ChangeDate", user.ChangeDate ?? DateTime.UtcNow);
            cm.Parameters.AddWithValue("@ChangeUserId", user.ChangeUserId ?? (object)DBNull.Value);
            cm.Parameters.AddWithValue("@ChangeUserName", user.ChangeUserName ?? (object)DBNull.Value);

            cm.Parameters.AddWithValue("@RecordStatus", user.RecordStatus);
            cm.Parameters.AddWithValue("@IS_ACTIVE", user.IsActive);
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