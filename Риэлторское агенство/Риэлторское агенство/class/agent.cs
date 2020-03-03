using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class agent:man
{
    private int dealshare;

    public agent(string fam, string name, string oth, int dealshare):base(fam, name, oth)
    {
        this.dealshare = dealshare;
    }

    public int get_dealshare()
    {
        return dealshare;
    }
}
