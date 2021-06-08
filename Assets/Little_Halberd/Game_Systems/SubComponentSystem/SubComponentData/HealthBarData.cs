using UnityEngine;
using UnityEngine.UI;

namespace LittleHalberd
{
    [System.Serializable]
    public class HealthBarData
    {
        public Slider Slider;
        public Gradient Gradient;
        public Image HealthImage;

        public delegate void SetChange(float val);
        public SetChange ChangeHealthBar;
    }
}