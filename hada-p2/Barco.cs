using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Barco
    {
        Dictionary<string, int> barcos = new Dictionary<string, int>()
        {
            {"Porta-aviões", 5},
            {"Navio-tanque", 4},
            {"Cruzador", 3},
            {"Submarino", 3},
            {"Destroyer", 2}
        };
    }
}
