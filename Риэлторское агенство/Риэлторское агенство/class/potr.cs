using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class potr
{
    private int mincena;
    private int maxcena;
    private int klient;
    private int agent;
    private string obj;
    private string city;
    private string street;

    public potr(int mincena, int maxcena,  int klient, int agent, string obj, string city, string street)
    {
        this.mincena = mincena;
        this.maxcena = maxcena;
        this.klient = klient;
        this.agent = agent;
        this.obj = obj;
        this.city = city;
        this.street = street;
    }

    public int get_mincena()
    {
        return mincena;
    }
    public int get_maxcena()
    {
        return maxcena;
    }

    public int get_klient()
    {
        return klient;
    }

    public int get_agent()
    {
        return agent;
    }

    public string get_obj()
    {
        return obj;
    }

    public string get_city()
    {
        return city;
    }

    public string get_street()
    {
        return street;
    }
}
