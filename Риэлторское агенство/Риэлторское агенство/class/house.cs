using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class house:obj
{
    private int etag;
    private int rooms;
    private int s;

    public house(string city, string street, int nm_h, int nm_kw, int x, int y, int etag, int rooms, int s) :base(city, street, nm_h, nm_kw, x, y)
    {
        this.etag = etag;
        this.rooms = rooms;
        this.s = s;
    }

    public int get_etag()
    {
        return etag;
    }

    public int get_rooms()
    {
        return rooms;
    }

    public int get_s()
    {
        return s;
    }
}

