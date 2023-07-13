using System;

namespace Halibut.TestUtils.Contracts
{
    public interface ICachingService
    {
        Guid NonCachableCall();
        Guid CachableCall();
        Guid AnotherCachableCall();
        Guid CachableCall(Guid input);
        Guid CachableCallThatThrowsAnExceptionWithARandomExceptionMessage(string exceptionMessagePrefix);
        Guid TwoSecondCachableCall();
    }
}