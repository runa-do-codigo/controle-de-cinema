using ControleDeCinema.Testes.Interface.ModuloAutenticacao;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ControleDeCinema.Testes.Interface.ModuloGeneroFilme;

public class AutenticacaoFormPageObject
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public AutenticacaoFormPageObject(IWebDriver driver)
    {
        this.driver = driver;

        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        wait.Until(d =>
            d.FindElement(By.CssSelector("input[data-se='InputEmail']")).Displayed &&
            d.FindElement(By.CssSelector("input[data-se='InputSenha']")).Displayed);
    }

    public AutenticacaoFormPageObject PreencherEmail(string email)
    {
        wait.Until(d =>
             d.FindElement(By.CssSelector("input[data-se='InputEmail']")).Displayed &&
            d.FindElement(By.CssSelector("input[data-se='InputEmail']")).Enabled
        );

        IWebElement inputEmail = wait.Until(d => d.FindElement(By.CssSelector("input[data-se='InputEmail']")));
        inputEmail.Clear();
        inputEmail.SendKeys(email);

        return this;
    }

    public AutenticacaoFormPageObject PreencherSenha(string senha)
    {
        wait.Until(d =>
            d.FindElement(By.CssSelector("input[data-se='InputSenha']")).Displayed &&
            d.FindElement(By.CssSelector("input[data-se='InputSenha']")).Enabled
        );

        IWebElement inputSenha = wait.Until(d => d.FindElement(By.CssSelector("input[data-se='InputSenha']")));
        inputSenha.Clear();
        inputSenha.SendKeys(senha);

        return this;
    }

    public AutenticacaoFormPageObject PreencherConfirmarSenha(string confirmarSenha)
    {
        wait.Until(d =>
            d.FindElement(By.CssSelector("input[data-se='InputConfirmarSenha']")).Displayed &&
            d.FindElement(By.CssSelector("input[data-se='InputConfirmarSenha']")).Enabled
        );

        IWebElement inputConfirmarSenha = wait.Until(d => d.FindElement(By.CssSelector("input[data-se='InputConfirmarSenha']")));
        inputConfirmarSenha.Clear();
        inputConfirmarSenha.SendKeys(confirmarSenha);

        return this;
    }
    public AutenticacaoFormPageObject SelecionarTipoDeUsuario(string tipoConta)
    {
        wait.Until(d =>
            d.FindElement(By.CssSelector("select[data-se='InputTipo']")).Displayed &&
            d.FindElement(By.CssSelector("select[data-se='InputTipo']")).Enabled
        );

        SelectElement selectTipoUsuario = new(driver.FindElement(By.CssSelector("select[data-se='InputTipo']")));

        wait.Until(_ => selectTipoUsuario.Options.Any(o => o.Text == tipoConta));

        selectTipoUsuario.SelectByText(tipoConta);

        return this;
    }

    public AutenticacaoIndexPageObject Confirmar()
    {
        wait.Until(d => d.FindElement(By.CssSelector("button[data-se='btnConfirmar']"))).Click();

        wait.Until(d => d.FindElements(By.CssSelector("form[action='/autenticacao/logout']")).Count > 0);

        return new AutenticacaoIndexPageObject(driver!);
    }
}