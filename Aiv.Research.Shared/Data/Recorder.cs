using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;

namespace Aiv.Research.Shared.Data
{
    public class Recorder
    {
        private List<Sample> m_hValues;
        private List<IDataExtractor> m_hInputMembers;
        private List<IDataExtractor> m_hOutputMembers;
        private object m_hTarget;
        private string m_sТекущийфайл;

        public bool Started { get; private set; }

        public Recorder(object hTarget)
        {
            m_hValues = new List<Sample>();

            var hInputMembers = (from m in hTarget.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                                 where m.GetCustomAttribute<NeuralInputAttribute>() != null
                                 select new { Member = m, Attribute = m.GetCustomAttribute<NeuralInputAttribute>() }).OrderBy(x => x.Attribute.Index);

            var hOutputMembers = (from m in hTarget.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                                  where m.GetCustomAttribute<NeuralIdealAttribute>() != null
                                  select new { Member = m, Attribute = m.GetCustomAttribute<NeuralIdealAttribute>() }).OrderBy(x => x.Attribute.Index);

            m_hInputMembers = new List<IDataExtractor>();

            foreach (var item in hInputMembers)
            {
                m_hInputMembers.Add(new FieldDataExtractor(hTarget, item.Member as FieldInfo));
            }


            m_hOutputMembers = new List<IDataExtractor>();

            foreach (var item in hOutputMembers)
            {
                m_hOutputMembers.Add(new FieldDataExtractor(hTarget, item.Member as FieldInfo));
            }

            m_hTarget = hTarget;
        }

        public void Start(string файл)
        {
            if (File.Exists(файл))
                File.Delete(файл);

            m_sТекущийфайл = файл;

            Started = true;
        }

        public void Stop()
        {
            Started = false;

            using (Stream hFs = File.OpenWrite(m_sТекущийфайл))
            {
                XmlSerializer hSerializer = new XmlSerializer(typeof(List<Sample>));
                hSerializer.Serialize(hFs, m_hValues);
            }
        }

        public Sample Update()
        {
            if (Started)
            {
                float[] hВход = Array.ConvertAll(m_hInputMembers.Select(x => x.GetValue()).ToArray(), x => (float)x);
                float[] hВыход = Array.ConvertAll(m_hOutputMembers.Select(x => x.GetValue()).ToArray(), x => (float)x);
                Sample hSample = new Sample(hВход, hВыход);
                m_hValues.Add(hSample);
                return hSample;
            }
            else
                return null;

        }


        private interface IDataExtractor
        {
            double GetValue();
        }
        private class PropertyDataExtractor : IDataExtractor
        {
            private object m_hTarget;
            private PropertyInfo m_hInfo;

            public PropertyDataExtractor(object hTarget, PropertyInfo hInfo)
            {
                m_hTarget = hTarget;
                m_hInfo = hInfo;
            }

            public double GetValue()
            {
                return (double)m_hInfo.GetValue(m_hTarget);
            }
        }
        private class FieldDataExtractor : IDataExtractor
        {
            private object m_hTarget;
            private FieldInfo m_hInfo;
            public FieldDataExtractor(object hTarget, FieldInfo hInfo)
            {
                m_hTarget = hTarget;
                m_hInfo = hInfo;
            }

            public double GetValue()
            {
                return (double)m_hInfo.GetValue(m_hTarget);
            }
        }
    }



}