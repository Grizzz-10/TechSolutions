// CAPA PRESENTACIÓN – productos e inventario
using System;
using System.Windows.Forms;
using Entidades;
using Negocio;

namespace Presentacion
{
    public partial class FrmProducto : Form
    {
        private readonly ProductoBLL bll = new ProductoBLL();

        public FrmProducto()
        {
            InitializeComponent();
        }

        private void FrmProducto_Load(object sender, EventArgs e)
        {
            Cargar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                bll.Registrar(new Producto
                {
                    Codigo = txtCodigo.Text,
                    Nombre = txtNombre.Text,
                    Precio = nudPrecio.Value,
                    Stock = (int)nudStock.Value
                });
                txtCodigo.Clear(); txtNombre.Clear(); nudPrecio.Value = 0; nudStock.Value = 0;
                Mensajes.Info("Producto registrado.");
                Cargar();
            }
            catch (Exception ex)
            {
                Mensajes.Mostrar(ex);
            }
        }

        private void btnIngresarStock_Click(object sender, EventArgs e)
        {
            try
            {
                var p = dgvProductos.CurrentRow == null ? null : dgvProductos.CurrentRow.DataBoundItem as Producto;
                if (p == null) throw new NegocioException("Seleccione un producto de la lista.");

                bll.IngresarStock(p.Id, (int)nudIngreso.Value);
                Cargar();
                Mensajes.Info("Stock actualizado.");
            }
            catch (Exception ex)
            {
                Mensajes.Mostrar(ex);
            }
        }

        private void chkStockBajo_CheckedChanged(object sender, EventArgs e)
        {
            Cargar();
        }

        private void Cargar()
        {
            try
            {
                var lista = bll.Listar();
                if (chkStockBajo.Checked)
                    lista = bll.StockBajo(lista, ProductoBLL.UmbralStockBajo);   // filtro con LINQ (en Negocio)
                dgvProductos.DataSource = lista;
                if (dgvProductos.Columns["Precio"] != null)
                    dgvProductos.Columns["Precio"].DefaultCellStyle.Format = "N2";
            }
            catch (Exception ex)
            {
                Mensajes.Mostrar(ex);
            }
        }
    }
}
