using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ControleDeCinema.Testes.Interface.ModuloGeneroFilme;

public class FilmeFormPageObject
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public FilmeFormPageObject(IWebDriver driver)
    {
        this.driver = driver;

        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        wait.Until(d => d.FindElement(By.CssSelector("form")).Displayed);
    }

    public FilmeFormPageObject PreencherNome(string nome)
    {
        var inputNome = driver?.FindElement(By.Id("Nome"));
        inputNome?.Clear();
        inputNome?.SendKeys(nome);

        return this;
    }

    public FilmeFormPageObject PreencherDuracao(int duracao)
    {
        wait.Until(d =>
            d.FindElement(By.Id("Duracao")).Displayed &&
            d.FindElement(By.Id("Duracao")).Enabled
        );

        var input = driver.FindElement(By.Id("Duracao"));
        input.Clear(); // limpa o campo antes
        input.SendKeys(duracao.ToString());   

        return this;
    }

    public FilmeFormPageObject PreencherLancamento(bool verdadeiro)
    { 
        wait.Until(d =>
            d.FindElement(By.Id("Lancamento")).Displayed &&
            d.FindElement(By.Id("Lancamento")).Enabled
        );

        var imput = driver.FindElement(By.Id("Lancamento"));
        imput?.Clear(); // limpa o campo antes
        imput?.SendKeys(verdadeiro ? "true" : "false");

        return this;
    }

    public FilmeFormPageObject SelecionarGenero(string genero)
    {
        wait.Until(d =>
            d.FindElement(By.Id("GeneroId")).Displayed &&
            d.FindElement(By.Id("GeneroId")).Enabled
        );

        var select = new SelectElement(driver.FindElement(By.Id("GeneroId")));
        select.SelectByText(genero);

        return this;
    }

    public FilmeIndexPageObject Confirmar()
    {
        wait.Until(d => d.FindElement(By.CssSelector("button[type='submit']"))).Click();

        wait.Until(d => d.FindElement(By.CssSelector("a[data-se='btnCadastrar']")).Displayed);

        return new FilmeIndexPageObject(driver!);
    }
}