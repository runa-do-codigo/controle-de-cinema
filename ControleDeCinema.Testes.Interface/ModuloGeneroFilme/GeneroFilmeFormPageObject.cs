using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ControleDeCinema.Testes.Interface.ModuloGeneroFilme;

public class GeneroFilmeFormPageObject
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public GeneroFilmeFormPageObject(IWebDriver driver)
    {
        this.driver = driver;

        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        wait.Until(d => d.FindElement(By.CssSelector("form[data-se='form']")).Displayed);
    }

    public GeneroFilmeFormPageObject PreencherDescricao(string descricao)
    {
        var inputDescricao = driver?.FindElement(By.CssSelector("input[data-se='InputDescricao']"));
        inputDescricao?.Clear();
        inputDescricao?.SendKeys(descricao);

        return this;
    }

    public GeneroFilmeIndexPageObject Confirmar()
    {
        wait.Until(d => d.FindElement(By.CssSelector("button[data-se='btnConfirmar']"))).Click();

        wait.Until(d => d.FindElement(By.CssSelector("a[data-se='btnCadastrar']")).Displayed);

        return new GeneroFilmeIndexPageObject(driver!);
    }
}