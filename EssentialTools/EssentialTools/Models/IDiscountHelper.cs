using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EssentialTools.Models
{
    public interface IDiscountHelper
    {
        decimal ApplyDiscount(decimal totalParam);
    }
    public class DefaultDescountHelper : IDiscountHelper
    {
        //public decimal DiscountSize { get; set; }
        public decimal discountSize;
        public DefaultDescountHelper(decimal discountParam)
        {
            discountSize = discountParam;
        }
        public decimal ApplyDiscount(decimal totalParam)
            {
                var discont = discountSize / 100m * totalParam;
                var res =  totalParam - discont;
                return res;
            }
    }
}
