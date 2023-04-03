using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour {
    [SerializeField] private Characteristics characteristics;
    [SerializeField] private UnityEvent<Characteristics> onCharacteristicsChange;


    private void Start() {
        onCharacteristicsChange.Invoke(this.characteristics);
    }
    public Characteristics Characteristics {
        get {
            return characteristics;
        }
    }
    public void AddCharacteristics(Characteristics characteristics) {
        this.characteristics += characteristics;
        onCharacteristicsChange.Invoke(this.characteristics);
    }


}
