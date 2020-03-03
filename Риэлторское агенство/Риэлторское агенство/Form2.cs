using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

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
            //try добавить проверку заполненности полей
            //{

            //}
            //catch 
            //{
            //    MessageBox.Show("Заполните путсые поля","Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            //}
        }

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

        private void close_txb() 
        {
            idTextBox.Visible = false;
            idTextBox1.Visible = false;
        }

        private void open_txb()
        {
            idTextBox.Visible = true;
            idTextBox1.Visible = true;
        }

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
