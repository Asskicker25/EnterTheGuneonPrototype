using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Input = RedLabsGames.Utls.Input.ActiveInputController;

namespace RedLabsGames.Samples
{
    public class SampleUsage : MonoBehaviour
    {

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("Hello world");
            }
        }
    }
}
