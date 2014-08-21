using System;
using System.Windows.Forms;
using System.Drawing;
using Inventor;

namespace Mugen
{
	/// <summary>
	/// AddSlotOptionButton class
	/// </summary>
	
	internal class AddSlotOptionButton : Button
	{
		#region "Methods"

        private Inventor.Application mApp = null; //This object is defined but not 

		public AddSlotOptionButton(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, Icon standardIcon, Icon largeIcon, ButtonDisplayEnum buttonDisplayType)
			: base(displayName, internalName, commandType, clientId, description, tooltip, standardIcon, largeIcon, buttonDisplayType)
		{
			
		}
		public AddSlotOptionButton(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType)
			: base(displayName, internalName, commandType, clientId, description, tooltip, buttonDisplayType)
		{
			
		}

		override protected void ButtonDefinition_OnExecute(NameValueMap context)
		{
			try
			{
                //MessageBox.Show("ERES UN CAPO");

                AutodeskInventorInterop AII = new AutodeskInventorInterop();

                string filename = "..\\CMUdata\\meshanid.srf";

                // Parses files into location and vertex connectivity from an SRF file
                AII.ReadFileDataToList(filename);
                //log.Info("Reading meshanid.srf");

                // Parses file for elements in riemanian metric matrix in 3D
                filename = "..\\CMUdata\\meshanid.nt3m";
                AII.ReadFileDataToList_nt3m(filename);
               // log.Info("Reading meshanid.nt3m");

                // Decomposes the Riemanian Matrix M=Q.L.Q^-1
                AII.EigenDecomp3D(AII.EigenList);
               // log.Info("EigenDecomposition");

              //  log.Info("User Coordinate System");

                Inventor.Application mApp = System.Runtime.InteropServices.Marshal.GetActiveObject("Inventor.Application") as Inventor.Application;
               
                Inventor.PartDocument oDoc = (Inventor.PartDocument)mApp.ActiveDocument;

                PartComponentDefinition oCompDef = default(PartComponentDefinition); //Defines a part component
                oCompDef = oDoc.ComponentDefinition;

                //Used to create a Unitvector where to store MajorAxis 2D for ellipse
                TransientGeometry oTG = mApp.TransientGeometry;
                UnitVector2d oUniVector = oTG.CreateUnitVector2d(1, 1);

                Point2d oPoint2d = mApp.TransientGeometry.CreatePoint2d(0, 0);

                Profile oProfile = default(Profile); //Creates a profile 
 
                //EdgeCollection oEdge = default(EdgeCollection);

                Edge[] oSideEdges = new Edge[5];

                //FilletFeature oFillet = default(FilletFeature);

                WorkPoint oWorkPoint1 = default(WorkPoint);
                WorkPoint oWorkPoint2 = default(WorkPoint);
                WorkPoint oWorkPoint3 = default(WorkPoint);

                UserCoordinateSystemDefinition oUCSDef = oCompDef.UserCoordinateSystems.CreateDefinition();

                UserCoordinateSystem oUCS = default(UserCoordinateSystem);

                EigenVectorPatternSketchPlane selectPlaneToSketch = new EigenVectorPatternSketchPlane();  //ERASE THIS NOT USEFUL

                PartComponentDefinition oDef = oDoc.ComponentDefinition;
                PlanarSketch oSketch = default(PlanarSketch);
                ObjectCollection oFitPoints = mApp.TransientObjects.CreateObjectCollection();

                Point2d oPoint2d_a = default(Point2d);
                SketchEllipticalArc oEllipticalArc = default(SketchEllipticalArc);
                SketchLine oAxis = default(SketchLine);
                RevolveFeature oRevolve = default(RevolveFeature);

                //  WorkPlane oWPain = default(WorkPlane);

                #region "This is my life"
                /****************************************** COMMENT ***********************************************/
                // Philippe Leefsma : Philippe.Leefsma@autodesk.com
                // Here is where all the pain happens
                // thank you for your help in advance.
                // Here is where I create a User defined coordinate system per each one of the features shown
                // I do not know if there is a way of making this faster?
                /****************************************** COMMENT ***********************************************/

                for (int i = 0; i < AII.VertexLocation.Count; i++)
                {

                    oWorkPoint1 = oCompDef.WorkPoints.AddFixed(
                       oTG.CreatePoint(
                       (double)AII.VertexLocation[i].origen.X,
                       (double)AII.VertexLocation[i].origen.Y,
                       (double)AII.VertexLocation[i].origen.Z)
                       );

                    //Change here u1/10 this is to make it closer to the mm value check if needs to be chage to in
                    oWorkPoint2 = oCompDef.WorkPoints.AddFixed(
                       oTG.CreatePoint(
                       (double)AII.EigenVectorValue[i].evc.u1 + (double)AII.VertexLocation[i].origen.X,
                       (double)AII.EigenVectorValue[i].evc.u2 + (double)AII.VertexLocation[i].origen.Y,
                       (double)AII.EigenVectorValue[i].evc.u3 + (double)AII.VertexLocation[i].origen.Z)
                       );
                    //Check here maybe this is why my rotations are screwd up, instead of v1 maybe is w1 
                    //If w is use I have a cool effect with the protution coming out of the screen
                    oWorkPoint3 = oCompDef.WorkPoints.AddFixed(
                       oTG.CreatePoint(
                       (double)AII.EigenVectorValue[i].evc.v1 + (double)AII.VertexLocation[i].origen.X,
                       (double)AII.EigenVectorValue[i].evc.v2 + (double)AII.VertexLocation[i].origen.Y,
                       (double)AII.EigenVectorValue[i].evc.v3 + (double)AII.VertexLocation[i].origen.Z)
                       );


                    //User Define Coordinate

                    oUCSDef.SetByThreePoints(oWorkPoint1, oWorkPoint2, oWorkPoint3);


                    oUCS = oCompDef.UserCoordinateSystems.Add(oUCSDef);

                    oPoint2d.X = 0;
                    oPoint2d.Y = 0;

                    double l1 = AII.EigenVectorValue[i].evl.l1;
                    double l2 = AII.EigenVectorValue[i].evl.l2; //Here maybe we can reduece time by pre-working these numbers

                    double h1 = (1 / Math.Sqrt(l1));  //Check here
                    double h2 = (1 / Math.Sqrt(l2));

                    //Change here to add alfa angle
                    oUniVector.X = 1;// Math.Abs(AII.EigenVectorValue[i].evc.u1); //CHNAGE HERE 0 ,1 //if this is negative changes the direction 
                    oUniVector.Y = 0;// Math.Abs(AII.EigenVectorValue[i].evc.u2);


                    oSketch = selectPlaneToSketch.SelectPlane(oDoc, oUCS);



                    //Candidates for config file
                    oWorkPoint1.Visible = false;
                    oWorkPoint2.Visible = false;
                    oWorkPoint3.Visible = false;
                    oUCS.Visible = false;



                    double PI = Math.Atan(1) * 4.0;
                    double duoPI = 2 * PI;
                    double sizeBubbleInverse = 3.5; //this changes the sizes in the features contracts the bubbles "SIZING METRIC"

                    #region "Revolve and Join"
                    oPoint2d_a = mApp.TransientGeometry.CreatePoint2d(0, 0);

                    //Create Elliptical Arc
                    oEllipticalArc = oSketch.SketchEllipticalArcs.Add(oPoint2d_a, oUniVector, h1 / (sizeBubbleInverse), h2 / (sizeBubbleInverse), 0, PI);

                    oAxis = oSketch.SketchLines.AddByTwoPoints(
                        oEllipticalArc.StartSketchPoint,
                        oEllipticalArc.EndSketchPoint);

                    oProfile = oSketch.Profiles.AddForSolid();

                        oRevolve = oDoc.ComponentDefinition.Features.RevolveFeatures.AddFull
                            (oProfile, oAxis, PartFeatureOperationEnum.kJoinOperation);
                    #endregion





                }

                #endregion








            }
			catch(Exception e)
			{
				MessageBox.Show(e.ToString());
			}
		}

		#endregion
	}
}
