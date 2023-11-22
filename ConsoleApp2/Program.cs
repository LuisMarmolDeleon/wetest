using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace ConsoleApp2
{
    public class Root
    {
        public string? version { get; set; }
        public string? generated { get; set; }
        public List<Site>? site { get; set; }

        public int HighVulnerabilityScore { get; set; }
        public int HighCount { get; set; }
        public int LowVulnerabilityScore { get; set; }
        public int LowCount { get; set; }
        public int MediumVulnerabilityScore { get; set; }
        public int MediumCount { get; set; }
        public int InformativeVulnerabilityScore { get; set; }
        public int InformativeCount { get; set; }

        public int FalsePositiveCount { get; set; }

        public int WebsiteScore { get; set; }
    }

    public class Site
    {
        public string? name { get; set; }
        public string? host { get; set; }
        public string? port { get; set; }
        public string? ssl { get; set; }
        public List<Alert>? alerts { get; set; }
    }

    public class Alert
    {
        public string? pluginid { get; set; }
        public string? alertRef { get; set; }
        public string? alert { get; set; }
        public string? name { get; set; }
        public string? riskcode { get; set; }
        public string? confidence { get; set; }
        public string? riskdesc { get; set; }
        public string? desc { get; set; }
        public List<Instance>? instances { get; set; }
        public string? count { get; set; }
        public string? solution { get; set; }
        public string? otherinfo { get; set; }
        public string? reference { get; set; }
        public string? cweid { get; set; }
        public string? wascid { get; set; }
        public string? sourceid { get; set; }
    }

    public class Instance
    {
        public string? uri { get; set; }
        public string? method { get; set; }
        public string? param { get; set; }
        public string? attack { get; set; }
        public string? evidence { get; set; }
        public string? otherinfo { get; set; }
    }

    public class Program
    {
        //var json = System.IO.File.ReadAllText("C:\\settings\\reports\\19-09-2023\\Tesfst\\report-0.json");

        public static Root roott = new Root(); // Haz de root una variable de instancia
        public static List<Alert> highAlerts = new List<Alert>();
        public static List<Alert> mediumAlerts = new List<Alert>();
        public static List<Alert> lowAlerts = new List<Alert>();
        public  static List<Alert> informationalAlerts = new List<Alert>();
        public static List <Alert> FalsePositiveAlerts = new List<Alert>();

        public void Main()
        {
            RiskFinder();
            Root r = GetRoot();
            Console.WriteLine(r.WebsiteScore);
        }

        public static void Main(string[] args)
        {
            Program program = new Program();
            program.Main();
        }

        public int Validation()
        {
            int OStype = 0;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                OStype = 1;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                OStype = 1;
            }
            return OStype;
        }

        //Obtiene los riesgos y los divide en 
        static void RiskFinder()
        {
            var json = System.IO.File.ReadAllText("report-0.json");
            Root root = JsonConvert.DeserializeObject<Root>(json);
            roott = root;
            string Risk;

            foreach (Site site in root.site)
            {
                foreach (Alert alert in site.alerts)
                {
                    Risk = RiskIdentifier(alert.riskdesc);
                    switch (Risk)
                    {
                        case "High":
                            highAlerts.Add(alert);
                            root.HighVulnerabilityScore += 4;
                            root.HighCount += 1;
                            break;
                        case "Medium":
                            mediumAlerts.Add(alert);
                            root.MediumVulnerabilityScore += 3;
                            root.MediumCount += 1;
                            break;
                        case "Low":
                            lowAlerts.Add(alert);
                            root.LowVulnerabilityScore += 2;
                            root.LowCount += 1;
                            break;
                        case "Informational":
                            informationalAlerts.Add(alert);
                            root.InformativeVulnerabilityScore += 1;
                            root.InformativeCount += 1;
                            break;
                        case "False":
                            FalsePositiveAlerts.Add(alert);
                            root.FalsePositiveCount += 1;
                            break;
                        default:
                            break;
                    }
                }
                root.WebsiteScore = 100 - (root.HighVulnerabilityScore + root.InformativeVulnerabilityScore + root.MediumVulnerabilityScore + root.LowVulnerabilityScore);
                Console.WriteLine("Website Score: " + Convert.ToInt32(root.WebsiteScore));
                PrintAlerts("High", highAlerts);
                PrintAlerts("Medium", mediumAlerts);
                PrintAlerts("Low", lowAlerts);
                PrintAlerts("Informational", informationalAlerts);
            }
        }

        static void PrintAlerts(string riskdesc, List<Alert> alerts)
        {
            Console.WriteLine($"\\{riskdesc} Alerts:");
            foreach (var alert in alerts)
            {
                Console.WriteLine($"- {alert.name}");
            }
        }

        public static string RiskIdentifier(string palabra)
        {
            string[] WordDivided = palabra.Split(' ');
            return WordDivided[0];
        }

        public  List<Alert> GetHighAlerts() => highAlerts;
        public  List<Alert> GetMediumAlerts() => mediumAlerts;
        public  List<Alert> GetLowAlerts() => lowAlerts;
        public  List<Alert> GetInformationalAlerts() => informationalAlerts;

        public List<Alert> GetFalsePositiveAlerts() => FalsePositiveAlerts;

        public  Root GetRoot() => roott;

        public List<Site> GetSites() => roott.site;
    }
}


