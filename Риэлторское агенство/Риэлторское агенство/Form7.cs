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
    public partial class Form7 : Form
    {
        private string znachenia = "";
        private string[] arr = null;
        public Form7()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.SetData("DataDirectory", Application.StartupPath.Replace(@"\bin\Debug", ""));
        }

        private void Form7_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" || comboBox2.Text == "" || comboBox3.Text == "" || textBox1.Text == "")
                MessageBox.Show("Заполните не заполненные поля", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                try
                {
                    predlog predlog = new predlog(Convert.ToInt32(textBox1.Text), Convert.ToInt32(comboBox2.Text.Replace(comboBox2.Text.Substring(comboBox2.Text.IndexOf(" ")), "")),
                        Convert.ToInt32(comboBox1.Text.Replace(comboBox1.Text.Substring(comboBox1.Text.IndexOf(" ")), "")), Convert.ToInt32(comboBox3.Text.Replace(comboBox3.Text.Substring(comboBox3.Text.IndexOf(" ")), "")));
                    @base @base = new @base("insert into predlog (cena, agent, klient, obj) values (" + predlog.get_cena() + ", " + predlog.get_agent() + ", " + predlog.get_klient() + ", " + predlog.get_obj() + ")");
                    @base.zapis_v_bd();
                    MessageBox.Show("Данные записанны");
                }
                catch
                {
                    MessageBox.Show("Добавьте объект в Базу данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void predlogBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            open_txt();
            agentTextBox.Text = comboBox4.Text.Replace(comboBox4.Text.Substring(comboBox4.Text.IndexOf(" ")), "");
            klientTextBox.Text = comboBox5.Text.Replace(comboBox5.Text.Substring(comboBox5.Text.IndexOf(" ")), "");
            objTextBox.Text = comboBox6.Text.Replace(comboBox6.Text.Substring(comboBox6.Text.IndexOf(" ")), "");
            this.Validate();
            this.predlogBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.baseDataSet);
            clos_txt();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            get_data(comboBox1, comboBox2, comboBox3);
            get_data(comboBox4, comboBox5, comboBox6);
            clos_txt();
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "baseDataSet.predlog". При необходимости она может быть перемещена или удалена.
            this.predlogTableAdapter.Fill(this.baseDataSet.predlog);
            dlya_sokrash();
        }

        private void zapis_v_combobox(ComboBox comboBox) 
        {
            arr = znachenia.Split('&');
            comboBox.Items.AddRange(arr);
        }

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            dlya_sokrash();
        }

        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {
            dlya_sokrash();
        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            dlya_sokrash();
        }

        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            dlya_sokrash();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }

        private void get_data(ComboBox cmb1, ComboBox cmb2, ComboBox cmb3) 
        {
            @base @base = new @base("select distinct agent.Id, man.fam, man.name, man.otch from man, agent, klient where man.dop_info = agent.Id and agent.Id <> klient.Id");
            znachenia = @base.vuvod();
            zapis_v_combobox(cmb1);
            @base.smena_zaprosa("select distinct klient.Id, man.fam, man.name, man.otch from man, klient, agent where man.dop_info = klient.Id and agent.Id <> klient.Id");
            znachenia = @base.vuvod();
            zapis_v_combobox(cmb2);
            @base.smena_zaprosa("select obj.Id, obj.city, obj.street, obj.nm_h, obj.nm_kw, kw.etag, kw.rooms, kw.s from obj, kw where obj.dop_inf = kw.Id");
            znachenia = @base.vuvod_obj("kw");
            @base.smena_zaprosa("select obj.Id, obj.city, obj.street, obj.nm_h, house.etag, house.rooms, house.s from obj, house where obj.dop_inf = house.Id");
            znachenia += @base.vuvod_obj("house");
            @base.smena_zaprosa("select obj.Id, obj.city, obj.street, land.s from obj, land where obj.dop_inf = land.Id");
            znachenia += @base.vuvod_obj("land");
            if (znachenia == "")
            {
                znachenia = "Отсутствую данные в БД";
            }
            zapis_v_combobox(cmb3);
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            open_txt();
            @base @base = new @base("select * from predlog, sdelka where predlog.Id = sdelka.predl and predlog.Id = " + idTextBox.Text + "");
            clos_txt();
            Boolean rez = @base.proverka_znachenei_v_bd();
            if (rez == true)
                MessageBox.Show("Данное предложение учавствует в зделке", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                open_txt();
                @base.smena_zaprosa("delete from predlog where Id = " + idTextBox.Text + "");
                clos_txt();
                @base.zapis_v_bd();
                MessageBox.Show("Удаление завершено");
                this.predlogTableAdapter.Fill(this.baseDataSet.predlog);
            }
        }

        private void dlya_sokrash() 
        {
            get_data_standart(comboBox4, agentTextBox);
            get_data_standart(comboBox5, klientTextBox);
            get_data_standart(comboBox6, objTextBox);
        }

        private void clos_txt() 
        {
            agentTextBox.Visible = false;
            klientTextBox.Visible = false;
            objTextBox.Visible = false;
            idTextBox.Visible = false;
        }

        private void open_txt()
        {
            agentTextBox.Visible = true;
            klientTextBox.Visible = true;
            objTextBox.Visible = true;
            idTextBox.Visible = true;
        }

        private void cenaTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }
    }
}
