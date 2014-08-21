/**
 * Author : Diego Andrade
 * Date : 03/25/2014
 * Project : Styling and functional pattern design
 * Codename : BREPCORE
 * Carnegie Mellon University
 * CERLAB
 * All rights reserved 
 */

/// <summary>
/// This class stores the information of the vertices extrated from SRF files.
/// These vertices are the base for the operations and are spread into four different possiblities
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mugen
{
    class VerticesOutput
    {

         public int id { get; set; }
         public char idC { get; set; }
         public string idS { get; set; }

         public double x { get; set; }
         public double y { get; set; }
         public double z { get; set; }
         public double value4 { get; set; }
         public double value5 { get; set; }
         public double value6 { get; set; }

         public string idSV { get; set; }
         public string idSF { get; set; }
         public int int_value1 { get; set; }
         public int int_value2 { get; set; }
         public int int_value3 { get; set; }

         public or origen;
         public vc vertexconn;

        public struct or
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }

        };

        public struct vc
        {
            public double V1 { get; set; }
            public double V2 { get; set; }
            public double V3 { get; set; }

        };

        public VerticesOutput() {}

        public VerticesOutput(string V, int v1, int v2, int v3)
        {
            this.idSV = V;
            this.int_value1 = v1;
            this.int_value2 = v2;
            this.int_value3 = v3;

            this.vertexconn.V1 = v1;
            this.vertexconn.V2 = v2;
            this.vertexconn.V3 = v3;

        }

        //public VerticesOutput(int id, double v1, double v2, double v3, double v4, double v5, double v6)
        //{
        //    this.id = id;
        //    this.value1 = v1;
        //    this.value2 = v2;
        //    this.value3 = v3;
        //    this.value4 = v4;
        //    this.value5 = v5;
        //    this.value6 = v6;
        //}

        //public VerticesOutput(double v1, double v2, double v3)
        //{
        //    this.value1 = v1;
        //    this.value2 = v2;
        //    this.value3 = v3;
        //}

        //public VerticesOutput(char V, double v1, double v2, double v3)
        //{
        //    this.idC = V;
        //    this.value1 = v1;
        //    this.value2 = v2;
        //    this.value3 = v3;

          

        //}

        public VerticesOutput(string V, double v1, double v2, double v3)
        {
            this.idS = V;
            this.x = v1;
            this.y = v2;
            this.z = v3;

            this.origen.X = v1;
            this.origen.Y = v2;
            this.origen.Z = v3;

        }

        //public VerticesOutput(double v1, double v2, double v3)
        //{
        //    origen.X = v1;
        //    origen.Y = v2;
        //    origen.Z = v3;
        //}

        //public VerticesOutput(int v1, int v2, int v3)
        //{
        //    vertexconn.V1 = v1;
        //    vertexconn.V2 = v2;
        //    vertexconn.V3 = v3;
        //}
      
    }
}
