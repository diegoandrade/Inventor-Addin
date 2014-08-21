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
using System.Diagnostics;

using System.Windows.Forms;

using System.IO;

//Logs
//using log4net;

//Matrix Mathnet
using MathNet.Numerics.LinearAlgebra.Double;

//for CultureInfo
using System.Globalization;

/* TODO:
 * [ ] Read Meshanid.srf files 
 * [ ] Read from a config file what files to read
 * [ ] Autosave data once read file is finalized
 * */

namespace Mugen
{

    /// <summary>
    /// This Class contains the strucutres to manage bubble location, anisotropy and other 
    /// features needed to interoperate with Autodesk Inventor. It is used in conjuction with 
    /// other classes such as:
    /// 
    /// 
    /// 
    /// 
    /// </summary>

    class AutodeskInventorInterop
    {



        //Defining log 
       // private static readonly ILog log = LogManager.GetLogger(typeof(BREPCore));

        //Vertex and Location Output
        public List<VerticesOutput> VertexLocation = new List<VerticesOutput>();
        public List<VerticesOutput> VertexConnectivity = new List<VerticesOutput>();

        //Use to read the nt3m files
        public List<EigenDecomposition> EigenList = new List<EigenDecomposition>();

        //Use to store values of eigen sizes and directions tetha
        public List<EigenSizeOrientation> EigenVectorValue = new List<EigenSizeOrientation>();

        //Use to read values from SRF
        public List<VerticesOutput> SRFVeticesMeshD = new List<VerticesOutput>();


       

         ///<Summary>
         /// Constructor
         ///</Summary>
        public AutodeskInventorInterop()
        {
            //Defines location of config file for log
           //// log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "log4net.config"));
        }

        public void ReadFileDataToList(string filename)
        {

            try
            {
                filename = "\\" + filename;
                string exePath = ""; // Path for the file containing SRF information
                exePath = System.Windows.Forms.Application.StartupPath + @filename;

              //  log.Info("ReadFileDataToList " + filename.ToString());

                StreamReader sr = new StreamReader(exePath);
                bool foundF = false;
                int lineCount = 0;   //line count for SRF file
                int lineCountVertexConnectivity = 0; // line count for SRF file with vertex connectivity information

                while (!sr.EndOfStream)
                {

                    string[] split = sr.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (split[0] == "Surf")
                    {
                       // log.Info("FOUND SURF");
                      //  Debug.WriteLine("Found Surf");

                    }
                    else if (split[0] == "F")
                    {
                      //  log.Info("FOUND F");
                      //  Debug.WriteLine("FOUND F");
                        foundF = true;
                    }
                    else if (split[0] == "V" && foundF == false)
                    {
                        lineCount++;

                        VertexLocation.Add(new VerticesOutput(
                               Convert.ToString(split[0]),
                               Convert.ToDouble(split[1]),
                               Convert.ToDouble(split[2]),
                               Convert.ToDouble(split[3])));

                        //Debug.WriteLine(lineCount.ToString());
                    }
                    else if (split[0] == "V" && foundF == true)
                    {
                       // Debug.WriteLine("FOUND F V");
                        foundF = true;

                        lineCountVertexConnectivity++;


                        VertexConnectivity.Add(new VerticesOutput(
                               Convert.ToString(split[0]),
                               Convert.ToInt32(split[1]),
                               Convert.ToInt32(split[2]),
                               Convert.ToInt32(split[3])));
                       
                       // Debug.WriteLine(lineCountVertexConnectivity.ToString());
                    }
                }


            }
            catch (Exception ex)
            {
                //The expection info goes here
                string Message = ex.ToString() + "\n\n\n\n ERROR 3000 : Error reading " + filename.ToString() + ".srf file";
                string Caption = "Styling and Functional Pattern Design";

               // log.Info("ERROR 3000 : Error reading " + filename.ToString() + ".srf file");

                MessageBoxButtons Buttons = MessageBoxButtons.RetryCancel;

                DialogResult Result = MessageBox.Show(
                    Message,
                    Caption,
                    Buttons,
                    MessageBoxIcon.Error);

            }

        }

        public void ReadFileDataToList_nt3m(string filename)
        {
            try
            {

                filename = "\\" + filename;
                string exePath = ""; // Path for the file containing SRF information
                exePath = System.Windows.Forms.Application.StartupPath + @filename;

            //    log.Info("ReadFileDataToList_nt3m " + filename.ToString());

                StreamReader sr = new StreamReader(exePath); //It works with Sytem IO

                int counter = 0;

                while (!sr.EndOfStream)
                {

                    string[] split = sr.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    //Debug.WriteLine("Column(" + split.ToString() + "): " + VolmeshInteraction.startWithColumn(split.ToString()).ToString());
                    counter++;
                    //Debug.WriteLine(counter.ToString());

                    //float val = (float)System.Convert.ToSingle(text);

                    //The files contains an extra value set to '0' by Ved wating his change on tensorclie 03/17/2014
                    EigenList.Add(new EigenDecomposition(
                                 (string)Convert.ToString(split[0]),
                                 (double)Convert.ToDouble(split[2]),
                                 (double)Convert.ToDouble(split[3]),
                                 (double)Convert.ToDouble(split[4]),
                                 (double)Convert.ToDouble(split[5]),
                                 (double)Convert.ToDouble(split[6]),
                                 (double)Convert.ToDouble(split[7])
                                 ));

                }


            }
            catch (Exception ex)
            {
                //The expection info goes here
                string Message = ex.ToString() + "\n\n\n\n ERROR 3010 : Error reading " + filename.ToString() + ".srf file";
                string Caption = "Styling and Functional Pattern Design";

               // log.Info("ERROR 3010 : Error reading " + filename.ToString() + ".srf file");

                MessageBoxButtons Buttons = MessageBoxButtons.RetryCancel;

                DialogResult Result = MessageBox.Show(
                    Message,
                    Caption,
                    Buttons,
                    MessageBoxIcon.Error);

            }
        }

        public void EigenDecomp3D(List<EigenDecomposition> EigenList)
        {
            try
            {

                double L1 = 0.0;
                double L2 = 0.0;
                double L3 = 0.0;
                double u11 = 0.0;
                double u21 = 0.0;
                double u31 = 0.0;
                double v12 = 0.0;
                double v22 = 0.0;
                double v32 = 0.0;
                double w13 = 0.0;
                double w23 = 0.0;
                double w33 = 0.0;


                //create matrix
                var matrix = new DenseMatrix(new[,] { { 0.0, 0.0, 0.0 }, { 0.0, 0.0, 0.0 }, { 0.0, 0.0, 0.0 } });
                
                // Format matrix output to console
                var formatProvider = (CultureInfo)CultureInfo.InvariantCulture.Clone();
                formatProvider.TextInfo.ListSeparator = " ";

                for (int i = 0; i < EigenList.Count; i++)
                {

                    // Create square symmetric matrix
                    matrix = new DenseMatrix(new[,] 
                    { 
                    {(double) EigenList[i].m11, (double) EigenList[i].m12, (double)  EigenList[i].m13 }, 
                    {(double) EigenList[i].m12, (double) EigenList[i].m22, (double)  EigenList[i].m23 }, 
                    {(double) EigenList[i].m13, (double) EigenList[i].m23, (double)  EigenList[i].m33 } 
                    });
                    //Console.WriteLine(@"Initial square symmetric matrix");
                    //Console.WriteLine(matrix.ToString("#0.00\t", formatProvider));
                    //Console.WriteLine();
                  
                    var evd = matrix.Evd();
                    //Console.WriteLine(@"Perform eigenvalue decomposition of symmetric matrix");

                    //// 1. Eigen vectors
                   // Console.WriteLine(@"1. Eigen vectors");
                    //Console.WriteLine(evd.EigenVectors().ToString("#0.00\t", formatProvider));
                    //Console.WriteLine();

                    //// 2. Eigen values as a complex vector
                    //Console.WriteLine(@"2. Eigen values as a complex vector");
                    //Console.WriteLine(evd.EigenValues().ToString("N", formatProvider));
                    //Console.WriteLine();

                    L1 = evd.EigenValues()[0].Real;
                    L2 = evd.EigenValues()[1].Real;
                    L3 = evd.EigenValues()[2].Real;

                    u11 = evd.EigenVectors().Storage[0, 0];
                    u21 = evd.EigenVectors().Storage[1, 0];
                    u31 = evd.EigenVectors().Storage[2, 0];

                    v12 = evd.EigenVectors().Storage[0, 1];
                    v22 = evd.EigenVectors().Storage[1, 1];
                    v32 = evd.EigenVectors().Storage[2, 1];

                    w13 = evd.EigenVectors().Storage[0, 2];
                    w23 = evd.EigenVectors().Storage[1, 2];
                    w33 = evd.EigenVectors().Storage[2, 2];


                    //// 3. Eigen values as the block diagonal matrix
                    //Console.WriteLine(@"3. Eigen values as the block diagonal matrix");
                    //Console.WriteLine(evd.D().ToString("#0.00\t", formatProvider));
                    //Console.WriteLine();

                    //// 4. Multiply V by its transpose VT
                    //var identity = evd.EigenVectors().TransposeAndMultiply(evd.EigenVectors());
                    //Console.WriteLine(@"4. Multiply V by its transpose VT: V*VT = I");
                    //Console.WriteLine(identity.ToString("#0.00\t", formatProvider));
                    //Console.WriteLine();

                    //// 5. Reconstruct initial matrix: A = V*D*V'
                    //var reconstruct = evd.EigenVectors() * evd.D() * evd.EigenVectors().Transpose();
                    //Console.WriteLine(@"5. Reconstruct initial matrix: A = V*D*V'");
                    //Console.WriteLine(reconstruct.ToString("#0.00\t", formatProvider));
                    //Console.WriteLine();

                    //// 6. Determinant of the matrix
                    //Console.WriteLine(@"6. Determinant of the matrix");
                    //Console.WriteLine(evd.Determinant);
                    //Console.WriteLine();

                    //// 7. Rank of the matrix
                    //Console.WriteLine(@"7. Rank of the matrix");
                    //Console.WriteLine(evd.Rank);
                    //Console.WriteLine();



                    EigenVectorValue.Add(new EigenSizeOrientation(
                         L1,
                         L2,
                         L3,
                         u11, u21, u31,
                         v12, v22, v32,
                         w13, w23, w33
                        ));

                }

            }

            catch (Exception ex)
            {
                //The expection info goes here
                string Message = ex.ToString() + "\n\n\n\n ERROR 3050 : Error Eigen decomposition";
                string Caption = "Styling and Functional Pattern Design";

             //   log.Info("ERROR 3050 : ERROR 3050 : Error Eigen decomposition");

                MessageBoxButtons Buttons = MessageBoxButtons.RetryCancel;

                DialogResult Result = MessageBox.Show(
                    Message,
                    Caption,
                    Buttons,
                    MessageBoxIcon.Error);


            }

        }

        public void UCS()
        {


            throw new NotImplementedException();

        }

        public int FindClosestPoint(VerticesOutput P)
        {

            int theClosestIndexIs = 0;

            try
            {
                ReadFileDataToList("meshd.srf");

                VerticesOutput theClosestPointIs = new VerticesOutput();

                double closestDistance = DistanceBetweenTwoPoints(P, VertexLocation[0]);

                
                double d = 0;

                for (int i = 0; i < VertexLocation.Count; i++)
                {
                    d = DistanceBetweenTwoPoints(P, VertexLocation[i]);

                    if (d < closestDistance)
                    {
                        closestDistance = d;
                        theClosestIndexIs = i;
                    }

                }

                theClosestPointIs = VertexLocation[theClosestIndexIs];
               

            }
            catch (Exception ex)
            {
                //The expection info goes here
                string Message = ex.ToString() + "\n\n\n\n ERROR 3070 : Problems finding closest point ";
                string Caption = "Styling and Functional Pattern Design";

               // log.Info(" ERROR 3070 : Problems finding closest point ");

                MessageBoxButtons Buttons = MessageBoxButtons.RetryCancel;

                DialogResult Result = MessageBox.Show(
                    Message,
                    Caption,
                    Buttons,
                    MessageBoxIcon.Error);
            }

            return theClosestIndexIs;
        }

        public double DistanceBetweenTwoPoints(VerticesOutput P1, VerticesOutput P2)
        {
            double dist = 0.0;

            try
            {
                
              dist = Math.Sqrt( Math.Pow((P2.x-P1.x),2) + 
                  Math.Pow((P2.y-P1.y),2) +
                  Math.Pow((P2.z-P1.z),2)
                      );
              
            }
            catch(Exception ex)
            {

                //The expection info goes here
                string Message = ex.ToString() + "\n\n\n\n ERROR 3080 : Problems finding distance between points from Vertices Output ";
                string Caption = "Styling and Functional Pattern Design";

               // log.Info(" ERROR 3080 : Problems finding distance between points from Vertices Output ");

                MessageBoxButtons Buttons = MessageBoxButtons.RetryCancel;

                DialogResult Result = MessageBox.Show(
                    Message,
                    Caption,
                    Buttons,
                    MessageBoxIcon.Error);
            }

            return dist;
        }


        public void ReadSRFFile()
        {
            try
            {
                string exePath = "";
                exePath = System.Windows.Forms.Application.StartupPath + @"\meshd.srf";

                StreamReader sr = new StreamReader(exePath);
                bool foundF = false;
                int lineCount = 0;

                while (!sr.EndOfStream && foundF == false)
                {

                    string[] split = sr.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (split[0] == "Surf")
                    {
                        Console.WriteLine("FOUND SURF");

                    }
                    else if (split[0] == "F")
                    {

                        Console.WriteLine("FOUND F");
                        foundF = true;
                    }
                    else
                    {
                        lineCount++;
                        SRFVeticesMeshD.Add(new VerticesOutput(
                                 Convert.ToString(split[0]),
                                 Convert.ToDouble(split[1]),
                                 Convert.ToDouble(split[2]),
                                 Convert.ToDouble(split[3])));
                    }

                }

                Debug.WriteLine("COUNTER LINES: " + lineCount.ToString());

                //Change here : make sure that SRFVeticesMeshD gets clean after using the tool it might have trash from other runs if 
                // not clean 

            }
            catch (Exception ex)
            {
                //The expection info goes here
                string Message = ex.ToString() + "\n\n\n\n ERROR 3060 : Error Reading MeshD.srf file";
                string Caption = "Styling and Functional Pattern Design";

                //log.Info("ERROR 3060 :  Error Reading MeshD.srf file");

                MessageBoxButtons Buttons = MessageBoxButtons.RetryCancel;

                DialogResult Result = MessageBox.Show(
                    Message,
                    Caption,
                    Buttons,
                    MessageBoxIcon.Error);
            }

        }

      
    }
}
