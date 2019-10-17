using System;
using System.Collections.Generic;
using System.Text;

namespace wwwTools.依赖注入_Test
{
    public class WwwDemo_DI : IWwwDemo_DI
    {
        public string GetWww()
        {
            return $"www";
        }
    }
}
