using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Chest : MonoBehaviour {

    [SerializeField] private Player player;
    [SerializeField] private KeyCode openKey = KeyCode.Space;
    [SerializeField] private Bonus healthBonus;
    [SerializeField] private Bonus manaBonus;
    [SerializeField] private Bonus coinBonus;

    [HideInInspector] public Bonus CurrentBonus;

    [SerializeField] public UnityEvent onOpen;

    private bool isLocked = false;

    private int _distinction = 100;
    private Characteristics _characteristics;
    private int[] _bonuses;

    private void Awake() {
        healthBonus.OnBonusViewDestroy.AddListener(Unlock);
        manaBonus.OnBonusViewDestroy.AddListener(Unlock);
        coinBonus.OnBonusViewDestroy.AddListener(Unlock);
        _characteristics = player.Characteristics;
        _bonuses = new int[] { _characteristics.Health, _characteristics.Mana, _characteristics.Coin };
        //Debug.Log($"{_characteristics.Health} : {_characteristics.Mana} : {_characteristics.Coin}");
    }

    private void Update() {
        if (!isLocked && Input.GetKeyDown(openKey)) {
            Open();
        }
    }

    private void Unlock() {
        isLocked = false;
    }

    private void Open() {
        isLocked = true;
        onOpen.Invoke();
    }

    private void OnOpenAnimationEnd() {
        _bonuses = new int[] { player.Characteristics.Health, player.Characteristics.Mana, player.Characteristics.Coin };
        Debug.Log($"{player.Characteristics.Health} : {player.Characteristics.Mana} : {player.Characteristics.Coin}");
        if (_bonuses.Max() - _bonuses.Min() > _distinction)
        {
            Debug.Log("true");
            switch (Array.IndexOf(_bonuses, _bonuses.Min()))
            {
                case 0:
                    healthBonus.Apply(player);
                    Debug.Log("0");
                    break;
                case 1:
                    manaBonus.Apply(player);
                    Debug.Log("1");
                    break;
                case 2:
                    coinBonus.Apply(player);
                    Debug.Log("2");
                    break;
            }
        }
        else
        {
            int index = 0;
            
            foreach (int bonus in _bonuses)
            {
                if (bonus != _bonuses.Min() &&
                    bonus != _bonuses.Max())
                {
                    index = Array.IndexOf(_bonuses, bonus);
                    break;
                }
            }

            Debug.Log("false");
            switch (index)
            {
                case 0:
                    healthBonus.Apply(player);
                    Debug.Log("0");
                    break;
                case 1:
                    manaBonus.Apply(player);
                    Debug.Log("1");
                    break;
                case 2:
                    coinBonus.Apply(player);
                    Debug.Log("2");
                    break;
            }
        }

       //player.Characteristics
        //_bonuses = new int[] { player.Characteristics.Health, player.Characteristics.Mana, player.Characteristics.Coin };
        //Debug.Log($"{player.Characteristics.Health} : {player.Characteristics.Mana} : {player.Characteristics.Coin}");
    }
}
