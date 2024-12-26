using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;

public class FireBaseManager : MonoBehaviour
{
    private DatabaseReference _reference;
    private FirebaseUser _idUser;

    private void Awake()
    {
        _reference = FirebaseDatabase.DefaultInstance.RootReference;
        _idUser = FirebaseAuth.DefaultInstance.CurrentUser;
    }
}
