using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particle = default;
    [SerializeField]
    private SpriteRenderer sprite = default;
    [SerializeField]
    private CircleCollider2D circleCollider = default;

    private Vector3 objectInitialPosition;

    private void Awake() 
    {
        objectInitialPosition = transform.localPosition;
    }

    private void OnEnable() 
    {
        transform.localPosition = objectInitialPosition;
        sprite.enabled = true;
        circleCollider.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 9)
        {
            Data.instance.CollectCoin();
            particle.Play();
            sprite.enabled = false;
            circleCollider.enabled = false;
        }
    }
}
