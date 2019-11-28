using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public GameObject obj;
    Vector3 sum_directions = new Vector3();

    // Constructor
    public Seed(GameObject seed_prefab, Vector3 position){
        this.obj = Instantiate(seed_prefab, position, Quaternion.identity);
    }

    public void add_direction(Vector3 direction){
        this.sum_directions += direction;
    }

    public Vector3 get_unit_direction(){
        Vector3 unit_dir = Vector3.Normalize(this.sum_directions);

        // Resetting sum directions if this function is called
        this.sum_directions = new Vector3(0,0,0); 

        return unit_dir;
    }
}
