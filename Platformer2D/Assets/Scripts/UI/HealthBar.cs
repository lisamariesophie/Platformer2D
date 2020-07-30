using UnityEngine;
using UnityEngine.UI;

// aus Brackeys Tutorial übernommen: https://www.youtube.com/watch?v=BLfNP4Sc_iA
public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public void SetMaxHealth(int health){
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }
    public void SetCurrentHealth(int health){
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
