using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ejerpeliculas.models;
using Xamarin.Forms;

namespace ejerpeliculas
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("ejerpeliculas.fichero.Novedades.xml");
            string contenido = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                contenido = reader.ReadToEnd();
            }

            XDocument docxml = XDocument.Parse(contenido);
            var consulta2 = from datos in docxml.Descendants("TITULOS")
                           select new
                           {
                               peli = datos.Attribute("PELICULA").Value
                               
                         

                           };

            foreach (var dato in consulta2)
            {
                this.controlpicker.Items.Add(dato.peli);
            }

            this.controlpicker.SelectedIndexChanged += (sender, args) =>
            {
               
                var consulta = from datos in docxml.Descendants("TITULOS")
                               where datos.Attribute("PELICULA").Value == controlpicker.SelectedItem.ToString()
                               select new
                               {
                                   imagen = datos.Element("IMAGEN").Value
                                   ,
                                   director = datos.Element("DIRECTOR").Value
                                   ,
                                   distribuidor = datos.Element("DISTRIBUIDOR").Value
                                   ,
                                   precio = int.Parse(datos.Element("PRECIO").Value)
                                   ,
                                   actores = datos.Element("ACTORES").Value

                               };
                distribuidor.Text = consulta.First().distribuidor;
                precio.Text = consulta.First().precio.ToString();
                director.Text = consulta.First().director.ToString();
                actores.Text = consulta.First().actores.ToString();
                imgPeli.Source = consulta.First().imagen.ToString();
            };

          


        }




    }
}
