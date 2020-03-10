using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Риэлторское_агенство
{
    public partial class Form4 : Form
    {
        private string[] arr = null;
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            @base @base = new @base("select potr.Id, potr.mincena, potr.maxcena, potr.agent, potr.klient, potr.city, potr.street, filter_h.minetag, filter_h.maxetag, filter_h.minrooms, filter_h.maxrooms, filter_h.mins, filter_h.maxs from potr, filter_h where potr.dop_info = filter_h.Id");
            string s = @base.vuvod_zakazov("house");
            comboBox1.Items.Clear();
            arr = s.Split('&');
            for (int i = 0; i < arr.Length - 1; i++)
            {
                int id_ag = Convert.ToInt32(arr[i].Substring(arr[i].IndexOf('Р')).Replace("Риэлтор:", "").Remove(arr[i].Substring(arr[i].IndexOf('Р')).Replace("Риэлтор:", "").IndexOf(" ")));
                int id_kl = Convert.ToInt32(arr[i].Substring(arr[i].LastIndexOf(':') + 1).Replace("Клиент:", "").Remove(arr[i].Substring(arr[i].LastIndexOf(':') + 1).Replace("Клиент:", "").IndexOf(" ")));
                @base.smena_zaprosa("select distinct agent.Id, man.fam, man.name, man.otch from man, klient, agent where man.dop_info = " + id_ag + " and klient.Id <> " + id_ag + " and man.dop_info = agent.Id");
                string per = @base.vuvod();
                arr[i] = arr[i].Replace(id_ag.ToString(), per.Replace("&", ""));
                @base.smena_zaprosa("select distinct klient.Id, man.fam, man.name, man.otch from man, klient, agent where man.dop_info = " + id_kl + " and agent.Id <> " + id_kl + " and man.dop_info = klient.Id");
                per = @base.vuvod();
                arr[i] = arr[i].Replace(id_kl.ToString(), per.Replace("&", ""));
            }
            comboBox1.Items.AddRange(arr);
        }
    }
}
