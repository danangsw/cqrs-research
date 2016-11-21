using EventFlow.Logs;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.TestHelpers.Extensions
{
    public static class MockLogExtensions
    {
        public static void VerifyNoErrorsLogged(this Mock<ILog> logMock)
        {
            logMock.Verify(
                m => m.Error(It.IsAny<Exception>(), It.IsAny<string>(), It.IsAny<object[]>()),
                Times.Never);
            logMock.Verify(
                m => m.Error(It.IsAny<string>(), It.IsAny<object[]>()),
                Times.Never);
        }
    }
}
