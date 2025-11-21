using ConnectApp.Domain.Entities.Users;
using ConnectApp.Domain.Interfaces.Users;
using ConnectApp.Infrastructure.Sql;
using ConnectApp.Shared.Results;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectApp.Infrastructure.Repositories.Users
{
    internal class BBK
    { /*
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
                var user = new User();
                var commandText = new StringBuilder()
                   .AppendLine("SELECT")
                   .AppendLine("    [Id]")
                   .AppendLine("    ,[Code]")
                   .AppendLine("    ,[Name]")
                   .AppendLine("    ,[CPF]")
                   .AppendLine("    ,[AccessKey]")
                   .AppendLine("    ,[Password]")
                   .AppendLine("    ,[Email]")
                   .AppendLine("    ,[Phone]")
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
                   .AppendLine("    ,[Id]")
                   .AppendLine("    ,[Name]")
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
                   .AppendLine("   FROM [JaytechAppDB].[dbo].[User]")
                   .AppendLine("WHERE 1 = 1 ")
                   .AppendLine(" And [Id] = @id");


                await using var cn = _connectionProvider.GetConnection();
                await cn.OpenAsync();
                await using var cm = cn.CreateCommand();
                cm.CommandText = commandText.ToString();
                cm.Parameters.AddWithValue("@Id", id);

                await using var reader = await cm.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    user = GetDataRecord(reader);
                }



                return user;
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
                    .AppendLine("    ,[Email]")
                    .AppendLine("    ,[Phone]")
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
                    .AppendLine("    ,[Id]")
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
                    .AppendLine("   FROM [JaytechAppDB].[dbo].[User]")
                    .AppendLine("WHERE 1 = 1");

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
            public async Task<User?> GetUserByUsernameAsync(string accessKey)
            {

                var commandText = new StringBuilder()
                    .AppendLine("SELECT * FROM ")
                    .AppendLine("[JAYTECHAPPDB].[DBO].[USER] ")
                    .AppendLine("WHERE [AccessKey] COLLATE SQL_Latin1_General_CP1_CS_AS  =  @AccessKey  ");

                await using var cn = _connectionProvider.GetConnection();
                await cn.OpenAsync();
                await using var cm = cn.CreateCommand();
                cm.CommandText = commandText.ToString();

                cm.Parameters.AddWithValue("@AccessKey", accessKey);

                await using var reader = await cm.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    //var parse = GetDataRecord(reader);
                    var UserResult = new Result<User>
                    {
                        Messages = { $"Erro ao atualizar usuário: " },

                    };

                }
                else
                {
                    var UserResult = new Result<User>
                    {
                        Messages = { $"Erro ao atualizar usuário: " },

                    };

                }

            }


            //---------------------------Post-----------------------------//

            public async Task<User?> CreateUserAsync(User user)
            {
                var commandText = new StringBuilder()
                  .AppendLine("INSERT INTO [JAYTECHAPPDB].[DBO].[USER]")
                  .AppendLine("(")
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
                  .AppendLine("    [Id],")

                  .AppendLine("    [CreationDate],")
                  .AppendLine("    [CreationUserId],")
                  .AppendLine("    [CreationUserName],")


                  .AppendLine("    [RecordStatus],")

                  .AppendLine("    [CPF],")

                  .AppendLine("    [IS_ACTIVE]")

                  .AppendLine(") VALUES (")

                  .AppendLine("    @Id,")
                  .AppendLine("    @Code,")
                  .AppendLine("    @Name,")
                  .AppendLine("    @AccessKey,")
                  .AppendLine("    @Password,")
                  .AppendLine("    @Email,")
                  .AppendLine("    @Phone,")

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
                  .AppendLine("    @Id,")
                  .AppendLine("    @CreationDate,")
                  .AppendLine("    @CreationUserId,")
                  .AppendLine("    @CreationUserName,")
                  .AppendLine("    @RecordStatus,")
                  .AppendLine("    @CPF,")
                  .AppendLine("    @IS_ACTIVE")
                  .AppendLine(")");
                try
                {
                    if (user == null)
                    {
                        Console.WriteLine("Erro: O objeto User é nulo.");
                        var UserResult = new Result<User>
                        {
                            Messages = { $"Erro ao atualizar usuário: " },

                        };
                        return user;
                    }

                    await using var cn = _connectionProvider.GetConnection();
                    await cn.OpenAsync();
                    await using var cm = cn.CreateCommand();
                    cm.CommandText = commandText.ToString();

                    AddUser(cm, user);

                    var x = await cm.ExecuteScalarAsync();
                    if (x == null)
                    {
                        _ = new Result<User>
                        {
                            Messages = { $"Erro ao atualizar usuário: " },


                        };
                        return user;

                    }

                }
                catch (DbException ex)
                {


                    _ = new Result<User>
                    {
                        Messages = { $"Erro ao atualizar usuário: {ex.Message}" },


                    };
                    return user;





                }
                catch (Exception ex)
                {
                    _ = new Result<User>
                    {
                        Messages = { $"Erro ao atualizar usuário: {ex.Message}" },


                    };

                    return user;

                }
            }
            /*  public async Task<UserResult> CreateUserAsync(User user)
              {
                  var commandText = new StringBuilder()
                      .AppendLine("INSERT INTO [JAYTECHAPPDB].[DBO].[USER]")
                      .AppendLine("(")
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
                      .AppendLine("    [Id],")

                      .AppendLine("    [CreationDate],")
                      .AppendLine("    [CreationUserId],")
                      .AppendLine("    [CreationUserName],")


                      .AppendLine("    [RecordStatus],")

                      .AppendLine("    [CPF],")

                      .AppendLine("    [IS_ACTIVE]")

                      .AppendLine(") VALUES (")

                      .AppendLine("    @Id,")
                      .AppendLine("    @Code,")
                      .AppendLine("    @Name,")
                      .AppendLine("    @AccessKey,")
                      .AppendLine("    @Password,")
                      .AppendLine("    @Email,")
                      .AppendLine("    @Phone,")

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
                      .AppendLine("    @Id,")
                      .AppendLine("    @CreationDate,")
                      .AppendLine("    @CreationUserId,")
                      .AppendLine("    @CreationUserName,")
                      .AppendLine("    @RecordStatus,")
                      .AppendLine("    @CPF,")
                      .AppendLine("    @IS_ACTIVE")
                      .AppendLine(")");
                  try
                  {
                      if (user == null)
                      {
                          Console.WriteLine("Erro: O objeto User é nulo.");
                          var UserResult = new UserResult
                          {
                              Erro = true,
                              Message = "Erro ao criar o usuario"
                          };
                          return UserResult;
                      }

                      await using var cn = _connectionProvider.GetConnection();
                      await cn.OpenAsync();
                      await using var cm = cn.CreateCommand();
                      cm.CommandText = commandText.ToString();

                      AddUser(cm, user);

                      var x = await cm.ExecuteScalarAsync();
                      if (x == null)
                      {
                          Console.WriteLine("Erro: Nenhum ID foi retornado após a inserção.");
                          var UserResult = new UserResult
                          {
                              Erro = true,
                              Message = "Erro ao criar o usuario"
                          };
                          return UserResult;
                      }

                  }
                  catch (DbException ex)
                  {
                      Console.WriteLine($"Erro ao atualizar usuário: {ex.Message}");
                      if (ex is SqlException sqlEx)
                          Console.WriteLine($"Código do erro SQL: {sqlEx.Number}");

                      var userCatch = new UserResult
                      {
                          Erro = true,
                          Message = $"Erro ao criar o usuario:  {ex}"
                      };
                      return userCatch;
                  }
                  catch (Exception ex)
                  {
                      Console.WriteLine($"Ocorreu um erro inesperado: {ex.Message}");
                      var userCatch = new UserResult
                      {
                          Erro = true,
                          Message = $"Erro Interno:  {ex}"
                      };
                      return userCatch;
                  }
                  var userResult = new UserResult
                  {
                      Id = user.Id,
                      Erro = false,
                      Message = "Usuário criado com sucesso"
                  };
                  return userResult;

              }


              






            //---------------------------Update-----------------------------//
            public async Task<User?> UpdateUserAsync(User user)
            {
                await using var cn = _connectionProvider.GetConnection();
                await cn.OpenAsync();

                var commandText = new StringBuilder()
                    .AppendLine("UPDATE [JAYTECHAPPDB].[DBO].[USER]")
                    .AppendLine("SET")
                    .AppendLine("    [Code] = @Code,")
                    .AppendLine("    [Name] = @Name,")
                    .AppendLine("    [AccessKey] = @AccessKey,")
                    .AppendLine("    [Password] = @Password,")
                    .AppendLine("    [Email] = @Email,")
                    .AppendLine("    [Phone] = @Phone,")
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
                    .AppendLine("    [Id] = @Id,")
                    .AppendLine("    [CreationDate] = @CreationDate,")
                    .AppendLine("    [CreationUserId] = @CreationUserId,")
                    .AppendLine("    [CreationUserName] = @CreationUserName,")
                    .AppendLine("    [ChangeDate] = @ChangeDate,")
                    .AppendLine("    [ChangeUserId] = @ChangeUserId,")
                    .AppendLine("    [ChangeUserName] = @ChangeUserName,")
                    .AppendLine("    [ExclusionDate] = @ExclusionDate,")
                    .AppendLine("    [ExclusionUserId] = @ExclusionUserId,")
                    .AppendLine("    [ExclusionUserName] = @ExclusionUserName,")
                    .AppendLine("    [RecordStatus] = @RecordStatus,")
                    .AppendLine("    [CPF] = @CPF,")
                    .AppendLine("    [IS_ACTIVE] = @IS_ACTIVE,")
                    .AppendLine("    [Name] = @Name")
                    .AppendLine("WHERE [Id] = @Id");



                try
                {
                    if (user == null)
                    {
                        return user;
                    }



                    await using var cm = cn.CreateCommand();
                    cm.CommandText = commandText.ToString();

                    AddUserUpdateParameters(cm, user);

                    int rowsAffected = await cm.ExecuteNonQueryAsync();

                    if (rowsAffected > 0)
                    {
                        return user;
                    }
                    else
                    {
                        return null;
                    }

                }
                catch (DbException ex)
                {
                    _ = new Result<User>
                    {
                        Messages = { $"Erro ao atualizar usuário: {ex.Message}" },


                    };
                    return user;
                }
                catch (Exception ex)
                {
                    _ = new Result<User>
                    {
                        Messages = { $"Erro ao atualizar usuário: {ex.Message}" },


                    };
                    return user;
                }
            }




            //---------------------------Private Methods-----------------------------//
            private static void AddUser(SqlCommand cm, User user)
            {
                cm.Parameters.AddWithValue("@Id", user.Id);
                cm.Parameters.AddWithValue("@Code", user.Code);
                cm.Parameters.AddWithValue("@Name", string.IsNullOrWhiteSpace(user.Name) ? DBNull.Value : user.Name);
                cm.Parameters.AddWithValue("@AccessKey", string.IsNullOrWhiteSpace(user.AccessKey) ? DBNull.Value : user.AccessKey);
                cm.Parameters.AddWithValue("@Password", string.IsNullOrWhiteSpace(user.Password) ? DBNull.Value : user.Password);
                //cm.Parameters.AddWithValue("@Emails", string.IsNullOrWhiteSpace(user.Emails) ? DBNull.Value : user.Emails);
                //cm.Parameters.AddWithValue("@Phones", string.IsNullOrWhiteSpace(user.Phones) ? DBNull.Value : user.Phones);

                cm.Parameters.AddWithValue("@TypeCode", user.TypeCode == 0 ? DBNull.Value : user.TypeCode);
                cm.Parameters.AddWithValue("@TypeName", string.IsNullOrWhiteSpace(user.TypeName) ? DBNull.Value : user.TypeName);

                cm.Parameters.AddWithValue("@ProfileCode", user.ProfileCode == 0 ? DBNull.Value : user.ProfileCode);
                cm.Parameters.AddWithValue("@ProfileName", string.IsNullOrWhiteSpace(user.ProfileName) ? DBNull.Value : user.ProfileName);

                cm.Parameters.AddWithValue("@StatusCode", user.StatusCode == 0 ? DBNull.Value : user.StatusCode);
                cm.Parameters.AddWithValue("@StatusName", string.IsNullOrWhiteSpace(user.StatusName) ? DBNull.Value : user.StatusName);

                cm.Parameters.AddWithValue("@LastAccess", user.LastAccess == default ? DBNull.Value : user.LastAccess);
                cm.Parameters.AddWithValue("@AccessCount", user.AccessCount == 0 ? DBNull.Value : user.AccessCount);

                cm.Parameters.AddWithValue("@Avatar", string.IsNullOrWhiteSpace(user.Avatar) ? DBNull.Value : user.Avatar);
                cm.Parameters.AddWithValue("@Note", string.IsNullOrWhiteSpace(user.Note) ? DBNull.Value : user.Note);

                cm.Parameters.AddWithValue("@BrokerId", user.BrokerId == Guid.Empty ? DBNull.Value : user.BrokerId);
                cm.Parameters.AddWithValue("@Id", user.AccountId == Guid.Empty ? DBNull.Value : user.AccountId);
                cm.Parameters.AddWithValue("@Name", string.IsNullOrWhiteSpace(user.AccountName) ? DBNull.Value : user.AccountName);

                cm.Parameters.AddWithValue("@CreationDate", user.CreationDate == default ? DateTime.UtcNow : user.CreationDate);
                cm.Parameters.AddWithValue("@CreationUserId", user.CreationUserId == Guid.Empty ? DBNull.Value : user.CreationUserId);
                cm.Parameters.AddWithValue("@CreationUserName", string.IsNullOrWhiteSpace(user.CreationUserName) ? DBNull.Value : user.CreationUserName);


                cm.Parameters.AddWithValue("@ChangeDate", DBNull.Value);
                cm.Parameters.AddWithValue("@ChangeUserId", DBNull.Value);
                cm.Parameters.AddWithValue("@ChangeUserName", DBNull.Value);
                cm.Parameters.AddWithValue("@ExclusionDate", DBNull.Value);
                cm.Parameters.AddWithValue("@ExclusionUserId", DBNull.Value);
                cm.Parameters.AddWithValue("@ExclusionUserName", DBNull.Value);

                cm.Parameters.AddWithValue("@RecordStatus", user.RecordStatus);
                cm.Parameters.AddWithValue("@CPF", string.IsNullOrWhiteSpace(user.CPF) ? DBNull.Value : user.CPF);
                
            }




            private static User GetDataRecord(SqlDataReader reader)
            {
                return new User
                {
                    // Campos obrigatórios que não devem ser nulos
                    Id = reader.GetGuidValue("Id"),
                    Name = reader.GetStringValue("Name"),
                    AccessKey = reader.GetStringValue("AccessKey"),
                    Password = reader.GetStringValue("Password"),


                    Code = reader.GetNullableInt("Code") ?? 0,
                    TypeCode = reader.GetNullableInt("TypeCode") ?? 0,
                    ProfileCode = reader.GetNullableInt("ProfileCode") ?? 0,
                    StatusCode = reader.GetNullableInt("StatusCode") ?? 0,
                    AccessCount = reader.GetNullableInt("AccessCount") ?? 0,
                    RecordStatus = reader.GetBoolean("RecordStatus"),
                    ,
                    //Emails = reader.GetNullableString("Emails"),
                    //Phones = reader.GetNullableString("Phones"),
                    TypeName = reader.GetNullableString("TypeName"),
                    ProfileName = reader.GetNullableString("ProfileName"),
                    StatusName = reader.GetNullableString("StatusName"),
                    CreationUserName = reader.GetNullableString("CreationUserName"),
                    ChangeUserName = reader.GetNullableString("ChangeUserName"),
                    ExclusionUserName = reader.GetNullableString("ExclusionUserName"),
                    AccountName = reader.GetNullableString("Name") ?? "",

                    Avatar = reader.GetStringValue("Avatar"),
                    Note = reader.GetStringValue("Note"),

                    // GUIDs que podem ser nulos
                    BrokerId = reader.GetNullableGuid("BrokerId"),
                    AccountId = reader.GetNullableGuid("Id"),
                    CreationUserId = reader.GetNullableGuid("CreationUserId"),
                    ChangeUserId = reader.GetNullableGuid("ChangeUserId"),
                    ExclusionUserId = reader.GetNullableGuid("ExclusionUserId"),

                    // Datas que podem ser nulas
                    LastAccess = reader.GetNullableDateTime("LastAccess"),
                    CreationDate = reader.GetNullableDateTime("CreationDate"),
                    ChangeDate = reader.GetNullableDateTime("ChangeDate"),
                    ExclusionDate = reader.GetNullableDateTime("ExclusionDate"),

                    // CPF pode ser nulo
                    CPF = reader.GetNullableString("CPF")

                };
            }
            private static void AddUserUpdateParameters(SqlCommand cm, User user)
            {

                cm.Parameters.AddWithValue("@Id", user.Id);
                cm.Parameters.AddWithValue("@Code", user.Code);
                cm.Parameters.AddWithValue("@Name", user.Name);
                cm.Parameters.AddWithValue("@AccessKey", user.AccessKey);
                cm.Parameters.AddWithValue("@Password", user.Password);
                cm.Parameters.AddWithValue("@Email", user.Emails ?? (object)DBNull.Value);
                cm.Parameters.AddWithValue("@Phone", user.Phones ?? (object)DBNull.Value);
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
                cm.Parameters.AddWithValue("@Id", user.AccountId ?? (object)DBNull.Value);
                cm.Parameters.AddWithValue("@CreationDate", user.CreationDate ?? (object)DBNull.Value);
                cm.Parameters.AddWithValue("@CreationUserId", user.CreationUserId ?? (object)DBNull.Value);
                cm.Parameters.AddWithValue("@CreationUserName", user.CreationUserName ?? (object)DBNull.Value);
                cm.Parameters.AddWithValue("@ChangeDate", user.ChangeDate ?? (object)DBNull.Value);
                cm.Parameters.AddWithValue("@ChangeUserId", user.ChangeUserId ?? (object)DBNull.Value);
                cm.Parameters.AddWithValue("@ChangeUserName", user.ChangeUserName ?? (object)DBNull.Value);
                cm.Parameters.AddWithValue("@ExclusionDate", user.ExclusionDate ?? (object)DBNull.Value);
                cm.Parameters.AddWithValue("@ExclusionUserId", user.ExclusionUserId ?? (object)DBNull.Value);
                cm.Parameters.AddWithValue("@ExclusionUserName", user.ExclusionUserName ?? (object)DBNull.Value);
                cm.Parameters.AddWithValue("@RecordStatus", user.RecordStatus);
                cm.Parameters.AddWithValue("@CPF", user.CPF ?? (object)DBNull.Value);
                
                cm.Parameters.AddWithValue("@Name", user.AccountName ?? (object)DBNull.Value);


            }








            //public async Task<bool> DeleteUserAsync(User user)
            //{
            //    try
            //    {
            //        var commandText = new StringBuilder()
            //            .AppendLine("UPDATE [JaytechAppDB].[dbo].[User]")
            //            .AppendLine("SET")
            //            .AppendLine("    [Name] = @Name,")
            //            .AppendLine("    [CPF] = @CPF,")
            //            .AppendLine("    [AccessKey] = @AccessKey,")
            //            .AppendLine("    [Password] = @Password,")
            //            .AppendLine("    [TypeCode] = @TypeCode,")
            //            .AppendLine("    [TypeName] = @TypeName,")
            //            .AppendLine("    [ProfileCode] = @ProfileCode,")
            //            .AppendLine("    [ProfileName] = @ProfileName,")
            //            .AppendLine("    [StatusCode] = @StatusCode,")
            //            .AppendLine("    [StatusName] = @StatusName,")
            //            .AppendLine("    [LastAccess] = @LastAccess,")
            //            .AppendLine("    [AccessCount] = @AccessCount,")
            //            .AppendLine("    [Avatar] = @Avatar,")
            //            .AppendLine("    [Note] = @Note,")
            //            .AppendLine("    [BrokerId] = @BrokerId,")
            //            .AppendLine("    [Id] = @Id,")
            //            .AppendLine("    [Name] = @Name,")
            //            .AppendLine("    [ChangeDate] = @ChangeDate,")
            //            .AppendLine("    [ChangeUserId] = @ChangeUserId,")
            //            .AppendLine("    [ChangeUserName] = @ChangeUserName,")
            //            .AppendLine("    [ExclusionDate] = @ExclusionDate,")
            //            .AppendLine("    [ExclusionUserId] = @ExclusionUserId,")
            //            .AppendLine("    [ExclusionUserName] = @ExclusionUserName,")
            //            .AppendLine("    [RecordStatus] = @RecordStatus,")
            //            .AppendLine("    [IS_ACTIVE] = @IsActive")
            //            .AppendLine("WHERE [Id] = @Id");

            //        await using var cn = _connectionProvider.GetConnection();
            //        await cn.OpenAsync();
            //        await using var cm = cn.CreateCommand();

            //        cm.CommandText = commandText.ToString();

            //        // Parâmetros principais
            //        cm.Parameters.AddWithValue("@Id", user.Id);
            //        cm.Parameters.AddWithValue("@Name", user.Name);
            //        cm.Parameters.AddWithValue("@CPF", user.CPF ?? (object)DBNull.Value);
            //        cm.Parameters.AddWithValue("@AccessKey", user.AccessKey);
            //        cm.Parameters.AddWithValue("@Password", user.Password);

            //        // Parâmetros de tipo/perfil/status
            //        cm.Parameters.AddWithValue("@TypeCode", user.TypeCode ?? (object)DBNull.Value);
            //        cm.Parameters.AddWithValue("@TypeName", user.TypeName ?? (object)DBNull.Value);
            //        cm.Parameters.AddWithValue("@ProfileCode", user.ProfileCode ?? (object)DBNull.Value);
            //        cm.Parameters.AddWithValue("@ProfileName", user.ProfileName ?? (object)DBNull.Value);
            //        cm.Parameters.AddWithValue("@StatusCode", user.StatusCode ?? (object)DBNull.Value);
            //        cm.Parameters.AddWithValue("@StatusName", user.StatusName ?? (object)DBNull.Value);

            //        // Parâmetros de acesso
            //        cm.Parameters.AddWithValue("@LastAccess", user.LastAccess ?? (object)DBNull.Value);
            //        cm.Parameters.AddWithValue("@AccessCount", user.AccessCount ?? (object)DBNull.Value);

            //        // Parâmetros opcionais
            //        cm.Parameters.AddWithValue("@Avatar", user.Avatar ?? (object)DBNull.Value);
            //        cm.Parameters.AddWithValue("@Note", user.Note ?? (object)DBNull.Value);

            //        // Parâmetros de auditoria
            //        cm.Parameters.AddWithValue("@BrokerId", user.BrokerId ?? (object)DBNull.Value);
            //        cm.Parameters.AddWithValue("@Id", user.Id ?? (object)DBNull.Value);
            //        cm.Parameters.AddWithValue("@Name", user.Name);
            //        cm.Parameters.AddWithValue("@ChangeDate", user.ChangeDate ?? (object)DBNull.Value);
            //        cm.Parameters.AddWithValue("@ChangeUserId", user.ChangeUserId ?? (object)DBNull.Value);
            //        cm.Parameters.AddWithValue("@ChangeUserName", user.ChangeUserName ?? (object)DBNull.Value);
            //        cm.Parameters.AddWithValue("@ExclusionDate", user.ExclusionDate ?? (object)DBNull.Value);
            //        cm.Parameters.AddWithValue("@ExclusionUserId", user.ExclusionUserId ?? (object)DBNull.Value);
            //        cm.Parameters.AddWithValue("@ExclusionUserName", user.ExclusionUserName ?? (object)DBNull.Value);

            //        // Status
            //        cm.Parameters.AddWithValue("@RecordStatus", user.RecordStatus);
            //        cm.Parameters.AddWithValue("@IsActive", user.IsActive);

            //        var rowsAffected = await cm.ExecuteNonQueryAsync();
            //        return rowsAffected > 0;
            //    }
            //    catch (Exception ex)
            //    {
            //        _logger.LogError(ex, "Erro ao atualizar usuário {UserId}", user.Id);
            //        return false;
            //    }
            //}
        }



    }









































































    /*
            public async Task<bool> UpdateUserAsync(User user)
            {
                // A string da query foi movida para fora do método para melhor organização
                var commandText = new StringBuilder()
                    .AppendLine("UPDATE [dbo].[User]")
                    .AppendLine("SET")
                    .AppendLine("    [Code] = @code,") // Parâmetro em snake_case
                    .AppendLine("    [Name] = @name,") // Parâmetro em snake_case
                    .AppendLine("    [AccessKey] = @access_key,") // Parâmetro em snake_case
                    .AppendLine("    [Password] = @password,") // Parâmetro em snake_case
                    .AppendLine("    [Email] = @email,") // Parâmetro em snake_case
                    .AppendLine("    [Phone] = @phone,") // Parâmetro em snake_case
                    .AppendLine("    [TypeCode] = @type_code,") // Parâmetro em snake_case
                    .AppendLine("    [TypeName] = @type_name,") // Parâmetro em snake_case
                    .AppendLine("    [ProfileCode] = @profile_code,") // Parâmetro em snake_case
                    .AppendLine("    [ProfileName] = @profile_name,") // Parâmetro em snake_case
                    .AppendLine("    [StatusCode] = @status_code,") // Parâmetro em snake_case
                    .AppendLine("    [StatusName] = @status_name,") // Parâmetro em snake_case
                    .AppendLine("    [LastAccess] = @last_access,") // Parâmetro em snake_case
                    .AppendLine("    [AccessCount] = @access_count,") // Parâmetro em snake_case
                    .AppendLine("    [Avatar] = @avatar,") // Parâmetro em snake_case
                    .AppendLine("    [Note] = @note,") // Parâmetro em snake_case
                    .AppendLine("    [BrokerId] = @broker_id,") // Parâmetro em snake_case
                    .AppendLine("    [Id] = @account_id,") // Parâmetro em snake_case
                    .AppendLine("    [CreationDate] = @creation_date,") // Parâmetro em snake_case
                    .AppendLine("    [CreationUserId] = @creation_user_id,") // Parâmetro em snake_case
                    .AppendLine("    [CreationUserName] = @creation_user_name,") // Parâmetro em snake_case
                    .AppendLine("    [ChangeDate] = @change_date,") // Parâmetro em snake_case
                    .AppendLine("    [ChangeUserId] = @change_user_id,") // Parâmetro em snake_case
                    .AppendLine("    [ChangeUserName] = @change_user_name,") // Parâmetro em snake_case
                    .AppendLine("    [ExclusionDate] = @exclusion_date,") // Parâmetro em snake_case
                    .AppendLine("    [ExclusionUserId] = @exclusion_user_id,") // Parâmetro em snake_case
                    .AppendLine("    [ExclusionUserName] = @exclusion_user_name,") // Parâmetro em snake_case
                    .AppendLine("    [RecordStatus] = @record_status,") // Parâmetro em snake_case
                    .AppendLine("    [CPF] = @cpf,") // Parâmetro em snake_case
                    .AppendLine("    [IS_ACTIVE] = @is_active") // Parâmetro em snake_case
                    .AppendLine("WHERE [Id] = @id;"); // Parâmetro em snake_case


                try
                {
                    // Valida se o objeto user não é nulo antes de prosseguir
                    if (user == null)
                    {
                        Console.WriteLine("Erro: O objeto User é nulo.");
                        return false;
                    }

                    await using var cn = _connectionProvider.GetConnection();
                    await cn.OpenAsync();
                    await using var cm = cn.CreateCommand();
                    cm.CommandText = commandText.ToString();

                    // Chamando o método auxiliar para adicionar os parâmetros
                    AddUserParameters(cm, user);

                    // Descomente o bloco abaixo para logar os parâmetros para depuração.
                    // Isso pode ajudar a verificar se todos os parâmetros estão sendo adicionados corretamente.
                    // foreach (DbParameter p in cm.Parameters)
                    // {
                    //     Console.WriteLine($"Parâmetro: {p.ParameterName}, ValorRecebimento: {p.Value}, DbType: {p.DbType}");
                    // }

                    // ExecuteNonQueryAsync retorna o número de linhas afetadas.
                    // Se for maior que 0, a atualização foi bem-sucedida.
                    int rowsAffected = await cm.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
                catch (DbException ex)
                {
                    // Logar o erro (usar um logger de verdade em uma aplicação real)
                    Console.WriteLine($"Erro ao atualizar usuário: {ex.Message}");
                    // Você pode lançar uma exceção mais específica ou retornar false
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocorreu um erro inesperado: {ex.Message}");
                    return false;
                }
            }

            private void AddUserParameters(DbCommand command, User user)
            {
                // Ao usar Microsoft.Data.SqlClient, você usa SqlParameter diretamente.
                // É uma boa prática especificar o SqlDbType para clareza e evitar inferências.
                // Os nomes dos parâmetros devem corresponder EXATAMENTE à query SQL (agora em snake_case).

                // Limpar parâmetros anteriores, caso o comando seja reutilizado (embora aqui seja novo)
                command.Parameters.Clear();

                command.Parameters.Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = user.Id });
                command.Parameters.Add(new SqlParameter("@code", SqlDbType.Int) { Value = user.Code });
                command.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar) { Value = user.Name });
                command.Parameters.Add(new SqlParameter("@access_key", SqlDbType.NVarChar) { Value = user.AccessKey });
                command.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar) { Value = user.Password });
                command.Parameters.Add(new SqlParameter("@email", SqlDbType.NVarChar) { Value = user.Email });
                command.Parameters.Add(new SqlParameter("@phone", SqlDbType.NVarChar) { Value = user.Phone });
                command.Parameters.Add(new SqlParameter("@type_code", SqlDbType.Int) { Value = user.TypeCode });
                command.Parameters.Add(new SqlParameter("@type_name", SqlDbType.NVarChar) { Value = user.TypeName });
                command.Parameters.Add(new SqlParameter("@profile_code", SqlDbType.Int) { Value = user.ProfileCode });
                command.Parameters.Add(new SqlParameter("@profile_name", SqlDbType.NVarChar) { Value = user.ProfileName });
                command.Parameters.Add(new SqlParameter("@status_code", SqlDbType.Int) { Value = user.StatusCode });
                command.Parameters.Add(new SqlParameter("@status_name", SqlDbType.NVarChar) { Value = user.StatusName });
                command.Parameters.Add(new SqlParameter("@last_access", SqlDbType.DateTime) { Value = user.LastAccess ?? (object)DBNull.Value });
                command.Parameters.Add(new SqlParameter("@access_count", SqlDbType.Int) { Value = user.AccessCount });
                command.Parameters.Add(new SqlParameter("@avatar", SqlDbType.NVarChar) { Value = user.Avatar });
                command.Parameters.Add(new SqlParameter("@note", SqlDbType.NVarChar) { Value = user.Note });
                command.Parameters.Add(new SqlParameter("@broker_id", SqlDbType.UniqueIdentifier) { Value = user.BrokerId ?? (object)DBNull.Value });
                command.Parameters.Add(new SqlParameter("@account_id", SqlDbType.UniqueIdentifier) { Value = user.Id ?? (object)DBNull.Value });
                command.Parameters.Add(new SqlParameter("@creation_date", SqlDbType.DateTime) { Value = user.CreationDate ?? (object)DBNull.Value });
                command.Parameters.Add(new SqlParameter("@creation_user_id", SqlDbType.UniqueIdentifier) { Value = user.CreationUserId ?? (object)DBNull.Value });
                command.Parameters.Add(new SqlParameter("@creation_user_name", SqlDbType.NVarChar) { Value = user.CreationUserName });
                command.Parameters.Add(new SqlParameter("@change_date", SqlDbType.DateTime) { Value = user.ChangeDate ?? (object)DBNull.Value });
                command.Parameters.Add(new SqlParameter("@change_user_id", SqlDbType.UniqueIdentifier) { Value = user.ChangeUserId ?? (object)DBNull.Value });
                command.Parameters.Add(new SqlParameter("@change_user_name", SqlDbType.NVarChar) { Value = user.ChangeUserName });
                command.Parameters.Add(new SqlParameter("@exclusion_date", SqlDbType.DateTime) { Value = user.ExclusionDate ?? (object)DBNull.Value });
                command.Parameters.Add(new SqlParameter("@exclusion_user_id", SqlDbType.UniqueIdentifier) { Value = user.ExclusionUserId ?? (object)DBNull.Value });
                command.Parameters.Add(new SqlParameter("@exclusion_user_name", SqlDbType.NVarChar) { Value = user.ExclusionUserName });
                command.Parameters.Add(new SqlParameter("@record_status", SqlDbType.Int) { Value = user.RecordStatus });
                command.Parameters.Add(new SqlParameter("@cpf", SqlDbType.NVarChar) { Value = user.CPF });
                command.Parameters.Add(new SqlParameter("@is_active", SqlDbType.Bit) { Value = user.IsActive }); // Mapeando bool para Bit
            }
                private void AddUserParameters(DbCommand command, User user)
                {
                    command.Parameters.Clear();

                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = user.Id });
                    command.Parameters.Add(new SqlParameter("@code", SqlDbType.Int) { Value = user.Code });
                    command.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar) { Value = (object)user.Name ?? DBNull.Value });
                    command.Parameters.Add(new SqlParameter("@access_key", SqlDbType.NVarChar) { Value = (object)user.AccessKey ?? DBNull.Value });
                    command.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar) { Value = (object)user.Password ?? DBNull.Value });
                    command.Parameters.Add(new SqlParameter("@email", SqlDbType.NVarChar) { Value = (object)user.Email ?? DBNull.Value });
                    command.Parameters.Add(new SqlParameter("@phone", SqlDbType.NVarChar) { Value = (object)user.Phone ?? DBNull.Value });
                    command.Parameters.Add(new SqlParameter("@type_code", SqlDbType.Int) { Value = user.TypeCode });
                    command.Parameters.Add(new SqlParameter("@type_name", SqlDbType.NVarChar) { Value = (object)user.TypeName ?? DBNull.Value });
                    command.Parameters.Add(new SqlParameter("@profile_code", SqlDbType.Int) { Value = user.ProfileCode });
                    command.Parameters.Add(new SqlParameter("@profile_name", SqlDbType.NVarChar) { Value = (object)user.ProfileName ?? DBNull.Value });
                    command.Parameters.Add(new SqlParameter("@status_code", SqlDbType.Int) { Value = user.StatusCode });
                    command.Parameters.Add(new SqlParameter("@status_name", SqlDbType.NVarChar) { Value = (object)user.StatusName ?? DBNull.Value });
                    command.Parameters.Add(new SqlParameter("@last_access", SqlDbType.DateTime) { Value = user.LastAccess ?? (object)DBNull.Value });
                    command.Parameters.Add(new SqlParameter("@access_count", SqlDbType.Int) { Value = user.AccessCount });
                    command.Parameters.Add(new SqlParameter("@avatar", SqlDbType.NVarChar) { Value = (object)user.Avatar ?? DBNull.Value });
                    command.Parameters.Add(new SqlParameter("@note", SqlDbType.NVarChar) { Value = (object)user.Note ?? DBNull.Value });

                    command.Parameters.Add(new SqlParameter("@broker_id", SqlDbType.UniqueIdentifier)
                    { Value = (user.BrokerId == Guid.Empty ? (object)DBNull.Value : user.BrokerId ?? (object)DBNull.Value) });

                    command.Parameters.Add(new SqlParameter("@account_id", SqlDbType.UniqueIdentifier)
                    { Value = (user.Id == Guid.Empty ? (object)DBNull.Value : user.Id ?? (object)DBNull.Value) });

                    command.Parameters.Add(new SqlParameter("@creation_date", SqlDbType.DateTime) { Value = user.CreationDate ?? (object)DBNull.Value });
                    command.Parameters.Add(new SqlParameter("@creation_user_id", SqlDbType.UniqueIdentifier)
                    { Value = (user.CreationUserId == Guid.Empty ? (object)DBNull.Value : user.CreationUserId ?? (object)DBNull.Value) });
                    command.Parameters.Add(new SqlParameter("@creation_user_name", SqlDbType.NVarChar) { Value = (object)user.CreationUserName ?? DBNull.Value });
                    command.Parameters.Add(new SqlParameter("@change_date", SqlDbType.DateTime) { Value = user.ChangeDate ?? (object)DBNull.Value });
                    command.Parameters.Add(new SqlParameter("@change_user_id", SqlDbType.UniqueIdentifier)
                    { Value = (user.ChangeUserId == Guid.Empty ? (object)DBNull.Value : user.ChangeUserId ?? (object)DBNull.Value) });
                    command.Parameters.Add(new SqlParameter("@change_user_name", SqlDbType.NVarChar) { Value = (object)user.ChangeUserName ?? DBNull.Value });
                    command.Parameters.Add(new SqlParameter("@exclusion_date", SqlDbType.DateTime) { Value = user.ExclusionDate ?? (object)DBNull.Value });
                    command.Parameters.Add(new SqlParameter("@exclusion_user_id", SqlDbType.UniqueIdentifier)
                    { Value = (user.ExclusionUserId == Guid.Empty ? (object)DBNull.Value : user.ExclusionUserId ?? (object)DBNull.Value) });
                    command.Parameters.Add(new SqlParameter("@exclusion_user_name", SqlDbType.NVarChar) { Value = (object)user.ExclusionUserName ?? DBNull.Value });

                    command.Parameters.Add(new SqlParameter("@record_status", SqlDbType.Int) { Value = user.RecordStatus });
                    command.Parameters.Add(new SqlParameter("@cpf", SqlDbType.NVarChar) { Value = (object)user.CPF ?? DBNull.Value });

                    command.Parameters.Add(new SqlParameter("@is_active", SqlDbType.Bit) { Value = user.IsActive });

                }
            }

       private static void AddUserParamsParameters(SqlCommand cm, UserParams user)
            {
                cm.Parameters.AddWithValue("@Id", user.Id);
                cm.Parameters.AddWithValue("@Code", user.Code);
                cm.Parameters.AddWithValue("@Name", string.IsNullOrWhiteSpace(user.Name) ? (object)DBNull.Value : user.Name);
                cm.Parameters.AddWithValue("@AccessKey", string.IsNullOrWhiteSpace(user.AccessKey) ? (object)DBNull.Value : user.AccessKey);
                //cm.Parameters.AddWithValue("@Password", string.IsNullOrWhiteSpace(user.Password) ? (object)DBNull.Value : user.Password);
                cm.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(user.Email) ? (object)DBNull.Value : user.Email);
                cm.Parameters.AddWithValue("@Phone", string.IsNullOrWhiteSpace(user.Phone) ? (object)DBNull.Value : user.Phone);

                cm.Parameters.AddWithValue("@TypeCode", user.TypeCode == 0 ? (object)DBNull.Value : user.TypeCode);
                cm.Parameters.AddWithValue("@TypeName", string.IsNullOrWhiteSpace(user.TypeName) ? (object)DBNull.Value : user.TypeName);

                cm.Parameters.AddWithValue("@ProfileCode", user.ProfileCode == 0 ? (object)DBNull.Value : user.ProfileCode);
                cm.Parameters.AddWithValue("@ProfileName", string.IsNullOrWhiteSpace(user.ProfileName) ? (object)DBNull.Value : user.ProfileName);

                cm.Parameters.AddWithValue("@StatusCode", user.StatusCode == 0 ? (object)DBNull.Value : user.StatusCode);
                cm.Parameters.AddWithValue("@StatusName", string.IsNullOrWhiteSpace(user.StatusName) ? (object)DBNull.Value : user.StatusName);

                cm.Parameters.AddWithValue("@LastAccess", user.LastAccess == default ? (object)DBNull.Value : user.LastAccess);
                cm.Parameters.AddWithValue("@AccessCount", user.AccessCount == 0 ? (object)DBNull.Value : user.AccessCount);

                cm.Parameters.AddWithValue("@Avatar", string.IsNullOrWhiteSpace(user.Avatar) ? (object)DBNull.Value : user.Avatar);
                cm.Parameters.AddWithValue("@Note", string.IsNullOrWhiteSpace(user.Note) ? (object)DBNull.Value : user.Note);

                cm.Parameters.AddWithValue("@BrokerId", user.BrokerId == Guid.Empty ? (object)DBNull.Value : user.BrokerId);
                cm.Parameters.AddWithValue("@Id", user.Id == Guid.Empty ? (object)DBNull.Value : user.Id);

                cm.Parameters.AddWithValue("@CreationDate", user.CreationDate == default ? DateTime.UtcNow : user.CreationDate);
                cm.Parameters.AddWithValue("@CreationUserId", user.CreationUserId == Guid.Empty ? (object)DBNull.Value : user.CreationUserId);
                cm.Parameters.AddWithValue("@CreationUserName", string.IsNullOrWhiteSpace(user.CreationUserName) ? (object)DBNull.Value : user.CreationUserName);


                cm.Parameters.AddWithValue("@ChangeDate", DBNull.Value);
                cm.Parameters.AddWithValue("@ChangeUserId", DBNull.Value);
                cm.Parameters.AddWithValue("@ChangeUserName", DBNull.Value);
                cm.Parameters.AddWithValue("@ExclusionDate", DBNull.Value);
                cm.Parameters.AddWithValue("@ExclusionUserId", DBNull.Value);
                cm.Parameters.AddWithValue("@ExclusionUserName", DBNull.Value);

                //cm.Parameters.AddWithValue("@RecordStatus", user.RecordStatus == 0 ? (object)DBNull.Value : user.RecordStatus);
                cm.Parameters.AddWithValue("@CPF", string.IsNullOrWhiteSpace(user.CPF) ? (object)DBNull.Value : user.CPF);
                cm.Parameters.AddWithValue("@IS_ACTIVE", user.IsActive != 0);
            }


            private static User GetDataRecord(SqlDataReader reader)
            {
                return new User
                {
                    Id = reader.GetGuid("id"),
                    Code = reader.GetInt32("code"),
                    Name = reader.GetString("name"),
                    AccessKey = reader.GetString("access_key"),
                    Password = reader.GetString("password"),
                    Email = reader.GetString("email"),
                    Phone = reader.GetString("phone"),
                    TypeCode = reader.GetInt32("type_code"),
                    TypeName = reader.GetString("type_name"),
                    ProfileCode = reader.GetInt32("profile_code"),
                    ProfileName = reader.GetString("profile_name"),
                    StatusCode = reader.GetInt32("status_code"),
                    StatusName = reader.GetString("status_name"),
                    LastAccess = reader.GetDateTime("last_access"),
                    AccessCount = reader.GetInt32("access_count"),
                    Enabled = reader.GetBoolean("enabled"),
                    Avatar = reader.GetString("avatar"),
                    Note = reader.GetString("note"),
                    BrokerId = reader.GetGuid("broker_id"),
                    Id = reader.GetGuid("account_id"),
                    CreationDate = reader.GetDateTime("creation_date"),
                    CreationUserId = reader.GetGuid("creation_user_id"),
                    CreationUserName = reader.GetString("creation_user_name"),
                    ChangeDate = reader.GetDateTime("change_date"),
                    ChangeUserId = reader.GetGuid("change_user_id"),
                    ChangeUserName = reader.GetString("change_user_name"),
                    ExclusionDate = reader.GetDateTime("exclusion_date"),
                    ExclusionUserId = reader.GetGuid("exclusion_user_id"),
                    ExclusionUserName = reader.GetString("exclusion_user_name"),
                    RecordStatus = (short)reader.GetInt32("record_status"),
                    Username = reader.GetString("username"),
                    Password = reader.GetString("password_hash"),
                    FirstName = reader.GetString("first_name"),
                    LastName = reader.GetString("last_name"),
                    CPF = reader.GetString("cpf"),
                    CreatedAt = reader.GetDateTime("created_at"),
                    IsActive = reader.GetInt32("is_active")
                };
            }




                var commandText = new StringBuilder()
                    .AppendLine("SELECT ")
                    .AppendLine(" [Id]")
                    .AppendLine(",[Code]")
                    .AppendLine(" ,[Name]")
                    .AppendLine(" ,[AccessKey")
                    .AppendLine(" ,[Password]")
                    .AppendLine(" ,[Email]")
                    .AppendLine(" ,[Phone]")
                    .AppendLine(" ,[TypeCode]")
                    .AppendLine(" ,[TypeName]")
                    .AppendLine(" ,[ProfileCode]")
                    .AppendLine(" ,[ProfileName]")
                    .AppendLine(" ,[StatusCode]")
                    .AppendLine(" ,[StatusName]")
                    .AppendLine(" ,[LastAccess]")
                    .AppendLine(" ,[AccessCount]")
                    .AppendLine(" ,[Avatar]")
                    .AppendLine(" ,[Note]")
                    .AppendLine(" ,[BrokerId]")
                    .AppendLine(" ,[Id]")
                    .AppendLine(" ,[CreationDate]")
                    .AppendLine(" ,[CreationUserId]")
                    .AppendLine(" ,[CreationUserName]")
                    .AppendLine(" ,[ChangeDate]")
                    .AppendLine(" ,[ChangeUserId]")
                    .AppendLine(" ,[ChangeUserName]")
                    .AppendLine(" ,[ExclusionDate]")
                    .AppendLine(" ,[ExclusionUserId]")
                    .AppendLine(" ,[ExclusionUserName]")
                    .AppendLine(" ,[RecordStatus]")
                    .AppendLine(" ,[CPF]")
                    .AppendLine(" ,[IS_ACTIVE]")
                    .AppendLine(" FROM [JaytechAppDB].[dbo].[User]")
                    .AppendLine("WHERE 1 = 1")
                    .AppendLine("AND [id] = @id");

                await using var cn = _connectionProvider.GetConnection();
                await cn.OpenAsync();
                await using var cm = cn.CreateCommand();
                cm.CommandText = commandText.ToString();

                cm.Parameters.AddWithValue("@Id", id);


                var x = commandText;

                var y = cm;

                await using var reader = await cm.ExecuteReaderAsync();

                var xpto = reader;
                if (await reader.ReadAsync())
                {
                    return user = GetDataRecord(reader);
                }
















            /**/

    }
}
