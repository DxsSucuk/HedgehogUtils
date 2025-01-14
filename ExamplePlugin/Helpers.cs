using RoR2;
using System;
using System.Collections.Generic;

namespace HedgehogUtils
{
    public static class Helpers
    {
        public static string ScepterDescription(string desc)
        {
            return "\n<color=#d299ff>SCEPTER: " + desc + "</color>";
        }

        public static T[] Append<T>(ref T[] array, List<T> list)
        {
            var orig = array.Length;
            var added = list.Count;
            Array.Resize<T>(ref array, orig + added);
            list.CopyTo(array, orig);
            return array;
        }

        public static Func<T[], T[]> AppendDel<T>(List<T> list) => (r) => Append(ref r, list);

        public static bool Flying(ICharacterFlightParameterProvider flight)
        {
            return flight != null && flight.isFlying;
        }
    }
}