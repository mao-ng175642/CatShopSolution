using System;
using System.Collections.Generic;
using System.Text;

namespace CatShopSolution.Utilitils.Exceptions
{
    public class CatShopException: Exception
    {
        public CatShopException()
        {
        }

        public CatShopException(string message)
            : base(message)
        {
        }

        public CatShopException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
