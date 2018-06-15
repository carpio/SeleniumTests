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
            var lista = _driver.FindElements(By.XPath("//*[id='menu-item-33']/ul/li"));
            
            //b.Hacer hover sobre Product Category
            //c.Leer el H1 dentro de la etiqueta header. 
            //Encontrar el H1 con la ruta: //*[@id="content"]/article/header/h1
            
            //var h1 = _driver.FindElement(By.XPath("//*[@id='content']/article/header/h1"));

            //d.Verificar que el texto de la etiqueta H1 
            //sea igual al texto de la opción a la que se dio clic.
            //•	Ejemplo: Product Category -> Opción: Accessories->H1 texto debe ser igual a Accessories
            //e.Asertar que al darle clic a la última opción del menú 
            //el texto del H1 y el texto de la opción coincidan
            //•	Obtener el último índice del arreglo de opciones 
            //y el texto del H1

        }

        [TearDown]
        public void FinPruebas()
        {
            _driver.Quit();
        }
    }
}
