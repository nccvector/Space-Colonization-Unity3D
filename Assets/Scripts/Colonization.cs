using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colonization : MonoBehaviour
{
    int food_iter = 0;
    int seed_iter = 0;
    int gen_per_frame = 100;

    public GameObject food_prefab;
    public GameObject seed_prefab;

    public int num_food_sources = 500;
    public int num_init_seeds = 50;
    public int max_seed_count = 10000;

    public float allowed_min_dist = 5f;
    public float kill_dist = 0.2f;
    public float step = 0.1f;

    List<GameObject> food_sources = new List<GameObject>();
    List<GameObject> seeds = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

        // Instantiating and filling food source list
        for(int i=0; i<num_food_sources; i++){
            // Spreading random food in space
            Vector3 random_pos = transform.position + new Vector3(Random.Range(-5,5),Random.Range(-5f,5f),Random.Range(-5,5));
            GameObject random_source = Instantiate(food_prefab, random_pos, Quaternion.identity);

            food_sources.Add(random_source);
        }

        // Instantiating and filling seeds list
        for(int i=0; i<num_init_seeds; i++){
            // Spreading random food in space
            Vector3 random_pos = transform.position + new Vector3(Random.Range(-5,5),Random.Range(-5f,5f),Random.Range(-5,5));
            GameObject random_seed = Instantiate(seed_prefab, random_pos, Quaternion.identity);
            random_seed.AddComponent<Seed>();

            seeds.Add(random_seed);
        }

        StartCoroutine(CorColonA());
    }

    void Colon()
    { 
        food_iter += 1;
        seed_iter += 1;
        
        // Generate seeds only if max count is not reached
        if(seeds.Count < max_seed_count){

            // Resetting frame iter
            if(food_iter >= food_sources.Count){
                food_iter = 0;
            }

            // Resetting seed iter
            if(seed_iter >= seeds.Count){
                seed_iter = 0;
            }

            GameObject food = food_sources[food_iter];

            GameObject closest_seed = null;
            float min_dist = Mathf.Infinity;

            Vector3 vec_2_add = new Vector3();

            // Finding valid closest seed
            foreach(GameObject seed in seeds){
                Vector3 dist_vec = food.transform.position - seed.transform.position;
                float dist_val = Vector3.Magnitude(dist_vec);

                if(dist_val <= allowed_min_dist && dist_val < min_dist){
                    closest_seed = seed;
                    vec_2_add = dist_vec;
                    min_dist = dist_val;
                }
            }

            // Check if a closest seed is found
            if(closest_seed != null){
                closest_seed.GetComponent<Seed>().add_direction(vec_2_add);
                if(min_dist <= kill_dist){
                    food_sources.Remove(food);
                    GameObject.Destroy(food, 0.0f);
                }
            }

            // Adding a new seed to seeds
            Vector3 dir_vector = seeds[seed_iter].GetComponent<Seed>().get_unit_direction();

            if(Vector3.Magnitude(dir_vector) >= 0.99f){
                GameObject new_seed = Instantiate(seed_prefab, seeds[seed_iter].transform.position + dir_vector * step, Quaternion.identity);
                new_seed.AddComponent<Seed>();
                seeds.Add(new_seed);
            }
        }
    }

    // Coroutine
    IEnumerator CorColonA()
    { 
        while(true){
            for(int i=0; i<25; i++)
            {
                Colon();
            }

            yield return new WaitForSeconds(0.01666f);
        }
    }
}
