using System;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Data.SqlClient;

public class SqlCache : MonoBehaviour
{
    private static int MAXORDERNUM = 1000;
    private static SqlDataReader reader;
    private static SqlConnection SqlConnection;
    static SqlCache()
    {
        SqlConnection = new SqlConnection(
            "Data Source=localhost;database=快餐订购系统;uid=fastfood;pwd=123456789;"
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
    public static bool QuaryAccount(string username, string password)
    {
        OpenSql();
        bool res = new SqlCommand(
            "select * from usrinfo where usr= \'"+username+"\' and pwd= \'"+password+"\'",
            SqlConnection
        ).ExecuteReader().Read();
        CloseSql();
        return res;
    }
    public static void CreateAccount(string username, string password)
    {
        OpenSql();
        new SqlCommand(
            "insert into usrinfo(usr, pwd, blc) select \'"+username+"\', \'"+password+"\', 0",
            SqlConnection
        ).ExecuteReader().Read();
        CloseSql();
    }
    public static List<List<string>> ListStores()
    {
        OpenSql();
        reader = new SqlCommand(
            "select * from storeinfo",
            SqlConnection
        ).ExecuteReader();
        List<List<string>> res = new List<List<string>>();
        while (reader.Read())
        {
            List<string> tmp = new List<string>();
            tmp.Add(reader["stnm"].ToString());
            tmp.Add(reader["optm"].ToString());
            tmp.Add(reader["cltm"].ToString());
            res.Add(tmp);
        }
        CloseSql();
        return res;
    }
    public static int QuaryBalance(string username, string password)
    {
        OpenSql();
        reader = new SqlCommand(
            "select blc from usrinfo where usr= \'"+username+"\' and pwd= \'"+password+"\'",
            SqlConnection
        ).ExecuteReader();
        int res = 0;
        if (reader.Read()) res = int.Parse(reader["blc"].ToString());
        CloseSql();
        return res;
    }
    public static List<List<string>> ListFoods(string store)
    {
        OpenSql();
        reader = new SqlCommand(
            "select * from menuinfo where store=\'"+store+"\'",
            SqlConnection
        ).ExecuteReader();
        List<List<string>> res = new List<List<string>>();
        while (reader.Read())
        {
            List<string> tmp = new List<string>();
            tmp.Add(reader["food"].ToString());
            tmp.Add(reader["price"].ToString());
            res.Add(tmp);
        }
        CloseSql();
        return res;
    }
    public static int QuaryPrice(string store, string food)
    {
        OpenSql();
        reader = new SqlCommand(
            "select price from menuinfo where store=\'"+store+"\' and food=\'"+food+"\'",
            SqlConnection
        ).ExecuteReader();
        int res = 0;
        if (reader.Read()) res = int.Parse(reader["price"].ToString());
        CloseSql();
        return res;
    }
    public static void UpdateBalance(string username, string password, int num)
    {
        OpenSql();
        new SqlCommand(
            "update usrinfo set blc="+num.ToString()+" where usr=\'"+username+"\' and pwd=\'"+password+"\'",
            SqlConnection
        ).ExecuteReader().Read();
        CloseSql();
    }
    public static List<List<string>> ListGifts()
    {
        OpenSql();
        reader = new SqlCommand(
            "select * from giftinfo",
            SqlConnection
        ).ExecuteReader();
        List<List<string>> res = new List<List<string>>();
        while (reader.Read())
        {
            List<string> tmp = new List<string>();
            tmp.Add(reader["store"].ToString());
            tmp.Add(reader["food"].ToString());
            res.Add(tmp);
        }
        CloseSql();
        return res;
    }
    public static void AddOrder(string username, string password, List<List<string>> order, string address)
    {
        OpenSql();
        reader = new SqlCommand(
            "select num from orderinfo where usr=\'"+username+"\' and pwd=\'"+password+"\'",
            SqlConnection
        ).ExecuteReader();
        int cot = 0;
        HashSet<int> st = new HashSet<int>();
        while (reader.Read())
        {
            string tmp = reader[0].ToString();
            if (tmp.Length > 0) st.Add(int.Parse(tmp));
        }
        for (cot=0; cot<=MAXORDERNUM; cot++)
            if (! st.Contains(cot)) break;
        CloseSql();
        foreach (var i in order)
        {
            OpenSql();
            new SqlCommand(
                "insert into orderinfo(usr, pwd, num, store, food, cotn, addr) select \'"+username+"\', \'"
                +password+"\', "+cot.ToString()+", \'"+i[0]+"\', \'"+i[1]+"\', "+i[2]+", \'"+address+"\'",
                SqlConnection
            ).ExecuteReader();
            CloseSql();
        }
    }
    public static List<List<string>> ListOrders(string username, string password)
    {
        OpenSql();
        reader = new SqlCommand(
            "select distinct num, addr from orderinfo where usr=\'"+username+"\' and pwd=\'"+password+"\'",
            SqlConnection
        ).ExecuteReader();
        List<List<string>> res = new List<List<string>>();
        while (reader.Read())
        {
            List<string> tmp = new List<string>();
            tmp.Add(reader[0].ToString());
            tmp.Add(reader[1].ToString());
            res.Add(tmp);
        }
        CloseSql();
        return res;
    }
    public static List<List<string>> ListOrderItems(string username, string password, int num)
    {
        OpenSql();
        reader = new SqlCommand(
            "select store, food, cotn from orderinfo where usr=\'"+username+"\' and pwd=\'"+password+"\' and num="+num.ToString(),
            SqlConnection
        ).ExecuteReader();
        List<List<string>> res = new List<List<string>>();
        while (reader.Read())
        {
            List<string> tmp = new List<string>();
            tmp.Add(reader[0].ToString());
            tmp.Add(reader[1].ToString());
            tmp.Add(reader[2].ToString());
            res.Add(tmp);
        }
        CloseSql();
        return res;
    }
    public static void RemoveOrder(string username, string password, int num)
    {
        OpenSql();
        new SqlCommand(
            "delete from orderinfo where usr=\'"+username+"\' and pwd=\'"+password+"\' and num="+num.ToString(),
            SqlConnection
        ).ExecuteReader();
        CloseSql();
    }
    private static bool ExecuteCommand(string command)
    {
        return reader.Read();
    }
}
