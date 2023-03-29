using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;
        public UserManager users;


        void Awake()
        {

            if (instance == null)
                instance = this;
            else if (instance != null)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);


            users = GetComponent<UserManager>();
        
    
        //InitGame();


        }

  //  public void AddUser()
   // {
       // users.user.Add("");



   // }


     }
