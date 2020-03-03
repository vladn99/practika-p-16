using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class sdelka
{
    private int predlog;
    private int potr;

    public sdelka(int predlog, int potr)
    {
        this.predlog = predlog;
        this.potr = potr;
    }

    public int get_predlog()
    {
        return predlog;
    }

    public int get_potr()
    {
        return potr;
    }
}
