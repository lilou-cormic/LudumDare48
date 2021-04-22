using System.Linq;
using UnityEngine;

namespace PurpleCable
{
    public class ResolutionSelector : ValueSelector<Resolution>
    {
        private void Awake()
        {
            Values = Screen.resolutions;
        }

        protected override void Start()
        {
            base.Start();

            SelectedIndex = Values.ToList().FindIndex(x => x.width == Screen.currentResolution.width && x.height == Screen.currentResolution.height);
        }

        protected override void OnValueChanged(Resolution value)
        {
            Screen.SetResolution(value.width, value.height, Screen.fullScreenMode);
        }

        protected override string GetDisplayText(Resolution value)
        {
            return value.width + "x" + value.height;
        }
    }
}
