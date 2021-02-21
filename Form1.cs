using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Finanças_Pessoais
{
    public partial class Form1 : Form
    {
        List<Movimento> movimentos;
        public Form1()
        {
            InitializeComponent();
            movimentos = new List<Movimento>();
            cMoeda.Items.Add("R$");
            cTransacao.Items.Add("A vista");
            cTransacao.Items.Add("Parcelada");
            cMeio.Items.Add("Dinheiro em espécie");
            cMeio.Items.Add("Cartão de crédito");
            cMeio.Items.Add("Cartão de débito");
            cMeio.Items.Add("Cheque");
            cMeio.Items.Add("Boleto bancário");
            cMeio.Items.Add("Depósito bancário");
            cMeio.Items.Add("Transferência bancária");
            cMeio.Items.Add("Outro");

            cMoeda.SelectedIndex = 0;
            cTransacao.SelectedIndex = 0;
            cMeio.SelectedIndex = 6;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void bIncluir_Click(object sender, EventArgs e)
        {
            int index = -1;
            string id = DateTime.Parse(cData.Text + " " + cHora.Text).ToString("ddMMyyyyHHmmss");
            foreach (Movimento mid in movimentos)
            {
                if (mid.Id == id)
                {
                    index = movimentos.IndexOf(mid);
                }
            }

            if (cValor.Text == "")
            {
                MessageBox.Show("Insira um valor para o movimento.");
                cValor.Focus();
                return;
            }

            char tipo;
            if (cPonte.Checked)
            {
                tipo = 'P';
            }
            else
            {
                if (rEntrada.Checked)
                {
                    tipo = 'E';
                }
                else
                {
                    tipo = 'S';
                }
            }    

            Movimento m = new Movimento();
            m.Id = id;
            m.Valor = string.Format("{0:#.00}", Convert.ToDecimal(cValor.Text) / 100).Replace(".", ",");
            m.Moeda = cMoeda.SelectedItem.ToString();
            m.Transacao = cTransacao.SelectedItem.ToString();
            m.Meio = cMeio.SelectedItem.ToString();
            m.Data = cData.Text;
            m.Hora = cHora.Text;
            m.Tipo = tipo;
            //m.Ponte = cValor.Text;
            m.DescOriDes = cDescOriDes.Text;

            if (index < 0)
            {
                movimentos.Add(m);
            }
            else
            {
                movimentos[index] = m;
            }
            bCancelar_Click(bCancelar, EventArgs.Empty);
            Listar();
            cValor.Focus();
        }

        private void bCancelar_Click(object sender, EventArgs e)
        {
            cValor.Text = "";
            cMoeda.SelectedIndex = 0;
            cTransacao.SelectedIndex = 0;
            cMeio.SelectedIndex = 6;
            cData.Text = "";
            cHora.Text = "";
            //rEntrada.Checked = true;
            //rSaida.Checked = false;
            cPonte.Checked = false;
            cDescOriDes.Text = "";
        }

        private void cValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cValor.Text == "") { cValor.Text = cValor.Text + ",00"; }
            //string cvlr = string.Format("{0:#.00}", Convert.ToDecimal("00" + cValor.Text) / 100);
            //cValor.Text += cvlr;
        }

        private void Listar()
        {
            vEntradas.Rows.Clear();
            vSaidas.Rows.Clear();
            dCaixa.Text = "0,00";
            dDiaE.Text = "0,00";
            dDiaS.Text = "0,00";
            foreach (Movimento m in movimentos)
            {
                Decimal Total;
                Decimal entradas;
                Decimal saidas;
                Decimal numa = Convert.ToDecimal(dCaixa.Text.Replace(",", "."));
                Decimal nume = Convert.ToDecimal(dDiaE.Text.Replace(",", "."));
                Decimal nums = Convert.ToDecimal(dDiaS.Text.Replace(",", "."));
                Decimal numb = Convert.ToDecimal(m.Valor.Replace(",", "."));
                if (m.Tipo == 'P')
                {
                    string[] rm = { m.Valor, m.Moeda, m.Transacao, m.Meio, m.Data, m.Hora, m.DescOriDes };
                    vEntradas.Rows.Add(rm);
                    vSaidas.Rows.Add(rm);
                    tab.SelectTab("tbSaidas");
                    Total = numa;
                    entradas = nume + numb;
                    saidas = nums - numb;
                }
                else
                {
                    if (m.Tipo == 'E')
                    {
                        string[] rm = { m.Valor, m.Moeda, m.Transacao, m.Meio, m.Data, m.Hora, m.DescOriDes };
                        vEntradas.Rows.Add(rm);
                        tab.SelectTab("tbEntradas");
                        Total = numa + numb;
                        entradas = nume + numb;
                        saidas = nums;
                    }
                    else
                    {
                        string[] rm = { m.Valor, m.Moeda, m.Transacao, m.Meio, m.Data, m.Hora, m.DescOriDes };
                        vSaidas.Rows.Add(rm);
                        tab.SelectTab("tbSaidas");
                        Total = numa - numb;
                        entradas = nume;
                        saidas = nums - numb;
                    }
                }
                dCaixa.Text = Total.ToString().Replace(".", ",");
                dDiaE.Text = entradas.ToString().Replace(".", ",");
                dDiaS.Text = saidas.ToString().Replace(".", ",");
            }
            dDiaS.Text = dDiaS.Text.Replace("-", "");
            dMesE.Text = dDiaE.Text;
            dAnoE.Text = dDiaE.Text;
            dMesS.Text = dDiaS.Text;
            dAnoS.Text = dDiaS.Text;
        }
    }
}
