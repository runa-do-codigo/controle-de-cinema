﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ControleDeCinema.Testes.Interface.ModuloGeneroFilme;

public class SalaFormPageObject
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public SalaFormPageObject(IWebDriver driver)
    {
        this.driver = driver;

        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        wait.Until(d => d.FindElement(By.CssSelector("form")).Displayed);
    }

    public SalaFormPageObject PreencherNumero(int numeroSala)
    {
        var inputNumero = driver?.FindElement(By.Id("Numero"));
        inputNumero?.Clear();
        inputNumero?.SendKeys(numeroSala.ToString());

        return this;
    }

    public SalaFormPageObject PreencherCapacidade(int capacidade)
    {
        wait.Until(d =>
            d.FindElement(By.Id("Capacidade")).Displayed &&
            d.FindElement(By.Id("Capacidade")).Enabled
        );

        var input = driver.FindElement(By.Id("Capacidade"));
        input.Clear(); // limpa o campo antes
        input.SendKeys(capacidade.ToString());

        return this;
    }

    public SalaIndexPageObject Confirmar()
    {
        wait.Until(d => d.FindElement(By.CssSelector("button[type='submit']"))).Click();

        wait.Until(d => d.FindElement(By.CssSelector("a[data-se='btnCadastrar']")).Displayed);

        return new SalaIndexPageObject(driver!);
    }
}