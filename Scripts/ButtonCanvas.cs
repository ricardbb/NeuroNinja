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

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonCanvas : MonoBehaviour {

    [SerializeField]
    private Sprite[] sprites;

    [SerializeField]
    private string[] content;

    [SerializeField]
    private Text currentText;

    [SerializeField]
    private Image currentImage;
    [SerializeField]
    private Audio records;

    private Canvas canvas;

    [SerializeField]
    private bool isFinish = false;

    private int index;

    [SerializeField]
    private Sprite[] gallery;
    [SerializeField]
    private Image icon;
    private static int audioOn = 0;

    // Use this for initialization
    void Start () {
        index = -1;
        canvas = GetComponent<Canvas>();

        //records.PlayAudioSource(index);
    }
	
	// Update is called once per frame
	void Update () {
        if (records)
        {
            if (canvas.enabled && index == -1)
            {
                index = 0;
                if(audioOn==0)
                     records.PlayAudioSource(index);
            }
            if(index!=-1 && !isFinish)
                if (!records.IsAudioSourcePlaying(index) && canvas.enabled && audioOn==0)
                {
                    ButtonNext();
                }
            ChangeSprite();
        }
        
    }

   

    public void ButtonNext()
    {
        if (index + 1 < sprites.Length && index + 1 < content.Length)
        {
            records.StopAudioSource(index);
            index++;
            ChangeContents();
            if(audioOn==0)
                records.PlayAudioSource(index);
        }
        else
        {
            ButtonClose();
        }
    }

    public void ButtonPrev()
    {
        if (index - 1 >= 0)
        {
            records.StopAudioSource(index);
            index--;
            ChangeContents();
            if(audioOn==0)
                records.PlayAudioSource(index);
        }
    }

    public void ButtonClose()
    {
        records.StopAudioSource(index);
        CanvasManager.Instance.ChangeCanvas();
    }

    public void ButtonPlayAudio()
    {
        if (audioOn == 0)
        {
            audioOn = 1;
            records.StopAudioSource(index);
        }
        else
        {
            audioOn = 0;
            records.PlayAudioSource(index);
        }

        
        ChangeSprite();
    }

    private void ChangeSprite()
    {
        icon.sprite = gallery[audioOn];
    }

    public void ButtonContinue()
    {
        CanvasManager.Instance.PauseCanvas();
    }

    public void ChangeScene(string name)
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
        Application.LoadLevel(name);
    }

    public void RetryLevel()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
        Application.LoadLevel(Application.loadedLevel);
    }

    public void BtnQuit()
    {
        Application.Quit();
    }

    void ChangeContents()
    {
        currentText.text = content[index];
        currentImage.sprite = sprites[index];
        currentImage.SetNativeSize();
    }

    public void ButtonSettings()
    {
        Settings.Instance.ButtonDone();
    }
}
