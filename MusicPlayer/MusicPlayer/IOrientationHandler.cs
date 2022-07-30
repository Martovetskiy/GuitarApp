using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPlayer
{
    public interface IOrientationHandler
    {
        void ForceLandscape();
        void ForcePortrait();
    }

}
