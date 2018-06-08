using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.IO;

namespace SeleniumTests
{
    class NUnitTest
    {
        //Declarando una instancia de IWebDriver global para toda la clase NUnitTests
        IWebDriver _myDriver;
        //IWebDriver _mySecondDriver;

        [SetUp]
        public void Initialize()
        {
            _myDriver = new FirefoxDriver();
            //_mySecondDriver = new FirefoxDriver();
        }

        [Test]
        public void ComandoTitlePrueba()
        {
            _myDriver.Url = "https://accounts.google.com";
            //var es una palabra reservada para declarar
            //una variable sin especificar su tipo en la declaración
            var miTitulo = _myDriver.Title;
            Console.WriteLine("El titulo de la página es: " + miTitulo);
        }

        [Test]
        public void OpenAppPrintTitleTest()
        {
            _myDriver.Url = "http://www.facebook.com";
            var myTitle = _myDriver.Title;
            Console.WriteLine(myTitle);
        }

        [Test]
        public void EjemploPageSourcePrueba()
        {
            _myDriver.Url = "http://demoqa.com";
            var pageSourceDemoQa = _myDriver.PageSource;
            Console.WriteLine("El código fuente de la página es: " + pageSourceDemoQa);
        }

        [Test]
        public void AbrirPestanaPrueba()
        {
            _myDriver.Url = "http://demoqa.com/frames-and-windows/";
            var link = _myDriver.FindElement(By.XPath(".//*[@id='tabs-1']/div/p/a"));
            link.Click();
        }

        [Test]
        public void QuitCommandTest()
        {
            _myDriver.Url = "http://demoqa.com/frames-and-windows/";
            _myDriver.FindElement(By.XPath(".//*[@id='tabs-1']/div/p/a")).Click();

        }

        [Test]
        public void GoToUrlCommandTest()
        {
            _myDriver.Url = "http://www.facebook.com";
            //_mySecondDriver.Url = "https://www.twitter.com";
            _myDriver.Navigate().GoToUrl("https://accounts.google.com");
            _myDriver.Navigate().Back();
            _myDriver.Navigate().Forward();
            _myDriver.Navigate().Refresh();
        }

        /// <summary>
        ///Web Elements Commands
        /// </summary>
        [Test]
        public void FindElementTest()
        {
            _myDriver.Url = "http://www.facebook.com";
            var htmlElement = _myDriver.FindElement(By.Id("email"));
            htmlElement.SendKeys("myFakeEmail@mail.com");
            var value = htmlElement.GetProperty("value");
            Console.WriteLine(value);
        }

        [Test]
        public void ObtenerPropiedadType()
        {
            _myDriver.Url = "http://www.facebook.com";
            var emailInput = _myDriver.FindElement(By.Id("email"));
            var type = emailInput.GetProperty("type");
            Console.WriteLine("El tipo de elemento es: " + type);
        }

        [Test]
        public void GetInputvalue()
        {
            _myDriver.Url = "http://www.facebook.com";
            var htmlElement = _myDriver.FindElement(By.Id("email"));
            htmlElement.SendKeys("myFakeEmail@mail.com");

            var value = htmlElement.GetProperty("value");

            Console.WriteLine(value);
        }

        [Test]
        public void GetElementClass()
        {
            _myDriver.Url = "http://www.facebook.com";
            var htmlElement = _myDriver.FindElement(By.Id("email"));

            var elementClassName = htmlElement.GetAttribute("class");

            Console.WriteLine(elementClassName);
        }

        [Test]
        public void FindElementText()
        {
            IWebElement htmlElement = null;

            var elementText = "";

            _myDriver.Url = "http://www.facebook.com";

            htmlElement = _myDriver.FindElement(By.XPath("/html/body/div[1]/div[3]/div[1]/div/div/div/div/div[2]/div[2]/div/div/div/div[1]/form/div[1]/div[6]/div[1]"));

            if(htmlElement != null)
                elementText = htmlElement.Text;
            
            Assert.That(elementText == "Birthday");            
        }

        [Test]
        public void ColeccionDeInputs()
        {
            _myDriver.Url = "http://www.seleniumeasy.com/test/input-form-demo.html";
            //Encontrar el formulario en el documento
            var formulario = _myDriver.FindElement(By.Id("contact_form"));
            //Encontrar LOS elementos input EN el <FORM>
            var coleccionDeInputs = formulario.FindElements(By.TagName("input"));
            //Iterar con un FOR la colección de <INPUT>s
            for(int i = 0; i < coleccionDeInputs.Count; i++)
            {
                //Por cada ciclo asignar el 
                //<input> a una variable llamada input
                var input = coleccionDeInputs[i];
                //Por cada ciclo obtener la propiedad TYPE del
                //input con la función GetProperty()
                var tipoDeInput = input.GetProperty("type");
                //Verificar si el elemento <input> es el textbox llamado
                //email
                if (input.GetProperty("name") == "email")
                    input.SendKeys("email@mail.com");//Si es el textbox
                //<input> llamado email enviar "email@mail.com" con el método
                //SendKeys()
                else
                    input.SendKeys("texto de prueba");//Si no es el elemento
                //<input name="email"> Enviar "texto de prueba"

                //Escribir el tipo de elemento <input type="text"> en
                //la consola del Test Explorer
                Console.WriteLine("El tipo de elemento es: " + tipoDeInput);
            }            
        }

        [Test]
        public void EjercicioLoginForm()
        {
            _myDriver.Url = "https://www.phptravels.net/admin";

            var formulario = _myDriver.FindElement(By.CssSelector("body > div:nth-child(1) > form.form-signin.form-horizontal.wow.fadeIn.animated.animated"));

            var inputsDeFormulario = formulario.FindElements(By.TagName("input"));
            
            foreach(var input in inputsDeFormulario)
            {                
                if (input.GetProperty("name") == "email")
                    input.SendKeys("admin@phptravels.com");
                if (input.GetProperty("name") == "password")
                    input.SendKeys("demoadmin");
                if (input.GetProperty("name") == "remember")
                {
                    var divAfueraDeInput = _myDriver.FindElement(By.CssSelector("body > div:nth-child(1) > form.form-signin.form-horizontal.wow.fadeIn.animated.animated > div:nth-child(1) > div > div:nth-child(1) > label > div"));

                    divAfueraDeInput.Click();
                }                
            }

            var botonLogin = formulario.FindElement(By.TagName("button"));
            botonLogin.Click();
        }

        [Test]
        public void EjercicioElementoSelectTest()
        {
            _myDriver.Url = "http://www.tizag.com/phpT/examples/formex.php";
            
            var education = _myDriver.FindElement(By.Name("education"));
            
            var favoriteDay = _myDriver.FindElement(By.Name("TofD"));
            
            //Crear objeto select
            var selectElementEducation = new SelectElement(education);
            SelectElement selectElementFavoriteDay = new SelectElement(favoriteDay);

            if (selectElementEducation.IsMultiple)
            {
                var opciones = selectElementEducation.AllSelectedOptions;
            }

            foreach (var option in selectElementEducation.Options)
            {
                Console.WriteLine("Este es el texto de la opción: " + option.Text);
                Console.WriteLine("Este es el valor de la opción: " + option.GetProperty("value"));
            }

            //Seleccionar por valor <option value="Jr.High">Jr.High</option>
            selectElementEducation.SelectByValue("Jr.High");

            //Obtener valor seleccionado del elemento
            Console.WriteLine("Este es el valor seleccionado: " + selectElementEducation.SelectedOption.Text);

            //Seleccionar por texto <option value="HighSchool">HighSchool</option>
            selectElementEducation.SelectByText("HighSchool");
            Console.WriteLine("Este es el valor seleccionado: " + selectElementEducation.SelectedOption.Text);

            //Seleccionar por opción: Del arreglo options la tercera [3]
            _myDriver.FindElement(By.XPath(".//*[@id='examp']/form/select[1]/option[3]")).Click();
            Console.WriteLine("Este es el valor seleccionado: " + selectElementEducation.SelectedOption.Text);

            //Seleccionar por index
            selectElementEducation.SelectByIndex(2);
            Console.WriteLine("Este es el valor seleccionado: " + selectElementEducation.SelectedOption.GetProperty("value"));

            //Multiple?
            Console.WriteLine("Education select es múltiple? " + selectElementEducation.IsMultiple.ToString());
            Console.WriteLine("Favorite select es múltiple? " + selectElementFavoriteDay.IsMultiple.ToString());
        }

        [Test]
        public void EjercicioElementoSelectMultipleTest()
        {
            _myDriver.Url = "http://toolsqa.com/automation-practice-form/";
            
            //Primero buscar el elemento select con mi driver
            var selectSeleniumCommands = _myDriver.FindElement(By.Id("selenium_commands"));

            //Crear objeto SelectElement
            SelectElement selectElement = new SelectElement(selectSeleniumCommands);
            
            //Multiple?
            Console.WriteLine("Education select es múltiple? " + selectElement.IsMultiple.ToString());

            selectElement.SelectByText("Browser Commands");
            selectElement.SelectByText("Wait Commands");

            //Cuales son las opciones seleccionadas?
            foreach(var opcionSeleccionada in selectElement.AllSelectedOptions)
            {
                Console.WriteLine("Opción seleccionada: " + opcionSeleccionada.Text);
            }
        }

        [Test]
        public void EjercicioDeTabla()
        {
            IWebElement tabla = null;

            _myDriver.Url = "http://toolsqa.com/automation-practice-table/";

            tabla = _myDriver.FindElement(By.XPath("//*[@id='content']/table"));

            if(tabla != null)
            {
                //Obtener todos los renglones <tr> de la tabla
                ReadOnlyCollection<IWebElement> renglones = tabla.FindElements(By.TagName("tr"));
                
                //Leer renglones para leer celdas de la tabla con un FOR
                for(int i = 0; i < renglones.Count; i++)
                {
                    //Obtener las celdas del renglón
                    ReadOnlyCollection<IWebElement> celdas = renglones[i].FindElements(By.TagName("td"));
                    //Iterar celdas
                    for(int j = 0; j < celdas.Count; j++)
                    {
                        //Obtener texto de la celda
                        var textoCeldaCelda = celdas[j].Text;
                        var textoCelda = renglones[i].FindElements(By.TagName("td"))[j].Text;

                        //Escribir texto de la celda en la consola
                        Console.WriteLine("Este es el texto de la celda: " + textoCelda);
                        Console.WriteLine("Texto celda celda: " + textoCeldaCelda);                        
                    }
                }
                Assert.Pass();
            }

            Assert.Fail();
        }

        [Test]
        public void TotalDeRenglonesTest()
        {
            _myDriver.Url = "http://toolsqa.com/automation-practice-table/";

            var totalBuildingsText = "";
            //Encontrar la tabla con id content
            var tabla = _myDriver.FindElement(By.Id("content"));
            //Obtener TODOS los renglones dentro de la tabla (<tr>s)
            var coleccionDeRenglones = tabla.FindElements(By.TagName("tr"));
            var contadorDeRenglones = 0;
            //Iterar con un FOREAC la colección de renglones
            foreach(var renglon in coleccionDeRenglones)
            {
                var ths = renglon.FindElements(By.TagName("th"));
                                
                var renglonConTotal = coleccionDeRenglones[1];

                var unicaCeldaRenglonTotal = renglonConTotal.FindElements(By.TagName("td"));

                totalBuildingsText = unicaCeldaRenglonTotal[0].Text;
                //Si tiene más de un elemento <th> significa que es el renglón de títulos
                if (!(ths.Count > 1))
                {
                    var celdas = renglon.FindElements(By.TagName("td"));
                    if (!(celdas[0].Text == "4 buildings"))
                        contadorDeRenglones++;
                }
            }

            Assert.That(contadorDeRenglones.ToString() + " buildings" == totalBuildingsText);
        }

        [Test]
        public void ExcepcionElementoNoEncontrado()
        {            
            _myDriver.Url = "http://toolsqa.com/automation-practice-table/";
            
            Assert.Throws<NoSuchElementException>(() => _myDriver.FindElement(By.XPath("//*[@id='content']/table1")));
        }

        [Test]
        public void AlertaSimpleTest()
        {
            //URL
            _myDriver.Url = "http://toolsqa.com/handling-alerts-using-selenium-webdriver/";

            //Buscar boton Simple Alert y darle clic
            _myDriver.FindElement(By.XPath("//button[contains(text(),'Simple Alert')]")).Click();

            //Esperar el alert
            Thread.Sleep(2000);

            //Driver encuentra el Alert
            IAlert alert = _myDriver.SwitchTo().Alert();

            //Clic al boton OK
            alert.Accept();
        }

        [Test]
        public void AlertaConfirmation()
        {
            _myDriver.Url = "http://toolsqa.com/handling-alerts-using-selenium-webdriver/";
            
            _myDriver.FindElement(By.XPath("//button[contains(text(),'Confirm Pop up')]")).Click();

            Thread.Sleep(2000);

            IAlert alert = _myDriver.SwitchTo().Alert();
            
            //Cancelar
            alert.Dismiss();
        }

        [Test]
        public void AlertaPrompt()
        {
            _myDriver.Url = "http://toolsqa.com/handling-alerts-using-selenium-webdriver/";

            _myDriver.FindElement(By.XPath("//button[contains(text(),'Prompt Pop up')]")).Click();

            Thread.Sleep(2000);

            IAlert alert = _myDriver.SwitchTo().Alert();

            //Enviar Yes al input del alert
            alert.SendKeys("Yes");

            //Aceptar después de recibir texto
            alert.Accept();
        }

        [Test]
        public void WebDriverWait()
        {
            _myDriver.Url = "http://toolsqa.wpengine.com/automation-practice-switch-windows/";

            WebDriverWait wait = new WebDriverWait(_myDriver, TimeSpan.FromMinutes(1));

            Func<IWebDriver, bool> waitForElement = 
                new Func<IWebDriver, bool>((IWebDriver Web) =>
            {
                Console.WriteLine(Web.FindElement(By.Id("target")).GetAttribute("innerHTML"));
                return true;
            });

            wait.Until(waitForElement);
        }

        [Test]
        public void WebDriverWait2()
        {
            _myDriver.Url = "http://toolsqa.wpengine.com/automation-practice-switch-windows/";

            WebDriverWait wait = new WebDriverWait(_myDriver, TimeSpan.FromMinutes(1));

            Func<IWebDriver, IWebElement> waitForElement = 
                new Func<IWebDriver, IWebElement>((IWebDriver Web) =>
            {
                Console.WriteLine("Waiting for color to change");

                IWebElement element = Web.FindElement(By.Id("colorVar"));
                if (element.GetAttribute("style").Contains("red"))
                {
                    return element;
                }
                return null;
            });

            IWebElement targetElement = wait.Until(waitForElement);
            Console.WriteLine("Inner HTML of element is " + targetElement.GetAttribute("innerHTML"));
        }

        [Test]
        public void BuzzBuzzWait()
        {
            _myDriver.Url = "http://toolsqa.wpengine.com/automation-practice-switch-windows/";

            WebDriverWait wait = new WebDriverWait(_myDriver, TimeSpan.FromSeconds(45));

            Func<IWebDriver, IWebElement> BuzBuzFunc = 
                new Func<IWebDriver, IWebElement>((IWebDriver web) => {
                var findElementBuzz = web.FindElement(By.Id("clock"));

                if (findElementBuzz.Text == "Buzz Buzz")
                    return findElementBuzz;
                else
                    return null;
            });

            wait.Until(BuzBuzFunc);
        }

        [Test]
        public void DefaultWait()
        {
            _myDriver.Url = "http://toolsqa.wpengine.com/automation-practice-switch-windows/";

            IWebElement element = _myDriver.FindElement(By.Id("colorVar"));

            DefaultWait<IWebElement> wait = new DefaultWait<IWebElement>(element);

            wait.Timeout = TimeSpan.FromMinutes(2);

            wait.PollingInterval = TimeSpan.FromMilliseconds(250);

            Func<IWebElement, bool> waiter = 
                new Func<IWebElement, bool>((IWebElement ele) =>
            {
                String styleAttrib = element.GetAttribute("style");
                if (styleAttrib.Contains("red"))
                {
                    Console.WriteLine("El color ya es rojo!!!!!");
                    return true;
                }
                Console.WriteLine("El color aún es " + styleAttrib);
                return false;
            });

            wait.Until(waiter);
        }

        [Test]
        public void DefaultWait2()
        {
            _myDriver.Url = "http://toolsqa.wpengine.com/automation-practice-switch-windows/";

            IWebElement element = _myDriver.FindElement(By.Id("clock"));

            DefaultWait<IWebElement> wait = new DefaultWait<IWebElement>(element);

            wait.Timeout = TimeSpan.FromMinutes(2);

            wait.PollingInterval = TimeSpan.FromMilliseconds(250);

            Func<IWebElement, bool> waiter = 
                new Func<IWebElement, bool>((IWebElement ele) =>
            {
                String elementText = element.Text;
                if (elementText.Contains("Buzz"))
                {
                    Console.WriteLine("Buzzzzzzz!!!");
                    return true;
                }
                Console.WriteLine("Current time is " + elementText);
                return false;
            });

            wait.Until(waiter);
        }

        [Test]
        public void ScreenShotExercise()
        {
            _myDriver.Url = "http://testing-ground.scraping.pro/login";

            //Elementos de la forma
            var userNameField = _myDriver.FindElement(By.Id("usr"));
            var userPasswordField = _myDriver.FindElement(By.Id("pwd"));
            var loginButton = _myDriver.FindElement(By.XPath("//input[@value='Login']"));

            //Entrar usuario y contraseña
            userNameField.SendKeys("admin");
            userPasswordField.SendKeys("12345");

            //Clic login
            loginButton.Click();

            //Extraer el texto y guardarlo en un archivo .txt
            var result = _myDriver.FindElement(By.XPath("//div[@id='case_login']/h3")).Text;
            
            File.WriteAllText("C://result.txt", result);

            //Tomar una captura
            Screenshot ss = ((ITakesScreenshot)_myDriver).GetScreenshot();
            ss.SaveAsFile("C://Image.png",
            ScreenshotImageFormat.Png);
        }

        [TearDown]
        public void EndTest()
        {
            //_myDriver.Close();
            _myDriver.Quit();
        }
    }
}
