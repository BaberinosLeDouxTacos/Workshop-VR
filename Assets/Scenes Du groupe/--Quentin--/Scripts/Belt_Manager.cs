﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Belt_Manager : MonoBehaviour {

    [SerializeField] VRTK_TransformFollow transformFollowScript;
    public Transform transformToFollow;

    private void OnEnable()
    {
        //transformFollowScript = GetComponent<VRTK_TransformFollow>();
        StartCoroutine(MyCouroutine());
    }

	IEnumerator MyCouroutine()
    {
        yield return null;
        transformToFollow = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.Headset);
        VRTK_SDKSetup sdksetup = transformToFollow.GetComponentInParent<VRTK_SDKSetup>();
        transformFollowScript.gameObjectToFollow = sdksetup.transform.GetChild(0).gameObject;
    }
}
