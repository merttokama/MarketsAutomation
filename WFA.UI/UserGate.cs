using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BLL.Repository;

namespace WFA.UI
{
    public partial class UserGate : DevExpress.XtraEditors.XtraForm
    {
        public UserGate()
        {
            InitializeComponent();
        }
        public static string kullaniciAdi { get; set; }
        public static string sifre { get; set; }


        private void textEdit1_Properties_Enter(object sender, EventArgs e)
        {
            if (txtUserName.Text == "UserName")
                txtUserName.Text = "";
        }

        private void textEdit1_Properties_Leave(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                txtUserName.Text = "UserName";
                txtUserName.ForeColor = Color.Silver;
            }
            else
            {
                txtUserName.ForeColor = Color.Black;
            }
        }

        private void textEdit2_Properties_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Password")
                txtPassword.Text = "";
        }

        private void textEdit2_Properties_Leave(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                txtPassword.Text = "Password";
                txtPassword.ForeColor = Color.Silver;
            }
            else
            {
                txtPassword.ForeColor = Color.Black;
            }
        }

        private void BtnSignIn_Click(object sender, EventArgs e)
        {
            if (checkBoxKeepMe.Checked == true)
            {
                kullaniciAdi = txtUserName.Text;
                sifre = txtPassword.Text;
            }
            else
            {
                kullaniciAdi = "";
                sifre = "";
            }
            if (new UsersRepo().GetAll().
                Any(x => x.UsersName == txtUserName.Text && x.Password == txtPassword.Text))
            {
                MainForm frm = new MainForm();
                this.Hide();
                frm.Show();
            }
            else
            {
                MessageBox.Show("Yanlış Kullanıcı Adı veya Şifre Girişi Yaptınız!");
            }
        }
    }
}