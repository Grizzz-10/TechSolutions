// CAPA PRESENTACIÓN – clientes (referencia solo a Entidades y Negocio, NO a Datos)
using System;
using System.Windows.Forms;
using Entidades;
using Negocio;

namespace Presentacion
{
    public partial class FrmCliente : Form
    {
        private readonly ClienteBLL bll = new ClienteBLL();

        public FrmCliente()
        {
            InitializeComponent();
        }

        private void FrmCliente_Load(object sender, EventArgs e)
        {
            Cargar(string.Empty);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                bll.Registrar(new Cliente
                {
                    Documento = txtDocumento.Text,
                    Nombre = txtNombre.Text,
                    Correo = txtCorreo.Text,
                    Telefono = txtTelefono.Text
                });
                txtDocumento.Clear(); txtNombre.Clear(); txtCorreo.Clear(); txtTelefono.Clear();
                Mensajes.Info("Cliente registrado.");
                Cargar(txtBuscar.Text);
            }
            catch (Exception ex)
            {
                Mensajes.Mostrar(ex);
            }
        }

        // Reporte de clientes: usa un procedimiento almacenado (usp_ReporteClientes) y el visor de reportes.
        private void btnReporte_Click(object sender, EventArgs e)
        {
            using (var f = new FrmReporteClientes())
                f.ShowDialog();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Cargar(txtBuscar.Text);
        }

        private void Cargar(string texto)
        {
            try
            {
                dgvClientes.DataSource = bll.Buscar(texto);
            }
            catch (Exception ex)
            {
                Mensajes.Mostrar(ex);
            }
        }
    }
}
