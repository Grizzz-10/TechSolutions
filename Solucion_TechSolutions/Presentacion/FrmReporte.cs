// CAPA PRESENTACIÓN – reporte de ventas por rango de fechas (solo Administrador)
// Vistas: detalle, o resúmenes agrupados con LINQ (por producto, vendedor, cliente). Permite anular una venta.
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Entidades;
using Negocio;

namespace Presentacion
{
    public partial class FrmReporte : Form
    {
        private readonly VentaBLL bll = new VentaBLL();
        private List<VentaReporte> ventas = new List<VentaReporte>();   // última consulta a la BD

        public FrmReporte()
        {
            InitializeComponent();
        }

        private void FrmReporte_Load(object sender, EventArgs e)
        {
            cboVista.Items.Add("Detalle");
            foreach (string a in VentaBLL.Agrupaciones) cboVista.Items.Add("Por " + a.ToLower());
            cboVista.SelectedIndex = 0;

            dtpDesde.Value = DateTime.Today.AddDays(-30);
            dtpHasta.Value = DateTime.Today;
            Generar();
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            Generar();
        }

        // Cambiar de vista NO vuelve a consultar la BD: reagrupa en memoria con LINQ.
        private void cboVista_SelectedIndexChanged(object sender, EventArgs e)
        {
            Mostrar();
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            try
            {
                var v = dgvVentas.CurrentRow == null ? null : dgvVentas.CurrentRow.DataBoundItem as VentaReporte;
                if (v == null) throw new NegocioException("Cambie a la vista \"Detalle\" y seleccione una venta.");
                if (v.Anulada) throw new NegocioException("Esa venta ya está anulada.");

                DialogResult r = MessageBox.Show(
                    "¿Anular la venta N.º " + v.IdVenta + " (" + v.Producto + " x " + v.Cantidad + ")?\nSe devolverá el stock.",
                    "Confirmar anulación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r != DialogResult.Yes) return;

                bll.Anular(v.IdVenta);          // transacción: anula + devuelve stock (COMMIT) o no hace nada (ROLLBACK)
                Mensajes.Info("Venta anulada y stock devuelto.");
                Generar();
            }
            catch (Exception ex)
            {
                Mensajes.Mostrar(ex);
            }
        }

        private void Generar()
        {
            try
            {
                ventas = bll.Reporte(dtpDesde.Value, dtpHasta.Value);
                Mostrar();
            }
            catch (Exception ex)
            {
                Mensajes.Mostrar(ex);
            }
        }

        private void Mostrar()
        {
            try
            {
                if (cboVista.SelectedIndex <= 0)
                {
                    dgvVentas.DataSource = ventas;
                    Formato("PrecioUnitario"); Formato("Total");
                }
                else
                {
                    dgvVentas.DataSource = bll.Resumir(ventas, VentaBLL.Agrupaciones[cboVista.SelectedIndex - 1]);
                    Formato("Total");
                }

                int vigentes = 0, anuladas = 0;
                foreach (VentaReporte v in ventas) { if (v.Anulada) anuladas++; else vigentes++; }
                lblResumen.Text = "Vigentes: " + vigentes + "  |  Anuladas: " + anuladas +
                                  "  |  Total vigente: S/ " + bll.TotalVigente(ventas).ToString("N2");
            }
            catch (Exception ex)
            {
                Mensajes.Mostrar(ex);
            }
        }

        private void Formato(string columna)
        {
            if (dgvVentas.Columns[columna] != null)
                dgvVentas.Columns[columna].DefaultCellStyle.Format = "N2";
        }
    }
}
