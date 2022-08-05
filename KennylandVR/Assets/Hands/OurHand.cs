using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class OurHand : MonoBehaviour
{
    public GameObject ourHandPrefab;
    public InputDeviceCharacteristics ourControllerCharacteristics;

    private InputDevice ourDevice;
    private Animator ourHandAnimator;

    // Start is called before the first frame update
    void Start()
    {
        InitializeOurHand();
    }
    void InitializeOurHand()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(ourControllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            ourDevice = devices[0];

            GameObject newHand = Instantiate(ourHandPrefab, transform);
            ourHandAnimator = newHand.GetComponent<Animator>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (ourDevice.isValid)
        {
            UpdateOurHand();
        }
        else
        {
            InitializeOurHand();
        }
        void UpdateOurHand()
        {
            if (ourDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
            {
                ourHandAnimator.SetFloat("Trigger", triggerValue);
            }
            else
            {
                ourHandAnimator.SetFloat("Trigger", 0);
            }

            if (ourDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
            {
                ourHandAnimator.SetFloat("Grip", gripValue);
            }
            else
            {
                ourHandAnimator.SetFloat("Grip", 0);
            }
        }
    }
}