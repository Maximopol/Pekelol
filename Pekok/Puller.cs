using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Pekok
{
    public class Puller
    {
        public static Feature Extrude(ModelDoc2 swDoc, double deepth)
        {
            return swDoc.FeatureManager.FeatureExtrusion2(true, false, false,
                0, 0, deepth, 0, false, false, false, false, 0, 0, false,
                false, false, false, true, true, true, 0, 0, false);
        }
        public static Feature Cut(ModelDoc2 swDoc, double deepth)
        {
            //swDoc.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0,
            //    deepth, 0, false, false, false, false, 0, 0, false,
            //    false, false, false, true, true, true, 0, 0, false);
            //return swDoc.FeatureManager.FeatureCut2(true, false, true, 0, 0,
            //    3.496, 0.01, false, false, false, false,
            //    1.74532925199433E-02, 1.74532925199433E-02,
            //    false, false);
            //return swDoc.FeatureManager.FeatureCut2(true, false, false, 0, 0,
            //    deepth, 0, false, false, false, false, 0, 0, false,
            //    false, false, false, false, false, false, false, false, false);
            return swDoc.FeatureManager.FeatureCut2(true, false, true, 0, 0,
               deepth, 0.01, false, false, false, false,
               1.74532925199433E-02, 1.74532925199433E-02,
                false,
                false, false, false, false, false, false, false, false, false);

            //return swDoc.FeatureManager.FeatureCut2(true, false, false, (int)swEndConditions_e.swEndCondBlind, (int)swEndConditions_e.swEndCondBlind,
            //    deepth, 0, false, false, false, false, 0, 0, false, false, false, false, false,
            //    false, false, false, false, false);
        }
    }
}