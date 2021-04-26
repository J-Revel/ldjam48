using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackCardBehaviour : MonoBehaviour
{
    public Transform darkProjectilePrefab;
    public float minLoadingDuration = 10, maxLoadingDuration = 60;
    public float shakeStartTime = 10;
    public float auraStartTime = 5;
    private float currentLoadingDuration;
    private float loadingTime;

    private ShakeFX shakeFX;
    private AuraFX auraFX;
    
    void Start()
    {
        shakeFX = GetComponentInChildren<ShakeFX>();
        auraFX = GetComponentInChildren<AuraFX>();
        currentLoadingDuration =  Random.Range(minLoadingDuration, maxLoadingDuration);
    }

    void Update()
    {
        loadingTime += Time.deltaTime;
        if(loadingTime > currentLoadingDuration)
        {
            loadingTime = 0;
            currentLoadingDuration =  Random.Range(minLoadingDuration, maxLoadingDuration);
            GameState.instance.SpawnDarkProjectile(transform.position);
        }
        shakeFX.value = Mathf.Max(0, (loadingTime - currentLoadingDuration + shakeStartTime) / shakeStartTime);
        auraFX.value = Mathf.Max(0, (loadingTime - currentLoadingDuration + auraStartTime) / auraStartTime);
    }
}
