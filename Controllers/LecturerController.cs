using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Lecturer")]
public class LecturerController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
