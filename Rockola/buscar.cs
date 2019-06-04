using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rockola
{
    public partial class buscar : Form
    {
        public buscar()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string texto = textBox1.Text;
            int largodetexto = texto.Length;
            string artistasencontrados = "";
            string cadenaartista = "";
           // for (int j = 0; j < contador; j++)
            {
                //artistasencontrados = listaArtista[j];
                if (artistasencontrados.Length >= largodetexto)
                {
                    cadenaartista = "";
                    for (int i = 0; i < largodetexto; i++)
                    {
                        cadenaartista = cadenaartista + artistasencontrados[i];
                    }
                    if (cadenaartista == texto)
                    {
                        //comboBox1.Items.Add(artistasencontrados);
                    }
                }
            }
        }
    }
}
