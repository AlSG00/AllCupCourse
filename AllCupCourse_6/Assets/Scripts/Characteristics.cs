using System;
using UnityEngine;
[Serializable] public struct Characteristics {


    public int Health;
    public int Mana;
    public int Coin;

    public Characteristics(int health, int mana, int coin) {
        Health = health;
        Mana = mana;
        Coin = coin;
    }

    public Color Color {
        get {
            if (Health >= Mana && Health >= Coin) {
                return Color.red;
            } else if (Mana >= Coin && Mana >= Health) {
                return Color.blue;
            } else {
                return Color.yellow;
            }
        }
        
    }

    public static Characteristics operator +(Characteristics a, Characteristics b) {
        return new Characteristics(a.Health + b.Health, a.Mana + b.Mana, a.Coin + b.Coin);
    }
}
