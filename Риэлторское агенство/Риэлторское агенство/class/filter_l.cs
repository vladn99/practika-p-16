using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class filter_l : potr
{
    private int mins;
    private int maxs;

    public filter_l(int mins, int maxs, int mincena, int maxcena, int klient, int agent, string obj, string city, string street) :base(mincena, maxcena, klient, agent, obj, city, street)
    {
        this.mins = mins;
        this.maxs = maxs;
    }
    
    public int get_mins()
    {
        return mins;
    }
    
    public int get_maxs()
    {
        return maxs;
    }
}

