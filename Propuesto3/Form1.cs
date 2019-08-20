using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Propuesto3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dgPedidos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["kobu"].ConnectionString);

        public void ListaAnios()
        {
            using (SqlCommand cmd = new SqlCommand("Usp_ListaAnios", cn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    using (DataSet df = new DataSet())
                    {
                        da.Fill(df, "ListaAnios");
                        cbxAnio.DataSource = df.Tables["ListaAnios"];
                        cbxAnio.DisplayMember = "Anios";
                        cbxAnio.ValueMember = "Anios";
                    }
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ListaAnios();
        }

        private void cbxAnio_SelectionChangeCommitted(object sender, EventArgs e)
        {
            using (SqlCommand cmd = new SqlCommand("Usp_ListaMeses", cn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@anio", cbxAnio.SelectedValue);
                    using (DataSet df = new DataSet())
                    {
                        da.Fill(df, "ListaMeses");
                        cbxMes.DataSource = df.Tables["ListaMeses"];
                        cbxMes.DisplayMember = "Mes";
                        cbxMes.ValueMember = "Mes";
                    }
                }
            }

        }

        private void cbxMes_SelectionChangeCommitted(object sender, EventArgs e)
        {
            using (SqlCommand cmd = new SqlCommand("Usp_Detalle_Ped_Mes_Anio", cn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@anio", cbxAnio.SelectedValue);
                    da.SelectCommand.Parameters.AddWithValue("@mes", cbxMes.SelectedValue);
                    using (DataSet df = new DataSet())
                    {
                        da.Fill(df, "Pedidos");
                        dgPedidos.DataSource = df.Tables["Pedidos"];
                    }
                }
            }
        }
    }
}
