using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Threading;

namespace SeleniumTests
{
    public class ProductosMostradosPorCategorias_Pruebas
    {
        IWebDriver _driver;
        [SetUp]
        public void InicioPruebas()
        {
            _driver = new FirefoxDriver();
            _driver.Url = "http://store.demoqa.com";
        }

        [Test]
        public void ClicOpcionCoincideConTituloMostrado_Prueba()
        {
            //a.Obtener la lista que contiene las opciones del menú
            
            var listaOpciones = 
                _driver.FindElements(
                    By.XPath("//*[@id='menu-item-33']/ul/li")
                    );

            var opcionProductCategory =
                _driver.FindElement(By.Id("menu-item-33"));

            var cantidadDeOpciones = listaOpciones.Count;
            //b.Hacer hover sobre Product Category

            var accion = new Actions(_driver);
            
            // Iterar para dar clic en cada opcion obtenida
            for(int i = 0; i < cantidadDeOpciones; i++)
            {
                //Leer el H1 dentro de la etiqueta header. 
                //Encontrar el H1 con la ruta: //*[@id="content"]/article/header/h1            
                //var h1 = _driver.FindElement(By.XPath("//*[@id='content']/article/header/h1"));

                //d.Verificar que el texto de la etiqueta H1 
                //sea igual al texto de la opción a la que se dio clic.
                //•	Ejemplo: Product Category -> Opción: Accessories->H1 texto debe ser igual a Accessories

                accion = new Actions(_driver);

                listaOpciones = _driver.FindElements(
                    By.XPath("//*[@id='menu-item-33']/ul/li")
                    );

                opcionProductCategory =
                _driver.FindElement(By.Id("menu-item-33"));

                accion.MoveToElement(opcionProductCategory).Perform();
                Thread.Sleep(1000);

                var opcion = listaOpciones[i];

                var textoOpcion = opcion.FindElement(
                    By.TagName("a")).Text;
       
                accion.MoveToElement(opcion).Perform();
                Thread.Sleep(1000);

                opcion.Click();
                Thread.Sleep(1000);

                var h1 = _driver.FindElement(
                    By.XPath("//*[@id='content']/article/header/h1")
                    );

                Console.WriteLine(textoOpcion + " es igual a " + h1.Text);

                _driver.Navigate().Back();
            }

            //e.Asertar que al darle clic a la última opción del menú 
            //el texto del H1 y el texto de la opción coincidan
            //•	Obtener el último índice del arreglo de opciones 
            //y el texto del H1

            var ultimoIndice = cantidadDeOpciones - 1;

            listaOpciones = _driver.FindElements(
                    By.XPath("//*[@id='menu-item-33']/ul/li")
                    );

            accion = new Actions(_driver);

            opcionProductCategory =
                _driver.FindElement(By.Id("menu-item-33"));

            accion.MoveToElement(opcionProductCategory).Perform();

            var ultimaOpcion = listaOpciones[ultimoIndice];

            Thread.Sleep(1000);

            accion.MoveToElement(ultimaOpcion).Perform();

            Thread.Sleep(1000);

            var textoUltimaOpcion = ultimaOpcion.FindElement(By.TagName("a")).Text;
            ultimaOpcion.Click();

            Thread.Sleep(1000);

            var h1Final = _driver.FindElement(
                    By.XPath("//*[@id='content']/article/header/h1")
                    );
            
            Assert.That(textoUltimaOpcion == h1Final.Text);
        }

        [TearDown]
        public void FinPruebas()
        {
            _driver.Quit();
        }
    }
}
