using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Firestore;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        
        private void Awake()
        {
            UniTask.Void(async () =>
            {
                DependencyStatus dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
                
                if (dependencyStatus == DependencyStatus.Available)
                {
                    Debug.Log("Firebase is ready to use");
                }
                else
                {
                    Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
                }

                var firestoreCollection = FirebaseFirestore.DefaultInstance.Collection("root");
                var doc = firestoreCollection.Document("test");
                //データを取得
                var snapshot = await doc.GetSnapshotAsync();

                var name = snapshot.ToDictionary()["name"];
                text.text = name.ToString();
            });
        }
    }
}