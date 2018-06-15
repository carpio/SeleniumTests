using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace SeleniumTests
{
    public class PruebasDeslizarProductosConBotones
    {
        IWebDriver _driver;

        [SetUp]
        public void InicioDePruebas()
        {
            _driver = new FirefoxDriver();
            _driver.Url = "http://store.demoqa.com";
        }

        [Test]
        public void DeslizamientoDeBotones_Prueba()
        {
            //a. Obtener contenedores de botones y de productos
            var contenedorBotones = _driver.FindElement(By.Id("slide_menu"));
            var contenedorProductos = _driver.FindElement(By.Id("slides"));
            //b.Obtener el arreglo de botones por la lista
            var botones = contenedorBotones.FindElements(By.TagName("a"));
            //c.Obtener el arreglo de productos en el carroussel
            var productos = contenedorProductos.FindElements(By.ClassName("slide"));
            //d.Verificar(Verify) que el arreglo de botones no esté vacío
            //e.Verificar que el arreglo de productos no esté vacío
            if (botones.Count > 0 && productos.Count > 0)
            {
                //f.Hacer loop(iterar) el arreglo de botones para darle clic
                //g.Verificar que el item seleccionado coincida con el que se hizo clic
                for(int i = 0; i < botones.Count; i++)
                {
                    var boton = botones[i];
                    var producto = productos[i];

                    botones[i].Click();
                    Thread.Sleep(1000);

                    Console.WriteLine("Producto Displayed " + (i + 1) + "?: " + producto.Displayed);
                }
            }
            else
                Assert.Fail();

            //h.Asertar que al darle clic al último elemento del arreglo botones coincida con el último elemento de arreglo de productos
            var ultimoLugarIndice = botones.Count - 1;

            botones[ultimoLugarIndice].Click();
            Thread.Sleep(1000);

            Assert.That(productos[ultimoLugarIndice].Displayed);
        }

        [Test]
        public void ItemConClicNoCoincide_Prueba()
        {
            //a. Obtener contenedores de botones y productos
            var lista = _driver.FindElement(By.Id("slide_menu"));
            var contenedorDeSlides = _driver.FindElement(By.Id("slides"));

            //b.Obtener el arreglo de botones por la lista
            //c.Obtener el arreglo de productos en el carroussel
            var botones = lista.FindElements(By.TagName("a"));
            var productos = contenedorDeSlides.FindElements(By.ClassName("slide"));

            //d.Verificar(Verify) que el arreglo de botones no esté vacío
            //e.Verificar que el arreglo de productos no esté vacío
            if (botones.Count > 0 && productos.Count > 0)
            {
                Console.WriteLine("Botones y productos no son nulos");
                //h.Asertar que al darle clic al último elemento del arreglo botones no se muestre el primer producto
                var ultimoIndice = botones.Count - 1;
                botones[ultimoIndice].Click();
                Thread.Sleep(1000);
                Assert.That(productos[0].Displayed == false);
            }
            else
                Assert.Fail();
        }

        [TearDown]
        public void FinDePruebas()
        {
            _driver.Quit();
        }
    }
}
