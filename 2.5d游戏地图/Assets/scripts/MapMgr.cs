using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapType
{
    水,土,麦子
}
public class MapMgr : MonoBehaviour
{
    static MapMgr _get;
    public static MapMgr Get
    {
        get
        {
            if (_get == null)
            {
                _get = GameObject.Find("GameObject").GetComponent<MapMgr>();
            }
            return _get;
        }
    }
    public MapItrm[,] mapItems;
    public List<MapItrm> sele;
    GameObject obj;

    float high = 1.086f;
    float side = 2.05f;
    int x_max;
    int y_max;
    private void Start()
    {
        sele = new List<MapItrm>();
        obj = Resources.Load<GameObject>("land_water");
    }
    
    public void BeginCreate(int _x_max, int _y_max)
    {
        if (transform.childCount != 0)
        {
            foreach (Transform item in transform)
            {
                Destroy(item.gameObject);
            }
        }
        x_max = _x_max;
        y_max = _y_max;

        mapItems = new MapItrm[x_max + 1, y_max + 1];
        for (int i = 0; i < x_max; i++)
        {
            for (int j = 0; j < y_max; j++)
            {
                float x = (j - i) * side / 2;
                float y = (j + i) * high / 2;
                GameObject obj1 = Instantiate(obj);
                obj1.transform.position = new Vector3(x, y, 0);
                obj1.transform.SetParent(transform, false);
                mapItems[i, j] = obj1.GetComponent<MapItrm>();
                mapItems[i, j].SetPos(i, j);
            }
        }
    }
    public void ChangeTerrain(string name)
    {
        for (int i = 0; i < sele.Count; i++)
        {
            Sprite sprite = Resources.Load<Sprite>(name);
            sele[i].SetSprite(sprite);
        }
        sele.Clear();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                MapItrm mapItrm = getGameXY(hit.point, hit.collider.gameObject.GetComponent<MapItrm>());
                mapItrm.Selected();
            }
        }
    }
    public MapItrm getGameXY(Vector2 pos, MapItrm tile)
    {
        Vector3 p = new Vector3(pos.x, pos.y, 0);// Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 0));

        int x = tile.x;
        int y = tile.y;

        Vector3 tilePos = tile.transform.position;
        ///八个点
        Vector3 leftTop = tilePos + new Vector3(-side, -high);
        Vector3 topMiddle = tilePos + new Vector3(0, -high / 2);
        Vector3 rightTop = tilePos + new Vector3(side, -high);
        Vector3 rightMiddle = tilePos + new Vector3(side, 0);
        Vector3 rightBottom = tilePos + new Vector3(side, high);
        Vector3 bottomMiddle = tilePos + new Vector3(0, high / 2);
        Vector3 leftBottom = tilePos + new Vector3(-side, high);
        Vector3 leftMiddle = tilePos + new Vector3(-side, 0);
        //判断是否在左上三角形
        bool isLeftTop = PointinTriangle(leftTop, topMiddle, leftMiddle, p);
        if (isLeftTop)
        {
            //Debug.Log("isLeftTop********************** " + x + "  " + (y - 1));
            return mapItems[x, y - 1];
        }
        bool isRightTop = PointinTriangle(topMiddle, rightTop, rightMiddle, p);
        if (isRightTop)
        {
            //Debug.Log("isRightTop******************************** " + (x - 1) + "  " + y);
            return mapItems[x - 1, y];
        }
        bool isRightBottom = PointinTriangle(rightMiddle, rightBottom, bottomMiddle, p);
        if (isRightBottom)
        {
            // Debug.Log("isRightBottom******************************" + x + "  " + y + 1);
            return mapItems[x, y + 1];
        }
        bool isLeftBottom = PointinTriangle(leftMiddle, bottomMiddle, leftBottom, p);
        if (isLeftBottom)
        {
            //Debug.Log("isLeftBottom******************************" + x + 1 + "  " + y);
            return mapItems[x + 1, y];
        }
        return tile;

    }
    public bool PointinTriangle(Vector3 A, Vector3 B, Vector3 C, Vector3 P)
    {
        Vector3 v0 = C - A;
        Vector3 v1 = B - A;
        Vector3 v2 = P - A;

        //float dot00 = v0.Dot(v0);
        float dot00 = Vector3.Dot(v0, v0);
        //float dot01 = v0.Dot(v1);
        float dot01 = Vector3.Dot(v0, v1);
        //float dot02 = v0.Dot(v2);
        float dot02 = Vector3.Dot(v0, v2);
        //float dot11 = v1.Dot(v1);
        float dot11 = Vector3.Dot(v1, v1);
        //float dot12 = v1.Dot(v2);
        float dot12 = Vector3.Dot(v1, v2);

        float inverDeno = 1 / (dot00 * dot11 - dot01 * dot01);

        float u = (dot11 * dot02 - dot01 * dot12) * inverDeno;
        if (u < 0 || u > 1) // if u out of range, return directly
        {
            return false;
        }

        float v = (dot00 * dot12 - dot01 * dot02) * inverDeno;
        if (v < 0 || v > 1) // if v out of range, return directly
        {
            return false;
        }

        return u + v <= 1;
    }
}
