using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace frazione
{
    public partial class Form1 : Form
    {
        private Frazione frazione1;
        private Frazione frazione2;
        private FrazioneDecimal frazioneDecimal;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            frazione1 = new Frazione(0, 1);
            frazione2 = new Frazione(0, 1);
            frazioneDecimal = new FrazioneDecimal(0, 1);
        }

        private void AggiornaInterfaccia()
        {
            textBox1.Text = $"{frazione1.Numeratore}/{frazione1.Denominatore}";
            textBox2.Text = $"{frazione2.Numeratore}/{frazione2.Denominatore}";
            textBox3.Text = $"{frazioneDecimal.ConvertiInDecimale()}";
        }

        private void button1_Click(object sender, EventArgs e)//pulsante semplifica frazione
        {
            frazione1.Semplifica();
            frazione2.Semplifica();
            AggiornaInterfaccia();
        }

        private void button2_Click(object sender, EventArgs e)//pulsante somma frazione
        {
            Frazione risultato = frazione1.Somma(frazione2);
            frazioneDecimal = new FrazioneDecimal(risultato.Numeratore, risultato.Denominatore);
            AggiornaInterfaccia();
        }

        private void button3_Click(object sender, EventArgs e)//pulsante sottrai frazione
        {
            Frazione risultato = frazione1.Sottrai(frazione2);
            frazioneDecimal = new FrazioneDecimal(risultato.Numeratore, risultato.Denominatore);
            AggiornaInterfaccia();
        }

        private void button4_Click(object sender, EventArgs e)//pulsante moltiplica frazione
        {
            Frazione risultato = frazione1.Moltiplica(frazione2);
            frazioneDecimal = new FrazioneDecimal(risultato.Numeratore, risultato.Denominatore);
            AggiornaInterfaccia();
        }

        private void button5_Click(object sender, EventArgs e)//pulsante dividi frazione
        {
            try
            {
                Frazione risultato = frazione1.Dividi(frazione2);
                frazioneDecimal = new FrazioneDecimal(risultato.Numeratore, risultato.Denominatore);
                AggiornaInterfaccia();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        // Metodo per sommare due frazioni
        public Frazione Somma(Frazione altraFrazione)
        {
            int nuovoNumeratore = numeratore * altraFrazione.denominatore + altraFrazione.numeratore * denominatore;
            int nuovoDenominatore = denominatore * altraFrazione.denominatore;
            return new Frazione(nuovoNumeratore, nuovoDenominatore);
        }

        // Metodo per sottrarre due frazioni
        public Frazione Sottrai(Frazione altraFrazione)
        {
            int nuovoNumeratore = numeratore * altraFrazione.denominatore - altraFrazione.numeratore * denominatore;
            int nuovoDenominatore = denominatore * altraFrazione.denominatore;
            return new Frazione(nuovoNumeratore, nuovoDenominatore);
        }

        // Metodo per moltiplicare due frazioni
        public Frazione Moltiplica(Frazione altraFrazione)
        {
            int nuovoNumeratore = numeratore * altraFrazione.numeratore;
            int nuovoDenominatore = denominatore * altraFrazione.denominatore;
            return new Frazione(nuovoNumeratore, nuovoDenominatore);
        }

        // Metodo per dividere due frazioni
        public Frazione Dividi(Frazione altraFrazione)
        {
            if (altraFrazione.numeratore != 0)
            {
                int nuovoNumeratore = numeratore * altraFrazione.denominatore;
                int nuovoDenominatore = denominatore * altraFrazione.numeratore;
                return new Frazione(nuovoNumeratore, nuovoDenominatore);
            }
            else
            {
                throw new ArgumentException("Impossibile dividere per zero.");
            }
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
