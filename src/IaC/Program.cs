using Pulumi;
using Pulumi.Kubernetes.Types.Inputs.Core.V1;
using Pulumi.Kubernetes.Types.Inputs.Apps.V1;
using Pulumi.Kubernetes.Types.Inputs.Meta.V1;
using System.Collections.Generic;
using IaC;
using Pulumi.Kubernetes.Core.V1;

void BuildDeployment(Constants.DeploymentSpec spec)
{
    var appLabels = new InputMap<string>
    {
        { "app.kubernetes.io/name", spec.Name }
    };

    _ = new Pulumi.Kubernetes.Apps.V1.Deployment(spec.Deployment, new DeploymentArgs
    {
        Spec = new DeploymentSpecArgs
        {
            Selector = new LabelSelectorArgs
            {
                MatchLabels = appLabels
            },
            Replicas = spec.Replicas,
            Template = new PodTemplateSpecArgs
            {
                Metadata = new ObjectMetaArgs
                {
                    Labels = appLabels
                },
                Spec = new PodSpecArgs
                {
                    Containers =
                    {
                        new ContainerArgs
                        {
                            Name = spec.Name,
                            Image = spec.Image,
                            Ports =
                            {
                                new ContainerPortArgs
                                {
                                    ContainerPortValue = spec.TargetPort,
                                }
                            }
                        },
                    }
                }
            }
        }
    });
    
    _ = new Service(spec.Service, new ServiceArgs
    {
        Spec = new ServiceSpecArgs
        {
            Type = ServiceSpecType.LoadBalancer,
            Selector = appLabels,
            Ports =
            {
                new ServicePortArgs
                {
                    Port = spec.ServicePort,
                    TargetPort = spec.TargetPort,
                }
            }
        }
    });
}

return await Deployment.RunAsync(() =>
{
    BuildDeployment(Constants.WebSpec);
    BuildDeployment(Constants.ApiSpec);

    // export the deployment name
    return new Dictionary<string, object?>
    {
        // ["name"] =  deployment.Metadata.Apply(m => m.Name)
    };
    
});