using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace frazione
{
    public partial class Form1 : Form
    {
        private Frazione frazione1;
        private FrazioneDecimal frazioneDecimal;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }


        private void button1_Click(object sender, EventArgs e)//pulsante semplifica frazione
        {
            int num = int.Parse(textBox1.Text);
            int denom = int.Parse(textBox2.Text);
            Frazione frazione = new Frazione(num, denom);
            frazione.Semplifica();
            textBox3.Text = frazione.Numeratore.ToString() + "/" + frazione.Denominatore.ToString();
        }

        private void button2_Click(object sender, EventArgs e)//pulsante somma frazione
        {
            int num = int.Parse(textBox1.Text);
            int denom = int.Parse(textBox2.Text);
            Frazione frazione = new Frazione(num, denom);
            textBox3.Text=frazione.Somma().ToString();   
        }

        private void button3_Click(object sender, EventArgs e)//pulsante sottrai frazione
        {
            int num = int.Parse(textBox1.Text);
            int denom = int.Parse(textBox2.Text);
            Frazione frazione =new Frazione(num, denom);
            textBox3.Text= frazione.Sottrai().ToString(); 
        }

        private void button4_Click(object sender, EventArgs e)//pulsante moltiplica frazione
        {
            int num = int.Parse(textBox1.Text);
            int denom = int.Parse(textBox2.Text);
            Frazione frazione = new Frazione(num,denom);
            textBox3.Text = frazione.Moltiplica().ToString();
           
        
        }

        private void button5_Click(object sender, EventArgs e)//pulsante dividi frazione
        {
            int num = int.Parse(textBox1.Text);
            int denom = int.Parse(textBox2.Text);
            Frazione frazione = new Frazione(num, denom);
            textBox3.Text = frazione.Dividi().ToString();
        }

        private void button6_Click(object sender, EventArgs e) //pulsante elevazione a potenza
        {
            int num = int.Parse(textBox1.Text);
            int denom = int.Parse(textBox2.Text);
            FrazioneDecimal frazione = new FrazioneDecimal(num, denom);
            textBox3.Text = frazione.ElevaAPotenza().ToString();
        }
    }


    // Classe Frazione
    public class Frazione
    {
        private int numeratore;
        private int denominatore;

        // Costruttore
        public Frazione(int numeratore, int denominatore)
        {
            this.numeratore = numeratore;
            this.denominatore = denominatore;
    
        }

        public Frazione()
        {
        
        }

        // Costruttore di copia
        public Frazione(Frazione altreFrazione)
        {
            this.numeratore = altreFrazione.numeratore;
            this.denominatore = altreFrazione.denominatore;
 
        }

        // Proprietà
        public int Numeratore
        {
            get { return numeratore; }
            set { numeratore = value; }
        }

        public int Denominatore
        {
            get { return denominatore; }
            set
            {
                if (value != 0)
                    denominatore = value;
                else
                    throw new ArgumentException("Il denominatore non può essere zero.");
            }
        }

       

        // Metodo per semplificare la frazione
        public void Semplifica()
        {
            int mcd = TrovaMCD(Math.Abs(numeratore), Math.Abs(denominatore));
            numeratore /= mcd;
            denominatore /= mcd;
        }

        // Metodo per trovare il Massimo Comun Divisore
        private int TrovaMCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        // Metodo per sommare nueratore e denominatore
        public int Somma()
        {
            int somma =  Numeratore + Denominatore;
            return somma;
        }

        // Metodo per sottrarre due frazioni
        public int Sottrai()
        {
            int sottrai;
            sottrai = Numeratore - Denominatore;
            return sottrai;
        }

        // Metodo per moltiplicare due frazioni
        public int Moltiplica()
        {
            int moltiplica;
            moltiplica = Numeratore * Denominatore;
            return moltiplica;
        }

        // Metodo per dividere due frazioni
        public double Dividi()
        {
            double dividi;
            double num = double.Parse(Numeratore.ToString());
            double denom = double.Parse(Denominatore.ToString());
            dividi =(num / denom);
            if (denom == 0)
            {
                MessageBox.Show("Errore, valore indefinito");
            }
            return dividi;
            
            
        }

        // Metodo per clonare la frazione
        public Frazione Clone()
        {
            return new Frazione(this);
        }
    }

    // Classe derivata da Frazione
    public class FrazioneDecimal : Frazione
    {
        // Costruttore
        public FrazioneDecimal(int numeratore, int denominatore) : base(numeratore, denominatore)
        {

        }

        public FrazioneDecimal(Frazione altreFrazione) : base(altreFrazione)
        {

        }

        // Metodo per rappresentare la frazione in decimale
        public decimal ConvertiInDecimale()
        {
            return (decimal)Numeratore / Denominatore;
        }

        // Metodo per convertire un numero decimale in frazione
        public static FrazioneDecimal ConvertiDaDecimale(decimal numeroDecimale)
        {
            int potenza = 1;
            while (Math.Round(numeroDecimale * potenza) % 1 != 0)
            {
                potenza *= 10;
            }

            return new FrazioneDecimal((int)(numeroDecimale * potenza), potenza);
        }

        // Metodo per elevare la frazione a una potenza
        public FrazioneDecimal ElevaAPotenza(int esponente)
        {
            return new FrazioneDecimal((int)Math.Pow(Numeratore, esponente), (int)Math.Pow(Denominatore, esponente));
        }

        
    }

    
}
