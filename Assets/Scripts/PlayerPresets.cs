using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new PlayerPreset", menuName = "PlayerPreset")]
public class PlayerPresets : ScriptableObject
{
    public float speedY;
    public float speedX;
    public float maxSpeed;
    public float speedInc;
}
