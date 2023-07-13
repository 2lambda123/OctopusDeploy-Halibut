using Halibut.ServiceModel;
using Halibut.TestUtils.SampleProgram.Base.Services;

namespace Halibut.TestUtils.SampleProgram.Base
{
    public class ServiceFactoryFactory
    {
        /// <summary>
        /// Used when this external binary has the service.
        /// ie Old/Previous Service.
        /// </summary>
        /// <returns></returns>
        public static DelegateServiceFactory CreateServiceFactory()
        {
            var services = new DelegateServiceFactory();
            services.Register<IEchoService>(() => new EchoService());
            services.Register<ICachingService>(() => new CachingService());
            services.Register<IMultipleParametersTestService>(() => new MultipleParametersTestService());
            return services;
        }
        
        /// <summary>
        /// Used when the test CLR has the services
        /// ie Old/Previous Client calling latest services.
        /// </summary>
        /// <param name="clientWhichTalksToLatestHalibut"></param>
        /// <param name="realServiceEndpoint"></param>
        /// <returns></returns>
        public static DelegateServiceFactory CreateProxyingServicesServiceFactory(HalibutRuntime clientWhichTalksToLatestHalibut, ServiceEndPoint realServiceEndpoint)
        {
            var forwardingEchoService = clientWhichTalksToLatestHalibut.CreateClient<IEchoService>(realServiceEndpoint);
            var services = new DelegateServiceFactory();
            services.Register<IEchoService>(() => new DelegateEchoService(forwardingEchoService));
            return services;
        }
    }
}