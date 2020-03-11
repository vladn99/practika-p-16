using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Риэлторское_агенство
{
    public partial class Form4 : Form
    {
        private string[] arr = null;
        private string[] arr_for_numbs = null;
        private string substr = "";

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
            @base @base = new @base("select potr.Id, potr.mincena, potr.maxcena, potr.agent, potr.klient, potr.city, potr.street, filter_h.minetag, filter_h.maxetag, filter_h.minrooms, filter_h.maxrooms, filter_h.mins, filter_h.maxs from potr, filter_h where potr.dop_info = filter_h.Id and potr.obj = N'Дом'");
            string s = @base.vuvod_zakazov("house");
            @base.smena_zaprosa("select potr.Id, potr.mincena, potr.maxcena, potr.agent, potr.klient, potr.city, potr.street, filter_kw.minetag, filter_kw.maxetag, filter_kw.minrooms, filter_kw.maxrooms, filter_kw.mins, filter_kw.maxs from potr, filter_kw where potr.dop_info = filter_kw.Id and potr.obj = N'Квартира'");
            s += @base.vuvod_zakazov("kw");
            @base.smena_zaprosa("select potr.Id, potr.mincena, potr.maxcena, potr.agent, potr.klient, potr.city, potr.street, filter_l.mins, filter_l.maxs from potr, filter_l where potr.dop_info = filter_l.Id and potr.obj = N'Земля'");
            s += @base.vuvod_zakazov("land");
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

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            string str = comboBox1.Text;
            char number;
            foreach (var item in str)
            {
               number = item;
               if (Char.IsDigit(number))
               {
                   substr += number;
               }
               else
                   substr += " ";
            }
            substr = Regex.Replace(substr, @"\s+", " ");
            arr_for_numbs = substr.Split(' ');
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = comboBox1.Text;
            string city = Regex.Match(str, @"Город:\S*").ToString().Replace("Город:", "");
            string street = Regex.Match(str, @"Улица:\S*").ToString().Replace("Улица:", "");
        }
    }
}
