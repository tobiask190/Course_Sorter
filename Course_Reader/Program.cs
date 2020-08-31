using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.IO;
using System.Threading;

namespace Course_Reader {
    class Program {
        static void Main(string[] args) {
            IWebDriver driver = new ChromeDriver();
            try {
                // Navigate to Url
                driver.Navigate().GoToUrl("https://registrationssb.ucr.edu/StudentRegistrationSsb/ssb/term/termSelection?mode=search");

                Action<Action<Actions>> Perform = perform => {
                    var a = new Actions(driver);
                    perform(a);
                    a.Build().Perform();
                };


                var f = driver.FindElement(By.Id("s2id_txt_term"));
                Perform(a => a.Click(f));

                Thread.Sleep(1000);

                //Click the first available term
                Perform(a => a.MoveToElement(f).MoveByOffset(0, 60).Click());
                
                Thread.Sleep(200);

                Perform(a => a.Click(driver.FindElement(By.Id("term-go"))));
                Thread.Sleep(1000);

                int t = 6000;
                void EnterSearch() {
                    Perform(a => a.Click(driver.FindElement(By.Id("search-go"))));
                    Thread.Sleep(2000);

                    //Find the entries per page field by the label
                    var perPage = driver.FindElement(By.CssSelector("[aria-label=\"10 per page\"]"));
                    Perform(a => a.Click(perPage));

                    //Menu appears above the field, so arrow down and enter 50
                    Perform(a => a.SendKeys(Keys.Down));
                    Perform(a => a.SendKeys(Keys.Down));
                    Perform(a => a.SendKeys(Keys.Down));
                    Perform(a => a.SendKeys(Keys.Enter));

                    Thread.Sleep(t);
                }

                void SetPage(int page) {
                    Perform(a => a.SendKeys(Keys.Tab));
                    Perform(a => a.SendKeys(Keys.Tab));
                    Perform(a => a.SendKeys(Keys.Tab));
                    Perform(a => a.SendKeys(Keys.Tab));
                    Perform(a => a.SendKeys((page).ToString()));
                    Perform(a => a.SendKeys(Keys.Enter));

                    //Find the button by its tooltip
                    //Perform(a => a.MoveToElement(driver.FindElement(By.XPath("//*[@title=\"Next\"]"))).Click());
                    Thread.Sleep(t);
                }

                EnterSearch();

                //Find the button by its tooltip
                //Perform(a => a.MoveToElement(driver.FindElement(By.XPath("//*[@title=\"Last\"]"))).Click());
                Thread.Sleep(t);

                int start = 179;

                SetPage(start);
                for (int i = start; i < 210; i++) {
                    if(i%10 == 0) {
                        driver.Navigate().GoToUrl("https://registrationssb.ucr.edu/StudentRegistrationSsb/ssb/classSearch/classSearch");
                        EnterSearch();
                        SetPage(i);
                    }

                    File.WriteAllText($"page{i}.html", driver.PageSource);

                    //Find the button by its tooltip
                    Perform(a => a.MoveToElement(driver.FindElement(By.XPath("//*[@title=\"Next\"]"))).Click());
                    Thread.Sleep(t);
                }

                //Perform(a => a.MoveToElement(perPage.FindElements(By.TagName("option"))[3]).Click());
            } finally {
                driver.Quit();
            }
        }
    }
}
