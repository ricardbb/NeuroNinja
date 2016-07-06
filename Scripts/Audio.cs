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

public class Audio : MonoBehaviour {
    [SerializeField]
    private AudioSource[] sound;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayAudioSource(int index)
    {
        if(!sound[index].isPlaying)
            sound[index].Play();
    }

    public void StopAudioSource(int index)
    {
        if (sound[index].isPlaying)
            sound[index].Stop();
    }

    public bool IsAudioSourcePlaying(int index)
    {
        return sound[index].isPlaying;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        for(int i=0; i< sound.Length; i++)
           sound[i].Play();
        
    }

}
