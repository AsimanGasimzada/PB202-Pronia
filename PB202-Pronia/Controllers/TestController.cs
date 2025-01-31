using Microsoft.AspNetCore.Mvc;
using PB202_Pronia.Models;

namespace PB202_Pronia.Controllers;

public class TestController : Controller
{
    private readonly Singleton _singleton;
    private readonly Scoped _scoped;
    private readonly Scoped _scoped2;
    private readonly Transient _transient;
    private readonly Transient _transient2;
    public TestController(Singleton singleton, Scoped scoped, Transient transient, Transient transient2, Scoped scoped2)
    {
        _singleton = singleton;
        _scoped = scoped;
        _transient = transient;
        _transient2 = transient2;
        _scoped2 = scoped2;
    }

    public IActionResult Index()
    {
        _singleton.Increase();
        _singleton.Increase();

        _transient.Increase();
        _transient2.Increase();


        _scoped.Increase();
        _scoped2.Increase();


        return Content($"Singleton: {_singleton.Count}   \n  Scoped: {_scoped.Count}   \n Transient {_transient.Count}");
    }

    public IActionResult Index2()
    {
        _singleton.Increase();
        _singleton.Increase();

        _transient.Increase();
        _transient2.Increase();


        _scoped.Increase();
        _scoped2.Increase();


        return Content($"Singleton: {_singleton.Count}   \n  Scoped: {_scoped.Count}   \n Transient {_transient.Count}");
    }
}
