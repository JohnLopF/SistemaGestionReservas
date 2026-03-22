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
                //Validar selección de tipo
                if (cmbTipo.SelectedItem == null)
                    throw new Exception("Debe seleccionar un tipo de habitación.");

                //Determinar tipo de instancia
                Reserva nueva;
                if (cmbTipo.SelectedItem.ToString() == "VIP")
                    nueva = new HabitacionVIP();
                else
                    nueva = new HabitacionEstandar();

                //Mapear datos desde la interfaz
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

                //Decidir si es Registro Nuevo o Edición
                if (string.IsNullOrEmpty(documentoEdicion))
                {
                    admin.RegistrarReserva(nueva);
                    MessageBox.Show("Reserva guardada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    admin.EditarReserva(documentoEdicion, nueva);
                    MessageBox.Show("Reserva actualizada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Resetear estado de edición
                    documentoEdicion = "";
                    btnGuardar.Text = "Registrar Reserva";                    
                }

                ActualizarPantalla();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                //Manejo de excepciones (Objetivo 5 de la práctica)
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
                dgvReservas.Columns["CostoTotal"].HeaderText = "Costo Final";
                dgvReservas.Columns["CostoTotal"].DefaultCellStyle.Format = "C0";
            }
        }
        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtDocumento.Clear();
            txtHabitacion.Clear();
            txtTarifa.Clear();
            numNoches.Value = 1;
            dtpFecha.Value = DateTime.Now;
            cmbTipo.SelectedIndex = 0;
            documentoEdicion = "";
            btnGuardar.Text = "Registrar Reserva";
            txtDocumento.Enabled = true;
        }

        private void dgvReservas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //Obtenemos la reserva seleccionada directamente
                var reservaSel = (Reserva)dgvReservas.Rows[e.RowIndex].DataBoundItem;

                //Cargar los datos en los controles
                txtNombre.Text = reservaSel.NombreCliente;
                txtDocumento.Text = reservaSel.DocumentoCliente;
                txtHabitacion.Text = reservaSel.NumeroHabitacion.ToString();
                numNoches.Value = reservaSel.DuracionEstadia;
                txtTarifa.Text = reservaSel.TarifaNoche.ToString();
                dtpFecha.Value = reservaSel.FechaReserva;
                cmbTipo.SelectedItem = reservaSel is HabitacionVIP ? "VIP" : "Estandar";
                
                documentoEdicion = reservaSel.DocumentoCliente;
                btnGuardar.Text = "Actualizar Cambios";
                
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            dgvReservas.DataSource = null;
            dgvReservas.DataSource = admin.BuscarPorNombre(txtBuscar.Text);
            FormatearColumnas();
        }
    }
}
