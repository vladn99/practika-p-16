using System;
using System.Windows.Forms;

namespace Риэлторское_агенство
{
    public partial class Form2 : Form
    {
        private string id_ag;
        private string id_mn;

        public Form2()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.SetData("DataDirectory", Application.StartupPath.Replace(@"\bin\Debug", ""));
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }
        //запись данных в БД
        private void button1_Click(object sender, EventArgs e)
        {
            agent agent = new agent(maskedTextBox1.Text, maskedTextBox2.Text, maskedTextBox3.Text, (int)numericUpDown1.Value);
            @base @base = new @base
                ("insert into agent (DealShare) values (" + agent.get_dealshare() + ") " +
                "declare @id int " +
                "select @id = max(id) from agent " +
                "insert into man (fam, name, otch, dop_info) values (N'" + agent.get_fam() + "', N'" + agent.get_name() + "', N'" + agent.get_oth() + "', @id)");
            @base.zapis_v_bd();
            MessageBox.Show("Данные записанны");
        }
        //сохранение изменений
        private void agentBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.agentBindingSource.EndEdit();
            this.manBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.baseDataSet);

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            reboot();
            close_txb();
        }
        //обновление данных в manTableAdapter и agentTableAdapter
        private void reboot()
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "baseDataSet.man". При необходимости она может быть перемещена или удалена.
            this.manTableAdapter.Fill(this.baseDataSet.man);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "baseDataSet.agent". При необходимости она может быть перемещена или удалена.
            this.agentTableAdapter.Fill(this.baseDataSet.agent);
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            reboot();
        }
        //удаление данных из БД
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            open_txb();
            id_ag = idTextBox.Text;
            id_mn = idTextBox1.Text;
            close_txb();
            @base @base = new @base("select agent.Id from agent, predlog, potr where predlog.agent = " + id_ag + " or potr.agent = " + id_ag + "");
            Boolean per = @base.proverka_znachenei_v_bd();
            if (per == false)
            {
                @base.smena_zaprosa("delete from agent where Id = " + id_ag + " delete from man where Id = " + id_mn + " and dop_info = " + id_ag + "");
                @base.zapis_v_bd();
                reboot();
                MessageBox.Show("Удаление агента завершено", "Сообщение");
            }
            else
                MessageBox.Show("Данный риэлтор принимает участие в сделке и не может быть удален.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        //ввод только буквенных значений
        private void vvod_bukw(KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
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
        //ввод только цифр
        private void dealShareTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
    }
}
