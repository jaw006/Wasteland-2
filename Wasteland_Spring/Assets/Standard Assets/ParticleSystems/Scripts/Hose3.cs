

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR.InteractionSystem.Sample;

// JW - Notes 5/3/2020:
// This script is the interface for Hose.cs script (mouse only) and Valve's VR Interaction System
//
// Bugs addressed:
    // Hose turns on but doesn't turn off
// TODO: Make hose system more robust
// Hose script hooks into Hose2
// Objects that need to be controlled in 2D debug mode must be under NoSteamVRFallbackObjects
// SetHoseState.cs script sets the hose state
// HoseState.cs activates the Hose object

namespace Valve.VR.InteractionSystem.Sample
{
    public class Hose3 : MonoBehaviour
    {
        public SteamVR_Action_Boolean plantAction;

        public Hand hand;

        public GameObject prefabToPlant;
        public Hose hose;

        private bool hoseOn = false;

        private void Start()
        {
           // prefabToPlant.SetActive(false);
        }
        private void OnEnable()
        {
            if (hand == null)
                hand = this.GetComponent<Hand>();

            if (plantAction == null)
            {
                Debug.LogError("<b>[SteamVR Interaction]</b> No plant action assigned", this);
                return;
            }

            plantAction.AddOnUpdateListener(OnPlantActionChange, hand.handType);
//            plantAction.AddOnChangeListener(OnPlantActionChange, hand.handType);
        }

        private void OnDisable()
        {
            if (plantAction != null)
                plantAction.RemoveOnUpdateListener(OnPlantActionChange, hand.handType);
//                plantAction.RemoveOnChangeListener(OnPlantActionChange, hand.handType);
        }

        private void Update()
        {
            if(hoseOn) {
                hose.FireProjectile();
            }
        }

        private void OnPlantActionChange(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue)
        {
            if (newValue)
            {
                Plant();
            }
        }

        public void Plant()
        {
            hoseOn = !hoseOn;
        }


    }

}
