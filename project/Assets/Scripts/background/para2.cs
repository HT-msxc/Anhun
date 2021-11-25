using UnityEngine;
using System.Collections;

public class para2 : MonoBehaviour
{
    public GameObject[] clouds;//云层
    public GameObject[] nearHills;//近山
    public GameObject[] farHills;//远山
    public GameObject[] lava;//地面

    // 移动的速度
    public float cloudLayerSpeedModifier;
    public float nearHillLayerSpeedModifier;
    public float farHillLayerSpeedModifier;
    public float lavalLayerSpeedModifier;

    public Camera myCamera;

    private Vector3 lastCamPos;

    void Start()
    {
        lastCamPos = myCamera.transform.position;//获取相机的位置
    }

    void Update()
    {
        Vector3 currCamPos = myCamera.transform.position;
        float yPosDiff = lastCamPos.y - currCamPos.y;//计算相机y轴的变化

        adjustParallaxPositionsForArray(clouds, cloudLayerSpeedModifier, yPosDiff);
        adjustParallaxPositionsForArray(nearHills, nearHillLayerSpeedModifier, yPosDiff);
        adjustParallaxPositionsForArray(farHills, farHillLayerSpeedModifier, yPosDiff);
        adjustParallaxPositionsForArray(lava, lavalLayerSpeedModifier, yPosDiff);

        lastCamPos = myCamera.transform.position;
    }
    // 数组来存储游戏对象
    void adjustParallaxPositionsForArray(GameObject[] layerArray, float layerSpeedModifier, float yPosDiff)
    {
        // 遍历改变精灵的位置
        for (int i = 0; i < layerArray.Length; i++)
        {
            Vector3 objPos = layerArray[i].transform.position;
            objPos.y += yPosDiff * layerSpeedModifier;
            layerArray[i].transform.position = objPos;
        }
    }
}