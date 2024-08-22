using SeleniumExtras.PageObjects;
using zelisCompassFrameWorkLib.zelisCompassBase;


namespace zelisCompassFrameWorkLib.zelisCompassBase
{
    public abstract class CompassBasePage: CompassBase
    {

        public CompassBasePage()
        {

            PageFactory.InitElements(WinDriverContext.CompassCMS, this);

        }

    }
}
