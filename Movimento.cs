using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finanças_Pessoais
{
    class Movimento
    {
        public string Id { get; set; }
        public string Valor { get; set; }
        public string Moeda { get; set; }
        public string Transacao { get; set; }
        public string Meio { get; set; }
        public string Data { get; set; }
        public string Hora { get; set; }
        public char Tipo { get; set; }
        public bool Ponte { get; set; }
        public string DescOriDes { get; set; }
    }
}