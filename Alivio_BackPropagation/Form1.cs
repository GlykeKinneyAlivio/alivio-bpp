using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Backprop;

namespace Alivio_BackPropagation
{
    public partial class Form1 : Form
    {
        NeuralNet Nn;

        public Form1()
        {
            InitializeComponent();
            this.Width = 417;
            this.Height = 253;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Nn = new NeuralNet(4, 125, 1);

            if (Nn != null)
            {
                button1.Enabled = false;
                button2.Enabled = true;
                button3.Enabled = true;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox8.Enabled = true;
            }

            label5.Text = "Created Neural Network";
        }

        int c = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            double[] inputs = new double[4];
            double[] desiredOutputs = new double[16];
            
            for(int epoch = 0; epoch < 100; epoch++)
            {
                for (int i = 0; i < 16; i++)
                {
                    inputs[0] = (i & 1) > 0 ? 1.0 : 0.0;
                    inputs[1] = (i & 2) > 0 ? 1.0 : 0.0;
                    inputs[2] = (i & 4) > 0 ? 1.0 : 0.0;
                    inputs[3] = (i & 8) > 0 ? 1.0 : 0.0;
                    desiredOutputs[i] = i == 15 ? 1.0 : 0.0;

                    Nn.setInputs(inputs);
                    Nn.setDesiredOutput(0, desiredOutputs[i]);
                    Nn.learn();
                }
                c++;
            }

            label5.Text = $"Trained for {c} epochs!";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                double input1 = Convert.ToDouble(textBox1.Text);
                double input2 = Convert.ToDouble(textBox2.Text);
                double input3 = Convert.ToDouble(textBox3.Text);
                double input4 = Convert.ToDouble(textBox4.Text);

                Validate(input1);
                Validate(input2);
                Validate(input3);
                Validate(input4);

                Nn.setInputs(0, input1);
                Nn.setInputs(1, input2);
                Nn.setInputs(2, input3);
                Nn.setInputs(3, input4);
                Nn.run();

                textBox8.Text = Nn.getOuputData(0).ToString("0.000000000000");
              
                label5.Text = "Successful logic AND!";
                timer1.Enabled = true;
                timer1.Start();
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid input. Please enter valid numbers.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void Validate(double input)
        {
            if (input != 0 && input != 1) throw new ArgumentException("Invalid input");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label5.Text = " ";
            timer1.Stop();
        }
    }
}
