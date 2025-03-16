using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUIPack_CasualGame
{

    public class Rotation : MonoBehaviour
    {
        void Update()
        {
            this.transform.Rotate(new Vector3(0, 0, 100f * Time.deltaTime));

        }
    }
}

