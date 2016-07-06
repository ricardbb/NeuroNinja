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
using System.Collections;

public class LevelProgress : MonoBehaviour {

    private int i;

    [SerializeField]
    private GameObject[] obstacles;


    // Use this for initialization
    void Start () {
        i = 0;
	}
	
	// Update is called once per frame
	void Update () {
        CompletObstacle();
	}

    void CompletObstacle()
    {
        try
        {
            if (obstacles[i].GetComponent<ObstacleController>().GetCurrectValue() <= 0)
            {
                i++;
                PlayerScriptController.Instance.Advancing = true;
                CanvasManager.Instance.SetShowing();
            }
        }
        catch (IndexOutOfRangeException)
        {

        }
       
    }


    public int GetTasckController()
    {
        try
        {
            return obstacles[i].GetComponent<ObstacleController>().GetTasckControll();
        }
        catch (IndexOutOfRangeException)
        {
            return -1;
        }
    }

}
