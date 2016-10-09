using System;
using System.Reflection;

namespace RiseEngine.Core
{
    public static class Engine
    {
        public static System.Version Version = Assembly.GetExecutingAssembly().GetName().Version;
        public static bool AsErrore = false;
        public static bool IsLoaded = false;
        public static int CurrentFrame = 0;
        public static UI.Cursor MouseCursor;

        public static void ReloadEngine() {

            Debug.Logs.Write("[Engine] Reloading...", Debug.LogType.Info);
            ContentEngine.ReloadContent();
            GameObjectsManager.Reload();
            GC.Collect();
            Initializer();

        }

        public static void Initializer()
        {

            Debug.Logs.Write("[Engine] Initializing...", Debug.LogType.Info);


            MouseCursor = new UI.Cursor();

            Core.GameObjectsManager.InitializePlugin();


            if (GameObjectsManager.IsFullLoaded())
            {
                Debug.Logs.Write("[Engine] Initializing Done !", Debug.LogType.Info);
            }
            else
            {
                Debug.Logs.Write("[Engine] Initializing Failed !", Debug.LogType.Info);

                switch (System.Windows.Forms.MessageBox.Show("Gosh !" + Environment.NewLine + "An error that occurred during initialization of the engine.", "MakerRiseEngine " + Version, System.Windows.Forms.MessageBoxButtons.AbortRetryIgnore, System.Windows.Forms.MessageBoxIcon.Error))
                {
                    case System.Windows.Forms.DialogResult.Abort:
                        System.Windows.Forms.Application.Exit();
                        break;
                    case System.Windows.Forms.DialogResult.Retry:
                        System.Windows.Forms.Application.Restart();
                        break;
                    case System.Windows.Forms.DialogResult.Ignore:
                        AsErrore = true;
                        break;
                    default:
                        break;
                }


            }

            IsLoaded = true;

        }

    }
}
