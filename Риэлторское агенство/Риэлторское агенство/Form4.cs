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
        private string[] arr_for_numbs_potr = null;
        private string[] arr_for_numbs_predl = null;
        private string substr = "";
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            //string s = "";
            //@base @base = new @base("select potr.Id, potr.mincena, potr.maxcena, potr.agent, potr.klient, potr.city, potr.street, filter_h.minetag, filter_h.maxetag, filter_h.minrooms, filter_h.maxrooms, filter_h.mins, filter_h.maxs from potr, filter_h, sdelka where potr.dop_info = filter_h.Id and potr.obj = N'Дом' and sdelka.potr <> potr.Id");
            //s += @base.vuvod_zakazov("house");
            //@base.smena_zaprosa("select potr.Id, potr.mincena, potr.maxcena, potr.agent, potr.klient, potr.city, potr.street, filter_kw.minetag, filter_kw.maxetag, filter_kw.minrooms, filter_kw.maxrooms, filter_kw.mins, filter_kw.maxs from potr, filter_kw, sdelka where potr.dop_info = filter_kw.Id and potr.obj = N'Квартира' and sdelka.potr <> potr.Id");
            //s += @base.vuvod_zakazov("kw");
            //@base.smena_zaprosa("select potr.Id, potr.mincena, potr.maxcena, potr.agent, potr.klient, potr.city, potr.street, filter_l.mins, filter_l.maxs from potr, filter_l, sdelka where potr.dop_info = filter_l.Id and potr.obj = N'Земля' and sdelka.potr <> potr.Id");
            //s += @base.vuvod_zakazov("land");
            //comboBox1.Items.Clear();
            //if (s == "")
            //    comboBox1.Items.Add("Отсутствуют данные в БД");
            //else
            //{
            //    arr = s.Split('&');
            //    for (int i = 0; i < arr.Length - 1; i++)
            //    {
            //        int id_ag = Convert.ToInt32(arr[i].Substring(arr[i].IndexOf('Р')).Replace("Риэлтор:", "").Remove(arr[i].Substring(arr[i].IndexOf('Р')).Replace("Риэлтор:", "").IndexOf(" ")));
            //        int id_kl = Convert.ToInt32(arr[i].Substring(arr[i].LastIndexOf(':') + 1).Replace("Клиент:", "").Remove(arr[i].Substring(arr[i].LastIndexOf(':') + 1).Replace("Клиент:", "").IndexOf(" ")));
            //        @base.smena_zaprosa("select distinct agent.Id, man.fam, man.name, man.otch from man, klient, agent where man.dop_info = " + id_ag + " and klient.Id <> " + id_ag + " and man.dop_info = agent.Id");
            //        string per = @base.vuvod();
            //        arr[i] = arr[i].Replace("Риэлтор:" + id_ag.ToString(), "Риэлтор:" + per.Replace("&", ""));
            //        @base.smena_zaprosa("select distinct klient.Id, man.fam, man.name, man.otch from man, klient, agent where man.dop_info = " + id_kl + " and agent.Id <> " + id_kl + " and man.dop_info = klient.Id");
            //        per = @base.vuvod();
            //        arr[i] = arr[i].Replace("Клиент:" + id_kl.ToString(), "Клиент:" + per.Replace("&", ""));
            //    }
            //    comboBox1.Items.AddRange(arr);
            //}
            //s = "";
            //arr = null;
            //@base.smena_zaprosa("select predlog.Id, obj.city, obj.street, obj.nm_h, house.etag, house.rooms, house.s, predlog.agent, predlog.klient from obj, house, predlog, sdelka where obj.dop_inf = house.Id and obj.Id = predlog.obj and obj.nm_kw is null and sdelka.predl <> predlog.Id");
            //s += @base.vuvod_obj("house", true);
            //@base.smena_zaprosa("select predlog.Id, obj.city, obj.street, obj.nm_h, obj.nm_kw, kw.etag, kw.rooms, kw.s, predlog.agent, predlog.klient from obj, kw, predlog, sdelka where obj.dop_inf = kw.Id and obj.Id = predlog.obj and obj.nm_kw is not null and sdelka.predl <> predlog.Id");
            //s += @base.vuvod_obj("kw", true);
            //@base.smena_zaprosa("select predlog.Id, obj.city, obj.street, land.s, predlog.agent, predlog.klient from obj, land, predlog, sdelka where obj.dop_inf = land.Id and obj.Id = predlog.obj and obj.nm_kw is null and obj.nm_h is null and sdelka.predl <> predlog.Id");
            //s += @base.vuvod_obj("land", true);
            //if (s == "")
            //    comboBox2.Items.Add("Отсутствуют данные в БД");
            //else
            //{
            //    string time_str;
            //    arr = s.Split('&');
            //    for (int i = 0; i < arr.Length; i++)
            //    {
            //        try
            //        {
            //            time_str = "";
            //            @base.smena_zaprosa("select cena from predlog where Id = " + arr[i].Remove(arr[i].IndexOf(" ")));
            //            time_str = @base.get_cena();
            //            arr[i] = arr[i].Insert(arr[i].Contains("Э") == true ? arr[i].IndexOf("Э") : arr[i].IndexOf("П"), " " + time_str + " ");
            //        }
            //        catch
            //        {
            //            continue;
            //        }
            //    }
            //    comboBox2.Items.AddRange(arr);
            //}
            reboot();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            arr_for_numbs_potr = polychenie_chisl_zn(comboBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Boolean rez;
            if (comboBox1.Text.Contains("Дом") == true && comboBox2.Text.Contains("Дом") == true)
            {
                rez = srawnenie_predl_potr(2);
                dlya_sokr(rez);
                
            }
            else if (comboBox1.Text.Contains("Квартира") == true && comboBox2.Text.Contains("Квартира") == true)
            {
                rez = srawnenie_predl_potr(3);
            }
            else if (comboBox1.Text.Contains("Земля") == true && comboBox2.Text.Contains("Земля") == true)
            {
                rez = srawnenie_predl_potr(1);
            }
            else
                MessageBox.Show("Типы выбранных вами объектов не совпадают", "Предуприждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private string [] polychenie_chisl_zn(ComboBox cmb) 
        {
            string[] array = null;
            substr = "";
            string str = cmb.Text;
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
            array = substr.Split(' ');
            return array;
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            arr_for_numbs_predl = polychenie_chisl_zn(comboBox2);
        }

        private Boolean srawnenie_predl_potr(int str_ind) 
        {
            int nesootv = 0;
            int j = 2;
            Boolean rez;
            for (int i = str_ind; i < arr_for_numbs_predl.Length - 3; i++)
            {
                if ((i == str_ind) == true ? !(Convert.ToInt32(arr_for_numbs_predl[i]) <= Convert.ToInt32(arr_for_numbs_potr[j - 1]) || Convert.ToInt32(arr_for_numbs_predl[i]) <= Convert.ToInt32(arr_for_numbs_potr[j])):
                    !(Convert.ToInt32(arr_for_numbs_predl[i]) >= Convert.ToInt32(arr_for_numbs_potr[j - 1]) && Convert.ToInt32(arr_for_numbs_predl[i]) <= Convert.ToInt32(arr_for_numbs_potr[j])))
                    nesootv++;
                j += 2;
            }
            Regex for_gorod = new Regex(@"Город:\w*\s"), for_street = new Regex(@"Улица:\w*\s");
            Match gorod_predl = for_gorod.Match(comboBox2.Text), gorod_potr = for_gorod.Match(comboBox1.Text), 
                street_predl = for_street.Match(comboBox2.Text), street_potr = for_street.Match(comboBox1.Text);
            if (gorod_potr.ToString().Equals(gorod_predl.ToString()) == false)
            {
                if (!(gorod_potr.ToString().Replace("Город:", "") == "") || !(gorod_potr.ToString().Replace("Город:", "") == "" && gorod_predl.ToString().Replace("Город:", "") == ""))
                    nesootv++;
            }
            if (street_potr.ToString().Equals(street_predl.ToString()) == false)
            {
                if (!(street_potr.ToString().Replace("Улица:", "") == "") || !(street_potr.ToString().Replace("Улица:", "") == "" && street_predl.ToString().Replace("Улица:", "") == ""))
                    nesootv++;
            }
            if (nesootv > 0)
                rez = false;
            else
                rez = true;
            return rez;
        }

        private void dlya_sokr(Boolean rez) 
        {
            if (rez == false)
                MessageBox.Show("Несоответствие предложения и потребности", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                sdelka sdelka = new sdelka(Convert.ToInt32(comboBox2.Text.Remove(comboBox2.Text.IndexOf(" "))), Convert.ToInt32(comboBox1.Text.Remove(comboBox2.Text.IndexOf(" "))));
                @base @base = new @base("insert into sdelka(predl, potr) values (" + sdelka.get_predlog() + ", " + sdelka.get_potr() + ")");
                @base.zapis_v_bd();
                MessageBox.Show("Данные записанны");
                reboot();
            }
        }

        private void reboot() 
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            string s = "";
            @base @base = new @base("select * from sdelka");
            bool pdf = @base.proverka_znachenei_v_bd();
            if (pdf == false)//если в тбл сделка есть записи один запрос иначе другой
            {
                MessageBox.Show("its work");
            }
            @base.smena_zaprosa("select potr.Id, potr.mincena, potr.maxcena, potr.agent, potr.klient, potr.city, potr.street, filter_h.minetag, filter_h.maxetag, filter_h.minrooms, filter_h.maxrooms, filter_h.mins, filter_h.maxs from potr, filter_h, sdelka where potr.dop_info = filter_h.Id and potr.obj = N'Дом' and sdelka.potr <> potr.Id");
            s += @base.vuvod_zakazov("house");
            @base.smena_zaprosa("select potr.Id, potr.mincena, potr.maxcena, potr.agent, potr.klient, potr.city, potr.street, filter_kw.minetag, filter_kw.maxetag, filter_kw.minrooms, filter_kw.maxrooms, filter_kw.mins, filter_kw.maxs from potr, filter_kw, sdelka where potr.dop_info = filter_kw.Id and potr.obj = N'Квартира' and sdelka.potr <> potr.Id");
            s += @base.vuvod_zakazov("kw");
            @base.smena_zaprosa("select potr.Id, potr.mincena, potr.maxcena, potr.agent, potr.klient, potr.city, potr.street, filter_l.mins, filter_l.maxs from potr, filter_l, sdelka where potr.dop_info = filter_l.Id and potr.obj = N'Земля' and sdelka.potr <> potr.Id");
            s += @base.vuvod_zakazov("land");
            comboBox1.Items.Clear();
            if (s == "")
                comboBox1.Items.Add("Отсутствуют данные в БД");
            else
            {
                arr = s.Split('&');
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    int id_ag = Convert.ToInt32(arr[i].Substring(arr[i].IndexOf('Р')).Replace("Риэлтор:", "").Remove(arr[i].Substring(arr[i].IndexOf('Р')).Replace("Риэлтор:", "").IndexOf(" ")));
                    int id_kl = Convert.ToInt32(arr[i].Substring(arr[i].LastIndexOf(':') + 1).Replace("Клиент:", "").Remove(arr[i].Substring(arr[i].LastIndexOf(':') + 1).Replace("Клиент:", "").IndexOf(" ")));
                    @base.smena_zaprosa("select distinct agent.Id, man.fam, man.name, man.otch from man, klient, agent where man.dop_info = " + id_ag + " and klient.Id <> " + id_ag + " and man.dop_info = agent.Id");
                    string per = @base.vuvod();
                    arr[i] = arr[i].Replace("Риэлтор:" + id_ag.ToString(), "Риэлтор:" + per.Replace("&", ""));
                    @base.smena_zaprosa("select distinct klient.Id, man.fam, man.name, man.otch from man, klient, agent where man.dop_info = " + id_kl + " and agent.Id <> " + id_kl + " and man.dop_info = klient.Id");
                    per = @base.vuvod();
                    arr[i] = arr[i].Replace("Клиент:" + id_kl.ToString(), "Клиент:" + per.Replace("&", ""));
                }
                comboBox1.Items.AddRange(arr);
            }
            s = "";
            arr = null;
            @base.smena_zaprosa("select predlog.Id, obj.city, obj.street, obj.nm_h, house.etag, house.rooms, house.s, predlog.agent, predlog.klient from obj, house, predlog, sdelka where obj.dop_inf = house.Id and obj.Id = predlog.obj and obj.nm_kw is null and sdelka.predl <> predlog.Id");
            s += @base.vuvod_obj("house", true);
            @base.smena_zaprosa("select predlog.Id, obj.city, obj.street, obj.nm_h, obj.nm_kw, kw.etag, kw.rooms, kw.s, predlog.agent, predlog.klient from obj, kw, predlog, sdelka where obj.dop_inf = kw.Id and obj.Id = predlog.obj and obj.nm_kw is not null and sdelka.predl <> predlog.Id");
            s += @base.vuvod_obj("kw", true);
            @base.smena_zaprosa("select predlog.Id, obj.city, obj.street, land.s, predlog.agent, predlog.klient from obj, land, predlog, sdelka where obj.dop_inf = land.Id and obj.Id = predlog.obj and obj.nm_kw is null and obj.nm_h is null and sdelka.predl <> predlog.Id");
            s += @base.vuvod_obj("land", true);
            if (s == "")
                comboBox2.Items.Add("Отсутствуют данные в БД");
            else
            {
                string time_str;
                arr = s.Split('&');
                for (int i = 0; i < arr.Length; i++)
                {
                    try
                    {
                        time_str = "";
                        @base.smena_zaprosa("select cena from predlog where Id = " + arr[i].Remove(arr[i].IndexOf(" ")));
                        time_str = @base.get_cena();
                        arr[i] = arr[i].Insert(arr[i].Contains("Э") == true ? arr[i].IndexOf("Э") : arr[i].IndexOf("П"), " " + time_str + " ");
                    }
                    catch
                    {
                        continue;
                    }
                }
                comboBox2.Items.AddRange(arr);
            }
        }
    }
}
