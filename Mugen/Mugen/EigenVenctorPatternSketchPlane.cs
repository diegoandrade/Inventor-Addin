using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Inventor;

namespace Mugen
{
    class EigenVectorPatternSketchPlane
    {

        public EigenVectorPatternSketchPlane() { }

        public PlanarSketch SelectPlane(PartDocument oDoc, UserCoordinateSystem oUCS)
        {
            PlanarSketch oSketch =  oDoc.ComponentDefinition.Sketches.Add(oUCS.XYPlane, false);
            return oSketch;
        }

        public PlanarSketch SelectPlane(PartDocument oDoc, UserCoordinateSystem oUCS, double u1, double u2, double u3, double v1, double v2, double v3)
        {

            PlanarSketch oSketch;

            if (u1 >= 0 && u2 >= 0 && u3 >= 0 && v1>=0 && v2<0 && v3<0)
            {
                 oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.XYPlane, false);
            }
            else if (u1 < 0 && u2 < 0 && u3 < 0 && v1<0 && v2>=0 && v3>=0)
            {
                 oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.XYPlane, false);
            }
            else if (u1 < 0 && u2 < 0 && u3 < 0 && v1 < 0 && v2 >=0 && v3 < 0)
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.YZPlane, false);
            }
            else if (u1 >= 0 && u2 >= 0 && u3 >= 0 && v1 >= 0 && v2 < 0 && v3 >= 0)
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.YZPlane, false);
            }
            else if (u1 >= 0 && u2 < 0 && u3 >= 0 && v1 >= 0 && v2 < 0 && v3 < 0)
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.XYPlane, false);
            }
            else if (u1 < 0 && u2 >= 0 && u3 < 0 && v1 < 0 && v2 >= 0 && v3 >= 0)
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.XYPlane, false);
            }
            else if (u1 < 0 && u2 < 0 && u3 < 0 && v1 < 0 && v2 < 0 && v3 >= 0)
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.YZPlane, false);
            }
            else if (u1 < 0 && u2 < 0 && u3 < 0 && v1 < 0 && v2 < 0 && v3 >= 0 )
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.YZPlane, false);
            }
            else
            {
                 oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.XZPlane, false);
            }


            return oSketch;

        }

        

        public PlanarSketch SelectPlane(PartDocument oDoc, UserCoordinateSystem oUCS, 
            double u1, double u2, double u3, 
            double v1, double v2, double v3,
            double w1, double w2, double w3)
        {

            PlanarSketch oSketch;

            if (u1 >= 0 && u2 >= 0 && u3 >= 0 && v1 < 0 && v2 < 0 && v3 >= 0 && w1 < 0 && w2 >= 0 && w3 < 0)
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.XZPlane, false);
            }
            else if (u1 < 0 && u2 < 0 && u3 < 0 && v1 >= 0 && v2 >= 0 && v3 < 0 && w1 >= 0 && w2 < 0 && w3 >= 0)
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.YZPlane, false);
            }
            else if (u1 >= 0 && u2 >= 0 && u3 < 0 && v1 >= 0 && v2 < 0 && v3 >= 0 && w1 >= 0 && w2 >= 0 && w3 >= 0)
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.YZPlane, false);
            }
            else if (u1 < 0 && u2 < 0 && u3 < 0 && v1 < 0 && v2 < 0 && v3 >= 0 && w1 < 0 && w2 >= 0 && w3 >= 0)
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.YZPlane, false);
            }
            else if (u1 >= 0 && u2 >= 0 && u3 < 0 && v1 < 0 && v2 >= 0 && v3 >= 0 && w1 >= 0 && w2 >= 0 && w3 >= 0)
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.XYPlane, false); ////*******change here 
            }
            else if (u1 >= 0 && u2 >= 0 && u3 < 0 && v1 < 0 && v2 >= 0 && v3 >= 0 && w1 >= 0 && w2 < 0 && w3 >= 0)
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.YZPlane, false);
            }
            else if (u1 < 0 && u2 >= 0 && u3 >= 0 && v1 < 0 && v2 < 0 && v3 >= 0 && w1 >= 0 && w2 >= 0 && w3 >= 0)
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.XYPlane, false);
            }
            else if (u1 < 0 && u2 < 0 && u3 >= 0 && v1 >= 0 && v2 < 0 && v3 < 0 && w1 >= 0 && w2 < 0 && w3 >= 0)
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.YZPlane, false);
            }
            else if (u1 < 0 && u2 < 0 && u3 >= 0 && v1 < 0 && v2 >= 0 && v3 >= 0 && w1 >= 0 && w2 >= 0 && w3 >= 0)
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.YZPlane, false);
            }
            else if (u1 >= 0 && u2 >= 0 && u3 < 0 && v1 >= 0 && v2 < 0 && v3 < 0 && w1 >= 0 && w2 >= 0 && w3 >= 0)
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.XYPlane, false); //Here there is aproblem since there the plnae is not tangent CHANGE HERE!!
            }
            else if (u1 < 0 && u2 < 0 && u3 < 0 && v1 >= 0 && v2 < 0 && v3 < 0 && w1 >= 0 && w2 < 0 && w3 >= 0)
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.YZPlane, false);
            }
            else if (u1 < 0 && u2 < 0 && u3 >= 0 && v1 < 0 && v2 >= 0 && v3 < 0 && w1 >= 0 && w2 >= 0 && w3 >= 0)
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.YZPlane, false);
            }
            else if (u1 < 0 && u2 >= 0 && u3 >= 0 && v1 >= 0 && v2 < 0 && v3 >= 0 && w1 < 0 && w2 < 0 && w3 < 0)
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.XYPlane, false);
            }
            else if (u1 < 0 && u2 >= 0 && u3 >= 0 && v1 >= 0 && v2 < 0 && v3 < 0 && w1 >= 0 && w2 < 0 && w3 >= 0)
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.XZPlane, false);
            }
            else if (u1 < 0 && u2 >= 0 && u3 >= 0 && v1 >= 0 && v2 < 0 && v3 < 0 && w1 >= 0 && w2 < 0 && w3 >= 0)
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.XZPlane, false);
            }
            else if (u1 >= 0 && u2 >= 0 && u3 >= 0 && v1 >= 0 && v2 < 0 && v3 >= 0 && w1 < 0 && w2 >= 0 && w3 >= 0)
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.YZPlane, false);
            }
            else if (u1 >= 0 && u2 >= 0 && u3 >= 0 && v1 >= 0 && v2 < 0 && v3 >= 0 && w1 < 0 && w2 < 0 && w3 >= 0)
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.YZPlane, false);
            }
            else if (u1 >= 0 && u2 >= 0 && u3 >= 0 && v1 >= 0 && v2 < 0 && v3 < 0 && w1 < 0 && w2 < 0 && w3 >= 0)
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.YZPlane, false);
            }
            else if (u1 >= 0 && u2 >= 0 && u3 >= 0 && v1 >= 0 && v2 >= 0 && v3 >= 0 && w1 >= 0 && w2 >= 0 && w3 >= 0)
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.XYPlane, false); //It could happen that everything is + and mot XY plane CHAGE HERE!!
            }
            else if (u1 >= 0 && u2 >= 0 && u3 >= 0 && v1 >= 0 && v2 < 0 && v3 >= 0 && w1 >= 0 && w2 >= 0 && w3 < 0)
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.XYPlane, false); 
            }
            else if (u1 >= 0 && u2 >= 0 && u3 >= 0 && v1 >= 0 && v2 < 0 && v3 < 0 && w1 >= 0 && w2 >= 0 && w3 < 0)
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.XYPlane, false);
            }
            else if (u1 < 0 && u2 < 0 && u3 < 0 && v1 < 0 && v2 >= 0 && v3 < 0 && w1 >= 0 && w2 >= 0 && w3 < 0)
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.XYPlane, false); 
            }
            else if (u1 >= 0 && u2 < 0 && u3 >= 0 && v1 >= 0 && v2 < 0 && v3 < 0 && w1 < 0 && w2 < 0 && w3 < 0)
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.XYPlane, false);
            }
            else if (u1 < 0 && u2 < 0 && u3 < 0 && v1 < 0 && v2 >= 0 && v3 >= 0 && w1 >= 0 && w2 >= 0 && w3 < 0)
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.XYPlane, false);
            }
            else if (u1 >= 0 && u2 >= 0 && u3 < 0 && v1 < 0 && v2 >= 0 && v3 < 0 && w1 >= 0 && w2 >= 0 && w3 >= 0)
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.XYPlane, false);
            }
            else if (u1 < 0 && u2 < 0 && u3 >= 0 && v1 >= 0 && v2 < 0 && v3 < 0 && w1 >= 0 && w2 >= 0 && w3 >= 0)
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.XYPlane, false);
            }
            else
            {
                oSketch = oDoc.ComponentDefinition.Sketches.Add(oUCS.XZPlane, false);
            }


            return oSketch;

        }



    }
}
