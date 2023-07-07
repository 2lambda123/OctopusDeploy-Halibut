using System;
using System.Threading.Tasks;
using Halibut.Exceptions;
using Halibut.Tests.Support;
using Halibut.Tests.Support.BackwardsCompatibility;
using Halibut.Tests.Support.TestAttributes;
using Halibut.Tests.TestServices;
using NUnit.Framework;

namespace Halibut.Tests.BackwardsCompatibility
{
    public class InvocationExceptionsAreMappedCorrectlyFixture
    {
        [Test]
        [TestCaseSource(typeof(ServiceConnectionTypesToTest))]
        public async Task OldInvocationExceptionMessages_AreMappedTo_ServiceInvocationHalibutClientException(ServiceConnectionType serviceConnectionType)
        {
            using (var clientAndService = await ClientAndPreviousServiceVersionBuilder
                       .ForServiceConnectionType(serviceConnectionType)
                       .WithServiceVersion(PreviousServiceVersions.v5_0_429)
                       .Build())
            {
                var echo = clientAndService.CreateClient<IEchoService>(se =>
                {
                    se.PollingRequestQueueTimeout = TimeSpan.FromSeconds(20);
                    se.PollingRequestMaximumMessageProcessingTimeout = TimeSpan.FromSeconds(20);
                });

                var ex = Assert.Throws<ServiceInvocationHalibutClientException>(() => echo.Crash());
            }
        }
    }
}