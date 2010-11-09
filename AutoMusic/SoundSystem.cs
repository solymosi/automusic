using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMusic
{
    static class SoundSystem
    {
        static private FMOD.System FMODSystem = null;

        /// <summary>
        /// Gets the active sound system
        /// </summary>
        static public FMOD.System Active { get { return FMODSystem; } }

        /// <summary>
        /// Returns true if the FMOD system is initialized
        /// </summary>
        static public bool Initialized
        {
            get
            {
                try { return (FMODSystem != null); }
                catch { return false; }
            }
        }

        /// <summary>
        /// Initializes the sound system
        /// </summary>
        static public void Initialize()
        {
            FMOD.System System = null;
            try
            {
                HandleError(FMOD.Factory.System_Create(ref System));
                HandleError(System.init(10, FMOD.INITFLAGS.NORMAL, (IntPtr)null));
            }
            catch { throw new Exception("Could not load FMOD."); }
            FMODSystem = System;
        }

        /// <summary>
        /// Unloads the sound system and frees all resources it used
        /// </summary>
        static public void Dispose()
        {
            if (FMODSystem != null)
            {
                try { FMODSystem.release(); }
                catch { }
            }
            FMODSystem = null;
        }

        /// <summary>
        /// Updates the sound system state. Should be called periodically from the application's Tick method
        /// </summary>
        static public void Update()
        {
            FMODSystem.update();
        }

        /// <summary>
        /// Throws an FMODException if the given result is not success
        /// </summary>
        /// <param name="FMODResult">FMOD result to check</param>
        static public void HandleError(FMOD.RESULT FMODResult)
        {
            if (FMODResult != FMOD.RESULT.OK) { throw new FMODException(FMODResult); }
        }
    }
}
