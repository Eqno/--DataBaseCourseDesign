using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Data.SqlClient;

public class SqlCache : MonoBehaviour
{
    private static SqlConnection SqlConnection;
    static SqlCache()
    {
        SqlConnection = new SqlConnection(
            "Data Source=localhost;database=快餐订购系统;Integrated Security=True;User Instance=False;"
        );
        Debug.Log("成功链接数据库");
    }
    public static void OpenSql()
    {
        if (SqlConnection.State == System.Data.ConnectionState.Closed)
        {
            SqlConnection.Open();
            Debug.Log("成功打开数据库");
        }
    }
    public static void CloseSql()
    {
        if (SqlConnection.State == System.Data.ConnectionState.Open)
        {   
            SqlConnection.Close();
            Debug.Log("成功关闭数据库");
        }
    }
    public static bool QuaryAccount(string accountName, string password = null)
    {
        return true;
        // OpenSql();
        // bool isMatch = false;

        // StringBuilder commandStr = new StringBuilder();
        // commandStr.Append(" Select * From Account");
        // commandStr.Append(" Where AccountName = '" + accountName + "'");

        // if (!string.IsNullOrEmpty(password))               //加了密码就是检测帐号密码同时匹配
        //     commandStr.Append(" And Password = '" + password + "'");

        // try
        // {
        //     //执行命令添加到sqlCommand，执行并读取结果
        //     SqlCommand sqlCommand = new SqlCommand(commandStr.ToString(), SqlConnection);
        //     SqlDataReader reader = sqlCommand.ExecuteReader();
        //     if (reader.Read())              //如果有查到返回true
        //         isMatch = true;
        //     MyApplication.Log("查询用户存在、或账户密码匹配成功");
        // }
        // catch (Exception e) { MyApplication.Log("查询用户存在 失败了啊 " + e.ToString()); }
        // finally { CloseSql(); }         //关闭数据连接

        // return isMatch;
    }
}
