using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
public class SimpleShoot : MonoBehaviour
{
    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")] [SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 150f;

    public AudioSource audioSource;
    public AudioClip fireSound;
    public AudioClip reload;
    public AudioClip outOfAmmo;

    public Magazine magazine;
    public XRBaseInteractor socketInteractor;
    private bool hasSlide = true;

    public void AddMagazine(XRBaseInteractable interactable)
    {
        magazine = interactable.GetComponent<Magazine>();
        audioSource.PlayOneShot(reload);
        hasSlide = false;
    }

    public void RemoveMagazine(XRBaseInteractable interactable)
    {
        magazine = null;
        audioSource.PlayOneShot(reload);
    }

    public void Slide()
    {
        hasSlide = true;
        audioSource.PlayOneShot(reload);
    }

    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();

        socketInteractor.onSelectEntered.AddListener(AddMagazine);
        socketInteractor.onSelectExited.AddListener(RemoveMagazine);
    }

    public void Fire()
    {
        if(magazine && magazine.numberOfBullet > 0 && hasSlide)
        {
            gunAnimator.SetTrigger("Fire");
        }
        else
        {
            audioSource.PlayOneShot(outOfAmmo);
        }
    }

    //This function creates the bullet behavior
    void Shoot()
    {
        if(magazine != null)
            magazine.numberOfBullet--;

        audioSource.pitch = Random.Range(0.9f, 1.0f);
        audioSource.PlayOneShot(fireSound);

        if (muzzleFlashPrefab)
        {
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            Destroy(tempFlash, destroyTimer);
        }

        if (!bulletPrefab) return;

        Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);

    }

    //This function creates a casing at the ejection slot
    void CasingRelease()
    {
        if (!casingExitLocation || !casingPrefab)
        { return; }

        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;

        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        Destroy(tempCasing, destroyTimer);
    }

}
