using Microsoft.AspNetCore.Mvc;
using NextHorizon.Models;
using System.Collections.Generic;

namespace NextHorizon.Controllers
{
    public class QAController : Controller
    {
        public IActionResult Dashboard()
        {
            ViewData["Title"] = "QA Dashboard";
            return View();
        }

        public IActionResult Rating(int id = 0)
        {
            ViewData["Title"] = "QA Rating Page";
            ViewData["ConversationId"] = id;

            var ticketContext = new Dictionary<int, (string Agent, string Customer, string ConversationDate)>
            {
                [10291] = ("Alyson Cruz", "Mark Reyes", "Apr 14, 2026"),
                [10290] = ("Miguel Dela Rosa", "Nina Alvarez", "Apr 14, 2026"),
                [10288] = ("Jenny Tan", "Carlo Santos", "Apr 14, 2026"),
                [10286] = ("Alyson Cruz", "Lia Garcia", "Apr 13, 2026"),
                [10283] = ("Arvin Santos", "Paolo Rivera", "Apr 13, 2026"),
                [10280] = ("Celine Ong", "Trisha Cruz", "Apr 13, 2026"),
                [10279] = ("Celine Ong", "David Lim", "Apr 13, 2026"),
                [10271] = ("Alyson Cruz", "Mark Reyes", "Apr 12, 2026"),
                [10266] = ("Miguel Dela Rosa", "Nina Alvarez", "Apr 12, 2026"),
                [10252] = ("Jenny Tan", "Carlo Santos", "Apr 11, 2026")
            };

            if (id > 0 && ticketContext.TryGetValue(id, out var context))
            {
                ViewData["ConversationAgent"] = context.Agent;
                ViewData["ConversationCustomer"] = context.Customer;
                ViewData["ConversationDate"] = context.ConversationDate;
            }
            else
            {
                ViewData["ConversationAgent"] = "Alyson Cruz";
                ViewData["ConversationCustomer"] = "Mark Reyes";
                ViewData["ConversationDate"] = "Apr 14, 2026";
            }

            var ratedSummaries = new Dictionary<int, (int[] AccuracyQuestions, int[] ToneQuestions, int[] ResolutionQuestions, string Notes, string RatedBy, string RatedOn, bool SubmittedToAgent, string SubmittedOn)>
            {
                [10271] = (new[] { 5, 5, 4 }, new[] { 5, 5, 5 }, new[] { 4, 4, 4 }, "Strong empathy and clear action steps. Minor follow-up detail could be tighter.", "QA Lead - Maria S.", "Apr 13, 2026 09:32 AM", true, "Apr 13, 2026 10:02 AM"),
                [10266] = (new[] { 5, 5, 5 }, new[] { 4, 4, 4 }, new[] { 5, 5, 5 }, "Accurate and complete resolution. Tone was good but slightly formal.", "QA Lead - Maria S.", "Apr 13, 2026 08:11 AM", false, ""),
                [10252] = (new[] { 4, 4, 4 }, new[] { 5, 5, 5 }, new[] { 5, 5, 5 }, "Great customer handling. One policy detail needed clearer wording.", "QA Analyst - Kevin T.", "Apr 12, 2026 04:26 PM", true, "Apr 12, 2026 04:40 PM")
            };

            if (id > 0 && ratedSummaries.TryGetValue(id, out var rated))
            {
                var ratedAccuracyAvg = rated.AccuracyQuestions.Average();
                var ratedToneAvg = rated.ToneQuestions.Average();
                var ratedResolutionAvg = rated.ResolutionQuestions.Average();

                var ratedAccuracy = (int)Math.Round(ratedAccuracyAvg);
                var ratedTone = (int)Math.Round(ratedToneAvg);
                var ratedResolution = (int)Math.Round(ratedResolutionAvg);

                var accuracyPoints = Math.Round((ratedAccuracyAvg / 5.0) * 35.0, 1);
                var tonePoints = Math.Round((ratedToneAvg / 5.0) * 35.0, 1);
                var resolutionPoints = Math.Round((ratedResolutionAvg / 5.0) * 30.0, 1);
                var weightedOverallPercent = Math.Round(accuracyPoints + tonePoints + resolutionPoints, 1);

                ViewData["IsRated"] = true;
                ViewData["RatedAccuracy"] = ratedAccuracy;
                ViewData["RatedTone"] = ratedTone;
                ViewData["RatedResolution"] = ratedResolution;
                ViewData["RatedAccuracyQ1"] = rated.AccuracyQuestions[0];
                ViewData["RatedAccuracyQ2"] = rated.AccuracyQuestions[1];
                ViewData["RatedAccuracyQ3"] = rated.AccuracyQuestions[2];
                ViewData["RatedToneQ1"] = rated.ToneQuestions[0];
                ViewData["RatedToneQ2"] = rated.ToneQuestions[1];
                ViewData["RatedToneQ3"] = rated.ToneQuestions[2];
                ViewData["RatedResolutionQ1"] = rated.ResolutionQuestions[0];
                ViewData["RatedResolutionQ2"] = rated.ResolutionQuestions[1];
                ViewData["RatedResolutionQ3"] = rated.ResolutionQuestions[2];
                ViewData["RatedNotes"] = rated.Notes;
                ViewData["RatedBy"] = rated.RatedBy;
                ViewData["RatedOn"] = rated.RatedOn;
                ViewData["RatedSubmittedToAgent"] = rated.SubmittedToAgent;
                ViewData["RatedSubmittedOn"] = rated.SubmittedOn;
                ViewData["RatedAccuracyPoints"] = accuracyPoints;
                ViewData["RatedTonePoints"] = tonePoints;
                ViewData["RatedResolutionPoints"] = resolutionPoints;
                ViewData["RatedOverallPercent"] = weightedOverallPercent;
            }
            else
            {
                ViewData["IsRated"] = false;
            }

            return View();
        }

        public IActionResult AllAgents()
        {
            ViewData["Title"] = "Agents";

            var agents = new List<AgentSummaryViewModel>
            {
                new AgentSummaryViewModel
                {
                    AgentName = "Alyson Cruz",
                    ResolvedTickets = 56,
                    RatedTickets = 41
                },
                new AgentSummaryViewModel
                {
                    AgentName = "Miguel Dela Rosa",
                    ResolvedTickets = 49,
                    RatedTickets = 35
                },
                new AgentSummaryViewModel
                {
                    AgentName = "Jenny Tan",
                    ResolvedTickets = 45,
                    RatedTickets = 29
                },
                new AgentSummaryViewModel
                {
                    AgentName = "Arvin Santos",
                    ResolvedTickets = 41,
                    RatedTickets = 27
                },
                new AgentSummaryViewModel
                {
                    AgentName = "Celine Ong",
                    ResolvedTickets = 38,
                    RatedTickets = 24
                }
            }
            .OrderByDescending(x => x.ResolvedTickets)
            .ThenBy(x => x.AgentName)
            .ToList();

            foreach (var item in agents)
            {
                // Keep a stable 1-5 display score without introducing fake random values.
                item.Score = item.ResolvedTickets <= 0
                    ? 0
                    : Math.Round(Math.Min(5.0, 3.5 + ((double)item.RatedTickets / item.ResolvedTickets) * 1.5), 1);
            }

            return View(agents);
        }

        public IActionResult AllResolvedTickets()
        {
            ViewData["Title"] = "Resolved Tickets Awaiting QA";
            return View();
        }

        public IActionResult AgentTickets(int id = 1, string? agentName = null)
        {
            ViewData["Title"] = "Agent Tickets";
            ViewData["AgentId"] = id;
            ViewData["AgentName"] = !string.IsNullOrWhiteSpace(agentName) ? agentName : id switch
            {
                1 => "Alyson Cruz",
                2 => "Miguel Dela Rosa",
                3 => "Jenny Tan",
                4 => "Arvin Santos",
                5 => "Ramon Lee",
                6 => "Celine Ong",
                _ => "Unknown Agent"
            };
            return View();
        }

        public IActionResult RatedHistory()
        {
            ViewData["Title"] = "Rated History";
            return View();
        }
    }
}
