using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Starbuy_Desktop
{
    public partial class FormMenu : Form {

        private Usuario user;
        private Categorias categorias = new Categorias();
        private OrdersResponse orders;
        private ItemsResponse items;
        public FormMenu() {
            API.getPedidos(Session.getSession().getJWT());
            API.getProducts(Session.getSession().getUser().username);
            orders = OrdersResponse.GetOrdersResponse();
            this.user = Session.getSession().getUser();
            items = ItemsResponse.GetItemsResponse();
            InitializeComponent();
            ReSize.labelResize(labelConfigCantoNome);
            ReSize.labelResize(labelEstoque);
            ReSize.labelResize(labelMenuPedidos);
            ReSize.pictureCrossBox(pictureBoxConfigCanto);
            ReSize.pictureCrossBox(pictureBoxMenuConfig);
            ReSize.pictureCrossBox(pictureBoxMenuEstoque);
            ReSize.pictureCrossBox(pictureBoxMenuMenu);
            ReSize.pictureCrossBox(pictureBoxMenuPedidos);
            ReSize.pictureCrossBox(pictureBoxMenuVendedorCross);
            ReSize.groupBoxResize(gboxConfigPerfil);
            ReSize.groupBoxResize(gboxMenu);
            ReSize.panelResize(panelPedidos);
            ReSize.panelResize(panelEstoque);
            int i = orders.getOrders().Length - 1;
            GetGroupBoxOrder(orders.getOrder(i),0);

            int x = items.getAllProdutos().Length - 1;
            GetGroupBoxProdutos(items.GetProdutos(x), 0);
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            labelConfigCantoNome.Text = Session.getSession().getUser().name;
            if (!string.IsNullOrEmpty(user.profile_picture))
            {
                WebClient wc = new WebClient();
                byte[] bytes = wc.DownloadData(user.profile_picture);
                MemoryStream ms = new MemoryStream(bytes);
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                System.Drawing.Image resizeSmall = (new Bitmap(img, 57, 51));
                pictureBoxConfigCanto.Image = resizeSmall;
            }

        }

        private void pictureBoxMenuVendedorCross_Click(object sender, EventArgs e)
        {
            DialogResult diag = MessageBox.Show("Deseja fechar o aplicativo e retornar a tela de login?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (diag == DialogResult.Yes)
            {
                this.Close();
                FormLogin formLogin = new FormLogin();
                formLogin.Show();
            }
        }

        private void pictureBoxMenuEstoque_Click(object sender, EventArgs e)
        {
            this.Close();

            FormEstoque estoque = new FormEstoque();
            estoque.Show();
        }

        private void pictureBoxMenuPedidos_Click(object sender, EventArgs e)
        {
            this.Close();
            FormPedidos pedidos = new FormPedidos();
            pedidos.Show();
        }

        private void pictureBoxMenuConfig_Click(object sender, EventArgs e)
        {
            this.Close();
            FormConfig config = new FormConfig();
            config.Show();
        }

        private void GetGroupBoxOrder(Order o, int i)
        {
            //referênciar cada caracteristica do produto a uma variavel
            string titleprod = o.item_with_assets.item.title.ToString();
            string priceprod = o.price.ToString();
            string stockprod = o.quantity.ToString();
            int cat = o.item_with_assets.item.category;
            string categoryprod = categorias.getCategoria(cat);
            string descriptionprod = o.item_with_assets.item.description;
            int status = o.status;
            string identifierProd = o.identifier;
            String statusProd = "";
            switch (status)
            {
                case 0:
                    statusProd = "Preparando";
                    break;
                case 1:
                    statusProd = "Enviado";
                    break;
                case 2:
                    statusProd = "Entregue";
                    break;

            }

            GroupBox currentGroupBox = new GroupBox();
            currentGroupBox.Size = new Size(900, 200);
            currentGroupBox.Location = new Point(3,3);
            if (o.item_with_assets.assets[0] == null)
            {
                //não tem imagem
            }
            else
            {
                try
                {
                    WebClient wc = new WebClient();
                    byte[] bytes = wc.DownloadData(o.item_with_assets.assets[0]);
                    MemoryStream ms = new MemoryStream(bytes);
                    System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                    System.Drawing.Image resizeSmall = (new Bitmap(img, 106, 106));
                    PictureBox imagemProduto = new PictureBox();
                    imagemProduto.Location = new Point(17, 22);
                    imagemProduto.Image = resizeSmall;
                    imagemProduto.Width = 98;
                    imagemProduto.Height = 98;
                    currentGroupBox.Controls.Add(imagemProduto);
                    ReSize.pictureCrossBox(imagemProduto);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }

            Label identifier = new Label();
            identifier.Text = "Identifier: " + identifierProd;
            identifier.Location = new Point(127, 34);
            identifier.Size = new Size(580, 15);
            currentGroupBox.Controls.Add(identifier);

            Label titulo = new Label();
            titulo.Text = titleprod;
            titulo.Location = new Point(127, 16);
            titulo.Size = new Size(580, 15);
            currentGroupBox.Controls.Add(titulo);


            Label preco = new Label();
            preco.Text = "Preço: " + priceprod;
            preco.Location = new Point(127, 55);
            currentGroupBox.Controls.Add(preco);

            Label estoque = new Label();
            estoque.Text = "Quantidade pedida: " + stockprod;
            estoque.Size = new Size(190, 15);
            estoque.Location = new Point(276, 55);
            currentGroupBox.Controls.Add(estoque);

            Label categoria = new Label();
            categoria.Text = "Categoria: " + categoryprod;
            categoria.Size = new Size(190, 15);
            categoria.Location = new Point(487, 55);
            currentGroupBox.Controls.Add(categoria);

            Label descricao = new Label();
            descricao.Text = descriptionprod;
            descricao.Location = new Point(127, 80);
            descricao.Size = new Size(580, 45);
            currentGroupBox.Controls.Add(descricao);

            Label statusEntrega = new Label();
            statusEntrega.Text = "Status da entrega: " + statusProd;
            statusEntrega.Location = new Point(127, 130);
            statusEntrega.Size = new Size(260, 15);
            currentGroupBox.Controls.Add(statusEntrega);

            Button btnStatus = new Button();
            btnStatus.Text = "Alterar Status";
            btnStatus.Location = new Point(487, 130);
            currentGroupBox.Controls.Add(statusEntrega);

            currentGroupBox.Visible = true;
            ReSize.labelResize(identifier);
            ReSize.labelResize(statusEntrega);
            ReSize.labelResize(titulo);
            ReSize.labelResize(preco);
            ReSize.labelResize(descricao);
            ReSize.labelResize(estoque);
            ReSize.labelResize(categoria);
            ReSize.groupBoxResize(currentGroupBox);
            titulo.Font = new Font(titulo.Font, FontStyle.Bold);
            panelPedidos.Controls.Add(currentGroupBox);
        }
        private void GetGroupBoxProdutos(Produtos p, int i)
        {
            //referênciar cada caracteristica do produto a uma variavel
            string id = p.item.identifier.ToString();
            string titleprod = p.item.title;
            string priceprod = p.item.price.ToString();
            int categoryprod = p.item.category;
            MessageBox.Show(p.item.description);

            string descriptionprod = p.item.description;

            GroupBox currentGroupBox = new GroupBox();
            currentGroupBox.Size = new Size(900, 220); 
            currentGroupBox.Location = new Point(3, 3);
            if (p == null)
            {

            }
            else if (p.assets[0] != null)
            {
                try
                {
                    WebClient wc = new WebClient();
                    byte[] bytes = wc.DownloadData(p.assets[0]);
                    MemoryStream ms = new MemoryStream(bytes);
                    System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                    System.Drawing.Image resizeSmall = (new Bitmap(img, 106, 106));
                    PictureBox imagemProduto = new PictureBox();
                    imagemProduto.Location = new Point(17, 22);
                    imagemProduto.Image = resizeSmall;
                    imagemProduto.Width = 98;
                    imagemProduto.Height = 98;
                    currentGroupBox.Controls.Add(imagemProduto);
                    ReSize.pictureCrossBox(imagemProduto);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            // arrumar as localizações dos itens
            // atribuindo o título do produto
            Label titulo = new Label();
            titulo.Text = titleprod;
            titulo.Location = new Point(127, 24); //localização do titulo
            currentGroupBox.Controls.Add(titulo);

            // atribuindo o preço do produto
            Label preco = new Label();
            preco.Text = "Preço: " + priceprod;
            preco.Location = new Point(127, 55); //localização do preço
            currentGroupBox.Controls.Add(preco);


            // atribuindo a quantidade em estoque do produto
            Label estoque = new Label();
            estoque.Text = "Quantidade em estoque: " + p.item.stock.ToString();
            estoque.Size = new Size(190, 15);
            estoque.Location = new Point(276, 55); //localização do estoque
            currentGroupBox.Controls.Add(estoque);

            // atribuindo a categoria do produto
            Label categoria = new Label();
            categoria.Text = "Categoria: " + categorias.getCategoria(categoryprod);
            categoria.Size = new Size(190, 15);
            categoria.Location = new Point(487, 55); //localização da categoria
            currentGroupBox.Controls.Add(categoria);

            // atribuindo a descrição do produto
            Label descricao = new Label();
            descricao.Text = descriptionprod;
            descricao.Location = new Point(127, 80);
            descricao.Size = new Size(580, 90);
            currentGroupBox.Controls.Add(descricao);

            currentGroupBox.Visible = true;
            ReSize.labelResize(titulo);
            ReSize.labelResize(preco);
            ReSize.labelResize(descricao);
            ReSize.labelResize(estoque);
            ReSize.labelResize(categoria);
            ReSize.groupBoxResize(currentGroupBox);
            panelEstoque.Controls.Add(currentGroupBox);
        }
    }
    
}
