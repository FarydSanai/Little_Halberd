using UnityEngine;
using UnityEngine.UI;

namespace LittleHalberd
{
    public class HealthBar : SubComponent
    {
        public Slider Slider;
        public Gradient Gradient;
        public Image HealthImage;

        public HealthBarData healthBarData;
        private void Start()
        {
            this.Slider = this.GetComponent<Slider>();

            healthBarData = new HealthBarData
            {
                Slider = this.Slider,
                Gradient = this.Gradient,
                HealthImage = this.HealthImage,

                ChangeHealthBar = ChangeHealthBar,
            };

            InitHealthBarValues(healthBarData);

            subComponentProcessor.healthBarData = healthBarData;
            subComponentProcessor.ArrSubComponents[(int)SubComponentType.HEALTH_BAR] = this;
        }
        private void InitHealthBarValues(HealthBarData healthBarData)
        {
            healthBarData.Slider.maxValue = control.CharacterMaxHP;
            healthBarData.Slider.value = control.CharacterMaxHP;
            healthBarData.HealthImage.color = healthBarData.Gradient.Evaluate(1f);
        }
        public override void OnUpdate()
        {
            healthBarData.HealthImage.color = Gradient.Evaluate(
                                                            healthBarData.Slider.normalizedValue);
            //throw new System.NotImplementedException();
        }

        public override void OnFixedUpdate()
        {
            //throw new System.NotImplementedException();
        }
        public void ChangeHealthBar(float health)
        {
            healthBarData.Slider.value -= health;

            healthBarData.HealthImage.color = healthBarData.Gradient.Evaluate(
                                                            healthBarData.Slider.normalizedValue);
        }
    }
}