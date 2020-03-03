using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class filter_h : potr
{
    private int minetag;
    private int maxetag;
    private int minrooms;
    private int maxrooms;
    private int mins;
    private int maxs;

    public filter_h(int minetag, int maxetag,  int minrooms, int maxrooms, int mins, int maxs, int mincena, int maxcena, int klient, int agent, string obj, string city, string street) :base(mincena, maxcena, klient, agent, obj, city, street)
    {
        this.minetag = minetag;
        this.minrooms = minrooms;
        this.mins = mins;
        this.maxetag = maxetag;
        this.maxrooms = maxrooms;
        this.maxs = maxs;
    }
    
    public int get_minetag()
    {
        return minetag;
    }

    public int get_minrooms()
    {
        return minrooms;
    }

    public int get_mins()
    {
        return mins;
    }
    
    public int get_maxetag()
    {
        return maxetag;
    }

    public int get_maxrooms()
    {
        return maxrooms;
    }

    public int get_maxs()
    {
        return maxs;
    }
}

