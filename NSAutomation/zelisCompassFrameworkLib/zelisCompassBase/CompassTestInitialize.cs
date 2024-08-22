using NUnit.Framework;
using zelisCompassFrameWorkLib.zelisCompassBase;
using zelisCompassFrameWorkLib.zelisHelpers;

namespace zelisCompassFrameWorkLib.zelisCompassBase
{
    [TestFixture]
    public  /*abstract*/ class CompassTestInitialize : CompassBase
    {
      

        [SetUp]
       public void InitializePriZem()
      
        {
            CompassContext = WinDriverContext.initializePriZem();
            
            LogHelper.CreateTestEvidenceFile();


        }





    }
  

}
