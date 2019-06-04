using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Rockola
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            direccion = Path.GetFullPath("F:");
        }
        string direccion;
        private void pictureBox5_MouseHover(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            string combi = Path.Combine(direccion, "salir2.PNG");
            pictureBox5.Image = new System.Drawing.Bitmap(combi);
            this.Close();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            volver();
            string combi = Path.Combine(direccion, "actusinfon.PNG");
            pictureBox1.Image = new System.Drawing.Bitmap(combi);
            pictureBox1.Size = new Size(123, 95);
            
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            volver();
            string combi = Path.Combine(direccion, "a.PNG");
            pictureBox1.Image = new System.Drawing.Bitmap(combi);
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            volver();
            string combi = Path.Combine(direccion, "home2.PNG");
            pictureBox2.Image = new System.Drawing.Bitmap(combi);

            combi = Path.Combine(direccion, "busauq.PNG");
            pictureBox3.Image = new System.Drawing.Bitmap(combi);
            pictureBox2.Size = new Size(123, 95);
            
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        { 
        }

        private void pictureBox3_MouseClick(object sender, MouseEventArgs e)
        {
            volver();
            string combi = Path.Combine(direccion, "home.PNG");
            pictureBox2.Image = new System.Drawing.Bitmap(combi);


             combi = Path.Combine(direccion, "jla.PNG");
            pictureBox3.Image = new System.Drawing.Bitmap(combi);
            pictureBox3.Size = new Size(123, 95);
        }
        private void volver()
        {
            pictureBox4.Size = new Size(112, 84);
            pictureBox1.Size = new Size(112, 84);
            pictureBox2.Size = new Size(112, 84);
            pictureBox3.Size = new Size(112, 84);

        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            volver();
            string combi = Path.Combine(direccion, "home.PNG");
            pictureBox2.Image = new System.Drawing.Bitmap(combi);
            combi = Path.Combine(direccion, "busauq.PNG");
            pictureBox3.Image = new System.Drawing.Bitmap(combi);
            pictureBox4.Size = new Size(123,95);

            homee.Size = new Size(355, 338);
            homee.panel3.Top = 0;
            homee.panel3.Left = 0;
            homee.panel6.Left = 800;
            homee.panel6.Top = 800;
            homee.panel7.Top = 1000;
            homee.panel7.Left = 1000;
        }





        home homee = new home();
            
            
        private void Form1_Load(object sender, EventArgs e)
        {
            
            homee.Show();
            homee.WindowState = FormWindowState.Minimized;
            


            
            this.TopMost = true;
            this.WindowState = FormWindowState.Normal;

            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            homee.actualizar();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            homee.Size = new System.Drawing.Size(478, 481);
            homee.WindowState = FormWindowState.Normal;
            homee.panel6.Top = 36;
            homee.panel6.Left = 0;
            homee.panel3.Left = 800;
            homee.panel3.Top = 800;
            homee.panel7.Top = 1000;
            homee.panel7.Left = 1000;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            homee.Size = new Size(299, 380);
            homee.panel7.Top = 36;
            homee.panel7.Left = 0;
            homee.panel3.Left = 800;
            homee.panel3.Top = 800;
            homee.panel6.Top = 1000;
            homee.panel6.Left = 1000;
        }
    }
}
