/* 
	Copyright © 2016  Ricard Borrull Baraut

	This file is part of NeuroNinja.

    NeuroNinja is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    NeuroNinja is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with NeuroNinja.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasManager : MonoBehaviour
{

    private static CanvasManager instance;

    public static CanvasManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CanvasManager>();
            }
            return instance;
        }
    }


    [SerializeField]
    private Canvas[] canvas;
    [SerializeField]
    private Canvas mainCanvas;


    [SerializeField]
    private Canvas pauseCanvas;

    private Slider mainSlider;
    private Component[] childrenSlider;

    private bool isShowing = false;
    private bool disable = false;

    //[SerializeField]
    //private bool activaCanvas;
    private int canvasIndex;


    // Use this for initialization
    void Start()
    {
        canvasIndex = 0;
        try
        {
            mainSlider = mainCanvas.GetComponentInChildren<Slider>();
            childrenSlider = mainSlider.GetComponentsInChildren<Image>();
            foreach (Image i in childrenSlider)
            {
                i.enabled = false;
            }

        }
        catch (UnassignedReferenceException)
        {

        }

    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    public void ChangeCanvas()
    {
        //baseCanvas.enabled = !baseCanvas.enabled;
        canvas[canvasIndex].enabled = !canvas[canvasIndex].enabled;

        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            canvasIndex++;
        }
        else
        {
            Time.timeScale = 0f;
        }
    }

    public void SetShowing()
    {
        isShowing = !isShowing;
        if (!disable)
        {
            
            try
            {
                foreach (Image i in childrenSlider)
                {
                    i.enabled = !i.enabled;
                }
            }
            catch (NullReferenceException)
            {

            }
        }
        
    }

    public void PauseCanvas()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
        }
        pauseCanvas.enabled = !pauseCanvas.enabled;
    }

	public void HideSlider()
	{
		try
        {
                if (isShowing)
                {
                    disable = !disable;
                    mainSlider.enabled = !mainSlider.enabled;
                    foreach (Image i in childrenSlider)
                    {
                        i.enabled = !i.enabled;
                    }
                }
            
        }
        catch (NullReferenceException)
        {

        }
	}
	
    private void HandleInput()
    {
        try
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (isShowing)
                {
                    disable = !disable;
                    mainSlider.enabled = !mainSlider.enabled;
                    foreach (Image i in childrenSlider)
                    {
                        i.enabled = !i.enabled;
                    }
                }

            }
        }
        catch (NullReferenceException)
        {

        }
        
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1f)
        {
            PauseCanvas();
        }

    }

 
}
