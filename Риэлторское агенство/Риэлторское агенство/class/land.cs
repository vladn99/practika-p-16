using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class land:obj
{
    private int s;

    public land(string city, string street, int nm_h, int nm_kw, int x, int y, int s) :base(city, street, nm_h, nm_kw, x, y)
    {
        this.s = s;
    }

    public int get_s()
    {
        return s;
    }
}

