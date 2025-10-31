using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectApp.Infrastructure.Sql
{
    public interface IConnectionProvider
    {
        SqlConnection GetConnection();
    }
}
