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

public class MainCanvas : MonoBehaviour {

    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Slider childrenPointer;
    private Settings settings;
    private int current;
    private int newValue;
    private DisplayData data;

    [SerializeField]
    private bool meditationOn = false;

    [SerializeField]
    private Image signalIcon;

    [SerializeField]
    private Sprite[] gallery;   //0:Connect; 1:Disconnect; 2,3,4:Connecting

	// Use this for initialization
	void Start () {
        current = 0;
        data = FindObjectOfType<DisplayData>();
        
        settings = FindObjectOfType<Settings>();
	}
	
	// Update is called once per frame
	void Update () {
        if (slider)
        {
            if (!meditationOn)
            {
                newValue = data.GetAttention();
            }
            else
            {
                newValue = data.GetMeditation();
            }
            ChangeVar();
            slider.value = current;
        }
        SignalConnection();
        if(childrenPointer)
            childrenPointer.value = settings.GetMinRang();
    }

    void ChangeVar()
    {
        if (current > newValue) current -= 1;
        if (current < newValue) current += 1;
    }

    void SignalConnection()
    {
        signalIcon.sprite = gallery[data.GetIndexSignalIcon()];
    }

    public int getCurrentValue()
    {
        return current;
    }
}
