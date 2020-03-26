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
            AppDomain.CurrentDomain.SetData("DataDirectory", Application.StartupPath.Replace(@"\bin\Debug", ""));
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            reboot_data_sdelka();
            label2.Text = "";
            label4.Text = "";
            label6.Text = "";
            label8.Text = "";
            label10.Text = "";
            label12.Text = "";
            reboot(comboBox1, comboBox2);
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            arr_for_numbs_potr = polychenie_chisl_zn(comboBox1);
            if (comboBox1.Text != "" && comboBox2.Text != "")
            {
                raschet();
            }
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
                dlya_sokr(rez);
            }
            else if (comboBox1.Text.Contains("Земля") == true && comboBox2.Text.Contains("Земля") == true)
            {
                rez = srawnenie_predl_potr(1);
                dlya_sokr(rez);
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
            if (comboBox1.Text != "" && comboBox2.Text != "")
            {
                raschet();
            }
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
                reboot(comboBox1, comboBox2);
                reboot_data_sdelka();
            }
        }

        private void reboot(ComboBox cmb1, ComboBox cmb2) 
        {
            //cmb1.Items.Clear();
            //comboBox2.Items.Clear();
            string s = "";
            @base @base = new @base(izm_zapr("select potr.Id, potr.mincena, potr.maxcena, potr.agent, potr.klient, potr.city, potr.street, filter_h.minetag, filter_h.maxetag, filter_h.minrooms, filter_h.maxrooms, filter_h.mins, filter_h.maxs from potr, filter_h, sdelka where potr.dop_info = filter_h.Id and potr.obj = N'Дом' and sdelka.potr <> potr.Id"));
            s += @base.vuvod_zakazov("house");
            @base.smena_zaprosa(izm_zapr("select potr.Id, potr.mincena, potr.maxcena, potr.agent, potr.klient, potr.city, potr.street, filter_kw.minetag, filter_kw.maxetag, filter_kw.minrooms, filter_kw.maxrooms, filter_kw.mins, filter_kw.maxs from potr, filter_kw, sdelka where potr.dop_info = filter_kw.Id and potr.obj = N'Квартира' and sdelka.potr <> potr.Id"));
            s += @base.vuvod_zakazov("kw");
            @base.smena_zaprosa(izm_zapr("select potr.Id, potr.mincena, potr.maxcena, potr.agent, potr.klient, potr.city, potr.street, filter_l.mins, filter_l.maxs from potr, filter_l, sdelka where potr.dop_info = filter_l.Id and potr.obj = N'Земля' and sdelka.potr <> potr.Id"));
            s += @base.vuvod_zakazov("land");
            cmb1.Items.Clear();
            if (s == "")
                cmb1.Items.Add("Отсутствуют данные в БД");
            else
            {
                s = s.Remove(s.Length - 1);
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
                cmb1.Items.AddRange(arr);
            }
            s = "";
            arr = null;
            cmb2.Items.Clear();
            @base.smena_zaprosa(izm_zapr("select predlog.Id, obj.city, obj.street, obj.nm_h, house.etag, house.rooms, house.s, predlog.agent, predlog.klient from obj, house, predlog, sdelka where obj.dop_inf = house.Id and obj.Id = predlog.obj and obj.nm_kw is null and sdelka.predl <> predlog.Id"));
            s += @base.vuvod_obj("house", true);
            @base.smena_zaprosa(izm_zapr("select predlog.Id, obj.city, obj.street, obj.nm_h, obj.nm_kw, kw.etag, kw.rooms, kw.s, predlog.agent, predlog.klient from obj, kw, predlog, sdelka where obj.dop_inf = kw.Id and obj.Id = predlog.obj and obj.nm_kw is not null and sdelka.predl <> predlog.Id"));
            s += @base.vuvod_obj("kw", true);
            @base.smena_zaprosa(izm_zapr("select predlog.Id, obj.city, obj.street, land.s, predlog.agent, predlog.klient from obj, land, predlog, sdelka where obj.dop_inf = land.Id and obj.Id = predlog.obj and obj.nm_kw is null and obj.nm_h is null and sdelka.predl <> predlog.Id"));
            s += @base.vuvod_obj("land", true);
            if (s == "")
                cmb2.Items.Add("Отсутствуют данные в БД");
            else
            {
                string time_str;
                s = s.Remove(s.Length - 1);
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
                cmb2.Items.AddRange(arr);
            }
        }

        private string izm_zapr(string zapr) 
        {
            @base @base = new @base("select * from sdelka");
            bool pdf = @base.proverka_znachenei_v_bd();
            if (pdf == false)
            {
                zapr = zapr.Replace(", sdelka", "");
                zapr = zapr.Contains("and sdelka.predl <> predlog.Id") == true ? zapr.Replace("and sdelka.predl <> predlog.Id", "") : zapr.Replace("and sdelka.potr <> potr.Id", "");
            }
            return zapr;
        }

        private void raschet() 
        {
            if (comboBox2.Text.Contains(" Дом ") == true)
            {
                label2.Text = (30000 + Convert.ToDouble(arr_for_numbs_predl[2]) / 100).ToString();
                label8.Text = (Convert.ToDouble(arr_for_numbs_predl[2]) / 100 * 3).ToString();
                rs("select DealShare from agent where Id = " + arr_for_numbs_predl[6] + " or Id = " + arr_for_numbs_potr[arr_for_numbs_potr.Length - 2] + "");
            }
            else if (comboBox2.Text.Contains(" Квартира ") == true)
            {
                label2.Text = (36000 + Convert.ToDouble(arr_for_numbs_predl[3]) / 100).ToString();
                label8.Text = (Convert.ToDouble(arr_for_numbs_predl[3]) / 100 * 3).ToString();
                rs("select DealShare from agent where Id = " + arr_for_numbs_predl[7] + " or Id = " + arr_for_numbs_potr[arr_for_numbs_potr.Length - 2] + "");
            }
            else
            {
                label2.Text = (30000 + Convert.ToDouble(arr_for_numbs_predl[1]) / 100 * 2).ToString();
                label8.Text = (Convert.ToDouble(arr_for_numbs_predl[1]) / 100 * 3).ToString();
                rs("select DealShare from agent where Id = " + arr_for_numbs_predl[3] + " or Id = " + arr_for_numbs_potr[arr_for_numbs_potr.Length - 2] + "");
            }
        }

        private void rs(string zp) 
        {
            string for_rez;
            string[] arr_for_time;
            @base  @base = new @base(zp);
            for_rez = @base.dealshear();
            arr_for_time = for_rez.Split('@');
            label4.Text = (Convert.ToDouble(label2.Text) / 100 * Convert.ToDouble(arr_for_time[0])).ToString();//
            label10.Text = arr_for_time.Length == 1 ? (Convert.ToDouble(label8.Text) / 100 * Convert.ToDouble(arr_for_time[0])).ToString() : (Convert.ToDouble(label8.Text) / 100 * Convert.ToDouble(arr_for_time[1])).ToString();
            label6.Text = (Convert.ToDouble(label2.Text) - Convert.ToDouble(label4.Text)).ToString();
            label12.Text = (Convert.ToDouble(label8.Text) - Convert.ToDouble(label10.Text)).ToString();
        }

        private void sdelkaBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.sdelkaBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.baseDataSet);

        }

        private void reboot_data_sdelka() 
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "baseDataSet.sdelka". При необходимости она может быть перемещена или удалена.
            this.sdelkaTableAdapter.Fill(this.baseDataSet.sdelka);
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            reboot(comboBox3, comboBox4);
            if (idTextBox.Text == "")
            {
                comboBox3.Enabled = false;
                comboBox4.Enabled = false;
                sdelkaBindingNavigator.Enabled = false;
            }
            else
            {
                string s = "";
                bool b;
                @base @base = new @base("select predlog.Id, obj.city, obj.street, obj.nm_h, house.etag, house.rooms, house.s, predlog.agent, predlog.klient from obj, house, predlog where obj.dop_inf = house.Id and obj.Id = predlog.obj and obj.nm_kw is null and predlog.Id = " + predlTextBox.Text + "");
                b = @base.proverka_znachenei_v_bd();
                if (b == true)
                {
                    s = @base.vuvod_obj("house", true);
                }
                else
                {
                    @base.smena_zaprosa("select predlog.Id, obj.city, obj.street, obj.nm_h, obj.nm_kw, kw.etag, kw.rooms, kw.s, predlog.agent, predlog.klient from obj, kw, predlog where obj.dop_inf = kw.Id and obj.Id = predlog.obj and obj.nm_kw is not null and predlog.Id = " + predlTextBox.Text + "");
                    b = @base.proverka_znachenei_v_bd();
                    if (b == true)
                    {
                        s = @base.vuvod_obj("kw", true);
                    }
                    else
                    {
                        @base.smena_zaprosa("select predlog.Id, obj.city, obj.street, land.s, predlog.agent, predlog.klient from obj, land, predlog where obj.dop_inf = land.Id and obj.Id = predlog.obj and obj.nm_kw is null and obj.nm_h is null and predlog.Id = " + predlTextBox.Text + "");
                        s = @base.vuvod_obj("land", true);
                    }
                }
                s = s.Remove(s.Length - 1);
                comboBox4.Items.Add(s);
                comboBox4.SelectedItem = s;
            }
        }
    }
}
