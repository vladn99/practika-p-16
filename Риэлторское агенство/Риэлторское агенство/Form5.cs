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
    public partial class Form5 : Form
    {
        private string id_ob;
        private string id_dopinf;
        private string type;
        public Form5()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.SetData("DataDirectory", Application.StartupPath.Replace(@"\bin\Debug", ""));
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            //// TODO: данная строка кода позволяет загрузить данные в таблицу "baseDataSet.land". При необходимости она может быть перемещена или удалена.
            //this.landTableAdapter.Fill(this.baseDataSet.land);
            reboot();
            close_txb();
            close();
        }

        private void close() 
        {
            groupBox1.Visible = false;
            groupBox3.Visible = false;
            groupBox4.Visible = false;
        }
        private void reboot() 
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "baseDataSet.obj". При необходимости она может быть перемещена или удалена.
            this.objTableAdapter.Fill(this.baseDataSet.obj);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "baseDataSet.house". При необходимости она может быть перемещена или удалена.
            this.houseTableAdapter.Fill(this.baseDataSet.house);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "baseDataSet.kw". При необходимости она может быть перемещена или удалена.
            this.kwTableAdapter.Fill(this.baseDataSet.kw);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "baseDataSet.kw". При необходимости она может быть перемещена или удалена.
            this.kwTableAdapter.Fill(this.baseDataSet.kw);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "baseDataSet.land". При необходимости она может быть перемещена или удалена.
            this.landTableAdapter.Fill(this.baseDataSet.land);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            @base @base;
            if (radioButton1.Checked == true)
            {
                house house = new house(
                    maskedTextBox1.Text, maskedTextBox2.Text, 
                    (int)numericUpDown3.Value, 0, (int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown4.Value, (int)numericUpDown5.Value, (int)numericUpDown6.Value);
                @base = new @base(
                    "insert into house(etag, rooms, s) values(" + house.get_etag() + ", " + house.get_rooms() + ", " + house.get_s() + ") " +
                    "declare @id int " +
                    "select @id = MAX(Id) from house " +
                    "insert into obj (city, street, nm_h, x, y, dop_inf) values(N'" + house.get_city() + "', N'" + house.get_street() + "', " + house.get_nm_h() + ", " + house.get_x() + ", " + house.get_y() + ", @id)");
                @base.zapis_v_bd();
                MessageBox.Show("Данные записанны");
            }
            else if (radioButton2.Checked == true)
            {
                kw kw = new kw(
                    maskedTextBox4.Text, maskedTextBox3.Text, (int)numericUpDown10.Value, 
                    (int)numericUpDown11.Value, (int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown9.Value, (int)numericUpDown8.Value, (int)numericUpDown7.Value);
                @base = new @base(
                    "insert into kw(etag, rooms, s) values (" + kw.get_etag() + ", " + kw.get_rooms() + ", " + kw.get_s() + ") " +
                    "declare @id int " +
                    "select @id=MAX(Id) from kw " +
                    "insert into obj (city, street, nm_kw, nm_h, x, y, dop_inf) values (N'" + kw.get_city() + "', N'" + kw.get_street() + "', " + kw.get_nm_kw() + ", " + kw.get_nm_h() + ", " + kw.get_x() + ", " + kw.get_y() + ", @id)");
                @base.zapis_v_bd();
                MessageBox.Show("Данные записанны");
            }
            else if (radioButton3.Checked == true)
            {
                land land = new land(maskedTextBox6.Text, maskedTextBox5.Text, 0, 0, (int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown13.Value);
                @base = new @base("insert into land(s) values (" + land.get_s() + ") declare @id int select @id=MAX(Id) from land insert into obj (city, street, x, y, dop_inf) values (N'" + land.get_city() + "', N'" + land.get_street() + "', " + land.get_x() + ", " + land.get_y() + ", @id)");
                @base.zapis_v_bd();
                MessageBox.Show("Данные записанны");
            }
            else
                MessageBox.Show("Выберите тип недвижимости", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            close();
            groupBox1.Visible = true;
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            close();
            groupBox3.Visible = true;
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            close();
            groupBox4.Visible = true;
        }

        private void houseBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.houseBindingSource.EndEdit();
            this.objBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.baseDataSet);

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            open_txb();
            id_dopinf = idTextBox.Text;
            id_ob = idTextBox1.Text;
            close_txb();
            type = "house";
            del(type, id_ob, id_dopinf);
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            reboot();
        }
        private void close_txb()
        {
            idTextBox.Visible = false;
            idTextBox1.Visible = false;
            idTextBox2.Visible = false;
            idTextBox3.Visible = false;
            idTextBox5.Visible = false;
            idTextBox4.Visible = false;
        }

        private void open_txb()
        {
            idTextBox.Visible = true;
            idTextBox1.Visible = true;
            idTextBox2.Visible = true;
            idTextBox3.Visible = true;
            idTextBox4.Visible = true;
            idTextBox5.Visible = true;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.kwBindingSource.EndEdit();
            this.objBindingSource1.EndEdit();
            this.kwTableAdapter.Update(this.baseDataSet);
            this.tableAdapterManager.UpdateAll(this.baseDataSet);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            open_txb();
            id_dopinf = idTextBox2.Text;
            id_ob = idTextBox3.Text;
            close_txb();
            type = "kw";
            del(type, id_ob, id_dopinf);
        }

        private void del(string type, string id_obj, string id_dopinf) 
        {
            Boolean rez1;
            @base @base = new @base("select obj.Id from obj, predlog where predlog.obj = " + id_ob + " and obj.Id = predlog.obj");
            rez1 = @base.proverka_znachenei_v_bd();
            if (rez1 == true)
                MessageBox.Show("Данный объект доступен для покупки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                @base.smena_zaprosa("delete from " + type + " where Id = " + id_dopinf + " delete from obj where Id = " + id_obj + " and dop_inf = " + id_dopinf + "");
                @base.zapis_v_bd();
                reboot();
                MessageBox.Show("Удаление объекта завершено", "Сообщение");
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.landBindingSource.EndEdit();
            this.objBindingSource2.EndEdit();
            this.landTableAdapter.Update(this.baseDataSet);
            this.tableAdapterManager.UpdateAll(this.baseDataSet);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            open_txb();
            id_dopinf = idTextBox4.Text;
            id_ob = idTextBox5.Text;
            close_txb();
            type = "land";
            del(type, id_ob, id_dopinf);
        }

        private void vvod_bukw(KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void vvod_numb(KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
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

        private void xTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_numb(e);
        }

        private void yTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_numb(e);
        }

        private void nm_hTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_numb(e);
        }

        private void etagTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_numb(e);
        }

        private void sTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_numb(e);
        }

        private void roomsTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_numb(e);
        }

        private void xTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_numb(e);
        }

        private void yTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_numb(e);
        }

        private void nm_hTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_numb(e);
        }

        private void nm_kwTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_numb(e);
        }

        private void etagTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_numb(e);
        }

        private void roomsTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_numb(e);
        }

        private void sTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_numb(e);
        }

        private void xTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_numb(e);
        }

        private void yTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_numb(e);
        }

        private void sTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_numb(e);
        }
    }
}
