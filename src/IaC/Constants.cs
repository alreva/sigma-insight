namespace IaC;

static class Constants
{
    private const string SolutionName = "sigma-insight";
    private const string Registry = "localhost:5000";
    private const string User = "sigma";
    private const string Label = "latest";
    
    public static readonly DeploymentSpec WebSpec = new(SolutionName, "web", 80, 5001);
    public static readonly DeploymentSpec ApiSpec = new(SolutionName, "api", 80, 5002, 2);
    
    public class DeploymentSpec
    {
        public DeploymentSpec(
            string appName,
            string serviceName,
            int targetPort,
            int servicePort,
            int replicas = 1)
        {
            AppName = appName;
            ServiceName = serviceName;
            TargetPort = targetPort;
            ServicePort = servicePort;
            Replicas = replicas;
        }

        public string Name => AppName + "-" + ServiceName;
        public string Deployment => Name + "-deployment-1";
        public string Image => Registry + "/" + User + "/" + Name + ":" + Label;
        public string Service => Name + "-service-1";
        public string AppName { get; init; }
        public string ServiceName { get; init; }
        public int TargetPort { get; init; }
        public int ServicePort { get; init; }
        public int Replicas { get; init; }

        public void Deconstruct(
            out string appName,
            out string serviceName,
            out int targetPort,
            out int servicePort,
            out int replicas)
        {
            appName = AppName;
            serviceName = ServiceName;
            targetPort = TargetPort;
            servicePort = ServicePort;
            replicas = Replicas;
        }
    }
}