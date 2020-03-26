using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

using System.Windows.Forms;

class @base
{
    private static string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory.Replace(@"\bin\Debug", "") + "\\base.mdf;Integrated Security=True;";
    SqlConnection connection = new SqlConnection(ConnectionString);
    private string data = "";
    private string sqlExpression = "";
    private bool zapis_ag_and_kl = false;
    private string fio = "";
    public @base(string sqlExpression) 
    {
        smena_zaprosa(sqlExpression);
        AppDomain.CurrentDomain.SetData("DataDirectory", Application.StartupPath.Replace(@"\bin\Debug", ""));
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
        if(zapis_ag_and_kl == false)
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
                    if (zapis_ag_and_kl == true)
                    {
                        fio = reader.GetInt32(0) + " " + reader.GetString(1) + " " + reader.GetString(2) + " " + reader.GetString(3);
                        continue;
                    }
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

    public string vuvod_obj(string type, bool btn_pow)
    {
        this.zapis_ag_and_kl = btn_pow;
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
                    try
                    {
                        data += reader.GetInt32(0) + " Дом Город:" + reader.GetString(1) + " Улица:" + reader.GetString(2) + " № дома:" + reader.GetInt32(3) + " Этажность:" + reader.GetInt32(4) + " Кол-во комнат:" + reader.GetInt32(5) + " Площадь:" + reader.GetInt32(6) + "&";
                        if (zapis_ag_and_kl == true)
                            data = data.Remove(data.Length - 1, 1) + " Риелтор:" + reader.GetInt32(7) + " Клиент:" + reader.GetInt32(8) + " &";
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            else if (type == "kw")
            {
                while (reader.Read())
                {
                    try
                    {
                        data += reader.GetInt32(0) + " Квартира Город:" + reader.GetString(1) + " Улица:" + reader.GetString(2) + " № дома:" + reader.GetInt32(3) + " № квартиры:" + reader.GetInt32(4) + " Этаж:" + reader.GetInt32(5) + " Кол-во комнат:" + reader.GetInt32(6) + " Площадь:" + reader.GetInt32(7) + "&";
                        if (zapis_ag_and_kl == true)
                            data = data.Remove(data.Length - 1, 1) + " Риелтор:" + reader.GetInt32(8) + " Клиент:" + reader.GetInt32(9) + " &";
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
                    try
                    {
                        data += reader.GetInt32(0) + " Земля Город:" + reader.GetString(1) + " Улица:" + reader.GetString(2) + " Площадь:" + reader.GetInt32(3) + "&";
                        if (zapis_ag_and_kl == true)
                            data = data.Remove(data.Length - 1, 1) + " Риелтор:" + reader.GetInt32(4) + " Клиент:" + reader.GetInt32(5) + " &";
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }
        connection.Close();
        if (zapis_ag_and_kl == true)
        { 
            zapis_ag_kl("select distinct agent.Id, man.fam, man.name, man.otch from man, agent, klient where man.dop_info = agent.Id and klient.Id <> @ and agent.Id = @", "Риелтор:");
            zapis_ag_kl("select distinct klient.Id, man.fam, man.name, man.otch from man, agent, klient where man.dop_info = klient.Id and agent.Id <> @ and klient.Id = @", "Клиент:");
        }
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
                        dat += reader.GetInt32(0) + " Квартира Цена:от " + reader.GetInt32(1) + " до " + reader.GetInt32(2) + " Город:" + reader.GetString(5) + " Улица:" + reader.GetString(6) + " Этаж:от " + reader.GetInt32(7) + " до " + reader.GetInt32(8) + " Кол-во комнат:от " + reader.GetInt32(9) + " до " + reader.GetInt32(10) + " Площадь:от " + reader.GetInt32(11) + " до " + reader.GetInt32(12) + " Риэлтор:" + reader.GetInt32(3) + " Клиент:" + reader.GetInt32(4) + " &";
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
                    dat += reader.GetInt32(0) + " Земля Цена:от " + reader.GetInt32(1) + " до " + reader.GetInt32(2) + " Город:" + reader.GetString(5) + " Улица:" + reader.GetString(6) + " Площадь:от " + reader.GetInt32(7) + " до " + reader.GetInt32(8) + " Риэлтор:" + reader.GetInt32(3) + " Клиент:" + reader.GetInt32(4) + " &";
                }
            }
        }
        connection.Close();
        return dat;
    }

    private void zapis_ag_kl(string zapr, string str_for_zamenu) 
    {
        MatchCollection matchs = Regex.Matches(data, str_for_zamenu + @"\S*");
        foreach (Match item in matchs)
        {
            smena_zaprosa(zapr.Replace("@", item.ToString().Replace(str_for_zamenu, "")));
            proverka_znachenei_v_bd();
            data = data.Replace(item.ToString(), str_for_zamenu + fio);
        }
    }

    public string get_cena() 
    {
        string time_str = "Цена:";
        connection.Open();
        SqlCommand command = new SqlCommand(sqlExpression, connection);
        SqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                time_str += reader.GetInt32(0);
            }
        }
        connection.Close();
        return time_str;
    }

    public string dealshear() 
    {
        string dlshr = "";
        connection.Open();
        SqlCommand command = new SqlCommand(sqlExpression, connection);
        SqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                dlshr += Convert.ToString(reader.GetInt32(0)) + "@";
            }
        }
        connection.Close();
        return dlshr.Remove(dlshr.Length - 1);
    }
}

