using Microsoft.AspNetCore.Mvc;

namespace NextHorizon.Controllers
{
    public class AgentController : Controller
    {
        // This action method corresponds to AgentDashboard.cshtml
        // URL Path: /Agent/AgentDashboard (or just /Agent if set as default)
        public IActionResult AgentDashboard()
        {
            ViewData["Title"] = "Agent Dashboard";
            return View(); 
        }

        // This action method corresponds to Tickets.cshtml
        // URL Path: /Agent/Tickets
        public IActionResult Tickets()
        {
            ViewData["Title"] = "Agent Tickets";
            return View();
        }
    }
}