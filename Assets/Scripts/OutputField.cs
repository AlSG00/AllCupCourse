using UnityEngine;
using TMPro;
using System;

public class OutputField : MonoBehaviour {
    private TMP_InputField outputField;

    private void Awake() {
        outputField = GetComponent<TMP_InputField>();
    }
    public void GetHashedCodeFromField(TMP_InputField inputField) {
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(inputField.text);
        byte[] outputBytes = System.Security.Cryptography.MD5.Create().ComputeHash(inputBytes);
        outputField.text = BitConverter.ToString(outputBytes).Replace("-", "");
    }    
}
