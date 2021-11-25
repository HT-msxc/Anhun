using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodAltar_Fujiman_Wind_Audio : SingletonHT<WoodAltar_Fujiman_Wind_Audio>
{
    public bool isFinishCreateWind;
    public void SetFinishCreateWind(bool value)
    {
        if(value == true && isFinishCreateWind == false)
        {
            AudioManager.Instance.PlayAudio("生成风场",AudioType.SoundEffect);
            isFinishCreateWind = value;
        }
    }
    public bool GetFinishCreateWind()
    {
        return isFinishCreateWind;
    }
}
