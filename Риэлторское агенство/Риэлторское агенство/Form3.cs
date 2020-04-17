using System;
using System.Windows.Forms;

namespace Риэлторское_агенство
{
    public partial class Form3 : Form
    {
        private string id_kl;
        private string id_mn;

        public Form3()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.SetData("DataDirectory", Application.StartupPath.Replace(@"\bin\Debug", ""));
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }
        //запись данных в БД
        private void button1_Click(object sender, EventArgs e)
        {
            string phone = maskedTextBox4.Text, mail = maskedTextBox5.Text;
            if (phone == "+7-   -   -  -" && mail == "     @")
                MessageBox.Show("Необходимо заполнить одно из полей", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                if (phone == "+7-   -   -  -")
                    phone = "";
                if (mail == "     @")
                    mail = "";
                klient klient = new klient(maskedTextBox1.Text, maskedTextBox2.Text, maskedTextBox3.Text, phone, mail);
                @base @base = new @base(
                    "insert into klient (phone, mail) values ('" + klient.get_phone() + "', '" + klient.get_mail() + "') " +
                    "declare @id int " +
                    "select @id = MAX(Id) from klient " +
                    "insert into man (fam, name, otch, dop_info) values (N'" + klient.get_fam() + "', N'" + klient.get_name() +"', N'" + klient.get_oth() + "', @id)");
                @base.zapis_v_bd();
                MessageBox.Show("Данные записанны");
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            reboot();
            close_txb();
        }
        //сохранение изменений
        private void klientBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.klientBindingSource.EndEdit();
            this.manBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.baseDataSet);

        }
        //обновление данных в manTableAdapter и klientTableAdapter
        private void reboot() 
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "baseDataSet.man". При необходимости она может быть перемещена или удалена.
            this.manTableAdapter.Fill(this.baseDataSet.man);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "baseDataSet.klient". При необходимости она может быть перемещена или удалена.
            this.klientTableAdapter.Fill(this.baseDataSet.klient);
        }
        //удаление данных из БД
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            open_txb();
            id_kl = idTextBox.Text;
            id_mn = idTextBox1.Text;
            close_txb();
            @base @base = new @base("select klient.Id from klient, predlog, potr where predlog.klient = " + id_kl + " or potr.klient = " + id_kl + "");
            Boolean per = @base.proverka_znachenei_v_bd();
            if (per == false)
            {
                @base.smena_zaprosa("delete from klient where Id = " + id_kl + " delete from man where Id = " + id_mn + " and dop_info = " + id_kl + "");
                @base.zapis_v_bd();
                reboot();
                MessageBox.Show("Удаление клиента завершено", "Сообщение");
            }
            else
                MessageBox.Show("Данный клиент принимает участие в сделке и не может быть удален.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            reboot();
        }
        //скрытие полей
        private void close_txb()
        {
            idTextBox.Visible = false;
            idTextBox1.Visible = false;
        }
        //открытие полей
        private void open_txb()
        {
            idTextBox.Visible = true;
            idTextBox1.Visible = true;
        }

        private void famTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_bukw(e);
        }

        private void nameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_bukw(e);
        }

        private void otchTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            vvod_bukw(e);
        }
        //ввод только буквенных значений
        private void vvod_bukw(KeyPressEventArgs e) 
        {
            char number = e.KeyChar;
            if (Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
        //ввод только цифр
        private void phoneTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && (e.KeyChar <= 39 || e.KeyChar >= 46) && number != 47 && number != 61) 
            {
                e.Handled = true;
            }
        }
    }
}
