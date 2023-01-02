using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO.Compression;
using System.IO;
using System.Text;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{

    [Microsoft.SqlServer.Server.SqlFunction(Name = "Fn_Seq", DataAccess = DataAccessKind.Read)]
    public static SqlInt64 Fn_Seq(SqlString SchemaName, SqlString SeqName)
    {
        SqlDataReader reader;
        SqlCommand command;
        string str;
        SqlInt64 seqVal = 0;

        str = "SELECT Next Value for " + (string)SchemaName  + "." + (string)SeqName; ;

        using (SqlConnection sqlCnn = new SqlConnection("context connection=true"))
        {
            sqlCnn.Open();
            command = new SqlCommand(str, sqlCnn);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                seqVal = reader.GetInt32(0);
            }

        }

        return seqVal;
    }
}
