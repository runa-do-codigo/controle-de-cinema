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

        wait.Until(d => d.FindElement(By.CssSelector("form[data-se='form']")).Displayed);
    }

    public FilmeFormPageObject PreencherTitulo(string titulo)
    {
        var InputTitulo = driver?.FindElement(By.CssSelector("input[data-se='InputTitulo']"));
        InputTitulo?.Clear();
        InputTitulo?.SendKeys(titulo);

        return this;
    }

    public FilmeFormPageObject PreencherDuracao(int duracao)
    {
        wait.Until(d =>
            d.FindElement(By.CssSelector("input[data-se='InputDuracao']")).Displayed &&
            d.FindElement(By.CssSelector("input[data-se='InputDuracao']")).Enabled
        );

        var input = driver.FindElement(By.CssSelector("input[data-se='InputDuracao']"));
        input.Clear(); // limpa o campo antes
        input.SendKeys(duracao.ToString());   

        return this;
    }

    public FilmeFormPageObject PreencherLancamento(bool verdadeiro)
    { 
        wait.Until(d =>
            d.FindElement(By.CssSelector("input[data-se='InputCheckBox']")).Displayed &&
            d.FindElement(By.CssSelector("input[data-se='InputCheckBox']")).Enabled
        );

        var imput = driver.FindElement(By.CssSelector("input[data-se='InputCheckBox']"));

        imput?.SendKeys(verdadeiro ? "true" : "false");

        return this;
    }

    public FilmeFormPageObject SelecionarGenero(string genero)
    {
        wait.Until(d =>
            d.FindElement(By.CssSelector("select[data-se='InputGenero']")).Displayed &&
            d.FindElement(By.CssSelector("select[data-se='InputGenero']")).Enabled
        );

        var select = new SelectElement(driver.FindElement(By.CssSelector("select[data-se='InputGenero']")));
        select.SelectByText(genero);

        return this;
    }

    public FilmeIndexPageObject Confirmar()
    {
        wait.Until(d => d.FindElement(By.CssSelector("button[data-se='btnConfirmar']"))).Click();

        wait.Until(d => d.FindElement(By.CssSelector("a[data-se='btnCadastrar']")).Displayed);

        return new FilmeIndexPageObject(driver!);
    }
}