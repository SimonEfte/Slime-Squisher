using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GUIPack_CasualGame
{
    public class AnimateColors : MonoBehaviour
    {
        [Tooltip("A list of the colors that will be animated")]
        public Color[] colorList;

        [Tooltip("The index number of the current color in the list")]
        public int colorIndex = 0;

        [Tooltip("How long the animation of the color change lasts, and a counter to track it")]
        public float changeTime = 1;
        public float changeTimeCount = 0;

        [Tooltip("How quickly the sprite animates from one color to another")]
        public float changeSpeed = 0.2f;

        [Tooltip("Is the animation paused?")]
        public bool isPaused = false;

        [Tooltip("Is the animation looping?")]
        public bool isLooping = true;

        [Tooltip("Should we start from a random color index")]
        public bool randomColor = false;

        void Start()
        {
            //Apply the chosen color to the sprite or text mesh
            SetColor();
        }

        // Update is called once per frame
        void Update()
        {
            //If the animation isn't paused, animate it over time
            if (isPaused == false)
            {
                if (changeTime > 0)
                {
                    //Count down to the next color change
                    if (changeTimeCount > 0)
                    {
                        changeTimeCount -= Time.deltaTime;
                    }
                    else
                    {
                        changeTimeCount = changeTime;

                        //Switch to the next color
                        if (colorIndex < colorList.Length - 1)
                        {
                            colorIndex++;
                        }
                        else
                        {
                            if (isLooping == true) colorIndex = 0;
                        }
                    }
                }

                //If we have a text mesh, animate its color
                if (GetComponent<TextMesh>())
                {
                    GetComponent<TextMesh>().color = Color.Lerp(GetComponent<TextMesh>().color, colorList[colorIndex], changeSpeed * Time.deltaTime);
                }

                //If we have a sprite renderer, animate its color
                if (GetComponent<SpriteRenderer>())
                {
                    GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, colorList[colorIndex], changeSpeed * Time.deltaTime);
                }

                //If we have a UI image, animate its color
                if (GetComponent<Image>())
                {
                    GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, colorList[colorIndex], changeSpeed * Time.deltaTime); 
                }
            }
            else
            {
                //Apply the chosen color to the sprite or text meshh
                SetColor();
            }
        }

        /// <summary>
        /// Applies the chosen color to the sprite based on the index from the list of colors
        /// </summary>
        void SetColor()
        {
            //A random color
            int tempColor = 0;

            //Set the color randomly
            if (randomColor == true) tempColor = Mathf.FloorToInt(Random.value * colorList.Length);

            //If you have a text mesh component attached to this object, set its color
            if (GetComponent<TextMesh>())
            {
                GetComponent<TextMesh>().color = colorList[tempColor];
            }

            //If you have a sprite renderer component attached to this object, set its color
            if (GetComponent<SpriteRenderer>())
            {
                GetComponent<SpriteRenderer>().color = colorList[tempColor];
            }

            // If you have a UI image attached to this object, set its color
            if (GetComponent<Image>())
            {
                GetComponent<Image>().color = colorList[tempColor];
            }
        }

        /// <summary>
        /// Sets the pause state of the color animation
        /// </summary>
        /// <param name="pauseState">Pause state, true paused, false unpaused</param>
        void Pause(bool pauseState)
        {
            isPaused = pauseState;
        }

    }
}

