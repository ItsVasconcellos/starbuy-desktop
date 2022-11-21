﻿using System;
using System.Net; 
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace Starbuy_Desktop
{
    
public partial class FormConfig : Form {

        private Usuario user;
        private MultiplosEnderecosResponse address;
        private Image ImgEnviar;
        private String path;
        static int height, width;
        public FormConfig() {
            this.user = Session.getSession().getUser();
            this.address = MultiplosEnderecosResponse.getEnderecosResponse();
            InitializeComponent();
        }

        private void FormConfig_Load(object sender, EventArgs e) {

            height = this.Height;
            width = this.Width;
            ///Deixando os label transparentes!
            labelConfigNome.Parent = pictureBoxBackground;
            labelConfigNome.BackColor = Color.Transparent;
            labelConfigID.Parent = pictureBoxBackground;
            labelConfigID.BackColor = Color.Transparent;
            labelConfigConfig.Parent = pictureBoxBackground;
            labelConfigConfig.BackColor = Color.Transparent;
            labelConfigCantoNome.Text = user.name;
            labelConfigNome.Text = user.name;
            labelConfigUsername.Text = user.username;
            labelConfigCidade.Text = user.city;
            lblEmail.Text = lblEmail.Text + " " + user.email;
            labelData.Text = labelData.Text + " " + user.registration;
            ReSize.buttonResize(btnAdicionarEndereco);
            ReSize.groupBoxResize(groupBoxConfig);
            ReSize.groupBoxResize(groupBoxConfigAlterar);
            ReSize.groupBoxResize(gboxConfigMenu);
            ReSize.groupBoxResize(gboxConfigPerfil);

            ReSize.labelResize(labelConfigAdicionarEndereco);
            ReSize.labelResize(labelConfigAlterarNúmero);
            ReSize.labelResize(labelConfigAlterarCidade);
            ReSize.labelResize(labelConfigCep);
            ReSize.labelResize(labelConfigAlterarTeste);
            ReSize.labelResize(labelConfigCantoNome);
            ReSize.labelResize(lblCep);
            ReSize.labelResize(labelConfigCidade);
            ReSize.labelResize(labelConfigID);
            ReSize.labelResize(labelConfigConfig);
            ReSize.labelResize(lblEmail);
            ReSize.labelResize(lblNum);
            ReSize.labelResize(labelConfigInfCidade);
            ReSize.labelResize(lblComplemento);
            ReSize.labelResize(labelConfigInfUsername);
            ReSize.labelResize(labelConfigNome);
            ReSize.labelResize(labelConfigUsername);
            ReSize.labelResize(lblRespCep);
            ReSize.labelResize(lblRespComplemento);
            ReSize.labelResize(lblResultEmail);
            ReSize.labelResize(lblRespNum);
            ReSize.labelResize(labelSelecionarEndereco);
            ReSize.labelResize(labelConfigAdicionarComplemento);
            ReSize.labelResize(labelAdicionarNome);
            ReSize.labelResize(labelData);
            ReSize.comboBoxResise(comboBoxEndereco);

            ReSize.pictureCrossBox(pictureBoxMenuVendedorCross);
            ReSize.pictureCrossBox(pictureBoxConfigMenu);
            ReSize.pictureCrossBox(pictureBoxConfigEstoque);
            ReSize.pictureCrossBox(pictureBoxConfigFoto) ;
            ReSize.pictureCrossBox(pictureBoxConfigPedidos);
            ReSize.pictureCrossBox(pictureBoxBackground);
            ReSize.pictureCrossBox(pictureBoxConfigCanto);
            ReSize.pictureCrossBox(pictureBoxConfigConfig);

            ReSize.textBoxResize(textBoxAdicionarNome);
            ReSize.textBoxResize(textBoxConfigAlterarCEP);
            ReSize.textBoxResize(textBoxConfigAlterarCidade);
            ReSize.textBoxResize(textBoxConfigAdicionarComplemento);
            ReSize.textBoxResize(textBoxConfigAdicionarRua);
            ReSize.maskedResize(maskedTextBoxAdicionarCEP);

            labelConfigConfig.Font = new Font(labelConfigConfig.Font, FontStyle.Bold);
            labelConfigNome.Font = new Font(labelConfigNome.Font, FontStyle.Bold);


            //Puxando imagem do servidor
            if (!string.IsNullOrEmpty(user.profile_picture))
            {
                WebClient wc = new WebClient();
                byte[] bytes = wc.DownloadData(user.profile_picture);
                MemoryStream ms = new MemoryStream(bytes);
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                System.Drawing.Image resizeSmall = (new Bitmap(img, 56, 50));
                pictureBoxConfigCanto.Image = resizeSmall;
                pictureBoxConfigCanto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
                System.Drawing.Image resizeProfile = (new Bitmap(img, 179, 179));
                pictureBoxConfigFoto.Image = resizeProfile;
                pictureBoxConfigFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            }
            if (MultiplosEnderecosResponse.getEnderecosResponse() == null)
            {
                MessageBox.Show("A");
            }
            else
            {
                lblRespCep.Text = address.getAddress(0).cep;
                lblRespComplemento.Text = address.getAddress(0).complement;
                lblRespNum.Text = address.getAddress(0).number.ToString();
                comboBoxEndereco.Items.Clear(); 
                foreach(Address ad in address.getAddresses())
                {
                    comboBoxEndereco.Items.Add(ad.cep);
                }
            }

            //pictureBoxConfigFoto.Image = ResizeImage(pictureBoxConfigFoto.Image);
            pictureBoxConfigCanto.Image = ResizeImage(pictureBoxConfigCanto.Image);
            pictureBoxConfigConfig.Image = ResizeImage(pictureBoxConfigConfig.Image);
            pictureBoxConfigEstoque.Image = ResizeImage(pictureBoxConfigEstoque.Image);
            pictureBoxConfigFoto.Image = ResizeImage(pictureBoxConfigFoto.Image);
            pictureBoxConfigMenu.Image = ResizeImage(pictureBoxConfigMenu.Image);
            pictureBoxConfigPedidos.Image = ResizeImage(pictureBoxConfigPedidos.Image);
            pictureBoxMenuVendedorCross.Image = ResizeImage(pictureBoxMenuVendedorCross.Image);

            comboBoxEndereco.SelectedIndex = 0;
        }

        private void pictureBoxMenuVendedorCross_Click(object sender, EventArgs e) {
            DialogResult diag = MessageBox.Show("Deseja fechar o aplicativo e retornar a tela de login?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (diag == DialogResult.Yes) {
                this.Close();
                FormLogin fm = new FormLogin();
                fm.Show();
            }
        }

        private void pictureBoxConfigMenu_Click(object sender, EventArgs e)
        {
            this.Close();
            FormMenu menu = new FormMenu();
            menu.Show();
        }

        private void pictureBoxConfigPedidos_Click(object sender, EventArgs e)
        {
            this.Close();
            FormPedidos pedidos = new FormPedidos();
            pedidos.Show();
        }

        private void pictureBoxConfigEstoque_Click(object sender, EventArgs e) 
        {
            this.Close();
            FormEstoque estoque = new FormEstoque();
            estoque.Show();
        }

        private void comboBoxEndereco_SelectedIndexChanged(object sender, EventArgs e)
        {
            int buscar = comboBoxEndereco.SelectedIndex;
            lblRespCep.Text = address.getAddress(buscar).cep;
            lblRespComplemento.Text = address.getAddress(buscar).complement;
            lblRespNum.Text = address.getAddress(buscar).number.ToString();
        }


        private void maskedTextBoxAdicionarCEP_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            
        }
        private void LocalizarCEP()
        {

        }

        public static Bitmap ResizeImage(Image image) { 
            var newHeight = image.Height * height / 786;
            var newWidth = image.Width * width / 1386;
            /*double locationX = inage.Location.X * propWidth;// + widthOriginal - p.Width;
            double locationY = p.Location.Y * propHeight;// + heightOriginal - p.Height;*/
            var destRect = new Rectangle(0, 0, newWidth, newHeight);
            var destImage = new Bitmap(newWidth, newHeight);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
    }

