using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitSprite : MonoBehaviour
{
    //private string _layerName = "Fragment";
    public float Gravity = 0.5f;
    public float DestroyTime = 5f;
    public float xOffset = -20f;
    public float yOffset = 10f;
    public float xscale = 1.5f;
    public float yscale = 1.0f;
    private int _splitPoint = 3;
    private float _splitForce = 50f;
    private int _seed = 0;
    private float _spriteWidth = 0;
    private float _spriteHeight = 0;

    Vector2 scale;
    private List<GameObject> _fragment = new List<GameObject>();
    private SpriteRenderer _spriteRenderer;
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if(_spriteRenderer == null)
        {
            throw new System.Exception("Cann't find SpriteRanderer");
        }
    }

    /// <summary>
    /// 调用此接口 实现分裂
    /// </summary>
    public void Split()
    {
        _spriteWidth = _spriteRenderer.sprite.texture.width;
        _spriteHeight = _spriteRenderer.sprite.texture.height;
        //scale = new Vector2(_spriteRenderer.gameObject.transform.localScale.x, _spriteRenderer.gameObject.transform.localScale.y);
        GetFragment(_spriteRenderer.sprite.texture, RandomSplits());
        for(int i = 0; i < _fragment.Count; ++i)
        {
            Boom(_fragment[i]);
        }
        Invoke("DestroyDelayTimer",DestroyTime);
    }
    void GetFragment(Texture2D texture2D, Vector2[] splits)
    {
        float[] splitX = new float[splits.Length + 2];
        float[] splitY = new float[splits.Length + 2];
        splitX[0] = 0;
        splitX[splitX.Length - 1] =  _spriteWidth;
        splitY[0] = 0;
        splitY[splitY.Length - 1] = _spriteHeight;
        for(int i = 0; i < splits.Length; ++i )
        {
            splitX[i + 1] = splits[i].x;
            //splitY[i + 1] = splits[i].y;
            splitY[ i + 1] = _spriteHeight - splits[i].y;
        }
        TSort<float> sort = new TSort<float>();
        sort.QuickSort(splitX, 0, splits.Length);
        sort.QuickSort(splitY, 0, splits.Length);

        //分割
        for(int i = 0; i < splitX.Length - 1; ++i)
        {
            for(int j = 0; j < splitY.Length - 1; ++j)
            {
                float x1 = splitX[i];
                float y1 = splitY[j];
                float x2 = splitX[i + 1];
                float y2 = splitY[j + 1];
                float centerX = gameObject.transform.position.x - gameObject.transform.localScale.x / 2 + (x1 + x2) / (2 * _spriteWidth);
                float centerY = gameObject.transform.position.y - gameObject.transform.localScale.y / 2 + (y1 + y2) / (2 * _spriteHeight);
                Rect rect = new Rect(x1, y1, x2 - x1, y2 - y1);
                // float cx = (x2 - x1) / 2;
                // float cy = (y2 - y1) / 2;
                // centerX = this.gameObject.transform.position.x + cx;
                // centerY = this.gameObject.transform.position.y + cy;
                Sprite sprite = Sprite.Create(texture2D, rect, Vector2.zero);
                Vector2 position = new Vector2(centerX, centerY);
                _fragment.Add(CreateFragment(sprite, position));
            }
        }
    }
    //- gameObject.transform.localScale.x / 2 + (x1 + x2) / (2 * _spriteWidth)
    //  - gameObject.transform.localScale.y / 2 + (y1 + y2) / (2 * _spriteHeight)

    //随机生成分割点
    Vector2[] RandomSplits()
    {
        System.Random random;
        Vector2[] splits = new Vector2[_splitPoint];

        float spanX = _spriteWidth / (2 * _splitPoint + 1);
        float spanY = _spriteHeight / (2 * _splitPoint + 1);

        for(int i = 0; i < _splitPoint; ++i)
        {
            random = new System.Random(unchecked((int)System.DateTime.Now.Ticks) + _seed);
            ++ _seed;

            double x = random.NextDouble() * spanX + 2 * (i + 1) * spanX;

            random = new System.Random(unchecked((int)System.DateTime.Now.Ticks) + _seed);
            ++ _seed;
            double y = random.NextDouble() * spanY + 2 * (i + 1) * spanY;

            splits[i] = new Vector2((float)x, (float)y);
        }

        return splits;
    }

    void Boom(GameObject fragment)
    {
        if(fragment != null)
        {
            Vector2 start = fragment.transform.position;
            Vector2 end = gameObject.transform.position;
            Vector2 direction = end - start;
            fragment.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,0) * _splitForce, ForceMode2D.Impulse);
        }
    }

    GameObject CreateFragment(Sprite sprite, Vector2 position)
    {
        GameObject fragment = new GameObject("Fragment");
        fragment.transform.localScale = new Vector2(fragment.transform.localScale.x * xscale, fragment.transform.localScale.y * yscale);
        position = new Vector2(Random.Range(xOffset,yOffset) + position.x, position.y);
        fragment.transform.position = position;
        fragment.AddComponent<SpriteRenderer>().sprite = sprite;
        fragment.GetComponent<SpriteRenderer>().sortingLayerName = "Environment";
        fragment.AddComponent<Rigidbody2D>();
        fragment.GetComponent<Rigidbody2D>().gravityScale = Gravity;
        fragment.AddComponent<BoxCollider2D>();
        return fragment;
    }
    void DestroyDelayTimer()
    {
        for(int i = 0; i < _fragment.Count; ++i)
        {
            if(_fragment[i] != null)
                Destroy(_fragment[i]);
        }
    }
}
