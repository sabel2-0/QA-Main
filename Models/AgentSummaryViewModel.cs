namespace NextHorizon.Models
{
    public class AgentSummaryViewModel
    {
        public string AgentName { get; set; } = string.Empty;
        public int ResolvedTickets { get; set; }
        public int RatedTickets { get; set; }
        public double Score { get; set; }
    }
}
