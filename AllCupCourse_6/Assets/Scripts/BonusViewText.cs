using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class BonusViewText : MonoBehaviour {
    TMP_Text selfText;

    private void Awake() {
        selfText = GetComponent<TMP_Text>();
    }

    public int Value {
        set {
            selfText.text = "+" + value.ToString();
        }
    }

    public Color Color {
        set {
            selfText.color = value;
        }
    }
}
