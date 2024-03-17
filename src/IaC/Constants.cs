namespace IaC;

static class Constants
{
    private const string SolutionName = "sigma-insight";
    private const string Registry = "localhost:5000";
    private const string User = "sigma";
    private const string Label = "latest";
    
    public static readonly DeploymentSpec WebSpec = new(SolutionName, "web", 80, 5001);
    public static readonly DeploymentSpec ApiSpec = new(SolutionName, "api", 80, 5002, 2);
    
    public record DeploymentSpec(
        string AppName,
        string ServiceName,
        int TargetPort,
        int ServicePort,
        int Replicas = 1)
    {
        public string Name => AppName + "-" + ServiceName;
        public string Deployment => Name + "-deployment-1";
        public string Image => Registry + "/" + User + "/" + Name + ":" + Label;
        public string Service => Name + "-service-1";
    }
}