using EventFlow.TestHelpers;
using NUnit.Framework;
using FluentAssertions;
using System.Collections.Generic;
using EventFlow.TestHelpers.Aggregates;
using System;

namespace Jmerp.Commons.Tests.UnitTests
{
    [Category(Categories.Unit)]
    public class ResponseResultTests : Test
    {
        [Test]
        public void Return_ErrorResponseResult()
        {
            //Arrange
            var errors = new List<ResponseError>() { new ResponseError("201","1 Some specs are not satisfied"),
                new ResponseError("203","2 Some specs are not satisfied")};

            //Act
            var result = ThingyErrorReturnService(errors.ToArray());

            //Assert
            result.Succeeded.Should().BeFalse();
            result.Errors.Should().HaveCount(errors.Count);
            result.ToString().Should().NotBeEmpty();
        }

        [Test]
        public void Return_SimpleResponseResult()
        {
            //Arrange
            var success = new List<string>() { "thingy-da7ab6b1-c513-581f-a1a0-7cdf17109deb" };

            //Act
            var result = ThingySuccessReturnService(success);

            //Assert
            result.Succeeded.Should().BeTrue();
            result.Responses.Should().HaveCount(success.Count);
            result.ToString().Should().BeSameAs("Succeeded");
        }

        [Test]
        public void Return_ThingyResponseResult()
        {
            //Arrange
            var thingyA = new ThingyPoco()
            {
                ThingyId = ThingyId.New, ThingyInt = 1, ThingyString = "ABC", ThingyDateTime = DateTime.Now,
                ThingySubClass = new ThingySubClass { ThingyInt = 12, ThingyString = "ABC2" }
            };
            var thingyB = new ThingyPoco()
            {
                ThingyId = ThingyId.New, ThingyInt = 2, ThingyString = "DEF", ThingyDateTime = DateTime.Now,
                ThingySubClass = new ThingySubClass { ThingyInt = 22, ThingyString = "DEF2" }
            };
            var success = new List<ThingyPoco>() { thingyA, thingyB };

            //Act
            var result = ThingySuccessReturnService(success);

            //Assert
            result.Succeeded.Should().BeTrue();
            result.Responses.Should().HaveCount(success.Count);
            result.ToString().Should().BeSameAs("Succeeded");
        }

        private ResponseResult ThingyErrorReturnService(params ResponseError[] errors)
        {
            return ResponseResult.Failed(errors);
        }

        private ResponseResult ThingySuccessReturnService(List<string> success)
        {
            return ResponseResult.Succeed(success);
        }

        private ResponseResult ThingySuccessReturnService(List<ThingyPoco> success)
        {
            return ResponseResult.Succeed(success);
        }
    }

    public class ThingyPoco
    {
        public ThingyId ThingyId { get; set; }
        public int ThingyInt { get; set; }
        public string ThingyString { get; set; }
        public DateTime ThingyDateTime { get; set; }
        public ThingySubClass ThingySubClass { get; set; }
    }

    public class ThingySubClass
    {
        public int ThingyInt { get; set; }
        public string ThingyString { get; set; }
    }
}
