using SistemaGestionReservas.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaGestionReservas
{
    public partial class Form1 : Form
    {
        private AdministradorHotel admin = new AdministradorHotel();
        public Form1()
        {
            InitializeComponent();
        }
    }
}
