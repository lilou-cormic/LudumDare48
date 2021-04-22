using UnityEngine;

namespace PurpleCable
{
    public class KongregateAPIBehaviour : MonoBehaviour, IStatsAPI
    {
        public static bool IsReady { get; private set; } = false;

        public event System.Action IsReadyChanged;

        private void OnValidate()
        {
            gameObject.name = "KongregateAPI";
        }

#if UNITY_WEBGL
        private static KongregateAPIBehaviour instance;

        public void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            Application.ExternalEval(
              @"if(typeof(kongregateUnitySupport) != 'undefined'){
        kongregateUnitySupport.initAPI('KongregateAPI', 'OnKongregateAPILoaded');
      };"
            );
        }

        public void OnKongregateAPILoaded(string userInfoString)
        {
            OnKongregateUserInfo(userInfoString);

            StatsManager.AddStatAPI(this);
        }

        public void OnKongregateUserInfo(string userInfoString)
        {
            var info = userInfoString.Split('|');
            var userId = System.Convert.ToInt32(info[0]);
            var username = info[1];
            var gameAuthToken = info[2];
            Debug.Log("Kongregate User Info: " + username + ", userId: " + userId);
        }

        public void SubmitStat(string statName, int value)
        {
            Application.ExternalCall("kongregate.stats.submit", statName, value);
        }
#else
        private void Start()
        {
            Destroy(gameObject);
        }

        public void SubmitStat(string statName, int value) { }
#endif
    }
}