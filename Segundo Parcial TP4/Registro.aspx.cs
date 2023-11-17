using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Segundo_Parcial_TP4
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = new HttpCookie("contraseña", this.TextBox4.Text);
            this.Response.Cookies.Add(cookie);
            this.Label7.Text = "La cookie contraseña tiene el valor: " + Request.Cookies["contraseña"].Value;
            this.Session["usuario"] = this.TextBox2.Text;
            Label6.Text = "Variable de session tiene el valor:" + Session["usuario"].ToString();
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
        }
    }
}