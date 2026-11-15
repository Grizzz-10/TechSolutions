// CAPA PRESENTACIÓN – registrar una venta
using System;
using System.Windows.Forms;
using Entidades;
using Negocio;

namespace Presentacion
{
    public partial class FrmVenta : Form
    {
        private readonly VentaBLL ventas = new VentaBLL();
        private readonly ClienteBLL clientes = new ClienteBLL();
        private readonly ProductoBLL productos = new ProductoBLL();

        public FrmVenta()
        {
            InitializeComponent();
        }

        private void FrmVenta_Load(object sender, EventArgs e)
        {
            try
            {
                cboCliente.DisplayMember = "Descripcion";
                cboCliente.ValueMember = "Id";
                cboCliente.DataSource = clientes.Listar();
                CargarProductos();
            }
            catch (Exception ex)
            {
                Mensajes.Mostrar(ex);
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                var c = cboCliente.SelectedItem as Cliente;
                var p = cboProducto.SelectedItem as Producto;
                if (c == null) throw new NegocioException("Seleccione un cliente.");
                if (p == null) throw new NegocioException("Seleccione un producto.");

                ventas.Registrar(c.Id, p.Id, (int)nudCantidad.Value);   // el precio lo pone la BD; el vendedor, la sesión
                Mensajes.Info("Venta registrada.");
                nudCantidad.Value = 1;
                CargarProductos();                                      // refresca el stock mostrado
            }
            catch (NegocioException ex)
            {
                Mensajes.Mostrar(ex);
                CargarProductos();                                      // por si el stock cambió mientras tanto
            }
            catch (Exception ex)
            {
                Mensajes.Mostrar(ex);
            }
        }

        // Solo informativo: el total real lo calcula la BD con el precio vigente al registrar.
        private void Calcular(object sender, EventArgs e)
        {
            var p = cboProducto.SelectedItem as Producto;
            decimal total = p == null ? 0m : p.Precio * nudCantidad.Value;
            lblTotal.Text = "Total estimado: S/ " + total.ToString("N2");
        }

        private void CargarProductos()
        {
            try
            {
                cboProducto.DisplayMember = "Descripcion";
                cboProducto.ValueMember = "Id";
                cboProducto.DataSource = productos.Listar();
                Calcular(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Mensajes.Mostrar(ex);
            }
        }
    }
}
