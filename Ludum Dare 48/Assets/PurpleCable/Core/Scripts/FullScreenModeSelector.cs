using UnityEngine;

namespace PurpleCable
{
    public class FullScreenModeSelector : ValueSelector<FullScreenMode>
    {
        protected override void Start()
        {
            base.Start();

            SelectedIndex = (int)Screen.fullScreenMode;
        }

        protected override void OnValueChanged(FullScreenMode value)
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, value);
        }
    }
}
