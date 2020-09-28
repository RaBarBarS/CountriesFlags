using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace PanstwaMiasta
{
    public partial class Form1 : Form
    {
        List<Panstwo> lista_panstw = new List<Panstwo>();
        ImageList imgList = new ImageList();
        
        [Serializable]
        public class Panstwo
        {
            public string nazwa { get; set; }
            public string stolica { get; set; }
            public string powierzchinia { get; set; }
            public string ludnosc { get; set; }
            public Image flaga { get; set; }
        }

        bool nazwa = true;
        bool stolica = true;
        bool powierzchnia_od = true;
        bool powierzchnia_do = true;
        bool ludnosc_od = true;
        bool ludnosc_do = true;

        

        public Form1()
        {
            InitializeComponent();
            read();
            imgList.ImageSize = new Size(120, 80);
            szukanko();
        }


        public bool czy_same_cyferki(TextBox textBox)
        {
            bool correct_value = true;
            byte[] asciiBytes = Encoding.ASCII.GetBytes(textBox.Text);
            for (int i = 0; i < asciiBytes.Count(); i++)
            {
                if (asciiBytes[i] < 48 || asciiBytes[i] > 57)
                {
                    textBox.BackColor = Color.LightCoral;
                    correct_value = false;
                }
                if (i == asciiBytes.Count() - 1 && correct_value)//ostatni przebieg fora
                {
                    textBox.BackColor = Color.White;
                }
            }
            if (textBox.Text.Count() == 0)//żeby jak puste to też wróciło do białego
            {
                correct_value = true;
                textBox.BackColor = Color.White;
            }
            if (correct_value)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool czy_same_literki(TextBox textBox)
        {
            bool correct_value = true;
            byte[] asciiBytes = Encoding.ASCII.GetBytes(textBox.Text);
            for (int i = 0; i < asciiBytes.Count(); i++)
            {
                if (asciiBytes[i] < 65 || asciiBytes[i] > 122 || asciiBytes[i] == 91 || asciiBytes[i] == 92 || asciiBytes[i] == 93 || asciiBytes[i] == 94 || asciiBytes[i] == 95 || asciiBytes[i] == 96)
                {
                    textBox.BackColor = Color.LightCoral;
                    correct_value = false;
                }
                if (i == asciiBytes.Count() - 1 && correct_value)//ostatni przebieg fora
                {
                    textBox.BackColor = Color.White;
                }
            }

            if (textBox.Text.Count() == 0)//żeby jak puste to też wróciło do białego
            {
                correct_value = true;
                textBox.BackColor = Color.White;
            }
            if (correct_value)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (czy_same_literki(textBox1))
            {
                nazwa = true;
                szukanko();
            }
            else
            {
                nazwa = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (czy_same_literki(textBox2))
            {
                stolica = true;
                szukanko();
            }
            else
            {
                stolica = false;
            }
        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (czy_same_cyferki(textBox3))
            {
                powierzchnia_od = true;
                szukanko();
            }
            else
            {
                powierzchnia_od = false;
            }
        }
        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text.Length == 0)
            {
                textBox3.Text = "0";
            }
            szukanko();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
       {
            if (czy_same_cyferki(textBox4))
            {
                powierzchnia_do = true;
                szukanko();
            }
            else
            {
                powierzchnia_do = false;
            }
        }
        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text.Length == 0)
            {
                textBox4.Text = "0";
            }
            szukanko();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (czy_same_cyferki(textBox6))
            {
                ludnosc_od = true;
                szukanko();
            }
            else
            {
                ludnosc_od = false;
            }
        }
        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (textBox6.Text.Length == 0)
            {
                textBox6.Text = "0";
            }
            szukanko();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (czy_same_cyferki(textBox5))
            {
                ludnosc_do = true;
                szukanko();
            }
            else
            {
                ludnosc_do = false;
            }
        }
        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text.Length == 0)
            {
                textBox5.Text = "0";
            }
            szukanko();
        }

        void szukanko()
        {
            imgList.Images.Clear();
            int ilosc = 0;
            if (nazwa && stolica && ludnosc_od && ludnosc_do && powierzchnia_od && powierzchnia_do)
            {
                if (textBox4.Text == "0" && textBox5.Text == "0")
                {
                    foreach (var elem in lista_panstw)
                    {
                        try
                        {
                            if (elem.nazwa.StartsWith(textBox1.Text) && elem.stolica.StartsWith(textBox2.Text) && Int32.Parse(elem.powierzchinia) > Int32.Parse(textBox3.Text) && Int32.Parse(elem.ludnosc) > Int32.Parse(textBox6.Text))
                            {
                                imgList.Images.Add(elem.flaga);
                                ilosc++;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("problem z konwersją liczb");
                        }
                    }
                }
                else if (textBox4.Text == "0")
                {
                    foreach (var elem in lista_panstw)
                    {
                        try
                        {
                            if (elem.nazwa.StartsWith(textBox1.Text) && elem.stolica.StartsWith(textBox2.Text) && Int32.Parse(elem.powierzchinia) > Int32.Parse(textBox3.Text) && Int32.Parse(elem.ludnosc) > Int32.Parse(textBox6.Text) && Int32.Parse(elem.ludnosc) < Int32.Parse(textBox5.Text))
                            {
                                imgList.Images.Add(elem.flaga);
                                ilosc++;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("problem z konwersją liczb");
                        }
                    }
                }
                else if (textBox5.Text == "0")
                {
                    foreach (var elem in lista_panstw)
                    {
                        try { 
                            if (elem.nazwa.StartsWith(textBox1.Text) && elem.stolica.StartsWith(textBox2.Text) && Int32.Parse(elem.powierzchinia) > Int32.Parse(textBox3.Text) && Int32.Parse(elem.ludnosc) > Int32.Parse(textBox6.Text) && Int32.Parse(elem.powierzchinia) < Int32.Parse(textBox4.Text))
                            {
                                imgList.Images.Add(elem.flaga);
                                ilosc++;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("problem z konwersją liczb");
                        }
                    }
                }
                else
                {
                    foreach (var elem in lista_panstw)
                    {
                        try
                        {
                            if (elem.nazwa.StartsWith(textBox1.Text) && elem.stolica.StartsWith(textBox2.Text) && Int32.Parse(elem.powierzchinia) > Int32.Parse(textBox3.Text) && Int32.Parse(elem.ludnosc) > Int32.Parse(textBox6.Text) && Int32.Parse(elem.ludnosc) < Int32.Parse(textBox5.Text) && Int32.Parse(elem.powierzchinia) < Int32.Parse(textBox4.Text))
                            {
                                imgList.Images.Add(elem.flaga);
                                ilosc++;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("problem z konwersją liczb");
                        }
                    }
                }
            }
            label5.Text = ilosc.ToString();
            listView1.Items.Clear();
            listView1.View = View.LargeIcon;
            listView1.LargeImageList = imgList;

            for (int i = 0; i < imgList.Images.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.ImageIndex = i;
                listView1.Items.Add(item);
            }
        }

        private void read()
        {
            try
            {
                try {
                    foreach (var line in File.ReadLines("panstwa.txt"))
                    {
                        Panstwo temp = new Panstwo();
                        var tempLine = line.Split(';');
                        temp.nazwa = tempLine[0];
                        temp.stolica = tempLine[1];
                        temp.powierzchinia = tempLine[2];
                        temp.ludnosc = tempLine[3];
                        try
                        {
                            temp.flaga = Image.FromFile(temp.nazwa + ".jpg");
                        }
                        catch
                        {
                            temp.flaga = Image.FromFile(temp.nazwa + ".png");
                        }
                        lista_panstw.Add(temp);
                    }
                    Console.WriteLine("dane się ładnie wczytały");
                }
                catch(IOException ee)
                {
                    Console.WriteLine("dane w pliku są problematyczne");
                }
                
            }
            catch (IOException e)
            {
                Console.WriteLine("nie istnieje plik panstwa.txt");
            }
        }

        
    }
}
