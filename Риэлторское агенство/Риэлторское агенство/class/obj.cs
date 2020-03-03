using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class obj
{
    private string city;
    private string street;
    private int nm_h;
    private int nm_kw;
    private int x;
    private int y;

    public obj(string city, string street, int nm_h, int nm_kw, int x, int y)
    {
        this.city = city;
        this.street = street;
        this.nm_h = nm_h;
        this.nm_kw = nm_kw;
        this.x = x;
        this.y = y;
    }

    public string get_city()
    {
        return city;
    }

    public string get_street()
    {
        return street;
    }

    public int get_nm_h()
    {
        return nm_h;
    }

    public int get_nm_kw()
    {
        return nm_kw;
    }

    public int get_x()
    {
        return x;
    }

    public int get_y()
    {
        return y;
    }
}

