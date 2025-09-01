using ControleDeCinema.Dominio.ModuloFilme;
using ControleDeCinema.Dominio.ModuloSala;
using ControleDeCinema.Dominio.ModuloSessao;
using ControleDeCinema.Testes.Interface.ModuloGeneroFilme;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ControleDeCinema.Testes.Interface.ModuloSessao;

public class SessaoFormPageObject
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public SessaoFormPageObject(IWebDriver driver)
    {
        this.driver = driver;

        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        wait.Until(d => d.FindElement(By.CssSelector("form[data-se='form']")).Displayed);
    }

    public SessaoFormPageObject PreencherNumeroIngressos(int numeroIngressos)
    {
        var inputNumero = driver?.FindElement(By.Id("NumeroMaximoIngressos"));
        inputNumero?.Clear();
        inputNumero?.SendKeys(numeroIngressos.ToString());

        return this;
    }

    public SessaoFormPageObject PreencherEncerrada(bool encerrada)
    {
        wait.Until(d =>
            d.FindElement(By.Id("Encerrada")).Displayed &&
            d.FindElement(By.Id("Encerrada")).Enabled
        );

        var imput = driver.FindElement(By.Id("Encerrada"));
        imput?.SendKeys(encerrada ? "true" : "false");

        return this;
    }

    public SessaoFormPageObject SelecionarFilme(Filme filme)
    {
        wait.Until(d =>
            d.FindElement(By.Id("FilmeId")).Displayed &&
            d.FindElement(By.Id("FilmeId")).Enabled
        );

        var select = new SelectElement(driver.FindElement(By.Id("FilmeId")));
        select.SelectByText(filme.ToString());

        return this;
    }

    public SessaoFormPageObject SelecionarSala(Sala sala)
    {
        wait.Until(d =>
            d.FindElement(By.Id("SalaId")).Displayed &&
            d.FindElement(By.Id("SalaId")).Enabled
        );

        var select = new SelectElement(driver.FindElement(By.Id("SalaId")));
        select.SelectByText(sala.ToString());

        return this;
    }

    public SessaoFormPageObject PreencherDataHora(DateTime dataHora)
    {
        wait.Until(d =>
            d.FindElement(By.Id("DataHora")).Displayed &&
            d.FindElement(By.Id("DataHora")).Enabled
        );

        var inputDataHora = driver.FindElement(By.Id("DataHora"));
        inputDataHora?.Clear();

        string valorFormatado = dataHora.ToString("yyyy-MM-ddTHH:mm");

        inputDataHora?.SendKeys(valorFormatado);

        return this;
    }


    public SessaoIndexPageObject Confirmar()
    {
        wait.Until(d => d.FindElement(By.CssSelector("button[data-se='btnConfirmar']"))).Click();

        wait.Until(d => d.FindElement(By.CssSelector("a[data-se='btnCadastrar']")).Displayed);

        return new SessaoIndexPageObject(driver!);
    }
}