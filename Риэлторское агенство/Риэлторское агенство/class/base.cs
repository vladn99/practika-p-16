using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

class @base
{
    private static string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory.Replace(@"\bin\Debug", "") + "\\base.mdf;Integrated Security=True;";
    SqlConnection connection = new SqlConnection(ConnectionString);
    private string data = "";
    private string sqlExpression = "";
    public @base(string sqlExpression) 
    {
        smena_zaprosa(sqlExpression);
    }
    public void zapis_v_bd() 
    {
        connection.Open();
        SqlCommand command = new SqlCommand(sqlExpression, connection);
        command.ExecuteNonQuery();
        connection.Close();
    }
    public Boolean proverka_znachenei_v_bd()
    {
        this.data = "";
        Boolean rez;
        connection.Open();
        SqlCommand command = new SqlCommand(sqlExpression, connection);
        SqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                try
                {
                    data += reader.GetInt32(0) + " " + reader.GetString(1) + " " + reader.GetString(2) + " " + reader.GetString(3) + "&";
                }
                catch 
                {
                    break;
                }
            }
            rez = true;
        }
        else
        {
            rez = false;
        }
        connection.Close();
        return rez;
    }
    public void smena_zaprosa(string sqlExpression) 
    {
        this.sqlExpression = sqlExpression;
    }

    public string vuvod() 
    {
        Boolean proverka = proverka_znachenei_v_bd();
        if (proverka == false)
        {
            data = "Отсутствую данные в БД";
        }
        return data;
    }

    public string vuvod_obj(string type) 
    {
        this.data = "";
        connection.Open();
        SqlCommand command = new SqlCommand(sqlExpression, connection);
        SqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            if (type == "house")
            {
                while (reader.Read())
                {
                    data += reader.GetInt32(0) + " Дом Город:" + reader.GetString(1) + " Улица:" + reader.GetString(2) + " № дома:" + reader.GetInt32(3) + " Этажность:" + reader.GetInt32(4) + " Кол-во комнат:" + reader.GetInt32(5) + " Площадь:" + reader.GetInt32(6) + "&";
                }
            }
            else if (type == "kw")
            {
                while (reader.Read())
                {
                    try
                    {
                        data += reader.GetInt32(0) + " Квартира Город:" + reader.GetString(1) + " Улица:" + reader.GetString(2) + " № дома:" + reader.GetInt32(3) + " № квартиры:" + reader.GetInt32(4) + " Этаж:" + reader.GetInt32(5) + " Кол-во комнат:" + reader.GetInt32(6) + " Площадь:" + reader.GetInt32(7) + "&";
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            else if(type == "land")
            {
                while (reader.Read())
                {
                    data += reader.GetInt32(0) + " Земля Город:" + reader.GetString(1) + " Улица:" + reader.GetString(2) + " Площадь:" + reader.GetInt32(3) + "&";
                }
            }
        }
        connection.Close();
        return data;
    }

    public string vuvod_zakazov(string type) 
    {
        string dat = "";
        connection.Open();
        SqlCommand command = new SqlCommand(sqlExpression, connection);
        SqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            if (type == "house")
            {
                while (reader.Read())
                {
                    dat += reader.GetInt32(0) + " Дом Цена:от " + reader.GetInt32(1) + " до " + reader.GetInt32(2) + " Город:" + reader.GetString(5) + " Улица:" + reader.GetString(6) + " Этажность:от " + reader.GetInt32(7) + " до " + reader.GetInt32(8) + " Кол-во комнат:от " + reader.GetInt32(9) + " до " + reader.GetInt32(10) + " Площадь:от " + reader.GetInt32(11) + " до " + reader.GetInt32(12) + " Риэлтор:" + reader.GetInt32(3) + " Клиент:" + reader.GetInt32(4) +" &";
                }
            }
            else if (type == "kw")
            {
                while (reader.Read())
                {
                    try
                    {
                        dat += reader.GetInt32(0) + " Квартира Город:" + reader.GetString(1) + " Улица:" + reader.GetString(2) + " № дома:" + reader.GetInt32(3) + " № квартиры:" + reader.GetInt32(4) + " Этаж:" + reader.GetInt32(5) + " Кол-во комнат:" + reader.GetInt32(6) + " Площадь:" + reader.GetInt32(7) + "&";
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            else if (type == "land")
            {
                while (reader.Read())
                {
                    dat += reader.GetInt32(0) + " Земля Город:" + reader.GetString(1) + " Улица:" + reader.GetString(2) + " Площадь:" + reader.GetInt32(3) + "&";
                }
            }
        }
        connection.Close();
        //smena_zaprosa("select distinct agent.Id, man.fam, man.name, man.otch from man, klient, agent where man.dop_info = " + id_ag + " and klient.Id <> " + id_ag + " and man.dop_info = agent.Id");
        //bool per = proverka_znachenei_v_bd();
        //dat += " Риэлтор: " + data.Replace("&", "");
        return dat;
    }
}

