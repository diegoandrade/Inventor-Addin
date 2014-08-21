using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mugen
{
    class EigenSizeOrientation
    {
        public double tetha { get; set; }
        private double L1 { get; set; }
        private double L2 { get; set; }
        private double L3 { get; set; }
        private double u11 { get; set; }
        private double u12 { get; set; }
        private double u13 { get; set; }
        private double u21 { get; set; }
        private double u22 { get; set; }
        private double u23 { get; set; }
        private double u31 { get; set; }
        private double u32 { get; set; }
        private double u33 { get; set; }

        public eigenvectors evc;
        public eigenvalues evl;


        public struct eigenvalues
        {
            public double l1 { get; set; }
            public double l2 { get; set; }
            public double l3 { get; set; }

        }

        public struct eigenvectors
        {
            public double u1 { get; set; }
            public double u2 { get; set; }
            public double u3 { get; set; }

            public double v1 { get; set; }
            public double v2 { get; set; }
            public double v3 { get; set; }

            public double w1 { get; set; }
            public double w2 { get; set; }
            public double w3 { get; set; }
        }

        public EigenSizeOrientation(double l1, double l2, double angle)
        {
            this.tetha = angle;
            this.L1 = l1;
            this.L2 = l2;

        }

        public EigenSizeOrientation(double l1, double l2, double l3, double angle)
        {
            this.tetha = angle;
            this.L1 = l1;
            this.L2 = l2;
            this.L3 = l3;

        }


        ///<Summary>
        /// Insert Comment
        ///</Summary>
        public EigenSizeOrientation(double l1, double l2, double l3,
            double u1, double u2, double u3,
            double v1, double v2, double v3,
            double w1, double w2, double w3)
        {
            this.evl.l1 = l1;
            this.evl.l2 = l2;
            this.evl.l3 = l3;
            this.evc.u1 = u1;
            this.evc.u2 = u2;
            this.evc.u3 = u3;
            this.evc.v1 = v1;
            this.evc.v2 = v2;
            this.evc.v3 = v3;
            this.evc.w1 = w1;
            this.evc.w2 = w2;
            this.evc.w3 = w3;

        }




    }
}
