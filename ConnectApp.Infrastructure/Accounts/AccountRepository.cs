using ConnectApp.Application.DTOs.Accounts;
using ConnectApp.Domain.Entities.Accounts;
using ConnectApp.Domain.Interfaces.Accounts;
using ConnectApp.Infrastructure.Sql;
using ConnectApp.Shared.SqlDataReaderShared;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Text;

namespace ConnectApp.Infrastructure.Accounts
{

    public class AccountRepository : IAccountRepository
    {
        public readonly IConnectionProvider _connectionProvider;
        public AccountRepository(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        //---------------------GET BY ID---------------------//
        public async Task<Account?> GetByIdAsync(Guid id)
        {
            await using var cn = _connectionProvider.GetConnection();
            {
                await cn.OpenAsync();
                var query = new StringBuilder()
                    .AppendLine("SELECT ")
                    .AppendLine("   [AccountId]")
                    .AppendLine("  ,[AccountName]")
                    .AppendLine("  ,[Ativa]")
                    .AppendLine("  ,[TemaPadrao]")
                    .AppendLine("  ,[UrlLogo]")
                    .AppendLine("  ,[UrlIcone]")
                    .AppendLine("  ,[UrlImagemLogin]")
                    .AppendLine("  ,[UrlImagemDashboard]")
                    .AppendLine("  ,[CreationDate]")
                    .AppendLine("  ,[CreationUserId]")
                    .AppendLine("  ,[CreationUserName]")
                    .AppendLine("  ,[ChangeDate]")
                    .AppendLine("  ,[ChangeUserId]")
                    .AppendLine("  ,[ChangeUserName]")
                    .AppendLine("  ,[ExclusionDate]")
                    .AppendLine("  ,[ExclusionUserId]")
                    .AppendLine("  ,[ExclusionUserName]")
                    .AppendLine("  ,[RecordStatus]")
                    .AppendLine("FROM [JAYTECHAPPDB].[DBO].[Account]")
                    .AppendLine("WHERE [AccountId] = @AccountId");

                using var command = new SqlCommand(query.ToString(), cn);
                command.Parameters.AddWithValue("@AccountId", id);
                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return ReaderAccount(reader);
                }
                return null;
            }
        }

        public async Task<List<Account>> GetAllAsync()
        {
            var accounts = new List<Account>();
            await using var cn = _connectionProvider.GetConnection();
            {
                await cn.OpenAsync();
                var query = new StringBuilder()
                   .AppendLine("SELECT ")
                    .AppendLine("   [AccountId]")
                    .AppendLine("  ,[AccountName]")
                    .AppendLine("  ,[Ativa]")
                    .AppendLine("  ,[TemaPadrao]")
                    .AppendLine("  ,[UrlLogo]")
                    .AppendLine("  ,[UrlIcone]")
                    .AppendLine("  ,[UrlImagemLogin]")
                    .AppendLine("  ,[UrlImagemDashboard]")
                    .AppendLine("  ,[CreationDate]")
                    .AppendLine("  ,[CreationUserId]")
                    .AppendLine("  ,[CreationUserName]")
                    .AppendLine("  ,[ChangeDate]")
                    .AppendLine("  ,[ChangeUserId]")
                    .AppendLine("  ,[ChangeUserName]")
                    .AppendLine("  ,[ExclusionDate]")
                    .AppendLine("  ,[ExclusionUserId]")
                    .AppendLine("  ,[ExclusionUserName]")
                    .AppendLine("  ,[RecordStatus]")
                    .AppendLine("FROM [JAYTECHAPPDB].[DBO].[Account]")
                    .AppendLine("WHERE 1 = 1")
                    .AppendLine(" AND  [RecordStatus] = 1");

                using var command = new SqlCommand(query.ToString(), cn);

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    accounts.Add(ReaderAccount(reader));
                }
                return accounts;
            }
        }


        //---------------------CREATE---------------------//
        public async Task<Account> CreateAsync(Account account)
        {
            var query = new StringBuilder()
                .AppendLine("INSERT INTO [JAYTECHAPPDB].[DBO].[Account] (")
                .AppendLine("    [AccountId]")
                .AppendLine("  , [AccountName]")
                .AppendLine("  , [Ativa]")
                .AppendLine("  , [TemaPadrao]")
                .AppendLine("  , [UrlLogo]")
                .AppendLine("  , [UrlIcone]")
                .AppendLine("  , [UrlImagemLogin]")
                .AppendLine("  , [UrlImagemDashboard]")
                .AppendLine("  , [CreationDate]")
                .AppendLine("  , [CreationUserId]")
                .AppendLine("  , [CreationUserName]")
                .AppendLine("  , [ChangeDate]")
                .AppendLine("  , [ChangeUserId]")
                .AppendLine("  , [ChangeUserName]")
                .AppendLine("  , [ExclusionDate]")
                .AppendLine("  , [ExclusionUserId]")
                .AppendLine("  , [ExclusionUserName]")
                .AppendLine("  , [RecordStatus]")
                .AppendLine(") VALUES (")
                .AppendLine("    @AccountId")
                .AppendLine("  , @AccountName")
                .AppendLine("  , @Ativa")
                .AppendLine("  , @TemaPadrao")
                .AppendLine("  , @UrlLogo")
                .AppendLine("  , @UrlIcone")
                .AppendLine("  , @UrlImagemLogin")
                .AppendLine("  , @UrlImagemDashboard")
                .AppendLine("  , @CreationDate")
                .AppendLine("  , @CreationUserId")
                .AppendLine("  , @CreationUserName")
                .AppendLine("  , @ChangeDate")
                .AppendLine("  , @ChangeUserId")
                .AppendLine("  , @ChangeUserName")
                .AppendLine("  , @ExclusionDate")
                .AppendLine("  , @ExclusionUserId")
                .AppendLine("  , @ExclusionUserName")
                .AppendLine("  , @RecordStatus")
                .AppendLine(")");

            try
            {
                await using var cn = _connectionProvider.GetConnection();
                await cn.OpenAsync();

                await using var command = new SqlCommand(query.ToString(), cn);

                SetCommand(account, command);

                var rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    return account;
                }

                throw new Exception("Nenhuma linha foi afetada durante a inserção da Account.");
            }
            catch (DbException ex)
            {
                throw new Exception($"Erro de banco de dados ao criar Account: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar Account: {ex.Message}", ex);
            }
        }



        //---------------------UPDATE---------------------//
        public async Task<bool> UpdateAsync(Account account, Guid id)
        {


            var query = new StringBuilder()
                .AppendLine(" UPDATE [JAYTECHAPPDB].[DBO].[Account] SET") 
                .AppendLine("    [AccountName] = @AccountName") 
                .AppendLine("  , [Ativa] = @Ativa")
                .AppendLine("  , [TemaPadrao] = @TemaPadrao")
                .AppendLine("  , [UrlLogo] = @UrlLogo")
                .AppendLine("  , [UrlIcone] = @UrlIcone")
                .AppendLine("  , [UrlImagemLogin] = @UrlImagemLogin")
                .AppendLine("  , [UrlImagemDashboard] = @UrlImagemDashboard")
                .AppendLine("  , [CreationDate] = @CreationDate")
                .AppendLine("  , [CreationUserId] = @CreationUserId")
                .AppendLine("  , [CreationUserName] = @CreationUserName")
                .AppendLine("  , [ChangeDate] = @ChangeDate")
                .AppendLine("  , [ChangeUserId] = @ChangeUserId")
                .AppendLine("  , [ChangeUserName] = @ChangeUserName")
                .AppendLine("  , [ExclusionDate] = @ExclusionDate")
                .AppendLine("  , [ExclusionUserId] = @ExclusionUserId")
                .AppendLine("  , [ExclusionUserName] = @ExclusionUserName")
                .AppendLine("  , [RecordStatus] = @RecordStatus")
                .AppendLine("WHERE [AccountId] = @AccountId"); 

            try
            {
                await using var cn = _connectionProvider.GetConnection();
                await cn.OpenAsync();
                await using var command = new SqlCommand(query.ToString(), cn);


                SetCommand(account, command);
                command.Parameters.AddWithValue("@AccountId", id);
                
                var rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    return true;
                }

                throw new Exception("Nenhuma linha foi afetada durante a inserção da Account.");
            }
            catch (DbException ex)
            {
                throw new Exception($"Erro de banco de dados ao criar Account: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar Account: {ex.Message}", ex);
            }
        }         
        

        //---------------------DELETE---------------------//
        public async Task<bool> DeleteAsync(Guid accountId)
        {
            var query = new StringBuilder()
                .AppendLine("UPDATE [JaytechAppDB].[dbo].[Account] ")
                .AppendLine(" SET [RecordStatus] = 0 ")
                .AppendLine(" WHERE [AccountId] = @accountId");

            try
            {
                await using var cn = _connectionProvider.GetConnection();
                await cn.OpenAsync();
                await using var command = new SqlCommand(query.ToString(), cn);


                command.Parameters.AddWithValue("@accountId", accountId);
               
                var rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    return true;
                }

                throw new Exception("Nenhuma linha foi afetada durante a inserção da Account.");
            }
            catch (DbException ex)
            {
                throw new Exception($"Erro de banco de dados ao criar Account: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar Account: {ex.Message}", ex);
            }
        }




        //---------------------METODOS---------------------//

        private static Account ReaderAccount(SqlDataReader reader)
        {
            return new Account
            {
                Id = reader.GetGuidValue("AccountId"),
                Name = reader.GetStringValue("AccountName"),
                Ativa = reader.GetBoolean("Ativa"),

                TemaPadrao = reader.GetNullableString("TemaPadrao"),
                UrlLogo = reader.GetNullableString("UrlLogo"),
                UrlIcone = reader.GetNullableString("UrlIcone"),
                UrlImagemLogin = reader.GetNullableString("UrlImagemLogin"),
                UrlImagemDashboard = reader.GetNullableString("UrlImagemDashboard"),

                CreationDate = reader.GetNullableDateTime("CreationDate"),
                CreationUserId = reader.GetNullableGuid("CreationUserId"),
                CreationUserName = reader.GetNullableString("CreationUserName"),
                ChangeDate = reader.GetNullableDateTime("ChangeDate"),
                ChangeUserId = reader.GetNullableGuid("ChangeUserId"),
                ChangeUserName = reader.GetNullableString("ChangeUserName"),
                ExclusionDate = reader.GetNullableDateTime("ExclusionDate"),
                ExclusionUserId = reader.GetNullableGuid("ExclusionUserId"),
                ExclusionUserName = reader.GetNullableString("ExclusionUserName"),

                RecordStatus = reader.GetBoolean("RecordStatus")

            };
        }
        private static void SetCommand(Account account, SqlCommand command)
        {

            command.Parameters.AddWithValue("@AccountId", account.Id);
            command.Parameters.AddWithValue("@AccountName", account.Name);

            command.Parameters.AddWithValue("@Ativa", account.Ativa);
            command.Parameters.AddWithValue("@TemaPadrao", (object?)account.TemaPadrao ?? DBNull.Value);
            command.Parameters.AddWithValue("@UrlLogo", (object?)account.UrlLogo ?? DBNull.Value);
            command.Parameters.AddWithValue("@UrlIcone", (object?)account.UrlIcone ?? DBNull.Value);
            command.Parameters.AddWithValue("@UrlImagemLogin", (object?)account.UrlImagemLogin ?? DBNull.Value);
            command.Parameters.AddWithValue("@UrlImagemDashboard", (object?)account.UrlImagemDashboard ?? DBNull.Value);


            command.Parameters.AddWithValue("@CreationDate", (object?)account.CreationDate ?? DBNull.Value);
            command.Parameters.AddWithValue("@CreationUserId", (object?)account.CreationUserId ?? DBNull.Value);
            command.Parameters.AddWithValue("@CreationUserName", (object?)account.CreationUserName ?? DBNull.Value);
            command.Parameters.AddWithValue("@ChangeDate", (object?)account.ChangeDate ?? DBNull.Value);
            command.Parameters.AddWithValue("@ChangeUserId", (object?)account.ChangeUserId ?? DBNull.Value);
            command.Parameters.AddWithValue("@ChangeUserName", (object?)account.ChangeUserName ?? DBNull.Value);
            command.Parameters.AddWithValue("@ExclusionDate", (object?)account.ExclusionDate ?? DBNull.Value);
            command.Parameters.AddWithValue("@ExclusionUserId", (object?)account.ExclusionUserId ?? DBNull.Value);
            command.Parameters.AddWithValue("@ExclusionUserName", (object?)account.ExclusionUserName ?? DBNull.Value);

            command.Parameters.AddWithValue("@RecordStatus", account.RecordStatus);
        }



    }
}




//public async Task<AccountResult?> GetByIdAsync(AccountResult account)
//{
//    await using var cn = _connectionProvider.GetConnection();
//    {
//        await cn.OpenAsync();

//        // ✅ Usando exatamente a mesma estrutura do seu INSERT
//        var query = new StringBuilder()
//            .AppendLine("INSERT INTO [JaytechAppDB].[dbo].[AccountResult]")
//            .AppendLine("           ([Id]")
//            .AppendLine("           ,[Name]")
//            .AppendLine("           ,[Ativa]")
//            .AppendLine("           ,[TemaPadrao]")
//            .AppendLine("           ,[UrlLogo]")
//            .AppendLine("           ,[UrlIcone]")
//            .AppendLine("           ,[UrlImagemLogin]")
//            .AppendLine("           ,[UrlImagemDashboard]")
//            .AppendLine("           ,[CreationDate]")
//            .AppendLine("           ,[CreationUserId]")
//            .AppendLine("           ,[CreationUserName]")
//            .AppendLine("           ,[ChangeDate]")
//            .AppendLine("           ,[ChangeUserId]")
//            .AppendLine("           ,[ChangeUserName]")
//            .AppendLine("           ,[ExclusionDate]")
//            .AppendLine("           ,[ExclusionUserId]")
//            .AppendLine("           ,[ExclusionUserName]")
//            .AppendLine("           ,[RecordStatus])")
//            .AppendLine("     VALUES")
//            .AppendLine("           (@Id")
//            .AppendLine("           ,@Name")
//            .AppendLine("           ,@Ativa")
//            .AppendLine("           ,@TemaPadrao")
//            .AppendLine("           ,@UrlLogo")
//            .AppendLine("           ,@UrlIcone")
//            .AppendLine("           ,@UrlImagemLogin")
//            .AppendLine("           ,@UrlImagemDashboard")
//            .AppendLine("           ,@CreationDate")
//            .AppendLine("           ,@CreationUserId")
//            .AppendLine("           ,@CreationUserName")
//            .AppendLine("           ,@ChangeDate")
//            .AppendLine("           ,@ChangeUserId")
//            .AppendLine("           ,@ChangeUserName")
//            .AppendLine("           ,@ExclusionDate")
//            .AppendLine("           ,@ExclusionUserId")
//            .AppendLine("           ,@ExclusionUserName")
//            .AppendLine("           ,@RecordStatus)");

//        using var command = new SqlCommand(query.ToString(), cn);

//        RecordParams(account, command);

//        try
//        {
//            int rowsAffected = await command.ExecuteNonQueryAsync();


//            return new AccountResult
//            {
//                Id = account.Id,
//                Name = account.Name,
//                Ativa = account.Ativa,
//                TemaPadrao = account.TemaPadrao ?? "Default",
//                UrlLogo = account.UrlLogo ?? string.Empty,
//                UrlIcone = account.UrlIcone ?? string.Empty,
//                UrlImagemLogin = account.UrlImagemLogin ?? string.Empty,
//                UrlImagemDashboard = account.UrlImagemDashboard ?? string.Empty,
//                CreationDate = account.CreationDate,
//                CreationUserId = account.CreationUserId,
//                CreationUserName = account.CreationUserName,
//                ChangeDate = account.ChangeDate,
//                ChangeUserId = account.ChangeUserId,
//                ChangeUserName = account.ChangeUserName,
//                ExclusionDate = account.ExclusionDate,
//                ExclusionUserId = account.ExclusionUserId,
//                ExclusionUserName = account.ExclusionUserName,


//            };
//        }
//        catch (SqlException sqlEx)
//        {

//            return Bad 
//            {
//                Error = true,
//                Message = $"Erro SQL: {sqlEx.Message}",
//                Code = 500
//            };
//        }
//        catch (Exception ex)
//        {
//            // ✅ Tratamento para outros erros
//            return new Application.DTOs.Accounts.AccountResult
//            {
//                Error = true,
//                Message = $"Erro: {ex.Message}",
//                Code = 500
//            };
//}
//  }
// }

/*  public async Task<Account> CreateAsync(Account account)
        {
            await using var cn = _connectionProvider.GetConnection();
            {
                await cn.OpenAsync();

                // ✅ Usando exatamente a mesma estrutura do seu INSERT
                var query = new StringBuilder()
                    .AppendLine("INSERT INTO [JaytechAppDB].[dbo].[AccountResult]")
                    .AppendLine("           ([Id]")
                    .AppendLine("           ,[Name]")
                    .AppendLine("           ,[Ativa]")
                    .AppendLine("           ,[TemaPadrao]")
                    .AppendLine("           ,[UrlLogo]")
                    .AppendLine("           ,[UrlIcone]")
                    .AppendLine("           ,[UrlImagemLogin]")
                    .AppendLine("           ,[UrlImagemDashboard]")
                    .AppendLine("           ,[CreationDate]")
                    .AppendLine("           ,[CreationUserId]")
                    .AppendLine("           ,[CreationUserName]")
                    .AppendLine("           ,[ChangeDate]")
                    .AppendLine("           ,[ChangeUserId]")
                    .AppendLine("           ,[ChangeUserName]")
                    .AppendLine("           ,[ExclusionDate]")
                    .AppendLine("           ,[ExclusionUserId]")
                    .AppendLine("           ,[ExclusionUserName]")
                    .AppendLine("           ,[RecordStatus])")
                    .AppendLine("     VALUES")
                    .AppendLine("           (@Id")
                    .AppendLine("           ,@Name")
                    .AppendLine("           ,@Ativa")
                    .AppendLine("           ,@TemaPadrao")
                    .AppendLine("           ,@UrlLogo")
                    .AppendLine("           ,@UrlIcone")
                    .AppendLine("           ,@UrlImagemLogin")
                    .AppendLine("           ,@UrlImagemDashboard")
                    .AppendLine("           ,@CreationDate")
                    .AppendLine("           ,@CreationUserId")
                    .AppendLine("           ,@CreationUserName")
                    .AppendLine("           ,@ChangeDate")
                    .AppendLine("           ,@ChangeUserId")
                    .AppendLine("           ,@ChangeUserName")
                    .AppendLine("           ,@ExclusionDate")
                    .AppendLine("           ,@ExclusionUserId")
                    .AppendLine("           ,@ExclusionUserName")
                    .AppendLine("           ,@RecordStatus)");

                using var command = new SqlCommand(query.ToString(), cn);

                // ✅ Parâmetros com tipos específicos conforme sua tabela
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = account.Id;
                command.Parameters.Add("@Name", SqlDbType.NVarChar, 255).Value = account.Name;
                command.Parameters.Add("@Ativa", SqlDbType.Bit).Value = account.Ativa;
                command.Parameters.Add("@TemaPadrao", SqlDbType.NVarChar, 50).Value = account.TemaPadrao ?? (object)DBNull.Value;
                command.Parameters.Add("@UrlLogo", SqlDbType.NVarChar, -1).Value = account.UrlLogo ?? (object)DBNull.Value; // -1 = MAX
                command.Parameters.Add("@UrlIcone", SqlDbType.NVarChar, -1).Value = account.UrlIcone ?? (object)DBNull.Value;
                command.Parameters.Add("@UrlImagemLogin", SqlDbType.NVarChar, -1).Value = account.UrlImagemLogin ?? (object)DBNull.Value;
                command.Parameters.Add("@UrlImagemDashboard", SqlDbType.NVarChar, -1).Value = account.UrlImagemDashboard ?? (object)DBNull.Value;
                command.Parameters.Add("@CreationDate", SqlDbType.DateTime).Value = account.CreationDate;
                command.Parameters.Add("@CreationUserId", SqlDbType.UniqueIdentifier).Value = account.CreationUserId;
                command.Parameters.Add("@CreationUserName", SqlDbType.NVarChar, 255).Value = account.CreationUserName;
                command.Parameters.Add("@ChangeDate", SqlDbType.DateTime).Value = account.ChangeDate ?? (object)DBNull.Value;
                command.Parameters.Add("@ChangeUserId", SqlDbType.UniqueIdentifier).Value = account.ChangeUserId ?? (object)DBNull.Value;
                command.Parameters.Add("@ChangeUserName", SqlDbType.NVarChar, 255).Value = account.ChangeUserName ?? (object)DBNull.Value;
                command.Parameters.Add("@ExclusionDate", SqlDbType.DateTime).Value = account.ExclusionDate ?? (object)DBNull.Value;
                command.Parameters.Add("@ExclusionUserId", SqlDbType.UniqueIdentifier).Value = account.ExclusionUserId ?? (object)DBNull.Value;
                command.Parameters.Add("@ExclusionUserName", SqlDbType.NVarChar, 255).Value = account.ExclusionUserName ?? (object)DBNull.Value;
                command.Parameters.Add("@RecordStatus", SqlDbType.Bit).Value = account.RecordStatus;

                try
                {
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    // ✅ Retorno com tratamento para valores nulos
                    return new Account
                    {
                        Id = account.Id,
                        Name = account.Name,
                        Ativa = account.Ativa,
                        TemaPadrao = account.TemaPadrao ?? "Default",
                        UrlLogo = account.UrlLogo ?? string.Empty,
                        UrlIcone = account.UrlIcone ?? string.Empty,
                        UrlImagemLogin = account.UrlImagemLogin ?? string.Empty,
                        UrlImagemDashboard = account.UrlImagemDashboard ?? string.Empty,
                        CreationDate = account.CreationDate,
                        CreationUserId = account.CreationUserId,
                        CreationUserName = account.CreationUserName,
                        ChangeDate = account.ChangeDate,
                        ChangeUserId = account.ChangeUserId,
                        ChangeUserName = account.ChangeUserName,
                        ExclusionDate = account.ExclusionDate,
                        ExclusionUserId = account.ExclusionUserId,
                        ExclusionUserName = account.ExclusionUserName,

                       
                    };
                }
                catch (SqlException sqlEx)
                {
                    
                    return new Account
                    {
                        
                    };
                }
                catch (Exception ex)
                {
                    // ✅ Tratamento para outros erros
                    return AccountResult
                    {
                        Error = true,
                        Message = $"Erro: {ex.Message}",
                        Code = 500
                    };
                }
            }
        }
*/



/*private static void ListRecordAccount(List<Account> accounts, SqlDataReader reader)
        {
            accounts.Add(new Account
            {
                Id = reader.GetGuidValue("Id"),
                Name = reader.GetStringValue("Name"),
                Ativa = reader.GetBoolean("Ativa"),
                TemaPadrao = reader.GetNullableString("TemaPadrao"),
                UrlLogo = reader.GetNullableString("UrlLogo"),
                UrlIcone = reader.GetNullableString("UrlIcone"),
                UrlImagemLogin = reader.GetNullableString("UrlImagemLogin"),
                UrlImagemDashboard = reader.GetNullableString("UrlImagemDashboard"),
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
            });
        }
        private static void RecordCommandParams(Account account, SqlCommand command)
        {
            command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = account.Id;
            command.Parameters.Add("@Name", SqlDbType.NVarChar, 255).Value = account.Name;
            command.Parameters.Add("@Ativa", SqlDbType.Bit).Value = account.Ativa;
            command.Parameters.Add("@TemaPadrao", SqlDbType.NVarChar, 50).Value = account.TemaPadrao ?? (object)DBNull.Value;
            command.Parameters.Add("@UrlLogo", SqlDbType.NVarChar, -1).Value = account.UrlLogo ?? (object)DBNull.Value; // -1 = MAX
            command.Parameters.Add("@UrlIcone", SqlDbType.NVarChar, -1).Value = account.UrlIcone ?? (object)DBNull.Value;
            command.Parameters.Add("@UrlImagemLogin", SqlDbType.NVarChar, -1).Value = account.UrlImagemLogin ?? (object)DBNull.Value;
            command.Parameters.Add("@UrlImagemDashboard", SqlDbType.NVarChar, -1).Value = account.UrlImagemDashboard ?? (object)DBNull.Value;
            command.Parameters.Add("@CreationDate", SqlDbType.DateTime).Value = account.CreationDate;
            command.Parameters.Add("@CreationUserId", SqlDbType.UniqueIdentifier).Value = account.CreationUserId;
            command.Parameters.Add("@CreationUserName", SqlDbType.NVarChar, 255).Value = account.CreationUserName;
            command.Parameters.Add("@ChangeDate", SqlDbType.DateTime).Value = account.ChangeDate ?? (object)DBNull.Value;
            command.Parameters.Add("@ChangeUserId", SqlDbType.UniqueIdentifier).Value = account.ChangeUserId ?? (object)DBNull.Value;
            command.Parameters.Add("@ChangeUserName", SqlDbType.NVarChar, 255).Value = account.ChangeUserName ?? (object)DBNull.Value;
            command.Parameters.Add("@ExclusionDate", SqlDbType.DateTime).Value = account.ExclusionDate ?? (object)DBNull.Value;
            command.Parameters.Add("@ExclusionUserId", SqlDbType.UniqueIdentifier).Value = account.ExclusionUserId ?? (object)DBNull.Value;
            command.Parameters.Add("@ExclusionUserName", SqlDbType.NVarChar, 255).Value = account.ExclusionUserName ?? (object)DBNull.Value;
            command.Parameters.Add("@RecordStatus", SqlDbType.Bit).Value = account.RecordStatus;
        }
*/



/*
 private static Account AccountResult (Account account)
        {
            return new Account
            {
                Id = account.Id,
                Name = account.Name,
                Ativa = account.Ativa,
                TemaPadrao = account.TemaPadrao,
                UrlLogo = account.UrlLogo,
                UrlIcone = account.UrlIcone,
                UrlImagemLogin = account.UrlImagemLogin,
                UrlImagemDashboard = account.UrlImagemDashboard

            };
        }
       
 
  command.Parameters.AddWithValue("@Id", account.Id);
                command.Parameters.AddWithValue("@Name", account.Name);
                command.Parameters.AddWithValue("@Ativa", account.Ativa);
                command.Parameters.AddWithValue("@TemaPadrao", account.TemaPadrao);
                command.Parameters.AddWithValue("@UrlLogo", (object)account.UrlLogo ?? DBNull.Value);
                command.Parameters.AddWithValue("@UrlIcone", (object)account.UrlIcone ?? DBNull.Value);
                command.Parameters.AddWithValue("@UrlImagemLogin", (object)account.UrlImagemLogin ?? DBNull.Value);
                command.Parameters.AddWithValue("@UrlImagemDashboard", (object)account.UrlImagemDashboard ?? DBNull.Value);

                //-----------------------------CREATE-----------------------------------------
                command.Parameters.AddWithValue("@CreationDate", account.CreationDate);
                command.Parameters.AddWithValue("@CreationUserId", account.CreationUserId);
                command.Parameters.AddWithValue("@CreationUserName", account.CreationUserName);

                //-----------------------------CHANGE-----------------------------------------
                command.Parameters.AddWithValue("@ChangeDate", account.ChangeDate);
                command.Parameters.AddWithValue("@ChangeUserId", account.ChangeUserId);
                command.Parameters.AddWithValue("@ChangeUserName", account.ChangeUserName);

                //----------------------------EXCLUSION--------------------------------------
                command.Parameters.AddWithValue("@ExclusionDate", (object)account.ExclusionDate ?? DBNull.Value);
                command.Parameters.AddWithValue("@ExclusionUserId", (object)account.ExclusionUserId ?? DBNull.Value);
                command.Parameters.AddWithValue("@ExclusionUserName", (object)account.ExclusionUserName ?? DBNull.Value);

                //-----------------------------STATUS----------------------------------------
                command.Parameters.AddWithValue("@RecordStatus", account.RecordStatus);


 */