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

using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Settings : MonoBehaviour {

    private static Settings instance;

    public static Settings Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Settings>();
            }
            return instance;
        }
    }

    [SerializeField]
    private Button[] buttons;

    private static int indexButtons=1;

    private PlayerScriptController player;

    [SerializeField]
    private Canvas canvas;

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerScriptController>();
        SetDificulty(indexButtons);

    }
	
	// Update is called once per frame
	void Update () {
	
	}

   public void ButtonDone()
    {
        canvas.enabled = !canvas.enabled;
        if(canvas.enabled)
            EventSystem.current.SetSelectedGameObject(buttons[indexButtons].gameObject, new BaseEventData(EventSystem.current));
    }

    //Parametrer: dificulty: 0 for easy; 1 for normal; 2 for hard;
    public void SetDificulty(int dificulty)
    {
        switch (dificulty)
        {
            case 0:
                player.SetMeditationRange(40);
                player.SetMeditationHightRange(65);

                player.SetAttentionRange(40);
                player.SetAttentionHightRange(65);

                indexButtons = 0;
                break;

            case 1:
                player.SetMeditationRange(60);
                player.SetMeditationHightRange(85);

                player.SetAttentionRange(60);
                player.SetAttentionHightRange(85);

                indexButtons = 1;
                break;

            case 2:
                player.SetMeditationRange(75);
                player.SetMeditationHightRange(100);

                player.SetAttentionRange(75);
                player.SetAttentionHightRange(100);

                indexButtons = 2;
                break;
        }
    }

    public int GetMinRang()
    {
        switch (indexButtons)
        {
            case 0: return 40;

            case 1: return 60;

            case 2: return 75;
        }
        return 0;
    }

}
