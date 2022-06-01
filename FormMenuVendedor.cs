﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Starbuy_Desktop
{
    public partial class FormMenu : Form
    {
        private LoginResponse lgresponse;
        public FormMenu(LoginResponse lgResp)
        {
            lgresponse = lgResp;
            InitializeComponent();
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            labelConfigCantoNome.Text = lgresponse.user.name;
        }

        private void pictureBoxMenuVendedorCross_Click(object sender, EventArgs e)
        {
            DialogResult diag = MessageBox.Show("Deseja fechar o aplicativo e retornar a tela de login?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (diag == DialogResult.Yes)
            {
                this.Hide();
                FormLogin fm = new FormLogin();
                fm.Show();
            }
        }
    }
}