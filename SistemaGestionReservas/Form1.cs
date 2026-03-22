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
        private string documentoEdicion = "";
        public Form1()
        {
            InitializeComponent();
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            //Configuración inicial de controles
            cmbTipo.Items.Clear();
            cmbTipo.Items.AddRange(new string[] { "VIP", "Estandar" });
            cmbTipo.SelectedIndex = 0;
            dtpFecha.Value = DateTime.Now;

            //Estética del DataGridView
            dgvReservas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReservas.MultiSelect = false;
            dgvReservas.ReadOnly = true;
            dgvReservas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //Determinar tipo de habitación
                Reserva nueva;
                if (cmbTipo.SelectedItem.ToString() == "VIP")
                    nueva = new HabitacionVIP();
                else
                    nueva = new HabitacionEstandar();

                //Obtener datos de la interfaz
                nueva.NombreCliente = txtNombre.Text.Trim();
                nueva.DocumentoCliente = txtDocumento.Text.Trim();

                if (!int.TryParse(txtHabitacion.Text, out int nHab))
                    throw new Exception("El número de habitación debe ser numérico.");
                nueva.NumeroHabitacion = nHab;

                if (!double.TryParse(txtTarifa.Text, out double tarifa))
                    throw new Exception("La tarifa debe ser un valor numérico.");
                nueva.TarifaNoche = tarifa;

                nueva.DuracionEstadia = (int)numNoches.Value;
                nueva.FechaReserva = dtpFecha.Value;

                //Registrar mediante lógica
                admin.RegistrarReserva(nueva);
                
                MessageBox.Show("Reserva guardada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                MessageBox.Show("Error: " + ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            
        }

        private void ActualizarPantalla()
        {
            dgvReservas.DataSource = null;
            dgvReservas.DataSource = admin.ObtenerTodas();
            FormatearColumnas();
        }

        private void FormatearColumnas()
        {
            if (dgvReservas.Columns.Count > 0)
            {
                dgvReservas.Columns["NombreCliente"].HeaderText = "Cliente";
                dgvReservas.Columns["DocumentoCliente"].HeaderText = "Identificación";
                dgvReservas.Columns["NumeroHabitacion"].HeaderText = "Habitacion";
                dgvReservas.Columns["FechaReserva"].HeaderText = "Entrada";
                dgvReservas.Columns["DuracionEstadia"].HeaderText = "Noches";
                dgvReservas.Columns["TarifaNoche"].HeaderText = "Tarifa p/n";
                dgvReservas.Columns["CostoTotal"].HeaderText = "Costo Total";

                //Formato de Moneda
                dgvReservas.Columns["TarifaNoche"].DefaultCellStyle.Format = "C0";
                dgvReservas.Columns["CostoTotal"].DefaultCellStyle.Format = "C0";
            }
        }        

    }
}
