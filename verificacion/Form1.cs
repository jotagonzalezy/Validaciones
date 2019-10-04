using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace verificacion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Limpiar();
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            var clase = new ValidacionClass();
            var resp = clase.ValidaRut(txtRut.Text, txtdv.Text);
            if (resp)
                MessageBox.Show(@"Correcto", @"Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                MessageBox.Show(@"Incorrecto", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            var random = new Random();
            var a = Convert.ToDouble(random.Next(10000000, 18000000));
            txtRut.Text = a.ToString(CultureInfo.InvariantCulture);
            txtdv.Text = "";
        }

        private void btnExtraeInfo_Click(object sender, EventArgs e)
        {
            Limpiar();

            var clase = new ValidacionClass();
            var valid = clase.Validar10(txtTarjeta.Text);
            if (valid)
            {
                lblValidacion.Text = @"Valida";
                var resp = clase.ExtraeData(txtTarjeta.Text);
                lbldv.Text = resp.Dv;
                lblCuenta.Text = resp.Cuenta;
                lblTipo.Text = resp.Tipo;
                lblIndustria.Text = resp.Industria;
                lblBanco.Text = resp.Banco;
            }
            else
            {
                lblValidacion.Text = @"Invalida";
            }
        }

        private void Limpiar()
        {
            lbldv.Text = "";
            lblCuenta.Text = "";
            lblTipo.Text = "";
            lblIndustria.Text = "";
            lblBanco.Text = "";
            lblValidacion.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var clase = new ValidacionClass();
            var resp = clase.ValidaSscc(txtsscc.Text);
            if (resp)
                MessageBox.Show(@"Correcto", @"Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                MessageBox.Show(@"Incorrecto", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var clase = new ValidacionClass();
            var resp = clase.ValidaGtin13(txtgtin13.Text);
            if (resp)
                MessageBox.Show(@"Correcto", @"Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                MessageBox.Show(@"Incorrecto", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var clase = new ValidacionClass();
            var resp = clase.ValidaGtin14(txtGtin14.Text);
            if (resp)
                MessageBox.Show(@"Correcto", @"Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                MessageBox.Show(@"Incorrecto", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void txtGtin14_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            var clase = new ValidacionClass();
            var resp = clase.ValidaContenedor(txtContenedor.Text);
            if (resp)
                MessageBox.Show(@"Correcto", @"Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                MessageBox.Show(@"Incorrecto", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
