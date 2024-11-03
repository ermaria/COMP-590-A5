using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handle : MonoBehaviour
{
    [SerializeField] private float extendSpeed = 0.1f; // Extend/collapse speed
    private bool weaponActive = true;                  // State of the lightsaber (on/off)
    private float scaleMin = 0;                        // Minimum scale value
    private float scaleMax;                            // Maximum scale value
    private float extendDelta;                         // Interpolation value
    private float scaleCurrent;                        // Current scale value along the y-axis
    private float localScaleX;                         // Initial x scale of the blade
    private float localScaleZ;
    public GameObject blade;
    //[SerializeField] private AudioClip extendSound;
    [SerializeField] private AudioClip collapseSound;
    public AudioSource audioSource;
    //private bool isExtending = true;
    // Start is called before the first frame update
    void Start()
    {
        localScaleX = transform.localScale.x;
        localScaleZ = transform.localScale.z;

        // Set the maximum scale value along the y-axis to the current y-axis scale
        scaleMax = transform.localScale.y;

        // Initialize current scale to maximum (blade starts on and fully extended)
        scaleCurrent = scaleMax;

        // Calculate interpolation value based on max scale and extend speed
        extendDelta = scaleMax / extendSpeed;

        // Set initial weapon state to active (blade on)
        weaponActive = true;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Invert extendDelta to toggle extension/collapse
            //extendDelta = weaponActive ? -Mathf.Abs(extendDelta) : Mathf.Abs(extendDelta);
            extendDelta = -extendDelta;
            Debug.Log("Spacebar pressed. New extendDelta: " + extendDelta);
            if (weaponActive)
            {
                audioSource.PlayOneShot(collapseSound, 1.0F);
            }
            else
            {
                audioSource.PlayOneShot(collapseSound, 1.0F);
            }
        }


        //extendDelta = isExtending ? Mathf.Abs(extendDelta) : -Mathf.Abs(extendDelta);

        // Adjust the current scale based on extendDelta and time elapsed
        scaleCurrent += extendDelta * Time.deltaTime;

        // Clamp scaleCurrent to ensure it stays within min and max values
        scaleCurrent = Mathf.Clamp(scaleCurrent, scaleMin, scaleMax);

        // Apply the current scale to the blade's local y-axis
        transform.localScale = new Vector3(localScaleX, scaleCurrent, localScaleZ);

        // Update lightsaber state based on whether the blade is visible
        weaponActive = scaleCurrent > 0;

        if (weaponActive && !blade.activeSelf)
        {
            blade.SetActive(true);
            //audioSource.PlayOneShot(collapseSound);
        }
        else if (!weaponActive && blade.activeSelf)
        {
            blade.SetActive(false);
            //audioSource.PlayOneShot(collapseSound);
        }
        Debug.Log("scaleCurrent: " + scaleCurrent + ", weaponActive: " + weaponActive);
    }
}
