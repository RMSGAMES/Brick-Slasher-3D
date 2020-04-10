using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffect;
    
    private void OnCollisionEnter(Collision coll)
    {
        //_explosionEffect.Play();
        Destroy(gameObject);
    }
}
