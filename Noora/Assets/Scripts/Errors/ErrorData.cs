using UnityEngine;

[CreateAssetMenu(fileName = "ErrorData", menuName = "ScriptableObjects/ErrorData", order = 1)]
public class ErrorData : ScriptableObject
{
    public string[] errors;
}