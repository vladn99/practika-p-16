using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class klient:man
{
    private string phone;
    private string mail;

    public klient(string fam, string name, string oth, string phone, string mail):base(fam, name, oth)
    {
        this.phone = phone;
        this.mail = mail;
    }

    public string get_mail()
    {
        return mail;
    }

    public string get_phone()
    {
        return phone;
    }
}
