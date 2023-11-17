using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Segundo_Parcial_TP4
{
    public partial class Archivos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string nombreCarpeta = Session["usuario"].ToString();
            Directory.CreateDirectory(Server.MapPath(nombreCarpeta));
            try
            {
                if (!IsPostBack)
                {
                    CargarArchivos();
                }
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                Directory.CreateDirectory(Server.MapPath(nombreCarpeta));
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
           
            string nombreCarpeta = Session["usuario"].ToString();
            string directorioAlmacenamiento = $"{Server.MapPath(nombreCarpeta)}";

            string rutaCarpeta = Path.Combine(directorioAlmacenamiento, nombreCarpeta);

            if (Directory.Exists(rutaCarpeta))
            {
                this.Label2.Text = rutaCarpeta;
                //FileUpload1.SaveAs(directorioAlmacenamiento)/ { FileUpload1.FileName});
                FileUpload1.SaveAs($"{Server.MapPath(nombreCarpeta)}/{FileUpload1.FileName}");
                CargarArchivos();
            }
            else
            {
                Directory.CreateDirectory( Server.MapPath(nombreCarpeta));
                FileUpload1.SaveAs($"{Server.MapPath(nombreCarpeta)}/{ FileUpload1.FileName}");
                GridView1.DataBind();
                CargarArchivos();
            }
        }

        private void CargarArchivos()
        {
            string nombreCarpeta = Session["usuario"].ToString();
            //string directorioAlmacenamiento = $"{Server.MapPath(nombreCarpeta)}";
            string carpeta = Server.MapPath(nombreCarpeta); // Ruta de la carpeta donde se almacenan los archivos
            string[] archivos = Directory.GetFiles(carpeta);

            GridView1.DataSource = archivos.Select(Path.GetFileName).ToList();
            GridView1.DataBind();
        }

        protected void GridViewArchivos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Descargar")
            {
                string nombreArchivo = e.CommandArgument.ToString();
                DescargarArchivo(nombreArchivo);
            }
        }

        protected void DescargarArchivo(string nombreArchivo)
        {
            string nombreCarpeta = Session["usuario"].ToString();
            string carpeta = Server.MapPath(nombreCarpeta);
            string rutaCompleta = Path.Combine(carpeta, nombreArchivo);

            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + nombreArchivo);
            Response.TransmitFile(rutaCompleta);
            Response.End();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DescargarArchivo("nombreArchivo");
            this.Label3.Text = "Aca estoy";
 
        }
    }
}