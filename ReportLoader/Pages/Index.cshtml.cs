using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ReportLoader.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ConsoleApp2.Program _program;

        // Propiedades para cada objeto que quieras pasar a la vista
        public ConsoleApp2.Root Root { get; private set; }
        public List<ConsoleApp2.Site> Sites { get; private set; }
        public List<ConsoleApp2.Alert> HighAlerts { get; private set; }
        public List<ConsoleApp2.Alert> MediumAlerts { get; private set; }
        public List<ConsoleApp2.Alert> LowAlerts { get; private set; }
        public List<ConsoleApp2.Alert> InformationalAlerts { get; private set; }

        public List<ConsoleApp2.Alert> FalsePositiveAlert { get; private set; }

        public IndexModel(ConsoleApp2.Program program)
        {
            _program = program;
        }

        public void OnGet()
        {
            _program.Main(); // Usa la instancia inyectada de Program

            // Supongamos que tienes un método en tu programa que devuelve cada objeto
            Root = _program.GetRoot();
            Sites = _program.GetSites();
            HighAlerts = _program.GetHighAlerts();
            MediumAlerts = _program.GetMediumAlerts();
            LowAlerts = _program.GetLowAlerts();
            InformationalAlerts = _program.GetInformationalAlerts();
            FalsePositiveAlert = _program.GetFalsePositiveAlerts();
        }
    }



}