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

        wait.Until(d => d.FindElement(By.CssSelector("form")).Displayed);
    }

    public AutenticacaoFormPageObject PreencherEmail(string email)
    {
        var inputNome = driver?.FindElement(By.Id("email"));
        inputNome?.Clear();
        inputNome?.SendKeys(email);

        return this;
    }

    public AutenticacaoFormPageObject PreencherSenha(int senha)
    {
        wait.Until(d =>
             d.FindElement(By.CssSelector("input[data-se='inputEmail']")).Displayed &&
            d.FindElement(By.CssSelector("input[data-se='inputEmail']")).Enabled
        );

        IWebElement inputEmail = wait.Until(d => d.FindElement(By.CssSelector("input[data-se='inputEmail']")));
        inputEmail.Clear();
        inputEmail.SendKeys(senha.ToString());

        return this;
    }

    public AutenticacaoFormPageObject PreencherSenha(string senha)
    {
        wait.Until(d =>
            d.FindElement(By.CssSelector("input[data-se='inputSenha']")).Displayed &&
            d.FindElement(By.CssSelector("input[data-se='inputSenha']")).Enabled
        );

        IWebElement inputSenha = wait.Until(d => d.FindElement(By.CssSelector("input[data-se='inputSenha']")));
        inputSenha.Clear();
        inputSenha.SendKeys(senha);

        return this;
    }

    public AutenticacaoFormPageObject PreencherConfirmarSenha(string confirmarSenha)
    {
        wait.Until(d =>
            d.FindElement(By.CssSelector("input[data-se='inputConfirmarSenha']")).Displayed &&
            d.FindElement(By.CssSelector("input[data-se='inputConfirmarSenha']")).Enabled
        );

        IWebElement inputConfirmarSenha = wait.Until(d => d.FindElement(By.CssSelector("input[data-se='inputConfirmarSenha']")));
        inputConfirmarSenha.Clear();
        inputConfirmarSenha.SendKeys(confirmarSenha);

        return this;
    }
    public AutenticacaoFormPageObject SelecionarTipoDeUsuario(string tipoConta)
    {
        wait.Until(d =>
            d.FindElement(By.CssSelector("select[data-se='selectTipoUsuario']")).Displayed &&
            d.FindElement(By.CssSelector("select[data-se='selectTipoUsuario']")).Enabled
        );

        SelectElement selectTipoUsuario = new(driver.FindElement(By.CssSelector("select[data-se='selectTipoUsuario']")));

        wait.Until(_ => selectTipoUsuario.Options.Any(o => o.Text == tipoConta));

        selectTipoUsuario.SelectByText(tipoConta);

        return this;
    }

    public AutenticacaoIndexPageObject Confirmar()
    {
        wait.Until(d => d.FindElement(By.CssSelector("button[type='submit']"))).Click();

        wait.Until(d => d.FindElement(By.CssSelector("a[data-se='btnCadastrar']")).Displayed);

        return new AutenticacaoIndexPageObject(driver!);
    }
}