using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO.Compression;
using System.IO;
using System.Text;
using Microsoft.SqlServer.Server;
using static System.Net.Mime.MediaTypeNames;

public partial class UserDefinedFunctions
{

    [Microsoft.SqlServer.Server.SqlFunction(Name = "Fn_Seq", DataAccess = DataAccessKind.Read)]
    public static SqlInt64 Fn_Seq(SqlString SchemaName, SqlString SeqName)
    {
        SqlDataReader reader;
        SqlCommand command;
        string str;
        SqlInt64 seqVal = 0;

        //str = "SELECT Next Value for " + (string)SchemaName  + "." + (string)SeqName; 
        //SELECT CAST(Next Value for dbo.seq_Teste AS BIGINT) AS SeqVal ;
        str = "SELECT CAST(Next Value for " + (string)SchemaName + "." + (string)SeqName + " AS BIGINT) AS SeqVal";  // 결과값은 무조건 BIGINT로
        
        using (SqlConnection sqlCnn = new SqlConnection("context connection=true"))
        {
            sqlCnn.Open();
            command = new SqlCommand(str, sqlCnn);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                //seqVal = reader.GetInt32(0);
                seqVal = reader.GetInt64(0);
            }

        }

        return seqVal;
    }
}
