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

namespace Rockola
{
    public partial class home : Form
    {
        public home()
        {
            InitializeComponent();
            labelactual.Text = letras[contadorletras].ToString();
            labelanterior.Text = "";
            labelsiguiente.Text = letras[contadorletras + 1].ToString();
        }

        string rutproyecto = "F:";
        public string letras = "ABCDEFGHIJlMNOPQRSTUVWXYZ";
        public string rutaaudio;
        public string dire;
        public List<string> listaArtista = new List<string>();
        public List<string> cancionesArista = new List<string>();

        public List<string> SiguienteCancionRuta = new List<string>();
        public List<string> siguientecancion = new List<string>();
        public string rutasuguiente = "";
        public int contador = 0;//cuanto artistas
        public int semuestra = 0;//artista actual
        public int numerodecanciones = 0;


        public int segiun = 0;
        public bool pres = false;

        public int contadorletras = 0;
        public int masomenos = 0;
        public int tiempo = 0;

        int cn = 0;
        private void home_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            label1.BackColor = Color.Transparent;
            panel1.BackColor = Color.Transparent;
            panel2.BackColor = Color.Transparent;
            labelactual.BackColor = Color.Transparent;
            labelanterior.BackColor = Color.Transparent;
            labelsiguiente.BackColor = Color.Transparent;
            labelArtista.BackColor = Color.Transparent;
            panel6.BackColor = Color.Transparent;
            panel3.BackColor = Color.Transparent;
            panel7.BackColor = Color.Transparent;


            //si no existe lo crea
            if ((File.Exists(@"C:\Windows\Temp\listado.txt") == false))
            {
                crearlista_artis();
              //  acualizarlista();
                artistaAcutal();
                //timer1.Enabled = true;
            }
            else
            {
                //solo actualiza los datos

                string ruta = Path.GetFullPath(rutproyecto);
                ruta = Path.Combine(ruta, "Direccion.txt");
                //recibe la direccion de artistas
                StreamReader di = new StreamReader(ruta);
                dire = di.ReadLine();
                di.Close();
                acualizarlista();
                artistaAcutal();
                timer1.Enabled = true;
            }
        }
        private void crearlista_artis()//buscar archivo 
        //y crear la lista d3 artistas
        {
            string nue = Path.GetFullPath(rutproyecto);//ruta acual del proyecto
            string ruta = Path.Combine(nue, "artista.bat");//combinar para seleccion de un archivo
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == System.Windows.Forms.DialogResult.OK)//abrir buscador de carpeta
            {
                dire = folder.SelectedPath;//obtiene ruta de carpeta seleccionada


                //se crea el archivo tipo bat para hacer listas

                StreamWriter crear;

                //modificva el archivo artista.bat
                string direex = "\"" + dire + "\"";
                crear = new StreamWriter(ruta);
                crear.WriteLine("dir /a /b /-p /o: " + direex.ToString() + " >C:\\WINDOWS\\Temp\\listado.txt");

                crear.Close();
                //editar = System.IO.Path.ChangeExtension(nue, ".bat");
                System.Diagnostics.Process.Start(ruta);


                //se guarda la direccion de artistas
                string paradireccion = Path.Combine(nue, "Direccion.txt");
                StreamWriter di = new StreamWriter(paradireccion);
                {
                    di.WriteLine(dire);

                    di.Close();
                }

                acualizarlista();
            }
        }


        private void acualizarlista()
        {

            //si existe la lista de artistas
            if ((File.Exists(@"C:\Windows\Temp\listado.txt") == true))
            {
                StreamReader leerartistas = new StreamReader(@"C:\Windows\Temp\listado.txt", System.Text.Encoding.Default);

                string linea = "";
                while (linea != null)
                {
                    linea = leerartistas.ReadLine();
                    if (linea != null)
                    {
                        string extencion = System.IO.Path.GetExtension(linea);
                        //agregarlo a la lista solo si son carpetas
                        if (extencion == "")
                        {
                            listaArtista.Add(enbusca(linea));
                            contador++;
                        }
                    }
                }
                leerartistas.Close();
                /* if (listBox1.Items.Count != 0)
                 {
                     int s = listBox1.Items.Count - 1;
                     listBox1.Items.RemoveAt(s);
                 }
                 */
                if (contador > 0)
                {
                    labelArtista.Text = listaArtista[0];


                }
            }

        }


        private void artistaAcutal()
        {
            bool error = false;
            do
            {
                try
                {
                    if (contador != 0)
                    {
                        //recibe el nombre
                        string artista = listaArtista[semuestra];
                        string nue = Path.GetFullPath(rutproyecto);
                        //une las direciones para ejecutar lista de canciones
                        nue = Path.Combine(nue, "canciones.bat");

                        //se modifica el archivo canciones.bat


                        StreamWriter crear;
                        //direcion de la carpeta
                        string cancio = Path.Combine(dire, artista);



                        rutaaudio = cancio;//ruta hasta donde el artista contenedora

                        cancio = "\"" + cancio + "\"";//para aceptar datos con espacios en .bat
                        crear = new StreamWriter(nue);
                        crear.WriteLine("dir /a /b /-p /o: " + cancio + " >C:\\WINDOWS\\Temp\\music.txt");

                        crear.Close();

                        System.Diagnostics.Process.Start(nue);
                        //fin


                        canciones();
                        error = false;
                    }
                }
                catch
                {
                    error = true;
                }

            } while (error == true);
        }
        private void canciones()
        {
            //leo la lista de canciones del artista en artista
            //solo archivos .mp3

            listBoxCanciones.Items.Clear();
            cancionesArista.Clear();
            
            StreamReader leercanciones = new StreamReader(@"C:\Windows\Temp\music.txt", System.Text.Encoding.Default);

            string linea = "";
            numerodecanciones = 0;
            while (linea != null)
            {
                linea = leercanciones.ReadLine();
                if (linea != null)
                {
                    string extencion = System.IO.Path.GetExtension(linea);
                     if (extencion == ".mp3")
                    {
                        cancionesArista.Add(enbusca(linea));
                        numerodecanciones++;
                    }

                }
            }
            leercanciones.Close();
            procedi();
        }



        private string enbusca(string linea)//recodificacion
        {

            //en el 4rchivo h4y c4rr4cteres ue no est4n bien por eso se modific4  
            int largo = linea.Length;
            string nuevo = "";
            string dato = "";
            string simbolo = "";
            for (int i = 0; i < largo; i++)
            {
                nuevo = "";
                simbolo = "";
                switch (linea[i].ToString())
                {

                    case "”":
                        {
                            simbolo = "”";
                            dato = "ö";
                            break;
                        }
                    case "¤":
                        {
                            simbolo = "¤";
                            dato = "ñ";
                            break;
                        }
                    case ",":
                        {
                            simbolo = ",";
                            dato = "é";
                            break;
                        }
                    case "¡":
                        {
                            simbolo = "¡";
                            dato = "í";
                            break;
                        }
                    case "Ö":
                        {
                            simbolo = "Ö";
                            dato = "Í";
                            break;

                        }
                    case "£":
                        {
                            simbolo = "£";
                            dato = "ú";
                            break;
                        }
                    case "é":
                        {
                            simbolo = "é";
                            dato = "Ú";
                            break;
                        }
                    case "¢":
                        {
                            simbolo = "¢";
                            dato = "ó";
                            break;
                        }
                    case "à":
                        {
                            simbolo = "à";
                            dato = "Ó";
                            break;
                        }
                }
                for (int j = 0; j < largo; j++)
                {
                    if (linea[j].ToString() == simbolo.ToString())
                    {
                        nuevo = nuevo + dato;
                    }
                    else
                    {
                        nuevo = nuevo + linea[j];
                    }

                }
                linea = nuevo;
            }

            return linea;
        }
        private void procedi()
        {
            listBoxCanciones.Items.Clear();
            for (int i = 0; i < numerodecanciones; i++)
            {
                listBoxCanciones.Items.Add(cancionesArista[i]);
            }
        }


        public double credito = 0;
        public bool nueva = false;
        int contadoreReproducciones = -1;
        private void button1_Click(object sender, EventArgs e)//seleccion de musica
        {
            
                if (credito >= 1)
                {
                    //Reproduciendo repro = new Reproduciendo();
                    if (nueva == false)
                    {
                        if (listBoxCanciones.SelectedIndex != -1)
                        {

                            rutasuguiente = Path.Combine(rutaaudio, listBoxCanciones.SelectedItem.ToString());
                            if (File.Exists(rutasuguiente) == true)
                            {
                                axWindowsMediaPlayer1.URL = rutasuguiente;
                                //repro.label6.Text = rutasuguiente;
                                label1.Text = labelArtista.Text + " - " + listBoxCanciones.SelectedItem;
                                label2.Text = labelArtista.Text + " - " + listBoxCanciones.SelectedItem;
                                progressBar1.Value = 0;
                                progressBar2.Value = 0;
                                credito--;
                                label6.Text = credito.ToString();
                                label8.Text = credito.ToString();
                                SiguienteCancionRuta.Add(rutasuguiente);
                                siguientecancion.Add(labelArtista.Text + " - " + listBoxCanciones.SelectedItem);
                                contadoreReproducciones++;
                                nueva = true;
                                duracion();


                            }

                        }
                    }
                    else
                    {
                        if (listBoxCanciones.SelectedIndex != -1)
                        {

                            rutasuguiente = Path.Combine(rutaaudio, listBoxCanciones.SelectedItem.ToString());
                            if (File.Exists(rutasuguiente) == true)
                            {
                                SiguienteCancionRuta.Add(rutasuguiente);
                                siguientecancion.Add(labelArtista.Text + " - " +listBoxCanciones.SelectedItem);
                                //repro.label6.Text = rutasuguiente;
                                
                                credito--;
                                label6.Text = credito.ToString();
                                label8.Text = credito.ToString();
                                contadoreReproducciones++;

                            }

                        }
                    }

                }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (listBoxCanciones.Items.Count > 0)
            {
                rutasuguiente = Path.Combine(rutaaudio, listBoxCanciones.Items[0].ToString());
            }
            if (File.Exists(rutasuguiente) == false)
            {


                artistaAcutal();

            }
            timer1.Enabled = false;
        }

        private void labelArtista_Click(object sender, EventArgs e)
        {
            artistaAcutal();
            timer1.Enabled = true;
        }



        private void duracion()
        {
            //Reproduciendo repro = new Reproduciendo();
            string s = axWindowsMediaPlayer1.newMedia(rutasuguiente).durationString;
            int minutos = 0;
            segiun = 0;
            string por = "";
            //bool listo=false;
            for (int i = 0; i < s.Length; i++)
            {
                if ((s[i].ToString() != ":"))//&&(listo==false))
                {
                    por =por+ s[i].ToString();
                }
                else if ((s[i].ToString() == ":"))
                {
                    minutos = int.Parse(por);
                    por = "";
                }
            }
            segiun = int.Parse(por);
            segiun = (minutos * 60) + segiun;
            progressBar1.Maximum = segiun;
            progressBar2.Maximum = segiun;
            timer2.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < segiun)
            {
                progressBar1.Value++;
                progressBar2.Value++;
            }
            else
            {
                timer2.Enabled = false;
                progressBar1.Value = 0;
                progressBar2.Value = 0;
            }
        }

        private void buttonSiguiente_MouseDown(object sender, MouseEventArgs e)
        {
            if (contador >= 0)
            {

                if (semuestra < contador - 1)
                {

                    semuestra++;
                    labelArtista.Text = listaArtista[semuestra];

                    //artistaAcutal();
                    // timer1.Enabled = true;
                }
                pres = true;
                masomenos = 1;
                timer3.Enabled = true;
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (tiempo > 2)
            {
                if ((contadorletras < 25) && (masomenos == 1))
                {
                    contadorletras++;
                    labelsiguiente.Text = letras[contadorletras + 1].ToString();
                    labelactual.Text = letras[contadorletras].ToString();
                    labelanterior.Text = letras[contadorletras - 1].ToString();
                }
                else if ((contadorletras > 1) && (masomenos == -1))
                {
                    contadorletras--;
                    labelsiguiente.Text = letras[contadorletras + 1].ToString();
                    labelactual.Text = letras[contadorletras].ToString();
                    labelanterior.Text = letras[contadorletras - 1].ToString();
                }
            }
            else
            {
                tiempo++;
            }
        }

        private void buttonSiguiente_MouseUp(object sender, MouseEventArgs e)
        {
            sisecambia();
        }

        private void buttonAnterior_MouseUp(object sender, MouseEventArgs e)
        {
            sisecambia();
        }

        private void buttonAnterior_MouseDown(object sender, MouseEventArgs e)
        {
            if (contador >= 0)
            {
                if (semuestra > 0)
                {
                    semuestra--;
                }
                if (semuestra >= 0)
                {
                    labelArtista.Text = listaArtista[semuestra];
                    // artistaAcutal();
                    masomenos = -1;
                    pres = true;
                    timer3.Enabled = true;
                }
            }
        }

        private void sisecambia()
        {
            pres = false;
            if ((pres == false) && (tiempo > 2))
            {
                semuestra = 0;
                pres = false;
                timer3.Enabled = false;
                string dato = listaArtista[semuestra];
                if (dato[0].ToString() != letras[contadorletras].ToString())
                {
                    for (int i = 0; i < contador - 1; i++)
                    {
                        string art = listaArtista[i];
                        if (art[0].ToString() == letras[contadorletras].ToString())
                        {
                            i = contador;

                            artistaAcutal();
                        }
                        else
                        {
                            semuestra++;
                        }
                    }
                }
                labelArtista.Text = listaArtista[semuestra];
            }
            else
            {

                string art = labelArtista.Text;
                if (art[0].ToString() != letras[contadorletras].ToString())
                {
                    for (int i = 0; i < 25; i++)
                    {
                        if (art[0].ToString() == letras[i].ToString())
                        {
                            if (i == 0)
                            {
                                labelanterior.Text = "";
                                labelactual.Text = letras[i].ToString();
                                labelsiguiente.Text = letras[i + 1].ToString();
                            }
                            else
                            {
                                labelanterior.Text = letras[i - 1].ToString();
                                labelactual.Text = letras[i].ToString();
                                labelsiguiente.Text = letras[i + 1].ToString();

                            }
                            contadorletras = i;
                        }
                    }
                }

            }



            tiempo = 0;
            timer3.Enabled = false;







        }

        private void panel1_Click(object sender, EventArgs e)
        {
            artistaAcutal();
            timer1.Enabled = true;
        }

        private void listBoxCanciones_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void actualizar()
        { 
            listaArtista.Clear();
            contador = 0;
            crearlista_artis();
            artistaAcutal();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            credito++;
            label6.Text = credito.ToString();
            label8.Text = credito.ToString();
        }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (contadoreReproducciones > 0)
            {
                cn++;
                if (cn <= contadoreReproducciones)
                {
                    axWindowsMediaPlayer1.URL = SiguienteCancionRuta[cn];
                    label1.Text = siguientecancion[cn];
                    label2.Text = siguientecancion[cn];
                    progressBar1.Value = 0;
                    progressBar2.Value = 0;
                    duracion();
                }
            }
        }

        private void listBoxCanciones_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //comboBox1.Items.Clear();
            listBox2.Items.Clear();
            string texto = textBox1.Text;
            int largodetexto = texto.Length;
            string artistasencontrados = "";
            string cadenaartista = "";
            for (int j = 0; j < contador; j++)
            {
                artistasencontrados = listaArtista[j];
                if (artistasencontrados.Length >= largodetexto)
                {
                    cadenaartista = "";
                    for (int i = 0; i < largodetexto; i++)
                    {
                        cadenaartista = cadenaartista + artistasencontrados[i];
                    }
                    if (cadenaartista == texto)
                    {
                        listBox2.Items.Add(artistasencontrados);
                    }
                }
            }
        }

        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox2.Items.Count > 0)
            {
                if (listBox2.SelectedIndex != -1)
                {
                    for (int i = 0; i < contador; i++)
                    {
                        if (listBox2.SelectedItem.ToString() == listaArtista[i].ToString())
                        {
                            labelArtista.Text = listaArtista[i];
                            semuestra = i;
                            artistaAcutal();
                            i = contador;
                        }
                    }
                   
                }
            }
            sisecambia();
            Size = new System.Drawing.Size(478, 481);
           WindowState = FormWindowState.Normal;
            panel6.Top = 36;
            panel6.Left = 0;
            panel3.Left = 800;
            panel3.Top = 800;
            panel7.Top = 1000;
            panel7.Left = 1000;

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
