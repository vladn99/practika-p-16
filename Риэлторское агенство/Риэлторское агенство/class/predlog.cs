using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class predlog
{
    private int cena;
    private int klient;
    private int agent;
    private int obj;

    public predlog(int cena, int klient, int agent, int obj)
    {
        this.cena = cena;
        this.klient = klient;
        this.agent = agent;
        this.obj = obj;
    }

    public int get_cena()
    {
        return cena;
    }

    public int get_klient()
    {
        return klient;
    }

    public int get_agent()
    {
        return agent;
    }

    public int get_obj()
    {
        return obj;
    }
}
