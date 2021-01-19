using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItrm : MonoBehaviour
{
    SpriteRenderer rend;
    public MapType mapType;
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        mapType = MapType.水;
    }
    public int x;
    public int y;
    public void SetPos(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
    public void SetSprite(Sprite sprite)
    {
        rend.color = Color.white;
        color = Color.white;
        rend.sprite = sprite;
    }
    Color color = Color.white;
    public void Selected()
    {
        if (color == Color.white)
        {
            MapMgr.Get.sele.Add(this);
            rend.color = Color.red;
            color = Color.red;
        }
        else
        {
            MapMgr.Get.sele.Remove(this);
            rend.color = Color.white;
            color = Color.white;
        }

    }
    void Update()
    {

    }
}
