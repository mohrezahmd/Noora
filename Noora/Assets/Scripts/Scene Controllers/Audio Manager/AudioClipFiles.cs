using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectData", menuName = "ScriptableObjects/AudioClips", order = 1)]
public class AudioClipFiles : ScriptableObject
{
    public Sound[] clips;

}
