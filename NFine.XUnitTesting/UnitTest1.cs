using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace NFine.XUnitTesting
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            List<string> list = new List<string>();
            Console.WriteLine("中文");
            string aaa = "中文信息";
            
            Assert.Empty(list);
        }

        

    }
}
