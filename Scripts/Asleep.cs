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

public class Asleep : MonoBehaviour {

    [SerializeField]
    private Slider slider;

    private float newValue;

    private float timer;

    [SerializeField]
    private float duration = 5f;

    [SerializeField]
    private AudioSource sound;

    private PlayerScriptController player;
    private int meditation;
    private int medRange;
    private int medHightRange;


	// Use this for initialization
	void Start () {
        timer = 0f;
        slider.value = 0;
        player = PlayerScriptController.Instance;
        
    }
	
	// Update is called once per frame
	void Update () {
        meditation = player.getMeditation();
        medRange = player.GetMeditationRange();
        medHightRange = player.GetMeditationHightRange();

        if (medRange <= meditation)
        {
            if (medHightRange <= meditation)
            {
                timer += Time.deltaTime*2;
            }
            else
            {
                timer += Time.deltaTime;
            }
            newValue = (timer * 100 / duration);
            slider.value = newValue;
            if (!sound.isPlaying && newValue<100)
                sound.Play();
        }
        else { if (sound.isPlaying) sound.Stop(); }

        if ( Time.timeScale == 0f )
            sound.Stop();
        EndScene();
    }

    void EndScene()
    {
        if(slider.value >= 100)
        {
            player.Sleeping = false;
        }
    }

}
