using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using AppGestaoDeResiduos.Controllers;
using AppGestaoDeResiduos.Models;
using AppGestaoDeResiduos.Data;
using System;
using AppGestaoDeResiduos.Dto;
using NuGet.ContentModel;

public class CaminhaoControllerTests
{
    private CaminhaoController _controller;
    private ApplicationDbContext _context;

    public CaminhaoControllerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new ApplicationDbContext(options);

        // Seed data
        _context.Caminhoes.AddRange(
            new Caminhao { CaminhaoId = 1, Placa = "ABC1234", LocalizacaoId = 1 },
            new Caminhao { CaminhaoId = 2, Placa = "XYZ5678", LocalizacaoId = 2 }
        );

        _context.Localizacoes.AddRange(
            new Localizacao { LocalizacaoId = 1, Longitude = (int?)1.0, Latitude = (int?)1.0, DataHora = DateTime.Now },
            new Localizacao { LocalizacaoId = 2, Longitude = (int?)2.0, Latitude = (int?)2.0, DataHora = DateTime.Now }
        );

        _context.SaveChanges();

        _controller = new CaminhaoController(_context);
    }

    [Fact]
    public async Task GetAllCaminhoes_ReturnsOkResult()
    {
        var result = await _controller.GetAllCaminhoes();
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var caminhoes = Assert.IsType<List<Caminhao>>(okResult.Value);
        Assert.Equal(2, caminhoes.Count);
    }

    [Fact]
    public async Task TrackCaminhoes_ReturnsOkResult()
    {
        var result = await _controller.TrackCaminhoes();
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var caminhoes = Assert.IsType<List<Caminhao>>(okResult.Value);
        Assert.Equal(2, caminhoes.Count);
    }

    [Fact]
    public async Task GetCaminhao_ReturnsNotFoundResult_WhenCaminhaoDoesNotExist()
    {
        var result = await _controller.GetCaminhao(999);
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetCaminhao_ReturnsOkResult_WhenCaminhaoExists()
    {
        var result = await _controller.GetCaminhao(1);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var caminhao = Assert.IsType<Caminhao>(okResult.Value);
        Assert.Equal("ABC1234", caminhao.Placa);
    }

    [Fact]
    public async Task AddCaminhao_ReturnsCreatedAtActionResult()
    {
        var caminhao = new Caminhao { CaminhaoId = 3, Placa = "NEW1234", LocalizacaoId = 3 };
        var result = await _controller.AddCaminhao(caminhao);
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var createdCaminhao = Assert.IsType<Caminhao>(createdAtActionResult.Value);
        Assert.Equal("NEW1234", createdCaminhao.Placa);
    }

    [Fact]
    public async Task AddOrUpdateCaminhao_UpdatesExistingCaminhao()
    {
        var caminhao = new Caminhao { CaminhaoId = 1, Placa = "UPDATED1234", LocalizacaoId = 1 };
        var result = await _controller.AddOrUpdateCaminhao(caminhao);
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var updatedCaminhao = Assert.IsType<Caminhao>(createdAtActionResult.Value);
        Assert.Equal("UPDATED1234", updatedCaminhao.Placa);
    }

    [Fact]
    public async Task GetColetas_ReturnsOkResult()
    {
        var result = await _controller.GetColetas();
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var coletas = Assert.IsType<List<Coleta>>(okResult.Value);
        Assert.Empty(coletas); // Assuming no coletas are seeded
    }

    [Fact]
    public async Task ScheduleColeta_ReturnsCreatedAtActionResult()
    {
        var coleta = new Coleta { ColetaId = 1, CaminhaoId = 1 };
        var result = await _controller.ScheduleColeta(coleta);
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var createdColeta = Assert.IsType<Coleta>(createdAtActionResult.Value);
        Assert.Equal(1, createdColeta.CaminhaoId);
    }

    [Fact]
    public async Task PostNotificarUsuario_ReturnsOkResult()
    {
        var usuario = new Usuario { UsuarioId = 1, FoiNotificado = false };
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();

        var request = new NotificarUsuarioRequest { UsuarioId = 1, Mensagem = "Teste" };
        var result = await _controller.PostNotificarUsuario(request);
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Usuário notificado com sucesso.", okResult.Value);
    }
}
