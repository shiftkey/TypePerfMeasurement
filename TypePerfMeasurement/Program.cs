using System;
using System.Collections.Generic;
using System.Text;
using PerformanceMeasurement;

namespace SimplePerfMeasurement
{
    class Program
    {
        static bool dummy;

        static void Main(string[] args)
        {
            // Measure how long it takes to fetch all the keys exactly once
            MultiSampleCodeTimer timer = new MultiSampleCodeTimer(8, 1000000);
            Console.WriteLine("Data units of msec resolution = " + MultiSampleCodeTimer.ResolutionUsec.ToString("f6") + " usec"); 
            // timer.OnMeasure = MultiSampleCodeTimer.PrintStats; // Uncomment if you want to see detailed stats.

            RuntimeTypeHandle typeHnd = default(RuntimeTypeHandle); 
            Type type = null;
            object anObj = "aString";
            bool result = false;

            // Baseline: fetching the System.Type for a literal type
            timer.Measure("10 typeof(string)                       ", delegate
            {
                type = typeof(string);
                type = typeof(string);
                type = typeof(string);
                type = typeof(string);
                type = typeof(string);
                type = typeof(string);
                type = typeof(string);
                type = typeof(string);
                type = typeof(string);
            });

            // Geting the type RuntimeTypeHandle is much faster!
            timer.Measure("10 typeof(string).TypeHandle            ", delegate
            {
                typeHnd = typeof(string).TypeHandle;
                typeHnd = typeof(string).TypeHandle;
                typeHnd = typeof(string).TypeHandle;
                typeHnd = typeof(string).TypeHandle;
                typeHnd = typeof(string).TypeHandle;
                typeHnd = typeof(string).TypeHandle;
                typeHnd = typeof(string).TypeHandle;
                typeHnd = typeof(string).TypeHandle;
                typeHnd = typeof(string).TypeHandle;
                typeHnd = typeof(string).TypeHandle;
            });

            // Baseline: checking if an object is of a type
            timer.Measure("10 anObj.GetType() == type              ", delegate
            {
                result = anObj.GetType() == type;
                result = anObj.GetType() == type;
                result = anObj.GetType() == type;
                result = anObj.GetType() == type;
                result = anObj.GetType() == type;
                result = anObj.GetType() == type;
                result = anObj.GetType() == type;
                result = anObj.GetType() == type;
                result = anObj.GetType() == type;
                result = anObj.GetType() == type;
            });


            // Doing the same thing with Runtime Type handles is fast  
            timer.Measure("10 Type.GetTypeHandle(obj).Equals(tHnd) ", delegate
            {
                result = Type.GetTypeHandle(anObj).Equals(typeHnd);
                result = Type.GetTypeHandle(anObj).Equals(typeHnd);
                result = Type.GetTypeHandle(anObj).Equals(typeHnd);
                result = Type.GetTypeHandle(anObj).Equals(typeHnd);
                result = Type.GetTypeHandle(anObj).Equals(typeHnd);
                result = Type.GetTypeHandle(anObj).Equals(typeHnd);
                result = Type.GetTypeHandle(anObj).Equals(typeHnd);
                result = Type.GetTypeHandle(anObj).Equals(typeHnd);
                result = Type.GetTypeHandle(anObj).Equals(typeHnd);
                result = Type.GetTypeHandle(anObj).Equals(typeHnd);
            });


            // But if you are checking against a particular type, it is fast.  
            timer.Measure("10 anObj.GetType() == typeof(string)    ", delegate
            {
                result = anObj.GetType() == typeof(string);
                result = anObj.GetType() == typeof(string);
                result = anObj.GetType() == typeof(string);
                result = anObj.GetType() == typeof(string);
                result = anObj.GetType() == typeof(string);
                result = anObj.GetType() == typeof(string);
                result = anObj.GetType() == typeof(string);
                result = anObj.GetType() == typeof(string);
                result = anObj.GetType() == typeof(string);
                result = anObj.GetType() == typeof(string);
            });

            // Even faster than isInst, which is suprising.    
            timer.Measure("10 (anObj is string)                    ", delegate
            {
                result = anObj is string;
                result = anObj is string;
                result = anObj is string;
                result = anObj is string;
                result = anObj is string;
                result = anObj is string;
                result = anObj is string;
                result = anObj is string;
                result = anObj is string;
                result = anObj is string;
            });

            dummy = result;
        }
    }
}
