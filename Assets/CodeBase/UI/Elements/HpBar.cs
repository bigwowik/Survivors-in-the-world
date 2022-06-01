using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class HpBar : MonoBehaviour
    {
        public Image ImageCurrent;
        public TextMeshProUGUI HpText;

        public void SetValue(float current, float max)
        {
            ImageCurrent.fillAmount = current / max;

            if (HpText != null)
                HpText.text = $"{current}/{max}";
        }
    }
}