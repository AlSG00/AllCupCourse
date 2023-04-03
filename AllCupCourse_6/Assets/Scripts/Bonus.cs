using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
[Serializable] public class Bonus : MonoBehaviour {
    [SerializeField] private Characteristics characteristics;
    [SerializeField] private Transform appearancePoint;
    [SerializeField] private GameObject bonusViewPrefab;
    
    private GameObject bonusView;
    private Queue<Player> applyQueque = new Queue<Player>();
    private bool allowBonusApply = true;
    private AudioSource selfAudioSource;

    [HideInInspector] public UnityEvent OnBonusViewDestroy;


    public void Apply(Player player) {
        applyQueque.Enqueue(player);
    }

    private void Awake() {
        selfAudioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        if (allowBonusApply && applyQueque.Count > 0) {
            applyQueque.Dequeue().AddCharacteristics(characteristics);
            selfAudioSource.Play();
            bonusView = Instantiate(bonusViewPrefab, appearancePoint);
            BonusViewText bonusViewText;
            if (!bonusView.TryGetComponent(out bonusViewText)) {
                Debug.LogError("Bonus view prefab must content BonusView component");
            }
            bonusViewText.Value = Mathf.Max(characteristics.Health, characteristics.Coin, characteristics.Mana);
            bonusViewText.Color = characteristics.Color;
            allowBonusApply = false;
        } else if (Input.GetKeyDown(KeyCode.Space) && !allowBonusApply && bonusView != null) {
            Invoke("DestroyView", 0.1f);
        }
    }

    public void DestroyView() {
        Destroy(bonusView);
        allowBonusApply = true;
        OnBonusViewDestroy.Invoke();
    }
}
