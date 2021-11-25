using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField]float liveTime = 0.5f;
    [SerializeField]float Width = 0.05f;
    float timeCount;
    [SerializeField]Transform FlashIn;
    [SerializeField]Transform FlashOut;
    SpriteRenderer m_Sprite;
    [SerializeField]Material m_flashMaterial;
    [SerializeField]AnimationCurve curve;
    [SerializeField]Vector2 offset;
    private void Awake() {
        m_Sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        m_flashMaterial = m_Sprite.material;
    }

    private void OnEnable() {
        transform.position = (FlashIn.position + FlashOut.position)/2;
        var distence = (FlashIn.position - FlashOut.position).magnitude;
        transform.GetChild(0).localScale = new Vector3(distence ,distence ,1);
        transform.Rotate(0,0,180*Mathf.Atan2((FlashOut.position - FlashIn.position).y,(FlashOut.position - FlashIn.position).x)/Mathf.PI);
        m_flashMaterial.SetFloat("Speed",-Mathf.Log10(distence));
        m_flashMaterial.SetFloat("Width",Width/(Mathf.Sqrt(distence) + 1));
        AudioManager.Instance.PlayAudio("闪电",AudioType.SoundEffect,gameObject);
    }
    private void Update() {
        timeCount += Time.deltaTime;
        if(timeCount >= liveTime)
        {
            gameObject.SetActive(false);
            timeCount = 0;
        }

        m_Sprite.color  = new Color(m_Sprite.color.r,m_Sprite.color.g,m_Sprite.color.b,curve.Evaluate(timeCount));
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && other.GetComponent<PlayerMovement>() != null)
        {
            if(other.GetComponent<PlayerMovement>().IsOnGround)
            {
                other.GetComponent<IGetHurt>().GetHurt(transform);
            }
            else
            {
                other.transform.position = FlashOut.position + new Vector3(offset.x,offset.y);
            }
        }
    }
    private void OnDisable() {
        transform.rotation = new Quaternion();
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(FlashOut.position + new Vector3(offset.x,offset.y),1);
    }
}
