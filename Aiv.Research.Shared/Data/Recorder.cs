using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using System.IO;

namespace Aiv.Research.Shared.Data
{
    public class Recorder
    {
        private List<Sample>            m_hValues;
        private List<IDataExtractor>    m_hInputMembers;
        private List<IDataExtractor>    m_hOutputMembers;
        private object                  m_hTarget;       
        private bool                    m_bStarted;

        public Recorder(object hTarget)
        {
            m_hValues          = new List<Sample>();

            var hInputMembers  = (from m in m_hValues.GetType().GetMembers()
                                 where m.GetCustomAttribute<NeuralInputAttribute>() != null
                                 select new { Member = m, Attribute = m.GetCustomAttribute<NeuralInputAttribute>() }).OrderBy(x => x.Attribute.Index);

            var hOutputMembers = (from m in m_hValues.GetType().GetMembers()
                                 where m.GetCustomAttribute<NeuralIdealAttribute>() != null
                                 select new { Member = m, Attribute = m.GetCustomAttribute<NeuralIdealAttribute>() }).OrderBy(x => x.Attribute.Index);

            m_hInputMembers    = new List<IDataExtractor>();

            foreach (var item in hInputMembers)
            {
                if (item.Member is PropertyInfo)
                    m_hInputMembers.Add(new PropertyDataExtractor(hTarget, item.Member as PropertyInfo));
                else
                    m_hInputMembers.Add(new FieldDataExtractor(hTarget, item.Member as FieldInfo));
            }


            m_hOutputMembers = new List<IDataExtractor>();

            foreach (var item in hOutputMembers)
            {
                if (item.Member is PropertyInfo)
                    m_hOutputMembers.Add(new PropertyDataExtractor(hTarget, item.Member as PropertyInfo));
                else
                    m_hOutputMembers.Add(new FieldDataExtractor(hTarget, item.Member as FieldInfo));
            }

            m_hTarget = hTarget;
        }

        public void Start(string sFilename)
        {
            if (File.Exists(sFilename))
                File.Delete(sFilename);
        }

        public void Stop()
        {
        }

        public void Update()
        {
            
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
