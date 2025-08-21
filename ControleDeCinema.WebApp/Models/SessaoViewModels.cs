using ControleDeCinema.Dominio.ModuloFilme;
using ControleDeCinema.Dominio.ModuloSala;
using ControleDeCinema.Dominio.ModuloSessao;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ControleDeCinema.WebApp.Models;

public abstract class FormularioSessaoViewModel
{
    [Required(ErrorMessage = "Informe a data/hora de início.")]
    public DateTime Inicio { get; set; }

    [Range(1, 2000, ErrorMessage = "O número máximo de ingressos deve ser ao menos 1.")]
    public int NumeroMaximoIngressos { get; set; }


    [Required(ErrorMessage = "Selecione um filme.")]
    public Guid FilmeId { get; set; }
    public List<SelectListItem>? Filmes { get; set; }

    [Required(ErrorMessage = "Selecione uma sala.")]
    public Guid SalaId { get; set; }
    public List<SelectListItem>? SalasDisponiveis { get; set; }

    public static Sessao ParaEntidade(
        FormularioSessaoViewModel formularioVm,
        List<Filme> filmesDisponiveis,
        List<Sala> salasDisponiveis)
    {
        var filme = filmesDisponiveis.Find(f => f.Id == formularioVm.FilmeId)
                    ?? throw new ArgumentException("Filme inválido.");

        var sala = salasDisponiveis.Find(s => s.Id == formularioVm.SalaId)
                   ?? throw new ArgumentException("Sala inválida.");

        return new Sessao(
            formularioVm.Inicio,
            formularioVm.NumeroMaximoIngressos,
            filme,
            sala
        );
    }
}

public class CadastrarSessaoViewModel : FormularioSessaoViewModel
{
    public CadastrarSessaoViewModel() { }

    public CadastrarSessaoViewModel(List<Filme> filmes, List<Sala> salas)
    {
        Filmes = filmes
            .Select(f => new SelectListItem(f.Titulo, f.Id.ToString()))
            .ToList();

        SalasDisponiveis = salas
            .Select(f => new SelectListItem(f.Numero.ToString(), f.Id.ToString()))
            .ToList();
    }
}

public class EditarSessaoViewModel : FormularioSessaoViewModel
{
    public Guid Id { get; set; }

    public EditarSessaoViewModel() { }

    public EditarSessaoViewModel(
        Guid id,
        DateTime inicio,
        int maxIngressos,
        List<Filme> filmes,
        List<Sala> salas
    )
    {
        Id = id;
        Inicio = inicio;
        NumeroMaximoIngressos = maxIngressos;

        Filmes = filmes
            .Select(f => new SelectListItem(f.Titulo, f.Id.ToString()))
            .ToList();

        SalasDisponiveis = salas
            .Select(f => new SelectListItem(f.Numero.ToString(), f.Id.ToString()))
            .ToList();
    }
}

public class ExcluirSessaoViewModel
{
    public Guid Id { get; set; }
    public DateTime Inicio { get; set; }
    public string Filme { get; set; }
    public int Sala { get; set; }

    public ExcluirSessaoViewModel(Guid id, DateTime inicio, string filmeTitulo, int salaNumero)
    {
        Id = id;
        Inicio = inicio;
        Filme = filmeTitulo;
        Sala = salaNumero;
    }
}

public class VisualizarSessoesViewModel
{
    public List<DetalhesSessaoViewModel> Registros { get; set; }

    public VisualizarSessoesViewModel(List<Sessao> sessoes)
    {
        Registros = sessoes
            .Select(DetalhesSessaoViewModel.ParaDetalhesVm)
            .ToList();
    }
}

public class DetalhesSessaoViewModel
{
    public Guid Id { get; set; }
    public DateTime Inicio { get; set; }
    public string Filme { get; set; }
    public int Sala { get; set; }
    public bool Encerrada { get; set; }
    public int MaxIngressos { get; set; }

    public DetalhesSessaoViewModel(
        Guid id,
        DateTime inicio,
        string filme,
        int sala,
        bool encerrada,
        int maxIngressos
    )
    {
        Id = id;
        Inicio = inicio;
        Filme = filme;
        Sala = sala;
        Encerrada = encerrada;
        MaxIngressos = maxIngressos;
    }

    public static DetalhesSessaoViewModel ParaDetalhesVm(Sessao s)
    {
        return new DetalhesSessaoViewModel(
            s.Id,
            s.Inicio,
            s.Filme?.Titulo ?? string.Empty,
            s.Sala?.Numero ?? 0,
            s.Encerrada,
            s.NumeroMaximoIngressos
        );
    }
}

public class VenderIngressoViewModel
{
    [Required]
    public Guid SessaoId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Assento inválido.")]
    public int Assento { get; set; }

    public bool MeiaEntrada { get; set; }
}
