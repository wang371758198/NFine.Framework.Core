using System;
using System.Collections.Generic;
using System.Text;
using NFine.Code;
using Xunit;

namespace NFine.XUnitTesting.NFine.Code
{
   public class Md5_Test
    {
        [Fact]
        public void Md5Test()
        {
            var result = Md5.md5("aaaaa", 16);
            Assert.Equal(result.Length, 16);
            Console.WriteLine(result);
        }
    }
}
