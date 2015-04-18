using UnityEngine;
using System.Collections;

public interface Weapon
{
    void fire(GameObject owner);

    AudioClip getAudioclip();
}
