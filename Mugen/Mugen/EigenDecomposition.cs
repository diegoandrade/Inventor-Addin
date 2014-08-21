/**
 * Author : Diego Andrade
 * Date : 03/25/2014
 * Project : Styling and functional pattern design
 * Codename : BREPCORE
 * Carnegie Mellon University
 * CERLAB
 * All rights reserved 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mugen
{
    //CHANGE NAME!!! It should be something like 
    class EigenDecomposition
    {
        public string idS { get; set; }
        public double m11 { get; set; }
        public double m12 { get; set; }
        public double m13 { get; set; }
        public double m22 { get; set; }
        public double m23 { get; set; }
        public double m33 { get; set; }

        public EigenDecomposition(string V, double v1, double v2, double v3)
        {
            this.idS = V;
            this.m11 = v1;
            this.m12 = v2;
            this.m22 = v3;
        }

        public EigenDecomposition(string V, double v1, double v2, double v3, double v4, double v5, double v6)
        {
            this.idS = V;
            this.m11 = v1;
            this.m12 = v2;
            this.m13 = v3;
            this.m22 = v4;
            this.m23 = v5;
            this.m33 = v6;
        }
    }
}
