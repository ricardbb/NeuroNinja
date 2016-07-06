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
using System.Collections;

public class ThunderLight : MonoBehaviour {

    [SerializeField]
    private Light thunder;

    private float thunderTimer;

    private float thunderDuration;

    private bool on;

	// Use this for initialization
	void Start () {
        thunderDuration = 1.2f;
        thunderTimer = 0f;
        on = false;
	}
	
	// Update is called once per frame
	void Update () {
         if (on) Lighting();
    }


    void Lighting()
    {
        thunderTimer += Time.deltaTime;

        if (thunderTimer >= thunderDuration/4)
        {
            thunder.intensity = 1F;
        }

        if (thunderTimer >= thunderDuration / 3)
        {
            thunder.intensity = 0.0F;
        }

        if (thunderTimer >= thunderDuration / 2)
        {
            thunder.intensity = 1F;
        }

        if (thunderTimer >= thunderDuration)
        {
            thunder.intensity = 0.0F;
            on = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            on = true;
        }
    }

}
