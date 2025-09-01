using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ControleDeCinema.Testes.Interface.ModuloIngresso;

public class IngressoFormPageObject
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public IngressoFormPageObject(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        wait.Until(d => d.FindElement(By.CssSelector("form[data=se='form']")).Displayed);
    }

    public IngressoFormPageObject PreencherNumero(int numero)
    {
        var input = driver.FindElement(By.Id("Numero"));
        input.Clear();
        input.SendKeys(numero.ToString());
        return this;
    }

    public IngressoFormPageObject SelecionarSessao(string sessao)
    {
        var select = new SelectElement(driver.FindElement(By.Id("SessaoId")));
        select.SelectByText(sessao);
        return this;
    }

    public IngressoFormPageObject MeioPagamento(bool meia)
    {
        var input = driver.FindElement(By.Id("Meia"));
        input.SendKeys(meia ? "true" : "false");
        return this;
    }

    public IngressoIndexPageObject Confirmar()
    {
        wait.Until(d => d.FindElement(By.CssSelector("button[data-se='btnConfirmar']"))).Click();
        wait.Until(d => d.FindElement(By.CssSelector("a[data-se='btnCadastrar']")).Displayed);
        return new IngressoIndexPageObject(driver);
    }
}