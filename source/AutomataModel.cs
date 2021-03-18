using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;

namespace AutomataUI
{
    class AutomataModel
    {
        public List<State> states { get; set; }
        public List<Transition> transitions { get; set; }

        public AutomataModel()
        {
            states = new List<State>();
            transitions = new List<Transition>();
            AddState("Init", 0, new SKPoint(0, 0)); // add default state
        }

        public void AddState(String name, int frames, SKPoint point)
        {
            SKRect bounds = new SKRect(point.X-20,point.Y-20,point.X+20,point.Y+20);

            states.Add(new State()
            {
                ID = RNGCharacterMask(),
                Name = UppercaseFirst(name),
                Duration = frames,
                Bounds = bounds
            });
        }

        public static string RNGCharacterMask()
        {
            int maxSize = 8;
            //int minSize = 5 ;
            char[] chars = new char[62];
            string a;
            a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            { result.Append(chars[b % (chars.Length - 1)]); }
            return result.ToString();
        }
        static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }
    }

    public abstract class UIelement //all UI Elements should be based upon this to make the hittest work
    {
        public SKRect Bounds { get; set; }
        public string Name { get; set; }
    }

    public class State : UIelement
    {
        public string ID { get; set; }

        public bool Active { get; set; }

        public int Duration { get; set; } //how many frames is the state locked

    }
    public class Transition : UIelement
    {
        public State startState { get; set; }

        public State endState { get; set; }

        public int Duration { get; set; }

        public bool IsPingPong { get; set; }

    }
}




