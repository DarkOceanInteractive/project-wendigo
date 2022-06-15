using UnityEngine;

namespace UnityWeld.Binding.Adapters
{
    /// <summary>
    /// Adapter for converting from a linear value between 0 and 1 to a decibel value.
    /// </summary>
    [Adapter(typeof(float), typeof(float))]
    public class FloatToDBAdapter : IAdapter
    {
        public object Convert(object value, AdapterOptions options)
        {
            return 20f + Mathf.Log10((float)value) * 20f;
        }
    }

    /// <summary>
    /// Adapter for converting from decibel value to a linear value between 0 and 1.
    /// </summary>
    [Adapter(typeof(float), typeof(float))]
    public class DBToFloatAdapter : IAdapter
    {
        public object Convert(object dBValue, AdapterOptions options)
        {
            return Mathf.Pow(10, ((float)dBValue - 20f) / 20f);
        }
    }
}