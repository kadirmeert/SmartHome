using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;

namespace smarthome
{
    public partial class Form1 : Form
    {
        bool ldrsensor = false;
        bool dhtsensor = false;
        bool buzzersensor = false;
        
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
          
            string[] ports = SerialPort.GetPortNames();

            for (int i = 0; i < System.IO.Ports.SerialPort.GetPortNames().Length; i++)
            {
                comboBox1.Items.Add(System.IO.Ports.SerialPort.GetPortNames()[i]);
            }

            comboBox2.Items.Add("4800");
            comboBox2.Items.Add("9600");
            comboBox2.SelectedIndex = 1;
            label4.Text = "Bağlantı Kapalı";
            
        }
       

        private void evegeldim_Click(object sender, EventArgs e)
        {


            if (MessageBox.Show(" DO YOU WANT TO ENTER THE PASSWORD ?","??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                textBox1.Focus();

            }


            else
            {
                MessageBox.Show("PASSWORD LOGIN PROCESS HAS BEEN CANCELED BY YOU.", "PASSWORD LOGIN CANCEL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();

            }



        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {




        }

        private void button1_Click(object sender, EventArgs e)
        {
            int intnum = 12345;
            int girilensayı = new int();
            girilensayı = int.Parse(textBox1.Text);
            if (intnum == girilensayı)
            {

                MessageBox.Show("CORRECT PASSWORD", "SAFETY ENTRY", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                if (girilensayı == intnum)
                {
                    DialogResult dialogResult3 = new DialogResult();
                    dialogResult3 = MessageBox.Show("DO YOU WANNA OPEN THE GREEN LIGHT?", "LED CONTROL", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult3 == DialogResult.Yes)
                    {
                        textBox1.Text = "OPEN THE GREEN LIGHT";
                        serialPort1.Write("A");
                    }

                }
            }
            else
            {
                if (!(girilensayı == intnum))
                {
                    
                    MessageBox.Show("INCORRECT PASSWORD", "ALARM!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult dialogResult4 = new DialogResult();
                    dialogResult4 = MessageBox.Show("DOU YOU WANNA OPEN THE RED LIGHT?", "LED CONTROL", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult4 == DialogResult.Yes)
                    {
                        
                        textBox1.Text = "OPEN THE RED LIGHT";
                        serialPort1.Write("B");
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            
           timer1.Start();
            
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            try
            {

                string sonuc = serialPort1.ReadLine();
                string[] pot = sonuc.Split(',');
                label5.Text = pot[0];
                label6.Text = pot[1];
                


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void button3_Click(object sender, EventArgs e)
        {
            
            
            DialogResult dialogResult = new DialogResult();
            dialogResult = MessageBox.Show("DO YOU WANNA OPEN MOTION DETECTOR?", "MOTION DETECTOR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)

            {

                     timer2.Start();
                    string sonuc = serialPort1.ReadLine();
                    string[] pot = sonuc.Split(',');
                    textBox3.Text = pot[3];
                    timer3.Stop();
                

            }
            else
            {
                if (dialogResult == DialogResult.No)
                {

                    
                        serialPort1.Write("D");
                        label12.Text = "PIR SENSOR CLOSE";
                    }
                
            }


        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            
            try
            {
                string sonuc = serialPort1.ReadLine();
                string[] pot = sonuc.Split(',');

                textBox3.Text = pot[3];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void textBox4_event(object sender, EventArgs e)
        {
           
        }



        private void button7_Click(object sender, EventArgs e)
        { 

            if (serialPort1.IsOpen == false)
            {
                if (comboBox1.Text == "")
                    return;
                serialPort1.PortName = comboBox1.Text;
                serialPort1.BaudRate = Convert.ToInt16(comboBox2.Text);
                try
                {
                    serialPort1.Open();
                    label4.Text = "Bağlantı Açık";
                }
                catch (Exception hata)
                {
                    MessageBox.Show("Hata:" + hata.Message);
                    throw;
                }
            }

            else
            {
                label3.Text = "Bağlantı Kuruldu";
            }
        }


        private void button8_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Close();
                label4.Text = "Bağlantı Kapalı";
            }
        }



        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort1.IsOpen)

                serialPort1.Close();

        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
           
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            DialogResult dialogResult = new DialogResult();
            dialogResult = MessageBox.Show("DO YOU WANA ACTİVE LDR??", "LDR SENSOR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                
                timer3.Start();
                string sonuc = serialPort1.ReadLine();
                string[] pot = sonuc.Split(',');
                textBox4.Text = pot[2];
                timer1.Stop();


            }
            else
            {
                if (dialogResult == DialogResult.No)
                {
                   
                    textBox4.Text = "LDR INACTİVE";
                }
            }
        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            
            try
            {
                string sonuc = serialPort1.ReadLine();
                 string[] pot = sonuc.Split(',');
                textBox4.Text = pot[2];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            int okunandeger = new int();
            okunandeger = int.Parse(textBox3.Text);
            
            if (buzzersensor == false) {
                if (okunandeger >0)
                {
                    
                    DialogResult dialogResult7 = new DialogResult();
                    buzzersensor = true;
                    dialogResult7 = MessageBox.Show("DOU YOU WANNA OPEN THE BUZZER", "BUZZER OPEN", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult7 == DialogResult.Yes)
                    {
                        serialPort1.Write("C");
                        label12.Text = "BUZZER İS OPEN";

                        buzzersensor = true;
                    }
                }
                else if (okunandeger == 0)
                {

                    buzzersensor = false;

                    serialPort1.Write("D");
                    label12.Text = "BUZZER İS CLOSE";

                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            int ldrokunandeger = new int();
            ldrokunandeger = int.Parse(textBox4.Text);

            DialogResult dialogResult6 = new DialogResult();
            DialogResult dialogResult11 = new DialogResult();

            if (ldrsensor == false)
            {
                if (ldrokunandeger < 300)
                {
                    ldrsensor = true;
                    dialogResult6 = MessageBox.Show("DOU WANNA OPEN THE LIGHT?", "LIGHT OPEN", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                }

                if (dialogResult6 == DialogResult.Yes)
                {

                    dialogResult11 = MessageBox.Show("DOES LED REFLECT STRONG??", "LED SETTING", MessageBoxButtons.YesNo);
                }

                if (dialogResult11 == DialogResult.Yes)
                {
                    ldrsensor = true;
                    serialPort1.Write("1");
                }
                if (dialogResult11 == DialogResult.No)
                {
                    ldrsensor = true;
                    serialPort1.Write("2");
                }
            }
            else
            {
                if (dialogResult6 == DialogResult.No)
                {

                    serialPort1.Write("3");
                }

               /* if (ldrokunandeger > 400)
                {

                    serialPort1.Write("3");
                }*/
            }            
        }


        private void label5_TextChanged(object sender, EventArgs e)
        {
            int okunansicaklik = new int();
            okunansicaklik = int.Parse(label5.Text);
            DialogResult dialogResult8 = new DialogResult();
            DialogResult dialogResult9 = new DialogResult();
            DialogResult dialogResult10 = new DialogResult();
            if (dhtsensor == false)
            {
                if (int.Parse(label5.Text) > 28)
                {


                    dhtsensor = true;
                    dialogResult8 = MessageBox.Show("DO YOU WANNA OPEN THE FAN ? ", "FAN OPEN", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }

                if (dialogResult8 == DialogResult.Yes)
                {
                    dialogResult9 = MessageBox.Show("DOU YOU WANNA FAST FAN", "FAN SETTING", MessageBoxButtons.YesNo);
                    if (dialogResult9 == DialogResult.Yes)
                    {
                        dhtsensor = true;
                        serialPort1.Write("G");
                        label7.Text = "FAN IS FAST";
                    }
                }
                if (dialogResult8 == DialogResult.No)
                { serialPort1.Write("F"); }

                if (dialogResult9 == DialogResult.No)
                {
                    dhtsensor = true;
                    serialPort1.Write("H");
                    label7.Text = "FAN IS LOW";
                }
            }

            if (int.Parse(label5.Text) == 29)
            {
                dialogResult10 = MessageBox.Show("DO YOU WANNA CLOSE  FAN ", "FAN CLOSE", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            if (dialogResult10 == DialogResult.Yes)
            { serialPort1.Write("F"); }
            if (dialogResult10 == DialogResult.No)
            {
                dhtsensor = false;
                serialPort1.Write("G");
            }
        }  
    }
}
    
            
        

        

        
    


        
    

