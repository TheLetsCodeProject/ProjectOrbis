using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Orbis { namespace Data {

        public struct ScoreProfile
        {
            public string Username;
            public int Score;

            public ScoreProfile(string _user, int _score)
            {
                Username = _user;
                Score = _score;
            }

            public ScoreProfile(int _score)
            {
                Username = "Unknown";
                Score = _score;
            }

            public static implicit operator ScoreProfile(int i)  // implicit digit to byte conversion operator
            {
                return new ScoreProfile(i);
            }

        }
} }

