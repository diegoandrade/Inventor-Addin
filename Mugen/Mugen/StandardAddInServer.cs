using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using Inventor;
using Microsoft.Win32;

namespace Mugen
{
    /// <summary>
    /// This is the primary AddIn Server class that implements the ApplicationAddInServer interface
    /// that all Inventor AddIns are required to implement. The communication between Inventor and
    /// the AddIn is via the methods on this interface.
    /// </summary>
    [GuidAttribute("cc527b4d-c28d-40fb-a46a-4978db960594")]
    public class StandardAddInServer : Inventor.ApplicationAddInServer
    {
        #region Data Members

        //Inventor application object
        private Inventor.Application m_inventorApplication;

        //buttons
        private AddSlotOptionButton m_addSlotOptionButton;

        //user interface event
        private UserInterfaceEvents m_userInterfaceEvents;

        // ribbon panel
        RibbonPanel m_partSketchSlotRibbonPanel;

        //event handler delegates

        private Inventor.UserInterfaceEventsSink_OnResetCommandBarsEventHandler UserInterfaceEventsSink_OnResetCommandBarsEventDelegate;
        private Inventor.UserInterfaceEventsSink_OnResetEnvironmentsEventHandler UserInterfaceEventsSink_OnResetEnvironmentsEventDelegate;
        private Inventor.UserInterfaceEventsSink_OnResetRibbonInterfaceEventHandler UserInterfaceEventsSink_OnResetRibbonInterfaceEventDelegate;

        #endregion

        public StandardAddInServer()
        {
        }

        #region ApplicationAddInServer Members

        public void Activate(Inventor.ApplicationAddInSite addInSiteObject, bool firstTime)
        {
            try
            {
                //the Activate method is called by Inventor when it loads the addin
                //the AddInSiteObject provides access to the Inventor Application object
                //the FirstTime flag indicates if the addin is loaded for the first time

                //initialize AddIn members
                m_inventorApplication = addInSiteObject.Application;
                Button.InventorApplication = m_inventorApplication;

                //initialize event delegates
                m_userInterfaceEvents = m_inventorApplication.UserInterfaceManager.UserInterfaceEvents;

                UserInterfaceEventsSink_OnResetCommandBarsEventDelegate = new UserInterfaceEventsSink_OnResetCommandBarsEventHandler(UserInterfaceEvents_OnResetCommandBars);
                m_userInterfaceEvents.OnResetCommandBars += UserInterfaceEventsSink_OnResetCommandBarsEventDelegate;

                UserInterfaceEventsSink_OnResetEnvironmentsEventDelegate = new UserInterfaceEventsSink_OnResetEnvironmentsEventHandler(UserInterfaceEvents_OnResetEnvironments);
                m_userInterfaceEvents.OnResetEnvironments += UserInterfaceEventsSink_OnResetEnvironmentsEventDelegate;

                UserInterfaceEventsSink_OnResetRibbonInterfaceEventDelegate = new UserInterfaceEventsSink_OnResetRibbonInterfaceEventHandler(UserInterfaceEvents_OnResetRibbonInterface);
                m_userInterfaceEvents.OnResetRibbonInterface += UserInterfaceEventsSink_OnResetRibbonInterfaceEventDelegate;

                //load image icons for UI items
                //PROBLEM: i CAN NOT ADD ANOTHER ICON EXCEPT FOR SYSTEM... HOW TO DO THIS?
              //  Icon anisotropyIcon = new Icon(this.GetType(), "\\AddSlotOption.ico");
                Icon addSlotOptionIcon = new Icon(SystemIcons.Exclamation, 32, 32);
              

                //retrieve the GUID for this class
                GuidAttribute addInCLSID;
                addInCLSID = (GuidAttribute)GuidAttribute.GetCustomAttribute(typeof(StandardAddInServer), typeof(GuidAttribute));
                string addInCLSIDString;
                addInCLSIDString = "{" + addInCLSID.Value + "}";

 
              



                //create button
                m_addSlotOptionButton = new AddSlotOptionButton(
                    "Add pattern", "Autodesk:Mugen:AddAnisotropyOptionCmdBtn", CommandTypesEnum.kShapeEditCmdType,
                    addInCLSIDString, "Creates an anisotropic pattern on a given surface",
                    "Add Anisotropic Pattern", addSlotOptionIcon, addSlotOptionIcon, ButtonDisplayEnum.kDisplayTextInLearningMode);

             
                //create the command category
                CommandCategory slotCmdCategory = m_inventorApplication.CommandManager.CommandCategories.Add("Anisotropy", "Autodesk:Mugen:AnisotropyCmdCat", addInCLSIDString);

                slotCmdCategory.Add(m_addSlotOptionButton.ButtonDefinition);
               

                if (firstTime == true)
                {
                    //access user interface manager
                    UserInterfaceManager userInterfaceManager;
                    userInterfaceManager = m_inventorApplication.UserInterfaceManager;

                    InterfaceStyleEnum interfaceStyle;
                    interfaceStyle = userInterfaceManager.InterfaceStyle;

                    //create the UI for classic interface
                    if (interfaceStyle == InterfaceStyleEnum.kClassicInterface)
                    {
                        //create toolbar
                        CommandBar slotCommandBar;
                        slotCommandBar = userInterfaceManager.CommandBars.Add("Anisotropy", "Autodesk:Mugen:AnistropyToolbar", CommandBarTypeEnum.kRegularCommandBar, addInCLSIDString);

                        //add buttons to toolbar
                        slotCommandBar.Controls.AddButton(m_addSlotOptionButton.ButtonDefinition, 0);
                    

                        //Get the 2d sketch environment base object
                        Inventor.Environment partSketchEnvironment;
                        partSketchEnvironment = userInterfaceManager.Environments["PMxPartEnvironment"];

                        //make this command bar accessible in the panel menu for the 2d sketch environment.
                        partSketchEnvironment.PanelBar.CommandBarList.Add(slotCommandBar);
                    }
                    //create the UI for ribbon interface
                    else
                    {
                        //get the ribbon associated with part document
                        Inventor.Ribbons ribbons;
                        ribbons = userInterfaceManager.Ribbons;

                        Inventor.Ribbon partRibbon;
                        partRibbon = ribbons["Part"];

                        //get the tabls associated with part ribbon
                        RibbonTabs ribbonTabs;
                        ribbonTabs = partRibbon.RibbonTabs;

                        RibbonTab partSketchRibbonTab;
                        partSketchRibbonTab = ribbonTabs["id_TabTools"];

                        //create a new panel with the tab
                        RibbonPanels ribbonPanels;
                        ribbonPanels = partSketchRibbonTab.RibbonPanels;

                        m_partSketchSlotRibbonPanel = ribbonPanels.Add("Anysotropy", "Autodesk:Mugen:AnysotropyRibbonPanel", "{cc527b4d-c28d-40fb-a46a-4978db960594}", "", false);

                        //add controls to the slot panel
                        CommandControls partSketchSlotRibbonPanelCtrls;
                        partSketchSlotRibbonPanelCtrls = m_partSketchSlotRibbonPanel.CommandControls;

                        //add the buttons to the ribbon panel
                        CommandControl drawSlotCmdBtnCmdCtrl;
                        drawSlotCmdBtnCmdCtrl = partSketchSlotRibbonPanelCtrls.AddButton(m_addSlotOptionButton.ButtonDefinition, false, true, "", false);


                     }
                }

                MessageBox.Show("This is a Carnegie Mellon University :: CERLAB test of \nAnisotropic Geometric Features Pattern Generation \nfor Industrial Design on Higly Curved Surfaces \nUsing Riemannian Metric Tensor fields \n\nAuthor: Diego Andrade \n\n2014");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void Deactivate()
        {
            //the Deactivate method is called by Inventor when the AddIn is unloaded
            //the AddIn will be unloaded either manually by the user or
            //when the Inventor session is terminated

            try
            {
                m_userInterfaceEvents.OnResetCommandBars -= UserInterfaceEventsSink_OnResetCommandBarsEventDelegate;
                m_userInterfaceEvents.OnResetEnvironments -= UserInterfaceEventsSink_OnResetEnvironmentsEventDelegate;

                UserInterfaceEventsSink_OnResetCommandBarsEventDelegate = null;
                UserInterfaceEventsSink_OnResetEnvironmentsEventDelegate = null;
                m_userInterfaceEvents = null;
                if (m_partSketchSlotRibbonPanel != null)
                {
                    m_partSketchSlotRibbonPanel.Delete();
                }

                //release inventor Application object
                Marshal.ReleaseComObject(m_inventorApplication);
                m_inventorApplication = null;

                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void ExecuteCommand(int CommandID)
        {
            //this method was used to notify when an AddIn command was executed
            //the CommandID parameter identifies the command that was executed

            //Note:this method is now obsolete, you should use the new
            //ControlDefinition objects to implement commands, they have
            //their own event sinks to notify when the command is executed
        }

        public object Automation
        {
            //if you want to return an interface to another client of this addin,
            //implement that interface in a class and return that class object 
            //through this property

            get
            {
                return null;
            }
        }

        private void UserInterfaceEvents_OnResetCommandBars(ObjectsEnumerator commandBars, NameValueMap context)
        {
            try
            {
                CommandBar commandBar;
                for (int commandBarCt = 1; commandBarCt <= commandBars.Count; commandBarCt++)
                {
                    commandBar = (Inventor.CommandBar)commandBars[commandBarCt];
                    if (commandBar.InternalName == "Autodesk:Mugen:AnistropyToolbar")
                    {

                        //add buttons to toolbar
                        commandBar.Controls.AddButton(m_addSlotOptionButton.ButtonDefinition, 0);

                        return;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void UserInterfaceEvents_OnResetEnvironments(ObjectsEnumerator environments, NameValueMap context)
        {
            try
            {
                Inventor.Environment environment;
                for (int environmentCt = 1; environmentCt <= environments.Count; environmentCt++)
                {
                    environment = (Inventor.Environment)environments[environmentCt];
                    if (environment.InternalName == "PMxPartEnvironment")
                    {
                        //make this command bar accessible in the panel menu for the 2d sketch environment.
                        environment.PanelBar.CommandBarList.Add(m_inventorApplication.UserInterfaceManager.CommandBars["Autodesk:Mugen:AnistropyToolbar"]);

                        return;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void UserInterfaceEvents_OnResetRibbonInterface(NameValueMap context)
        {
            try
            {

                UserInterfaceManager userInterfaceManager;
                userInterfaceManager = m_inventorApplication.UserInterfaceManager;

                //get the ribbon associated with part document
                Inventor.Ribbons ribbons;
                ribbons = userInterfaceManager.Ribbons;

                Inventor.Ribbon partRibbon;
                partRibbon = ribbons["Part"];

                //get the tabls associated with part ribbon
                RibbonTabs ribbonTabs;
                ribbonTabs = partRibbon.RibbonTabs;

                RibbonTab partSketchRibbonTab;
                partSketchRibbonTab = ribbonTabs["id_TabTools"];

                //create a new panel with the tab
                RibbonPanels ribbonPanels;
                ribbonPanels = partSketchRibbonTab.RibbonPanels;

                m_partSketchSlotRibbonPanel = ribbonPanels.Add("Anisotropy", "Autodesk:Mugen:AnysotropyRibbonPanel",
                                                             "{cc527b4d-c28d-40fb-a46a-4978db960594}", "", false);

                //add controls to the slot panel
                CommandControls partSketchSlotRibbonPanelCtrls;
                partSketchSlotRibbonPanelCtrls = m_partSketchSlotRibbonPanel.CommandControls;

                //add the buttons to the ribbon panel
                CommandControl drawSlotCmdBtnCmdCtrl;
                drawSlotCmdBtnCmdCtrl = partSketchSlotRibbonPanelCtrls.AddButton(m_addSlotOptionButton.ButtonDefinition, false, true, "", false);

             }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        #endregion
    }
}
