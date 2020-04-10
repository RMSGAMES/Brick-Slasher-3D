using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int _projectileCount = 3;
    [SerializeField] private int _bulletImpulse = 300;
    [SerializeField] private float _shootSpeed = 1f;

    [Header("References")]
    [SerializeField] private GameObject _projectile;
    [SerializeField] private ParticleSystem _muzzleEffect;

    [Header("Other")]
    [SerializeField] private GameController _gameController;
    [SerializeField] private PlayerUI _playerUI;

    #region Private
    private Camera _camera;
    private float _lastShotTime;
    #endregion

    private void Start()
    {
        _lastShotTime = 0.0f;
        _projectileCount = 3 * (_gameController.currentLevel + 1);

        _camera = Camera.main;
        _playerUI.UpdateProjectileCount(_projectileCount);
    }

    private void Update()
    {
        if (FireButton)
        {
            StartCoroutine(FireOneShot());
        }
    }

    /// <summary>
    /// Create and fire a bullet
    /// </summary>
    /// <returns></returns>
    private IEnumerator FireOneShot()
    {
        if (_projectileCount > 0)
        {
            if (Time.time > (_lastShotTime + _shootSpeed))
            {
                // generate a ray based on camera position + mouse cursor screen coordinate
                Ray ourRay = UtilityHelper.isMobile ? _camera.ScreenPointToRay(Input.GetTouch(0).position) : _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayHit = new RaycastHit(); // initialize forensics data container

                if (Physics.Raycast(ourRay, out rayHit))
                {
                    GameObject t_projectile = Instantiate(_projectile, _muzzleEffect.transform.position, _muzzleEffect.transform.rotation);
                    Rigidbody t_rigidbody = t_projectile.GetComponent<Rigidbody>();
                    _muzzleEffect.transform.LookAt(rayHit.point);
                    t_rigidbody.AddForce((_muzzleEffect.transform.forward) * _bulletImpulse);

                    _muzzleEffect.Play();

                    // Update projectile count
                    _projectileCount -= 1;
                    _playerUI.UpdateProjectileCount(_projectileCount);

                    _lastShotTime = Time.time;
                }
            }
        }
        else
        {
            yield return new WaitForSeconds(3f); // wait for set reload time

            if (!_gameController.isAllBarrelsDestroyed())
            {
                EventHandler.OnGameLoseEvent();
            }
        }
    }

    public bool FireButton
    {
        get
        {
#if !INPUT_MANAGER
            return Input.GetMouseButton(0);
#else
            return bl_Input.GetKey("Fire");
#endif
        }
    }
}