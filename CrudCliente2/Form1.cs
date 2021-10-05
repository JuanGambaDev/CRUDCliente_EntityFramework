using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrudCliente2.Models;

namespace CrudCliente2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Refrescar();
        }

        #region HELPER
        public void Refrescar()
        {
            using (ClienteDataEntities db = new ClienteDataEntities())
            {
                var lst = from d in db.ClienteTabla
                          select d;
                dataGridView1.DataSource = lst.ToList();
            }
        }

        private int? GetId()
        {
            try
            {
                return int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
            }
            catch
            {
                return null;
            }
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            Presentacion.FormTabla oFormTabla = new Presentacion.FormTabla();
            oFormTabla.ShowDialog();

            Refrescar();
        }

        private void buttonEditar_Click(object sender, EventArgs e)
        {
            int? id = GetId();
            if (id != null)
            {
                Presentacion.FormTabla oFormTabla = new Presentacion.FormTabla(id);
                oFormTabla.ShowDialog();

                Refrescar();
                
            }
        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {

            const string message = "Esta seguro de eliminar el cliente de la base de datos?";
            const string caption = "Eliminar Cliente";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            // Si el boton "si" es presionado ...
            if (result == DialogResult.Yes)
            {
                // Elimina el usuario de la Base de datos
                int? id = GetId();
                if (id != null)
                {
                    using (ClienteDataEntities db = new ClienteDataEntities())
                    {
                        ClienteTabla oTabla = db.ClienteTabla.Find(id);
                        db.ClienteTabla.Remove(oTabla);

                        db.SaveChanges();

                    }
                    Refrescar();
                }

            }

            

        }
    }
}
