using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GhostShadow : MonoBehaviour
{
    // Start is called before the first frame update
    public float waitingTime;
    public float existingTime;
    public SpriteRenderer spriteRenderer;
    Sprite currentSprite;
    float alpha;
    public int numOfGhostShadow;
    public int shadowCount;
    public AnimationCurve alphaCurve;
    public Queue<GameObject> freeGhostShadows;
    public Queue<GameObject> busyGhostShadow;
    public GameObject ghostShadowPrefab;
    public Color color;
    public int currentOrder;
    private void Awake() 
    {
        ghostShadowPrefab = Resources.Load<GameObject>("Prefab/GhostShadow");
    }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        freeGhostShadows = new Queue<GameObject>();
        busyGhostShadow = new Queue<GameObject>();
    }

    public void ShowGhostShadows()
    {
        if (numOfGhostShadow == 0)
            return;
        shadowCount = 0;
        currentOrder = GetComponent<SpriteRenderer>().sortingOrder - numOfGhostShadow - 1;
        Invoke(nameof(CreateGhostShadow), waitingTime);
    }

    public void CreateGhostShadow()
    {
        float xtime =  alphaCurve.Evaluate(1f * shadowCount / numOfGhostShadow);
        currentSprite = spriteRenderer.sprite;
        GameObject shadow;
        if (freeGhostShadows.Count <= 0)
        {
            shadow = Instantiate<GameObject>(ghostShadowPrefab);
            shadow.SetActive(false);
            freeGhostShadows.Enqueue(shadow);
        }
        else 
            shadow = freeGhostShadows.Dequeue();
        SpriteRenderer targetRenderer = shadow.GetComponent<SpriteRenderer>();
        targetRenderer.sortingOrder = currentOrder++;
        shadow.transform.position = transform.position;
        shadow.transform.rotation = transform.rotation;
        shadow.transform.localScale = transform.localScale;
        targetRenderer.sprite = currentSprite;
        targetRenderer.color = new Color(color.r,color.g,color.b,alphaCurve.Evaluate(xtime));
        shadow.SetActive(true);
        busyGhostShadow.Enqueue(shadow);
        shadowCount++;
        if (shadowCount < numOfGhostShadow)
            Invoke(nameof(CreateGhostShadow), waitingTime);
        Invoke(nameof(ResolveGhostShadow), existingTime);
    }

    public void ResolveGhostShadow()
    {
        GameObject newShadow = busyGhostShadow.Dequeue();
        SpriteRenderer targetRenderer = newShadow.GetComponent<SpriteRenderer>();
        targetRenderer.sprite = null;
        newShadow.SetActive(false);
        freeGhostShadows.Enqueue(newShadow);
    }

    public void RefreshGhostShadows()
    {
        if (freeGhostShadows != null)
            freeGhostShadows.Clear();
        if (busyGhostShadow != null)
            busyGhostShadow.Clear();
    }
}