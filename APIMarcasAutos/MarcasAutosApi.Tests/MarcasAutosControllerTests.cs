using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using APIMarcasAutos.Controllers;
using APIMarcasAutos.Models;

public class MarcasAutosControllerTests
{
    private readonly MarcasAutosController _controller;
    private readonly AppDbContext _context;

    public MarcasAutosControllerTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
                        .UseInMemoryDatabase(databaseName: "TestDatabase")
                        .Options;

        _context = new AppDbContext(options);

        // Insertar datos de prueba en la base de datos en memoria
        _context.MarcasAutos.Add(new MarcaAuto { Id = 1, Nombre = "Marca 1" });
        _context.MarcasAutos.Add(new MarcaAuto { Id = 2, Nombre = "Marca 2" });
        _context.SaveChanges();

        _controller = new MarcasAutosController(_context);
    }

    [Fact]
    public async Task GetMarcasAutos_ReturnsAllMarcasAutos()
    {
        var result = await _controller.GetMarcasAutos();

        // Verificar que el resultado no es nulo y que es de tipo OkObjectResult
        var actionResult = Assert.IsType<ActionResult<IEnumerable<MarcaAuto>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

        // Verificar que el valor retornado sea una lista no vacía
        var marcasAutos = Assert.IsType<List<MarcaAuto>>(okResult.Value);
        Assert.NotEmpty(marcasAutos);
    }
}