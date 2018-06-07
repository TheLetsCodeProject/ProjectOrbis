using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Orbis
{
    namespace Data
    {

        public class ScoreBoard : MonoBehaviour
        {
            private void Awake()
            {
                UploadScore(new ScoreProfile("test", 10));
                GetHighscores();
                Debug.Log(Highscores[0].Username);
            }
            private List<ScoreProfile> Highscores = new List<ScoreProfile>();
            [SerializeField] private string PrivateKey;
            [SerializeField] private string PublicKey;
            private string serviceAddress = "http://dreamlo.com/lb/";
            private DataFormat format = DataFormat.PIPE;
            private bool doDebug = false;

            #region Constructors
            /// <summary>
            /// Creates an instance of ScoreBoard
            /// </summary>
            /// <param name="Private">The private key used by dreamlo</param>
            /// <param name="Public">The public key used by dreamlo</param>
            /// <param name="form">The type of data to be used, defaults to PIPE</param>
            public ScoreBoard(string Private, string Public, DataFormat form)
            {
                this.PrivateKey = Private;
                this.PublicKey = Public;
                this.format = form;
            }

            /// <summary>
            /// Creates an instance of ScoreBoard
            /// </summary>
            /// <param name="Private">The private key used by dreamlo</param>
            /// <param name="Public">The public key used by dreamlo</param>
            /// <param name="form">The type of data to be used, defaults to PIPE</param>
            /// <param name="_doBebug">Does this log to the console</param>
            public ScoreBoard(string Private, string Public, DataFormat form, bool _doBebug)
            {
                this.PrivateKey = Private;
                this.PublicKey = Public;
                this.format = form;
                this.doDebug = _doBebug;
            }

            /// <summary>
            /// Creates an instance of ScoreBoard
            /// </summary>
            /// <param name="Private">The private key used by dreamlo</param>
            /// <param name="Public">The public key used by dreamlo</param>
            public ScoreBoard(string Private, string Public)
            {
                this.PrivateKey = Private;
                this.PublicKey = Public;
            }

            /// <summary>
            /// Creates an instance of ScoreBoard
            /// </summary>
            /// <param name="Private">The private key used by dreamlo</param>
            /// <param name="Public">The public key used by dreamlo</param>
            /// /// <param name="_doBebug">Does this log to the console</param>
            public ScoreBoard(string Private, string Public, bool _doDebug)
            {
                this.PrivateKey = Private;
                this.PublicKey = Public;
                this.doDebug = _doDebug;
            }
            #endregion

            /// <summary>
            /// Uploads score to dreamlo
            /// </summary>
            /// <param name="username">The name of the user</param>
            /// <param name="score">The user score</param>
            public void UploadScore(string username, int score)
            {
                StartCoroutine(UploadScoreData(username, score));
            }

            /// <summary>
            /// Uploads score to dreamlo
            /// </summary>
            /// <param name="profile">Score profile to upload</param>
            public void UploadScore(ScoreProfile profile)
            {
                StartCoroutine(UploadScoreData(profile.Username, profile.Score));
            }

            /// <summary>
            /// Returns a list of scores.
            /// </summary>
            /// <returns>ScoreProfile()</returns>
            public void GetHighscores()
            {
                StartCoroutine(DownloadScoreData());
            }


            IEnumerator UploadScoreData(string username, int score)
            {
                WWW www = new WWW(serviceAddress + PrivateKey + "/add/test/10");
                yield return www;

                if (string.IsNullOrEmpty(www.error) && doDebug) {
                    Debug.Log("Upload successful");
                } else {
                    Debug.LogError("Request could not be made: " + www.error);
                }
            }

            IEnumerator DownloadScoreData()
            {

                WWW www = new WWW(serviceAddress + PublicKey + "/pipe/");
                yield return www;

                if (string.IsNullOrEmpty(www.error) && doDebug) {
                    FormatData(www.text);
                }
                else {
                    Debug.LogError("Request could not be made: " + www.error);
                }

            }

            private void FormatData(string textStream)
            {
                Highscores.Clear();

                string[] entries = textStream.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < entries.Length; i++) {
                    string[] entryInfo = entries[i].Split(new char[] { '|' });
                    string username = entryInfo[0];
                    int score = int.Parse(entryInfo[1]);
                    Highscores.Add(new ScoreProfile(username, score));
                }
            }
        }

        public enum DataFormat { JSON, XML, PIPE}

    }
}


