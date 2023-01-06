using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertMiniMap : MonoBehaviour
{
    public void InstantiateMiniMap(GameObject Canvas, GameObject PosImage, Transform pos)
    {
        GameObject newCanvas = Instantiate(Canvas, transform);

        GameObject createImage;

        createImage = Instantiate(PosImage) as GameObject;
        createImage.transform.SetParent(newCanvas.transform, false);

        createImage.GetComponent<RectTransform>().localPosition = new Vector3((pos.localPosition.x / 50) + (pos.localPosition.y / 1.5f) - 4, (pos.localPosition.z / 50), 0);
    }
}
