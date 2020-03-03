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
    public partial class Form6 : Form
    {
        private string znachenia = "";
        private string[] arr = null;
        public Form6()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.SetData("DataDirectory", Application.StartupPath.Replace(@"\bin\Debug", ""));
        }

        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            update_data();
            get_data(comboBox2, comboBox3);
            close_obj();
        }

        private void get_data(ComboBox cmb1, ComboBox cmb2)
        {
            @base @base = new @base("select distinct agent.Id, man.fam, man.name, man.otch from man, agent, klient where man.dop_info = agent.Id and agent.Id <> klient.Id");
            znachenia = @base.vuvod();
            zapis_v_combobox(cmb1);
            @base.smena_zaprosa("select distinct klient.Id, man.fam, man.name, man.otch from man, klient, agent where man.dop_info = klient.Id and agent.Id <> klient.Id");
            znachenia = @base.vuvod();
            zapis_v_combobox(cmb2);
        }

        private void zapis_v_combobox(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            arr = znachenia.Split('&');
            comboBox.Items.AddRange(arr);
        }

        private void get_data_standart(ComboBox cmb1, TextBox cmb2)
        {
            open_txt();
            string per = "";
            foreach (string item in cmb1.Items)
            {
                if (item.Contains(cmb2.Text) == true)
                {
                    per = item;
                }
            }
            cmb1.SelectedItem = per;
            clos_txt();
        }

        private void clos_txt()
        {
            //agentTextBox.Visible = false;
            //klientTextBox.Visible = false;
            //idTextBox.Visible = false;
            //idTextBox1.Visible = false;
            //agentTextBox1.Visible = false;
            //klientTextBox1.Visible = false;
            //idTextBox2.Visible = false;
            //idTextBox3.Visible = false;
            //agentTextBox2.Visible = false;
            //klientTextBox2.Visible = false;
            //idTextBox4.Visible = false;
            //idTextBox5.Visible = false;
        }

        private void open_txt()
        {
            agentTextBox.Visible = true;
            klientTextBox.Visible = true;
            idTextBox.Visible = true;
            idTextBox1.Visible = true;
            agentTextBox1.Visible = true;
            klientTextBox1.Visible = true;
            idTextBox2.Visible = true;
            idTextBox3.Visible = true;
            agentTextBox2.Visible = true;
            klientTextBox2.Visible = true;
            idTextBox4.Visible = true;
            idTextBox5.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" && comboBox2.Text == "" && comboBox3.Text == "")
                MessageBox.Show("Заполниет не заполненные поля", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                if (comboBox1.Text == "Дом")
                {
                    filter_h filter_h = new filter_h(Convert.ToInt32(numericUpDown2.Value), Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(numericUpDown6.Value), Convert.ToInt32(numericUpDown5.Value),
                        Convert.ToInt32(numericUpDown4.Value), Convert.ToInt32(numericUpDown3.Value), Convert.ToInt32(numericUpDown8.Value), Convert.ToInt32(numericUpDown7.Value),
                        Convert.ToInt32(comboBox3.Text.Replace(comboBox3.Text.Substring(comboBox3.Text.IndexOf(" ")), "")),
                        Convert.ToInt32(comboBox2.Text.Replace(comboBox2.Text.Substring(comboBox2.Text.IndexOf(" ")), "")),
                        comboBox1.Text, maskedTextBox2.Text, maskedTextBox1.Text);
                    zapis_v_bd("insert into filter_h(minetag, maxetag, minrooms, maxrooms, mins, maxs) values(" + filter_h.get_minetag() + ", " + filter_h.get_maxetag() + ", " + filter_h.get_minrooms() + ", " + filter_h.get_maxrooms() + ", " + filter_h.get_mins() + ", " + filter_h.get_maxs() + ") " +
                        "declare @per int " +
                        "select @per = max(Id) from filter_h " +
                        "insert into potr(mincena, maxcena, klient, agent, city, street, obj, dop_info) values(" + filter_h.get_mincena() + ", " + filter_h.get_maxcena() + ", " + filter_h.get_klient() + ", " + filter_h.get_agent() + ", N'" + filter_h.get_city() + "', N'" + filter_h.get_street() + "', N'" + filter_h.get_obj() + "', @per)");
                }
                else if (comboBox1.Text == "Квартира")
                {
                    filter_kw filter_kw = new filter_kw(Convert.ToInt32(numericUpDown2.Value), Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(numericUpDown6.Value), Convert.ToInt32(numericUpDown5.Value),
                        Convert.ToInt32(numericUpDown4.Value), Convert.ToInt32(numericUpDown3.Value), Convert.ToInt32(numericUpDown8.Value), Convert.ToInt32(numericUpDown7.Value),
                        Convert.ToInt32(comboBox3.Text.Replace(comboBox3.Text.Substring(comboBox3.Text.IndexOf(" ")), "")),
                        Convert.ToInt32(comboBox2.Text.Replace(comboBox2.Text.Substring(comboBox2.Text.IndexOf(" ")), "")),
                        comboBox1.Text, maskedTextBox2.Text, maskedTextBox1.Text);
                    zapis_v_bd("insert into filter_kw(minetag, maxetag, minrooms, maxrooms, mins, maxs) values(" + filter_kw.get_minetag() + ", " + filter_kw.get_maxetag() + ", " + filter_kw.get_minrooms() + ", " + filter_kw.get_maxrooms() + ", " + filter_kw.get_mins() + ", " + filter_kw.get_maxs() + ") " +
                        "declare @per int " +
                        "select @per = max(Id) from filter_kw " +
                        "insert into potr(mincena, maxcena, klient, agent, city, street, obj, dop_info) values(" + filter_kw.get_mincena() + ", " + filter_kw.get_maxcena() + ", " + filter_kw.get_klient() + ", " + filter_kw.get_agent() + ", N'" + filter_kw.get_city() + "', N'" + filter_kw.get_street() + "', N'" + filter_kw.get_obj() + "', @per)");
                }
                else if (comboBox1.Text == "Земля")
                {
                    filter_l filter_l = new filter_l(Convert.ToInt32(numericUpDown4.Value), Convert.ToInt32(numericUpDown3.Value), 
                        Convert.ToInt32(numericUpDown8.Value), Convert.ToInt32(numericUpDown7.Value), 
                        Convert.ToInt32(comboBox3.Text.Replace(comboBox3.Text.Substring(comboBox3.Text.IndexOf(" ")), "")),
                        Convert.ToInt32(comboBox2.Text.Replace(comboBox2.Text.Substring(comboBox2.Text.IndexOf(" ")), "")),
                        comboBox1.Text, maskedTextBox2.Text, maskedTextBox1.Text);
                    zapis_v_bd("insert into filter_l(mins, maxs) values(" + filter_l.get_mins() + ", " + filter_l.get_maxs() + ") " +
                        "declare @per int " +
                        "select @per = max(Id) from filter_l " +
                        "insert into potr(mincena, maxcena, klient, agent, city, street, obj, dop_info) values(" + filter_l.get_mincena() + ", " + filter_l.get_maxcena() + ", " + filter_l.get_klient() + ", " + filter_l.get_agent() + ", N'" + filter_l.get_city() + "', N'" + filter_l.get_street() + "', N'" + filter_l.get_obj() + "', @per)");
                }
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Дом")
            {
                open_obj();
                groupBox3.Text = "Этажность";
                groupBox2.Visible = true;
            }
            else if (comboBox1.Text == "Квартира")
            {
                open_obj();
                groupBox3.Text = "Этаж";
                groupBox2.Visible = true;
            }
            else if (comboBox1.Text == "Земля")
                close_obj();
                groupBox2.Visible = true;
        }

        private void close_obj() 
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
        }

        private void open_obj()
        {
            groupBox1.Visible = true;
            groupBox3.Visible = true;
        }

        private void zapis_v_bd(string s) 
        {
            @base @base = new @base(s);
            @base.zapis_v_bd();
            MessageBox.Show("Данные записанны");
        }

        private void filter_hBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            open_txt();
            agentTextBox.Text = comboBox4.Text.Remove(comboBox4.Text.IndexOf(" "));
            klientTextBox.Text = comboBox5.Text.Remove(comboBox5.Text.IndexOf(" "));
            clos_txt();
            this.Validate();
            this.filter_hBindingSource.EndEdit();
            this.potrBindingSource.EndEdit();
            this.filter_hTableAdapter.Update(this.baseDataSet);
            this.potrTableAdapter.Update(this.baseDataSet);

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            open_txt();
            agentTextBox1.Text = comboBox6.Text.Remove(comboBox6.Text.IndexOf(" "));
            klientTextBox1.Text = comboBox7.Text.Remove(comboBox7.Text.IndexOf(" "));
            clos_txt();
            this.Validate();
            this.filter_kwBindingSource.EndEdit();
            this.potrBindingSource1.EndEdit();
            this.filter_kwTableAdapter.Update(this.baseDataSet);
            this.potrTableAdapter.Update(this.baseDataSet);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            open_txt();
            agentTextBox2.Text = comboBox8.Text.Remove(comboBox8.Text.IndexOf(" "));
            klientTextBox2.Text = comboBox9.Text.Remove(comboBox9.Text.IndexOf(" "));
            clos_txt();
            this.Validate();
            this.filter_lBindingSource.EndEdit();
            this.potrBindingSource2.EndEdit();
            this.filter_lTableAdapter.Update(this.baseDataSet);
            this.potrTableAdapter.Update(this.baseDataSet);
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            get_data(comboBox4, comboBox5);
            get_data(comboBox6, comboBox7);
            get_data(comboBox8, comboBox9);
            dlya_sokrash();
        }

        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            dlya_sokrash();
        }

        private void dlya_sokrash()
        {
            get_data_standart(comboBox4, agentTextBox);
            get_data_standart(comboBox5, klientTextBox);
            get_data_standart(comboBox6, agentTextBox1);
            get_data_standart(comboBox7, klientTextBox1);
            get_data_standart(comboBox8, agentTextBox2);
            get_data_standart(comboBox9, klientTextBox2);
        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            dlya_sokrash();
        }

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            dlya_sokrash();
        }

        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {
            dlya_sokrash();
        }

        private void bindingNavigatorMoveFirstItem1_Click(object sender, EventArgs e)
        {
            dlya_sokrash();
        }

        private void bindingNavigatorMovePreviousItem1_Click(object sender, EventArgs e)
        {
            dlya_sokrash();
        }

        private void bindingNavigatorMoveNextItem1_Click(object sender, EventArgs e)
        {
            dlya_sokrash();
        }

        private void bindingNavigatorMoveLastItem1_Click(object sender, EventArgs e)
        {
            dlya_sokrash();
        }

        private void bindingNavigatorMoveFirstItem2_Click(object sender, EventArgs e)
        {
            dlya_sokrash();
        }

        private void bindingNavigatorMovePreviousItem2_Click(object sender, EventArgs e)
        {
            dlya_sokrash();
        }

        private void bindingNavigatorMoveNextItem2_Click(object sender, EventArgs e)
        {
            dlya_sokrash();
        }

        private void bindingNavigatorMoveLastItem2_Click(object sender, EventArgs e)
        {
            dlya_sokrash();
        }

        private void vvod_bukw(KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void cityTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_bukw(e);
        }

        private void streetTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_bukw(e);
        }

        private void cityTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_bukw(e);
        }

        private void streetTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_bukw(e);
        }

        private void cityTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_bukw(e);
        }

        private void streetTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_bukw(e);
        }

        private void vvod_cifr(KeyPressEventArgs e) 
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void minsTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_cifr(e);
        }

        private void maxsTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_cifr(e);
        }

        private void mincenaTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_cifr(e);
        }

        private void maxcenaTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_cifr(e);
        }

        private void maxcenaTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_cifr(e);
        }

        private void mincenaTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_cifr(e);
        }

        private void maxsTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_cifr(e);
        }

        private void minsTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_cifr(e);
        }

        private void maxroomsTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_cifr(e);
        }

        private void minroomsTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_cifr(e);
        }

        private void maxetagTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_cifr(e);
        }

        private void minetagTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_cifr(e);
        }

        private void maxcenaTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_cifr(e);
        }

        private void mincenaTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_cifr(e);
        }

        private void maxsTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_cifr(e);
        }

        private void minsTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_cifr(e);
        }

        private void minetagTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_cifr(e);
        }

        private void maxetagTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_cifr(e);
        }

        private void minroomsTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_cifr(e);
        }

        private void maxroomsTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_cifr(e);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            open_txt();
            delete(idTextBox, idTextBox1, "filter_h");
            clos_txt();
        }

        private void delete(TextBox txt1, TextBox txt2, string type) 
        {
            @base @base = new @base("select * from sdelka, potr where sdelka.potr = " + txt2.Text + "");
            Boolean rez = @base.proverka_znachenei_v_bd();
            if (rez == false)
            {
                @base.smena_zaprosa("delete from " + type + " where Id = " + txt1.Text + " delete from potr where Id = " + txt2.Text + "");
                @base.zapis_v_bd();
                MessageBox.Show("Удаление объекта завершено");
                update_data();
            }
            else
                MessageBox.Show("Данный объект учавствует в зделке", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void update_data() 
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "baseDataSet.filter_l". При необходимости она может быть перемещена или удалена.
            this.filter_lTableAdapter.Fill(this.baseDataSet.filter_l);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "baseDataSet.filter_kw". При необходимости она может быть перемещена или удалена.
            this.filter_kwTableAdapter.Fill(this.baseDataSet.filter_kw);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "baseDataSet.potr". При необходимости она может быть перемещена или удалена.
            this.potrTableAdapter.Fill(this.baseDataSet.potr);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "baseDataSet.filter_h". При необходимости она может быть перемещена или удалена.
            this.filter_hTableAdapter.Fill(this.baseDataSet.filter_h);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            open_txt();
            delete(idTextBox2, idTextBox3, "filter_kw");
            clos_txt();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            open_txt();
            delete(idTextBox4, idTextBox5, "filter_l");
            clos_txt();
        }
    }
}
