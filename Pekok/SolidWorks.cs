using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Pekok
{
    internal class SolidWorks
    {
        internal static SldWorks swApp;
        internal static ModelDoc2 swDoc;

        internal static void OpenApp()
        {
            if (swApp == null)
            {
                try
                {
                    swApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
                }
                catch
                {
                    swApp = Activator.CreateInstance(Type.GetTypeFromProgID("SldWorks.Application")) as SldWorks;
                }
                swApp.FrameState = (int)swWindowState_e.swWindowMaximized;
                swApp.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swInputDimValOnCreate, false);
            }
            swApp.Visible = true;
        }
        internal static void OpenDoc()
        {
            if (swApp != null)
            {

                ISldWorks iSwApp = (ISldWorks)swApp;
                iSwApp.CloseAllDocuments(true);

                swDoc = (ModelDoc2)swApp.INewPart();
                swDoc.SetUnits((short)swLengthUnit_e.swMM, (short)swFractionDisplay_e.swDECIMAL, 0, 0, false);
            }

        }
        internal static void Close()
        {
            if (swApp != null)
            {
                swApp.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swInputDimValOnCreate, true);
                swApp.ExitApp();

                swApp = null;
                swDoc = null;
            }

        }
    }
}