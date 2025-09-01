using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ControleDeCinema.Testes.Interface.ModuloIngresso;

public class IngressoIndexPageObject
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public IngressoIndexPageObject(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public IngressoIndexPageObject IrPara(string enderecoBase)
    {
        driver.Navigate().GoToUrl(Path.Combine(enderecoBase, "ingressos"));
        return this;
    }

    public IngressoFormPageObject ClickCadastrar()
    {
        wait.Until(d => d.FindElement(By.CssSelector("a[data-se='btnCadastrar']"))).Click();
        return new IngressoFormPageObject(driver);
    }

    public IngressoFormPageObject ClickEditar()
    {
        wait.Until(d => d.FindElement(By.CssSelector(".card a[title='Edição']"))).Click();
        return new IngressoFormPageObject(driver);
    }

    public IngressoFormPageObject ClickExcluir()
    {
        wait.Until(d => d.FindElement(By.CssSelector(".card a[title='Exclusão']"))).Click();
        return new IngressoFormPageObject(driver);
    }

    public bool ContemIngresso(string numero)
    {
        wait.Until(d => d.FindElement(By.CssSelector("a[data-se='btnCadastrar']")).Displayed);
        return driver.PageSource.Contains(numero);
    }
}