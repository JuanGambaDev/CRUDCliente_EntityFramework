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

namespace CrudCliente2.Presentacion
{
    public partial class FormTabla : Form
    {
        public int? id;
        ClienteTabla oTabla = null;
        public FormTabla(int? id=null)
        {
            InitializeComponent();

            this.id = id;
            if (id != null)
            {
                CargaDatos();
            }
        }

        private void CargaDatos()
        {
            using (ClienteDataEntities db = new ClienteDataEntities())
            {
                oTabla = db.ClienteTabla.Find(id);
                txtNombres.Text = oTabla.Nombres;
                txtApellidos.Text = oTabla.Apellidos;
                txtDireccion.Text = oTabla.Direccion;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (ClienteDataEntities db = new ClienteDataEntities())
            {
                if (id == null)
                    oTabla = new ClienteTabla();

                oTabla.Nombres = txtNombres.Text;
                oTabla.Apellidos = txtApellidos.Text;
                oTabla.Direccion = txtDireccion.Text;

                if (txtNombres.Text == string.Empty)
                {
                    MessageBox.Show("Ingrese datos en el campo Nombres!");
                    return;
                }
                else if (txtApellidos.Text == string.Empty)
                {
                    MessageBox.Show("Ingrese datos en el campo Apellidos");
                    return;
                }
                else if (txtDireccion.Text == string.Empty)
                {
                    MessageBox.Show("Ingrese datos en el campo Direccion");
                    return;
                }
                else
                {
                    if (id == null)
                    {
                        db.ClienteTabla.Add(oTabla);

                    }
                    else
                    {
                        db.Entry(oTabla).State = System.Data.Entity.EntityState.Modified;
                    }
                    db.SaveChanges();

                    this.Close();
                }

                
            }
        }

        private void FormTabla_Load(object sender, EventArgs e)
        {

        }
    }
}
