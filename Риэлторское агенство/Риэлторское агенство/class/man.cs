using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class man
{
    private string fam;
    private string name;
    private string oth;

    public man(string fam, string name, string oth)
    {
        this.fam = fam;
        this.name = name;
        this.oth = oth;
    }

    public string get_fam()
    {
        return fam;
    }

    public string get_name()
    {
        return name;
    }

    public string get_oth()
    {
        return oth;
    }
}
