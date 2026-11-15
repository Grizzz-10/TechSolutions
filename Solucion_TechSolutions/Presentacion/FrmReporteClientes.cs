// CAPA PRESENTACIÓN: pide el nombre, llama a Negocio y muestra el reporte. No tiene SQL.
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;      // ReportViewer, ReportDataSource, ReportParameter (paquete NuGet)
using Entidades;
using Negocio;

namespace Presentacion
{
    public partial class FrmReporteClientes : Form
    {
        private readonly ReporteClienteBLL bll = new ReporteClienteBLL();

        public FrmReporteClientes()
        {
            InitializeComponent();
        }

        // Al abrir la pantalla se muestra el reporte completo (todos los clientes).
        private void FrmReporteClientes_Load(object sender, EventArgs e)
        {
            btnVer_Click(this, EventArgs.Empty);
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            try
            {
                // 1) DATOS: Presentación -> Negocio -> Datos -> procedimiento almacenado
                List<Cliente> datos = bll.Reporte(txtNombre.Text);

                // 2) DISEÑO: el reporte que está dentro del .exe
                reportViewer1.LocalReport.ReportEmbeddedResource = "Presentacion.ReporteClientes.rdlc";

                // 3) Entregar DATOS y PARÁMETRO (mismos nombres que en el .rdlc)
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", datos));
                string filtro = txtNombre.Text.Trim() == "" ? "Todos" : txtNombre.Text.Trim();
                reportViewer1.LocalReport.SetParameters(new ReportParameter("Filtro", filtro));

                // 4) MOSTRAR (desde el visor se imprime o se exporta a PDF)
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)       // regla de Negocio, sin permiso o sin conexión (Mensajes decide qué se muestra)
            {
                Mensajes.Mostrar(ex);
            }
        }
    }
}
